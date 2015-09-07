#######																								  #######
###																										  ###
### IN SILENT MODE ONLY THE GISA CLIENT IS INSTALLED (THE .NET FRAMEWORK 3.5 HAS TO BE ALREADY INSTALLED) ###
###																										  ###
######																								  #######

; Definitions
!define PRODUCT_NAME "Gisa"
!define PRODUCT_VERSION "2.0"
!define PRODUCT_PUBLISHER "ParadigmaXis, S.A."
!define PRODUCT_WEB_SITE "http://gisa.paradigmaxis.pt"
!define PRODUCT_REGKEY "Software\ParadigmaXis\GISA"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\GISA.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define DBFILENAME "GISA.mdf"
!define DBLOGFILENAME "GISA_log.ldf"
!define DBINSTANCENAME "GISA"

; Get GISA.exe Version
!system "GetExeVersion.exe"
!include "Version.txt"

; MUI 1.67 compatible ------
!include "MUI2.nsh"

; Windows Service Lib
!include "..\Lib\ServiceLib.nsh"

; Logic Lib adds some more familiar flow control and logic to NSI Scripts. Things like if, else, while loops, for loops and similar. It is also known as the NSIS Logic Library.
!include "LogicLib.nsh"

; Timer functions
!include "Time.nsi"

; x64 functions
!include "..\Lib\x64.nsh"

; Registry functions
!include "..\Lib\Registry.nsh"

; Custom plugins dir
!addplugindir "..\Plugins"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "..\Resources\GISA.ico"
!define MUI_UNICON "..\Resources\GISA.ico"

InstType "Monoposto"
InstType "Cliente"
InstType "Servidor"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!define MUI_LICENSEPAGE_RADIOBUTTONS
!insertmacro MUI_PAGE_LICENSE "..\Resources\License.rtf"
; Components page
!define MUI_PAGE_CUSTOMFUNCTION_PRE VerifyInstalledComponents
!insertmacro MUI_PAGE_COMPONENTS
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!define MUI_PAGE_CUSTOMFUNCTION_PRE VerifyRequirements
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!insertmacro MUI_PAGE_FINISH

; Uninstaller component page
!define MUI_PAGE_CUSTOMFUNCTION_PRE un.AvailableComponents
!insertmacro MUI_UNPAGE_COMPONENTS
; Uninstaller page
!insertmacro MUI_UNPAGE_INSTFILES
; Uninstaller finish page
!insertmacro MUI_UNPAGE_FINISH

; Language files
!insertmacro MUI_LANGUAGE "Portuguese"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "..\Bin\Setup.exe"
InstallDir "$PROGRAMFILES\ParadigmaXis\Gisa"
!define INSTALL_DIR_GISA "$PROGRAMFILES\ParadigmaXis\Gisa"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show
BrandingText "Powered by ${PRODUCT_PUBLISHER}"

