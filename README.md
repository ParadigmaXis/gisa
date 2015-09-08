# GISA - Gestão Integrada de Sistemas de Arquivo

_<inserir parágrafos com o contexto do projeto>_
## O que é
O GISA é uma aplicação de gestão de Arquivos, concebido para dar apoio ao arquivista nas múltiplas operações da cadeia arquivística. GISA é marca registada da ParadigmaXis, sob o n.º 373005.

## Configuração do ambiente de desenvolvimento
### Configuração de instância do SQLServer
 - Named Instance: ``GISA``
 - Database collation: ``Latin1-General-CS-AS``
 - No configuration Manager:
   - activar os protocolos named pipes e TCP/IP
   - garantir que o SQLBrowser está sempre activo
   - nas propriedades do protocolo TCP/IP:
     - limpar a porta definida em dynamic ports
     - definir o TCP Port com ``1433``

### Configuração da firewall do windows
 - A port exception for TCP Port ``1433``. In the New Inbound Rule Wizard dialog, use the following information to create a port exception:
   - Select Port
   - Select TCP and specify port ``1433``
   - Allow the connection
   - Choose all three profiles (Domain, Private & Public)
   - Name the rule “SQL – TCP 1433″
 - A port exception for UDP Port ``1434``. Click New Rule again and use the following information to create another port exception:
   - Select Port
   - Select UDP and specify port ``1434``
   - Allow the connection
   - Choose all three profiles (Domain, Private & Public)
   - Name the rule “SQL – UDP 1434″
 - A program exception for ``sqlservr.exe`` Click New Rule again and use the following information to create a program exception:
   - Select Program
   - Click Browse to select ``sqlservr.exe`` at this location: ``C:\Program Files\Microsoft SQL Server\MSSQL11.<INSTANCE_NAME>\MSSQL\Binn\sqlservr.exe`` where <INSTANCE_NAME> is the name of your SQL instance.  
   - Allow the connection
   - Choose all three profiles (Domain, Private & Public)
   - Name the rule “SQL – sqlservr.exe″
 - A program exception for ``sqlbrowser.exe``. Click New Rule again and use the following information to create another program exception:
   - Select Program
   - Click Browse to select ``sqlbrowser.exe`` at this location: ``C:\Program Files (x86)\Microsoft SQL Server\90\Shared\sqlbrowser.exe``. 
   - Allow the connection
   - Choose all three profiles (Domain, Private & Public)
   - Name the rule “SQL – sqlbrowser.exe″

### Trabalhar na _solution_ GISA.sln, no diretório ``client``
 - antes de abrir a _solution_ executar ``updateVersionNumber.bat`` para criar o ficheiro ``AssemblyInfoShared.cs``.
