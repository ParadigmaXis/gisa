Linhas gerais de uso
====================

A grande maioria dos Arquivos, por limitação de recursos, têm que
estabelecer prioridades nas suas tarefas. Alguns adotam procedimentos
que, embora não sejam os mais adequados, permitem obter alguns
resultados a curto prazo. Embora o GISA seja flexível a práticas
tradicionalmente usadas pelos Arquivos, apresentam-se neste manual os
procedimentos mais adequados, de modo a tirar o máximo partido das
potencialidades oferecidas pelo software, ficando ao cargo de cada
serviço de Arquivo que via tomar. Na impossibilidade de adotar a via
mais adequada, aconselha-se sempre que possível a uma situação de
compromisso. Não sendo possível tratar todos os documentos existentes,
selecionam-se os mais pertinentes ou consultados com maior frequência,
pelo seu valor histórico, social, cultural ou jurídico.

Antes de arrancar, e principalmente se vão ser vários colaboradores a
usar a aplicação, é aconselhável que seja feito um planeamento das
tarefas as serem efetuadas no GISA. Há algumas tarefas que só podem ser
executadas após a execução de outras, há também algumas que poderão ser
feitas em simultâneo e é crucial a distribuição das responsabilidades de
acesso e utilização, por parte dos utilizadores, em função das tarefas
atribuídas a cada um.

Definição de utilizadores e perfis de utilização
------------------------------------------------

A primeira tarefa a fazer no GISA (desde que não seja monoposto) é criar
utilizadores, grupos de utilizadores e definir permissões por módulos.
Para um maior detalhe consultar `Utilizadores <utilizadores.html>`__ ou
`Grupos de utilizadores <grupos_utilizadores.html>`__.

Existem valores de `permissões por omissão <permissoes_omissao.html>`__
atribuídas pelo sistema, que poderão a qualquer momento ser alterados
por um utilizador. Contudo, para que o resultado da atribuição de
permissões seja o esperado, será necessário ter um conhecimento
detalhado do `cálculo de permissões <permissoes_calculo.html>`__.

O restante tipo de `permissões por plano de
classificação <permissoes_plano.html>`__ e `permissões por objeto
digital <permissoes_od.html>`__, deverá ser gerido à medida do
necessário. As `permissões por módulo <permissoes_modulo.html>`__ também
podem ser alteradas num painel específico.

Para uma melhor compreensão do mecanismo das permissões consultar
`exemplos de atribuição de permissões <permissoes_exemplos.html>`__.

Construção do plano de classificação
------------------------------------

Teoricamente, o passo a seguir, deverá ser a construção de um `plano de
classificação <introducao.html#plano-de-classificacao>`__, seguindo a
abordagem do GISA. Em caso de dificuldade, pode somente criar-se o nível
orgânico de topo, onde ficarão todas as estruturas documentais, e
futuramente, criar-se a estrutura orgânica, movendo as estruturas
documentais previamente criadas para as entidades produtoras adequadas.

Para a construção de um plano de classificação no GISA, seguindo um
quadro orgânico-funcional, pode seguir-se o seguinte procedimento:

-  Criar uma *entidade detentora* e, opcionalmente, grupos de arquivos.
   Mais informação em `Entidade
   Detentora <descricao_ui.html#entidade-detentora>`__ e `Grupo de
   arquivos <descricao_ui.html#grupo-de-arquivos>`__.
-  Criar a *estrutura orgânica*, debaixo de uma entidade detentora ou de
   um grupo de arquivo e para isso:

   -  Criar uma entidade produtora no módulo *Controlo de
      autoridade/Entidade produtora*. Mais informação em `Criar uma
      entidade
      produtora <entidade_produtora.html#criar-uma-entidade-produtora>`__.
   -  Definir essa entidade produtora como o *nível de topo* da
      estrutura orgânica. Mais informação em `Definir nível de
      topo <descricao_ui.html#definir-nivel-de-topo>`__.
   -  Criar o resto das entidades produtoras ( `Criar uma entidade
      produtora <entidade_produtora.html#criar-uma-entidade-produtora>`__),
      relacionando-as ( `Relações entre entidades
      produtoras <entidade_produtora.html#relacoes>`__) entre si de
      forma a constituir a estrutura orgânica. Essas relações poderão
      ser do tipo *hierárquico*, *associativo*, *familiar* ou
      *temporal*.

