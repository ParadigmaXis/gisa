Permissões
==================================================

A gestão de permissões é necessária quando existem utilizadores com
responsabilidades e competências distintas. As *permissões* são dadas a
um *utilizador ou grupo de utilizadores*, podendo ou não efetuar
*operações* sobre *recursos* existentes na aplicação.

Os valores possíveis para as permissões são:

-  ``Sim`` - para dar acesso, ou
-  ``Não`` - para limitar o acesso.

Existem três diferentes recursos da aplicação, cujas operações deverão
estar ou não acessíveis, dependendo do utilizador:

-  ``Módulos`` - é onde se define a possibilidade ou não de criar,
   alterar, remover ou visualizar registos em determinada área da
   aplicação. Por exemplo, definir só permissões de leitura na área de
   Controlo de Autoridade, ou permissões totais na Descrição de Unidades
   físicas, etc..
-  ``Níveis`` - é possível controlar o tipo de acesso (criação,
   alteração, remoção e visualização) a cada nível de descrição da
   estrutura arquivística existente. Por exemplo, um utilizador só poder
   visualizar e expandir determinado nível de descrição, sem poder
   editar, apagar ou criar nível subjacente.
-  ``Objetos digitais`` - é possível controlar o tipo de acesso (escrita
   e visualização) a cada objeto digital de forma independente do acesso
   à unidade de descrição correspondente. Assim, qualquer utilizador com
   acesso a uma descrição de um documento, pode não ter acesso ao
   documento digital ou então ter acesso, total (a todos os objetos
   digitais) ou parcial (a parte dos objetos digitais).

Em ambiente monoposto o GISA admite um único utilizador com permissões
totais.

Quando se cria um utilizador, um nível de descrição ou um objeto
digital, o sistema atribui automaticamente `permissões por
omissão <permissoes.html#permissoes_omissao>`__. São valores implícitos e
representam-se em itálico. Estes valores podem ser alterados
explicitamente pelo utilizador ou alterados implicitamente, sendo o
resultado de `cálculo de permissões <permissoes.html#permissoes_calculo>`__. Esta
distinção de valores é importante nos cálculos de permissões, pois um
valor explícito prevalece sobre um valor implícito. Os valores das
permissões por omissão de um utilizador, sobre um nível ou objeto por
ele criado, são excecionalmente **Sim** explícitos, uma vez que ele deve
ser o proprietário do nível e só perder essa prevalência de forma
explícita por algum utilizador com permissão para tal.

Permissões por omissão
------------------

Quando se criam utilizadores, grupos de utilizadores, níveis de descrição e objetos digitais, surgem permissões por omissão, cujo valor depende de caso para caso. Apresentam-se a seguir os diferentes casos.

### Grupos ou Utilizadores novos
#### Permissões nos módulos

O valor por omissão das permissões nos módulos, para o caso de criação de um:

-  **Utilizador** - é *Não* (implícito) para todas as operações (Criar, Ler, Escrever, Apagar).

-  **Grupo de utilizadores** - também é *Não* (implícito) para todas as operações (Criar, Ler, Escrever, Apagar).

#### Permissões nos níveis

Quando se criam grupos ou utilizadores novos, o sistema atribui automaticamente permissões sobre os níveis de descrição já existentes. O valor por omissão nas permissões nos níveis, para o caso de criação de um:

-  **Utilizador com acesso a toda a informação** - é *Sim* (implícito) para todas as operações (Criar, Ler, Escrever, Apagar e Expandir) em todos os níveis existentes. Isto é equivalente a criar um utilizador que pertença a um grupo de utilizadores pré-definido, cujas permissões têm o valor Sim (explícito) para todas as operações de todos os níveis, e a aplicar a **Regra 2.1** do cálculo de permissões.

-  **Utilizador com acesso apenas a informação publicada** - é *Não* (implícito) nas operações sobre todos os níveis existentes, exceto na operação Ler sobre os níveis publicados que tem o valor *Sim* (implícito). Isto é equivalente a criar um utilizador que pertença a um grupo de utilizadores pré-definido, cujo valor das permissões das várias operações sobre os níveis é Não (explícito), exceto a permissão da operação Ler sobre os níveis publicados que é Sim (explícito). As permissões deste novo utilizador são calculadas aplicando a **Regra 2.1** do cálculo de permissões. Na prática, esse utilizador só pode ler níveis publicados.