; Init function
Function .onInit
 ; Make shure that there is only one Gisa Installer instance running
 System::Call 'kernel32::CreateMutexA(i 0, i 0, t "myMutex") i .r1 ?e'
  Pop $R0

 StrCmp $R0 0 +3
 MessageBox MB_OK|MB_ICONEXCLAMATION "Já existe outra instância da instalação do Gisa a correr."
  Abort
  
 ; call userInfo plugin to get user info.  The plugin puts the result in the stack
 UserInfo::getAccountType
 # pop the result from the stack into $0
 Pop $0
 # compare the result with the string "Admin" to see if the user is admin.
 # If match, jump 3 lines down.
 strCmp $0 "Admin" +3
  MessageBox MB_OK "O utilizador usado não é administrador.$\r$\nA instalação será abortada."
  Abort

 ; The computer name
 Var /GLOBAL SQLSERVER
 ReadRegStr $R2 HKLM "SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName" "ComputerName"
 StrCpy $SQLSERVER "$R2\${DBINSTANCENAME}"
 
 ; Set SQLPath variable
 ${If} ${RunningX64}
  SetRegView 64
  ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
  IFErrors 0 +7
   SetRegView 32
   ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
   SetRegView 64
 ${Else}
  SetRegView 32
  ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
 ${EndIf}
 
 Var /GLOBAL SQLPATH
 StrCpy $SQLPATH "$R1"
 
 ; in silent mode, if Gisa Client is already installed and we are trying to install it again "\Cliente" is added to the path
 ; this is to prevent this bug 
 StrCmp ${INSTALL_DIR_GISA} $INSTDIR +2 0
  StrCpy $INSTDIR ${INSTALL_DIR_GISA}
 
 ; in silent mode the client should only be installed after the old version (if exist) be uninstalled 
 IfSilent 0 done
  ReadRegStr $0 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Client"  
  StrCmp $0 "1" 0 done
   ; o processo de (des)instalacao so prossegue se a versao nova e a instalada forem diferentes
   Call CheckIfDiffVersion
  
  
   ; corre o uninstaller em modo silent mas nao espera que o processo termine. 
   ; NOTA: Na versao que espera que o processo termine (nsexec::ExecToStack '"$INSTDIR\uninst.exe" /S _?=$INSTDIR') o processo, na realidade, nunca termina
   ;       e nao foi descoberto como forçar isso. Para contornar optou-se por criar um ciclo infinito que espera ate a pasta cliente ser apagada. Ate la
   ;       vao sendo feitos sleeps de 2s
   nsexec::ExecToStack "$INSTDIR\uninst.exe /S"
   ; nsexec::ExecToStack '"$INSTDIR\uninst.exe" /S _?=$INSTDIR'
   ${While} 1 = 1
    ${If} ${FileExists} "${INSTALL_DIR_GISA}\Cliente"
	 sleep 2000
	${Else}
	 ${Break}	 
	${EndIf}
  ${EndWhile}
 done:
  ClearErrors
FunctionEnd

Function CheckIfDiffVersion
 MoreInfo::GetProductVersion "$INSTDIR\Cliente\GISA.exe"
 Pop $0 
 
 StrCmp $0 ${Version} 0 continue 
 Abort
 
 continue:
FunctionEnd

; ** Requirements **

Function VerifyRequirements
 GetCurInstType $0
 Call VerifyDotNet
 ${if} $0 == 0
  Call VerifySqlServer
 ${EndIf}
FunctionEnd

Function VerifyDotNet
   
 GetVersion::WindowsName
  Pop $R0

 ${if} $R0 == XP
 ${OrIf} $R0 == Vista
  DetailPrint "Verificando .NET 3.5 Service Pack 1..."
  ReadRegDWORD $R1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5" "SP"

  ${if} $R1 <> "1"
  ${AndIf} $R1 <> "2"
   # `/SD IDYES' tells MessageBox to automatically choose IDNO if the installer is silent
   MessageBox MB_YESNO|MB_ICONQUESTION 'O Service Pack 1 do .NET 3.5 não foi encontrada neste computador. $\r$\nDeseja prosseguir com a sua instalação?' /SD IDNO IDYES yes IDNO no 
   yes:
    Call InstallDotNet
    Return
   no:
    MessageBox MB_OK "A instalação do Service Pack 1 da framework .NET 3.5 foi cancelada. A instalação do Gisa será abortada!"
    Abort
  ${EndIf}
 ${EndIf}
FunctionEnd

Function InstallDotNet
 nsexec::ExecToStack '"$EXEDIR/Microsoft .NET Framework/dotnetfx35.exe"'
 Pop $1

 StrCmp $1 "0" Flawless Damn
 Flawless:
  Return
 Damn:
  MessageBox MB_OK "A instalação da framework .NET 3.5 SP1 falhou. A instalação do Gisa será abortada!"
  Abort
FunctionEnd