-  Criar a *estrutura documental*, ou seja, unidades informacionais
   (séries, subséries, documentos ou subdocumentos) organizadas
   hierarquicamente debaixo das respetivas entidades produtoras da
   estrutura orgânica. Mais informação em `Estrutura
   documental <descricao_ui.html#estrutura-documental>`__.

Descrição multinível
--------------------

A descrição documental é fundamental na localização de documentos. A
descrição multinível usada no GISA permite gerir as descrições de forma
a que o esforço desta tarefa possa ser reduzido, disponibilizando níveis
de descrição suficientemente genéricos, de conjuntos de documentos
preferencialmente pouco pesquisados, embora nestes casos nunca possam
ser garantidos bons resultados nas pesquisas de informação. Ao mesmo
tempo, podem existir níveis de descrição mais específicos, no limite o
próprio documento, quando a informação neles contida é suficientemente
relevante para tal.

Muitos Arquivos usam a unidade física (que pode corresponder a várias
unidades informacionais) como unidade de descrição arquivística, em vez
da unidade informacional. Embora possa facilitar o trabalho, esta
prática, como já foi dito, poderá não conduzir a grandes resultados na
pesquisa de informação.

O preenchimento dos campos de descrição pode envolver a referência a
unidades físicas que deverão ser previamente recenseadas, a conteúdos e
tipologias informacionais que deverão constar na lista de registos de
autoridade ou então a objetos digitais. Ou seja, deverá ser previsto se
será conveniente executar as tarefas de recenseamento de unidades
físicas e de construção da lista dos registos de autoridade antes da
descrição das unidades informacionais ou executá-las à medida que vão
sendo necessárias para determinada descrição.

Quaisquer outras tarefas, tais como avaliação, pesquisa, impressão de
relatórios, estatísticas, publicação na Internet, só são possíveis sobre
meta-informação introduzida anteriormente no sistema.

A descrição de cada campo e o modo de preenchimento encontra-se em
detalhe na secção `Descrição
multinível <descricao_ui.html#descricao-multinivel>`__.

Recenseamento de unidades físicas
---------------------------------

A representação e descrição do arquivo em termos físicos é importante,
pois facilita a gestão de um arquivo. Quando a pesquisa de determinada
informação é crucial, não basta descrever a unidade física, é
indispensável a descrição das unidades informacionais que a constituem.

Para fazer o recenseamento e descrição de unidades físicas, ver
`Unidades Físicas <descricao_uf.html>`__.

A associação de unidades físicas a unidades informacionais pode ser
feita via:

-  unidades físicas e para isso consultar `Unidades de
   descrição <descricao_uf.html#unidades-de-descricao>`__
-  unidades informacionais e para isso consultar `Identificação -
   Dimensão e suporte <ident_dim.html#dimensao-e-suporte>`__.

Criação de tipologias informacionais
------------------------------------

A criação de tipologias informacionais numa lista controlada e o
relacionamento entre os diferentes termos, encontra-se detalhada em
`Tipologias informacionais <tipologia_informacional.html>`__.

Entradas no registo de autoridade deste tipo servem para o preenchimento
do campo `3.1. Âmbito e
Conteúdo <ambito_conteudo.html#conteudo-e-estrutura---ambito-e-conteudo>`__
das descrições arquivísticas segundo a ISAD(G).

Criação de registos de autoridade do tipo ideográfico, geográfico e onomástico
------------------------------------------------------------------------------

A página `Conteúdos <conteudo.html>`__ apresenta os passos necessários à
criação de registos de autoridade do tipo:

-  ``Ideográfico``,
-  ``Onomástico`` ou
-  ``Nome geográfico ou topónimo citadino``.

Estes registos são utilizados na indexação das unidades de descrição
arquivística, preenchendo a zona `\*. Indexação existente no módulo
Unidades informacionais/Descrição <indexacao.html>`__.

Associação de objetos digitais
------------------------------

A associação de objetos digitais que não se encontrem num Repositório
digital (imagens, som, documentos de texto, etc.) às descrições
arquivísticas encontra-se detalhada na secção *Índice de imagens* da
página `Unidades informacionais <descricao_ui.html>`__.

A associação de objetos digitais, que se encontrem num Repositório
digital, às descrições arquivísticas encontra-se detalhada na página
`Objetos digitais <objetos_digitais.html>`__.

Avaliação documental
--------------------

Independentemente da abordagem adotada, a *avaliação documental* tem
alguns procedimentos que deverão ser tidos em conta.

