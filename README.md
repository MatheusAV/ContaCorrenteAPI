# ContaCorrenteAPI

![Badge](https://img.shields.io/badge/Status-Em%20Desenvolvimento-blue)
![GitHub issues](https://img.shields.io/github/issues/MatheusAV/ContaCorrenteAPI)
![GitHub stars](https://img.shields.io/github/stars/MatheusAV/ContaCorrenteAPI)
![GitHub forks](https://img.shields.io/github/forks/MatheusAV/ContaCorrenteAPI)

## 📝 Descrição

A **ContaCorrenteAPI** é uma API RESTful desenvolvida para gerenciar contas correntes de forma segura e eficiente, permitindo operações de movimentação (crédito/débito) e consulta de saldo com suporte à idempotência.

## 🚀 Funcionalidades

- **Movimentação de Conta**: Permite operações de crédito e débito em contas correntes.
- **Consulta de Saldo**: Retorna o saldo atual da conta, incluindo nome do titular e data/hora da consulta.
- **Idempotência**: Garante que operações duplicadas não sejam processadas novamente, garantindo a integridade dos dados.

## 📋 Pré-requisitos

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQLite](https://www.sqlite.org/download.html)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)

## 🛠️ Configuração do Projeto

1. Clone o repositório:

   ```bash
   git clone https://github.com/MatheusAV/ContaCorrenteAPI.git
   cd ContaCorrenteAPI

2. Instale as dependências do projeto:
    ```bash
   dotnet restore

4. Configure o banco de dados SQLite. O banco será inicializado automaticamente ao executar o projeto, mas certifique-se de que o arquivo database.sqlite está acessível no diretório Questao5.
5. Execute a aplicação:
    ```bash
   dotnet run --project Questao5

7. Acesse a documentação Swagger em http://localhost:7140/swagger.

## 📂 Estrutura do Projeto

```plaintext
ContaCorrenteAPI/
├── API.Tests/                # Testes unitários para Handlers
├── Questao5/
│   ├── Application/           # Camada de aplicação com comandos, consultas e handlers
│   ├── Controllers/           # Controllers da API
│   ├── Domain/                # Entidades e enums do domínio
│   ├── Infrastructure/        # Configuração do banco de dados e repositórios
│   ├── Program.cs             # Configuração principal da API
│   ├── appsettings.json       # Configurações da aplicação
│   └── database.sqlite        # Banco de dados SQLite
└── README.md                  # Documentação do projeto
```

## 🧩 Endpoints

### 1. Movimentação de Conta
- **Endpoint**: `POST /api/contacorrente/movimentacao`
- **Descrição**: Realiza uma movimentação na conta corrente (crédito ou débito).
- **Body**:
    ```json
    {
      "IdRequisicao": "unique-request-id",
      "IdContaCorrente": "account-id",
      "Valor": 150.75,
      "TipoMovimento": "C"
    }
    ```
- **Respostas**:
  - `200 OK`: Movimentação realizada com sucesso, retorna o `IdMovimento`.
  - `400 Bad Request`: Erro de validação, com descrição do erro.

### 2. Consulta de Saldo
- **Endpoint**: `GET /api/contacorrente/saldo/{idContaCorrente}`
- **Descrição**: Retorna o saldo atual da conta corrente.
- **Parâmetros**:
  - `idContaCorrente` (string): ID da conta a ser consultada.
- **Respostas**:
  - `200 OK`: Retorna saldo, número da conta e nome do titular.
  - `400 Bad Request`: Conta inexistente ou inativa.

---

## 🧪 Testes
Os testes unitários para os handlers `MovimentacaoHandler` e `SaldoHandler` estão localizados em `API.Tests/Handlers`.

Para executar os testes, use o comando:

```bash
dotnet test
```
---
## 📝 Tecnologias Utilizadas

- .NET 6.0 - Framework principal
- SQLite - Banco de dados leve para armazenar as contas e movimentações
- Dapper - Biblioteca ORM para mapeamento de dados
- MediatR - Implementação do padrão CQRS (Command Query Responsibility Segregation)
- Swagger - Documentação interativa da API
- NUnit/xUnit - Frameworks de teste para .NET
