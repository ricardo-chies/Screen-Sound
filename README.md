# ScreenSound API

ScreenSound API é uma aplicação que disponibiliza endpoints para gerenciar artistas e músicas. Esta aplicação foi construída utilizando ASP.NET Core e Entity Framework Core com um banco de dados MySQL.

## Estrutura do Projeto

- ScreenSound: Projeto console
- ScreenSound.API: Projeto API
- ScreenSound.Data: Projeto de acesso a dados
- ScreenSound.Models: Projeto contendo as entidades do domínio

## Funcionalidades

- Gerenciar artistas
- Gerenciar músicas
- Gerenciar gêneros musicais
- Relacionar músicas com artistas e gêneros

## Tecnologias Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core 7.0
- MySQL
- Swagger para documentação da API
- Migrations
- Clean Architecture

## Pré-requisitos

Antes de começar, certifique-se de ter as seguintes ferramentas instaladas em sua máquina:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)

## Clonando o Repositório

Para clonar o repositório, execute o seguinte comando:

```bash
git clone https://github.com/seu-usuario/screen-sound.git
```

## Configurando

1. Navegue até o diretório padrão do projeto

```
cd screen-sound
```

2. Restaure as dependências do projeto

```
dotnet restore
```

3. Configure a string de conexão com o banco de dados MySql no arquivo 'appsettings.json' localizado no diretório 'ScreenSound.API'

4. Crie as migrações e atualize o banco de dados:

```
cd ScreenSound.API
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Executando a aplicação

Para executar a API documentada com Swagger utilize o comando:

```
dotnet run --project ScreenSound.API
```

Para executar o projeto console utilize o comando:

```
dotnet run --project ScreenSound
```

## Autor
José Ricardo Chies Gonçalves

LinkedIn:
https://www.linkedin.com/in/ricardo-chies-087557216/

E-mail:
chies.dev@gmail.com
