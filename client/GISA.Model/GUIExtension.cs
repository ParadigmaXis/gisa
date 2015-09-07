//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;



//#If DEBUG Then
//Imports NUnit.Framework
//#End If
//Imports System.IO
//Imports System.Reflection


//Public Interface IGUIExtension
//    Sub Install(ByVal Form1 As System.Windows.Forms.Form)
//    Sub Uninstall(ByVal Form1 As System.Windows.Forms.Form)
//End Interface

//Public Interface IBasePanel
//	Sub Install()
//	Function CanUninstall() As Boolean
//End Interface

//Public Class ConfiguredExtensionLoader
//	Dim Assemblies As System.Collections.Hashtable
//    Private Function EnsureLoaded(ByVal AssemblyName As String) As [Assembly]
//    	Dim Asm As [Assembly]
//    	For Each Asm in AppDomain.CurrentDomain.GetAssemblies()
//            Trace.WriteLine(Asm.GetName().Name)
//    		If Asm.FullName.Equals(AssemblyName) Then
//    			Return Asm
//    		End If
//    	Next
//    	Return AppDomain.CurrentDomain.Load(AssemblyName)
//    End Function
//    Private Function CheckGuiExtension(ByVal Asm As [Assembly], ByVal ClassName As String) As [Type]
//    	Dim ExtensionClassName As String = GetType(GISA.Model.IGUIExtension).Name
//    	Dim T As [Type] = Asm.GetType(ClassName)
//    	If Not T.GetInterface(ExtensionClassName) Is Nothing Then
//    		Return T
//    	End If
//    	Throw New ArgumentException("", "ClassName")
//    End Function
//    Private Function CreateIGUIExtension(ByVal T As [Type]) As IGUIExtension
//    	Return Nothing 'CType(asm.CreateInstance(t.FullName), GISA.Model.IGUIExtension)
//    End Function
//    Private CurrentExtension As IBasePanel
//    Private Sub OnFunction()
//    	If Not CurrentExtension Is Nothing Then
//    		If Not CurrentExtension.CanUninstall() Then
//    			' FIXME Select CurrentExtension in OutlookBar
//    			Exit Sub
//    		End If
//        End If
//        ' CurrentExtension = CType(Me.Tag.CreateInstance(), IBasePanel)
//        ' WorkPanel.Controls.Add(CurrentExtension)
//        ' CurrentExtension.Dock = DockStyle.Fill
//        ' CurrentExtension.Install()
//    End Sub
//    Public Sub Install()
//        With CType(System.Threading.Thread.CurrentPrincipal, GisaPrincipal).TrusteeRow
//            Dim tp As GisaDataSet.TrusteePrivilegeRow
//            For Each tp In .GetTrusteePrivilegeRows()
//                Dim Asm As [Assembly] = EnsureLoaded(tp.FunctionOperationRowParent.TipoFunctionRowParent.ModuleName)
//                ' FIXME Form.OutlookBar.Categories.Add(tp.FunctionOperationRowParent.TipoFunctionRowParent.TipoFunctionGroupRowParent.Name)
//                Try
//					Dim T As [Type] = CheckGuiExtension(Asm, tp.FunctionOperationRowParent.TipoFunctionRowParent.ClassName)
//					' FIXME Form.OutlookBar.Categories.Item(tp.FunctionOperationRowParent.TipoFunctionRowParent.TipoFunctionGroupRowParent.Name).Items.Add(tp.FunctionOperationRowParent.TipoFunctionRowParent.Name)
//					' FIXME Also assign Icon:
//					' FIXME Form.OutlookBar.ImageList.Add(FindImageInResources(Asm, ImageName))
//					With CreateIGUIExtension(Type.GetType(tp.FunctionOperationRowParent.TipoFunctionRowParent.ClassName))
//						'.Install(tp.FunctionOperationRowParent.TipoOperationRow.CodeName)
//					End With
//                    Trace.WriteLine(tp.FunctionOperationRowParent.TipoFunctionRowParent.ModuleName + _
//                     "(" + tp.FunctionOperationRowParent.TipoFunctionRowParent.ClassName + ")" + _
//                     tp.FunctionOperationRowParent.TipoOperationRow.CodeName)
//                Catch ex As ArgumentException
//                    Trace.WriteLine("Ignored an assigned privilege: " + _
//                     tp.FunctionOperationRowParent.TipoFunctionRowParent.ModuleName + _
//                     "(" + tp.FunctionOperationRowParent.TipoFunctionRowParent.ClassName + ")" + _
//                     tp.FunctionOperationRowParent.TipoOperationRow.CodeName)
//                Catch ex As TargetInvocationException
//                    Trace.WriteLine("Failed privilege: " + _
//                     tp.FunctionOperationRowParent.TipoFunctionRowParent.ModuleName + _
//                     "(" + tp.FunctionOperationRowParent.TipoFunctionRowParent.ClassName + ")" + _
//                     tp.FunctionOperationRowParent.TipoOperationRow.CodeName)
//                End Try
//            Next
//        End With
//    End Sub
//End Class

//#If DEBUG Then
//<TestFixture()> Public Class TestConfiguredExtensionLoader
//    <SetUp()> Public Sub SetUp()
//        Dim tfg As GisaDataSet.TipoFunctionGroupRow
//        Dim tf as GisaDataSet.TipoFunctionRow
//        Dim top As GisaDataSet.TipoOperationRow
//        Dim top2 As GisaDataSet.TipoOperationRow
//        Dim fo As GisaDataSet.FunctionOperationRow

