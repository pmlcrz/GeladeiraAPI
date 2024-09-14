# Projeto: Arquitetura SOLID do Sistema - CodeRDIversity

Este projeto tem como objetivo aplicar os princípios de SOLID na arquitetura do nosso sistema, utilizando camadas de repositório e serviço para implementar as funcionalidades de CRUD e validação. Também foi configurado o gerenciamento de banco de dados utilizando o Entity Framework e o ADO.NET.

## Estrutura do Projeto

### 1. Camada de Repositório
Foi criada uma interface `IRepository` contendo os principais métodos de CRUD, e essa interface foi implementada na classe `Repository.cs`.

- **Métodos implementados:**
  - Create
  - Read
  - Update
  - Delete

### 2. Camada de Serviço
Em seguida, foi criada uma interface chamada `IService`, contendo os principais métodos de serviços e validação. Essa interface foi implementada na classe `Service.cs`.

- **Métodos implementados:**
  - Validação de entradas
  - Lógica de negócio específica para a aplicação

### 3. Injeção de Dependência
Foi realizada a injeção de dependência da `Service`, `DbContext`, e `Repository` na `Program.cs`, de modo a garantir que as dependências sejam gerenciadas pelo contêiner de injeção do ASP.NET Core.

### 4. Modificação do Controller
A interface `IService` foi chamada no construtor do `Controller.cs`, permitindo que o controlador faça uso dos métodos de serviço e validação.

### 5. Validação de Regras Implementadas
Testamos e validamos todas as regras implementadas nas primeiras aulas, como:
- Validação de posição vazia
- Lógica para esvaziar o container

### 6. Testes
Todos os métodos foram testados e validados antes de serem enviados para garantir que o sistema esteja funcionando conforme o esperado.

---

## Configuração e Migrações

### Entity Framework

Este projeto utiliza o Entity Framework para o gerenciamento de banco de dados.

#### Criando a migração inicial

1. Para criar uma migração inicial, execute o seguinte comando no Package Manager Console:
    ```bash
    Add-Migration Inicial
    ```

2. Para aplicar as migrações e atualizar o banco de dados:
    ```bash
    Update-Database
    ```

### ADO.NET

Além do Entity Framework, o projeto também utiliza ADO.NET para interações diretas com o banco de dados, utilizando `SqlConnection` e `SqlCommand` para executar comandos SQL.


Aluna: Pamela Cruz