Function VerifySqlServer
 ClearErrors
 
 ReadRegStr $R0 HKLM "Software\Microsoft\Microsoft SQL Server\100" "VerSpecificRootDir"
 IfErrors 0 validate_instance
  MessageBox MB_YESNO|MB_ICONQUESTION 'Não foi encontrado o MS SQL Server 2008 neste computador. $\r$\nDeseja prosseguir com a sua instalação (ver. 32bit)?' IDYES yes IDNO no
  yes:
    Call InstallSQLServer
    Goto validate_instance
   no:
    MessageBox MB_OK "A instalação do MS SQL Server 2008 foi cancelada. $\r$\nPode efectuar a instalação manualmente usando $\r$\no executável na pasta Microsoft SQL Server.$\r$\nSiga as instruções de instalação disponiveis no $\r$\nficheiro README.txt guardado no CD de instalação. $\r$\nA instalação do Gisa será abortada!"
    Abort
 
 validate_instance:
  GetTempFileName $R0
  nsexec::ExecToStack '"$SQLPATH\Binn\osql.exe" -E -S "$SQLSERVER" -Q "SELECT @@version" -o "$R0" -b'
  Pop $1
  
  StrCmp $1 "0" success error
  success:
   ; Verify if it is SQL Server 2008
   Push 11 ;line number to read from
   Push "$R0" ;text file to read
   Call ReadFileLine
   Pop $0 ;output string 
  
   StrCpy $0 $0 25 1
   StrCmp $0 "Microsoft SQL Server 2008" 0 error
  
   Return
  error:
   MessageBox MB_OK "Não foi encontrado o MS SQL Server 2008 com a instância GISA. $\r$\nA instalação do Gisa será abortada!$\r$\n$\r$\nNOTA: Pode efectuar a instalação manualmente usando o executável na pasta Microsoft SQL Server.$\r$\nSiga as instruções de instalação disponiveis no $\r$\nficheiro README.txt guardado no CD de instalação A instalação do Gisa será abortada!"
   Abort
  
FunctionEnd

Function InstallSQLServer
 Push $R1
 Call RestartRequired
 Exch $R1
 StrCmp $R1 "1" RestartRequired RestartNotRequired
 
 RestartRequired:
  MessageBox MB_OK 'O Windows precisa de ser reiniciado porque ainda tem um $\r$\nprocesso de instalação pendente. A instalação será abortada.'
  Abort
 
 RestartNotRequired:
  Call VerifySQLServerPreRequisites
 
  UserMgr::GetCurrentUserName
  Pop $0
 
  UserMgr::GetSIDFromUserName "" "$0"
  Pop $0

  UserMgr::GetUserNameFromSID "$0"
  Pop $0

  nsexec::ExecToStack '"$EXEDIR/Microsoft SQL Server/SQLEXPRWT_x86_ENU.exe" /ACTION=INSTALL /INSTALLSHAREDDIR="$PROGRAMFILES\Microsoft SQL Server" /INSTANCEDIR="$PROGRAMFILES\Microsoft SQL Server" /SQLSYSADMINACCOUNTS="$0" /ConfigurationFile="$EXEDIR/Microsoft SQL Server/ConfigurationFile.ini"'
  Pop $1
 
  StrCmp $1 "0" Flawless Damn
  Flawless:
   ; update $SQLPATH variables
   ${If} ${RunningX64}
    ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
    IfErrors 0 +7
     SetRegView 32
     ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
     SetRegView 64
   ${Else}
    ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
   ${EndIf}
   StrCpy $SQLPATH "$R1"
   
   ReadRegStr $R2 HKLM "SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName" "ComputerName"
   StrCpy $R2 "$R2\${DBINSTANCENAME}"
   StrCpy $SQLSERVER "$R2"
   
   Return
  Damn:
   MessageBox MB_OK "A instalação do MS SQL Server 2008 falhou. A instalação do Gisa será abortada!"
   Abort
FunctionEnd

Function RestartRequired
	Exch $R1         ;Original Variable
	Push $R2
	Push $R3         ;Counter Variable
 
	StrCpy $R1 "0" 1     ;initialize variable with 0
	StrCpy $R3 "0" 0    ;Counter Variable
	
	${registry::Read} "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager" "PendingFileRenameOperations" $R2 $R3
	StrCmp $R2 "" 0 FoundRestart
	
	;First Check Current User RunOnce Key
	EnumRegValue $R2 HKCU "Software\Microsoft\Windows\CurrentVersion\RunOnce" $R3
	StrCmp $R2 "" 0 FoundRestart
 
	;Next Check Local Machine Key
	EnumRegValue $R2 HKLM "Software\Microsoft\Windows\CurrentVersion\RunOnce" $R3
	StrCmp $R2 "" ExitFunc 0
 
	FoundRestart:
		StrCpy $R1 "1" 1
 
	ExitFunc:
		Pop $R3
		Pop $R2
		Exch $R1
FunctionEnd