Numa primeira etapa, deverá ser efetuada a *`avaliação das
séries <avaliacao.html#avaliacao-de-um-nivel-documental>`__* ou dos
documentos que não constituam série, escolhendo qual a melhor abordagem
para chegar a um destino final adequado. Assim, se a série for para:

-  ``Conservação`` - salvo alguma exceção, a maioria dos documentos
   dessa série deverão ser conservados.
-  ``Eliminação`` - deve indicar-se o prazo ao fim do qual os seus
   documentos poderão ser eliminados, decidindo após esse prazo qual o
   verdadeiro destino de cada um.

Para o caso de séries cujo destino é ``Conservação``, pode definir-se de
imediato o destino de todos os seus documentos em bloco, pois serão na
sua maioria para conservar. Para um maior detalhe consultar `Passo 1:
Avaliação e seleção dos conteúdos da unidade de
descrição <avaliacao.html#passo-1avaliacao-e-selecao-dos-conteudos-da-unidade-de-descricao>`__.

Periodicamente, poderão ser listados, por série para eliminar, todos os
documentos cujo *prazo de conservação está ultrapassado*. Para isso,
usar a pesquisa avançada na área de *Unidades informacionais/Pesquisa*,
detalhada em `Pesquisa na
Descrição <pesquisa_ui.html#pesquisa-na-descricao>`__.

Como numa série cujo destino final seja ``Eliminação``, alguns dos seus
documentos poderão ser, por algum motivo, para ``Conservação``, quando
existirem documentos cujo prazo de conservação está ultrapassado, será
conveniente estabelecer o destino definitivo de cada um deles,
individualmente ou em bloco. Para se trabalhar em bloco, deverá ser
selecionada a série, que se pretende, e registar na zona `Passo 1:
Avaliação e seleção dos conteúdos da unidade de
descrição <avaliacao.html#passo-1avaliacao-e-selecao-dos-conteudos-da-unidade-de-descricao>`__.

Para o caso dos documentos serem registados como eliminados, é
conveniente associá-los a um *auto de eliminação* ainda no painel `Passo
1: Avaliação e seleção dos conteúdos da unidade de
descrição <avaliacao.html#passo-1avaliacao-e-selecao-dos-conteudos-da-unidade-de-descricao>`__
e incluir as correspondentes unidades físicas, que são para eliminar, no
auto de eliminação através do painel `Passo 2: Seleção das unidades
físicas <avaliacao.html#passo-2selecao-das-unidades-fisicas>`__.

Publicação de unidades de descrição
-----------------------------------

Para publicar uma determinada unidade de descrição na Internet, ver a
secção `Publicação de um nível de
descrição <avaliacao.html#publicacao-de-um-nivel-de-descricao>`__.

Para a publicação em lote, de todos os níveis debaixo de um determinado
nível, ver a secção `Passo 1: Avaliação e seleção dos conteúdos da
unidade de
descrição <avaliacao.html#passo-1avaliacao-e-selecao-dos-conteudos-da-unidade-de-descricao>`__.

Pesquisa
--------

A pesquisa no GISA pode ser feita:

-  ``via aplicação``, dividindo-se em dois tipos de objetos de procura
   diferentes:

   -  ``informação`` - sendo efetuada na área de *Unidades
      informacionais* e o resultado são registos de séries ou
      documentos, os quais poderão referenciar os respetivos documentos
      digitais, caso estejam acessíveis.
   -  ``suportes físicos`` - neste caso a pesquisa é feita na área de
      *Unidades físicas*, devolvendo o registo das unidades físicas.