- **Grupo de utilizadores** - é *Não* (implícito) para todas as operações (Criar, Ler, Escrever, Apagar e Expandir) sobre os níveis existentes.

#### Permissões nos objetos digitais

O valor por omissão das permissões sobre os objetos digitais já existentes, quando é criado um:

- **Utilizador** - é *Sim* (implícito) para Ler e Escrever.

- **Utilizador com acesso a toda a informação** - é *Sim* (implícito) para Ler e Escrever. Isto é equivalente a criar um utilizador que pertença a um grupo de utilizadores pré-definido, cujas permissões têm o valor Sim (explícito) para Ler e Escrever de todos os níveis, e a aplicar a **Regra 2.1** do cálculo de permissões.

-  **Utilizador com acesso apenas a informação publicada** - é *Sim (implícito)* para Ler e *Não* (implícito) para Escrever. Isto é equivalente a criar um utilizador que pertença a um grupo de utilizadores pré-definido, cujo valor das permissões de Ler e Escrever sobre os níveis é Não (explícito), exceto a permissão da operação Ler sobre os níveis publicados que é Sim (explícito). As permissões deste novo utilizador são calculadas aplicando a Regra 2.1 do cálculo de permissões. Na prática, esse utilizador só pode ler níveis publicados.

-  **Grupo de utilizadores** - é *Não* (implícito) para Ler e Escrever.


### Níveis novos

Um utilizador, para criar níveis de descrição, debaixo de um determinado nível, deve pelo menos ter permissões para Criar e Expandir sobre esse nível. Para qualquer nível criado de novo, o sistema atribui automaticamente permissões iniciais, ao utilizador que o criou e a todos o outros, estabelecendo as operações (Criar, Ler, Escrever, Apagar e/ou Expandir) possíveis nesse nível.

Quando um nível novo é criado **debaixo de um nível** documental, todos os utilizadores e grupos ficam com permissões implícitas no novo nível, iguais às permissões que têm sobre o nível documental imediatamente superior.

Quando um nível novo (orgânico ou documental) está diretamente **subjacente a uma unidade orgânica**, o valor por omissão das permissões é:

-  **no utilizador que criou o nível** - Sim (explícito) sobre todas as operações, uma vez que esse utilizador é o proprietário do nível. Este valor é excecionalmente explícito, uma vez que deve ter prevalência sobre qualquer resultado de cálculo de permissões e só poder ser alterado de forma explícita por algum utilizador com permissões para isso.

- **nos outros utilizadores**, definidos como:

    -  **Utilizador com acesso apenas a informação publicada** - *Não* (implícito) em todas as operações desse nível. Se o nível for publicado, a operação Ler passará a ter o valor *Sim* (implícito).
    -  **Utilizador com acesso a toda a informação** - *Sim* (implícito) em todas as operações sobre esse nível.

- **nos grupos de utilizadores** - *Não* (implícito) em todas as operações.

### Objetos digitais novos

Um utilizador com permissões de criação de objetos digitais tem, pelo menos, permissões para Ler e Expandir os níveis de descrição aos quais vai associar o objeto digital. Para qualquer objeto digital criado de novo, o sistema atribui permissões iniciais ao utilizador que o criou e a todos o outros.

Para um objeto digital novo, o valor por omissão das permissões sobre as operações (Ler e Escrever) é:

-  **no utilizador que criou o objeto digital** - Sim (explícito) em todas as operações, uma vez que esse utilizador é o proprietário do objeto digital. Este valor é excecionalmente explícito, uma vez que deve ter prevalência sobre qualquer resultado de cálculo de permissões e só poder ser alterado de forma explícita por algum utilizador com permissões para isso.

-  nos outros utilizadores, definidos como:

    -  **Utilizador com acesso apenas a informação publicada** - as permissões deste utilizador devem ser implícitas mas iguais às permissões que ele tem sobre o nível do objeto, antes de este estar publicado (caso tenha acontecido). Se o objeto digital for publicado, a operação Ler passa a ter o valor *Sim* (implícito).
    -  **Utilizador com acesso a toda a informação** - *Sim* (implícito) em todas as operações sobre esse objeto digital.

-  nos grupos de utilizadores - *Não* (implícito) em todas as operações.



Cálculo de permissões
------------------