Function VerifySQLServerPreRequisites
 DetailPrint "Verificando se a versão do Windows Installer é maior ou igual a 4.5..."
 Call CheckWindowsInstaller
 Pop $0 
 StrCpy $R0 $0
 
 DetailPrint "Verificando PowerShell..."
 Call IsPowerShellInstalled
 Pop $0
 StrCpy $R1 "$0"
 
 StrCmp $R0$R1 "" _prerequisitesIntalled _prerequisitesNotIntalled
  _prerequisitesNotIntalled:
   MessageBox MB_YESNO|MB_ICONQUESTION 'Para prosseguir com a instalação do SQL Server 2008 $\r$\né necessário instalar o(s) seguinte(s) pacote(s). $\r$\n$\r$\n$R0$\r$\n$R1$\r$\n$\r$\nDeseja prosseguir a instalação desses pacotes?' IDYES yes IDNO no
   yes:
    StrCmp $R1 "" +2 0
     Call InstallPowerShell
	StrCmp $R0 "" +2 0
	 Call InstallWindowsInstaller
	 
    Goto _prerequisitesIntalled
   no:
    MessageBox MB_OK "A instalação do MS SQL Server 2008 foi cancelada. $\r$\nPode efectuar a instalação manualmente usando o executável na pasta Microsoft SQL Server.$\r$\nSiga as instruções de instalação disponiveis no $\r$\nficheiro README.txt guardado no CD de instalação A instalação do Gisa será abortada!"
    Abort

 _prerequisitesIntalled:
  Return
FunctionEnd

Function CheckWindowsInstaller
 GetDllVersion $SYSDIR\msi.dll $R0 $R1
 IntOp $R2 $R0 >> 16
 IntOp $R2 $R2 & 0x0000FFFF ; $R2 now contains major version
 IntOp $R3 $R0 & 0x0000FFFF ; $R3 now contains minor version
 StrCpy $0 "$R2$R3"
 IntOp $0 $0 + 0
 ${If} $0 < 45
  Push "Windows Installer 4.5"
 ${Else}
  Push ""
 ${EndIf}
 Return
FunctionEnd

Function IsPowerShellInstalled
  Push $0
  ReadRegDWORD $0 HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\PowerShell\1" "Install"
  StrCmp $0 "" psNotInstalled psInstalled
  psNotInstalled:
    Push "PowerShell 1.0"
    Goto psCheckDone
  psInstalled:
    Push ""
  psCheckDone:
    Return
FunctionEnd

Function InstallPowerShell
 GetVersion::WindowsVersion
 Pop $0
 
 DetailPrint "Instalando PowerShell 1.0"
 
 ${If} $0 == 5.2 
   nsexec::ExecToStack '"$EXEDIR/Microsoft SQL Server/PreRequisites/WindowsServer2003.WindowsXP-KB926139-v2-x64-ENU.exe"'
   Pop $1
 ${ElseIf} $0 == 5.1
   nsexec::ExecToStack '"$EXEDIR/Microsoft SQL Server/PreRequisites/WindowsXP-KB926139-v2-x86-ENU.exe"'
   Pop $1
 ${ElseIf} $0 == 6.0
 ${AndIf} ${RunningX64}
   nsexec::ExecToStack 'wusa.exe "$EXEDIR/Microsoft SQL Server/PreRequisites/Windows6.0-KB928439-x64.msu"'
   Pop $1
 ${ElseIf} $0 == 6.0
 ${AndIfNot} ${RunningX64}
   nsexec::ExecToStack 'wusa.exe "$EXEDIR/Microsoft SQL Server/PreRequisites/Windows6.0-KB928439-x86.msu"'
   Pop $1
 ${Else}
  MessageBox MB_OK "A instalação do PowerShell 1.0 falhou $\r$\npara a versão $0 do Windows. $\r$\nA instalação do Gisa será abortada!"
  Abort
 ${EndIf}
 
 StrCmp $1 "0" Flawless Damn
 Flawless:
  Return
 Damn:
  MessageBox MB_OK "A instalação do PowerShell 1.0 falhou. A instalação do Gisa será abortada!"
  Abort
  
FunctionEnd

