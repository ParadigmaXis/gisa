using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientPesquisaRule: PesquisaRule
	{
        #region " Listas Paginadas "
		public override string CollateOptionCIAI
		{
			get
			{
				return "COLLATE LATIN1_GENERAL_CI_AI";
			}
		}

		public override string CollateOptionCIAIEscape
		{
			get
			{
				return "COLLATE LATIN1_GENERAL_CI_AI ESCAPE '\\'";
			}
		}
		#endregion

		public override string sanitizeSearchTerm(string str, bool addWildcardsToExtremities) {
			//escapar os caracteres especiais usados em em pressões regulares de SqlServer
			str = str.Replace("[", "[[]");

			//escapar outros caracteres importantes
			str = str.Replace("'", "''");

			if (addWildcardsToExtremities) {
				//garantir que não existem wildcards nas pontas
				str = str.Trim(' ', '%');

				//adicionar wildcards nas pontas
				str = string.Format("%{0}%", str);
			}

			return str;
		}

        public override string sanitizeSearchTerm_WithoutWidcards(string str)
        {
            //escapar os caracteres especiais usados em em pressões regulares de SqlServer
            str = str.Replace("[", "[[]");

            //escapar outros caracteres importantes
            str = str.Replace("'", "''");
            return str;
        }

		public override string buildLikeStatement(string str1, string str2)
		{
			return str1 + " " + CollateOptionCIAI + " LIKE " + str2 + " " + CollateOptionCIAIEscape;
		}

		#region " SlavePanelPesquisa "

        public override void LoadSelectedData(DataSet currentDataSet, long IDNivel, long IDTipoFRDBase, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@IDNivel", IDNivel);
                command.Parameters.AddWithValue("@IDTipoFRDBase", IDTipoFRDBase);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "WHERE ID=@IDNivel");
                da.Fill(currentDataSet, "Nivel");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel=@IDNivel AND IDTipoFRDBase=@IDTipoFRDBase");
				da.Fill(currentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN NivelControloAut nca ON nca.IDControloAut = ControloAut.ID " +
                    "WHERE nca.ID=@IDNivel");
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDatasExistencia"],
                    "INNER JOIN NivelControloAut nca ON nca.IDControloAut = ControloAutDatasExistencia.IDControloAut " +
                    "WHERE nca.ID=@IDNivel");
                da.Fill(currentDataSet, "ControloAutDatasExistencia");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                    "WHERE ID=@IDNivel");
                da.Fill(currentDataSet, "NivelControloAut");
			}
		}

		public override void LoadImagemVolume(DataSet currentDataSet, long frdID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@frdID", frdID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemVolume"],
                    string.Format("INNER JOIN (SELECT DISTINCT IDSFDImagemVolume FROM SFRDImagem WHERE IDFRDBase=@frdID) SFRDImagem ON SFRDImagem.IDSFDImagemVolume = SFRDImagemVolume.ID", frdID.ToString()));
				da.Fill(currentDataSet, "SFRDImagemVolume");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagem"],
                    "WHERE IDFRDBase=@frdID");
				da.Fill(currentDataSet, "SFRDImagem");
			}
		}

        public override List<UFRule.UFsAssociadas> LoadDetalhesUF(DataSet currentDataSet, string frdID, IDbConnection conn)
		{
            List<UFRule.UFsAssociadas> result = new List<UFRule.UFsAssociadas>();
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@frdID", frdID);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@eliminado", 0);

                command.CommandText =
                    "SELECT nEDs.Codigo + '/' + nUFs.Codigo Codigo, ndUFs.Designacao, dp.InicioAno, dp.InicioMes, dp.InicioDia, dp.InicioAtribuida, dp.FimAno, dp.FimMes, dp.FimDia, dp.FimAtribuida, c.Cota, sfrduf.Cota " +
                    "FROM SFRDUnidadeFisica sfrduf " +
                        "INNER JOIN Nivel nUFs ON nUFs.ID = sfrduf.IDNivel AND nUFs.isDeleted = @isDeleted " +
                        "INNER JOIN NivelDesignado ndUFs ON ndUFs.ID = nUFs.ID AND ndUFs.isDeleted = @isDeleted " +
                        "LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = nUFs.ID AND nuf.isDeleted = @isDeleted AND (nuf.Eliminado = @eliminado OR nuf.Eliminado IS NULL) " +
                        "INNER JOIN RelacaoHierarquica rh ON rh.ID = nUFs.ID AND rh.isDeleted = @isDeleted " +
                        "INNER JOIN Nivel nEDs ON nEDs.ID = rh.IDUpper AND nEDs.isDeleted = @isDeleted " +
                        "INNER JOIN FRDBase frd ON frd.IDNivel = nUFs.ID AND frd.isDeleted = @isDeleted " +
                        "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID AND dp.isDeleted = @isDeleted " +
                        "LEFT JOIN SFRDUFCota c ON c.IDFRDBase = frd.ID AND c.isDeleted = @isDeleted " +
                    "WHERE sfrduf.IDFRDBase = @frdID AND sfrduf.isDeleted = @isDeleted ";
                SqlDataReader reader = command.ExecuteReader();

                UFRule.UFsAssociadas ufAssociada;
                while (reader.Read())
                {
                    ufAssociada = new UFRule.UFsAssociadas();
                    ufAssociada.Codigo = reader.GetValue(0).ToString();
                    ufAssociada.Designacao = reader.GetValue(1).ToString();
                    ufAssociada.DPInicioAno = reader.GetValue(2).ToString();
                    ufAssociada.DPInicioMes = reader.GetValue(3).ToString();
                    ufAssociada.DPInicioDia = reader.GetValue(4).ToString();
                    if (reader.GetValue(5) != DBNull.Value)
                        ufAssociada.DPInicioAtribuida = System.Convert.ToBoolean(reader.GetValue(5));
                    else
                        ufAssociada.DPInicioAtribuida = false;
                    ufAssociada.DPFimAno = reader.GetValue(6).ToString();
                    ufAssociada.DPFimMes = reader.GetValue(7).ToString();
                    ufAssociada.DPFimDia = reader.GetValue(8).ToString();
                    if (reader.GetValue(9) != DBNull.Value)
                        ufAssociada.DPFimAtribuida = System.Convert.ToBoolean(reader.GetValue(9));
                    else
                        ufAssociada.DPInicioAtribuida = false;
                    ufAssociada.Cota = reader.GetValue(10).ToString();
                    ufAssociada.CotaDocumento = reader.GetValue(11).ToString();
                    result.Add(ufAssociada);
                }
                reader.Close();
            }

			return result;
		}

		public override void LoadFRDBaseData(DataSet currentDataSet, string id, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@id", id);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"], 
					"WHERE ID IN (SELECT IDControloAut " +
                    "FROM IndexFRDCA WHERE IDFRDBase=@id)");
				da.Fill(currentDataSet, "ControloAut");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"],
                    "WHERE IDFRDBase=@id");
				da.Fill(currentDataSet, "IndexFRDCA");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"], 
					"WHERE ID IN (SELECT IDDicionario " + 
					"FROM ControloAutDicionario WHERE IDControloAut IN " + 
					"(SELECT IDControloAut FROM IndexFRDCA " +
                    "WHERE IDFRDBase=@id))");
				da.Fill(currentDataSet, "Dicionario");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"], 
					"WHERE IDControloAut IN (SELECT IDControloAut " +
                    "FROM IndexFRDCA WHERE IDFRDBase=@id)");
				da.Fill(currentDataSet, "ControloAutDicionario");
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAgrupador"],
                    "WHERE IDFRDBase=@id");
                da.Fill(currentDataSet, "SFRDAgrupador");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDContexto"],
                    "WHERE IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDContexto");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDConteudoEEstrutura"],
                    "WHERE IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDConteudoEEstrutura");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDocumentacaoAssociada"],
                    "WHERE IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDDocumentacaoAssociada");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDCondicaoDeAcesso"],
                    "WHERE IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDCondicaoDeAcesso");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDNotaGeral"],
                    "WHERE IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDNotaGeral");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDimensaoSuporte"],
                    "WHERE IDFRDBase=@id");
                da.Fill(currentDataSet, "SFRDDimensaoSuporte");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Iso639"], 
					"INNER JOIN SFRDLingua ON Iso639.ID = SFRDLingua.IDIso639 " +
                    "WHERE SFRDLingua.IDFRDBase=@id");
				da.Fill(currentDataSet, "Iso639");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDLingua"],
                    "WHERE SFRDLingua.IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDLingua");
				
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Iso15924"], 
					"INNER JOIN SFRDAlfabeto ON Iso15924.ID = SFRDAlfabeto.IDIso15924 " +
                    "WHERE SFRDAlfabeto.IDFRDBase=@id");
				da.Fill(currentDataSet, "Iso15924");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAlfabeto"],
                    "WHERE SFRDAlfabeto.IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDAlfabeto");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoFormaSuporteAcond"], 
					"INNER JOIN SFRDFormaSuporteAcond ON TipoFormaSuporteAcond.ID = SFRDFormaSuporteAcond.IDTipoFormaSuporteAcond " +
                    "WHERE SFRDFormaSuporteAcond.IDFRDBase=@id");
				da.Fill(currentDataSet, "TipoFormaSuporteAcond");				
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDFormaSuporteAcond"],
                    "WHERE SFRDFormaSuporteAcond.IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDFormaSuporteAcond");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoMaterialDeSuporte"], 
					"INNER JOIN SFRDMaterialDeSuporte ON TipoMaterialDeSuporte.ID = SFRDMaterialDeSuporte.IDTipoMaterialDeSuporte " +
                    "WHERE SFRDMaterialDeSuporte.IDFRDBase=@id");
				da.Fill(currentDataSet, "TipoMaterialDeSuporte");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDMaterialDeSuporte"],
                    "WHERE SFRDMaterialDeSuporte.IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDMaterialDeSuporte");
				

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoTecnicasDeRegisto"], 
					"INNER JOIN SFRDTecnicasDeRegisto ON TipoTecnicasDeRegisto.ID = SFRDTecnicasDeRegisto.IDTipoTecnicasDeRegisto " +
                    "WHERE SFRDTecnicasDeRegisto.IDFRDBase=@id");
				da.Fill(currentDataSet, "TipoTecnicasDeRegisto");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDTecnicasDeRegisto"],
                    "WHERE SFRDTecnicasDeRegisto.IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDTecnicasDeRegisto");
				
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoEstadoDeConservacao"], 
					"INNER JOIN SFRDEstadoDeConservacao ON TipoEstadoDeConservacao.ID = SFRDEstadoDeConservacao.IDTipoEstadoDeConservacao " +
                    "WHERE SFRDEstadoDeConservacao.IDFRDBase=@id");
				da.Fill(currentDataSet, "TipoEstadoDeConservacao");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDEstadoDeConservacao"],
                    "WHERE SFRDEstadoDeConservacao.IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDEstadoDeConservacao");


				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"], 
					"INNER JOIN FRDBaseDataDeDescricao On FRDBaseDataDeDescricao.IDTrusteeOperator = Trustee.ID " +
                    "WHERE FRDBaseDataDeDescricao.IDFRDBase=@id");
				da.Fill(currentDataSet, "Trustee");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"], 
					"INNER JOIN FRDBaseDataDeDescricao On FRDBaseDataDeDescricao.IDTrusteeOperator = TrusteeUser.ID " +
                    "WHERE FRDBaseDataDeDescricao.IDFRDBase=@id");
				da.Fill(currentDataSet, "TrusteeUser");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBaseDataDeDescricao"],
                    "WHERE FRDBaseDataDeDescricao.IDFRDBase=@id");
				da.Fill(currentDataSet, "FRDBaseDataDeDescricao");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAvaliacao"],
                    "WHERE SFRDAvaliacao.IDFRDBase=@id");
				da.Fill(currentDataSet, "SFRDAvaliacao");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"],
                    "WHERE SFRDDatasProducao.IDFRDBase=@id");
                da.Fill(currentDataSet, "SFRDDatasProducao");
			}
		}

		public override void LoadRHParentsSelectedResult(DataSet currentDataSet, long NivelID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@NivelID", NivelID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
					"INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = Nivel.ID " +
                    "WHERE RelacaoHierarquica.ID = @NivelID");
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    "WHERE ID = @NivelID");
				da.Fill(currentDataSet, "RelacaoHierarquica");
			}
		}

        public override List<TermosIndexacao> GetTermosIndexacao(long NivelID, IDbConnection conn) {
            List<TermosIndexacao> ret = new List<TermosIndexacao>();

            try {
                string query = string.Format(@"
                    SELECT dict.Termo, OutrasFormas = LEFT(o1.list, LEN(o1.list)-1), idx.Selector, ca.IDTipoNoticiaAut
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN IndexFRDCA idx ON idx.IDFRDBase = frd.ID AND idx.isDeleted = 0
                    INNER JOIN ControloAut ca ON ca.ID = idx.IDControloAut  AND ca.IDTipoNoticiaAut IN (1, 2, 3) AND ca.isDeleted = 0
                    INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = idx.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                    INNER JOIN Dicionario dict ON dict.ID = cad.IDDicionario  AND cad.isDeleted = 0
                    CROSS APPLY ( 
                        SELECT Termo + '; ' AS [text()] 
                        FROM Dicionario d
		                    INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma <> 1 AND cad.isDeleted = 0
                        WHERE d.isDeleted = 0 AND cad.IDControloAut = ca.ID
                        ORDER BY d.Termo
                        FOR XML PATH('') 
                    ) o1 (list)
                    WHERE n.ID = {0}  AND n.isDeleted = 0 
                    ORDER BY ca.IDTipoNoticiaAut, dict.Termo ", NivelID);

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)conn))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.Parameters.AddWithValue("@NivelID", NivelID);
                    command.Parameters.AddWithValue("@IDTipoControloAutForma1", 1);
                    command.Parameters.AddWithValue("@IDTipoNoticiaAut1", 1);
                    command.Parameters.AddWithValue("@IDTipoNoticiaAut2", 2);
                    command.Parameters.AddWithValue("@IDTipoNoticiaAut3", 3);
                    command.CommandText = @"
