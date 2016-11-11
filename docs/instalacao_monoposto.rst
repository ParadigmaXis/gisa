Instalação do GISA Open source em modo Monoposto
===================================
Requisitos
-----------------------------

O equipamento necessário depende do volume de informação e do número de utilizadores. Os requisitos aconselhados, dizem respeito a um caso típico de utilização e, são os que se seguem.

Computador com as seguintes características:

-    Processador (CPU): Pentium Core Duo 2.13GHz ou superior
-    Memória (RAM): 2 GB ou superior
-    Capacidade de armazenamento: 2GB ou superior (deverá ser contemplado o crescimento da BD)
-    Sistemas operativos: Windows XP, com o Service Pack 1a ou superior; Windows Vista; Windows 7 ou Windows 8.

Para verificar que tem instalado o último service pack e quaisquer atualizações críticas para o sistema operativo que está a usar bem como para instalar a Microsoft .NET Framework visite o sítio http://windowsupdate.microsoft.com na Web.

Software:

-    Microsoft .NET Framework 3.5 com o Service Pack 1
-    Microsoft SQL Server 2008 R2:
  -     com os componentes adicionais: Database Engine Services; Management Tools Basic e SQL Client Connectivity SDK
  -     com as configurações seguintes: nome da instância = GISA; base de dados = Case Sensitive e Accent Sensitive; autenticação = Mixed mode
-  Microsoft SQL Server Management Studio

Instalação monoposto usando o setup.exe do GISA Open Source
-----------------------------

O installer (setup.exe) da última versão poderá ser criado conforme se indica em https://github.com/ParadigmaXis/gisa ou então, também se encontra disponível para download uma versão recente em https://github.com/ParadigmaXis/gisa/tree/master/deployable/release/CD. Seguir os seguintes passos:

-    Executar setup.exe com privilégios de Administrador
-    Depois de aceitar a licença, escolher o tipo de instalação Monoposto e por fim o diretório de instalação pretendido.
-    No SQL Server Configuration Manager, no SQL Server Network Configuration:
   -       ativar os protocolos named pipes e TCP/IP
   -       nas Propriedades do protocolo TCP/IP: 1)limpar a porta definida em dynamic ports e 2) definir o TCP Port com 1433
-    No SQL Server Configuration Manager, no SQL Server Services:
   -       garantir que o serviço SQLBrowser está sempre ativo
   -       reiniciar o serviço SQLServer GISA
-    No Microsoft SQL Server Management Studio:
   -   executar o script DBInstallScript.sql, que se encontra no diretório Servidor do diretório de instalação do GISA
-    No diretório de instalação, dentro da pasta Cliente, existe um ficheiro de configuração GISA.exe.config que deverá ter os seguintes valores (alterar caso necessário):
   -   <add key=“GISA.ServerLocation” value=“localhost\GISA”/>
   -   <add key=“GISA.DataSource” value=“GISA”/>
-    Executar o GISA com utilizador ADMIN:
   -   login: admin
   -   password: 123456