Function InstallWindowsInstaller
 GetVersion::WindowsVersion
 Pop $0
 
 DetailPrint "Instalando Windows Installer 4.5"
 
 ${If} $0 == 5.1
  nsexec::ExecToStack '"$EXEDIR/Microsoft SQL Server/PreRequisites/WindowsXP-KB942288-v3-x86.exe"'
  Pop $1
 ${ElseIf} $0 == 5.2
  nsexec::ExecToStack '"$EXEDIR/Microsoft SQL Server/PreRequisites/WindowsServer2003-KB942288-v4-x64.exe"'
  Pop $1
 ${ElseIf} $0 == 6.0
 ${AndIf} ${RunningX64}
   nsexec::ExecToStack 'wusa.exe "$EXEDIR/Microsoft SQL Server/PreRequisites/Windows6.0-KB942288-v2-x64.msu"'
   Pop $1
 ${ElseIf} $0 == 6.0
 ${AndIfNot} ${RunningX64}
   nsexec::ExecToStack 'wusa.exe "$EXEDIR/Microsoft SQL Server/PreRequisites/Windows6.0-KB942288-v2-x86.msu"'
   Pop $1
 ${Else}
  MessageBox MB_OK "A instalação do Windows Installer 4.5 falhou $\r$\npara a versão $0 do Windows. $\r$\nA instalação do Gisa será abortada!"
  Abort 
 ${EndIf}
 
 ; ERROR_SUCCESS_REBOOT_REQUIRED	3010	A restart is required to complete the install. This message is indicative of a success. This does not include installs where the ForceReboot action is run.
 StrCmp $1 "0" Flawless _Reboot
 _Reboot:
  StrCmp $1 "3010" ForceReboot Damn 
 Damn:
  MessageBox MB_OK "A instalação do Windows Installer 4.5 falhou. A instalação do Gisa será abortada!"
  Abort
 ForceReboot:
  MessageBox MB_YESNO|MB_ICONQUESTION 'Para terminar instalação do Windows Installer 4.5 $\r$\ne prosseguir com a instalação do SQL Server 2008 $\r$\no Windows precisa de ser reiniciado. $\r$\nPretende reiniciar o Windows agora?' IDYES yes IDNO no
  yes:
   Reboot
   Goto Flawless
  no:
   MessageBox MB_OK "A instalação do Windows Installer 4.5 foi interrompida. A instalação do Gisa será abortada!"
   Abort
 Flawless:
  Return
FunctionEnd

; **Client**

Section "Cliente" Client
 SectionIn 1 2
  
 ; Output directory
 SetOutPath "$INSTDIR\Cliente"
 
 ; Get client files
 SetOverwrite try
 File "..\..\client\GISA\bin\Release\*.dll"
 File "..\..\client\GISA\bin\Release\*.exe"
 File "..\..\client\ExternalDependencies\ImportFromExcel\*.dll"
 SetOverwrite ifnewer
 File "..\Resources\readme.txt"

 ; Set variable
 WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\Cliente\GISA.exe"
 WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Client" "1"
 
SectionEnd

; **Server**

Function CreateDB
 
 ; Create database
 StrCpy $0 "CREATE DATABASE [GISA] ON  PRIMARY ( NAME = N'GISA', FILENAME = N'$INSTDIR\Servidor\${DBFILENAME}' , SIZE = 3328KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ) LOG ON ( NAME = N'GISA_log', FILENAME = N'$INSTDIR\Servidor\${DBLOGFILENAME}' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%) COLLATE SQL_Latin1_General_CP850_CS_AS"
 nsexec::ExecToStack '"$SQLPATH\Binn\osql.exe" -E -S "$SQLSERVER" -d "master" -Q "$0" -b'
  Pop $1

 StrCmp $1 "0" 0 error
 
 ; Add database artifacts
 GetTempFileName $R0
 nsexec::ExecToStack '"$SQLPATH\Binn\osql.exe" -E -S "$SQLSERVER" -i "DBInstallScript.sql" -o "$R0" -b'
 Pop $1

 Delete "$INSTDIR\Servidor\DBInstallScript.sql"

 GetTempFileName $R0
 nsexec::ExecToStack '"$SQLPATH\Binn\osql.exe" -E -S "$SQLSERVER" -d "GISA" -i "CreateNTAuthoritySystemLogin.sql" -o "$R0" -b'
 Pop $1

 Delete "$INSTDIR\Servidor\CreateNTAuthoritySystemLogin.sql"

 Return

 error:
  MessageBox MB_OK "Os ficheiros da base de dados não foram criados!"
  Return

