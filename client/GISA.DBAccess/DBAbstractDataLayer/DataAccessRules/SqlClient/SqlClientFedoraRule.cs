using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    public class SqlClientFedoraRule : FedoraRule
    {
        public override void LoadObjDigitalData(DataSet currentDataSet, long docID, long IDTipoNivelRelacionado, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.CommandText = "CREATE TABLE #niveisTemp(ID BIGINT); CREATE TABLE #odsTemp(ID BIGINT)";
                command.ExecuteNonQuery();

                if (IDTipoNivelRelacionado == 9)
                {
                    // no caso do docID ser um documento/processo é necessário obter possíveis subdocumentos

                    command.CommandText = @"
                    INSERT INTO #niveisTemp
                    SELECT ID 
                    FROM RelacaoHierarquica
                    WHERE IDTipoNivelRelacionado = @IDTipoNivelRelacionado AND IDUpper = @docID AND isDeleted = @isDeleted";
                    command.Parameters.AddWithValue("@docID", docID);
                    command.Parameters.AddWithValue("@IDTipoNivelRelacionado", 10);
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.ExecuteNonQuery();

                    // carregar rows referentes só aos subdocumentos
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #niveisTemp ON #niveisTemp.ID = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                        "INNER JOIN #niveisTemp ON #niveisTemp.ID = NivelDesignado.ID");
                    da.Fill(currentDataSet, "NivelDesignado");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDocumentoSimples"],
                        "INNER JOIN #niveisTemp ON #niveisTemp.ID = NivelDocumentoSimples.ID");
                    da.Fill(currentDataSet, "NivelDocumentoSimples");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN #niveisTemp ON #niveisTemp.ID = RelacaoHierarquica.ID");
                    da.Fill(currentDataSet, "RelacaoHierarquica");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                        "INNER JOIN #niveisTemp ON #niveisTemp.ID = FRDBase.IDNivel");
                    da.Fill(currentDataSet, "FRDBase");

                    command.Parameters.Clear();
                }

                command.CommandText = "INSERT INTO #niveisTemp (ID) VALUES (@docID)";
                command.Parameters.AddWithValue("@docID", docID);
                command.ExecuteNonQuery();

                LoadDataRows(currentDataSet, docID, conn);

                command.CommandText = "DROP TABLE #niveisTemp; DROP TABLE #odsTemp";
                command.ExecuteNonQuery();
            }
        }

        private void LoadDataRows(DataSet currentDataSet, long docID, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@SFRDImagemTipo", "Fedora");

                // carregar toda a informação referente aos objectos digitais do documento actual e seus subdocumentos
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemVolume"],
                    "INNER JOIN ( " +
                        "SELECT DISTINCT IDSFDImagemVolume " +
                        "FROM SFRDImagem " +
                            "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                            "INNER JOIN #niveisTemp ON #niveisTemp.ID = FRDBase.IDNivel " +
                        "WHERE SFRDImagem.Tipo = @SFRDImagemTipo" +
                    ") img ON img.IDSFDImagemVolume = SFRDImagemVolume.ID");
                da.Fill(currentDataSet, "SFRDImagemVolume");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagem"],
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "INNER JOIN #niveisTemp ON #niveisTemp.ID = FRDBase.IDNivel " +
                    "WHERE SFRDImagem.Tipo = @SFRDImagemTipo");
                da.Fill(currentDataSet, "SFRDImagem");

                command.CommandText = @"
                    INSERT INTO #odsTemp
                    SELECT IDObjetoDigital
                    FROM SFRDImagemObjetoDigital
                        INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx AND SFRDImagem.isDeleted = @isDeleted
                        INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase AND FRDBase.isDeleted = @isDeleted
                        INNER JOIN #niveisTemp ON #niveisTemp.ID = FRDBase.IDNivel
                    WHERE SFRDImagem.Tipo = 'Fedora' AND SFRDImagemObjetoDigital.isDeleted = @isDeleted";
                command.ExecuteNonQuery();

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ObjetoDigital"],
                    "INNER JOIN #odsTemp ON #odsTemp.ID = ObjetoDigital.ID");
                da.Fill(currentDataSet, "ObjetoDigital");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemObjetoDigital"],
                    "INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "INNER JOIN #niveisTemp ON #niveisTemp.ID = FRDBase.IDNivel " +
                    "WHERE SFRDImagem.Tipo = @SFRDImagemTipo");
                da.Fill(currentDataSet, "SFRDImagemObjetoDigital");

                // carregar permissões
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeObjetoDigitalPrivilege"],
                    "INNER JOIN #odsTemp ON #odsTemp.ID = TrusteeObjetoDigitalPrivilege.IDObjetoDigital");
                da.Fill(currentDataSet, "TrusteeObjetoDigitalPrivilege");

                // carregar ODs e respetivas permissões sem um nivel documental correspondente
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ObjetoDigital"],
                    "INNER JOIN ObjetoDigitalRelacaoHierarquica ON ObjetoDigitalRelacaoHierarquica.ID = ObjetoDigital.ID " +
                    "INNER JOIN #odsTemp ON #odsTemp.ID = ObjetoDigitalRelacaoHierarquica.IDUpper ");
                da.Fill(currentDataSet, "ObjetoDigital");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ObjetoDigitalRelacaoHierarquica"],
                    "INNER JOIN #odsTemp ON #odsTemp.ID = ObjetoDigitalRelacaoHierarquica.IDUpper");
                da.Fill(currentDataSet, "ObjetoDigitalRelacaoHierarquica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeObjetoDigitalPrivilege"],
                    "INNER JOIN ObjetoDigitalRelacaoHierarquica ON ObjetoDigitalRelacaoHierarquica.ID = TrusteeObjetoDigitalPrivilege.IDObjetoDigital " +
                    "INNER JOIN #odsTemp ON #odsTemp.ID = ObjetoDigitalRelacaoHierarquica.IDUpper ");
                da.Fill(currentDataSet, "TrusteeObjetoDigitalPrivilege");
            }
        }

        public override void LoadObjDigitalSimples(DataSet currentDataSet, long docID, long IDTipoNivelRelacionado, IDbConnection conn)
        {
            if (IDTipoNivelRelacionado < 9) return;

            using (var command = new SqlCommand("", (SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.CommandText = "CREATE TABLE #temp(ID BIGINT)";
                command.ExecuteNonQuery();

                // obter possiveis subdocumentos no caso do nivel documental actual ser um documento/processo
                if (IDTipoNivelRelacionado == 9)
                {
                    command.CommandText = @"
                        INSERT INTO #temp
                        SELECT ID 
                        FROM RelacaoHierarquica
                        WHERE IDUpper = @docID";
                    command.Parameters.AddWithValue("@docID", docID);
                    command.ExecuteNonQuery();
                }

                command.Parameters.AddWithValue("@isDeleted", 0);

                // carregar rows referentes só aos subdocumentos
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN #temp ON #temp.ID = Nivel.ID");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                    "INNER JOIN #temp ON #temp.ID = NivelDesignado.ID");
                da.Fill(currentDataSet, "NivelDesignado");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDocumentoSimples"],
                    "INNER JOIN #temp ON #temp.ID = NivelDocumentoSimples.ID");
                da.Fill(currentDataSet, "NivelDocumentoSimples");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    "INNER JOIN #temp ON #temp.ID = RelacaoHierarquica.ID");
                da.Fill(currentDataSet, "RelacaoHierarquica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "INNER JOIN #temp ON #temp.ID = FRDBase.IDNivel");
                da.Fill(currentDataSet, "FRDBase");

                command.CommandText = "DROP TABLE #temp";
                command.ExecuteNonQuery();
            }
        }

        public override void LoadObjDigitalPermissoes(DataSet currentDataSet, long nRowID, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@nRowID", nRowID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"]);
                da.Fill(currentDataSet, "Trustee");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"]);
                da.Fill(currentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"]);
                da.Fill(currentDataSet, "TrusteeGroup");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["UserGroups"]);
                da.Fill(currentDataSet, "UserGroups");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE FRDBase.IDNivel=" + nRowID.ToString());
                da.Fill(currentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemVolume"]);
                da.Fill(currentDataSet, "SFRDImagemVolume");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagem"],
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "WHERE FRDBase.IDNivel=@nRowID");
                da.Fill(currentDataSet, "SFRDImagem");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ObjetoDigital"],
                    "INNER JOIN SFRDImagemObjetoDigital ON SFRDImagemObjetoDigital.IDObjetoDigital = ObjetoDigital.ID " +
                    "INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "WHERE FRDBase.IDNivel=@nRowID");
                da.Fill(currentDataSet, "ObjetoDigital");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemObjetoDigital"],
                    "INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "WHERE FRDBase.IDNivel=@nRowID");
                da.Fill(currentDataSet, "SFRDImagemObjetoDigital");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeObjetoDigitalPrivilege"],
                    "INNER JOIN ObjetoDigital ON ObjetoDigital.ID = TrusteeObjetoDigitalPrivilege.IDObjetoDigital " +
                    "INNER JOIN SFRDImagemObjetoDigital ON SFRDImagemObjetoDigital.IDObjetoDigital = ObjetoDigital.ID " +
                    "INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "WHERE FRDBase.IDNivel=@nRowID");
                da.Fill(currentDataSet, "TrusteeObjetoDigitalPrivilege");
            }
        }

        public override void LoadObjDigitalPermissoesSimples(DataSet currentDataSet, long nRowIDUpper, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.CommandText = "CREATE TABLE #temp (ID BIGINT)";
                command.ExecuteNonQuery();

                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@nRowIDUpper", nRowIDUpper);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado", 10);

                command.CommandText = @"INSERT INTO #temp
