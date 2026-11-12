# 📚 Controle de Leitura

Projeto pessoal de gerenciamento e acompanhamento de leituras, desenvolvido com C# e .NET 10. A proposta é organizar livros, registrar o progresso das leituras e acompanhar metas individuais.

## 🎯 Objetivo

Desenvolver uma API REST para centralizar o gerenciamento da vida de leitura, permitindo que cada pessoa mantenha sua biblioteca organizada e acompanhe sua evolução ao longo do tempo.

## ✨ Funcionalidades planejadas

- Cadastro e gerenciamento de livros.
- Organização dos livros em uma estante pessoal.
- Definição e atualização do status de leitura, como **Quero ler**, **Lendo** e **Lido**.
- Registro e acompanhamento do progresso de cada leitura.
- Definição de uma meta anual de leitura.
- Acompanhamento do progresso em relação à meta anual.

> As funcionalidades serão implementadas gradualmente ao longo do desenvolvimento do projeto.

## 🛠️ Tecnologias

- **Linguagem:** C#
- **Plataforma:** .NET 10 / ASP.NET Core
- **Arquitetura:** API REST organizada em camadas
- **Persistência:** Entity Framework Core
- **Documentação da API:** Swagger / OpenAPI
- **Versionamento:** Git e GitHub

## 🏗️ Estrutura da solução

A solução está organizada inicialmente nos seguintes projetos:

```text
SistemaControleLeitura/
├── SistemaControleLeitura.API/
├── SistemaControleLeitura.Application/
├── SistemaControleLeitura.Domain/
└── SistemaControleLeitura.Infrastructure/
```

- **API:** ponto de entrada HTTP, endpoints e configurações da aplicação.
- **Application:** casos de uso, serviços, DTOs e contratos da aplicação.
- **Domain:** entidades, enums e regras centrais do domínio.
- **Infrastructure:** implementações de persistência e integrações externas.

## 🚧 Status do projeto

Em desenvolvimento. A estrutura inicial da solução foi criada e as funcionalidades serão adicionadas por etapas.

## 👩‍💻 Autoria

Projeto pessoal desenvolvido por Bianca Fernandes para praticar e aprofundar conhecimentos em desenvolvimento backend com .NET, APIs REST, persistência de dados e boas práticas de desenvolvimento.
