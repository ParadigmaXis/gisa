using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.GUIHelper;

namespace GISA.Controls.Nivel
{
	public partial class FormAddNivel
	{
		private long mIDTipoNivelRelacionado = long.MinValue;
		public long IDTipoNivelRelacionado
		{
			get {return mIDTipoNivelRelacionado;}
			set {mIDTipoNivelRelacionado = value;}
		}

		protected virtual void UpdateButtonState()
		{
			bool validCodigo = false;
			if (IDTipoNivelRelacionado == long.MinValue)
                validCodigo = GUIHelper.GUIHelper.CheckValidCodigoParcial(txtCodigo.Text);
			else
                validCodigo = GUIHelper.GUIHelper.CheckValidCodigoParcialForTipo(txtCodigo.Text, IDTipoNivelRelacionado);

			btnAccept.Enabled = validCodigo && txtDesignacao.Text.Trim().Length > 0;
		}

		protected virtual void FocusFirstField()
		{
			if (txtCodigo.Enabled && ! txtCodigo.ReadOnly)
				txtCodigo.Focus();
			else if (txtDesignacao.Enabled && ! txtDesignacao.ReadOnly)
				txtDesignacao.Focus();
		}

		public virtual void LoadData()
		{

		}

		protected virtual void SynchronizeTextBoxs()
		{

		}

		protected void AddHandlers()
		{
            txtCodigo.TextChanged += txtCodigo_TextChanged;
			txtDesignacao.TextChanged += txtDesignacao_TextChanged;
		}

		protected void RemoveHandlers()
		{
            txtCodigo.TextChanged -= txtCodigo_TextChanged;
			txtDesignacao.TextChanged -= txtDesignacao_TextChanged;
		}

		private void txtDesignacao_TextChanged(object sender, System.EventArgs e)
		{
			SynchronizeTextBoxs();
			UpdateButtonState();
		}

		private void txtCodigo_TextChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}

		private void FormNivelDocumental_Activated(object sender, System.EventArgs e)
		{
			FocusFirstField();
		}
	}
} //end of root namespace