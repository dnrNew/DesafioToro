# Desafio Toro Full-Stack

<p align="left">
<img src="http://img.shields.io/static/v1?label=STATUS&message=FINALIZADO&color=GREEN&style=for-the-badge"/>
</p>

Projeto criado para desenvolver a User Story `TORO-004` do Desafio Toro Desenvolvedor Full-Stack.

O projeto simula uma lista de usuários em que cada usuário pode ter uma lista de ativos na bolsa de valores. Um usuário pode visualizar sua lista de ativos e executar uma ordem para comprar uma quantidade de ações, de acordo com uma lista aleatória de 5 ações mais negociadas no dia. Após a compra das ações o saldo e a lista de ativos do usuário é atualizado.

A arquitetura do projeto foi baseada em camadas com o Domínio, regras de negócio desacoplados e API RESTFull.

# Índice
* [Começando](#-começando)
* [Como executar o projeto](#%EF%B8%8F-como-executar-o-projeto)
* [Executando os testes](#%EF%B8%8F-executando-os-testes)

## 🚀 Começando

Clonar o repositório do projeto 
```
git clone https://github.com/dnrNew/DesafioToro.git
```
Instalar as dependências do projeto:

- .NET Core 6 - https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- Node.js versão v16.17.0 - https://nodejs.org/en/download/
- Docker versão 20.10.17 - https://docs.docker.com/desktop/install/windows-install/

Abrir um terminal e digitar os seguinte comandos:

- Instalar o Angular

```
npm install -g @angular/cli  
```

### 🛠️ Como executar o projeto

Para inicializar todos os serviços de uma vez basta utilizar o docker-compose, de dentro da pasta raiz do projeto, com o comando:

```
docker-compose up
```

Para rodar apenas o serviço de Frontend, acesse a pasta **frontend**, com o comando:

```
ng serve
```

Para rodar apenas o serviço de Backend, acesse a pasta **backend**, com os comandos:

```
dotnet restore 
dotnet run
```

## ⚙️ Executando os testes

Foram criados testes de unidade no projeto de Backend que podem ser executados de dentro da pasta **backend**, com o comando:

```
dotnet test 
```
