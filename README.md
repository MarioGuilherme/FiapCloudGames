# FiapCloudGames

## 📌 Objetivos
Desenvolver uma aplicação fazendo a sua evolução contínua durante as fases das aulas da Pós Graduação, servindo como pilar para prática das aulas e servindo como entrega do Tech Challenge. Este projeto visa a ideia inicial ndaa criação de uma plataforma de jogos com promoções e usuários. Tendo as funcionalidades iniciais implementadas de cadastrar, editar, excluir e visualizar os usuários juntamente com autenticação JWT.

## 🚀 Instruções de uso
Faça o clone do projeto e já acesse a pasta do projeto clonado:
```
git clone https://github.com/MarioGuilherme/FiapCloudGames && cd .\FiapCloudGames
```

### ▶️ Iniciar Projeto
  1 - Navegue até o diretório da camada API da aplicação:
  ```
  cd .\FiapCloudGames.API\
  ```
  2 - Insira o comando de execução do projeto:
  
  _(O BANCO DE DADOS É CRIADO AUTOMATICAMENTE QUANDO O PROJETO É INICIADO, SEM PRECISAR EXECUTAR O ```Database-Update```)_:
  ```
  docker-compose up
  ```
  3 - Acesse https://localhost:8081/swagger/index.html

### 🧪 Executar testes
  1 - Navegue até o diretório dos testes:
  ```
  cd .\FiapCloudGames.Tests\
  ```
  2 - E insira o comando de execução de testes:
  ```
  dotnet test
  ```

## 🛠️ Tecnologias e Afins
- .NET 8 com C# 13;
- ASP.NET Core;
- Uso de Middlewares e IActionFilters;
- EntityFrameworkCore;
- SQL SERVER;
- FluentValidation;
- Swagger;
- xUnit junto com Moq;
- Autenticação JWT;
- Segurança de Criptografia com BCrypt;