As permissões dos utilizadores sobre os níveis, objetos digitais ou módulos, poderão ser alteradas de forma explícita pelo utilizador, via área de *Administração*, caso contrário, assumirá um valor implícito, sendo o valor definido por omissão ou então resultado do cálculo de permissões inerente ao GISA.

O cálculo de permissões de um utilizador ocorre sempre que:

-  este é associado a um grupo;
-  as suas permissões sobre um nível documental superior forem alteradas explicitamente;
-  um nível de descrição é movido na estrutura para de baixo de outro nível;
-  as permissões de um grupo ao qual pertence são alteradas.

O cálculo, ocorrendo, aplica as regras apresentadas a seguir.

Para ver exemplos deste cálculo, consultar **Exemplos de atribuição de permissões**.

### Regras de cálculo das permissões dos utilizadores
#### Regra 1

1. Caso o utilizador tenha um valor explícito na permissão de uma operação sobre um nível, objeto ou módulo, este não é afetado por nenhum cálculo, permanecendo esse valor.

2. Caso o utilizador tenha um valor implícito na permissão de uma operação sobre um nível, objeto ou módulo, o valor passará a ser o resultado do cálculo aplicando a **Regra 2**.

#### Regra 2

Caso o utilizador tenha um valor implícito na permissão de uma operação sobre um nível, objeto ou módulo:

##### Regra 2. 1

Se o utilizador pertence a um grupo de utilizadores, a permissão sobre um nível, objeto ou módulo, assume o valor implícito equivalente ao valor da permissão do grupo, desde que este seja explícito. Caso o resultado não seja um valor explícito, aplicar a **Regra 3**.

##### Regra 2. 2

Se o utilizador pertence a vários grupos de utilizadores, a permissão sobre um nível, objeto ou módulo, assume o valor implícito equivalente ao valor resultado da aplicação das regras do cálculo de permissões entre grupos, desde que este seja explícito. Caso o resultado não seja um valor explícito, aplicar a **Regra 3**.

##### Regra 2. 3

Se o utilizador não pertence a nenhum grupo, aplicar a **Regra 3**.

#### Regra 3

Caso o utilizador tenha um valor implícito na permissão de uma operação sobre um nível, objeto ou módulo, não pertence a nenhum grupo ou o resultado da permissão via grupo(s) tem um valor implícito:

1. Se se tratar da permissão de uma operação sobre um nível documental, esta assume o valor implícito equivalente ao valor da permissão do nível documental hierarquicamente superior.

2. Caso contrário, aplicar a **Regra 4**.

#### Regra 4

Se nenhuma das regras se aplicar, ou seja, se o utilizador tiver um valor implícito na permissão de uma operação sobre um objeto ou módulo, não pertence a nenhum grupo ou o resultado da permissão via grupo(s) tem um valor implícito, a permissão mantêm-se com o valor implícito que tiver.
Regras de cálculo das permissões entre grupos de utilizadores

Quando um utilizador tem uma permissão implícita e pertence a vários grupos, deverá haver um cálculo de permissões entre esses grupos. O resultado deste cálculo é utilizado na **Regra 2** do **Cálculo de permissões** e é obtido através das regras de cálculo apresentadas a seguir.

#### Regra 5

Se determinada permissão, nos vários grupos tiver valores explícitos e entre eles houver pelo menos um Não explícito é esse o resultado, senão é o Sim explícito. Caso não haja valores explícitos, aplica-se a **Regra 6**.

#### Regra 6

Se só houver permissões implícitas nos vários grupos, desde que um deles tenha o valor *Não* implícito, é esse o resultado. Caso contrário, o resultado é o valor *Sim* implícito.


Exemplos de atribuição de permissões
------------------

### Exemplo de grupo de utilizadores só com acesso à área de pesquisa

Este exemplo tem como objetivo __definir um conjunto de utilizadores só com permissões para aceder às áreas de Pesquisa__. 

Assim, apresentamos a melhor forma de o fazer:

1. Na área `Grupo de utilizadores <grupos_utilizadores.html>`__, criar o grupo de utilizadores **Leitor**, cujas `permissões por omissão <permissoes.html#permissoes_por_omissao>`__ sobre os módulos aparecem todas a *Não*. 

2. Definir explicitamente as __permissões sobre os módulos__ no grupo de utilizadores. Para este caso, colocar o valor Sim (Explícito) nos módulos de *Pesquisa* que existem nas *Unidades informacionais* e também nas *Unidades físicas*. 

