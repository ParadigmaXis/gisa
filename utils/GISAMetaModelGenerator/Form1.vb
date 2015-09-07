Imports System.Xml.XmlDocument
Imports System.Xml.XmlNode
Imports System.IO
Imports System.Xml
Imports System.Xml.Schema

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        FillcbDataBaseList()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtSchemaName As System.Windows.Forms.TextBox
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents lblSchemaName As System.Windows.Forms.Label
    Friend WithEvents lblDatabase As System.Windows.Forms.Label
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents btnGenerateXSD As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbDataBaseList As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtSchemaName = New System.Windows.Forms.TextBox
        Me.txtDatabase = New System.Windows.Forms.TextBox
        Me.txtServer = New System.Windows.Forms.TextBox
        Me.lblSchemaName = New System.Windows.Forms.Label
        Me.lblDatabase = New System.Windows.Forms.Label
        Me.lblServer = New System.Windows.Forms.Label
        Me.btnGenerateXSD = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cbDataBaseList = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(72, 32)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Generate"
        '
        'txtSchemaName
        '
        Me.txtSchemaName.Location = New System.Drawing.Point(88, 72)
        Me.txtSchemaName.Name = "txtSchemaName"
        Me.txtSchemaName.Size = New System.Drawing.Size(128, 20)
        Me.txtSchemaName.TabIndex = 13
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(88, 48)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(100, 20)
        Me.txtDatabase.TabIndex = 11
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(88, 24)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(128, 20)
        Me.txtServer.TabIndex = 9
        Me.txtServer.Text = "APOLO"
        '
        'lblSchemaName
        '
        Me.lblSchemaName.Location = New System.Drawing.Point(8, 72)
        Me.lblSchemaName.Name = "lblSchemaName"
        Me.lblSchemaName.Size = New System.Drawing.Size(80, 20)
        Me.lblSchemaName.TabIndex = 12
        Me.lblSchemaName.Text = "XML Name:"
        '
        'lblDatabase
        '
        Me.lblDatabase.Location = New System.Drawing.Point(8, 48)
        Me.lblDatabase.Name = "lblDatabase"
        Me.lblDatabase.Size = New System.Drawing.Size(80, 20)
        Me.lblDatabase.TabIndex = 10
        Me.lblDatabase.Text = "Database:"
        '
        'lblServer
        '
        Me.lblServer.Location = New System.Drawing.Point(8, 24)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(80, 20)
        Me.lblServer.TabIndex = 8
        Me.lblServer.Text = "Server:"
        '
        'btnGenerateXSD
        '
        Me.btnGenerateXSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerateXSD.Location = New System.Drawing.Point(6, 104)
        Me.btnGenerateXSD.Name = "btnGenerateXSD"
        Me.btnGenerateXSD.Size = New System.Drawing.Size(208, 23)
        Me.btnGenerateXSD.TabIndex = 7
        Me.btnGenerateXSD.Text = "Generate MetaModel"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbDataBaseList)
        Me.GroupBox1.Controls.Add(Me.lblDatabase)
        Me.GroupBox1.Controls.Add(Me.lblSchemaName)
        Me.GroupBox1.Controls.Add(Me.txtServer)
        Me.GroupBox1.Controls.Add(Me.txtDatabase)
        Me.GroupBox1.Controls.Add(Me.txtSchemaName)
        Me.GroupBox1.Controls.Add(Me.btnGenerateXSD)
        Me.GroupBox1.Controls.Add(Me.lblServer)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(221, 136)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "System Tables"
        '
        'cbDataBaseList
        '
        Me.cbDataBaseList.Location = New System.Drawing.Point(88, 48)
        Me.cbDataBaseList.Name = "cbDataBaseList"
        Me.cbDataBaseList.Size = New System.Drawing.Size(128, 21)
        Me.cbDataBaseList.TabIndex = 14
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(221, 80)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "XSD Schema"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(228, 234)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "GISA Meta-Model Generator"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'obter o nome das base de dados online no servidor (local)
    Private Sub FillcbDataBaseList()
        cbDataBaseList.Items.Clear()
        Dim conn As New OleDb.OleDbConnection(GetConnectionString(ServerName))
        Try
            conn.Open()
            Dim cmd As String
            Dim command As New OleDb.OleDbCommand("USE master; SELECT name FROM sysdatabases ORDER BY name", conn)
            Dim reader As OleDb.OleDbDataReader = command.ExecuteReader()
            While reader.Read()
                cbDataBaseList.Items.Add(reader.GetValue(0).ToString())
            End While
            reader.Close()
            conn.Close()
        Catch ex As Exception
            Console.WriteLine(ex)
            Throw
        End Try
    End Sub

