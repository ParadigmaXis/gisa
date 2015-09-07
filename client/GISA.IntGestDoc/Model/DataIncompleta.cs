using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model
{
    public class DataIncompleta
    {
        internal string AnoInicio { get; set; }
        internal string MesInicio { get; set; }
        internal string DiaInicio { get; set; }
        internal string AnoFim { get; set; }
        internal string MesFim { get; set; }
        internal string DiaFim { get; set; }

        public DataIncompleta(string dataCompleta)
        {
            // exclusivo para as datas vindas do DocInPorto
            var dt = ParseDate(dataCompleta);

            this.AnoInicio = dt.Year.ToString().TrimStart('0');
            this.MesInicio = dt.Month.ToString().TrimStart('0');
            this.DiaInicio = dt.Day.ToString().TrimStart('0');
        }

        public DataIncompleta(string dataCompletaInicio, string dataCompletaFim)
        {
            // exclusivo para as datas vindas do DocInPorto
            if (string.IsNullOrEmpty(dataCompletaInicio))
            {
                this.AnoInicio = "";
                this.MesInicio = "";
                this.DiaInicio = "";
            }
            else
            {
                var dtInicio = ParseDate(dataCompletaInicio);
                this.AnoInicio = dtInicio.Year.ToString().TrimStart('0');
                this.MesInicio = dtInicio.Month.ToString().TrimStart('0');
                this.DiaInicio = dtInicio.Day.ToString().TrimStart('0');
            }

            if (string.IsNullOrEmpty(dataCompletaInicio))
            {
                this.AnoFim = "";
                this.MesFim = "";
                this.DiaFim = "";
            }
            else
            {
                var dtFim = ParseDate(dataCompletaFim);
                this.AnoFim = dtFim.Year.ToString().TrimStart('0');
                this.MesFim = dtFim.Month.ToString().TrimStart('0');
                this.DiaFim = dtFim.Day.ToString().TrimStart('0');
            }
        }

        public DataIncompleta(string anoInicio, string mesInicio, string diaInicio, string anoFim, string mesFim, string diaFim)
        {
            this.AnoInicio = anoInicio.Trim().TrimStart('0');
            this.MesInicio = mesInicio.Trim().TrimStart('0');
            this.DiaInicio = diaInicio.Trim().TrimStart('0');
            this.AnoFim = anoFim.Trim().TrimStart('0');
            this.MesFim = mesFim.Trim().TrimStart('0');
            this.DiaFim = diaFim.Trim().TrimStart('0');
        }

        public override int GetHashCode()
        {
            return this.AnoInicio.GetHashCode() ^ this.MesInicio.GetHashCode() ^ this.DiaInicio.GetHashCode()
                ^ this.AnoFim.GetHashCode() ^ this.MesFim.GetHashCode() ^ this.DiaFim.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is DateTime)
            {
                DateTime dt = (DateTime)obj;

                return dt.Year.ToString().Equals(this.AnoInicio) && dt.Month.ToString().Equals(this.MesInicio) && dt.Day.ToString().Equals(this.DiaInicio);
            }
            else if (obj is DataIncompleta)
            {
                DataIncompleta d = (DataIncompleta)obj;
                return d.AnoInicio.Equals(this.AnoInicio) && d.MesInicio.Equals(this.MesInicio) && d.DiaInicio.Equals(this.DiaInicio)
                    && d.AnoFim.Equals(this.AnoFim) && d.MesFim.Equals(this.MesFim) && d.DiaFim.Equals(this.DiaFim);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return this.AnoInicio + "/" + this.MesInicio + "/" + this.DiaInicio + " — " + this.AnoFim + "/" + this.MesFim + "/" + this.DiaFim;
        }

        public static string FormatDateToString(string date)
        {
            return ParseDate(date).ToShortDateString();
        }

        public static int CompareDates(string date1, string date2)
        {
            return DateTime.Compare(ParseDate(date1), ParseDate(date2));
        }

        private static string[] dateMasks = new String[2] { "dd-MM-yyyy H:mm:ss", "dd-MM-yyyy" };
        internal static DateTime ParseDate(string date){
            try
            {
                return DateTime.ParseExact(date, dateMasks, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            }
            catch (FormatException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