FunctionEnd

Section "Servidor" Server
 SectionIn 1 3
 
 ; If in silent mode Server must not be installed
 IfSilent 0 +2
  Return

 ; Output directory
 SetOutPath "$INSTDIR\Servidor"
 AccessControl::GrantOnFile "$INSTDIR\Servidor" "(BU)" "FullAccess"
 
 ; Get server files
 SetOverwrite try
 File "..\..\server\DatabaseScripts\DatabaseInstallScript\DBInstallScript.sql"
 File "..\..\server\DatabaseScripts\Monoposto\CreateNTAuthoritySystemLogin.sql"
 File "..\..\server\DatabaseScripts\Outros Scripts\SQLServer\drop_connections.sql"
 File "..\..\server\GISAServer\GISAServer.Service\bin\Release\*.dll"
 File "..\..\server\GISAServer\GISAServer.Service\bin\Release\*.exe"
 File "..\..\server\GISAServer\GISAServer.Service\bin\Release\*.config"
 SetOverwrite ifnewer
 File "..\..\server\GISAServer\Conf\hibernate.cfg.xml"
 
 ; Create database if it is a monoposto installation
 GetCurInstType $0
 StrCmp $0 "0" 0 +2
  Call CreateDB
 
 ; Install windows service
 GetVersion::WindowsName
  Pop $R0
   
 ${If} $R0 == XP
 ${OrIf} $R0 == "Server 2003"
 ${OrIf} $R0 == "Server 2003 R2"
  !insertmacro SERVICE "create" "GisaService" "path=$INSTDIR\Servidor\GISAService.exe;autostart=1;depend=HTTPFilter;depend=RPCSS;"
 ${Else}
  !insertmacro SERVICE "create" "GisaService" "path=$INSTDIR\Servidor\GISAService.exe;autostart=1;"
 ${EndIf}
  
 ; Start service
 !insertmacro SERVICE "start" "GisaService" ""

 ; Set variable
 WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Server" "1"
  
SectionEnd

Function ReadFileLine
Exch $0 ;file
Exch
Exch $1 ;line number
Push $2
Push $3

  FileOpen $2 $0 r
 StrCpy $3 0

Loop:
 IntOp $3 $3 + 1
  ClearErrors
  FileRead $2 $0
  IfErrors +2
 StrCmp $3 $1 0 loop
  FileClose $2

Pop $3
Pop $2
Pop $1
Exch $0
FunctionEnd

; * Post installation section *

Section -Post
 SetShellVarContext all
 ReadRegStr $0 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Client"
 ReadRegStr $1 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Server"

 IntOp $2 $0 | $1
 IntCmp $2 0 end
  WriteUninstaller "$INSTDIR\uninst.exe"

  ; Programs directory and shortcuts
  CreateDirectory "$SMPROGRAMS\ParadigmaXis\Gisa"
  CreateShortCut "$SMPROGRAMS\ParadigmaXis\Gisa\Uninstall.lnk" "$INSTDIR\uninst.exe"
  
  ; If client was installed
  StrCmp $0 "1" 0 +2
   CreateShortCut "$SMPROGRAMS\ParadigmaXis\Gisa\Gisa.lnk" "$INSTDIR\Cliente\GISA.exe"

  ; Prepare vars to write key values
  ; Get local time
  Call GetLocalTime
  Pop "$0" ;Variable (for day)
  Pop "$1" ;Variable (for month)
  Pop "$2" ;Variable (for year)
  Pop "$3" ;Variable (for day of week name)
  Pop "$4" ;Variable (for hour)
  Pop "$5" ;Variable (for minute)
  Pop "$6" ;Variable (for second)

  ; Format date in yyyy-mm-dd hh:mm
  StrCpy $R1 "$2-$1-$0 $4:$5"

  ; Common registry keys
  WriteRegStr HKLM "${PRODUCT_REGKEY}" "InstallationDate" "$R1"
  WriteRegStr HKLM "${PRODUCT_REGKEY}" "Version" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
  
  Call SetRegPermissions
  
  

 end:
SectionEnd


Function SetRegPermissions

 GetVersion::WindowsName
  Pop $R0
   
 ${If} $R0 == XP
 ${OrIf} $R0 == "Server 2003"
 ${OrIf} $R0 == "Server 2003 R2"
  AccessControl::GrantOnRegKey HKLM "${PRODUCT_REGKEY}" "(S-1-5-32-545)" "FullAccess"
 ${Else}
  SetOverwrite try
  File "..\..\installer\Tools\SetACLx64.exe"
  File "..\..\installer\Tools\SetACLx86.exe"
 
  ${If} ${RunningX64}
   nsexec::ExecToStack '"$INSTDIR\Servidor\SetACLx64.exe" -on "hklm\software\paradigmaxis\gisa" -ot reg -actn ace -ace "n:S-1-5-32-545;p:full;s:y"'
  ${Else}
   nsexec::ExecToStack '"$INSTDIR\Servidor\SetACLx86.exe" -on "hklm\software\paradigmaxis\gisa" -ot reg -actn ace -ace "n:S-1-5-32-545;p:full;s:y"'
  ${EndIf}
  Delete "$INSTDIR\Servidor\SetACLx64.exe"
  Delete "$INSTDIR\Servidor\SetACLx86.exe"
 ${EndIf}
 
FunctionEnd

; * Additional installer functions *

Function .onSelChange
 Call VerifyInstalledComponents
FunctionEnd

; Let install only unexistent compoments
Function VerifyInstalledComponents
 ; Disable components
 ReadRegStr $0 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Client"
 StrCmp $0 "1" 0 +3
  IntOp $1 !${SF_SELECTED} | ${SF_RO}
  SectionSetFlags ${Client} $1

 ReadRegStr $0 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Server"
  StrCmp $0 "1" 0 +3
  IntOp $1 !${SF_SELECTED} | ${SF_RO}
  SectionSetFlags ${Server} $1
FunctionEnd

; ** Unistall **
Function un.onInit

 ; Get local time
 Call un.GetLocalTime
 Pop "$0" ;Variable (for day)
 Pop "$1" ;Variable (for month)
 Pop "$2" ;Variable (for year)
 Pop "$3" ;Variable (for day of week name)
 Pop "$4" ;Variable (for hour)
 Pop "$5" ;Variable (for minute)
 Pop "$6" ;Variable (for second)
 
 ; Format date in yyyymmddhhmmss
 ; $R7 will be ALWAYS the unistall date/time during unistall process
 Strcpy $R7 "$2$1$0$4$5$6"
 
 ReadRegStr $R2 HKLM "SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName" "ComputerName"

 ; Creating backup dir
 StrCpy $0 "Backup\$R7"
 CreateDirectory "$INSTDIR\$0"
 
 ; Set flag for database backup (value "1" only if monoposto version is installed)
 ReadRegStr $R0 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Client"
 ReadRegStr $R1 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Server"
 
 IntOp $R5 $R0 | $R1
 
 ; Set SQLPath variable
 ${If} ${RunningX64}
  SetRegView 64
  ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
  IFErrors 0 +7
   SetRegView 32
   ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
   SetRegView 64
 ${Else}
  SetRegView 32
  ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
 ${EndIf}
 
 StrCpy $SQLPATH "$R1"
FunctionEnd

; * Client *
Section "un.Cliente" un.Client
 SetShellVarContext all

 StrCpy $R0 "$INSTDIR\Cliente"

 Delete "$SMPROGRAMS\ParadigmaXis\Gisa\Gisa.lnk"

 Delete "$R0\readme.txt"
 Delete "$R0\*.dll"
 Delete "$R0\*.exe"

 RMDir "$R0"

 DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"

 ; Update variable
 WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Client" "0"

SectionEnd

; * Server *
Function un.GisaService
 DetailPrint "Removendo o serviço Gisa..."
 
 stop:
  Push 'stop'
  Push 'GisaService'
  Push ''
  Call un.Service
  
  Push 'status'
  Push 'GisaService'
  Push ''
  Call un.Service
  Pop $0

  ${If} $0 == stopped
   Push 'delete'
   Push 'GisaService'
   Push ''
   Call un.Service
   Pop $0
  ${Else}
   Sleep 1000
   Goto stop
  ${EndIf}

  ; Wait to free all resources
  DetailPrint "Esperando que todos os recursos sejam libertados..."
  delete:
   ClearErrors
   Delete "$INSTDIR\Servidor\GISAService.exe"
   IfErrors delete