#Region "xsd schema"

    Private tabelas() As String = { _
            "Dicionario", _
            "ControloAutRel", _
            "IndexFRDCA", _
            "TipoNoticiaATipoControloAForma", _
            "NivelDesignado", _
            "SFRDUFCota", _
            "SFRDImagemVolume", _
            "TipoFRDBase", _
            "TipoOperation", _
            "SFRDUFComponente", _
            "SFRDUFDescricaoFisica", _
            "TipoFunctionGroup", _
            "NivelUnidadeFisicaCodigo", _
            "SFRDAvaliacao", _
            "TipoEntidadeProdutora", _
            "GlobalConfig", _
            "FunctionOperation", _
            "TipoControloAutRel", _
            "TipoControloAutForma", _
            "Iso15924", _
            "AutoEliminacao", _
            "ControloAut", _
            "SFRDCondicaoDeAcesso", _
            "Iso639", _
            "TipoTecnicasDeRegisto", _
            "TipoNoticiaAut", _
            "TipoFormaSuporteAcond", _
            "TipoEstadoDeConservacao", _
            "TipoOrdenacao", _
            "SFRDNotaGeral", _
            "TipoTradicaoDocumental", _
            "TipoMaterialDeSuporte", _
            "ControloAutDicionario", _
            "Trustee", _
            "NivelControloAut", _
            "ControloAutEntidadeProdutora", _
            "SecurableObject", _
            "SFRDUnidadeFisica", _
            "SFRDUFAutoEliminacao", _
            "Iso3166", _
            "TipoDensidade", _
            "TipoSubDensidade", _
            "TipoSuporte", _
            "TipoMedida", _
            "Nivel", _
            "SFRDAvaliacaoRel", _
            "ClientLicense", _
            "ServerLicense", _
            "ClientActivity", _
            "TipoServer", _
            "TipoClient", _
            "FRDBase", _
            "AccessControlElement", _
            "TipoNivel", _
            "SecurableObjectNivel", _
            "TipoNivelRelacionado", _
            "TrusteeGroup", _
            "TrusteePrivilege", _
            "SFRDTradicaoDocumental", _
            "UserGroups", _
            "SFRDMaterialDeSuporte", _
            "TrusteeUser", _
            "SFRDOrdenacao", _
            "TipoFunction", _
            "ControloAutDataDeDescricao", _
            "SFRDEstadoDeConservacao", _
            "RelacaoTipoNivelRelacionado", _
            "SFRDFormaSuporteAcond", _
            "TipoPertinencia", _
            "FRDBaseDataDeDescricao", _
            "SFRDTecnicasDeRegisto", _
            "SFRDImagem", _
            "NivelUnidadeFisica", _
            "SFRDConteudoEEstrutura", _
            "ProductFunction", _
            "ControloAutDatasExistencia", _
            "SFRDContexto", _
            "SFRDDatasProducao", _
            "SFRDDocumentacaoAssociada", _
            "RelacaoHierarquica", _
            "TipoNivelRelacionadoCodigo", _
            "SFRDUFMateriaisComponente", _
            "SFRDUFTecnicasRegComponente", _
            "TipoAcondicionamento", _
            "TipoEstadoConservacao", _
            "TipoTecnicaRegisto", _
            "TipoMaterial"}

    Dim sw2 As System.IO.StreamWriter = New System.IO.StreamWriter("GISADataset.xml")
    Dim myDataSet As GISADataset
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'esta função não devolve os valores default das colunas

        Me.Cursor = Cursors.WaitCursor


        Dim s As String
        Dim dt As DataTable
        Dim dc As DataColumn

        myDataSet = New GISADataset

        'If tab.Length <> myDataSet.Tables.Count Then Return

        'For Each a As String In tab
        '    If Not myDataSet.Tables.Contains(a) Then
        '        Console.WriteLine(a)
        '    End If
        'Next

        sw2.WriteLine("<schema>")
        sw2.WriteLine("    <nome>GISA</nome>")
        'sw2.WriteLine("    <path><\path>")

        sw2.WriteLine("    <tabelas>")

        For Each a As String In tabelas
            dt = myDataSet.Tables(a)

            sw2.WriteLine(String.Format("        <tab nome=""{0}"" >", dt.TableName))

            sw2.WriteLine("            <colunas>")
            For Each dc In dt.Columns
                If dc.AutoIncrement Then
                    sw2.WriteLine(String.Format("            <coluna nome=""{0}"" identidade=""True"" tipo={1} valor_inicial=""{2}"" passo=""{3}"" />", dc.ColumnName, Tipo((dc.DataType).ToString(), dc.MaxLength), dc.AutoIncrementSeed + 1, dc.AutoIncrementStep))
                End If
            Next

            For Each dc In dt.Columns
                If Not dc.AutoIncrement Then
                    sw2.WriteLine(String.Format("                <coluna nome=""{0}"" identidade=""False"" tipo={1} nullable=""{2}"" default=""{3}"" />", dc.ColumnName, Tipo((dc.DataType).ToString(), dc.MaxLength), dc.AllowDBNull, dc.DefaultValue))
                End If
            Next
            sw2.WriteLine("            </colunas>")

            If dt.PrimaryKey.Length > 0 Then
                sw2.WriteLine("            <pk>")
                For Each dc In dt.PrimaryKey
                    sw2.WriteLine(String.Format("                <chave>{0}</chave>", dc.ColumnName))
                Next
                sw2.WriteLine("            </pk>")
            End If

            If dt.ParentRelations.Count() > 0 Then
                sw2.WriteLine("            <fk>")
                Dim dro As DataRelation
                For Each dro In dt.ParentRelations

                    Dim col As DataColumn
                    sw2.WriteLine("                <chave>")

                    For Each col In dro.ChildKeyConstraint.Columns
                        sw2.WriteLine(String.Format("                    <col_estrangeira>{0}</col_estrangeira>", col.ColumnName))
                    Next
                    sw2.WriteLine(String.Format("                    <ref_tab>{0}</ref_tab>", dro.ParentTable))

                    For Each col In dro.ParentKeyConstraint.Columns
                        sw2.WriteLine(String.Format("                    <ref_col>{0}</ref_col>", col.ColumnName))
                    Next

                    sw2.WriteLine("                </chave>")

                    'sw2.WriteLine(String.Format("            <chave col_estrangeira=""{0}"" ref_tab=""{1}"" ref_id=""{2}"" />", dro.ChildKeyConstraint.Columns(0), dro.ParentTable, dro.ParentKeyConstraint.Columns(0)))
                    'Console.WriteLine(String.Format("FK: {0}; tabela: {1}; PK: {2}", dro.ChildKeyConstraint.Columns(0), dro.ParentTable, dro.ParentKeyConstraint.Columns(0)))
                Next
                sw2.WriteLine("            </fk>")

            End If

            Dim uniqueConstraints As ArrayList = GetUniqueConstraints(dt)
            If uniqueConstraints.Count > 0 Then
                sw2.WriteLine("            <unique>")
                sw2.WriteLine("                <restricao>")
                For Each constraint As DataColumn() In uniqueConstraints
                    For Each col As DataColumn In constraint
                        sw2.WriteLine(String.Format("                    <ref_col>{0}</ref_col>", col.ColumnName))
                    Next
                Next
                sw2.WriteLine("                </restricao>")
                sw2.WriteLine("            </unique>")
            End If
            sw2.WriteLine("        </tab>")

        Next

        sw2.WriteLine("    </tabelas>")
        sw2.WriteLine("</schema>")
        sw2.Close()
        Me.Cursor = Cursors.Default
    End Sub

    Private Function Tipo(ByVal t As String, ByVal l As Integer) As String
        If t.Equals("System.Int64") Then
            'Return "BIGINT"
            Return String.Format("""{0}"" tamanho=""{1}""", "integer", "8B")
        ElseIf t.Equals("System.Int32") Then
            'Return "INT"
            Return String.Format("""{0}"" tamanho=""{1}""", "integer", "4B")
        ElseIf t.Equals("System.Int16") Then
            'Return "SMALLINT"
            Return String.Format("""{0}"" tamanho=""{1}""", "integer", "2B")
        ElseIf t.Equals("System.Byte") Then
            'Return "TINYINT"
            Return String.Format("""{0}"" tamanho=""{1}""", "integer", "1B")
        ElseIf t.Equals("System.Boolean") Then
            Return """bit"""
        ElseIf t.Equals("System.Decimal") Then
            Return """numeric"""
        ElseIf t.Equals("System.DateTime") Then
            Return """datetime"""
        ElseIf t.Equals("System.String") Then
            'algumas colunas tem tamanho fixo:
            ' Dicionario -> CatCode
            ' Trustee -> CatCode
            ' Nivel -> CatCode
            ' SecurableObject -> CatCode
            Return String.Format("""string"" encoding=""UNICODE"" tamanho=""{0}"" comp_variavel=""True""", l)
        ElseIf t.Equals("System.Byte[]") Then
            Return """timestamp"""
        Else
            Return ""
        End If

    End Function

    Private Function HasUniqueColumns(ByVal table As DataTable) As Boolean
        For Each col As DataColumn In table.Columns
            If col.Unique Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function GetUniqueConstraints(ByVal table As DataTable) As ArrayList
        Dim constraints As New ArrayList 'cada elemento da lista é uma array de colunas correspondente a uma restrição
        Dim uc As UniqueConstraint
        For Each constraint As Constraint In table.Constraints
            If TypeOf constraint Is UniqueConstraint Then
                uc = DirectCast(constraint, System.Data.UniqueConstraint)
                If Not uc.IsPrimaryKey Then
                    constraints.Add(uc.Columns)
                End If
            End If
        Next
        Return constraints
    End Function
