using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using System.Text;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient {
    class SqlClientEADGeneratorRule : EADGeneratorRule {

        public override string get_eadheader_audience(long IDNivel, IDbConnection conn) {
            string ret = "internal";
            bool publicar = false;
            try {
                string query = string.Format(@"
                    SELECT av.Publicar  
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN SFRDAvaliacao av ON av.IDFRDBase = frd.ID AND av.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0", 
                    IDNivel);
                    
                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
				SqlDataReader reader = command.ExecuteReader();
				if (reader.Read() && !reader.IsDBNull(0)) {
                    publicar = reader.GetBoolean(0);
				}
				reader.Close();
                ret = publicar ? "external" : "internal";
			}			
			catch (Exception ex) {
				Trace.WriteLine(ex);
				throw;
			}

            return ret;
        }

        /**
         * Nesta versão, devolve sempre "PT"
         */
        public override string get_eadid_countrycode(long IDNivel, IDbConnection conn) {
            return "PT";
        }

        /*
         * Ver os codigos na tabela TipoNivelRelacionado:
         */
        public override string get_CodigoTipoNivelRelacionado(long IDNivel_PAI, long IDNivel, IDbConnection conn) {
            string ret = "";
            try {
                string query = string.Format(@"
                    SELECT tnr.Codigo AS TipoNivelRelacionado_Codigo
                    FROM Nivel n
                    INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.IDUpper = {0} AND rh.isDeleted = 0
                    INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado AND tnr.isDeleted = 0
                    WHERE n.ID = {1}  AND n.isDeleted = 0 ",
                    IDNivel_PAI, IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        ret = reader.GetString(0);
                }
                reader.Close();
            }
            catch (Exception ex) {
                Trace.WriteLine(ex);
                throw;
            }
            return ret;
        }


        public override string get_NvlDesg_Designacao_Dict_Termo(long IDNivel, IDbConnection conn) {
            string ret = "";
            try {
                string query = string.Format(@"
                    SELECT nd.Designacao AS NivelDesignacao,
                    d.Termo AS Designacao_dict_termo
                    FROM Nivel n
                    LEFT JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = 0
                    LEFT JOIN NivelControloAut nca ON nca.ID = n.ID AND nca.isDeleted = 0
                    LEFT JOIN ControloAutDicionario cad ON cad.IDControloAut = nca.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                    LEFT JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", 
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    // O termo ou aparece em NivelDesignacao (0) ou em Designacao_dict_termo (1):
                    if (!reader.IsDBNull(0))
                        ret = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        ret = reader.GetString(1);
                }
                reader.Close();
            }
            catch (Exception ex) {
                Trace.WriteLine(ex);
                throw;
            }
            return ret;
        }

        /*
         * Pode devolver "" caso nao exista o autor
         */
        public override string get_Author(long IDNivel, IDbConnection conn) {
            string ret = "";
            try {
                string query =
                    " SELECT  u.FullName " +
                    " FROM FRDBaseDataDeDescricao d " +
                    " INNER JOIN TrusteeUser u ON u.ID = d.IDTrusteeAuthority AND u.isDeleted = 0" +
                    " INNER JOIN FRDBase frd ON frd.ID = d.IDFRDBase AND frd.isDeleted = 0 " +
                    " WHERE frd.IDNivel = " + IDNivel +
                    " AND d.isDeleted = 0";

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    ret = reader.GetString(0);
                }
                reader.Close();
            }
            catch (Exception ex) {
                Trace.WriteLine(ex);
                throw;
            }
            return ret;
        }


        public override EAD_profiledesc get_profiledesc(long IDNivel, IDbConnection conn) {
            EAD_profiledesc result = new EAD_profiledesc();
            result.creation = string.Empty;
            result.date = string.Empty;

            try {
                string query =
                    " SELECT u.FullName, MIN(fdd.DataEdicao) " +
                    " FROM FRDBaseDataDeDescricao fdd " +
                    " INNER JOIN TrusteeUser u ON u.ID = fdd.IDTrusteeOperator AND u.isDeleted = 0 " +
                    " INNER JOIN FRDBase frd ON frd.ID = fdd.IDFRDBase AND frd.isDeleted = 0 " +
                    " WHERE frd.IDNivel = " + IDNivel + " AND fdd.isDeleted = 0 " +
                    " GROUP BY u.FullName ";
  
                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    // creation: 0
                    if (!reader.IsDBNull(0))
                        result.creation = reader.GetString(0);
                    // creation: 1
                    if (!reader.IsDBNull(1))
                        result.date = reader.GetDateTime(1).ToString();
                }
                reader.Close();
            }
            catch (Exception ex) {
                Trace.WriteLine(ex);
                throw;
            }

            return result;
        }

        public override EAD_physdesc get_physdesc(long IDNivel, IDbConnection conn) {
            StringBuilder sb_dim = new StringBuilder();
            var tiposSuporte = new Dictionary<string, long>();
            string unidade = string.Empty;
            decimal MaxMedidaAltura = 0;
            decimal SumMedidaLargura = 0;
            decimal MaxMedidaProfundidade = 0;

            EAD_physdesc result = new EAD_physdesc();
            try {
                // NOTA: o LEFT JOIN TipoMedida nao e´ um INNER JOIN pois ainda existem dados onde esta´ a NULL
                // a informacao do IDTipoMedida
                string query = @" 
                    SELECT COUNT(ta.Designacao) AS count_unidades, ta.Designacao,
                    MAX(df.MedidaAltura), SUM(df.MedidaLargura), MAX(df.MedidaProfundidade), tm.Designacao
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDUnidadeFisica sfrdUF on sfrdUF.IDFRDBase = frd.ID AND sfrdUF.isDeleted = 0
                    INNER JOIN Nivel nUF on nUF.ID = sfrdUF.IDNivel AND nUF.isDeleted = 0
                    INNER JOIN FRDBase frdUF ON frdUF.IDNivel = nUF.ID AND frdUF.isDeleted = 0
                    INNER JOIN SFRDUFDescricaoFisica df on df.IDFRDBase = frdUF.ID AND df.isDeleted = 0 AND df.isDeleted = 0
                    INNER JOIN TipoAcondicionamento ta ON ta.ID = df.IDTipoAcondicionamento AND ta.isDeleted = 0
                    LEFT JOIN TipoMedida tm ON tm.ID = df.IDTipoMedida AND tm.isDeleted = 0
                    WHERE n.ID = " + IDNivel + 
                    " AND n.isDeleted = 0 AND frd.isDeleted = 0  GROUP BY ta.Designacao, df.MedidaAltura, df.MedidaLargura, df.MedidaProfundidade, tm.Designacao ";

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read()) {
                    long quant = (!reader.IsDBNull(0) ? reader.GetInt32(0) : 0);
                    var designacao = (!reader.IsDBNull(1) ? reader.GetString(1) : string.Empty);
                    decimal medidaAltura = !reader.IsDBNull(2) ? reader.GetDecimal(2) : 0;
                    MaxMedidaAltura = medidaAltura > MaxMedidaAltura ? medidaAltura : MaxMedidaAltura;
                    SumMedidaLargura += !reader.IsDBNull(3) ? reader.GetDecimal(3) : 0;
                    decimal medidaProfundidade = !reader.IsDBNull(4) ? reader.GetDecimal(4) : 0;
                    MaxMedidaProfundidade = medidaProfundidade > MaxMedidaProfundidade ? medidaProfundidade : MaxMedidaProfundidade;
                    unidade = !reader.IsDBNull(5) ? reader.GetString(5) : string.Empty;

                    string uf = designacao;
                    if (designacao.ToUpper().Equals("<Desconhecido>".ToUpper()))
                        uf = "Unidade(s) Física(s)";

                    if (tiposSuporte.ContainsKey(designacao))
                        tiposSuporte[designacao] += quant;
                    else
                        tiposSuporte[designacao] = quant;
                }
                reader.Close();

                if (!MaxMedidaAltura.Equals(String.Empty))
                    sb_dim.Append((sb_dim.Length > 0 ? "; " : " ")).Append(MaxMedidaAltura);
                if (!SumMedidaLargura.Equals(String.Empty))
                    sb_dim.Append(" x ").Append(SumMedidaLargura);
                if (!MaxMedidaProfundidade.Equals(String.Empty))
                    sb_dim.Append(" x ").Append(MaxMedidaProfundidade);

                command.CommandText = @"
                    SELECT COALESCE(Nota, '') 
                    FROM FRDBase frd
                        LEFT JOIN SFRDDimensaoSuporte ds ON ds.IDFRDBase = frd.ID AND ds.isDeleted = 0
                    WHERE frd.IDNivel = " + IDNivel +
                        " AND frd.isDeleted = 0";
                result.notes = command.ExecuteScalar().ToString();
                result.extent = tiposSuporte;
                result.dimension = sb_dim.ToString();
                result.unit = unidade;
            }

            catch (Exception ex) {
                Trace.WriteLine(ex);
                throw;
            }
            return result;
        }

        public override string get_EntidadeDetentoraForNivel(long IDNivel, IDbConnection conn) {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "sp_getEntidadeDetentoraForNivel";    // IDTipoNivel = 1
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@nivelID", SqlDbType.BigInt);
            command.Parameters[0].Value = IDNivel;
            SqlDataReader reader = command.ExecuteReader();
            
            bool has_entsDets = false;
            StringBuilder entidades = new StringBuilder();

            StringBuilder in_clause = new StringBuilder();
            int i = 0;

            while (reader.Read()) {
                in_clause.Append( (i++==0 ? "" : ", ") + reader.GetInt64(0).ToString());
                has_entsDets = true;
            }
            reader.Close();

            if (has_entsDets) {
                command.CommandText = " SELECT nd.Designacao  FROM  Nivel  n  " +
                    " INNER JOIN  NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = 0 " +
                    " WHERE n.ID IN ( " + in_clause.ToString() + " ) " +
                    " AND n.isDeleted = 0 ";
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();
                i = 0;
                while (reader.Read()) {
                    entidades.Append((i++ == 0 ? "" : "; ") + reader.GetString(0));
                }
                reader.Close();
            }
            return entidades.ToString();
        }


        public override string get_DatasDeProducao(long IDNivel, IDbConnection conn) {
            String datas = "";
            StringBuilder datas_inicio = new StringBuilder();
            StringBuilder datas_fim = new StringBuilder();
            bool existe_inicio = false;
            bool existe_fim = false;

            try {
                string query = @"
                SELECT d.InicioTexto, d.InicioAno, d.InicioMes, d.InicioDia, d.InicioAtribuida,
                d.FimTexto, d.FimAno, d.FimMes, d.FimDia, d.FimAtribuida
                FROM FRDBase frd 
                INNER JOIN SFRDDatasProducao d on d.IDFRDBase = frd.ID AND d.isDeleted = 0
                WHERE frd.IDNivel = " + IDNivel + " AND frd.isDeleted = 0 ";

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    // InicioTexto:
                    if (!reader.IsDBNull(0) && !reader.GetString(0).Trim().Equals(string.Empty))
                        datas_inicio.Append(reader.GetString(0)).Append(" ");
                    // InicioAtribuida
                    if (!reader.IsDBNull(4) && reader.GetBoolean(4))
                        datas_inicio.Append("[");
                    existe_inicio = 
                           (!reader.IsDBNull(1) && !reader.GetString(1).Equals(string.Empty))
                        || (!reader.IsDBNull(2) && !reader.GetString(2).Equals(string.Empty))
                        || (!reader.IsDBNull(3) && !reader.GetString(3).Equals(string.Empty));
                    // InicioAno:
                    datas_inicio.Append(!reader.IsDBNull(1) ? reader.GetString(1) : "    ");
                    // InicioMes:
                    datas_inicio.Append(!reader.IsDBNull(2) ? "/" + reader.GetString(2) : "/  ");
                    // InicioDia:
                    datas_inicio.Append(!reader.IsDBNull(3) ? "/" + reader.GetString(3) : "/  ");
                    // InicioAtribuida
                    if (!reader.IsDBNull(4) && reader.GetBoolean(4))
                        datas_inicio.Append("]");

                    // FimTexto:
                    if (!reader.IsDBNull(5) && !reader.GetString(5).Trim().Equals(string.Empty))
                        datas_fim.Append(reader.GetString(5)).Append(" ");
                    // FimAtribuida
                    if (!reader.IsDBNull(9) && reader.GetBoolean(9))
                        datas_fim.Append("[");
                    existe_fim = 
                           (!reader.IsDBNull(6) && !reader.GetString(6).Equals(string.Empty))
                        || (!reader.IsDBNull(7) && !reader.GetString(7).Equals(string.Empty))
                        || (!reader.IsDBNull(8) && !reader.GetString(8).Equals(string.Empty));
                    // FimAno:
                    datas_fim.Append(!reader.IsDBNull(6) ? reader.GetString(6) : "    ");
                    // FimMes:
                    datas_fim.Append(!reader.IsDBNull(7) ? "/" + reader.GetString(7) : "/  ");
                    // FimDia:
                    datas_fim.Append(!reader.IsDBNull(8) ? "/" + reader.GetString(8) : "/  ");
                    // FimAtribuida
                    if (!reader.IsDBNull(9) && reader.GetBoolean(9))
                        datas_fim.Append("]");

                    if (existe_inicio && existe_fim)
                        datas = datas_inicio + " - " + datas_fim;
                    else if (existe_inicio)
                        datas = datas_inicio.ToString();
                    else if (existe_fim)
                        datas = datas_fim.ToString();
                }
                reader.Close();
            }
            catch (Exception ex) {
                Trace.WriteLine(ex);
                throw;
            }

            return datas.ToString();
        }

        public override string get_Extremos_DatasDeProducao_UnidadeFisica(long IDNivel, IDbConnection conn) {
            String datas_UF = "";
            StringBuilder datas_inicio = new StringBuilder();
            StringBuilder datas_fim = new StringBuilder();
            bool existe_inicio = false;
            bool existe_fim = false;

            try {
                string query = @"
                    SELECT 
                    MIN(dbo.fn_AddPaddingToDateMember_new(d.InicioAno, 4) + dbo.fn_AddPaddingToDateMember_new(d.InicioMes, 2) + dbo.fn_AddPaddingToDateMember_new(d.InicioDia, 2)) AS data_min,
                    MAX(dbo.fn_AddPaddingToDateMember_new(d.FimAno, 4) + dbo.fn_AddPaddingToDateMember_new(d.FimMes, 2) + dbo.fn_AddPaddingToDateMember_new(d.FimDia, 2)) AS data_max
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID 
                    INNER JOIN SFRDUnidadeFisica sfrdUF on sfrdUF.IDFRDBase = frd.ID
                    INNER JOIN Nivel nUF on nUF.ID = sfrdUF.IDNivel
                    INNER JOIN FRDBase frdUF ON frdUF.IDNivel = nUF.ID
                    INNER JOIN SFRDDatasProducao d on d.IDFRDBase = frdUF.ID AND d.isDeleted = 0
                    WHERE n.ID = " + IDNivel + " AND frd.isDeleted = 0 " +
                    " GROUP BY n.ID ";

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    // InicioTexto:
                    if (existe_inicio = (!reader.IsDBNull(0) && !reader.GetString(0).Trim().Equals(string.Empty)) )
                        datas_inicio.Append(Date_questionMarks_Format(reader.GetString(0)) );

                    // FimTexto:
                    if (existe_fim = (!reader.IsDBNull(1) && !reader.GetString(1).Trim().Equals(string.Empty)) )
                        datas_fim.Append(Date_questionMarks_Format(reader.GetString(1)) );

                    if (existe_inicio && existe_fim)
                        datas_UF = datas_inicio + " - " + datas_fim;
                    else if (existe_inicio)
                        datas_UF = datas_inicio.ToString();
                    else if (existe_fim)
                        datas_UF = datas_fim.ToString();
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return datas_UF.ToString().Trim();
        }

        /*
         * Assume uma data com o formato YYYYMMAA (com numeros ou ??) 
         */
        private string Date_questionMarks_Format(string date_questionMarks) {
            string yyyy = date_questionMarks.Substring(0, 4);
            string mm = date_questionMarks.Substring(4, 2);
            string dd = date_questionMarks.Substring(6, 2);

            //return (yyyy.StartsWith("?") ? "____" : yyyy) + "/" +
            //    (mm.StartsWith("?") ? "__" : mm) + "/" +
            //    (dd.StartsWith("?") ? "__" : dd);
            return (yyyy.StartsWith("?") ? " " : yyyy) + "/" +
                (mm.StartsWith("?") ? " " : mm) + "/" +
                (dd.StartsWith("?") ? "" : dd);
        }


        public override string get_physdescCota(long IDNivel, IDbConnection conn) {
            StringBuilder cotas = new StringBuilder();
            try {
                string query = @"
                    SELECT  c.Cota + ' - ' + sfrdUF.Cota
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID 
                    INNER JOIN SFRDUnidadeFisica sfrdUF on sfrdUF.IDFRDBase = frd.ID AND sfrdUF.isDeleted = 0
                    INNER JOIN Nivel nUF on nUF.ID = sfrdUF.IDNivel AND nUF.isDeleted = 0
                    INNER JOIN FRDBase frdUF ON frdUF.IDNivel = nUF.ID AND frdUF.isDeleted = 0
                    LEFT JOIN SFRDUFCota c ON c.IDFRDBase = frdUF.ID AND c.isDeleted = 0
                    WHERE n.ID = " + IDNivel + " AND n.isDeleted = 0";

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                int i = 0;
                while (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        cotas.Append(i++ == 0 ? "" : "; ").Append(reader.GetString(0));
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return cotas.ToString();
        }

        public override List<Origination_EntProdutora_Tipo> get_EntidadesProdutoras_Origination(long IDNivel, IDbConnection conn) {
            List<Origination_EntProdutora_Tipo> result = new List<Origination_EntProdutora_Tipo>();

            //StringBuilder termo_origs = new StringBuilder();
            try {
                string query = string.Format(@"
                    WITH Temp (ID, IDUpper, IDTipoNivelRelacionado)
                    AS (
                        SELECT ID, IDUpper, IDTipoNivelRelacionado 
                        FROM RelacaoHierarquica rh
                        WHERE rh.ID = {0} AND rh.isDeleted = 0
                        
                        UNION ALL
                    	
                        SELECT RelacaoHierarquica.ID, RelacaoHierarquica.IDUpper, RelacaoHierarquica.IDTipoNivelRelacionado
                        FROM RelacaoHierarquica
                        INNER JOIN Temp ON Temp.IDUpper = RelacaoHierarquica.ID 
                        WHERE RelacaoHierarquica.IDTipoNivelRelacionado > 6 AND RelacaoHierarquica.isDeleted = 0
                    )
                    SELECT IDUpper, Termo, caep.IDTipoEntidadeProdutora  
                    FROM Temp
                        INNER JOIN Nivel n ON n.ID = Temp.ID AND n.isDeleted = 0
                        INNER JOIN Nivel nUpper ON nUpper.ID = Temp.IDUpper AND nUpper.isDeleted = 0
                        INNER JOIN NivelControloAut nca ON nca.ID = nUpper.ID AND nca.isDeleted = 0
                        INNER JOIN ControloAutEntidadeProdutora caep ON caep.IDControloAut = nca.IDControloAut AND caep.isDeleted = 0
                        INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = nca.IDControloAut  AND cad.IDTipoControloAutForma = 1  AND cad.isDeleted = 0
                        INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0
                    WHERE n.IDTipoNivel = 3 AND nUpper.IDTipoNivel = 2 AND n.isDeleted = 0 ", IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    Origination_EntProdutora_Tipo elem = new Origination_EntProdutora_Tipo();
                    // Nao se usa a coluna 0 (IDUpper)
                    if (!reader.IsDBNull(1))
                        elem.termo = reader.GetString(1);
                    else
                        elem.termo = string.Empty;

                    if (!reader.IsDBNull(2))
                        elem.IDTipoEntidadeProdutora = reader.GetInt64(2);
                    else
                        elem.IDTipoEntidadeProdutora = -1;

                    result.Add(elem);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return result;

        }

        public override List<Origination_EntProdutora_Tipo> get_Autores_Origination(long IDNivel, IDbConnection conn)
        {
            List<Origination_EntProdutora_Tipo> result = new List<Origination_EntProdutora_Tipo>();

            //StringBuilder termo_origs = new StringBuilder();
            try
            {
                string query = string.Format(@"
                    SELECT d.Termo, caep.IDTipoEntidadeProdutora  
                    FROM FRDBase frd
                        INNER JOIN SFRDAutor a ON a.IDFRDBase = frd.ID AND a.isDeleted = 0
                        INNER JOIN ControloAutEntidadeProdutora caep ON caep.IDControloAut = a.IDControloAut AND caep.isDeleted = 0
                        INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = a.IDControloAut  AND cad.IDTipoControloAutForma = 1  AND cad.isDeleted = 0
                        INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0
                    WHERE frd.IDNivel = {0} AND frd.isDeleted = 0", IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Origination_EntProdutora_Tipo elem = new Origination_EntProdutora_Tipo();
                    // Nao se usa a coluna 0 (IDUpper)
                    if (!reader.IsDBNull(0))
                        elem.termo = reader.GetString(0);
                    else
                        elem.termo = string.Empty;

                    if (!reader.IsDBNull(1))
                        elem.IDTipoEntidadeProdutora = reader.GetInt64(1);
                    else
                        elem.IDTipoEntidadeProdutora = -1;

                    result.Add(elem);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return result;

        }

        public override ArchDesc_Acqinfo_Custodhist get_ArchDesc_Acqinfo_Custodhist(long IDNivel, IDbConnection conn) {
            ArchDesc_Acqinfo_Custodhist ret = new ArchDesc_Acqinfo_Custodhist();
            ret.acqinfo = string.Empty;
            ret.custodhist = string.Empty;

            try {
                string query = string.Format(@"
                    SELECT ctx.FonteImediataDeAquisicao, ctx.HistoriaCustodial
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN SFRDContexto ctx ON ctx.IDFRDBase = frd.ID AND ctx.isDeleted = 0
                    WHERE n.ID = {0} AND n.isDeleted = 0", IDNivel);
                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        ret.acqinfo = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        ret.custodhist = reader.GetString(1);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return ret;
        }

        public override string get_bioghist(long IDNivel, IDbConnection conn) {
            string ret = string.Empty;
            try {
                string query = string.Format(@"
                    WITH bioghist(HistoriaAdministrativa, DescHistorica) 
                    AS (
	                    SELECT ctx.HistoriaAdministrativa, NULL
	                    FROM Nivel n0
	                    INNER JOIN FRDBase frd ON frd.IDNivel = n0.ID AND frd.isDeleted = 0 
	                    INNER JOIN SFRDContexto ctx ON ctx.IDFRDBase  = frd.ID AND ctx.isDeleted = 0
	                    WHERE n0.ID = {0} AND n0.CatCode <> 'CA' AND n0.isDeleted = 0
                    		
	                    UNION ALL
                    	
	                    SELECT NULL, ca.DescHistoria
	                    FROM Nivel n
	                    INNER JOIN NivelControloAut nca ON nca.ID = n.ID AND nca.isDeleted = 0
	                    INNER JOIN ControloAut ca ON ca.ID = nca.IDControloAut AND ca.isDeleted = 0
	                    WHERE n.ID = {0}  AND n.CatCode = 'CA' AND n.isDeleted = 0
                    )
                    SELECT COALESCE(b.HistoriaAdministrativa,  b.DescHistorica) AS bioghist
                    FROM bioghist b ", IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        ret = reader.GetString(0);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return ret;
        }

        public override List<ScopeContent> get_scopecontent(long IDNivel, IDbConnection conn) {
            List<ScopeContent> ret = new List<ScopeContent>();
            try {
                string query = string.Format(@"
                    SELECT dict.Termo, idx.Selector, ca.IDTipoNoticiaAut, ce.ConteudoInformacional
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN IndexFRDCA idx ON idx.IDFRDBase = frd.ID AND idx.isDeleted = 0
                    INNER JOIN ControloAut ca ON ca.ID = idx.IDControloAut AND ca.isDeleted = 0
                    INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = idx.IDControloAut 
	                    AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                    INNER JOIN Dicionario dict ON dict.ID = cad.IDDicionario  AND cad.isDeleted = 0
                    LEFT JOIN SFRDConteudoEEstrutura ce ON ce.IDFRDBase = frd.ID AND ce.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 
                    ORDER BY ca.IDTipoNoticiaAut, idx.Selector ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    ScopeContent _new = new ScopeContent();
                    _new.dict_Termo = string.Empty;
                    _new.IDTipoNoticiaAut = -1;
                    _new.IndexFRDCA_Selector = -1000;
                    _new.ConteudoInformacional = string.Empty;
                    if (!reader.IsDBNull(0))
                        _new.dict_Termo = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        _new.IndexFRDCA_Selector = reader.GetInt32(1);
                    if (!reader.IsDBNull(2))
                        _new.IDTipoNoticiaAut = reader.GetInt64(2);
                    if (!reader.IsDBNull(3))
                        _new.ConteudoInformacional = reader.GetString(3);
                    ret.Add(_new);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return ret;
        }

        public override bool isProcessoDeObras(long IDNivel, IDbConnection conn) {
            bool ret = false;
            try {
                string query = string.Format(@"
                    SELECT  tt.BuiltInName
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN IndexFRDCA idx ON idx.IDFRDBase = frd.ID AND idx.isDeleted = 0
                    INNER JOIN ControloAut ca ON ca.ID = idx.IDControloAut AND ca.isDeleted = 0
                    INNER JOIN TipoTipologias tt ON tt.ID = ca.IDTipoTipologia AND tt.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", 
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        ret = reader.GetString(0).Equals("PROCESSO_DE_OBRAS");
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return ret;
        }

        public override ScopeContent_PROCESSO_DE_OBRAS get_scopecontent_PROCESSO_DE_OBRAS(long IDNivel, IDbConnection conn) {
            ScopeContent_PROCESSO_DE_OBRAS ret = new ScopeContent_PROCESSO_DE_OBRAS();
            ret.scopeContent = this.get_scopecontent(IDNivel, conn);
            ret.requerentes = new List<string>();
            ret.averbamentos = new List<string>();
            ret.loc_actual = new List<string>();
            ret.loc_actual_OutrasFormas = new List<Termo_Outras_Formas>();
            ret.loc_antiga = new List<string>();
            ret.tipo_obra = string.Empty;
            ret.strPH = string.Empty;
            ret.tecnicoObra = new List<string>();
            ret.tecnicoObra_OutrasFormas = new List<Termo_Outras_Formas>();
            ret.atestado = new List<string>();
            ret.datasLicenca = new List<string>();

            try {
                // Requerentes:
                string query = string.Format(@"
                    SELECT l.Nome, l.Tipo 
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN LicencaObraRequerentes l ON l.IDFRDBase = frd.ID AND l.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);
                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    // Nome: 0
                    if (!reader.IsDBNull(0)) {
                        // Tipo: 1
                        if (reader.GetString(1).Trim().Equals("INICIAL"))
                            ret.requerentes.Add(reader.GetString(0));
                        else
                            ret.averbamentos.Add(reader.GetString(0));
                    }
                }
                reader.Close();

                // Localizacao:
                // 0: actual; 1: antiga
                query = string.Format(@"
                    SELECT 1 AS LocActual, dic.Termo AS FormaAutorizada, l.NumPolicia,  
                        OutrasFormas = LEFT(o1.list, LEN(o1.list)-1)
                    FROM LicencaObraLocalizacaoObraActual l
                        INNER JOIN FRDBase frd ON frd.ID = l.IDFRDBase AND frd.isDeleted = 0 
                        INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = l.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                        INNER JOIN Dicionario dic ON dic.ID = cad.IDDicionario AND dic.isDeleted = 0
                        CROSS APPLY ( 
                            SELECT Termo + '; ' AS [text()] 
                            FROM Dicionario d
		                        INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma <> 1 AND cad.isDeleted = 0
                            WHERE d.isDeleted = 0 AND cad.IDControloAut = l.IDControloAut
                            FOR XML PATH('') 
                        ) o1 (list)

                    WHERE frd.IDNivel = {0} AND l.isDeleted = 0

                    UNION ALL
                    SELECT 0, l.NomeLocal COLLATE LATIN1_GENERAL_CS_AS, l.NumPolicia,  ''
                    FROM Nivel n
                        INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                        INNER JOIN LicencaObraLocalizacaoObraAntiga l ON l.IDFRDBase = frd.ID AND l.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0                     
                    ", IDNivel);

                command.CommandText = query;
                reader = command.ExecuteReader();

                while (reader.Read()) {
                    // Localizacao actual:
                    if (reader.GetSqlInt32(0) == 1) {
                        Termo_Outras_Formas loc_actual_OutrasFormas = new Termo_Outras_Formas();
                        loc_actual_OutrasFormas.Termo = string.Empty;
                        loc_actual_OutrasFormas.Outras_Formas = string.Empty;

                        // Rua (forma autorizada) e numero:
                        string rua_numero = reader.GetString(1) +
                            (reader.IsDBNull(2) || reader.GetString(2).Equals(string.Empty) ?
                                string.Empty : ", " + reader.GetString(2));

                        ret.loc_actual.Add(rua_numero.Trim());

                        // Outras formas: (3)
                        loc_actual_OutrasFormas.Termo = rua_numero.Trim(); //reader.GetString(1);
                        if (!reader.IsDBNull(3) && !reader.GetString(3).Equals(string.Empty)) {
                            loc_actual_OutrasFormas.Outras_Formas = reader.GetString(3);
                        }
                        ret.loc_actual_OutrasFormas.Add(loc_actual_OutrasFormas);
                    }
                    else {
                        // Localizacao antiga:
                        string rua_numero = reader.GetString(1) +
                            (reader.IsDBNull(2) || reader.GetString(2).Equals(string.Empty) ?
                                string.Empty : ", " + reader.GetString(2));
                        ret.loc_antiga.Add(rua_numero.Trim());
                    }
                }
                reader.Close();

                // TipoObra, PHTexto:
                query = string.Format(@"
                    SELECT l.TipoObra, l.PHTexto
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN LicencaObra l ON l.IDFRDBase = frd.ID AND l.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);

                command.CommandText = query;
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    // TipoObra
                    if (!reader.IsDBNull(0) )
                        ret.tipo_obra = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        ret.strPH = reader.GetString(1);
                }
                reader.Close();

                // TecnicoObra: forma autorizada e outras formas
                query = string.Format(@"
                    SELECT dic.Termo AS FormaAutorizada,
                    OutrasFormas = LEFT(o1.list, LEN(o1.list)-1)
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN LicencaObraTecnicoObra l ON l.IDFRDBase = frd.ID AND l.isDeleted = 0
                    INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = l.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                    INNER JOIN Dicionario dic ON dic.ID = cad.IDDicionario AND dic.isDeleted = 0
                    CROSS APPLY ( 
                        SELECT Termo + '; ' AS [text()] 
                        FROM Dicionario d
		                    INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma <> 1 AND cad.isDeleted = 0
                        WHERE d.isDeleted = 0 AND cad.IDControloAut = l.IDControloAut
                        FOR XML PATH('') 
                    ) o1 (list)
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);

                command.CommandText = query;
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    // TecnicoObra
                    if (!reader.IsDBNull(0)) {
                        ret.tecnicoObra.Add(reader.GetString(0));

                        Termo_Outras_Formas tecnico_OutrasFormas = new Termo_Outras_Formas();
                        tecnico_OutrasFormas.Termo = reader.GetString(0);
                        tecnico_OutrasFormas.Outras_Formas = string.Empty;
                        if (!reader.IsDBNull(1)) {
                            tecnico_OutrasFormas.Outras_Formas = reader.GetString(1);
                        }
                        ret.tecnicoObra_OutrasFormas.Add(tecnico_OutrasFormas);
                    }
                }
                reader.Close();

                // Atestado:
                query = string.Format(@"
                    SELECT l.Codigo
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN LicencaObraAtestadoHabitabilidade l ON l.IDFRDBase = frd.ID AND l.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);
                command.CommandText = query;
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    // Codigo
                    if (!reader.IsDBNull(0))
                        ret.atestado.Add(reader.GetString(0));
                }
                reader.Close();

                // DatasLicenca:
                query = string.Format(@"
                    SELECT dbo.fn_AddPaddingToDateMember_new(d.Ano, 4) + 
	                    dbo.fn_AddPaddingToDateMember_new(d.Mes, 2) + 
	                    dbo.fn_AddPaddingToDateMember_new(d.Dia, 2)
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN LicencaObraDataLicencaConstrucao d ON d.IDFRDBase = frd.ID AND d.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);
                command.CommandText = query;
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    // YYYYMMDD:
                    if (!reader.IsDBNull(0))
                        ret.datasLicenca.Add(Date_questionMarks_Format(reader.GetString(0)));
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return ret;
        }

        public override Appraisal get_Appraisal(long IDNivel, IDbConnection conn) {
            Appraisal ret = new Appraisal();
            ret.pertinencia_Nivel = string.Empty;
            ret.pertinencia_Ponderacao = string.Empty;
            ret.pertinencia_FreqUso = string.Empty;
            ret.densidade_Tipo = string.Empty;
            ret.densidade_Grau = string.Empty;
            ret.densidade_InfoRel = new List<Appraisal_InfoRelacionada>();
            ret.enqdr_Legal_Diploma = string.Empty;
            ret.enqdr_Legal_RefTblSeleccao = string.Empty;
            ret.destino_Final = string.Empty;
            ret.prazo_Conservacao = string.Empty;
            ret.auto_eliminacao = string.Empty;
            ret.observacoes = string.Empty;

            try {
                string query = string.Format(@"
                    SELECT tp.Ponderacao AS pert_ponderacao, tp.Designacao AS pert_Nivel, 
                    av.Frequencia,
                    td.Designacao AS densidade_designacao, 
                    tsd.Designacao AS subDensidade_designacao,
                    av.RefTabelaAvaliacao,
                    CASE
                    WHEN av.Preservar is null THEN ''
                    WHEN av.Preservar = 1 THEN 'Conservação'
                    WHEN av.Preservar = 0 THEN 'Eliminação'
                    END AS DestinoFinal,
                    av.PrazoConservacao,
                    ae.Designacao,
                    av.Observacoes

                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    INNER JOIN SFRDAvaliacao av ON av.IDFRDBase = frd.ID AND av.isDeleted = 0
                    LEFT JOIN AutoEliminacao ae ON ae.ID = av.IDAutoEliminacao AND ae.isDeleted = 0
                    LEFT JOIN TipoPertinencia tp ON tp.ID = av.IDPertinencia AND tp.isDeleted = 0
                    LEFT JOIN TipoDensidade td ON td.ID = av.IDDensidade
                    LEFT JOIN TipoSubDensidade tsd ON tsd.ID = av.IDSubdensidade
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    // ponderacao: pert_ponderacao
                    if (!reader.IsDBNull(0))
                        ret.pertinencia_Ponderacao = reader.GetString(0).Trim();
                    // nivel: pert_designacao
                    if (!reader.IsDBNull(1))
                        ret.pertinencia_Nivel = reader.GetString(1).Trim();
                    // Frequencia
                    if (!reader.IsDBNull(2))
                        ret.pertinencia_FreqUso = reader.GetDecimal(2).ToString().Trim();
                    // densidade_Tipo: densidade_designacao
                    if (!reader.IsDBNull(3))
                        ret.densidade_Tipo = reader.GetString(3).Trim();
                    // densidade_Grau: subDensidade_designacao
                    if (!reader.IsDBNull(4))
                        ret.densidade_Grau = reader.GetString(4).Trim();
                    // Enqdr Legal: RefTblSeleccao
                    if (!reader.IsDBNull(5))
                        ret.enqdr_Legal_RefTblSeleccao = reader.GetInt32(5).ToString().Trim();
                    // Destino final:
                    ret.destino_Final = reader.GetString(6).Trim();
                    // Prazo conservacao:
                    if (!reader.IsDBNull(7) && reader.GetInt16(7) > 0)
                        ret.prazo_Conservacao = reader.GetInt16(7).ToString().Trim();
                    // Num. do auto de eliminacao:
                    if (!reader.IsDBNull(8))
                        ret.auto_eliminacao = reader.GetString(8).Trim();
                    // Observacoes:
                    if (!reader.IsDBNull(9))
                        ret.observacoes = reader.GetString(9).Trim();

                }
                reader.Close();

                // Lista de informacao relacionada:
                query = string.Format(@"
                    SELECT nd.Designacao,
                    td.Designacao AS Desig_Tipo_Densidade, 
                    tsd.Designacao AS Desig_Grau_Densidade,
                    avr.Ponderacao
                    FROM Nivel n
                    INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = 0
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDAvaliacaoRel avr ON avr.IDFRDBase = frd.ID AND avr.isDeleted = 0
                    INNER JOIN TipoDensidade td ON td.ID = avr.Densidade AND td.isDeleted = 0
                    INNER JOIN TipoSubDensidade tsd ON tsd.ID = avr.SubDensidade AND tsd.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);

                command.CommandText = query;
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    Appraisal_InfoRelacionada val = new Appraisal_InfoRelacionada();
                    val.densidade_InfoRel_Titulo = reader.GetString(0).Trim();
                    if (!reader.IsDBNull(1))
                        val.densidade_InfoRel_Tipo = reader.GetString(1).Trim();
                    if (!reader.IsDBNull(2))
                        val.densidade_InfoRel_Grau = reader.GetString(2).Trim();
                    if (!reader.IsDBNull(3))
                        val.densidade_InfoRel_Ponderacao = reader.GetDecimal(3).ToString().Trim();
                    ret.densidade_InfoRel.Add(val);
                }
                reader.Close();

                // Enquadramento legal para o enqdr_Legal_Diploma:
                query = string.Format(@"
                    SELECT dic.Termo
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN IndexFRDCA idx ON idx.IDFRDBase = frd.ID AND Selector = 1 AND idx.isDeleted = 0
                    INNER JOIN ControloAut ca ON ca.ID = idx.IDControloAut AND ca.isDeleted = 0
                    INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = idx.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                    INNER JOIN Dicionario dic ON dic.ID = cad.IDDicionario AND dic.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);

                command.CommandText = query;
                reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        ret.enqdr_Legal_Diploma = reader.GetString(0).Trim();
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return ret;
        }

        public override string get_Accruals(long IDNivel, IDbConnection conn) {
            string ret = string.Empty;
            try {
                string query = string.Format(@"
                    SELECT ce.Incorporacao
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDConteudoEEstrutura ce ON ce.IDFRDBase = frd.ID AND ce.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        ret = reader.GetString(0);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return ret;
        }

        public override Arrangement get_Arrangement(long IDNivel, IDbConnection conn) {
            Arrangement result = new Arrangement();
            result.tradicaoDocumental = new List<string>();
            result.ordenacao = new List<string>();
            try {
                // Tradicao documental
                string query = string.Format(@"
                    SELECT ttd.Designacao
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDTradicaoDocumental td ON td.IDFRDBase = frd.ID AND td.isDeleted = 0
                    INNER JOIN TipoTradicaoDocumental ttd ON ttd.ID = td.IDTipoTradicaoDocumental AND ttd.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result.tradicaoDocumental.Add(reader.GetString(0));
                }
                reader.Close();

                // Ordenacao:
                query = string.Format(@"
                    SELECT  ord.Designacao
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDOrdenacao o ON o.IDFRDBase = frd.ID AND o.isDeleted = 0
                    INNER JOIN TipoOrdenacao ord ON ord.ID = o.IDTipoOrdenacao AND ord.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);

                command.CommandText = query;
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result.ordenacao.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return result;
        }

        public override CondicoesDeAcesso get_CondicoesDeAcesso(long IDNivel, IDbConnection conn) {
            CondicoesDeAcesso result = new CondicoesDeAcesso();
            result.CondicaoDeAcesso = string.Empty;
            result.CondicaoDeReproducao = string.Empty;
            result.AuxiliarDePesquisa = string.Empty;

            try {
                string query = string.Format(@"
                    SELECT ca.CondicaoDeAcesso, ca.CondicaoDeReproducao, ca.AuxiliarDePesquisa
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDCondicaoDeAcesso ca ON ca.IDFRDBase = frd.ID AND ca.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result.CondicaoDeAcesso = reader.GetString(0);

                    if (!reader.IsDBNull(1))
                        result.CondicaoDeReproducao = reader.GetString(1);

                    if (!reader.IsDBNull(2))
                        result.AuxiliarDePesquisa = reader.GetString(2);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return result;
        }

        public override List<LangMaterial> get_LangMaterial(long IDNivel, IDbConnection conn) {
            List<LangMaterial> result = new List<LangMaterial>();
            try {
                string query = string.Format(@"
                    SELECT iso.BibliographicCodeAlpha3, iso.LanguageNameEnglish
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDCondicaoDeAcesso ca ON ca.IDFRDBase = frd.ID AND ca.isDeleted = 0
                    INNER JOIN SFRDLingua l ON l.IDFRDBase = frd.ID AND l.isDeleted = 0
                    INNER JOIN Iso639 iso ON iso.ID = l.IDIso639 AND iso.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    LangMaterial l = new LangMaterial();
                    l.BibliographicCodeAlpha3 = string.Empty;
                    l.LanguageNameEnglish = string.Empty;
                    if (!reader.IsDBNull(0))
                        l.BibliographicCodeAlpha3 = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        l.LanguageNameEnglish = reader.GetString(1);
                    if (!l.isEmpty())
                        result.Add(l);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return result;
        }

        public override PhysTech get_PhysTech(long IDNivel, IDbConnection conn) {
            PhysTech result = new PhysTech();
            result.estado_conservacao = new List<string>();
            result.material_suporte = new List<string>();
            result.suporte_acondicionamento = new List<string>();
            result.tecnicas_registo = new List<string>();

            try {
                // suporte_acondicionamento:
                string query = string.Format(@"
                    SELECT tf.Designacao AS suporte_acondicionamento
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    LEFT JOIN SFRDFormaSuporteAcond f ON f.IDFRDBase = frd.ID AND f.isDeleted = 0
                    LEFT JOIN TipoFormaSuporteAcond tf ON tf.ID = f.IDTipoFormaSuporteAcond AND tf.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result.suporte_acondicionamento.Add(reader.GetString(0));
                }
                reader.Close();

                // material_suporte:
                query = string.Format(@"
                    SELECT ms.Designacao AS material_suporte
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    LEFT JOIN SFRDMaterialDeSuporte m ON m.IDFRDBase = frd.ID AND m.isDeleted = 0
                    LEFT JOIN TipoMaterialDeSuporte ms ON ms.ID = m.IDTipoMaterialDeSuporte AND ms.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);

                command.CommandText = query;
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result.material_suporte.Add(reader.GetString(0));
                }
                reader.Close();

                // tecnicas_registo:
                query = string.Format(@"
                    SELECT ttr.Designacao AS tecnicas_registo
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    LEFT JOIN SFRDTecnicasDeRegisto tr ON tr.IDFRDBase = frd.ID AND tr.isDeleted = 0
                    LEFT JOIN TipoTecnicasDeRegisto ttr ON ttr.ID = tr.IDTipoTecnicasDeRegisto AND ttr.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);

                command.CommandText = query;
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result.tecnicas_registo.Add(reader.GetString(0));
                }
                reader.Close();

                // estado_conservacao:
                query = string.Format(@"
                    SELECT tec.Designacao AS estado_conservacao
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    LEFT JOIN SFRDEstadoDeConservacao ec ON ec.IDFRDBase = frd.ID AND ec.isDeleted = 0
                    LEFT JOIN TipoEstadoDeConservacao tec ON tec.ID = ec.IDTipoEstadoDeConservacao AND tec.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ", IDNivel);

                command.CommandText = query;
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result.estado_conservacao.Add(reader.GetString(0));
                }
                reader.Close();

            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return result;
        }


        public override DocumentacaoAssociada get_DocumentacaoAssociada(long IDNivel, IDbConnection conn) {
            DocumentacaoAssociada result = new DocumentacaoAssociada();
            result.originalsloc = string.Empty;
            result.altformavail = string.Empty;
            result.relatedmaterial = string.Empty;
            result.bibliography = string.Empty;
            try {
                string query = string.Format(@"
                    SELECT da.ExistenciaDeOriginais AS originalsloc,
                    da.ExistenciaDeCopias as altformavail,
                    da.UnidadesRelacionadas as relatedmaterial,
                    da.NotaDePublicacao as bibliography
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDDocumentacaoAssociada da ON da.IDFRDBase = frd.ID AND da.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result.originalsloc = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        result.altformavail = reader.GetString(1);
                    if (!reader.IsDBNull(2))
                        result.relatedmaterial = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        result.bibliography = reader.GetString(3);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return result;
        }

        public override string get_NotaGeral(long IDNivel, IDbConnection conn) {
            string ret = string.Empty;
            try {
                string query = string.Format(@"
                    SELECT ng.NotaGeral
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDNotaGeral ng ON ng.IDFRDBase = frd.ID AND ng.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        ret = reader.GetString(0);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return ret;
        }

        public override Processinfo get_Processinfo(long IDNivel, IDbConnection conn) {
            Processinfo result = new Processinfo();
            result.nota_arquivista = string.Empty;

            try {
                // Notas do arquivista:
                string query = string.Format(@"
                    SELECT frd.NotaDoArquivista AS processinfo
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result.nota_arquivista = reader.GetString(0);
                }
                reader.Close();

                // Datas:
                result.datas_autores = new List<Processinfo_Date>();

                query = string.Format(@"
                    SELECT ddd.DataAutoria AS data_descricao, 
                    ddd.DataEdicao AS data_registo, 
                    op.FullName AS Operator, ta.FullName AS Authority
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN FRDBaseDataDeDescricao ddd ON ddd.IDFRDBase = frd.ID AND ddd.isDeleted = 0
                    LEFT JOIN TrusteeUser op ON op.ID = ddd.IDTrusteeOperator AND op.isDeleted = 0
                    LEFT JOIN TrusteeUser ta ON ta.ID = ddd.IDTrusteeAuthority AND ta.isDeleted = 0
                    WHERE n.ID = {0}
                    ORDER BY data_registo DESC",
                    IDNivel);
                command = new SqlCommand(query, (SqlConnection)conn);
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    Processinfo_Date info_datas = new Processinfo_Date();
                    // data_descricao:
                    if (!reader.IsDBNull(0))
                        //info_datas.data_descricao = reader.GetDateTime(0).ToString();
                        info_datas.data_descricao = reader.GetDateTime(0).ToString("yyyy/MM/dd").Replace("-", "/");
                    else
                        info_datas.data_descricao = string.Empty;
                    // data_registo:
                    if (!reader.IsDBNull(1))
                        info_datas.data_registo = reader.GetDateTime(1).ToString("yyyy/MM/dd hh:mm:ss").Replace("-", "/");
                    else
                        info_datas.data_registo = string.Empty;
                    // operador:
                    if (!reader.IsDBNull(2))
                        info_datas.operador = reader.GetString(2);
                    else
                        info_datas.operador = string.Empty;
                    // authority:
                    if (!reader.IsDBNull(3))
                        info_datas.autoridade = reader.GetString(3);
                    else
                        info_datas.autoridade = string.Empty;

                    if (!info_datas.data_registo.Equals(string.Empty) || info_datas.data_descricao.Equals(string.Empty))
                        result.datas_autores.Add(info_datas);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return result;
        }

        public override string get_descrules(long IDNivel, IDbConnection conn) {
            string ret = string.Empty;
            try {
                string query = string.Format(@"
                    SELECT  frd.RegrasOuConvencoes as descrules
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        ret = reader.GetString(0);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }
            return ret;
        }


        /*
         * Fecho transitivo (para descendentes):
         */
        public override List<NiveisDescendentes> get_All_NiveisDescendentes(long IDNivelTopo, long IDTrustee, IDbConnection conn) {
            List<NiveisDescendentes> result = new List<NiveisDescendentes>();
            try {

                string query = string.Format(@"
                    WITH Descs (ID, IDUpper, gen) AS (
	                    SELECT ID, IDUpper, 0 AS gen
	                    FROM RelacaoHierarquica 
	                    WHERE IDUpper  = {0} AND isDeleted = 0
	                    UNION ALL
	                    SELECT rh.ID, rh.IDUpper, gen + 1
	                    FROM RelacaoHierarquica rh
	                        INNER JOIN Descs d ON d.ID = rh.IDUpper
                        WHERE rh.isDeleted = 0
                    )

                    SELECT DISTINCT ID, IDUpper, gen 
                    INTO #Descs
                    FROM Descs", IDNivelTopo);
                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                command.ExecuteNonQuery();

                PermissoesRule.Current.GetEffectiveReadPermissions(" FROM #Descs ", IDTrustee, conn);

                query = @"
                    SELECT DISTINCT d.ID, d.IDUpper, 
                        n.IDTipoNivel,
                        tnr.Codigo AS Codigo_TipoNivelRelacionado, 
                        tnr.Designacao AS Designacao_TipoNivelRelacionado,
                        dict.Termo Termo_Estrutural_Filho,
                        d.gen                     
                    FROM #Descs d
                        INNER JOIN #effective E ON E.IDNivel = d.ID
                        INNER JOIN RelacaoHierarquica rh ON rh.ID = d.ID AND rh.isDeleted = 0
                        INNER JOIN Nivel n ON n.ID = rh.ID AND n.isDeleted = 0
                        INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado AND tnr.isDeleted = 0
                        LEFT JOIN NivelControloAut nca ON nca.ID = d.ID AND nca.isDeleted = 0
                        LEFT JOIN ControloAutDicionario cad ON cad.IDControloAut = nca.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                        LEFT JOIN Dicionario dict ON dict.ID = cad.IDDicionario AND dict.isDeleted = 0
                    WHERE E.Ler = 1 
                    ORDER BY d.IDUpper, d.ID";

                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    NiveisDescendentes ndesc = new NiveisDescendentes();
                    ndesc.IDNivelPai = reader.GetInt64(1);
                    ndesc.IDNivel = reader.GetInt64(0);
                    ndesc.TipoNivel = reader.GetInt64(2);
                    if (!reader.IsDBNull(4))
                        ndesc.Termo_Estrutural_Filho = reader.GetString(3);
                    ndesc.geracao = reader.GetInt32(6);

                    result.Add(ndesc);
                }
                reader.Close();

                PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);

                command.CommandText = "DROP TABLE #Descs";
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return result;
        }


        public override int get_Count_All_NiveisDescendentes(long IDNivelTopo, long IDTrustee, IDbConnection conn) {
            int result = 0;
            try {
                string query = string.Format(@"
                    WITH Descs (ID, IDUpper, gen) AS (
	                    SELECT ID, IDUpper, 0 AS gen
	                    FROM RelacaoHierarquica 
	                    WHERE IDUpper  = {0} AND isDeleted = 0
	                    UNION ALL
	                    SELECT rh.ID, rh.IDUpper, gen + 1
	                    FROM RelacaoHierarquica rh
	                        INNER JOIN Descs d ON d.ID = rh.IDUpper
                        WHERE rh.isDeleted = 0
                    )

                    SELECT DISTINCT ID, IDUpper, gen
                    INTO #Descs
                    FROM Descs", IDNivelTopo);
                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                command.ExecuteNonQuery();

                PermissoesRule.Current.GetEffectiveReadPermissions(" FROM #Descs ", IDTrustee, conn);

                command.CommandText = @"
SELECT COUNT(d.ID) 
FROM #Descs d
    INNER JOIN #effective E ON E.IDNivel = d.ID
WHERE E.Ler = 1";
                result = Convert.ToInt32(command.ExecuteScalar());

                PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);

                command.CommandText = "DROP TABLE #Descs";
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return result;
        }


        public override List<NiveisDescendentes> get_NiveisDescendentes(long IDNivel, long IDTrustee, IDbConnection conn) {
            List<NiveisDescendentes> result = new List<NiveisDescendentes>();

            try {
                string query = string.Format(" FROM RelacaoHierarquica WHERE IDUpper = {0} AND isDeleted = 0 ", IDNivel);
                PermissoesRule.Current.GetEffectiveReadPermissions(query, IDTrustee, conn);

                query = string.Format(@"
                    SELECT nfilho.ID, 
                    nfilho.IDTipoNivel,
                    tnr.Codigo AS Codigo_TipoNivelRelacionado, 
                    tnr.Designacao AS Designacao_TipoNivelRelacionado,
                    d.Termo Termo_Estrutural_Filho
                    FROM Nivel n
                    
                    INNER JOIN #effective E ON e.IDNivel = n.ID
                    INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = n.ID AND rh.isDeleted = 0
                    INNER JOIN Nivel nfilho ON nfilho.ID = rh.ID AND nfilho.isDeleted = 0
                    LEFT JOIN NivelDesignado ndf ON ndf.ID = nfilho.ID AND ndf.isDeleted = 0
                    INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado AND tnr.isDeleted = 0

                    LEFT JOIN NivelControloAut nca ON nca.ID = nfilho.ID AND nca.isDeleted = 0
                    LEFT JOIN ControloAutDicionario cad ON cad.IDControloAut = nca.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                    LEFT JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0

                    WHERE n.ID = {0}  AND n.isDeleted = 0 AND E.Ler = 1 ",
                    IDNivel, IDTrustee);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    NiveisDescendentes ndesc = new NiveisDescendentes();
                    ndesc.IDNivelPai = IDNivel;
                    ndesc.IDNivel = reader.GetInt64(0);
                    ndesc.TipoNivel = reader.GetInt64(1);
                    if (!reader.IsDBNull(4))
                        ndesc.Termo_Estrutural_Filho = reader.GetString(4);
                    ndesc.geracao = 0;

                    result.Add(ndesc);
                }
                reader.Close();

                PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return result;
        }

        public override string get_dao_href(long IDNivel, IDbConnection conn) {
            string result = string.Empty;
            try {
                string query = string.Format(@"
                    SELECT iv.Mount, im.Identificador
                    FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDImagem im ON im.IDFRDBase = frd.ID AND im.Tipo = 'Web' AND im.isDeleted = 0
                    INNER JOIN SFRDImagemVolume iv ON iv.ID = im.IDSFDImagemVolume AND im.isDeleted = 0
                    WHERE n.ID = {0}  AND n.isDeleted = 0 ",
                    IDNivel);

                SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    if (!reader.IsDBNull(0))
                        result = reader.GetString(0);
                    if (!result.EndsWith("/"))
                        result += "/";
                    if (!reader.IsDBNull(1))
                        result += reader.GetString(1);
                }
                reader.Close();
            }
            catch (Exception ex) { Trace.WriteLine(ex); throw; }

            return result;
        }

    }
}
