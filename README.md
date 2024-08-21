# ScreenSound

O ScreenSound é uma aplicação que gerencia informações sobre músicas e artistas, permitindo aos usuários explorar e avaliar conteúdos musicais.
A base desse projeto foi realizada em algumas formações que realizei na Alura.

Porém após a conclusão da formação, busquei melhorar o projeto aplicando ajustes nos padrões de código e arquitetura.
Implementei conhecimentos que aprendi durante minha trajetória profisional e em outros cursos da até esse projeto final.

### Nota
Este repositório contém arquivos e dados que, em um ambiente de produção, seriam considerados sensíveis. 
Eles foram incluídos intencionalmente para fins didáticos e explicativos, de modo a ilustrar conceitos e práticas. 
Recomenda-se não utilizar esses dados em um ambiente real sem as devidas precauções de segurança.

## Estrutura do Projeto

- ScreenSound.API: Projeto API
- ScreenSound.Data: Projeto de acesso a dados
- ScreenSound.Models: Projeto contendo as entidades do domínio
- ScreenSound.Web: Projeto FrontEnd Blazor
- ScreenSound.Tests: Projeto de Testes xUnit onde estão os testes unitários
- ScreenSound.Tests.Integration: Projeto de Testes xUnit onde estão os testes de integração com o banco de dados MySql e com a API.
- ScreenSound.Tests.Interface: Projeto de Testes xUnit onde estão os testes de interface, criados utilizando Selenium WebDriver.

## Funcionalidades

- Gerenciar artistas
- Gerenciar músicas
- Gerenciar gêneros musicais
- Relacionar músicas com artistas e gêneros
- Classificar Artistas

## Tecnologias Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core 7.0
- MySQL
- Swagger para documentação da API
- Migrations
- Clean Architecture
- Identity
- Blazor WebAssembly

## Pré-requisitos

Antes de começar, certifique-se de ter as seguintes ferramentas instaladas em sua máquina:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)

## Clonando o Repositório

Para clonar o repositório, execute o seguinte comando:

```bash
git clone https://github.com/ricardo-chiues/screen-sound.git
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

Ou utilizando o Visual Studio, selecione como projeto inicializador ScreenSound.API
Abra o gerenciador de pacotes do nuget, selecione em projeto padrão ScreenSound.Data

Utilize o comando:
```
Update-Database
```

## Executando a aplicação

Para executar a API documentada com Swagger utilize o comando:

```
dotnet run --project ScreenSound.API
```

Acesse a porta informada no console em seu navegador adicionando o endpoint do swagger
Ex: https://localhost:7146/Swagger/index.html

Para executar o projeto Web utilize o comando:

```
dotnet run --project ScreenSound.Web
```

Acesse a porta informada no console em seu navegador
Ex: http://localhost:5036

Caso tenha o Visual Studio instalado em seu computador, a maneira mais simples é abrir a solution do projeto e iniciar ambos os projetos configurando 'Vários projetos de inicialização' e selecionando ScreenSound.API e ScreenSound.Web

Para consumir o projeto web primeiro é necessário criar um usuário pela API e realizar o login pelo projeto Web.

## Testes

### Testes Unitários
Os testes unitários estão localizados no projeto ScreenSound.Tests. 
Eles são responsáveis por validar a lógica de negócio das classes e métodos do projeto. 
Para isso, utilizamos o framework xUnit, que oferece uma estrutura simples e poderosa para a criação e execução de testes automatizados.

### Testes de Integração
Os testes de integração estão no projeto ScreenSound.Tests.Integration. 
Esses testes validam a integração entre os diferentes módulos da aplicação, como a interação com o banco de dados MySQL e as requisições à API.

Para os testes de integração com o banco de dados MySQL, é utilizado o Testcontainers.MySql, que permite criar um container MySQL temporário, garantindo que os testes sejam executados em um ambiente controlado e isolado. 
Além disso, há uma configuração específica para testes de integração com a API, utilizando o WebApplicationFactory para criar uma instância da aplicação e realizar as requisições HTTP necessárias.

### Testes de Interface
Os testes de interface estão no projeto ScreenSound.Tests.Interface e foram criados utilizando o Selenium WebDriver. 
Esses testes automatizam a interação com a interface do usuário, verificando o comportamento e a funcionalidade do frontend da aplicação. 
Eles são essenciais para garantir que o usuário final tenha uma experiência consistente e sem erros ao interagir com a aplicação.

### Integração Contínua
Para garantir a qualidade do código, os testes são integrados ao processo de CI/CD, sendo executados automaticamente a cada novo commit ou pull request. 
Isso assegura que as alterações no código não introduzam regressões ou bugs.

## Executando os Testes
1. Clone o repositório e navegue até o diretório do projeto.

2. Certifique-se de que o MySQL esteja configurado corretamente para os testes de integração.

3. Execute os testes unitários com o comando:

```
dotnet test ScreenSound.Tests
```

4. Execute os testes de integração com o comando:

```
dotnet test ScreenSound.Tests.Integration
```

5. Execute os testes de interface com o comando:

```
dotnet test ScreenSound.Tests.Interface
```

## Layout do Projeto
<p align="center">
  <img src="src/assets/to_readme/artistas.png">
  <img src="src/assets/to_readme/login.png">
  <img src="src/assets/to_readme/musicas.png">
  <img src="src/assets/to_readme/cadastro.png">
  <img src="src/assets/to_readme/api.png">
</p>

## Autor
José Ricardo Chies Gonçalves

LinkedIn:
https://www.linkedin.com/in/ricardo-chies-087557216/

E-mail:
chies.dev@gmail.com
