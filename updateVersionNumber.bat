@ECHO OFF

for /f %%i in ('git rev-parse --short HEAD') do SET HASH=%%i
for /f "delims=" %%a in ('git show -s --format^=%%ci %HASH%^^') do set TIMESTAMP=%%a

SET ASSEMBLYVERSION=2.11.*
SET INFOVERSION=2.11.%HASH%

powershell -Command "(gc ./client/AssemblyInfosShared/AssemblyInfoShared.cs.svnt) -replace '\$WCREV\$', '%ASSEMBLYVERSION%' -replace '\$HASH\$', '%INFOVERSION%' | Out-File ./client/AssemblyInfosShared/AssemblyInfoShared.cs"
powershell -Command "(gc ./installer/Resources/readme.txt.svnt) -replace '\$WCREV\$', '%INFOVERSION%' -replace '\$WCDATE\$', '%TIMESTAMP%' | Out-File ./installer/Resources/readme.txt"

powershell -Command "(gc ./server/DatabaseScripts/DatabaseInstallScript/set_database_version_number.sql.svnt) -replace '\$WCREV\$', '%INFOVERSION%' | Out-File ./server/DatabaseScripts/DatabaseInstallScript/set_database_version_number.sql"
