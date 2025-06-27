<p align="center">
  <img src="wwwroot/img/icone-logotipo.svg" alt="Ícone Cesta Solidária" width="150" align="center"/>
</p>
<h1 align="center">Cesta Solidária</h1>

A **Cesta Solidária** é uma plataforma web desenvolvida para conectar **doadores de alimentos** a **Organizações Não Governamentais (ONGs)** que necessitam de suprimentos para distribuir a pessoas em situação de insegurança alimentar e desnutrição. Este projeto surgiu como uma iniciativa no segundo semestre do curso de Análise e Desenvolvimento de Sistemas na PUC Minas, com o objetivo de facilitar o contato entre doadores de ONGs e otimizar o processo de doação de alimentos, alinhando-se aos Objetivos de Desenvolvimento Sustentável da ONU de combater a fome e reduzir o desperdício de alimentos. Por se tratar de um projeto acadêmico, a aplicação apenas simula de forma fictícia o processo de doação de alimentos.

## Índice 
* [Ferramentas](#ferramentas)
* [Tecnologias](#tecnologias)
* [Funcionalidades](#funcionalidades)
    * [Funcionalidades de autenticação e autorização](#funcionalidades-de-autenticação-e-autorização)
    * [Funcionalidades para doadores](#funcionalidades-para-doadores)
    * [Funcionalidades para ONGs](#funcionalidades-para-ongs)
    * [Funcionalidades de transações de doações](#funcionalidades-de-transações-de-doações)
* [Demonstração da solução](#demonstração-da-solução)
* [Como acessar e utilizar](#como-acessar-e-utilizar)
* [Arquitetura da solução](#arquitetura-da-solução)
    * [Diagrama de Classes](#diagrama-de-classes)
    * [Diagrama Entidade Relacionamento (ER)](#diagrama-entidade-relacionamento-er)
    * [Projeto do Banco de Dados](#projeto-do-banco-de-dados)
* [Identidade visual](#identidade-visual)
* [Contribuidores](#contribuidores)

## Ferramentas

- **GitHub**: plataforma utilizada para armazenar o repositório de código-fonte e de documentação. 
- **GitHub Project**: utilizado como ferramenta para organizar e acompanhar as tarefas do projeto, por meio de um quadro Kanban.
- **Microsoft Teams**: utilizado como ferramenta de comunicação para realização das reuniões em grupo semanais.
- **Figma**: utilizado para o design da interface, prototipagem de telas e construção do template padrão da aplicação.
- **Miro**: utilizado para construção do fluxo do usuário.
- **Lucid**: utilizado para construção dos diagramas da arquitetura da solução.
- **Visual Studio**: IDE utilizada para desenvolvimento da aplicação.

## Tecnologias

- **HTML**: utilizado no front-end para definição da estrutura e conteúdo das telas.
- **CSS**: utilizado no front-end para definição do estilo e responsividade das telas.
- **Bootstrap**: utilizado no front-end para facilitar a estilização e responsividade.
- **JavaScript**: linguagem de programação utilizada para permitir a interatividade, manipulação dinâmica de elementos e validações de dados no lado do cliente.
- **C#**: linguagem de programação para implementar o back-end da aplicação.
- **ASPNET Core MVC 8.0**: framework de desenvolvimento web utilizado.
- **Entity Framework Core**: utilizado como ORM (Object-Relational Mapping).
- **Azure SQL**: banco de dados relacional utilizado no projeto original.*
- - **Provedor de Banco de Dados In-Memory para EF Core**: utilizado para demonstração pública da aplicação.**
- **Azure App Service**: utilizado para implantação da aplicação.
- **Git**: utilizado para versionamento do código.
  
**O Azure SQL foi utilizado com a licença de estudante no Azure, que fornece créditos limitados para uso acadêmico. Como não há mais créditos disponíveis, a versão da aplicação que utiliza a conexão com o Azure SQL não está ativa mais.* 

***O BD In-Memory está sendo utilizado exclusivamente para demonstração da aplicação em portfólio. Essa opção foi escolhida por ser possível de implementar gratuitamente, simular diretamente o uso do EF Core, com fácil integração com o framework utilizado no desenvolvimento e por ser uma forma segura de demonstrar as funcionalidades.* 

## Funcionalidades

### Funcionalidades de autenticação e autorização

* **Cadastro de usuários**: A plataforma permite o cadastro de três tipos de usuário (Doador Pessoa Física, Doador Pessoa Jurídica e ONG)
* **Autenticação**: A plataforma permite que usuários cadastrados façam o login
* **Autorização**: A platforma autoriza recursos específicos para cada tipo de usuário
* **Gerenciamento da conta**: O usuário pode visualizar as informações que cadastrou em uma conta, editá-las e excluir a conta.

### Funcionalidades para doadores

* **Oferecer doações de alimentos**: doadores podem cadastrar ofertas de doação de cestas básicas de alimentos completas (com itens pré-definidos) e de alimentos não perecíveis específicos, que podem ser selecionados em uma lista, incluindo informações para entrega e agendamento da entrega.
* **Gerenciar ofertas de alimentos**: doadores podem visualizar os detalhes de todas as ofertas que cadastraram de acordo com seu status (pendente, agendada, cancelada ou concluídas); e editar e cancelar ofertas que ainda estão pendentes.
* **Visualizar solicitações de ONGs**: doadores podem visualizar a lista e os detalhes de doações solicitadas por ONGs em aberto no momento em seu estado, incluindo as informações para entrega e para contato com a ONG.
* **Agendar a entrega de uma doação solicitada**: doadores podem aceitar solicitações de ONGs e agendar uma data e horário disponibilizado pela ONG para entrega ou retirada da doação.

### Funcionalidades para ONGs

* **Solicitar doações de alimentos**: ONGs podem cadastrar solicitações de doação de cestas básicas de alimentos completas (com itens pré-definidos) e de alimentos não perecíveis específicos, que podem ser selecionados em uma lista, incluindo informações para entrega e agendamento da entrega.
* **Gerenciar solicitações de alimentos**: ONGs podem visualizar os detalhes de todas as solicitações que cadastraram de acordo com seu status (pendente, agendada, cancelada ou concluída); e editar e cancelar solicitações que ainda estão pendentes.
* **Visualizar ofertas de doadores**: ONGs podem visualizar a lista e os detalhes de doações oferecidas por doadores em aberto no momento em seu estado, incluindo as informações para entrega e para contato com o doador.
* **Agendar a entrega de uma doação oferecida**: ONGs podem aceitar ofertas de doadores e agendar uma data e horário disponibilizado pelo doador para entrega ou retirada da doação.

### Funcionalidades de transações de doações

* **Gerenciar agendamentos**: os usuários podem visualizar os detalhes de todos os seus agendamentos (incluindo das doações que cadastrou e das doações que aceitou de outro usuário) de acordo com seu status (próximos, cancelados e concluídos), reagendar o horário, cancelar o agendamento e marcar uma entrega como concluída.
* **Notificar alterações de agendamentos**: a aplicação notifica os usuários quando uma de suas doações cadastradas é aceita e agendada e quando algum de seus agendamentos tem alteração, seja por reagendamento, cancelamento ou conclusão. 

## Demonstração da solução

![mockup-cesta-solidaria](https://github.com/user-attachments/assets/4859ce79-fa6d-448f-82e7-776fb16b109e)

<h4> A demonstração a seguir simula uma interação entre um doador e uma ONG do mesmo estado através da plataforma. </h4>

https://github.com/user-attachments/assets/6d80bbb3-f683-4d92-abe5-089a444997a9

## Como acessar e utilizar

1. **Acesse a plataforma**: [clique aqui](https://cestasolidaria-hvdkhqg4a4enghc3.canadacentral-01.azurewebsites.net/) para acessar.

2. **Cadastre-se (opcional)**: escolha uma das opções de cadastro (Doador Pessoa Física, Doador Pessoa Jurídica e ONG) e crie uma conta com dados fictícios. Você pode utilizar uma conta de teste já existente, se preferir (informações abaixo).

3. **Faça login com a conta cadastrada ou use um dos usuários de teste**:
   
   | Tipo de perfil         | E-mail            | Senha    |
   | ---------------------- | ----------------- | -------- |
   | Doador Pessoa Física   | doadorpf@demo.com | senha123 |
   | Doador Pessoa Jurídica | doadorpj@demo.com | senha123 |
   | ONG                    | ong@demo.com      | senha123 |


4. **Utilize as funcionalidades que desejar**:
- Cadastre uma oferta ou solicitação de doação;
- Visualize, edite ou cancela doações cadastradas por você;
- Visualize as ofertas ou solicitações disponíveis em seu estado para aceite e agendamento;
- Agende a entrega de uma oferta ou solicitação;
- Visualize, reagende ou cancele um agendamento;
- Marque uma doação como concluída;
- Visualize, edite ou exclua a sua conta (As contas de teste têm permissão apenas de leitura e você não poderá alterar informações ou excluir a conta).

> [!WARNING]
> A demonstração pública utiliza o Provedor de Banco de Dados In-Memory para EF Core. Os dados inseridos são armazenados temporariamente apenas na memória do servidor e serão permanentemente perdidos a cada reinicialização da aplicação. Além disso, por ser um projeto acadêmico e uma versão de demonstração, ela não possui os mesmos níveis de segurança de um sistema real. Por isso, NÃO insira informações pessoais sensíveis (como CPF, números de documentos, endereços completos ou senhas reais) e tenha em mente que os dados serão eventualmente perdidos.

## Arquitetura da solução

### Diagrama de Classes

![Diagrama de Classes](https://github.com/user-attachments/assets/69b26f6c-633d-4268-b6ed-d28b0ae014ed)

### Diagrama Entidade Relacionamento (ER)

![Diagrama Entidade Relacionamento](https://github.com/user-attachments/assets/671460dc-d8ab-454c-aa18-b33eb8c3de60)

### Projeto do Banco de Dados

![Projeto da Base de Dados](https://github.com/user-attachments/assets/a7153c4d-263c-495c-ade2-816408be2378)

## Identidade visual

### Logotipo
![Logotipo](https://github.com/user-attachments/assets/c5242353-3cae-4a26-8b20-251dd588e79a)

### Paleta de cores
![Paleta de cores](https://github.com/user-attachments/assets/5798bea0-0f6c-4f4b-a594-b68c3c7a8697)

## Contribuidores
Este projeto foi desenvolvido em grupo como parte das atividades acadêmicas do curso de Análise e Desenvolvimento de Sistemas. Os membros da equipe são:
* Isabela Borges
* Guilherme Moreira Leocadio | [GitHub](https://github.com/le0cadio)
* Harrison Costa Oliveira | [GitHub](https://github.com/harriscosta)
* Michelle Rossi Broch | [GitHub](https://github.com/michellerbroch)

Orientadora:
* Rosilane Ribeiro da Mota

Devido a políticas de segurança da PUC Minas, o repositório utilizado originalmente para documentação detalhada e desenvolvimento da aplicação, incluindo histórico completo de versionamento do código, não pode ser disponibilizado publicamente. 