|image0|

3. Na área`Utilizadores <permissoes.html#utilizadores>`__ , criar os __utilizadores__, cujas
`permissões por omissão <permissoes.html#permissoes_por_omissao>`__ sobre os módulos são iniciadas com o valor *Não*. 
Neste exemplo, pouco importa o que é definido por omissão nas permissões sobre os níveis. 

4. Associar cada utilizador ao grupo **Leitor**. A janela a seguir mostra o utilizador **fatima** associado a esse grupo. 

|image1|

As permissões do utilizador, como não foram definidas explicitamente, aparecem em itálico e são o resultado da aplicação das regras do `cálculo de permissões <permissoes.html#permissoes_calculo>`__.

|image2|

Em __conclusão__, pode verificar-se que as permissões do utilizador **fatima** com o valor:

    - *Sim* implícito é o resultado da combinação do *Não* existente, por omissão, no utilizador, com o Sim explícito do grupo de utilizadores, aplicando a `Regra 2.1 <permissoes.html#regra_2_1>`__ do cálculo de permissões.

    - *Não* implícito, deve-se ao facto de o grupo de utilizadores ao qual pertence também ter *Não* implícito e portanto, ficar com o valor inicial inalterado, segundo a `Regra 4 <permissoes.html#regra_4>`__ do cálculo de permissões. 

	
### Exemplo de associação de um utilizador a mais que um grupo de utilizadores

Este exemplo ilustra as permissões de um utilizador, que __pertence a mais que um grupo de utilizadores__, como sendo o resultado do cálculo das combinações das permissões dos vários grupos.

1. Na área `Grupo de utilizadores <grupos_utilizadores.html>`__, criar um __grupo de utilizadores__, **LeitorCA**, com permissões explícitas de leitura nos módulos do *Controlo de autoridade* e com restrição explícita no acesso ao módulo *Pesquisa* das *Unidades Físicas*, tal como se pode observar no seguinte painel: 

|image3|

2. Na área `Utilizadores <utilizadores.html>`__, associar o __utilizador__ **fatima** ao grupo **LeitorCA**. Como este utilizador pertence ao grupo **Leitor** (ver exemplo anterior) e as suas permissões não foram definidas explicitamente, resultam do cálculo das permissões dos dois grupos. 

|image4|

Em __conclusão__, verifica-se que o utilizador **fatima** não tendo permissões explícitas, assume o valor:

- *Sim* na operação ''Ler'' nos módulos do *Controlo de autoridade*, sendo o resultado da combinação das permissões dos dois grupos de utilizadores a ele atribuídos, por aplicação da `Regra 2.2 <permissoes.html#regra_2_2>`__, a qual remete para a `Regra 5 <permissoes.html#regra_5>`__ do cálculo de permissões. Pela `Regra 5 <permissoes.html#regra_5>`__, entre vários grupos de utilizadores, quando um dos grupos (**LeitorCA**) tem uma permissão com Sim explícito e o outro (**Leitor**) com *Não* implícito, o resultado é o Sim explícito. Voltando à `Regra 2.2 <permissoes.html#regra_2_2>`__, o resultado final é  portanto o mesmo valor mas implícito, *Sim*.

-  *Não* existente no módulo *Pesquisa* de *Unidades físicas*, sendo o resultado da combinação das permissões dos dois grupos de utilizadores a ele atribuídos, por aplicação da `Regra 2.2 <permissoes.html#regra_2_2>`__, a qual remete para a `Regra 5 <permissoes.html#regra_5>`__ do cálculo de permissões. Pela `Regra 5 <permissoes.html#regra_5>`__, quando um dos grupos (**LeitorCA**) tem um Não explícito e o outro (**Leitor**) um Sim explícito, o resultado é o Não explícito. Voltando à `Regra 2.2 <permissoes.html#regra_2_2>`__, o resultado final é  portanto o mesmo valor mas implícito, *Não*.

-  *Não* implícito nas restantes permissões, por aplicação da `Regra 4 <permissoes.html#regra_4>`__ do cálculo de permissões. Isto deve-se ao facto de os grupos de utilizadores, ao qual o utilizador pertence, terem todos valores implícitos e tratar-se de permissão sobre módulos.

### Exemplos de atribuição de permissões por nível

