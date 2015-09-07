using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Text;
using GISA.Reports;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public partial class FormCustomizableReports
	{

	#region Properties
		private string mSelectClause = string.Empty;
		public string SelectClause
		{
			get
			{
				return mSelectClause;
			}
			set
			{
				mSelectClause = value;
			}
		}

		private string mJoinClause = string.Empty;
		public string JoinClause
		{
			get
			{
				return mJoinClause;
			}
			set
			{
				mJoinClause = value;
			}
		}

		private string mWhereClause = string.Empty;
		public string WhereClause
		{
			get
			{
				return mWhereClause;
			}
			set
			{
				mWhereClause = value;
			}
		}
	#endregion

		// Adicionar campos à lista
		public void AddParameters(List<ReportParameter> parameters)
		{
			ListViewItem item = null;
			foreach (ReportParameter param in parameters)
			{
				item = new ListViewItem();
				item.Text = Relatorio.GetParameterName(param);
				item.Tag = param;
				ListView1.Items.Add(item);
			}
			ListView1.ArrangeIcons(ListViewAlignment.SnapToGrid);
		}

		public List<ReportParameter> GetSelectedParameters()
		{
			List<ReportParameter> parameters = new List<ReportParameter>();
			foreach (ListViewItem item in ListView1.Items)
			{
				if (item.Checked)
					parameters.Add((ReportParameter)item.Tag);
			}
			return parameters;
		}

		private void BuildReportQuery(List<ReportParameter> parameters)
		{
			List<string> fields = new List<string>();
			List<string> joins = new List<string>();
			List<string> wheres = new List<string>();

			foreach (ReportParameter parameter in parameters)
			{
				foreach (string field in parameter.DBField)
				{
					InsertIfNotExist(ref fields, field);
				}

				foreach (string field in parameter.DBField)
				{
					InsertIfNotExist(ref joins, field);
				}

				foreach (string field in parameter.DBField)
				{
					InsertIfNotExist(ref wheres, field);
				}
			}

			mSelectClause = BuildClause(fields);
			mJoinClause = BuildClause(joins);
			mWhereClause = BuildClause(wheres);
		}

		private void InsertIfNotExist(ref List<string> lista, string elemento)
		{
			if (! (lista.Contains(elemento)))
			{
				lista.Add(elemento);
			}
		}

		private string BuildClause(List<string> lista)
		{
			StringBuilder clause = new StringBuilder();
			foreach (string elemento in lista)
			{
				if (clause.Length > 0)
				{
					clause.Append(", ");
				}

				clause.Append(elemento);
			}
			return clause.ToString();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			List<ReportParameter> parameters = new List<ReportParameter>();
			parameters = GetSelectedParameters();
			BuildReportQuery(parameters);
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			UpdateCheckBoxes(true);
		}

		private void Button4_Click(object sender, System.EventArgs e)
		{
			UpdateCheckBoxes(false);
		}

		private void UpdateCheckBoxes(bool val)
		{
			foreach (ListViewItem item in ListView1.Items)
			{
				item.Checked = val;
			}
		}
	}
} //end of root namespace