//        Dim t As GisaDataSet.TrusteeRow
//        Dim tu As GISADataset.TrusteeUserRow

//        Dim Versao() As Byte

//        With GisaDataSetHelper.GetInstance()
//            tfg = .TipoFunctionGroup.AddTipoFunctionGroupRow(1, "FunctionGroup", 1, Versao, False)
//            tf = .TipoFunction.AddTipoFunctionRow(tfg, 1, "Function", "library.dll", "GISA.Recolha.Extension", 1, Nothing, Nothing, Versao, False)
//            top = .TipoOperation.AddTipoOperationRow(1, "Operation 1", "Operation1", Versao, False)
//            top2 = .TipoOperation.AddTipoOperationRow(2, "Operation 2", "Operation2", Versao, False)
//            fo = .FunctionOperation.AddFunctionOperationRow(1, 1, top, Versao, False)
//            fo = .FunctionOperation.AddFunctionOperationRow(1, 1, top2, Versao, False)

//            t = .Trustee.AddTrusteeRow("user", "blabla", "USR", False, True, Versao, False)
//            tu = .TrusteeUser.AddTrusteeUserRow(t, "Password", "Full Name", False, Nothing, Versao, False)

//            .TrusteePrivilege.AddTrusteePrivilegeRow(t, 1, 1, 1, True, Versao, False)
//            .TrusteePrivilege.AddTrusteePrivilegeRow(t, 1, 1, 2, True, Versao, False)

//            'Trustee.RegisterPrincipal(GisaDataSetHelper.GetInstance(), DirectCast(.TipoProduct.Select("BuiltInName LIKE 'GISABASE'")(0), GISADataset.TipoProductRow), tu)
//        End With
//    End Sub
//    <Test()> Public Sub ListPermissions()
//        With New ConfiguredExtensionLoader
//            .Install()
//        End With
//    End Sub
//End Class
//#End If

//Public Class ExtensionLoader
//    Private _Extensions() As IGUIExtension
//    Private _Form As System.Windows.Forms.Form

//    Public Sub New(ByVal Form As System.Windows.Forms.Form)
//        _Form = Form
//    End Sub
//    Public Sub Install()
//        Dim ExtensionClassName As String = GetType(GISA.Model.IGUIExtension).Name
//        Debug.WriteLine("GUIExtension.Install() started...")
//        Try
//            Dim asm As [Assembly]
//            Dim t As Type
//            Dim f As FileInfo
//            ' Enumerate assemblies in caller's assembly directory
//            For Each f In New FileInfo([Assembly].GetCallingAssembly().GetName().FullName).Directory.GetFiles()
//                If f.Extension.ToLower().Equals(".dll") Then
//                    ' Load dll and look up inside for a class that implements the GUIExtension interface
//                    asm = [Assembly].Load(f.Name.Substring(0, f.Name.Length() - 4))
//                    Debug.WriteLine("Scanning library " + f.Name + " for extensions of " + ExtensionClassName + ".")
//                    For Each t In asm.GetTypes()
//                        If Not t.GetInterface(ExtensionClassName) Is Nothing Then
//                            Try
//                                Dim GUIExtension As GISA.Model.IGUIExtension
//                                GUIExtension = Nothing
//                                Try
//                                    ' Try to create an instance.
//                                    GUIExtension = CType(asm.CreateInstance(t.FullName), GISA.Model.IGUIExtension)
//                                    Debug.WriteLine("Creation of " + t.FullName + " As " + ExtensionClassName + " succeeded.")
//                                Catch ex As TargetInvocationException
//                                    ' The constructor threw an exception: throw it here as well.
//                                    Debug.WriteLine("Creation of " + t.FullName + " As " + ExtensionClassName + " failed.")
//                                    Throw ex.InnerException
//                                End Try
//                                If GUIExtension Is Nothing Then Throw New NullReferenceException(t.FullName)
//                                ' Give the extension a chance to add controls to the Form.
//                                GUIExtension.Install(_Form)
//                                ' Include the extension in the list of active extensions.
//                                If _Extensions Is Nothing Then
//                                    _Extensions = CType(Array.CreateInstance(GetType(GISA.Model.IGUIExtension), 1), GISA.Model.IGUIExtension())
//                                Else
//                                    ReDim Preserve _Extensions(_Extensions.Length())
//                                End If
//                                _Extensions(_Extensions.GetUpperBound(0)) = GUIExtension
//                                Debug.WriteLine("Installation of " + t.FullName + " As " + ExtensionClassName + " succeeded.")
//                            Catch ex As System.Security.SecurityException
//                                Debug.WriteLine("Installation of " + t.FullName + " As " + ExtensionClassName + " failed.")
//                                ' Ignore the security exception.
//                                ' This means that the Extension requires a role that is not present.
//                                ' As such, the main form should not install the Extension.
//                            End Try
//                        End If
//                    Next
//                End If
//            Next
//        Catch ex As Exception
//            System.Windows.Forms.MessageBox.Show(ex.Message, "GISA Extension Loader")
//        End Try
//        Debug.WriteLine("GUIExtension.Install() complete.")
//    End Sub

//    Public Sub Uninstall()
//        If Not _Extensions Is Nothing Then
//            Dim i As Integer
//            For i = _Extensions.GetLowerBound(0) To _Extensions.GetUpperBound(0)
//                ' Give the extension a chance to remove Controls from the Form.
//                _Extensions(i).Uninstall(_Form)
//            Next
//            _Extensions = Nothing
//        End If
//    End Sub
//End Class
