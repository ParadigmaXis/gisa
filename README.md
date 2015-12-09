# GISA - Gestão Integrada de Sistemas de Arquivo

_<inserir parágrafos com o contexto do projeto>_
## O que é
O GISA é uma aplicação informática de gestão de Arquivos, concebida para apoiar o arquivista nas múltiplas operações da cadeia arquivística. GISA é marca registada da ParadigmaXis SA, sob o n.º 373005.

Para uma informação mais detalhada, consultar o sítio do [GISA](http://gisa.paradigmaxis.pt).

## Módulos e Licença
Os módulos do GISA atualmente existentes em código aberto, sob a licença [GNU General Public License v2.0] (http://www.gnu.org/licenses/gpl-2.0.html) são os seguintes:
 - *Unidades Informacionais*: Descrição arquivística segundo [ISAD(G)] (http://www.ica.org/10207/standards/isadg-general-international-standard-archival-description-second-edition.html), avaliação, indexação, pesquisa, exportação das descrições para EAD, importação de dados via Excel, associação da descrição ao respetivo documento digital ou digitalizado.
 - *Unidades Físicas*: Recenseamento das unidades físicas e pesquisa.
 - *Controlo de Autoridade*: Definição do plano de classificação orgânico segundo a [ISAAR-CPF] (http://www.ica.org/10203/standards/isaar-cpf-international-standard-archival-authority-record-for-corporate-bodies-persons-and-families-2nd-edition.html), criação de termos de indexação e de relações entre eles de forma a construir um tesauro.
 - *Administração*: Configurações, Estatísticas, Controlo de acessos.
 - *Gestão de Requisições*: Controlo de requisições e devoluções.
 - *Gestão de Depósitos*: Controlo do espaço livre em depósito.
 - *Catálogo especial de Processos de obras*: Descrição específica de uma tipologia de documentos (Processos de obras) e respetiva pesquisa.

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
 - Antes de abrir a _solution_ executar o ``updateVersionNumber.bat`` para criar o ficheiro ``AssemblyInfoShared.cs``.

### Criação de um _installer_
 - Garantir que o caminho do `msbuild` está no _path_ do sistema (e.g., `C:\Windows\Microsoft.NET\Framework\v4.0.30319`)
 - Instalar o [NSIS](http://nsis.sourceforge.net/Download) >2.4 e adicionar o diretório no _path_ do sistema (e.g., `C:\Program Files (x86)\NSIS`)
 - Compilar o GISA e o respetivo _installer_: `> msbuild GISABuilder.xml`

## Instalação

Para a instalação do GISA poderá usar um installer que tenha criado para o efeito ou então fazer o [download do setup.exe] (https://github.com/ParadigmaXis/gisa/tree/master/deployable/release/CD).

Os requisitos e os procedimentos de instalação do GISA encontram-se no [Manual de instalação online](http://gisa.paradigmaxis.pt/docs/inicio#manual_de_instalacao_do_gisa_open_source).

Depois de instalada, o acesso à aplicação é feito com o seguinte login:
 * Username: admin
 * Password: 123456

## Utilização

O GISA inclui um [Manual do Utilizador](http://gisa.paradigmaxis.pt/docs/inicio#manual_de_instalacao) disponível à comunidade para ajuda na utilização da aplicação e para colaboração na redação do mesmo.

## Divulgação

No sentido de fomentar a divulgação do GISA e a comunicação entre os diferentes membros desta comunidade, são disponibilizados os seguintes canais:
*	[Twitter](http://twitter.com/gisa)
*	[Facebook](http://www.facebook.com/pages/GISA/144794542242404)
*	[Linkedin](http://www.linkedin.com/groups?gid=3751885)

Também se disponibiliza um [grupo de utilizadores do GISA] (https://groups.google.com/forum/#!forum/gisa-users) para apoio técnico.