-  ``via Web``, acessível por todos através do URL do GISA Internet
   atribuído a cada arquivo (ex: http://arquivo.cm-gaia.pt/)

A pesquisa, quer via aplicação quer via Web, é “full-text”, ou seja,
recupera informação procurando palavras ou expressões de pesquisa na
meta-informação registada. Para um melhor entendimento de como construir
expressões de pesquisa consultar `Expressões de
pesquisa <pesquisa.html>`__.

Elaboração de inventários, catálogos e outros relatórios
--------------------------------------------------------

Para elaboração de listagens necessárias ao serviço de arquivo:

-  ``Catálogos``, ``inventários`` de unidades informacionais e autos de
   eliminação, na área *Unidades informacionais/Descrição*. Para mais
   detalhe ver a secção `Geração de
   relatórios <descricao_ui.html#geracao-de-relatorios>`__.

\* ``Listas de unidades informacionais``, resultados de pesquisas
(obedecendo a determinados critérios de pesquisa) efetuadas na área
*Unidades informacionais/Pesquisa*. Para mais detalhe ver a secção
`Relatórios de unidades informacionais <pesquisa_ui.html#relatorios>`__.

-  ``Listas de unidades físicas``, resultados de pesquisas (obedecendo a
   determinados critérios de pesquisa) efetuadas na área *Unidades
   físicas/Pesquisa*. Para mais detalhe ver a secção `Relatórios de
   unidades físicas <pesquisa_uf.html#relatorios>`__.

\* ``Listas de entidades produtoras``, ver a secção `Geração de
relatório de entidades
produtoras <entidade_produtora.html#geracao-de-relatorio-de-entidades-produtoras>`__
da página `Entidades produtoras <entidade_produtora.html>`__.

-  ``Listas de registos de autoridade do tipo Conteúdo``, ver a secção
   `Geração de relatório de
   conteúdos <conteudo.html#geracao-de-relatorio-de-conteudos>`__.

\*
``Listas de registos de autoridade do tipo Tipologias informacional``,
ver a secção `Geração de relatório de tipologias
informacionais <tipologia_informacional.html#geracao-de-relatorio-de-tipologias-informacionais>`__.

Análise estatística e controlo de desempenho
--------------------------------------------

A análise estatística que pode ser feita no GISA, encontra-se detalhada
na página `Estatísticas <estatisticas.html>`__.

Gestão de requisições
---------------------

Quando se fazem pesquisas de documentos, pode ser importante saber se
determinado documento se encontra em depósito ou se foi requisitado.
Para se ter esta informação será necessário registar no sistema todas as
requisições de documentos (ver `Requisições <requisicoes.html>`__), bem
como todas as devoluções (ver `Devoluções <devolucoes.html>`__).

Periodicamente, poderá ser necessário saber qual a lista de todos os
documentos requisitados e ainda não devolvidos. Ver o procedimento em
`Imprimir Lista de
requisitados <requisicoes.html#imprimir-lista-de-requisitados>`__.

Controlo da ocupação do depósito
--------------------------------

O controlo da taxa de ocupação do depósito torna-se crítico quando o
espaço livre é escasso. Para se efetuar este controlo (ver com detalhe
em `Gestão de depósitos <gestao_depositos.html>`__) de forma eficaz,
será necessário registar:

-  todas as unidades físicas existentes no depósito, não esquecendo o
   preenchimento da sua dimensão. Para isso consultar `Unidades
   Físicas <descricao_uf.html>`__;

\* todas as entradas de unidades físicas no depósito, não esquecendo o
preenchimento da sua dimensão. Para isso consultar `Unidades
Físicas <descricao_uf.html>`__;

-  o destino de todos os documentos para eliminar, criando os respetivos
   autos de eliminação quando o prazo de conservação for ultrapassado.
   Este processo de avaliação é feito na área de *Unidades
   informacionais/Descrição* e para um maior detalhe consultar `Conteúdo
   e estrutura - Avaliação, seleção e eliminação <avaliacao.html>`__;

\* o abate (eliminação física) dos documentos que constam nos autos de
eliminação. Para isso consultar `Abate de Unidades
físicas <gestao_depositos.html#unidades-fisicas-associadas>`__.

Registo do abate de unidades físicas
------------------------------------

O registo da eliminação física de documentos é feito na área de
*Unidades físicas/Gestão de depósitos* e encontra-se explicada com
detalhe em `Abate de Unidades
físicas <gestao_depositos.html#unidades-fisicas-associadas>`__.

Exportação de dados do GISA
---------------------------

Para *exportar* dados do GISA:

-  *para ficheiro*, usando o formato **EAD**, consultar a secção
   `Exportação para EAD <descricao_ui.html#exportacao-para-ead>`__;

\* *para servidores agregadores*, que reconheçam o protocolo
**OAI-PMH**, basta ter o GISA Internet instalado e na instalação ser
configurada essa opção.

Importação de dados para o GISA
-------------------------------

Para *importar* dados para o GISA, estes devem estar num ficheiro Excel,
obedecendo a um formato e a um procedimento detalhados em `Importação de
dados em Excel <descricao_ui.html#importacao-de-dados-em-excel>`__.
