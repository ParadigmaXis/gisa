using System;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Web;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using GISA.SharedResources;
using GISA.Model;
using GISA.Webservices.DocInPorto;

namespace GISA.Model
{
	public class ImageHelper
	{

	#region  Excepções 
		public class UnsupportedImageTypeException : ApplicationException
		{

			public UnsupportedImageTypeException(string message) : base(message)
			{
			}
		}

		public class ForbiddenResourceException : ApplicationException
		{

			public ForbiddenResourceException(string message, Exception innerException) : base(message, innerException)
			{
			}
		}

		public class UnretrievableResourceException : ApplicationException
		{

			public UnretrievableResourceException(string message, Exception innerException) : base(message, innerException)
			{
			}
		}
	#endregion

	#region  Obtenção de imagens remotas 

        public static Image GetDICImageResource(string locationIdentifier, string otherLocationParameter, ResourceAccessType tipoAcesso)
        {
            Image imageResource = null;

            try
            {
                var location = tipoAcesso == ResourceAccessType.DICAnexo ? DocInPortoHelper.getDocInPortoAnexo(locationIdentifier, otherLocationParameter) : DocInPortoHelper.getDocInPortoConteudo(locationIdentifier);
                if (location == null || location.Length == 0) return null;

                var splits = location.Split('.');
                var extension = splits[splits.Length - 1];
                if (!(isValidImageFileExtension(extension)))
                {
                    System.Diagnostics.Trace.WriteLine("extension: " + extension);
                    imageResource = GetFileIcon(location);
                }
                else
                {
                    FileStream resourceStream = new FileStream(locationIdentifier, FileMode.Open, FileAccess.Read);
                    imageResource = Image.FromStream(resourceStream);
                    resourceStream.Close();
                }
            }
            catch (ForbiddenResourceException ex)
            {
                Trace.WriteLine(ex);
                imageResource = null;

                // accept "forbidden" responses, as they might be dued to our 
                // system's limitations towards using authentication mecanisms 
                // (ie, of a "foreign" web application)
            }
            catch (Exception ex)
            {
                imageResource = null;
                Trace.WriteLine(ex);
                throw new UnretrievableResourceException("The image resource is unavailable", ex);
            }
            return imageResource;
        }

        public static Image GetSmbImageResource(string locationIdentifier)
        {
            Image imageResource = null;

            try
            {
                FileInfo fileResource = null;
                try
                {
                    fileResource = new FileInfo(locationIdentifier);
                }
                catch (System.Security.SecurityException ex)
                {
                    throw new ForbiddenResourceException(string.Format("Not enough permissions to acccess file {0}", locationIdentifier), ex);
                }

                // if the file does not exist report an invalid image
                if (!fileResource.Exists)
                {
                    throw new FileNotFoundException(fileResource.FullName);
                }

                // check if the extension is one of a known image type
                string extension = null;
                extension = fileResource.Extension.Substring(1);
                if (!(isValidImageFileExtension(extension)))
                {
                    imageResource = GetFileIcon(fileResource.Extension);
                }
                else
                {
                    FileStream resourceStream = new FileStream(fileResource.FullName, FileMode.Open, FileAccess.Read);
                    imageResource = Image.FromStream(resourceStream);
                    resourceStream.Close();
                }
            }
            catch (ForbiddenResourceException ex)
            {
                Trace.WriteLine(ex);
                imageResource = null;

                // accept "forbidden" responses, as they might be dued to our 
                // system's limitations towards using authentication mecanisms 
                // (ie, of a "foreign" web application)
            }
            catch (Exception ex)
            {
                imageResource = null;
                Trace.WriteLine(ex);
                throw new UnretrievableResourceException("The image resource is unavailable", ex);
            }
            return imageResource;
        }