#End Region

#Region "system tables"

    Private ReadOnly Property ServerName() As String
        Get
            Return txtServer.Text
        End Get
    End Property

    Private ReadOnly Property DatabaseName() As String
        Get
            Return cbDataBaseList.SelectedItem.ToString()
        End Get
    End Property

    Private Function GetConnectionString(ByVal ServerName As String) As String
        Return "Provider=SQLOLEDB;Data Source=" + ServerName + ";Initial Catalog=master;Persist Security Info=True;Integrated Security=SSPI;"
    End Function

    Private Sub BuildXML(ByVal filename As String)
        Me.Cursor = Cursors.WaitCursor
        GetTablesInfo()
        Dim tablesOrdered As ArrayList = GetTabelasOrdenadas()
        ScriptTablesData()
        Dim xwriter As XmlTextWriter
        Try
            xwriter = New XmlTextWriter(filename, System.Text.Encoding.UTF8)
            xwriter.Formatting = Formatting.Indented
            xwriter.WriteProcessingInstruction("xml", "version=""1.0"" encoding=""UTF-8""")
            xwriter.WriteStartElement("schema")
            xwriter.WriteElementString("nome", "GISA")
            xwriter.WriteStartElement("tabelas")

            For Each tabela As tableDepth In tablesOrdered
                Dim tab As DataRow = metads.Tables("sysobjects").Select(String.Format("name='{0}'", tabela.tab))(0)
                xwriter.WriteStartElement("tab") ' tabela
                xwriter.WriteAttributeString("nome", tab("name"))
                Dim desc As DataRow() = metads.Tables("sys.extended_properties").Select(String.Format("major_id={0}", tab("id")))
                If desc.Length > 0 Then
                    xwriter.WriteAttributeString("descricao", desc(0)("value"))
                End If

                'inserir as colunas por cada tabela
                xwriter.WriteStartElement("colunas") 'colunas
                For Each col As DataRow In metads.Tables("syscolumns").Select("id=" + tab("id").ToString(), "id,colorder")
                    xwriter.WriteStartElement("coluna") 'coluna
                    xwriter.WriteAttributeString("nome", col("name"))

                    If col("colstat") = 1 Then
                        ' no caso de haver uma coluna com autonumber, o seu tipo terá que ser forçosamente inteiro
                        xwriter.WriteAttributeString("identidade", "True")
                        Dim tipoRow As DataRow = metads.Tables("systypes").Select("xtype=" + col("xtype").ToString())(0)
                        xwriter.WriteAttributeString("tipo", TranslateType(tipoRow("name")))
                        xwriter.WriteAttributeString("tamanho", GetIntLength(tipoRow("name")))
                        xwriter.WriteAttributeString("valor_inicial", GetIdentitySeed(tab("name")))
                        xwriter.WriteAttributeString("passo", GetIdentityIncrement(tab("name")))
                        xwriter.WriteEndElement() 'fechar coluna
                    Else
                        xwriter.WriteAttributeString("identidade", "False")
                        Dim tipoRow As DataRow = metads.Tables("systypes").Select("xtype=" + col("xtype").ToString())(0)
                        xwriter.WriteAttributeString("tipo", TranslateType(tipoRow("name")))

                        If TranslateType(tipoRow("name")).Equals("integer") Then
                            xwriter.WriteAttributeString("tamanho", GetIntLength(tipoRow("name")))
                        ElseIf TranslateType(tipoRow("name")).Equals("string") Then
                            If tipoRow("name").Equals("nchar") OrElse tipoRow("name").Equals("nvarchar") OrElse tipoRow("name").Equals("ntext") Then
                                xwriter.WriteAttributeString("encoding", "UNICODE")
                            End If
                            If tipoRow("name").Equals("ntext") Then
                                xwriter.WriteAttributeString("tamanho", "1073741823")
                            ElseIf tipoRow("name").Equals("nchar") OrElse tipoRow("name").Equals("nvarchar") Then
                                xwriter.WriteAttributeString("tamanho", CInt(col("length")) / 2)
                            Else
                                xwriter.WriteAttributeString("tamanho", col("length"))
                            End If
                            If tipoRow("name").Equals("varchar") OrElse tipoRow("name").Equals("nvarchar") OrElse tipoRow("name").Equals("ntext") Then
                                xwriter.WriteAttributeString("comp_variavel", "True")
                            Else
                                xwriter.WriteAttributeString("comp_variavel", "False")
                            End If
                        End If

                        If DirectCast(col("isnullable"), Integer) = 1 Then
                            xwriter.WriteAttributeString("nullable", "True")
                        Else
                            xwriter.WriteAttributeString("nullable", "False")
                        End If

                        Dim defaultValue As DataRow() = metads.Tables("syscomments").Select("id=" + col("cdefault").ToString())
                        If defaultValue.Length > 0 Then
                            xwriter.WriteAttributeString("default", GetDefaultValue(defaultValue(0)("Text").ToString()))
                        Else
                            xwriter.WriteAttributeString("default", "")
                        End If

                        Dim descricao As DataRow() = metads.Tables("sys.extended_properties").Select(String.Format("major_id={0} and minor_id={1}", col("id").ToString(), col("colorder")))
                        If descricao.Length > 0 Then
                            xwriter.WriteAttributeString("descricao", descricao(0)("value").ToString())
                        End If
                        xwriter.WriteEndElement() 'fechar coluna
                    End If
                Next
                xwriter.WriteEndElement() 'fechar colunas da tabela

                'chaves primarias
                xwriter.WriteStartElement("pk") 'chaves primárias
                Dim pk As New ArrayList
                pk = GetPrimaryKeys(tab("name").ToString())
                For Each pkey As String In pk
                    xwriter.WriteElementString("chave", pkey)
                Next
                xwriter.WriteEndElement() 'fechar chaves primárias

                'chaves estrangeiras
                If metads.Tables("sysreferences").Select("fkeyid=" + tab("id").ToString()).Length > 0 Then
                    xwriter.WriteStartElement("fk") 'chaves estrangeiras

                    For Each dr As DataRow In metads.Tables("sysreferences").Select("fkeyid=" + tab("id").ToString())
                        xwriter.WriteStartElement("chave")
                        For i As Integer = 1 To 16
                            If dr.IsNull("fkey" + i.ToString()) Or CInt(dr("fkey" + i.ToString())) <= 0 Then
                                Exit For
                            End If
                            Dim foreignquery As String = String.Format("id={0} AND colid={1}", dr("fkeyid").ToString(), dr("fkey" + i.ToString()).ToString())
                            Dim referencesquery As String = String.Format("id={0} AND colid={1}", dr("rkeyid").ToString(), dr("rkey" + i.ToString()).ToString())
                            Dim parentColumnName As String = metads.Tables("syscolumns").Select(foreignquery)(0)("name").ToString()
                            Dim childColumnName As String = metads.Tables("syscolumns").Select(referencesquery)(0)("name").ToString()
                            xwriter.WriteElementString("col_estrangeira", parentColumnName)
                            xwriter.WriteElementString("ref_col", childColumnName)
                        Next
                        xwriter.WriteElementString("ref_tab", metads.Tables("sysobjects").Select("id=" + dr("rkeyid").ToString())(0)("name").ToString())
                        xwriter.WriteEndElement()
                    Next

                    xwriter.WriteEndElement() 'fechar chaves estrangeiras
                End If

                'restrições únicas
                If HasUniqueConstraints(tab("name").ToString()) Then
                    xwriter.WriteStartElement("unique") 'restrições únicas
                    For Each uc As ArrayList In GetUniqueConstraints(tab("name").ToString())
                        xwriter.WriteStartElement("restricao") 'restrção única
                        For Each coluna As String In uc
                            xwriter.WriteElementString("ref_col", coluna)
                        Next
                        xwriter.WriteEndElement() 'fechar restrção única
                    Next
                    xwriter.WriteEndElement() 'fechar restrições únicas
                End If

                'informacao
                If ds.Tables(tab("name").ToString()).Rows.Count > 0 Then
                    xwriter.WriteStartElement("informacao") 'informacao

                    If IsSelfChild(tab("name").ToString()) Then
                        ScriptSelfChildData(xwriter, tab)
                    Else
                        For Each row As DataRow In ds.Tables(tab("name").ToString()).Rows
                            xwriter.WriteStartElement("linha") 'linha
                            For Each column As DataColumn In ds.Tables(tab("name").ToString()).Columns
                                Dim sysCol As DataRow = metads.Tables("syscolumns").Select(String.Format("name='{0}' AND id={1}", column.ColumnName, tab("id").ToString()))(0)
                                ' nao gravar os valores do timestamp
                                Dim tipoRow As DataRow = metads.Tables("systypes").Select("xtype=" + sysCol("xtype").ToString())(0)
                                If Not tipoRow("name").equals("timestamp") Then
                                    If row(column) Is DBNull.Value Then
                                        xwriter.WriteAttributeString(column.ColumnName, "null")
                                    ElseIf tipoRow("name").equals("bit") Then
                                        'forçar que os valores das colunas do tipo bit sejam 0 ou 1 (caso não sejam null)
                                        xwriter.WriteAttributeString(column.ColumnName, translateBoolean(row(column)).ToString())
                                    ElseIf TranslateType(tipoRow("name").tostring()).Equals("integer") Then
                                        xwriter.WriteAttributeString(column.ColumnName, row(column).ToString())
                                    ElseIf TranslateType(tipoRow("name").tostring()).Equals("datetime") Then
                                        xwriter.WriteAttributeString(column.ColumnName, String.Format("'{0}'", row(column).ToString()))
                                    Else
                                        xwriter.WriteAttributeString(column.ColumnName, String.Format("'{0}'", row(column).ToString()))
                                    End If

                                End If
                            Next
                            xwriter.WriteEndElement() 'fechar linha
                        Next
                    End If

                    xwriter.WriteEndElement() 'fechar informacao
                End If
                xwriter.WriteEndElement() 'fechar tabela
            Next
            xwriter.WriteEndElement()
            xwriter.WriteEndElement()
        Catch ex As XmlException
            Console.WriteLine(ex)
            Throw
        Catch e As Exception
            Console.WriteLine(e)
            Throw
        Finally
            If Not xwriter Is Nothing Then
                xwriter.Close()
            End If
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private metads As New DataSet
    Private Sub GetTablesInfo()
        Dim conn As New OleDb.OleDbConnection(GetConnectionString(ServerName))
        conn.Open()
        Dim filter As String = "WHERE sysobjects.xtype like 'U' and sysobjects.name not like 'dtproperties' and sysobjects.name not like 'sys%'"
        Dim da As New OleDb.OleDbDataAdapter("USE " + DatabaseName + "; SELECT DISTINCT sysobjects.* FROM sysobjects " + filter + ";", conn)

        da.Fill(metads, "sysobjects")
        metads.Tables("sysobjects").PrimaryKey = New DataColumn() {metads.Tables("sysobjects").Columns("id")}

        da.SelectCommand.CommandText = "USE " + DatabaseName + "; SELECT DISTINCT syscolumns.* FROM syscolumns inner join sysobjects on syscolumns.id=sysobjects.id " + filter + ";"
        da.Fill(metads, "syscolumns")
        metads.Tables("syscolumns").PrimaryKey = New DataColumn() {metads.Tables("syscolumns").Columns("id"), metads.Tables("syscolumns").Columns("colid")}

        da.SelectCommand.CommandText = "USE " + DatabaseName + "; SELECT DISTINCT sysreferences.* FROM sysreferences inner join sysobjects on sysreferences.fkeyid=sysobjects.id or sysreferences.rkeyid=sysobjects.id " + filter + ";"
        da.Fill(metads, "sysreferences")

        da.SelectCommand.CommandText = "USE " + DatabaseName + "; SELECT DISTINCT syscomments.* FROM syscomments inner join syscolumns on syscolumns.cdefault = syscomments.id inner join sysobjects on syscolumns.id=sysobjects.id " + filter + ";"
        da.Fill(metads, "syscomments")

        da.SelectCommand.CommandText = "USE " + DatabaseName + "; SELECT DISTINCT sys.extended_properties.* FROM sys.extended_properties inner join sysobjects on sys.extended_properties.major_id=sysobjects.id " + filter + ";"
        da.Fill(metads, "sys.extended_properties")

        da.SelectCommand.CommandText = "USE " + DatabaseName + "; SELECT * FROM systypes;"
        da.Fill(metads, "systypes")
    End Sub

    Private Function TranslateType(ByVal sqlType As String) As String
        Select Case sqlType
            Case "bigint", "int", "smallint", "tinyint"
                Return "integer"
            Case "char", "varchar", "nchar", "nvarchar", "ntext"
                Return "string"
            Case "timestamp"
                Return "timestamp"
            Case "bit"
                Return "bit"
            Case "numeric"
                Return "numeric"
            Case "datetime"
                Return "datetime"
            Case "datetime2"
                Return "datetime2"
            Case Else
                Debug.Assert(False, "unknown type")
        End Select
    End Function

    Private Function GetIntLength(ByVal sqlType As String) As String
        Select Case sqlType
            Case "bigint"
                Return "8B"
            Case "int"
                Return "4B"
            Case "smallint"
                Return "2B"
            Case "tinyint"
                Return "1B"
        End Select
    End Function

    Private Function GetDefaultValue(ByVal txt As String) As String
        If txt.StartsWith("(") Then
            txt = txt.Substring(1)
        Else
            txt = txt.Substring(0)
        End If
        If txt.EndsWith(")") Then
            txt = txt.Substring(0, txt.Length - 1)
        Else
            txt = txt.Substring(0, txt.Length - 0)
        End If
        Return txt
    End Function

    Private Function GetIdentitySeed(ByVal tableName As String) As String
        Dim conn As New OleDb.OleDbConnection(GetConnectionString(ServerName))
        conn.Open()
        Dim command As New OleDb.OleDbCommand("USE " + DatabaseName + "; SELECT IDENT_SEED ('" + tableName + "')", conn)
        Dim result As String = command.ExecuteScalar().ToString()
        conn.Close()
        Return result
    End Function

    Private Function GetIdentityIncrement(ByVal tableName As String) As String
        Dim conn As New OleDb.OleDbConnection(GetConnectionString(ServerName))
        conn.Open()
        Dim command As New OleDb.OleDbCommand("USE " + DatabaseName + "; SELECT IDENT_INCR ('" + tableName + "')", conn)
        Dim result As String = command.ExecuteScalar().ToString()
        conn.Close()
        Return result
    End Function

    Private Function GetPrimaryKeys(ByVal tableName As String) As ArrayList
        Dim pk As New ArrayList
        Dim conn As New OleDb.OleDbConnection(GetConnectionString(ServerName))
        conn.Open()
        Dim command As New OleDb.OleDbCommand("USE " + DatabaseName + "; EXEC sp_pkeys '" + tableName + "'", conn)
        Dim reader As OleDb.OleDbDataReader = command.ExecuteReader()
        While reader.Read()
            pk.Add(reader.GetValue(3))
        End While
        reader.Close()
        conn.Close()
        Return pk
    End Function

    Private Function HasUniqueConstraints(ByVal tableName As String) As Boolean
        Dim conn As New OleDb.OleDbConnection(GetConnectionString(ServerName))
        conn.Open()
        Dim cmd As String = _
            "USE " + DatabaseName + "; " + _
            "SELECT COUNT(*) " + _
            "FROM sysobjects curr " + _
                "INNER JOIN sysobjects parent ON parent.id = curr.parent_obj " + _
            "WHERE parent.name LIKE '" + tableName + "' " + _
                "AND curr.xtype LIKE 'UQ'"

        Dim command As New OleDb.OleDbCommand(cmd, conn)
        Dim numberUC As Integer = CInt(command.ExecuteScalar())
        conn.Close()
        Return numberUC > 0
    End Function

    Private Function GetUniqueConstraints(ByVal tableName As String) As ArrayList
        Dim ucs As New ArrayList ' estrutura onde vão ser guardadas todas as restrições únicas
        Dim conn As New OleDb.OleDbConnection(GetConnectionString(ServerName))
        conn.Open()

        'obter ids das restrições únicas na tabela sysobjects
        Dim cmd As String = _
            "USE " + DatabaseName + "; " + _
            "SELECT curr.id AS cnst_id, curr.parent_obj AS table_id " + _
            "FROM sysobjects curr " + _
                "INNER JOIN sysobjects parent ON parent.id = curr.parent_obj " + _
            "WHERE parent.name LIKE '" + tableName + "' " + _
                "AND curr.xtype LIKE 'UQ'"
        Dim command As New OleDb.OleDbCommand(cmd, conn)
        Dim reader As OleDb.OleDbDataReader = command.ExecuteReader()

        Dim cnst_id As New ArrayList
        Dim table_id As New ArrayList
        While reader.Read()
            cnst_id.Add(reader.GetValue(0))
            table_id.Add(reader.GetValue(1))
        End While

        reader.Close()

        'obter ids das restrições únicas na tabela sysindexes
        For i As Integer = 0 To cnst_id.Count() - 1
            Dim uc As New ArrayList ' estrutura onde vão ser guardadas todas as colunas de cada restrição única
            cmd = _
                "USE " + DatabaseName + "; " + _
                "SELECT	indid " + _
                "FROM sysindexes " + _
                "WHERE name = object_name(" + cnst_id(i).ToString() + ") " + _
                    "AND id = " + table_id(i).ToString()

            command.CommandText = cmd
            Dim indid As Integer = CInt(command.ExecuteScalar())

            Dim j As Integer = 1
            While True
                cmd = String.Format("USE " + DatabaseName + "; SELECT index_col('{0}', {1}, {2})", tableName, indid, j)
                command.CommandText = cmd
                Dim coluna As String = command.ExecuteScalar().ToString()
                If coluna.Length = 0 Then
                    Exit While
                End If
                uc.Add(coluna)
                j += 1
            End While
            ucs.Add(uc)
        Next

        conn.Close()
        Return ucs
    End Function

    'retorna true se uma tabela está relacionada com ela propria
    Private Function IsSelfChild(ByVal tabName As String) As Boolean
        Dim tab As DataRow = metads.Tables("sysobjects").Select(String.Format("name='{0}'", tabName))(0)
        If metads.Tables("sysreferences").Select("fkeyid=" + tab("id").ToString() + "AND rkeyid=" + tab("id").ToString()).Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ScriptSelfChildData(ByVal xwriter As XmlTextWriter, ByVal tab As DataRow)
        Dim tabName As String = tab("name").ToString()

        'obter as colunas que compõe a relação da tabela com ela própria
        Dim foreignColumns As New ArrayList
        Dim parentColumns As New ArrayList
        For Each dr As DataRow In metads.Tables("sysreferences").Select("fkeyid=" + tab("id").ToString())
            If tabName.Equals(metads.Tables("sysobjects").Select("id=" + dr("rkeyid").ToString())(0)("name")) Then
                For i As Integer = 1 To 16
                    If dr.IsNull("fkey" + i.ToString()) Or CInt(dr("fkey" + i.ToString())) <= 0 Then
                        Exit For
                    End If
                    Dim foreignquery As String = String.Format("id={0} AND colid={1}", dr("fkeyid").ToString(), dr("fkey" + i.ToString()).ToString())
                    Dim referencesquery As String = String.Format("id={0} AND colid={1}", dr("rkeyid").ToString(), dr("rkey" + i.ToString()).ToString())
                    'Dim parentColumnName As String = metads.Tables("syscolumns").Select(foreignquery)(0)("name").ToString()
                    'Dim childColumnName As String = metads.Tables("syscolumns").Select(referencesquery)(0)("name").ToString()
                    parentColumns.Add(metads.Tables("syscolumns").Select(referencesquery)(0)("name"))
                    foreignColumns.Add(metads.Tables("syscolumns").Select(foreignquery)(0)("name"))
                Next
            End If
        Next

        Dim query As New System.Text.StringBuilder
        For Each foreignColumn As String In foreignColumns
            If query.Length > 0 Then
                query.Append(" AND ")
            End If

            If ds.Tables(tabName).Columns(foreignColumn).DataType Is GetType(String) OrElse _
                ds.Tables(tabName).Columns(foreignColumn).DataType Is GetType(Byte) OrElse _
                ds.Tables(tabName).Columns(foreignColumn).DataType Is GetType(Int64) Then

                query.AppendFormat("Isnull({0},1) = 1", foreignColumn)
            Else
                query.AppendFormat("Isnull({0},'Null Column') = 'Null Column'", foreignColumn)
            End If

        Next

        Dim query2 As New System.Text.StringBuilder
        While True
            If query.Length = 0 Then
                Exit While
            End If
            query2 = New System.Text.StringBuilder
            For Each row As DataRow In ds.Tables(tabName).Select(query.ToString())
                Dim query3 As New System.Text.StringBuilder
                For Each col As String In parentColumns
                    If query3.Length > 0 Then
                        query3.Append(" AND ")
                    End If
                    query3.AppendFormat("{0} = {1}", foreignColumns(parentColumns.IndexOf(col)).ToString(), row(col))
                Next

                If query2.Length > 0 Then
                    query2.Append(" OR ")
                End If

                If query3.Length > 0 Then
                    query2.AppendFormat("({0})", query3.ToString())
                End If

                xwriter.WriteStartElement("linha") 'linha
                For Each column As DataColumn In ds.Tables(tab("name").ToString()).Columns
                    Dim sysCol As DataRow = metads.Tables("syscolumns").Select(String.Format("name='{0}' AND id={1}", column.ColumnName, tab("id").ToString()))(0)
                    ' nao gravar os valores do timestamp
                    Dim tipoRow As DataRow = metads.Tables("systypes").Select("xtype=" + sysCol("xtype").ToString())(0)
                    If Not tipoRow("name").Equals("timestamp") Then
                        If row(column) Is DBNull.Value Then
                            xwriter.WriteAttributeString(column.ColumnName, "null")
                        ElseIf tipoRow("name").Equals("bit") Then
                            'forçar que os valores das colunas do tipo bit sejam 0 ou 1 (caso não sejam null)
                            xwriter.WriteAttributeString(column.ColumnName, CInt(row(column)))
                        ElseIf TranslateType(tipoRow("name").ToString()).Equals("integer") Then
                            xwriter.WriteAttributeString(column.ColumnName, row(column).ToString())
                        ElseIf TranslateType(tipoRow("name").ToString()).Equals("datetime") Then
                            xwriter.WriteAttributeString(column.ColumnName, String.Format("'{0}'", row(column).ToString()))
                        Else
                            xwriter.WriteAttributeString(column.ColumnName, String.Format("'{0}'", row(column).ToString()))
                        End If
                    End If
                Next
                xwriter.WriteEndElement() 'fechar linha
            Next
            query = query2
        End While
    End Sub

    Private Function translateBoolean(ByVal val As Boolean) As Integer
        If val Then
            Return 1
        Else
            Return 0
        End If
    End Function

    Private Sub txt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServer.TextChanged, txtDatabase.TextChanged, txtSchemaName.TextChanged, cbDataBaseList.SelectedIndexChanged
        UpdateButtonstate()
        'FillcbDataBaseList()
    End Sub

    Private Sub UpdateButtonstate()
        If txtServer.Text.Trim().Length > 0 AndAlso _
            Not cbDataBaseList.SelectedItem Is Nothing AndAlso _
            txtSchemaName.Text.Trim().Length > 0 Then

            btnGenerateXSD.Enabled = True
        Else
            btnGenerateXSD.Enabled = False
        End If
    End Sub

    Private Sub btnGenerateXSD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateXSD.Click
        Dim mSaveDialog As SaveFileDialog = New SaveFileDialog
        mSaveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        mSaveDialog.AddExtension = True
        mSaveDialog.DefaultExt = "xml"
        mSaveDialog.Filter = "XML (*.xml)|*.xml"
        mSaveDialog.OverwritePrompt = True
        mSaveDialog.ValidateNames = True
        mSaveDialog.FileName = txtSchemaName.Text + ".xml"
        If mSaveDialog.ShowDialog() = DialogResult.OK Then
            mSaveDialog.InitialDirectory = New System.IO.FileInfo(mSaveDialog.FileName).Directory.ToString()
            BuildXML(mSaveDialog.FileName)
        End If
    End Sub

    Dim ds As New DataSet
    'obter toda informação contida em cada tabela da base de dados
    Private Sub ScriptTablesData()
        Dim conn As New OleDb.OleDbConnection(GetConnectionString(ServerName))
        conn.Open()
        Dim cmd As String
        Dim command As New OleDb.OleDbCommand("", conn)

        Dim da As New OleDb.OleDbDataAdapter(String.Empty, conn)
        Dim str As String = "insert into "
        Dim colNames As New System.Text.StringBuilder
        Dim colValues As New System.Text.StringBuilder
        For Each tab As DataRow In metads.Tables("sysobjects").Rows
            da.SelectCommand.CommandText = "USE " + DatabaseName + "; SELECT * FROM " + tab("name").ToString() + ";"
            da.Fill(ds, tab("name").ToString())
        Next
        conn.Close()
    End Sub