SELECT dict.Termo, OutrasFormas = LEFT(o1.list, LEN(o1.list)-1), idx.Selector, ca.IDTipoNoticiaAut
FROM Nivel n
    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = @isDeleted
    INNER JOIN IndexFRDCA idx ON idx.IDFRDBase = frd.ID AND idx.isDeleted = @isDeleted
    INNER JOIN ControloAut ca ON ca.ID = idx.IDControloAut  AND ca.IDTipoNoticiaAut IN (@IDTipoNoticiaAut1, @IDTipoNoticiaAut2, @IDTipoNoticiaAut3) AND ca.isDeleted = @isDeleted
    INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = idx.IDControloAut AND cad.IDTipoControloAutForma = @IDTipoControloAutForma1 AND cad.isDeleted = @isDeleted
    INNER JOIN Dicionario dict ON dict.ID = cad.IDDicionario  AND cad.isDeleted = @isDeleted
    CROSS APPLY ( 
        SELECT Termo + '; ' AS [text()] 
        FROM Dicionario d
		    INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma <> @IDTipoControloAutForma1 AND cad.isDeleted = @isDeleted
        WHERE d.isDeleted = 0 AND cad.IDControloAut = ca.ID
        ORDER BY d.Termo
        FOR XML PATH('') 
    ) o1 (list)