SELECT rh.ID
FROM FRDBase frdUpper
    INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = frdUpper.IDNivel AND rh.IDTipoNivelRelacionado = @IDTipoNivelRelacionado AND rh.isDeleted = @isDeleted
WHERE frdUpper.IDNivel = @nRowIDUpper AND frdUpper.isDeleted = @isDeleted";
                command.ExecuteNonQuery();

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"]);
                da.Fill(currentDataSet, "Trustee");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"]);
                da.Fill(currentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"]);
                da.Fill(currentDataSet, "TrusteeGroup");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["UserGroups"]);
                da.Fill(currentDataSet, "UserGroups");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    "INNER JOIN #temp ON #temp.ID = RelacaoHierarquica.ID");
                da.Fill(currentDataSet, "RelacaoHierarquica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN #temp ON #temp.ID = Nivel.ID");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "INNER JOIN #temp ON #temp.ID = FRDBase.IDNivel");
                da.Fill(currentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagem"],
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "INNER JOIN #temp ON #temp.ID = FRDBase.IDNivel");
                da.Fill(currentDataSet, "SFRDImagem");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ObjetoDigital"],
                    "INNER JOIN SFRDImagemObjetoDigital ON SFRDImagemObjetoDigital.IDObjetoDigital = ObjetoDigital.ID " +
                    "INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "INNER JOIN #temp ON #temp.ID = FRDBase.IDNivel");
                da.Fill(currentDataSet, "ObjetoDigital");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemObjetoDigital"],
                    "INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "INNER JOIN #temp ON #temp.ID = FRDBase.IDNivel");
                da.Fill(currentDataSet, "SFRDImagemObjetoDigital");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemVolume"]);
                da.Fill(currentDataSet, "SFRDImagemVolume");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeObjetoDigitalPrivilege"],
                    "INNER JOIN ObjetoDigital ON ObjetoDigital.ID = TrusteeObjetoDigitalPrivilege.IDObjetoDigital " +
                    "INNER JOIN SFRDImagemObjetoDigital ON SFRDImagemObjetoDigital.IDObjetoDigital = ObjetoDigital.ID " +
                    "INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "INNER JOIN #temp ON #temp.ID = FRDBase.IDNivel");
                da.Fill(currentDataSet, "TrusteeObjetoDigitalPrivilege");

                command.CommandText = "DROP TABLE #temp";
                command.ExecuteNonQuery();
            }
        }

        public override bool GetObjDigitalSimplesPub(long nRowIDUpper, IDbConnection conn)
        {
            using (var command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@imgTipo", "Fedora");
                command.Parameters.AddWithValue("@odPublicado", 1);
                command.Parameters.AddWithValue("@nRowIDUpper", nRowIDUpper);
                command.CommandText = @"
SELECT COUNT(od.Publicado) 
FROM FRDBase frd WITH (UPDLOCK)
	INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID AND img.isDeleted = @isDeleted AND img.Tipo = @imgTipo
    INNER JOIN SFRDImagemObjetoDigital imgOD ON imgOD.IDFRDBase = img.IDFRDBase AND imgOD.idx = img.idx AND imgOD.isDeleted = @isDeleted
    INNER JOIN ObjetoDigital odUpper ON odUpper.ID = imgOD.IDObjetoDigital AND odUpper.isDeleted = @isDeleted
    INNER JOIN ObjetoDigitalRelacaoHierarquica odrh ON odrh.IDUpper = odUpper.ID AND odrh.isDeleted = @isDeleted
    INNER JOIN ObjetoDigital od ON od.ID = odrh.ID AND od.isDeleted = @isDeleted
WHERE frd.isDeleted = @isDeleted AND frd.IDNivel = @nRowIDUpper AND od.Publicado = @odPublicado";

                return System.Convert.ToInt64(command.ExecuteScalar()) > 0;
            }
        }

        public override void LoadSFRDImagemFedora(DataSet currentDataSet, long docID, long IDTipoNivelRelacionado, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@docID", docID);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado", IDTipoNivelRelacionado);
                command.Parameters.AddWithValue("@SFRDImagemTipo", "Fedora");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemVolume"]);
                da.Fill(currentDataSet, "SFRDImagemVolume");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagem"],
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "WHERE FRDBase.IDNivel=@docID AND SFRDImagem.Tipo=@SFRDImagemTipo");
                da.Fill(currentDataSet, "SFRDImagem");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ObjetoDigital"],
                    "INNER JOIN SFRDImagemObjetoDigital ON SFRDImagemObjetoDigital.IDObjetoDigital = ObjetoDigital.ID " +
                    "INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "WHERE FRDBase.IDNivel=@docID AND SFRDImagem.Tipo=@SFRDImagemTipo");
                da.Fill(currentDataSet, "ObjetoDigital");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemObjetoDigital"],
                    "INNER JOIN SFRDImagem ON SFRDImagem.IDFRDBase = SFRDImagemObjetoDigital.IDFRDBase AND SFRDImagem.idx = SFRDImagemObjetoDigital.idx " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDImagem.IDFRDBase " +
                    "WHERE FRDBase.IDNivel=@docID AND SFRDImagem.Tipo=@SFRDImagemTipo");
                da.Fill(currentDataSet, "SFRDImagemObjetoDigital");
            }
        }

        public override void GetPidsPorNvl(List<string> IDNiveis, IDbConnection conn, out List<string> pids)
        {
            GisaDataSetHelperRule.ImportIDs(IDNiveis.Select(id => System.Convert.ToInt64(id)).ToArray(), conn);

            pids = new List<string>();

            var cmd = @"
SELECT od.pid 
FROM (
	SELECT img.IDFRDBase
	FROM #temp
		INNER JOIN FRDBase frd ON frd.IDNivel = #temp.ID AND frd.isDeleted = 0
		INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID AND img.Tipo = 'Fedora' AND img.isDeleted = 0
	GROUP BY img.IDFRDBase
	HAVING COUNT(img.IDFRDBase) = 1
) imgs
	INNER JOIN SFRDImagem img ON img.IDFRDBase = imgs.IDFRDBase AND img.Tipo = 'Fedora' AND img.isDeleted = 0
	INNER JOIN SFRDImagemObjetoDigital imgOD ON imgOD.IDFRDBase = img.IDFRDBase AND imgOD.idx = img.idx AND imgOD.isDeleted = 0
	INNER JOIN ObjetoDigital od ON od.ID = imgOD.IDObjetoDigital AND od.isDeleted = 0";
            var command = new SqlCommand(cmd, (SqlConnection)conn);
            command.CommandText = cmd;
            
            var reader = command.ExecuteReader();

            while (reader.Read())
                pids.Add(reader.GetValue(0).ToString());

            reader.Close();
        }

        public override bool CanUserDeleteAnyAssocOD2UI(long nivelID, long userID, IDbConnection conn)
        {
            string cmd = string.Format(@"
                select ID into #users from Trustee where ID = {0};
                select IDGroup into #temp from UserGroups where IDUser = {0};

                select ods.ID
                into #ods
                FROM (
                select od.ID
                from FRDBase frd 
	                inner join SFRDImagem img on img.IDFRDBase = frd.ID and img.isDeleted = 0
	                inner join SFRDImagemObjetoDigital imgod on imgod.idx = img.idx and imgod.isDeleted = 0
	                inner join ObjetoDigital od on od.ID = imgod.IDObjetoDigital and od.isDeleted = 0
                where frd.IDNivel = {1} and frd.isDeleted = 0
                union
                select odSimples.ID 
                from FRDBase frd 
	                inner join SFRDImagem img on img.IDFRDBase = frd.ID and img.isDeleted = 0
	                inner join SFRDImagemObjetoDigital imgod on imgod.idx = img.idx and imgod.isDeleted = 0
	                inner join ObjetoDigital od on od.ID = imgod.IDObjetoDigital and od.isDeleted = 0
	                inner join ObjetoDigitalRelacaoHierarquica rhod on rhod.IDUpper = od.ID and rhod.isDeleted = 0
	                inner join ObjetoDigital odSimples on odSimples.ID = rhod.ID and odSimples.isDeleted = 0
                where frd.IDNivel = {1} and frd.isDeleted = 0) ods;

                select #ods.ID, coalesce(users.perm, groups.perm, 0) effectivePerm
                into #effectivePerms
                from #ods
	                left join (
		                select todp.IDObjetoDigital, todp.IDTrustee, MIN(convert(tinyint, todp.IsGrant)) as perm
		                from TrusteeObjetoDigitalPrivilege todp
		                inner join #ods on #ods.ID = todp.IDObjetoDigital
		                inner join #temp on #temp.IDGroup = todp.IDTrustee
		                where todp.IDTipoOperation = 3 
		                group by todp.IDObjetoDigital, todp.IDTrustee
	                ) groups on groups.IDObjetoDigital = #ods.ID
	                left join (
		                select todp.IDObjetoDigital, todp.IDTrustee, convert(tinyint, todp.IsGrant) as perm
		                from TrusteeObjetoDigitalPrivilege todp
		                inner join #ods on #ods.ID = todp.IDObjetoDigital
		                inner join #users on #users.ID = todp.IDTrustee
		                where todp.IDTipoOperation = 3
	                ) users on users.IDObjetoDigital = #ods.ID;
            
                select COUNT(ID) from #effectivePerms where effectivePerm = 0", userID, nivelID);

            SqlCommand command = new SqlCommand(cmd, (SqlConnection)conn);
            
            return System.Convert.ToInt64(command.ExecuteScalar()) == 0;
        }

        public override void LoadTitulos(DataSet currentDataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ObjetoDigitalTitulo"]);
                da.Fill(currentDataSet, "ObjetoDigitalTitulo");
            }
        }

        public override List<string> GetAssociatedODs(long nivelID, IDbConnection conn)
        {
            var res = new List<string>();
            var cmd = @"
                SELECT od.Titulo
                FROM Nivel n
                    INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = 0
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID AND img.isDeleted = 0
                    INNER JOIN SFRDImagemObjetoDigital imgod ON imgod.idx = img.idx AND imgod.isDeleted = 0
                    INNER JOIN ObjetoDigital od ON od.ID = imgod.IDObjetoDigital AND od.isDeleted = 0
                WHERE n.ID = " + nivelID.ToString() + " AND n.isDeleted = 0";
            SqlCommand command = new SqlCommand(cmd, (SqlConnection)conn);
            var reader = command.ExecuteReader();

            while (reader.Read())
                res.Add(reader.GetValue(0).ToString());

            reader.Close();

            return res;
        }
    }
}