Apresentam-se de seguida alguns casos exemplificativos de como usar esta funcionalidade de controlo de acesso a níveis arquivísticos:

-  **Exemplo 1** - Definição de um utilizador externo com acesso de leitura aos níveis publicados e também a um determinado documento específico, que não é considerado público. 

-  **Exemplo 2** - Definição de um grupo de utilizadores, cujos utilizadores deverão ter acesso total a um determinado ramo da estrutura arquivística e acesso de leitura e navegação em todos os outros níveis produtores e documentais. 

-  **Exemplo 3** - Definição de um grupo de utilizadores, cujos utilizadores deverão ter acesso total a todos os níveis da estrutura arquivística, exceto a um ramo, cujo acesso deverá ser interdito. 

#### Exemplo 1

Este exemplo é o caso típico de um __utilizador externo registado no sistema só com acesso de leitura aos níveis publicados e a determinados documentos__, não públicos, aos quais ele, por determinada razão, poderá aceder para leitura.

  - Primeiro, na área `Utilizadores <utilizadores.html>`__, criar um utilizador, **antonio1945**, escolhendo a opção ''Acesso apenas a informação publicada''. Por `omissão <permissoes.html#nineis_novos>`__, as permissões deste utilizador sobre os __módulos__ assumem o valor *Não* implícito e as suas permissões sobre os __níveis__ assumem também o valor *Não* implícito, exceto nos níveis publicados, cuja operação ''Ler'' assume o valor *Sim* implícito.
  - De seguida, associar este utilizador ao grupo **Leitor**, referido nos exemplos anteriores, para poder ter acesso aos módulos de pesquisa, por aplicação da `Regra 2.1 <permissoes.html#regra_2>`__ do cálculo de permissões. 
  - Para definir que o utilizador **antonio1945** tem acesso de leitura a um determinado documento, no módulo `Permissões pelo Plano de Classificação <permissoes.html#permissoes_plano>`__:
     - Selecionar o documento *Testamento de António Francisco (fl. 6v-8v)* e o utilizador **antonio1945** na área de contexto. 
     - No ''Filtro'' manter a opção ''Próprio'', pois é sobre esse nível que se pretende definir permissões. 
     - Atribuir explicitamente permissões de leitura ao nível mostrado, colocando em ''Ler'' o valor Sim (explícito), aplicando-se a `Regra 1 <permissoes.html#regra_1>`__ do cálculo de permissões.

|image5|

#### Exemplo 2

Neste exemplo, escolhe-se uma situação em que há a necessidade de um grupo de utilizadores internos __com autorização para navegar e visualizar todos os níveis de descrição__, mas __com permissão total somente sobre informação relativa a uma série documental__.

Como pode haver grupos de utilizadores a trabalhar em séries diferentes, a melhor forma é criar um grupo (**GrupoA**), cujos utilizadores terão acesso de leitura e navegação em todos níveis arquivísticos. Este grupo poderá ser combinado com qualquer outro grupo, a ser criado com determinado perfil, como por exemplo, com acesso total a uma série específica (**GrupoB**).

Antes de mais, dado que neste caso se vão atribuir as mesmas permissões a um conjunto muito grande de níveis ao mesmo tempo, será conveniente definir-se um número grande de elementos por página no módulo `Configuração global <configuracao_global.html>`__. No fim desta tarefa de atribuição de permissões, pode sempre voltar-se ao número de elementos por página inicial.

Então, o procedimento pode ser o seguinte:

1. Na área `Grupo de utilizadores <grupos_utilizadores.html>`__, criar o grupo de utilizadores **GrupoA**, cujas permissões sobre os níveis ficam, por `omissão <permissoes.html#grupos_ou_utilizadores_novos>`__, com o valor *Não* implícito. 

2. Para atribuir as permissões no painel de `Permissões pelo Plano de Classificação <permissoes.html#permissoes_plano>`__, de forma que o grupo  __só tenha acesso de leitura e navegação em todos os níveis__ arquivísticos:

|image6|

      - Selecionar a entidade produtora de topo da estrutura orgânica e de seguida o grupo **GrupoA**, na área de contexto. 
      - No ''Filtro'' selecionar a opção ''Todos'', para se poderem mostrar todos os níveis da estrutura arquivística. Se houver muitas páginas na ''Lista de Permissões Atribuídas'', ou se volta a aumentar o número de elementos por página na `Configuração global <configuracao_global.html>`__ para se poder reduzir o número de páginas ou se atribui __permissões explícitas somente aos níveis produtores e aos documentais de topo__, uma vez que os restantes níveis documentais herdam as permissões dos níveis documentais de topo (ver `Regra 3 <permissoes.html#regra_3>`__ do cálculo de permissões). Seguindo esta última via, é aconselhável usar-se os vários filtros para o conseguir de uma forma rápida. Assim, para se selecionar os níveis produtores, usa-se o filtro ''Todos'' e como a lista aparece ordenada por tipo de nível, basta selecionar os primeiros níveis da lista até encontrar o primeiro nível documental. Para se selecionar os níveis documentais de topo, usa-se o filtro ''Todos documentais'' (para mais detalhe ver `Permissões pelo Plano de Classificação <permissoes.html#permissoes_plano>`__). 
      - Na ''Lista de Permissões Atribuídas'', para cada página, selecionar todos níveis pretendidos ao mesmo tempo e na janela de edição múltipla (para a abrir, clicar com lado direito do rato), atribuir **Sim** explícito nas operações ''Ler'' e ''Expandir''.

|image7|

3. Neste momento, ainda falta criar o outro grupo de utilizadores, **GrupoB**,  na área `Grupo de utilizadores <grupos_utilizadores.html>`__, cujas permissões sobre os níveis têm o `valor por omissão <permissoes.html#grupos_ou_utilizadores_novos>`__ *Não*. 

4. Para que o **GrupoB** tenha acesso total à série *Obras Municipais* e a todos os seus documentos, no painel de `Permissões pelo Plano de Classificação <permissoes.html#permissoes_plano>`__, selecionar a série *Obras Municipais* e o **GrupoB**. No ''Filtro'', selecionar a opção ''Próprio'' e atribuir Sim a todas as operações do nível selecionado.

|image8|

5. Na área de `Utilizadores <utilizadores.html>`__, criar os utilizadores, escolhendo a opção ''Acesso apenas a informação publicada'' para que, por `omissão <permissoes.html#permissoes_omissao\>`__, todos os níveis publicados tenham o valor *Sim* na operação ''Ler'' e *Não* em todos os outros casos. Por fim, associar os utilizadores ao **GrupoA** e ao **GrupoB**, ativando o `cálculo de permissões <permissoes.html#permissoes_calculo>`__. 

Em __conclusão__, depois dos cálculos, as permissões de __cada um destes utilizadores__ sobre os níveis:

-  Na série *Obras municipais*, têm o valor *Sim* implícito em todas as operações, por aplicação da `Regra 2.1 <permissoes.html#regra_2>`__ e da `Regra 5 <permissoes.html#regra_5>`__ do cálculo de permissões.

|image9|

-  Nos __documentos subjacentes__ à série *Obras municipais*, assumem o valor *Sim* em todas as operações, por aplicação da `Regra 3 <permissoes.html#regra_3>`__ do cálculo de permissões.
 
-  Nos restantes níveis, por aplicação da `Regra 2.2 <permissoes.html#regra_2>`__ e `Regra 5 <permissoes.html#regra_5>`__ do cálculo de permissões, nas operações ''Ler'' e ''Expandir'', assumem o valor *Sim*, e nas operações ''Criar'', ''Escrever'' e ''Apagar'', mantêm o valor *Não* por se aplicar a `Regra 4 <permissoes.html#regra_4>`__.

|image10|

#### Exemplo 3

Este exemplo ilustra um caso em que existe um grupo de utilizadores internos com __acesso total, exceto a determinada informação produzida por um departamento que deve ser interditada, por ser confidencial__.

1. Primeiro, na área de `Grupo de utilizadores <grupos_utilizadores.html>`__, criar o **GrupoC**, cujas permissões sobre os níveis assumem, por `omissão <permissoes.html#grupos_ou_utilizadores_novos\>`__, o valor *Não*. 

2. Na área `Permissões pelo Plano de Classificação <permissoes.html#permissoes_plano>`__:
  - Selecionar a entidade produtora, *Polícia*, na área de contexto, cujos documentos são de acesso restrito.
  - Selecionar o **GrupoC** na área de contexto, onde definir as permissões.
  - No ''Filtro'' selecionar a opção ''Todos documentais'', para listar as permissões dos níveis documentais diretamente relacionados com essa entidade produtora (ou com outras subjacentes a esse, caso existissem).
  - Na ''Lista de Permissões Atribuídas'', selecionar os níveis todos aos quais se retirar explicitamente a permissão a todas as operações, colocando **Não** (explícito) em todas as operações.