WHERE n.ID = @NivelID AND n.isDeleted = @isDeleted
ORDER BY ca.IDTipoNoticiaAut, dict.Termo ";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TermosIndexacao _new = new TermosIndexacao();
                        _new.Termo = string.Empty;
                        _new.Outras_Formas = string.Empty;
                        _new.IDTipoNoticiaAut = -1;
                        _new.IndexFRDCA_Selector = -1000;

                        if (!reader.IsDBNull(0))
                            _new.Termo = reader.GetString(0);
                        if (!reader.IsDBNull(1))
                            _new.Outras_Formas = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            _new.IndexFRDCA_Selector = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            _new.IDTipoNoticiaAut = reader.GetInt64(3);

                        ret.Add(_new);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return ret;
        }

        public override List<string> LoadDocumentoCotas(string IDFRDbase, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = string.Format(@"
SELECT COALESCE(c.Cota, ''), COALESCE(sfrduf.Cota, '')
FROM FRDBase frd
	LEFT JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDFRDBase = frd.ID AND sfrduf.isDeleted = 0
	LEFT JOIN Nivel nuf ON nuf.ID = sfrduf.IDNivel AND nuf.isDeleted = 0
    LEFT JOIN FRDBase frduf ON frduf.IDNivel = nuf.ID AND frduf.isDeleted = 0
    LEFT JOIN SFRDUFCota c ON c.IDFRDBase = frduf.ID AND c.isDeleted = 0
WHERE frd.ID = {0}", IDFRDbase.ToString());
            var reader = command.ExecuteReader();

            var result = new List<string>();
            while (reader.Read())
            {
                string cota = string.Empty;
                var cota1 = reader.GetString(0);
                var cota2 = reader.GetString(1);
                if (cota1.Length > 0 && cota2.Length == 0)
                    result.Add(cota1);
                else if (cota1.Length == 0 && cota2.Length > 0)
                    result.Add(cota2);
                else if (cota1.Length > 0 && cota2.Length > 0)
                    result.Add(cota1 + " - " + cota2);                
            }
            
            reader.Close();

            return result;
        }

        public override long CountSubDocumentos(long IDNivel, IDbConnection conn)
        {
            var res = new List<NivelRule.NivelDocumentalListItem>();
            using (var command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@IDNivel", IDNivel);
                command.Parameters.AddWithValue("@IDTipoFRDBase1", 1);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado10", 10);

                command.CommandText = @"
SELECT COUNT(rh.ID)
FROM RelacaoHierarquica rh
	INNER JOIN NivelDesignado nd ON nd.ID = rh.ID AND nd.isDeleted = @isDeleted
	INNER JOIN FRDBase frd ON frd.IDNivel = rh.ID AND frd.IDTipoFRDBase = @IDTipoFRDBase1 AND frd.isDeleted = @isDeleted
	INNER JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID AND dp.isDeleted = @isDeleted
WHERE rh.IDUpper = @IDNivel AND rh.IDTipoNivelRelacionado = @IDTipoNivelRelacionado10 AND rh.isDeleted = @isDeleted";
                return System.Convert.ToInt64(command.ExecuteScalar());
            }
        }

        public override List<NivelRule.NivelDocumentalListItem> GetSubDocumentos(long IDNivel, IDbConnection conn)
        {
            var res = new List<NivelRule.NivelDocumentalListItem>();
            using (var command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@IDNivel", IDNivel);
                command.Parameters.AddWithValue("@IDTipoFRDBase1", 1);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado10", 10);

                command.CommandText = @"
SELECT rh.ID, nd.Designacao, dp.InicioAno, dp.InicioMes, dp.InicioDia, dp.InicioAtribuida, dp.FimAno, dp.FimMes, dp.FimDia, dp.FimAtribuida, rh.IDTipoNivelRelacionado
FROM RelacaoHierarquica rh
	INNER JOIN NivelDesignado nd ON nd.ID = rh.ID AND nd.isDeleted = @isDeleted
	INNER JOIN FRDBase frd ON frd.IDNivel = rh.ID AND frd.IDTipoFRDBase = @IDTipoFRDBase1 AND frd.isDeleted = @isDeleted
	INNER JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID AND dp.isDeleted = @isDeleted
WHERE rh.IDUpper = @IDNivel AND rh.IDTipoNivelRelacionado = @IDTipoNivelRelacionado10 AND rh.isDeleted = @isDeleted
ORDER BY rh.ID";
                var reader = command.ExecuteReader();
                var sdoc = new NivelRule.NivelDocumentalListItem();
                while (reader.Read())
                {
                    sdoc = new NivelRule.NivelDocumentalListItem();
                    sdoc.IDNivel = reader.GetInt64(0);
                    sdoc.Designacao = reader.GetString(1);
                    sdoc.InicioAno = reader.GetValue(2).ToString();
                    sdoc.InicioMes = reader.GetValue(3).ToString();
                    sdoc.InicioDia = reader.GetValue(4).ToString();
                    sdoc.InicioAtribuida = reader.GetBoolean(5);
                    sdoc.FimAno = reader.GetValue(6).ToString();
                    sdoc.FimMes = reader.GetValue(7).ToString();
                    sdoc.FimDia = reader.GetValue(8).ToString();
                    sdoc.FimAtribuida = reader.GetBoolean(9);
                    sdoc.IDTipoNivelRelacionado = reader.GetInt64(10);
                    res.Add(sdoc);
                }
                reader.Close();
            }
            return res;
        }
		#endregion

		#region " PesquisaList "
        public override void CalculateOrderedItems(ArrayList ordenacao, List<string> IDs, Int64? IDNivelEstrutura, long userID, bool SoDocExpirados, bool _newSearch, out long nrResults, IDbConnection conn)
		{
            // NOTAS: a lista IDs por vir:
            //  - a null quando a pesquisa é por estrutura e, para além do nivel seleccionado, não existe mais nenhum critério definido
            //  - vazia quando não foi encontrado qualquer documento
            //  - com dados quando foram encontrados documentos
            //
            //        o IDNivelEstrutura só vem diferente de null quando se utiliza o critério pesquisa por estrutura

            nrResults = 0;

            if (!_newSearch && MesmaOrdenacao(ordenacao)) return;

            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                // criar tabela para calcular a ordenação dos items a serem apresentados na pesquisa
                command.CommandText =
                            " IF OBJECT_ID(N'tempdb..#Search_Struct', N'U') IS NOT NULL " +
                                "DROP TABLE #Search_Struct " +
                            "CREATE TABLE #Search_Struct (ID BIGINT, gen INT); " +
                            "CREATE TABLE #OrderedItems (seq_id INT Identity(1,1) NOT NULL, IDNivel BIGINT NOT NULL ); " +
                            "CREATE TABLE #SPParametersNiveis (IDNivel BIGINT); " +
                            "CREATE TABLE #SPResultsCodigos(IDNivel BIGINT, CodigoCompleto NVARCHAR(300)); " +
                            "CREATE TABLE #filteredIDs (IDNivel BIGINT); ";
                command.ExecuteNonQuery();

                // só no caso de uma nova pesquisa é que se filtram os resultados por permissão de leitura e por documentos expirados se a opção estiver activa
                if (_newSearch)
                {
                    var long_IDs = new long[] { };
                    if (IDs != null)
                    {
                        // inserir, numa tabela temporária, os IDs do resultado vindo do servidor de pesquisa
                        long_IDs = IDs.ConvertAll<long>(new Converter<string, long>(delegate(string id) { return System.Convert.ToInt64(id); })).ToArray();
                    }

                    // mesmo que a lista IDs venha a null convém correr este método porque cria a tabela #temp que é usada mais abaixo
                    GisaDataSetHelperRule.ImportIDs(long_IDs, conn);
                    var table = "#temp";

                    if (IDNivelEstrutura != null)
                    {
                        command.CommandText = "Search_Estrutura";
                        command.Parameters.Add("@TrusteeID", SqlDbType.Decimal);
                        command.Parameters[0].Value = userID.ToString();
                        command.Parameters.Add("@NivelID", SqlDbType.Decimal);
                        command.Parameters[1].Value = IDNivelEstrutura.ToString();
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();

                        command.CommandType = CommandType.Text;
                        command.Parameters.Clear();

                        if (IDs != null)
                            table = "(SELECT N.ID FROM #temp N INNER JOIN #Search_Struct S ON S.ID = N.ID)";
                        else
                        {
                            table = "#Search_Struct";

                            PermissoesRule.Current.GetEffectiveReadPermissions(" FROM #Search_Struct ", userID, conn);

                            command.CommandText = "DELETE FROM #Search_Struct WHERE ID IN (SELECT IDNivel FROM #effective WHERE Ler = @Ler OR Ler IS NULL)";
                            command.Parameters.AddWithValue("@Ler", 0);
                            command.ExecuteNonQuery();

                            PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);
                        }
                    }

                    // inserir os IDs na cache com permissão de leitura 
                    long start = DateTime.Now.Ticks;
                    command.CommandText = string.Format(
                        "{0}" +                        
                        "INSERT INTO #filteredIDs " +
                        "SELECT E.ID " +
                        "FROM {2} E " +
                        "{1} ",
                        SoDocExpirados ? "DECLARE @now DATETIME; SET @now = GETDATE(); " : "",
                        SoDocExpirados ? "INNER JOIN FRDBase frd ON frd.IDNivel = E.ID AND dbo.fn_IsPrazoElimExp(frd.ID, @now) = 1" : "",
                        table);
                    command.ExecuteNonQuery();

                    Debug.WriteLine("<<FilterIDs>> " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
                }
                else // caso em que se pretende reordenar o resultado de pesquisa
                {
                    GisaDataSetHelperRule.ImportIDs(CacheSearchResult.ToArray(), conn);
                    command.CommandText = 
                        "INSERT INTO #filteredIDs " +
                        "SELECT ID " +
                        "FROM #temp";
                    command.ExecuteNonQuery();
                }

                lastOrdenacao = ordenacao;
                innerQuery = new StringBuilder();
                orderByQuery = new StringBuilder();
                bool joinDatasProducao = true;
                bool joinLicencaObras = true;
                for (int i = 0; i < ordenacao.Count; i = i + 2)
                {
                    Object a = ordenacao[i];
                    string order = string.Empty;
                    if ((bool)ordenacao[i + 1])
                        order = "ASC";
                    else
                        order = "DESC";

                    if (orderByQuery.Length > 0)
                        orderByQuery.Append(", ");

                    switch ((int)a)
                    {
                        case 0: // identificador
                            innerQuery.Append("INNER JOIN Nivel n ON n.ID = sc.IDNivel ");
                            orderByQuery.AppendFormat("n.ID {0}", order);
                            break;
                        case 1: // código referência
                            command.CommandText =
                                "INSERT INTO #SPParametersNiveis " +
                                "SELECT IDNivel " +
                                "FROM #filteredIDs ";
                            command.ExecuteNonQuery();

                            command.CommandText = "sp_getCodigosCompletosNiveis";
                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();

                            codigosCalculados = true;

                            innerQuery.Append("INNER JOIN (SELECT IDNivel, MIN(CodigoCompleto) CodigoCompleto FROM #SPResultsCodigos GROUP BY IDNivel) spRCod ON spRCod.IDNivel = sc.IDNivel ");
                            orderByQuery.AppendFormat("spRCod.CodigoCompleto {0}", order);
                            break;
                        case 2: // Nível de descrição
                            innerQuery.Append("INNER JOIN (SELECT ID, MIN(IDTipoNivelRelacionado) IDTipoNivelRelacionado FROM RelacaoHierarquica GROUP BY ID) rh ON rh.ID = sc.IDNivel ");
                            orderByQuery.AppendFormat("rh.IDTipoNivelRelacionado {0}", order);
                            break;
                        case 3: // Título
                            innerQuery.Append("INNER JOIN NivelDesignado nd ON nd.ID = sc.IDNivel ");
                            orderByQuery.AppendFormat("nd.Designacao {0}", order);
                            break;
                        case 4: // Data de produção início
                            if (joinDatasProducao)
                            {
                                innerQuery.Append("LEFT JOIN SFRDDatasProducao frdDP ON frdDP.IDFRDBase = frd.ID ");
                                joinDatasProducao = false;
                            }
                            orderByQuery.AppendFormat("frdDP.InicioAno {0}, frdDP.InicioMes {0}, frdDP.InicioDia {0}", order);
                            break;
                        case 5: // Data de produção fim
                            if (joinDatasProducao)
                            {
                                innerQuery.Append("LEFT JOIN SFRDDatasProducao frdDP ON frdDP.IDFRDBase = frd.ID ");
                                joinDatasProducao = false;
                            }
                            orderByQuery.AppendFormat("frdDP.FimAno {0}, frdDP.FimMes {0}, frdDP.FimDia {0}", order);
                            break;
                        case 6: // Requisitado
                            innerQuery.Append(
                                "LEFT JOIN (" +
                                    "SELECT n.ID IDNivel, CASE WHEN (NOT MAX(req.Data) IS NULL AND MAX(dev.Data) IS NULL) OR (NOT MAX(req.Data) IS NULL AND NOT MAX(dev.Data) IS NULL AND MAX(req.Data) > MAX(dev.Data)) THEN 1 ELSE 0 END Requisitado " +
                                    "FROM Nivel n " +
                                        "LEFT JOIN DocumentosMovimentados dm ON dm.IDNivel = n.ID AND dm.isDeleted = 0 " +
                                        "LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = 'REQ' AND req.isDeleted = 0 " +
                                        "LEFT JOIN Movimento dev ON dev.ID = dm.IDMovimento AND dev.CatCode = 'DEV' AND dev.isDeleted = 0 " +
                                    "WHERE n.isDeleted = 0 " +
                                    "GROUP BY n.ID " +
                                ") nReq ON nReq.IDNivel = sc.IDNivel ");
                            orderByQuery.AppendFormat("nReq.Requisitado {0}", order);
                            break;
                        case 7: // Agrupador
                            innerQuery.Append("LEFT JOIN SFRDAgrupador agr ON agr.IDFRDBase = frd.ID ");
                            orderByQuery.AppendFormat("agr.Agrupador {0}", order);
                            break;
                        case 8: // Requerentes iniciais
                            if (joinLicencaObras)
                            {
                                CriaTabelaLicObr(conn);
                                innerQuery.Append("LEFT JOIN #LicObr ON #LicObr.ID = frd.ID ");
                                joinLicencaObras = false;
                            }
                            orderByQuery.AppendFormat("#LicObr.RequerentesIniciais {0}", order);
                            break;
                        case 9:
                            // Localização da obra (actual)
                            if (joinLicencaObras)
                            {
                                CriaTabelaLicObr(conn);
                                innerQuery.Append("LEFT JOIN #LicObr ON #LicObr.ID = frd.ID ");
                                joinLicencaObras = false;
                            }
                            orderByQuery.AppendFormat("#LicObr.LocObraDesignacaoAct {0}", order);
                            break;
                        case 10: // Num. polícia (actual)
                            if (joinLicencaObras)
                            {
                                CriaTabelaLicObr(conn);
                                innerQuery.Append("LEFT JOIN #LicObr ON #LicObr.ID = frd.ID ");
                                joinLicencaObras = false;
                            }
                            orderByQuery.AppendFormat("#LicObr.LocObraNumPoliciaAct {0}", order);
                            break;
                        case 11: // Tipo de obra
                            if (joinLicencaObras)
                            {
                                CriaTabelaLicObr(conn);
                                innerQuery.Append("LEFT JOIN #LicObr ON #LicObr.ID = frd.ID ");
                                joinLicencaObras = false;
                            }
                            orderByQuery.AppendFormat("#LicObr.TipoObra {0}", order);
                            break;
                    }
                }

                StringBuilder com = new StringBuilder();

                // calcular a ordenação dos items a serem apresentados na pesquisa
                if (ordenacao.Count > 0)
                    com.AppendFormat(
                        "INSERT INTO #OrderedItems (IDNivel)" +
                        "SELECT sc.IDNivel " +
                        "FROM #filteredIDs sc " +
                        "INNER JOIN FRDBase frd ON frd.IDNivel = sc.IDNivel AND frd.IDTipoFRDBase = 1 " +
                        "{0} ORDER BY {1}; ", innerQuery, orderByQuery);
                else
                    com.AppendFormat(
                        "INSERT INTO #OrderedItems (IDNivel) " +
                        "SELECT IDNivel " +
                        "FROM #filteredIDs; ");

                command.CommandType = CommandType.Text;
                command.CommandText = com.ToString();
                command.ExecuteNonQuery();

                CacheSearchResult.Clear();

                command.CommandText = "SELECT IDNivel FROM #OrderedItems ORDER BY seq_id";
                var reader = command.ExecuteReader();
                while (reader.Read())
                    CacheSearchResult.Add(reader.GetInt64(0));
                reader.Close();

                nrResults = CacheSearchResult.Count;

                command.CommandText = "DROP TABLE #Search_Struct; DROP TABLE #OrderedItems; DROP TABLE #filteredIDs;" + (!codigosCalculados?"DROP TABLE #SPParametersNiveis;DROP TABLE #SPResultsCodigos;":"");
                command.ExecuteNonQuery();
            }
		}

        private void CriaTabelaLicObr(IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = @"
                SELECT 
                    frd.ID, 
                    RequerentesIniciais = LEFT(o1.list, LEN(o1.list)-1), 
                    LocObraNumPoliciaAct = LEFT(o3.list, LEN(o3.list)-1),
                    LocObraDesignacaoAct = LEFT(o4.list, LEN(o4.list)-1),
                    lo.TipoObra
                INTO #LicObr
                FROM #filteredIDs sc 
                    INNER JOIN FRDBase frd ON frd.IDNivel = sc.IDNivel AND frd.isDeleted = 0 AND frd.IDTipoFRDBase = 1
	                INNER JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0
	                CROSS APPLY ( 
		                SELECT CONVERT(VARCHAR(200), Nome) + '; ' AS [text()] 
		                FROM LicencaObraRequerentes lor
		                WHERE lor.isDeleted = 0 AND lor.Tipo = 'INICIAL' AND frd.ID = lor.IDFRDBase AND NOT Nome is null AND LEN(Nome) <> 0
		                FOR XML PATH('') 
	                ) o1 (list)
	                CROSS APPLY (
		                SELECT CASE WHEN (NumPolicia IS NOT NULL) THEN NumPolicia + '; ' ELSE '' END
                        AS [text()]
		                FROM LicencaObraLocalizacaoObraActual loa
		                WHERE loa.isDeleted = 0 AND frd.ID = loa.IDFRDBase AND NOT NumPolicia is null AND LEN(NumPolicia) <> 0
		                FOR XML PATH('') 
	                ) o3 (list)
	                CROSS APPLY (
		                SELECT Termo + '; ' AS [text()]  
		                FROM LicencaObraLocalizacaoObraActual loa
			                INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = loa.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
			                INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0
		                WHERE loa.isDeleted = 0 AND frd.ID = loa.IDFRDBase
		                FOR XML PATH('') 
	                ) o4 (list)";
            command.CommandTimeout = 500;
            command.ExecuteNonQuery();
        }

		public override int CountPages(int itemsPerPage, IDbConnection conn)
		{
            if (CacheSearchResult.Count % itemsPerPage != 0)
				return System.Convert.ToInt32(CacheSearchResult.Count/itemsPerPage) + 1;
			else
                return System.Convert.ToInt32(CacheSearchResult.Count / itemsPerPage);
		}

		public override int GetPageForID(long IDFRDBase, int pageLimit, IDbConnection conn)
		{
            int id_seq = CacheSearchResult.FindIndex(ID => ID == IDFRDBase);

			if (id_seq % pageLimit != 0)
				return id_seq/pageLimit + 1;
			else
				return id_seq/pageLimit;
		}

		public override ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn)
		{
            ArrayList rows = new ArrayList();
			using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                var pageItems = new long[itemsPerPage];
                if (((pageNr - 1) * itemsPerPage + itemsPerPage) > CacheSearchResult.Count)
                {
                    var nrItems = ((pageNr - 1) * itemsPerPage + itemsPerPage) - CacheSearchResult.Count;
                    CacheSearchResult.CopyTo((pageNr - 1) * itemsPerPage, pageItems, 0, itemsPerPage - nrItems);
                }
                else
                    CacheSearchResult.CopyTo((pageNr - 1) * itemsPerPage, pageItems, 0, itemsPerPage);
                GisaDataSetHelperRule.ImportIDs(pageItems, conn);

                rows = GetPesquisaResultsDetails("#temp", !codigosCalculados, conn);

                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@isDeleted", 0);
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #temp ON #temp.ID = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                        "INNER JOIN #temp ON #temp.ID = FRDBase.IDNivel");
                    da.Fill(currentDataSet, "FRDBase");
                }
            }

			return rows;
		}

        public override ArrayList GetPesquisaResultsDetails(string idsTableName, bool calculaCodigos, IDbConnection conn)
        {
            var rows = new ArrayList();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                if (calculaCodigos)
                {
                    command.CommandText =
                        "CREATE TABLE #SPParametersNiveis (IDNivel BIGINT); " +
                        "CREATE TABLE #SPResultsCodigos(IDNivel BIGINT, CodigoCompleto NVARCHAR(300)); " +
                        "INSERT INTO #SPParametersNiveis " +
                        "SELECT ID " +
                        "FROM #temp oi";
                    command.ExecuteNonQuery();

                    command.CommandText = "sp_getCodigosCompletosNiveis";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
                command.CommandType = CommandType.Text;
                command.CommandText = string.Format(@"
                    SELECT T.ID, frd.ID IDFRDBase, spRCod.CodigoCompleto, rh.IDTipoNivelRelacionado, 
                        nd.Designacao, frdDP.InicioAno, frdDP.InicioMes, frdDP.InicioDia, frdDP.InicioAtribuida, frdDP.FimAno, frdDP.FimMes, 
                        frdDP.FimDia, frdDP.FimAtribuida, nReq.Requisitado, agr.Agrupador,
                        RequerentesIniciais = LEFT(o1.list, LEN(o1.list)-1), 
                        RequerentesAverbamentos = LEFT(o2.list, LEN(o2.list)-1),
                        LocObraNumPoliciaAct = LEFT(o3.list, LEN(o3.list)-1),
                        LocObraDesignacaoAct = LEFT(o4.list, LEN(o4.list)-1),
                        LocObraNumPoliciaAnt = LEFT(o5.list, LEN(o5.list)-1),
                        LocObraDesignacaoAnt = LEFT(o6.list, LEN(o6.list)-1),
                        TecnicoObra = LEFT(o7.list, LEN(o7.list)-1),
                        AtestHabit = LEFT(o8.list, LEN(o8.list)-1),
                        DataLicConstr = LEFT(o9.list, LEN(o9.list)-1),
                        lo.PropriedadeHorizontal,
                        lo.TipoObra
                    FROM {0} T
                        INNER JOIN FRDBase frd ON frd.IDNivel = T.ID AND frd.IDTipoFRDBase = @IDTipoFRDBase
                        INNER JOIN (SELECT IDNivel, MIN(CodigoCompleto) CodigoCompleto FROM #SPResultsCodigos GROUP BY IDNivel) spRCod ON spRCod.IDNivel = T.ID 
                        INNER JOIN NivelDesignado nd ON nd.ID = T.ID 
                        INNER JOIN (SELECT ID, MIN(IDTipoNivelRelacionado) IDTipoNivelRelacionado FROM RelacaoHierarquica GROUP BY ID) rh ON rh.ID = T.ID 
                        LEFT JOIN SFRDDatasProducao frdDP ON frdDP.IDFRDBase = frd.ID 
                        LEFT JOIN SFRDAgrupador agr ON agr.IDFRDBase = frd.ID 
                        LEFT JOIN ( 
                            SELECT n.ID IDNivel, CASE WHEN (NOT MAX(req.Data) IS NULL AND MAX(dev.Data) IS NULL) OR (NOT MAX(req.Data) IS NULL AND NOT MAX(dev.Data) IS NULL AND MAX(req.Data) > MAX(dev.Data)) THEN 1 ELSE 0 END Requisitado 
                            FROM Nivel n 
                                LEFT JOIN DocumentosMovimentados dm ON dm.IDNivel = n.ID AND dm.isDeleted=@isDeleted 
                                LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = 'REQ' AND req.isDeleted=@isDeleted 
                                LEFT JOIN Movimento dev ON dev.ID = dm.IDMovimento AND dev.CatCode = 'DEV' AND dev.isDeleted=@isDeleted 
                            WHERE n.isDeleted=@isDeleted 
                            GROUP BY n.ID 
                        ) nReq ON nReq.IDNivel = T.ID 
                        LEFT JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted=@isDeleted
                        CROSS APPLY ( 
	                        SELECT CONVERT(VARCHAR(200), Nome) + '; ' AS [text()] 
	                        FROM LicencaObraRequerentes lor
	                        WHERE lor.isDeleted=@isDeleted AND lor.Tipo = 'INICIAL' AND lo.IDFRDBase = lor.IDFRDBase
	                        FOR XML PATH('') 
                        ) o1 (list)
                        CROSS APPLY ( 
	                        SELECT CONVERT(VARCHAR(200), Nome) + '; ' AS [text()] 
	                        FROM LicencaObraRequerentes lor
	                        WHERE lor.isDeleted=@isDeleted AND lor.Tipo = 'AVRB' AND lo.IDFRDBase = lor.IDFRDBase
	                        FOR XML PATH('') 
                        ) o2 (list)
                        CROSS APPLY (
	                        SELECT NumPolicia + '; ' AS [text()]
	                        FROM LicencaObraLocalizacaoObraActual loa
	                        WHERE loa.isDeleted=@isDeleted AND frd.ID = loa.IDFRDBase AND NOT NumPolicia is null AND LEN(NumPolicia) <> @NumPolicia
	                        FOR XML PATH('') 
                        ) o3 (list)
                        CROSS APPLY (
	                        SELECT Termo + '; ' AS [text()]  
	                        FROM LicencaObraLocalizacaoObraActual loa
		                        INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = loa.IDControloAut AND cad.IDTipoControloAutForma = @IDTipoControloAutForma AND cad.isDeleted=@isDeleted
		                        INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted=@isDeleted
	                        WHERE loa.isDeleted=@isDeleted AND lo.IDFRDBase = loa.IDFRDBase
	                        FOR XML PATH('') 
                        ) o4 (list)
                        CROSS APPLY (
	                        SELECT NumPolicia + '; ' AS [text()]  
	                        FROM LicencaObraLocalizacaoObraAntiga loa
	                        WHERE loa.isDeleted=@isDeleted AND lo.IDFRDBase = loa.IDFRDBase AND NOT NumPolicia is null AND LEN(NumPolicia) <> @NumPolicia
	                        FOR XML PATH('') 
                        ) o5 (list)
                        CROSS APPLY (
	                        SELECT CONVERT(VARCHAR(200), NomeLocal) + '; ' AS [text()]
	                        FROM LicencaObraLocalizacaoObraAntiga loa
	                        WHERE loa.isDeleted=@isDeleted AND lo.IDFRDBase = loa.IDFRDBase
	                        FOR XML PATH('') 
                        ) o6 (list)
                        CROSS APPLY (
	                        SELECT Termo + '; ' AS [text()]
	                        FROM LicencaObraTecnicoObra tecObr
		                        INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = tecObr.IDControloAut AND cad.IDTipoControloAutForma = @IDTipoControloAutForma AND cad.isDeleted=@isDeleted
		                        INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted=@isDeleted
	                        WHERE tecObr.isDeleted=@isDeleted AND lo.IDFRDBase = tecObr.IDFRDBase
	                        FOR XML PATH('') 
                        ) o7 (list)
                        CROSS APPLY (
	                        SELECT CONVERT(VARCHAR(200), Codigo) + '; ' AS [text()]
	                        FROM LicencaObraAtestadoHabitabilidade ah
	                        WHERE ah.isDeleted=@isDeleted AND lo.IDFRDBase = ah.IDFRDBase
	                        FOR XML PATH('') 
                        ) o8 (list)
                        CROSS APPLY (
	                        SELECT dbo.fn_AddPaddingToDateMember_new(dlc.Ano, 4) + '/' 
		                        + dbo.fn_AddPaddingToDateMember_new(dlc.Mes, 2) + '/' 
		                        + dbo.fn_AddPaddingToDateMember_new(dlc.Dia, 2) + '; ' AS [text()]
	                        FROM LicencaObraDataLicencaConstrucao dlc
	                        WHERE dlc.isDeleted=@isDeleted AND frd.ID = dlc.IDFRDBase
	                        FOR XML PATH('') 
                        ) o9 (list)
                    ORDER BY T.seq_nr", idsTableName);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@IDTipoControloAutForma", 1);
                command.Parameters.AddWithValue("@NumPolicia", 0);
                command.Parameters.AddWithValue("@IDTipoFRDBase", 1);
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();

                NivelDocumental item;
                while (reader.Read())
                {
                    item = new NivelDocumental();
                    item.IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                    item.IDFRDBase = System.Convert.ToInt64(reader.GetValue(1));
                    item.CodigoCompleto = reader.GetValue(2).ToString();
                    item.IDTipoNivelRelacionado = System.Convert.ToInt64(reader.GetValue(3));
                    item.Designacao = reader.GetValue(4).ToString();
                    item.InicioAno = reader.GetValue(5).ToString();
                    item.InicioMes = reader.GetValue(6).ToString();
                    item.InicioDia = reader.GetValue(7).ToString();
                    item.InicioAtribuida = reader.GetValue(8) == DBNull.Value ? false : System.Convert.ToBoolean(reader.GetValue(8));
                    item.FimAno = reader.GetValue(9).ToString();
                    item.FimMes = reader.GetValue(10).ToString();
                    item.FimDia = reader.GetValue(11).ToString();
                    item.FimAtribuida = reader.GetValue(12) == DBNull.Value ? false : System.Convert.ToBoolean(reader.GetValue(12));
                    item.Requisitado = System.Convert.ToBoolean(reader.GetValue(13));
                    item.Agrupador = reader.IsDBNull(14) ? "" : reader.GetString(14);

                    item.RequerentesIniciais = reader.GetValue(15).ToString();
                    item.RequerentesAverbamentos = reader.GetValue(16).ToString();
                    item.LocObraNumPoliciaAct = reader.GetValue(17).ToString();
                    item.LocObraDesignacaoAct = reader.GetValue(18).ToString();
                    item.LocObraNumPoliciaAnt = reader.GetValue(19).ToString();
                    item.LocObraDesignacaoAnt = reader.GetValue(20).ToString();
                    item.TecnicoObra = reader.GetValue(21).ToString();
                    item.AtestHabit = reader.GetValue(22).ToString();
                    item.DataLicConstr = reader.GetValue(23).ToString();
                    if (reader.GetValue(24) != DBNull.Value)
                        item.PropriedadeHorizontal = System.Convert.ToBoolean(reader.GetValue(24));
                    item.TipoObra = reader.GetValue(25).ToString();

                    rows.Add(item);
                }
                reader.Close();
            }
            return rows;
        }

		public override void DeleteTemporaryResults(IDbConnection conn)
		{
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText =
                    @"IF OBJECT_ID(N'tempdb..#LicObr', N'U') IS NOT NULL
                    DROP TABLE #LicObr; 
                DROP TABLE #SPParametersNiveis; 
                DROP TABLE #SPResultsCodigos;";

                command.ExecuteNonQuery();
            }
			codigosCalculados = false;
		}

		#endregion

		#region " SlavePanelPesquisaUF "
        private void FiltraOperador(string operador, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandType = CommandType.Text;
            command.CommandText = string.Format(
                "SELECT DISTINCT frd.IDNivel " +
                "INTO #NivOper " +
                "FROM FRDBaseDataDeDescricao ddd " +
                    "INNER JOIN FRDBase frd ON frd.ID = ddd.IDFRDBase AND frd.isDeleted = 0 " +
                    "LEFT JOIN TrusteeUser tu ON tu.ID = ddd.IDTrusteeOperator AND tu.isDeleted = 0 " +
		            "LEFT JOIN UserGroups ug ON ug.IDUser = tu.ID AND ug.isDeleted = 0 " +
 		            "LEFT JOIN TrusteeGroup tg ON tg.ID= ug.IDGroup AND tg.isDeleted = 0 " +
		            "LEFT JOIN Trustee t_u ON t_u.ID = tu.ID AND t_u.isDeleted = 0 " +
		            "LEFT JOIN Trustee t_g ON t_g.ID = tg.ID AND t_g.isDeleted = 0 " +
                "WHERE frd.IDTipoFRDBase=2 AND ddd.isDeleted = 0 " +
                    "AND (" +
                        "(NOT t_u.Name IS NULL " +
                            "AND t_u.Name COLLATE LATIN1_GENERAL_CI_AI LIKE '{0}' COLLATE LATIN1_GENERAL_CI_AI) " +
                        "OR (NOT t_g.Name IS NULL " +
                            "AND t_g.Name COLLATE LATIN1_GENERAL_CI_AI LIKE '{0}' COLLATE LATIN1_GENERAL_CI_AI))", operador.Replace('*', '%'));
            command.ExecuteNonQuery();
        }

        private void FiltraDataEdicao(int anoEdicaoInicio, int mesEdicaoInicio, int diaEdicaoInicio, int anoEdicaoFim, int mesEdicaoFim, int diaEdicaoFim, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandType = CommandType.Text;
            command.CommandText = string.Format(
                "SELECT DISTINCT IDNivel " +
                "INTO #NivDtEd " +
                "FROM (" +
                    "SELECT frd.IDNivel," +
		                "dbo.fn_ComparePartialDate2(" +
			                "STR(YEAR(ddd.DataEdicao))," +
			                "STR(MONTH(ddd.DataEdicao))," +
			                "STR(DAY(ddd.DataEdicao))," +
			                "'{0}'," +
                            "'{1}'," +
                            "'{2}') AS dateCompare1," +
		                "dbo.fn_ComparePartialDate2(" +
			                "STR(YEAR(ddd.DataEdicao))," +
			                "STR(MONTH(ddd.DataEdicao))," +
			                "STR(DAY(ddd.DataEdicao))," +
                            "'{3}'," +
                            "'{4}'," +
                            "'{5}') AS dateCompare2 " +
                    "FROM FRDBaseDataDeDescricao  ddd " +
		                "INNER JOIN FRDBase frd ON frd.ID = ddd.IDFRDBase AND frd.isDeleted = 0 " +
                    "WHERE frd.IDTipoFRDBase=2 AND ddd.isDeleted = 0 " +
                ") dates " +
                "WHERE (dateCompare1 IS NULL OR dateCompare1 >= 0) " +
	                "AND (dateCompare2 IS NULL OR dateCompare2 <= 0)",
                    anoEdicaoInicio < 0 ? "????" : anoEdicaoInicio.ToString(), mesEdicaoInicio < 0 ? "??" : mesEdicaoInicio.ToString(), diaEdicaoInicio < 0 ? "??" : diaEdicaoInicio.ToString(), anoEdicaoFim < 0 ? "????" : anoEdicaoFim.ToString(), mesEdicaoFim < 0 ? "??" : mesEdicaoFim.ToString(), diaEdicaoFim < 0 ? "??" : diaEdicaoFim.ToString());
            command.ExecuteNonQuery();
        }

        private void FiltraIDNivel(long IDNivel, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandType = CommandType.Text;
            command.CommandText = string.Format(
                "CREATE TABLE #SubNiveis (" +
			        "IDNivel BIGINT," +
			        "IDNivelUpper BIGINT" +
		        ");" +

		        "INSERT INTO #SubNiveis VALUES({0}, NULL);" +

		        "WHILE (@@ROWCOUNT > 0)" +
		        "BEGIN " +
			        "INSERT INTO #SubNiveis " +
			        "SELECT rh.ID, rh.IDUpper " +
			        "FROM RelacaoHierarquica rh " +
				        "LEFT JOIN #SubNiveis sn ON rh.ID=sn.IDNivel AND rh.IDUpper=sn.IDNivelUpper " +
				        "INNER JOIN #SubNiveis snUpper ON snUpper.IDNivel = rh.IDUpper " +
			        "WHERE sn.IDNivel IS NULL " +
				        "AND rh.isDeleted = 0;" +
		        "END " +
		        // verificar que unidades fisicas se encontram associadas ao conjunto de niveis encontrado
		        // e adiciona-las como resultado
		        "SELECT DISTINCT nUF.ID IDNivel " +
                "INTO #NivID " +
		        "FROM Nivel nUF " +
			        "INNER JOIN SFRDUnidadeFisica sfrdUF ON nUF.ID=sfrdUF.IDNivel AND sfrdUF.isDeleted = 0 " +
			        "INNER JOIN FRDBase frdUF ON frdUF.ID=sfrdUF.IDFRDBase AND frdUF.isDeleted = 0 " +
			        "INNER JOIN #SubNiveis sn ON frdUF.IDNivel=sn.IDNivel " +
		        "WHERE nUF.IDTipoNivel=4 " +
                    "AND nUF.isDeleted = 0;" +

		        "DROP TABLE #SubNiveis;", IDNivel);
            command.ExecuteNonQuery();
        }

        private void FiltraUAsAssociadas(int assoc, IDbConnection conn)
        {
            string join = string.Empty;
            string where = string.Empty;

            if (assoc == 1)
            {
                join = "INNER";
                where = "sfrduf.isDeleted = @isDeleted";
            }
            else if (assoc == 2)
            {
                join = "LEFT";
                where = "sfrduf.IDNivel IS NULL";
            }
            else
                // não deve chegar aqui
                return;

            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #NivUAs (IDNivel BIGINT)";
                command.ExecuteNonQuery();

                command.CommandText = string.Format(
                    "INSERT INTO #NivUAs " +
                    "SELECT n.ID IDNivel " +                    
                    "FROM Nivel n " +
                        "{0} JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDNivel = n.ID " +
                    "WHERE n.IDTipoNivel = @IDTipoNivel AND n.isDeleted = @isDeleted AND {1} " +
                    "GROUP BY n.ID", join, where);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@IDTipoNivel", 4);
                command.ExecuteNonQuery();
            }
        }

        public override List<DocAssociado> LoadEstruturaDocsData(DataSet currentDataSet, string id, IDbConnection conn)
		{
            List<DocAssociado> docsAssociados = new List<DocAssociado>();
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #SPParametersNiveis (IDNivel BIGINT);CREATE TABLE #SPResultsCodigos(IDNivel BIGINT, CodigoCompleto NVARCHAR(300));";
                command.ExecuteNonQuery();

                command.CommandText =
                    "INSERT INTO #SPParametersNiveis " +
                    "SELECT nDocs.ID FROM Nivel nDocs " +
                    "INNER JOIN FRDBase frd ON frd.IDNivel = nDocs.ID " +
                    "INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDFRDBase = frd.ID " +
                    "WHERE sfrduf.IDNivel = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                command.CommandText = "sp_getCodigosCompletosNiveis";
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM #SPResultsCodigos";
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();

                Hashtable codigos = new Hashtable();
                long IDNivel;
                while (reader.Read())
                {
                    IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                    if (!codigos.ContainsKey(IDNivel))
                        codigos.Add(IDNivel, reader.GetValue(1).ToString());
                }
                reader.Close();

                command.CommandText =
                    "SELECT a.IDNivel, tnr.GUIOrder, tnr.Designacao, a.Designacao, a.Requisitado " +
                    "FROM ( " +
                        "SELECT ud.IDNivel, min(rh.IDTipoNivelRelacionado) as IDTipoNivelRelacionado, ud.Designacao, nReq.Requisitado " +
                        "FROM ( " +
                            "SELECT nca.ID IDNivel, d.Termo Designacao " +
                            "FROM #SPParametersNiveis " +
                                "INNER Join FRDBase frd ON frd.IDNivel = #SPParametersNiveis.IDNivel AND frd.isDeleted=@isDeleted " +
                                "INNER JOIN NivelControloAut nca ON nca.ID = frd.IDNivel " +
                                "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = nca.IDControloAut AND cad.IDTipoControloAutForma = @IDTipoControloAutForma AND cad.isDeleted=@isDeleted " +
                                "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted=@isDeleted " +
                            "UNION " +
                            "SELECT frd.IDNivel, nd.Designacao " +
                            "FROM #SPParametersNiveis " +
                                "INNER Join FRDBase frd ON frd.IDNivel = #SPParametersNiveis.IDNivel AND frd.isDeleted=@isDeleted " +
                                "INNER Join NivelDesignado nd ON nd.ID = frd.IDNivel AND nd.isDeleted =@isDeleted " +
                        ") ud " +
                            "INNER JOIN RelacaoHierarquica rh ON ud.IDNivel = rh.ID " +
                            "LEFT JOIN (" +
                                "SELECT n.ID IDNivel, CASE WHEN (NOT MAX(req.Data) IS NULL AND MAX(dev.Data) IS NULL) OR (NOT MAX(req.Data) IS NULL AND NOT MAX(dev.Data) IS NULL AND MAX(req.Data) > MAX(dev.Data)) THEN 1 ELSE 0 END Requisitado " +
                                "FROM Nivel n " +
                                    "LEFT JOIN DocumentosMovimentados dm ON dm.IDNivel = n.ID AND dm.isDeleted = @isDeleted " +
                                    "LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = 'REQ' AND req.isDeleted = @isDeleted " +
                                    "LEFT JOIN Movimento dev ON dev.ID = dm.IDMovimento AND dev.CatCode = 'DEV' AND dev.isDeleted = @isDeleted " +
                                "WHERE n.isDeleted = @isDeleted " +
                                "GROUP BY n.ID " +
                            ") nReq ON nReq.IDNivel = ud.IDNivel " +
                        "GROUP BY ud.IDNivel, ud.Designacao, nReq.Requisitado " +
                    ") a " +
                        "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = a.IDTipoNivelRelacionado";
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@IDTipoControloAutForma", 1);
                command.Parameters.AddWithValue("@reqCatCode", "REQ");
                command.Parameters.AddWithValue("@devCatCode", "DEV");
                reader = command.ExecuteReader();

                DocAssociado dAssoc;
                while (reader.Read())
                {
                    dAssoc = new DocAssociado();
                    dAssoc.IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                    dAssoc.GUIOrder = System.Convert.ToInt32(reader.GetValue(1));
                    dAssoc.RelDesignacao = reader.GetValue(2).ToString();
                    dAssoc.NivelDesignacao = reader.GetValue(3).ToString();
                    dAssoc.Codigo = codigos[dAssoc.IDNivel].ToString();
                    dAssoc.Requisitado = System.Convert.ToBoolean(reader.GetValue(4));
                    docsAssociados.Add(dAssoc);
                }
                reader.Close();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #SPParametersNiveis niveis ON niveis.IDNivel = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");
                }

                command.CommandText = "DROP TABLE #SPParametersNiveis; DROP TABLE #SPResultsCodigos";
                command.ExecuteNonQuery();
            }

			return docsAssociados;
		}

		public override void LoadFRDBaseUFData(DataSet currentDataSet, long FRDBaseRowIDNivel, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@FRDBaseRowIDNivel", FRDBaseRowIDNivel);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel=@FRDBaseRowIDNivel");
				da.Fill(currentDataSet, "FRDBase");

				if (currentDataSet.Tables["FRDBase"].Select(string.Format("IDNivel={0}", FRDBaseRowIDNivel.ToString())).Length > 0)
				{
					long id = (long)currentDataSet.Tables["FRDBase"].Select("IDNivel="+FRDBaseRowIDNivel.ToString())[0]["ID"];

                    command.Parameters.AddWithValue("@FRDBaseRowID", id);

					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"], 
						"WHERE ID IN (SELECT IDControloAut " +
                        "FROM IndexFRDCA WHERE IDFRDBase=@FRDBaseRowID)");
					da.Fill(currentDataSet, "ControloAut");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"], 
						"WHERE IDFRDBase=" + id);
					da.Fill(currentDataSet, "IndexFRDCA");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"], 
						"WHERE ID IN (SELECT IDDicionario " + 
						"FROM ControloAutDicionario WHERE IDControloAut IN " + 
						"(SELECT IDControloAut FROM IndexFRDCA " +
                        "WHERE IDFRDBase=@FRDBaseRowID))");
					da.Fill(currentDataSet, "Dicionario");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"], 
						"WHERE IDControloAut IN (SELECT IDControloAut " +
                        "FROM IndexFRDCA WHERE IDFRDBase=@FRDBaseRowID)");
					da.Fill(currentDataSet, "ControloAutDicionario");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDContexto"],
                        "WHERE IDFRDBase=@FRDBaseRowID");
					da.Fill(currentDataSet, "SFRDContexto");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUFDescricaoFisica"],
                        "WHERE IDFRDBase=@FRDBaseRowID");
					da.Fill(currentDataSet, "SFRDUFDescricaoFisica");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDConteudoEEstrutura"],
                        "WHERE IDFRDBase=@FRDBaseRowID");
					da.Fill(currentDataSet, "SFRDConteudoEEstrutura");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDocumentacaoAssociada"],
                        "WHERE IDFRDBase=@FRDBaseRowID");
					da.Fill(currentDataSet, "SFRDDocumentacaoAssociada");
				}

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "WHERE ID=@FRDBaseRowIDNivel OR ID IN (SELECT IDUpper FROM RelacaoHierarquica WHERE ID=@FRDBaseRowIDNivel)");
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                    "WHERE ID IN (SELECT IDUpper FROM RelacaoHierarquica WHERE ID=@FRDBaseRowIDNivel)");
				da.Fill(currentDataSet, "NivelDesignado");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"], 
					"WHERE ID=@FRDBaseRowIDNivel");
				da.Fill(currentDataSet, "RelacaoHierarquica");
			}
		}

		public override bool isNivelDeleted(DataSet currentDataSet, long nivelID, IDbConnection conn)
		{
			int count;
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@nivelID", nivelID);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.CommandText =
                    "SELECT COUNT(ID) FROM Nivel WHERE ID=@nivelID AND isDeleted=@isDeleted";
                IDataReader reader = command.ExecuteReader();

                reader.Read();
                count = ((int)(reader.GetValue(0)));
                reader.Close();
            }

			return count == 0;			
		}
		#endregion

        #region " PesquisaListUF "
        public override void CalculateOrderedItemsUF(ArrayList ordenacao, List<string> ids, string operador, int anoEdicaoInicio, int mesEdicaoInicio, int diaEdicaoInicio, int anoEdicaoFim, int mesEdicaoFim, int diaEdicaoFim, long IDNivel, int assoc, bool _newSearch, out long nrResults, IDbConnection conn)
		{
            nrResults = 0;
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);

            if (!_newSearch && MesmaOrdenacao(ordenacao)) return;

            if (_newSearch)
            {
                if (ids.Count > 0)
                {
                    StringBuilder innerJoin = new StringBuilder();
                    StringBuilder dropTables = new StringBuilder();

                    // inserir, numa tabela temporária, os IDs ordenados do resultado da pesquisa
                    var long_IDs = ids.ConvertAll<long>(new Converter<string, long>(delegate(string id) { return System.Convert.ToInt64(id); })).ToArray();
                    GisaDataSetHelperRule.ImportIDs(long_IDs, conn);

                    if (operador.Length > 0)
                    {
                        FiltraOperador(operador, conn);
                        innerJoin.Append("INNER JOIN #NivOper ON #NivOper.IDNivel = T.ID ");
                        dropTables.Append("DROP TABLE #NivOper;");
                    }

                    if (anoEdicaoInicio > 0 || mesEdicaoInicio > 0 || diaEdicaoInicio > 0 || anoEdicaoFim > 0 || mesEdicaoFim > 0 || diaEdicaoFim > 0)
                    {
                        FiltraDataEdicao(anoEdicaoInicio, mesEdicaoInicio, diaEdicaoInicio, anoEdicaoFim, mesEdicaoFim, diaEdicaoFim, conn);
                        innerJoin.Append("INNER JOIN #NivDtEd ON #NivDtEd.IDNivel = T.ID ");
                        dropTables.Append("DROP TABLE #NivDtEd;");
                    }

                    if (IDNivel > 0)
                    {
                        FiltraIDNivel(IDNivel, conn);
                        innerJoin.Append("INNER JOIN #NivID ON #NivID.IDNivel = T.ID ");
                        dropTables.Append("DROP TABLE #NivID;");
                    }

                    if (assoc > 0)
                    {
                        FiltraUAsAssociadas(assoc, conn);
                        innerJoin.Append("INNER JOIN #NivUAs ON #NivUAs.IDNivel = T.ID ");
                        dropTables.Append("DROP TABLE #NivUAs;");
                    }

                    command = new SqlCommand("", (SqlConnection)conn);
                    command.CommandText = string.Format(
                        "CREATE TABLE #filteredIDs (IDNivel BIGINT); " +
                        "INSERT INTO #filteredIDs SELECT T.ID FROM #temp T {0} " +
                        "{1}", innerJoin.ToString(), dropTables.ToString());
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = 
                        "CREATE TABLE #filteredIDs (IDNivel BIGINT); ";
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                GisaDataSetHelperRule.ImportIDs(CacheSearchResult.ToArray(), conn);
                command.CommandText = "CREATE TABLE #filteredIDs (IDNivel BIGINT); " +
                    "INSERT INTO #filteredIDs " +
                    "SELECT ID " +
                    "FROM #temp";
                command.ExecuteNonQuery();
            }

			lastOrdenacao = ordenacao;
			command = new SqlCommand(string.Empty, (SqlConnection) conn);			
			innerQuery = new StringBuilder();
			orderByQuery = new StringBuilder();
			bool joinDatasProducao = true;
			bool joinFRDBase = true;
			for (int i = 0; i < ordenacao.Count; i = i + 2)
			{
				Object colInd = ordenacao[i];
				string order = string.Empty;
				if ((bool) ordenacao[i+1])
					order = "ASC";
				else
					order = "DESC";

				if (orderByQuery.Length > 0)
					orderByQuery.Append(", ");
				
				switch((int)colInd)
				{
                    case 0: //Código
						StringBuilder innerJoinQuery = new StringBuilder( 
							"INNER JOIN RelacaoHierarquica ON Nivel.ID=RelacaoHierarquica.ID " + 
							"INNER JOIN Nivel ParentNivel ON ParentNivel.ID = RelacaoHierarquica.IDUpper");

						StringBuilder whereQuery = new StringBuilder(
							" WHERE RelacaoHierarquica.IDTipoNivelRelacionado=11 " +
							"AND RelacaoHierarquica.isDeleted = 0 " +
							"AND ParentNivel.isDeleted = 0 " +
                            "AND Nivel.IDTipoNivel = 4 " +	
							"AND Nivel.isDeleted = 0");

						command.CommandText = 
							"SELECT Nivel.ID, " +
							"ParentNivel.Codigo CodigoEntidadeDetentora, " +
							"Nivel.Codigo CodigoUnidadeFisica, " +
							"CONVERT(SMALLINT, SUBSTRING(Nivel.Codigo, 3, 4)) as ano, " + 
							"CONVERT(BIGINT, SUBSTRING(Nivel.Codigo, 8,100)) as counter " +
							"INTO #temp_cod " + 
							"FROM Nivel " + innerJoinQuery + whereQuery;
			
						command.ExecuteNonQuery();

						codigosCalculados = true;

                        innerQuery.Append("INNER JOIN #temp_cod ON #temp_cod.ID = sc.IDNivel ");						

						if ((bool) ordenacao[i+1])
                            orderByQuery.Append("#temp_cod.CodigoEntidadeDetentora ASC, #temp_cod.ano DESC, #temp_cod.counter DESC");
						else
                            orderByQuery.Append("#temp_cod.CodigoEntidadeDetentora DESC, #temp_cod.ano ASC, #temp_cod.counter ASC");
						break;
                    case 1: //Título
						innerQuery.Append("INNER JOIN NivelDesignado nd ON nd.ID = sc.IDNivel ");
						orderByQuery.AppendFormat("nd.Designacao {0}", order);
						break;
                    case 2: //Datas produção início
						if (joinFRDBase)
						{
							innerQuery.Append(
								"LEFT JOIN FRDBase frd ON frd.IDNivel = sc.IDNivel ");
							joinFRDBase = false;
						}
						if (joinDatasProducao)
						{
							innerQuery.Append(								
								"LEFT JOIN SFRDDatasProducao frdDP ON frdDP.IDFRDBase = frd.ID ");
							joinDatasProducao = false;
						}
						orderByQuery.AppendFormat("frdDP.InicioAno {0}, frdDP.InicioMes {0}, frdDP.InicioDia {0}", order);
						break;
                    case 3: //Datas produção fim
						if (joinFRDBase)
						{
							innerQuery.Append(
								"LEFT JOIN FRDBase frd ON frd.IDNivel = sc.IDNivel ");
							joinFRDBase = false;
						}
						if (joinDatasProducao)
						{
							innerQuery.Append(						
								"LEFT JOIN SFRDDatasProducao frdDP ON frdDP.IDFRDBase = frd.ID ");
							joinDatasProducao = false;
						}
						orderByQuery.AppendFormat("frdDP.FimAno {0}, frdDP.FimMes {0}, frdDP.FimDia {0}", order);
						break;
                    case 4: //Cota
						if (joinFRDBase)
						{
							innerQuery.Append(
								"LEFT JOIN FRDBase frd ON frd.IDNivel = sc.IDNivel ");
							joinFRDBase = false;
						}
						innerQuery.Append(						
							"LEFT JOIN SFRDUFCota cota ON cota.IDFRDBase = frd.ID ");
						orderByQuery.AppendFormat("cota.Cota {0}", order);
						break;
                    case 5:     // Guia de incorporacao
                        innerQuery.Append(
                            "LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = sc.IDNivel ");
                        orderByQuery.AppendFormat(" nuf.GuiaIncorporacao {0}", order);
                        break;
                    case 6:     // Em deposito ?
                        innerQuery.Append("LEFT JOIN NivelUnidadeFisica nuf_1 ON nuf_1.ID = sc.IDNivel ");
                        orderByQuery.AppendFormat(" nuf_1.Eliminado {0}", order);
                        break;

                    case 7:     // CodigoBarras
                        innerQuery.Append("LEFT JOIN NivelUnidadeFisica nuf_2 ON nuf_2.ID = sc.IDNivel ");
                        orderByQuery.AppendFormat(" nuf_2.CodigoBarras {0}", order);

                        break;
                }
			}

            command = new SqlCommand(string.Empty, (SqlConnection)conn);

            StringBuilder com = new StringBuilder();
            com.Append("CREATE TABLE #OrderedItems (seq_id INT Identity(1,1) NOT NULL, IDNivel BIGINT NOT NULL ); ");

            if (ordenacao.Count > 0)
                com.AppendFormat(
                    "INSERT INTO #OrderedItems (IDNivel) " +
                    "SELECT sc.IDNivel " +
                    "FROM #filteredIDs sc {0} ORDER BY {1}; ", innerQuery, orderByQuery);
            
            else
                com.Append(
                    "INSERT INTO #OrderedItems (IDNivel) " +
                    "SELECT IDNivel " +
                    "FROM #filteredIDs fmUF; ");

            command.CommandText = com.ToString();
			command.ExecuteNonQuery();

            CacheSearchResult.Clear();

            command.CommandText = "SELECT IDNivel FROM #OrderedItems ORDER BY seq_id";
            var reader = command.ExecuteReader();
            while (reader.Read())
                CacheSearchResult.Add(reader.GetInt64(0));
            reader.Close();

            nrResults = CacheSearchResult.Count;

            command.CommandText = "DROP TABLE #OrderedItems; DROP TABLE #filteredIDs;";
            command.ExecuteNonQuery();
		}

		public override ArrayList GetItemsUF(DataSet currentDataSet, int pageNr, int itemsPerPage, long TipoNivelUF, IDbConnection conn)
		{
			ArrayList rows = new ArrayList();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {

                var pageItems = new long[itemsPerPage];
                if (((pageNr - 1) * itemsPerPage + itemsPerPage) > CacheSearchResult.Count)
                {
                    var nrItems = ((pageNr - 1) * itemsPerPage + itemsPerPage) - CacheSearchResult.Count;
                    CacheSearchResult.CopyTo((pageNr - 1) * itemsPerPage, pageItems, 0, itemsPerPage - nrItems);
                }
                else
                    CacheSearchResult.CopyTo((pageNr - 1) * itemsPerPage, pageItems, 0, itemsPerPage);
                GisaDataSetHelperRule.ImportIDs(pageItems, conn);

                if (!codigosCalculados)
                {
                    StringBuilder innerJoinQuery = new StringBuilder(
                        "INNER JOIN RelacaoHierarquica ON Nivel.ID=RelacaoHierarquica.ID " +
                        "INNER JOIN Nivel ParentNivel ON ParentNivel.ID = RelacaoHierarquica.IDUpper");

                    StringBuilder whereQuery = new StringBuilder(
                        " WHERE Nivel.IDTipoNivel=" + TipoNivelUF.ToString() + " " +
                        "AND RelacaoHierarquica.isDeleted = @isDeleted " +
                        "AND ParentNivel.isDeleted = @isDeleted " +
                        "AND Nivel.IDTipoNivel = @IDTipoNivel " +
                        "AND Nivel.isDeleted = @isDeleted");

                    command.CommandText = "CREATE TABLE #temp_cod (ID BIGINT, CodigoEntidadeDetentora NVARCHAR(50), CodigoUnidadeFisica NVARCHAR(50), ano SMALLINT, counter BIGINT)";
                    command.ExecuteNonQuery();
                    command.CommandText =
                        "INSERT INTO #temp_cod " +
                        "SELECT Nivel.ID, " +
                        "ParentNivel.Codigo CodigoEntidadeDetentora, " +
                        "Nivel.Codigo CodigoUnidadeFisica, " +
                        "CONVERT(SMALLINT, SUBSTRING(Nivel.Codigo, 3, 4)) as ano, " +
                        "CONVERT(BIGINT, SUBSTRING(Nivel.Codigo, 8,100)) as counter " +
                        "FROM Nivel " + innerJoinQuery + whereQuery;
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.Parameters.AddWithValue("@IDTipoNivel", 4);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }

                command.CommandText = string.Format(@"
                    SELECT T.ID, #temp_cod.CodigoEntidadeDetentora + '/' + #temp_cod.CodigoUnidadeFisica Codigo, nd.Designacao, 
                        frdDP.InicioAno, frdDP.InicioMes, frdDP.InicioDia, frdDP.FimAno, frdDP.FimMes, frdDP.FimDia, ct.Cota, 
                        nuf.GuiaIncorporacao, nuf.Eliminado, nuf.CodigoBarras,
                        AutosEliminacao = LEFT(o1.list, LEN(o1.list)-1)				
				    FROM #temp T
				    LEFT JOIN FRDBase frd ON frd.IDNivel = T.ID
				    INNER JOIN #temp_cod ON #temp_cod.ID = T.ID
				    INNER JOIN NivelDesignado nd ON nd.ID = T.ID
                    LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = T.ID
				    LEFT JOIN SFRDUFCota ct ON ct.IDFRDBase = frd.ID
				    LEFT JOIN SFRDDatasProducao frdDP ON frdDP.IDFRDBase = frd.ID
                    CROSS APPLY ( 
                        SELECT CONVERT(VARCHAR(200), autos.Designacao) + '; ' AS [text()]
                        FROM (
                            SELECT ae.Designacao
                            FROM SFRDUFAutoEliminacao ufae
                                INNER JOIN AutoEliminacao ae ON ae.ID = ufae.IDAutoEliminacao AND ae.isDeleted = @isDeleted
                            WHERE ufae.isDeleted = @isDeleted AND ufae.IDFRDBase = frd.ID
                            UNION
                            SELECT ae.Designacao
                            FROM SFRDUnidadeFisica sfrduf
                                INNER JOIN FRDBase frdNvlDoc ON frdNvlDoc.ID = sfrduf.IDFRDBase AND frdNvlDoc.isDeleted = @isDeleted
	                            INNER JOIN SFRDAvaliacao av ON av.IDFRDBase = frdNvlDoc.ID AND av.isDeleted = @isDeleted
	                            INNER JOIN AutoEliminacao ae ON ae.ID = av.IDAutoEliminacao AND ae.isDeleted = @isDeleted
                            WHERE sfrduf.isDeleted = @isDeleted AND sfrduf.IDNivel = frd.IDNivel
                        ) autos
                        GROUP BY autos.Designacao
                        FOR XML PATH('') 
                    ) o1 (list)
				    ORDER BY T.seq_nr");
                command.Parameters.AddWithValue("@isDeleted", 0);
                SqlDataReader reader = command.ExecuteReader();

                ArrayList row;
                while (reader.Read())
                {
                    row = new ArrayList();
                    row.Add(reader.GetValue(0));
                    row.Add(reader.GetValue(1));
                    row.Add(reader.GetValue(2));
                    row.Add(reader.GetValue(3));
                    row.Add(reader.GetValue(4));
                    row.Add(reader.GetValue(5));
                    row.Add(reader.GetValue(6));
                    row.Add(reader.GetValue(7));
                    row.Add(reader.GetValue(8));
                    row.Add(reader.GetValue(9));
                    // Guia de incorporacao:
                    row.Add(reader.GetValue(10));
                    // Eliminado:
                    if (reader.IsDBNull(11)) row.Add(false);
                    else row.Add(reader.GetValue(11));
                    // CodigoBarras:
                    if (reader.IsDBNull(12)) row.Add(string.Empty);
                    else row.Add(reader.GetString(12));
                    row.Add(reader.IsDBNull(13) ? string.Empty : reader.GetString(13));

                    rows.Add(row);
                }
                reader.Close();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #temp ON #temp.ID = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                        "INNER JOIN #temp ON #temp.ID = FRDBase.IDNivel");
                    da.Fill(currentDataSet, "FRDBase");
                }
            }
			
			return rows;
		}

		public override void DeleteTemporaryResultsUF(IDbConnection conn)
		{
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "DROP TABLE #temp_cod;";
                command.ExecuteNonQuery();
            }
			codigosCalculados = false;
        }
        #endregion
    }
}