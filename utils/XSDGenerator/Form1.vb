Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

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
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerateXSD As System.Windows.Forms.Button
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents lblDatabase As System.Windows.Forms.Label
    Friend WithEvents txtSchemaName As System.Windows.Forms.TextBox
    Friend WithEvents lblSchemaName As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnGenerateXSD = New System.Windows.Forms.Button
        Me.lblServer = New System.Windows.Forms.Label
        Me.txtServer = New System.Windows.Forms.TextBox
        Me.txtDatabase = New System.Windows.Forms.TextBox
        Me.lblDatabase = New System.Windows.Forms.Label
        Me.txtSchemaName = New System.Windows.Forms.TextBox
        Me.lblSchemaName = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnGenerateXSD
        '
        Me.btnGenerateXSD.Location = New System.Drawing.Point(208, 56)
        Me.btnGenerateXSD.Name = "btnGenerateXSD"
        Me.btnGenerateXSD.TabIndex = 0
        Me.btnGenerateXSD.Text = "Get schema"
        '
        'lblServer
        '
        Me.lblServer.Location = New System.Drawing.Point(8, 8)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(80, 20)
        Me.lblServer.TabIndex = 1
        Me.lblServer.Text = "Server:"
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(84, 8)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.TabIndex = 2
        Me.txtServer.Text = "(local)\SQLEXPRESS"
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(84, 36)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.TabIndex = 4
        Me.txtDatabase.Text = ""
        '
        'lblDatabase
        '
        Me.lblDatabase.Location = New System.Drawing.Point(8, 36)
        Me.lblDatabase.Name = "lblDatabase"
        Me.lblDatabase.Size = New System.Drawing.Size(80, 20)
        Me.lblDatabase.TabIndex = 3
        Me.lblDatabase.Text = "Database:"
        '
        'txtSchemaName
        '
        Me.txtSchemaName.Location = New System.Drawing.Point(84, 64)
        Me.txtSchemaName.Name = "txtSchemaName"
        Me.txtSchemaName.TabIndex = 6
        Me.txtSchemaName.Text = ""
        '
        'lblSchemaName
        '
        Me.lblSchemaName.Location = New System.Drawing.Point(8, 64)
        Me.lblSchemaName.Name = "lblSchemaName"
        Me.lblSchemaName.Size = New System.Drawing.Size(80, 20)
        Me.lblSchemaName.TabIndex = 5
        Me.lblSchemaName.Text = "SchemaName:"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(288, 89)
        Me.Controls.Add(Me.txtSchemaName)
        Me.Controls.Add(Me.lblSchemaName)
        Me.Controls.Add(Me.txtDatabase)
        Me.Controls.Add(Me.lblDatabase)
        Me.Controls.Add(Me.txtServer)
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.btnGenerateXSD)
        Me.Name = "Form1"
        Me.Text = "XSDGenerator"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateXSD.Click
        Dim mSaveDialog As SaveFileDialog = New SaveFileDialog
        mSaveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        mSaveDialog.AddExtension = True
        mSaveDialog.DefaultExt = "xsd"
        mSaveDialog.Filter = "XML Schema definition(*.xsd)|*.xsd"
        mSaveDialog.OverwritePrompt = True
        mSaveDialog.ValidateNames = True
        mSaveDialog.FileName = txtDatabase.Text + ".xsd"
        If mSaveDialog.ShowDialog() = DialogResult.OK Then
            mSaveDialog.InitialDirectory = New System.IO.FileInfo(mSaveDialog.FileName).Directory.ToString()
            GenerateXSD(mSaveDialog.FileName)
        End If
    End Sub


    Private ReadOnly Property ServerName() As String
        Get
            Return txtServer.Text
        End Get
    End Property

    Private ReadOnly Property DatabaseName() As String
        Get
            Return txtDatabase.Text
        End Get
    End Property

    Private ReadOnly Property SchemaName() As String
        Get
            Return txtSchemaName.Text()
        End Get
    End Property

    Private Sub GenerateXSD(ByVal filename As String)
        Dim OldCursor As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Dim conn As New OleDb.OleDbConnection(GetConnectionString(ServerName))
        conn.Open()
        Dim ds As New DataSet
        ds.CaseSensitive = True
        Dim metads As New DataSet
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

        da.SelectCommand.CommandText = "USE " + DatabaseName + "; SELECT * FROM systypes;"
        da.Fill(metads, "systypes")

        ' Row que refere o tipo datetime2
        Dim dateTime2Type As DataRow = metads.Tables("systypes").Select("Name='datetime2'")(0)

        For Each dr As DataRow In metads.Tables("sysobjects").Rows
            da.SelectCommand.CommandText = String.Format("USE " + DatabaseName + "; SELECT * FROM [{0}]", dr("name"))
            da.FillSchema(ds, SchemaType.Source, dr("name").ToString())

            ' forçar que o mapeamento do datetime2 seja para um System.DateTime em vez de System.String
            Dim cols As DataRow() = metads.Tables("syscolumns").Select(String.Format("id={0} AND xtype={1}", dr("id").ToString(), dateTime2Type("xtype").ToString()))
            For Each col As DataRow In cols
                ds.Tables(dr("name").ToString()).Columns.Item(col("name").ToString()).DataType = System.Type.GetType("System.DateTime")
            Next

            Console.WriteLine("Including table '" + dr("name").ToString() + "'")
        Next

        Dim foreignColumns As New ArrayList
        Dim referencesColumns As New ArrayList
        For Each dr As DataRow In metads.Tables("sysreferences").Rows
            foreignColumns.Clear()
            referencesColumns.Clear()
            Dim foreignTableName As String = metads.Tables("sysobjects").Select("id=" + dr("fkeyid").ToString())(0)("name").ToString()
            Dim referencesTableName As String = metads.Tables("sysobjects").Select("id=" + dr("rkeyid").ToString())(0)("name").ToString()
            For i As Integer = 1 To 16
                If dr.IsNull("fkey" + i.ToString()) Or CInt(dr("fkey" + i.ToString())) <= 0 Then
                    Exit For
                End If
                Dim foreignquery As String = String.Format("id={0} AND colid={1}", dr("fkeyid").ToString(), dr("fkey" + i.ToString()).ToString())
                Dim referencesquery As String = String.Format("id={0} AND colid={1}", dr("rkeyid").ToString(), dr("rkey" + i.ToString()).ToString())
                Dim parentColumnName As String = metads.Tables("syscolumns").Select(foreignquery)(0)("name").ToString()
                Dim childColumnName As String = metads.Tables("syscolumns").Select(referencesquery)(0)("name").ToString()
                foreignColumns.Add(ds.Tables(foreignTableName).Columns(parentColumnName))
                referencesColumns.Add(ds.Tables(referencesTableName).Columns(childColumnName))
            Next
            Dim RelationName As String = FixRelationName(referencesTableName + foreignTableName, foreignColumns)
            ds.Relations.Add(RelationName, DirectCast(referencesColumns.ToArray(GetType(DataColumn)), DataColumn()), DirectCast(foreignColumns.ToArray(GetType(DataColumn)), DataColumn()))
        Next

        For Each dr As DataRow In metads.Tables("syscomments").Rows
            Dim metaColumn As DataRow
            Dim metaTable As DataRow
            Dim column As DataColumn

            metaColumn = metads.Tables("syscolumns").Select("cdefault = " + dr("id").ToString())(0)
            metaTable = metads.Tables("sysobjects").Select("id = " + metaColumn("id").ToString())(0)

            column = ds.Tables(metaTable("name")).Columns(metaColumn("name"))

            If column.ColumnName = "isDeleted" Then
                column.DefaultValue = 0
            Else
                'column.DataType()

                'Dim c As System.ComponentModel.TypeConverter
                'c = System.ComponentModel.TypeDescriptor.GetConverter(column.DataType)
                'column.DefaultValue = c.ConvertFrom(dr("text").ToString().TrimStart("("c).TrimEnd(")"c))
                Dim txt As String = dr("text").ToString()
                If txt.StartsWith("('") Then
                    txt = txt.Substring(2)
                Else
                    txt = txt.Substring(1)
                End If
                If txt.EndsWith("')") Then
                    txt = txt.Substring(0, txt.Length - 2)
                Else
                    txt = txt.Substring(0, txt.Length - 1)
                End If
                If column.DataType Is GetType(Boolean) Or column.DataType Is GetType(Byte) Or column.DataType Is GetType(Int64) Then
                    column.DefaultValue = TranslateBoolean(Convert.ChangeType(txt.Replace("(", "").Replace(")", ""), GetType(Integer)))
                ElseIf column.DataType Is GetType(DateTime) And txt.EndsWith("()") And txt.Length > 2 Then
                    column.DefaultValue = Convert.ChangeType(txt, column.DataType)
                Else
                    column.DefaultValue = Convert.ChangeType(txt, column.DataType)
                End If
            End If
        Next

        ' CLIENTE SERVIDOR 1
        ''ds.Tables("Dicionario").Columns("Termo").Unique = True
        'ds.Tables("TipoMedida").Columns("Designacao").Unique = True
        'ds.Tables("TipoQuantidade").Columns("Designacao").Unique = True
        'ds.Tables("AutoEliminacao").Columns("Designacao").Unique = True
        ''ds.Tables("Trustee").Columns("Name").Unique = True

        'CLIENTE SERVIDOR 2
        ds.Tables("TipoMaterial").Columns("Designacao").Unique = True
        ds.Tables("TipoSuporte").Columns("Designacao").Unique = True
        ds.Tables("TipoEstadoConservacao").Columns("Designacao").Unique = True
        ds.Tables("TipoTecnicaRegisto").Columns("Designacao").Unique = True
        ds.Tables("TipoMedida").Columns("Designacao").Unique = True
        ds.Tables("TipoAcondicionamento").Columns("Designacao").Unique = True
        ds.Tables("AutoEliminacao").Columns("Designacao").Unique = True
        'ds.Tables("Trustee").Columns("Name").Unique = True

        Dim FrdConstraint As New System.Data.UniqueConstraint( _
            ds.Tables("FRDBase").TableName + "_Constraint" + ds.Tables("FRDBase").Constraints.Count.ToString(), _
            New DataColumn() { _
                ds.Tables("FRDBase").Columns("IDNivel"), _
                ds.Tables("FRDBase").Columns("IDTipoFRDBase")}, _
            False)
        ds.Tables("FRDBase").Constraints.Add(FrdConstraint)

        'ds.Tables("Dicionario").CaseSensitive = True
        Dim DicConstraint As New System.Data.UniqueConstraint( _
            ds.Tables("Dicionario").TableName + "_Constraint" + ds.Tables("Dicionario").Constraints.Count.ToString(), _
            New DataColumn() { _
                ds.Tables("Dicionario").Columns("Termo"), _
                ds.Tables("Dicionario").Columns("CatCode")}, _
            False)
        ds.Tables("Dicionario").Constraints.Add(DicConstraint)

        ds.DataSetName = SchemaName
        ds.WriteXmlSchema(filename)

        conn.Close()

        Me.Cursor = OldCursor
    End Sub
    
    Private Function TranslateBoolean (ByVal val As Integer) As Boolean 
    	If val = 1 Then
    		Return true
		Else
    		return false
    	End If
    End Function

    Private Function GetConnectionString(ByVal ServerName As String) As String
        Return "Provider=SQLOLEDB;Data Source=" + ServerName + ";Initial Catalog=master;Persist Security Info=True;Integrated Security=SSPI;"
    End Function

    Private Function FixRelationName(ByVal RelationName As String, ByVal childColumns As ArrayList) As String
        If DirectCast(childColumns.Item(0), DataColumn).ColumnName.EndsWith("Alias") Then
            RelationName = RelationName + "Alias"
        ElseIf DirectCast(childColumns.Item(0), DataColumn).ColumnName.EndsWith("Authority") Then
            RelationName = RelationName + "Authority"
        ElseIf DirectCast(childColumns.Item(0), DataColumn).ColumnName.EndsWith("Upper") Then
            RelationName = RelationName + "Upper"
        End If
        Return RelationName
    End Function

    Private Sub txt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServer.TextChanged, txtDatabase.TextChanged, txtSchemaName.TextChanged
        UpdateButtonstate()
    End Sub

    Private Sub UpdateButtonstate()
        If txtDatabase.Text.Trim().Length > 0 AndAlso _
            txtDatabase.Text.Trim().Length > 0 AndAlso _
            txtSchemaName.Text.Trim().Length > 0 Then

            btnGenerateXSD.Enabled = True
        Else
            btnGenerateXSD.Enabled = False
        End If
    End Sub
End Class