#End Region

#Region " Calculate Tables Order "
    Public Structure tableDepth
        Dim tab As String
        Dim dep As Integer
        Public Sub New(ByVal tab As String, ByVal dep As Integer)
            Me.tab = tab
            Me.dep = dep
        End Sub
    End Structure

    Dim orderedTables As New ArrayList
    Public Function GetTabelasOrdenadas() As ArrayList
        orderedTables.Clear()
        'obter as tabelas que não tem chaves estrangeiras (hierarquicamente sem filhas)
        For Each tab As DataRow In metads.Tables("sysobjects").Rows
            If metads.Tables("sysreferences").Select("fkeyid=" + tab("id").ToString()).Length = 0 Then
                orderedTables.Add(New tableDepth(tab("name"), 1))
            End If
        Next

        Dim depth As tableDepth
        For i As Integer = 0 To orderedTables.Count - 1
            depth = DirectCast(orderedTables(i), tableDepth)
            Dim tab As DataRow = metads.Tables("sysobjects").Select(String.Format("name='{0}'", depth.tab.ToString()))(0)
            If metads.Tables("sysreferences").Select("rkeyid=" + tab("id").ToString()).Length > 0 Then
                ChamaMetodo(depth.tab, 2)
            End If
        Next

        orderedTables.Sort(New TableDepthSorter)
        'ImprimeListaTabelas()
        Return orderedTables
    End Function

    'metodo recursivo que calcula a profundidade de cada tabela e insere-as no array ar
    Private Sub ChamaMetodo(ByVal tabName As String, ByVal pr As Integer, Optional ByVal selfNode As Boolean = False)
        Dim tab As DataRow = metads.Tables("sysobjects").Select(String.Format("name='{0}'", tabName))(0)
        For Each rel As DataRow In metads.Tables("sysreferences").Select("rkeyid=" + tab("id").ToString())
            Dim refTab As DataRow = metads.Tables("sysobjects").Select("id=" + rel("fkeyid").ToString())(0)
            If metads.Tables("sysreferences").Select("rkeyid=" + refTab("id").ToString()).Length > 0 AndAlso Not refTab Is tab Then
                ChamaMetodo(refTab("name").ToString(), pr + 1)
            End If
            If Not refTab Is tab Then
                ListaTabela(refTab("name").ToString(), pr)
            End If
        Next
    End Sub

    'adiciona uma tabela a lista com a respectiva profundidade
    Private Sub ListaTabela(ByVal dt As String, ByVal pr As Integer)
        Dim r As tableDepth
        For Each i As Object In orderedTables
            r = DirectCast(i, tableDepth)
            If r.tab Is dt Then
                If r.dep < pr Then
                    orderedTables.Remove(i)
                    orderedTables.Add(New tableDepth(dt, pr))
                    Return
                End If
                Return
            End If
        Next
        orderedTables.Add(New tableDepth(dt, pr))
    End Sub

    Private Sub ImprimeListaTabelas()
        Dim r As tableDepth
        For Each i As Object In orderedTables
            r = DirectCast(i, tableDepth)
            Console.WriteLine(String.Format("{0}, {1}", r.tab, r.dep))
            'Console.WriteLine("""" + r.tab + """,")
        Next
    End Sub

    Public Class TableDepthSorter
        Implements IComparer

        Public Overloads Function Compare(ByVal obj1 As Object, ByVal obj2 As Object) As Integer _
            Implements IComparer.Compare

            If DirectCast(obj1, Form1.tableDepth).dep > DirectCast(obj2, Form1.tableDepth).dep Then
                Return 1
            ElseIf DirectCast(obj1, Form1.tableDepth).dep < DirectCast(obj2, Form1.tableDepth).dep Then
                Return -1
            End If

            Return 0
        End Function
    End Class
#End Region

End Class

