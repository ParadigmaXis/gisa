!define File "..\..\client\GISA\bin\Release\GISA.exe"
 
OutFile "GetExeVersion.exe"
SilentInstall silent
 
Section
 
 ## Get file version
 GetDllVersion "${File}" $R0 $R1
  IntOp $R2 $R0 / 0x00010000
  IntOp $R3 $R0 & 0x0000FFFF
  IntOp $R4 $R1 / 0x00010000
  IntOp $R5 $R1 & 0x0000FFFF
  ;StrCpy $R1 "$R2.$R3.$R4.$R5"
  ; só é preciso o numero de versao
  StrCpy $R1 "$R4"
 
 ## Write it to a !define for use in main script
 FileOpen $R0 "$EXEDIR\Version.txt" w
  FileWrite $R0 '!define Version "$R1"'
 FileClose $R0
 
SectionEnd