|image11|

3. Na área de `Utilizadores <utilizadores.html>`__, criar os utilizadores, escolhendo a opção ''Acesso a toda a informação''. As permissões destes utilizadores sobre os níveis assumem o valor *Sim*. 

4. Por fim, associar os utilizadores ao grupo **GrupoC**, desencandeando a aplicação da `Regra 2.1 <permissoes.html#regra_2>`__ do cálculo de permissões. 

Em __conclusão__, as permissões de um destes utilizadores, por exemplo o **UtilizadorC**:

-  Nos níveis documentais, diretamente ou indiretamente, debaixo da entidade produtora *Polícia* assumem o valor *Não* em todas as operações, por aplicação da `Regra 2.1 <permissoes.html#regra_2>`__ do cálculo de permissões.

|image12|

-  Os restantes níveis, mantêm o valor *Sim*, por ser aplicada a `Regra 4 <permissoes.html#regra_4>`__.

|image13|

### Exemplo de atribuição de permissões a objetos digitais

Nesta secção usa-se como exemplo a definição de um determinado grupo de utilizadores poder visualizar todos os níveis de descrição subjacentes a uma dada série, mas não poder visualizar os objetos digitais associados.

Assim, na área de `Permissões por Objeto Digital <permissoes.html#permissoes_od>`__:

|image14|

1. Seleciona-se a série *Ephemera*, debaixo da qual se pretende definir as permissões dos objetos digitais.

2. Seleciona-se o utilizador ou grupo de utilizadores, neste caso o grupo **Leitores**.

3. Selecionam-se todos os objetos digitais e clica-se com o botão direito do rato.

|image15|

4. Atribui-se **Não** para ''Ler'' e para ''Escrever''. Passando a ser o seguinte:

|image16|

Qualquer utilizador, sem permissões explícitas para esses objetos digitais, ao ser associado ao grupo **Leitores** passa a não poder visualizar os objetos digitais debaixo da série *Ephemera*. 

Por exemplo, o utilizador **fatima** cujas permissões por omissão são de ''Acesso a toda a informação'', tem as seguintes permissões sobre os objetos digitais deste exemplo:

|image17|

Se este utilizador for adicionado ao grupo **Leitores** passa a herdar, pela `Regra 2.1 <permissoes.html#regra_2>`__ do cálculo de permissões, as permissões deste grupo relativamente aos objetos digitais deste exemplo:

|image18|


.. |image0| image:: _static/images/permissoesmodulogrupo.png
   :width: 650px
.. |image1| image:: _static/images/atribuirgrupoautilizador.png
   :width: 650px
.. |image2| image:: _static/images/permissoesmoduloutilizador.png
   :width: 650px
.. |image3| image:: _static/images/permissoesmodulogrupo2.png
   :width: 650px
.. |image4| image:: _static/images/permissoesmoduloutilizador2.png
   :width: 650px
.. |image5| image:: _static/images/exemplo1.png
   :width: 650px
.. |image6| image:: _static/images/exemplo2_1.png
   :width: 650px
.. |image7| image:: _static/images/exemplo2_2.png
   :width: 650px
.. |image8| image:: _static/images/exemplo2_3.png
   :width: 650px
.. |image9| image:: _static/images/exemplo2_4.png
   :width: 650px
.. |image10| image:: _static/images/exemplo2_5.png
   :width: 650px
.. |image11| image:: _static/images/exemplo3_1.png
   :width: 650px
.. |image12| image:: _static/images/exemplo3_2.png
   :width: 650px
.. |image13| image:: _static/images/exemplo3_3.png
   :width: 650px
.. |image14| image:: _static/images/exemplopermissoesod1.png
   :width: 650px
.. |image15| image:: _static/images/exemplopermissoesod2.png
   :width: 650px
.. |image16| image:: _static/images/exemplopermissoesod5.png
   :width: 650px
.. |image17| image:: _static/images/exemplopermissoesod4.png
   :width: 650px
.. |image18| image:: _static/images/exemplopermissoesod3.png
   :width: 650px

   