        public static Image GetWebImageResource(string locationIdentifier)
        {
            Image imageResource = null;

            try
            {
                Uri url = null;
                WebRequest request = null;
                WebResponse response = null;
                string sUrl;

                if (((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Select()[0])).URLBaseActivo)
                    sUrl = string.Format(((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Select()[0])).URLBase.Replace("<id>", "{0}"), locationIdentifier.Trim());
                else
                    sUrl = locationIdentifier.Trim();

                url = new System.Uri(sUrl);
                if (!(url.Scheme.Equals("http")))
                    throw new UriFormatException();

                // check if the url points to an image resource
                request = WebRequest.Create(url.AbsoluteUri);

                try
                {
                    response = request.GetResponse();
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        int statusCode = 0;
                        statusCode = Convert.ToInt32(((HttpWebResponse)ex.Response).StatusCode);
                        if (statusCode == 401 || statusCode == 403)
                        {
                            // we'd like to distinguish forbidden resources from the other causes of failure
                            throw new ForbiddenResourceException(string.Format("{0} Forbidden: {1}", statusCode, url.AbsoluteUri), ex);
                        }
                        else
                            throw;
                    }
                    else
                        throw;
                }

                // check if the response obtained is an image
                if (!(isValidImageMimeType(response.ContentType)))
                {
                    string fileName = response.ResponseUri.Segments[response.ResponseUri.Segments.Length - 1];
                    string[] fileNameParts = fileName.Split('.');
                    if (fileNameParts.Length == 0)
                        imageResource = GetFileIcon(null);
                    else
                        imageResource = GetFileIcon("*." + fileNameParts[fileNameParts.Length - 1]);
                }
                else
                {
                    Stream resourceStream = response.GetResponseStream();
                    imageResource = Image.FromStream(resourceStream);
                    resourceStream.Close();
                }

                response.Close();
            }
            catch (ForbiddenResourceException ex)
            {
                Trace.WriteLine(ex);
                imageResource = null;

                // accept "forbidden" responses, as they might be dued to our 
                // system's limitations towards using authentication mecanisms 
                // (ie, of a "foreign" web application)
            }
            catch (Exception ex)
            {
                imageResource = null;
                Trace.WriteLine(ex);
                throw new UnretrievableResourceException("The image resource is unavailable", ex);
            }
            return imageResource;
        }

        public static string getAndConvertImageResourceToPng(string url, out bool success, out string errorMessage)
        {
            return getAndConvertImageResourceToPng(url, out success, out errorMessage, false);
        }