FunctionEnd

Section "un.Servidor" un.Server
 
 ; Uninstall and delete service
 Call un.GisaService

 StrCpy $0 "Backup\$R7"
 
 ; Backup database (only if monoposto version is installed)
 IntCmp $R5 0 +2
  Call un.BackupDatabase

 ;backup logs
 StrCpy $1 "$INSTDIR\Servidor"
  
 CopyFiles "$1\index" "$INSTDIR\$0\"
 CopyFiles "$1\logs" "$INSTDIR\$0\"

 RMDir /r "$1\index"
 RMDir /r "$1\logs"
 
 Delete "$1\drop_connections.sql" 
 Delete "$1\DBInstallScript.sql"
 Delete "$1\CreateNTAuthoritySystemLogin.sql"
 Delete "$1\hibernate.cfg.xml"
 Delete "$1\*.config"
 Delete "$1\*.dll"
 Delete "$1\*.exe"

 RMDir "$1"
 
 ; Update variable
 WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Server" "0"
SectionEnd

Function un.BackupDatabase
 ClearErrors
 ; $0 is reserved for backup dir name 
 
 ReadRegStr $R2 HKLM "SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName" "ComputerName"
 StrCpy $R2 "$R2\${DBINSTANCENAME}"
 StrCpy $SQLSERVER "$R2"

 ; drop all databse connections
 nsexec::ExecToStack '"$SQLPATH\Binn\osql.exe" -E -S "$SQLSERVER" -d "master" -i "$INSTDIR\Servidor\drop_connections.sql" -b'
  Pop $1
 
 ; Detach database
 nsexec::ExecToStack '"$SQLPATH\Binn\osql.exe" -E -S "$SQLSERVER" -d "master" -Q "sp_detach_db GISA" -b'
  Pop $1
 
 StrCmp $1 "0" 0 error 
 
 CopyFiles "$INSTDIR\Servidor\${DBFILENAME}" "$INSTDIR\$0\${DBFILENAME}"
 CopyFiles "$INSTDIR\Servidor\${DBLOGFILENAME}" "$INSTDIR\$0\${DBLOGFILENAME}"

 ; Delete original files
 Delete "$INSTDIR\Servidor\${DBFILENAME}"
 Delete "$INSTDIR\Servidor\${DBLOGFILENAME}"
 Return
 
 error:
  MessageBox MB_OK "Não foi possível efectuar o backup da base de dados.$\r$\nPor favor, faça-o manualmente ou contacte o administrador do sistema."
  Return
FunctionEnd

Section -un.Install
 SetShellVarContext all
 ReadRegStr $0 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Client"
 ReadRegStr $1 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Server"

 IntOp $0 $0 | $1
 IntCmp $0 1 end
  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"

  Delete "$SMPROGRAMS\ParadigmaXis\Gisa\Uninstall.lnk"

  RMDir "$SMPROGRAMS\ParadigmaXis\Gisa"
  RMDir "$SMPROGRAMS\ParadigmaXis"

  Delete "$INSTDIR\uninst.exe"

  RMDir "$INSTDIR"
 end:
SectionEnd

; Let uninstall only existent compoments
Function un.AvailableComponents
 ;
 ${If} ${RunningX64}
  SetRegView 64
  ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
  IFErrors 0 +7
   SetRegView 32
   ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
   SetRegView 64
 ${Else}
  SetRegView 32
  ReadRegStr $R1 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup" "SQLPath"
 ${EndIf}
 
 StrCpy $R1 $SQLPATH
 
 ReadRegStr $0 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Client"
 StrCmp $0 "1" +3 0
  IntOp $1 !${SF_SELECTED} | ${SF_RO}
  SectionSetFlags ${un.Client} $1

 ReadRegStr $0 ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Server"
 StrCmp $0 "1" +3 0
  IntOp $1 !${SF_SELECTED} | ${SF_RO}
  SectionSetFlags ${un.Server} $1
  
FunctionEnd