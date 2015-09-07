namespace GISA.IntGestDoc.UserInterface
{
    partial class CorrespondenciaSuggestionPickerLstGeog : CorrespondenciaSuggestionPickerList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chLocNroPolicia = new System.Windows.Forms.ColumnHeader();

            // 
            // chLocNroPolicia
            // 
            this.chLocNroPolicia.Text = "Número de polícia";
            this.chLocNroPolicia.Width = 150;

            this.lvCorrepondencias.Columns.Add(this.chLocNroPolicia);   
        }

        #endregion

        private System.Windows.Forms.ColumnHeader chLocNroPolicia;
    }
}