        public static string getAndConvertImageResourceToPng(string url, out bool success, out string errorMessage, bool fromRepository)
        {
            string fullFilename = "";
            success = true;
            errorMessage = null;
            var ticks = DateTime.Now.Ticks;

            try
            {
                Uri uri = new Uri(url);
                WebRequest request = WebRequest.Create(uri);
                if (fromRepository)
                {
                    var username = ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).FedoraUsername;
                    var password = ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).FedoraPassword;
                    var credentials = new CredentialCache();
                    
                    credentials.Add(uri, "Basic", new NetworkCredential(username, password));
                    request.Credentials = credentials;
                    request.PreAuthenticate = true;
                    request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(username + ":" + password)));
                }

                WebResponse response = request.GetResponse();
                Stream downloadedStream = response.GetResponseStream();

                string gisaTempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ParadigmaXis\\GISA";
                fullFilename = String.Format("{0}\\{1}.jpg", gisaTempPath, Guid.NewGuid());

                try
                {
                    FileStream fileStream = new FileStream(fullFilename, FileMode.Create);

                    // Copy downloaded stream to file
                    byte[] buffer = new byte[8 * 1024];
                    int len;
                    while ((len = downloadedStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, len);
                    }

                    fileStream.Close();
                    downloadedStream.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    throw new IOException("Erro ao escrever o ficheiro obtido de " + url + ":", ex);
                }

                // If not JPEG, try to convert to JPEG
                using (FileStream fs = new FileStream(fullFilename, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (Image newImage = Image.FromStream(fs, true, false))
                    {
                        if (!newImage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                        {
                            string fullConvertedName = String.Format("{0}\\{1}.jpg", gisaTempPath, Guid.NewGuid());
                            newImage.Save(fullConvertedName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            newImage.Dispose();

                            // Delete original
                            try
                            {
                                if (File.Exists(fullFilename)) File.Delete(fullFilename);
                            }
                            catch (IOException ex)
                            {
                                Trace.WriteLine("Unable to delete: " + fullFilename);
                                Trace.WriteLine(ex.ToString());
                            }                            
                            fullFilename = fullConvertedName;
                        }
                        newImage.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                errorMessage = "De momento, esta imagem encontra-se inacessível. Por favor volte a tentar mais tarde.";
                Trace.WriteLine(ex.ToString());
            }

            Debug.WriteLine(string.Format("<<getAndConvertImageResourceToPng>> url {0}: {1}", url, new TimeSpan(DateTime.Now.Ticks - ticks).ToString()));

            return fullFilename;
        }

        #region Limpeza de ficheiros

            public static void DeleteTempFiles()
            {
                DeleteFilteredFiles("*.pdf");
            }

            public static void DeleteFilteredFiles(string filter)
            {
                string gisaTempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ParadigmaXis\\GISA";
                string[] files = Directory.GetFiles(gisaTempPath, filter);
                bool retVal;
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo info = new FileInfo(file);
                        retVal = FileInUse(info.FullName);

                        if (!retVal) {
                            try { File.Delete(file); }
                            catch (IOException ex) { Trace.WriteLine(String.Format("Erro ao apagar ficheiro temporário {0}. Limpeza do ficheiro adiada. {1}", info.FullName, ex.ToString())); }
                        }
                    }
                    catch (Exception ex) { Trace.WriteLine("Erro durante limpeza de ficheiros temporários: " + ex.ToString()); }
                }
            }

            private static bool FileInUse(string path)
            {
                string message = string.Empty;
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                    }
                    return false;
                }
                catch
                {
                    return true;
                }
            }

        #endregion

        #region Obter ícons de um determinado ficheiro
        // Get Icon
		public static System.UInt32 SHGFI_ICON = Convert.ToUInt32(0X100);

		// Use Passed dwFileAttribute
		public static System.UInt32 SHGFI_USEFILEATTRIBUTES = Convert.ToUInt32(0X10);

		public static System.UInt32 FILE_ATTRIBUTE_NORMAL = Convert.ToUInt32(0X80);

		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO
		{

			public IntPtr hIcon;
			public int iIcon;
			public System.UInt32 dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
			public string szTypeName;

		}

		[DllImport("Shell32.dll")]
		public extern static IntPtr SHGetFileInfo(string pszPath, System.UInt32 dwFileAttributes, ref SHFILEINFO psfi, System.UInt32 cbFileInfo, System.UInt32 uFlags);

		private static Image GetFileIcon(string extensionName)
		{
			// obter o ícone em tamanho grande do ficheiro a associar
			SHFILEINFO shfi = new SHFILEINFO();
			SHGetFileInfo(extensionName, FILE_ATTRIBUTE_NORMAL, ref shfi, System.Convert.ToUInt32(System.Runtime.InteropServices.Marshal.SizeOf(shfi)), Convert.ToUInt32(Convert.ToInt32(SHGFI_USEFILEATTRIBUTES) | Convert.ToInt32(SHGFI_ICON)));

			System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(shfi.hIcon);

			return icon.ToBitmap();
		}
        #endregion

        public static bool isValidImageResource(string fullPath, ResourceAccessType tipoAcesso)
        {
            bool valid = false;
            try
            {
                switch (tipoAcesso)
                {
                    case ResourceAccessType.Web:
                        GetWebImageResource(fullPath);
                        break;
                    case ResourceAccessType.Smb:
                        GetSmbImageResource(fullPath);
                        break;
                    default: 
                        return false;
                }
                valid = true;
            }
            catch (UnretrievableResourceException)
            {
                valid = false;
            }
            return valid;
        }

        public static bool isValidImageResource(string otherLocationParameter, string locationIdentifier, ResourceAccessType tipoAcesso)
		{
			bool valid = false;
			try
			{
                var img = GetDICImageResource(locationIdentifier, otherLocationParameter, tipoAcesso);
				valid = true;
			}
			catch (UnretrievableResourceException)
			{
				valid = false;
			}
			return valid;
		}

		private static bool isValidImageFileExtension(string extension)
		{
			foreach (ImageCodecInfo cdc in ImageCodecInfo.GetImageDecoders())
			{
				foreach (string allowedExtension in cdc.FilenameExtension.Replace("*.", "").Split(';'))
				{

					if (string.Compare(extension, allowedExtension, true) == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		private static bool isValidImageMimeType(string mimeType)
		{
			foreach (ImageCodecInfo cdc in ImageCodecInfo.GetImageDecoders())
			{
				if (string.Compare(mimeType, cdc.MimeType, true) == 0)
				{
					return true;
				}
			}
			return false;
		}
	#endregion

    #region  Manipulação de imagens 
		private static Size mZoomStep = new Size(25, 25);
		public static Size ZoomStep
		{
			get
			{
				return mZoomStep;
			}
		}
		public static Size getSizeSameAspectRatio(Size imageSize, Size availableSize)
		{

			// protect agains too much zoom out
			if (imageSize.Width < ZoomStep.Width | imageSize.Height < ZoomStep.Height)
			{
				return imageSize;
			}

			Size resultSize = new Size();

			// sx and sy are the ratios (for width and height)
			// between panel and image dimensions
			double sx = availableSize.Width / (double)imageSize.Width;
			double sy = availableSize.Height / (double)imageSize.Height;
			if (sx < sy)
			{
				resultSize.Width = availableSize.Width;
				resultSize.Height = System.Convert.ToInt32(imageSize.Height * sx);
			}
			else
			{
				resultSize.Width = System.Convert.ToInt32(imageSize.Width * sy);
				resultSize.Height = availableSize.Height;
			}

			Debug.Write("get:" + resultSize.Width.ToString() + " " + resultSize.Height.ToString() + System.Environment.NewLine);

			return resultSize;
		}
	#endregion

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().ToArray().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        public static Image SaveAsJpeg(Image Img, MemoryStream ms, Int64 Quality)
        {
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            var QualityEncoder = System.Drawing.Imaging.Encoder.Quality;

            using (EncoderParameters EP = new EncoderParameters(1))
            {
                using (EncoderParameter QualityEncoderParameter = new EncoderParameter(QualityEncoder, Quality))
                {
                    EP.Param[0] = QualityEncoderParameter;
                    Img.Save(ms, jgpEncoder, EP);
                }
            }
            return Image.FromStream(ms);
        }

        private static int QUALITY = 100;
        public static byte[] ConvertToJpegFormat(byte[] img, out Image imgJpeg)
        {
            imgJpeg = null;
            MemoryStream ms = new MemoryStream(img);
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            Image img1 = Image.FromStream(ms);

            if (img1.Width > IMAGEWIDTH || img1.Height > IMAGEHEIGHT || img1.HorizontalResolution > RATIOHORIZONTAL || img1.VerticalResolution > RATIOVERTICAL)
                img1 = AjustImage(img1);

            MemoryStream ms2 = new MemoryStream();
            imgJpeg = SaveAsJpeg(img1, ms2, QUALITY);
            return ms2.ToArray();
        }

        private static double RATIOVERTICAL = 0.55;
        private static double RATIOHORIZONTAL = 1;
        private static int IMAGEWIDTH = 800;
        private static int IMAGEHEIGHT = 800;
        private static double RATIOGOLD = 0.707;
        private static int DESIRED_DPI = 100;
        private static Image AjustImage(Image img)
        {
            try
            {
                // Bitmap image = (Bitmap)Bitmap.FromFile(baseImageLocation);
                // Bitmap.FromFile incorrectly locks the file and prevents removal later on
                // To circumvent this, we load the image using a stream
                /* Stream stream = File.OpenRead(baseImageLocation);
                Bitmap image = (Bitmap)Bitmap.FromStream(stream);
                stream.Close(); */


                Bitmap image = (Bitmap)img;
                float ratio = (float)image.Width / (float)image.Height;
                int imgWidth, imgHeight;

                if (ratio >= RATIOVERTICAL && ratio <= RATIOHORIZONTAL)
                {
                    if (ratio > RATIOGOLD)
                    {
                        imgWidth = IMAGEWIDTH;
                        imgHeight = imgWidth * image.Height / image.Width;
                    }
                    else
                    {
                        imgHeight = IMAGEHEIGHT;
                        imgWidth = imgHeight * image.Width / image.Height;
                    }
                    image = ResizeImage(image, imgWidth, imgHeight, DESIRED_DPI);
                }
                else
                {
                    if (ratio > RATIOHORIZONTAL)
                    {
                        imgHeight = IMAGEHEIGHT;
                        imgWidth = imgHeight * image.Width / image.Height;
                        image = ResizeImage(image, imgWidth, imgHeight, DESIRED_DPI);
                        image = CropImage(image, new Rectangle((image.Width / 2) - (IMAGEWIDTH / 2), 0, IMAGEWIDTH, image.Height));
                    }
                    else
                    {
                        imgWidth = IMAGEWIDTH;
                        imgHeight = imgWidth * image.Height / image.Width;
                        image = ResizeImage(image, imgWidth, imgHeight, DESIRED_DPI);
                        image = CropImage(image, new Rectangle(0, (image.Height / 2) - (IMAGEHEIGHT / 2), image.Width, IMAGEHEIGHT));
                    }
                }

                return image as Image;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ajustment failed.", ex);
            }
        }

        public static Bitmap ResizeImage(Bitmap srcImage, int newWidth, int newHeight, int desiredDpi)
        {
            try
            {
                Bitmap newImage = new Bitmap(newWidth, newHeight);
                if (desiredDpi > 0)
                    newImage.SetResolution(desiredDpi, desiredDpi);

                using (Graphics gr = Graphics.FromImage(newImage))
                {
                    gr.SmoothingMode = SmoothingMode.AntiAlias;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                }

                return newImage;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Resizing failed.", ex);
            }
        }

        public static Bitmap CropImage(Bitmap srcImage, Rectangle croppedArea)
        {
            try
            {
                Bitmap newImage = srcImage.Clone(croppedArea, srcImage.PixelFormat);
                return newImage;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Cropping failed.", ex);
            }
        }
	}
}