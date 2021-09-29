# Jogo de Leilao Reverso

Esta aplicação foi desenvolvida por Clever Divino Inácio de Almeida como instrumento de pesquisa para defesa de um mestrado em Engenharia de Produção e Sistemas da PUC Goiás. 
O objetivo geral desta pesquisa é avaliar a atratividade dos modelos mais utilizados de LR através de experimento aplicado por meio de uma dinâmica de jogos empresariais em 
contexto de fornecimento de serviços de logística.

Este jogo foi desenvolvido em Asp.net core C#, utilizou a biblioteca Microsoft SignalR para dotar o software de comportamento em tempo real e utilizou banco de dados MS Sql 
Server 2019. 

Etapas para utilização do software

I) Download e execução do script do banco de dados:
https://github.com/cleveralmeida/LeilaoReverso/blob/0a23f5995db64d6ca65dabbbd609db1691ff646c/LR/leilaoreverso.sql

II) Configurar a string de conexão do banco de dados (DefaultConnection da appsettings.Development) conforme o banco de dados onde executou o script do banco de dados; 

III) Cadastrar jogadores (Comprador e Fornecedores) - primeiro cadastre um comprador e depois cadastre os fornecedores de forma que estejam relacionados ao comprador. 
Referencie os fornecedores no mesmo Curso e Nível de ensino. Segue exemplos abaixo:

![CadastroComprador](https://user-images.githubusercontent.com/10202296/135170870-f64cba3d-faf6-426d-ab46-98e4c4693626.PNG)
![CadastroFornecedor](https://user-images.githubusercontent.com/10202296/135170997-8c2a32e3-cdf7-489a-9ac8-c56f9ef4d49d.PNG)


IV) Etapas de execução do jogo:
  1.	Comprador e fornecedores efetuam login e vão para a página inicial;
  2.	Comprador entra em criar LR, avalia as informações e confirma a LR;
  3.	Fornecedores relacionados ao comprador recebem convites para participarem do LR;
  4.	Comprador é redirecionado para o painel do comprador;
  5.	Fornecedores que receberam convites vão entram em participar de LR, avaliam as informações e decidem se submetem um lance ou não;
  6.	Fornecedor é redirecionado para o painel do fornecedor;
  7.	Fornecedor efetuava novo lance, cancela o lance ou participa novamente de LR;
  8.	Fornecedor e comprador aguardam o término do LR;
  9.	Comprador apura o (s) vencedor (es);
  10.	Comprador e fornecedores são redirecionados para a página de resultados;
  11.	Comprador e fornecedores, a qualquer momento, podem verificar seus resumos;
  12.	Comprador e fornecedores podem avaliavar o jogo após participarem de 4 LRs.
  
  
  Nota: Este software foi indicado como público pelo desenvolvedor e em acordância do orientador e do coordenador do MEPROS PUC Goiás

