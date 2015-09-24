# GISA - Gestão Integrada de Sistemas de Arquivo

_<inserir parágrafos com o contexto do projeto>_
## O que é
O GISA é uma aplicação de gestão de Arquivos, concebida para apoiar o arquivista nas múltiplas operações da cadeia arquivística. GISA é marca registada da ParadigmaXis, sob o n.º 373005.

Para uma informação mais detalhada, consultar o sítio do [GISA](http://gisa.paradigmaxis.pt).

O GISA inclui um [Manual do Utilizador](http://gisa.paradigmaxis.pt/docs/) disponível à comunidade para ajuda na utilização da aplicação e para colaboração na redação do mesmo.

No sentido de fomentar a divulgação do GISA e a comunicação entre os diferentes membros desta comunidade, são disponibilizados os seguintes canais:
*	[Twitter](http://twitter.com/gisa)
*	[Facebook](http://www.facebook.com/pages/GISA/144794542242404)
*	[Linkedin](http://www.linkedin.com/groups?gid=3751885)

Também se disponibiliza um [grupo de utilizadores do GISA] (https://groups.google.com/forum/#!forum/gisa-users) para apoio técnico.

## Licença
Os módulos do GISA disponibilizados em código aberto sob a licença [GNU General Public License v2.0] (http://www.gnu.org/licenses/gpl-2.0.html)  são os seguintes:
 - *Unidades Informacionais*: Descrição arquivística segundo [ISAD(G)] (http://www.ica.org/10207/standards/isadg-general-international-standard-archival-description-second-edition.html), avaliação, indexação, pesquisa, exportação das descrições para EAD, importação de dados via Excel, associação da descrição ao respetivo documento digital ou digitalizado.
 - *Unidades Físicas*: Recenseamento das unidades físicas e pesquisa.
 - *Controlo de Autoridade*: Definição do plano de classificação orgânico segundo a [ISAAR-CPF] (http://www.ica.org/10203/standards/isaar-cpf-international-standard-archival-authority-record-for-corporate-bodies-persons-and-families-2nd-edition.html), criação de termos de indexação e de relações entre eles de forma a construir um tesauro.
 - *Administração*: Configurações, Estatísticas, Controlo de acessos.
 - *Gestão de Requisições*: Controlo de requisições e devoluções.
 - *Gestão de Depósitos*: Controlo do espaço livre em depósito.
 - *Catálogo especial de Processos de obras*: Descrição específica de uma tipologia de documentos e respetiva pesquisa.

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

## Instalação do GISA
### Modo de funcionamento

O GISA disponibilizado em código aberto pode ter um dos seguintes modos de funcionamento:
 -	*Monoposto*, permitindo a execução autónoma a partir de um único posto de trabalho.
 - *Cliente/servidor*, estando a informação centralizada no servidor e acessível a vários utilizadores em diferentes postos de trabalho (clientes).
Dependendo do modo de funcionamento escolhido, os requisitos necessários para instalar e a forma como se processa a instalação são diferentes.

### Monoposto GISA

**Requisitos de instalação**

O equipamento necessário depende do volume de informação e do número de utilizadores. Os requisitos aconselhados, dizem respeito a um caso típico de utilização e, são os que se seguem.

Computador com as seguintes características:
 -	Processador (CPU): Pentium Core Duo 2.13GHz ou superior
 -	Memória (RAM): 2 GB ou superior
 -	Capacidade de armazenamento: 2GB ou superior (deverá ser contemplado o crescimento da BD)
 -	Sistemas operativos:
   -	Windows XP, com o Service Pack 1a ou superior
   -	Windows Vista
   -	Windows 7

Software necessário:
 - Microsoft .NET Framework 3.5 com o Service Pack 1
 - Microsoft SQLServer 2008
   -	com os componentes adicionais:
      -	Database Engine Services
      -	Management Tools Basic
      -	SQL Client Connectivity SDK
   -	com as configurações seguintes:
      -	nome da instância: GISA
      -	base de dados: Case Sensitive e Accent Sensitive
      -	autenticação: Mixed mode

*Importante*: Para verificar que tem instalado o último service pack e quaisquer atualizações críticas para o sistema operativo que está a usar bem como para instalar a Microsoft .NET Framework visite o sítio [http://windowsupdate.microsoft.com na Web] (http://windowsupdate.microsoft.com na Web).

**Instalação**

A instalação do software é iniciada na máquina escolhida, executando o programa **setup.exe** com privilégios de Administrador. Para instalar o GISA em monoposto, deverá ser aceite a licença, para poder escolher o tipo de instalação Monoposto e o diretório de instalação.

### Cliente/servidor

**Requisitos Cliente(s) GISA**

 Computador com as seguintes características:
- Processador (CPU): Pentium P4, 1.6 GHz ou superior
-	Memória (RAM): 1 GB ou superior
-	Capacidade de armazenamento: 170 MB de espaço disponível em disco
-	Velocidade da rede: 1Gbps (recomendado) ou 100Mbps (mínimo) - não necessária na versão monoposto
-	Sistemas operativos:
   -	Windows XP, com o service pack 1a ou superior
   -	Windows Vista
   -	Windows 7

Software necessário:
- Microsoft .NET Framework 3.5 com o Service Pack 1

**Requisitos Servidor GISA**

Computador com características próprias de um servidor aplicacional, sendo aconselhadas as seguintes:
-	Nº de processadores:  2 ou mais
-	Processador (CPU): Intel Core Duo ou superior
-	Velocidade do Processador: 3.00GHz no mínimo 
-	Memoria (RAM): 2 GB ou superior
-	Controlador do disco:  SATA2 ou superior
-	Capacidade de armazenamento: 2GB ou superior (deverá ser contemplado o crescimento da BD)
-	Velocidade da rede: 1Gbps (recomendado) ou 100Mbps (mínimo)
-	Sistema operativo: Windows Server 2003 ou superior

Software necessário:
-	Sistema de Gestão de Base de Dados Relacional (SGBDR): Microsoft SQL Server 2008 R2

**Instalação**

1. Executar em cada máquina cliente o programa **setup.exe** com privilégios de Administrador. Aceitando a licença, escolher o modo de instalação Cliente e o diretório de instalação.

2. Executar no servidor o programa **setup.exe** com privilégios de Administrador. Aceitando a licença, escolher o modo de instalação Servidor e o diretório de instalação.

