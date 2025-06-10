Requisitos:
  - Visual Studio 2022
  - git
    
Como rodar a aplicação:
  1° - Aplicar o comando "git clone https://github.com/josenoe97/ClientesAPI.git"
  
  2° - Acessar o a pasta que deseja compilar (ClientesAPI ou ClientesAPI.Testes)
  
  3° - Aplicar o comando dotnet run
  
  4° - Acessar o localhost com a url do swagger (exemplo: https://localhost:7254/swagger/index.html)
  
  5° - Ou compilando o visual studio de forma manual, não precisando executar as etapas: 2,3,4.

Versão NET 8.0

Pacotes Utilizados(Instalados no NuGET):

- ClientesAPI
  - Entity Framework
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.InMemory
    - Microsoft.EntityFrameworkCore.Relational
    - Microsoft.EntityFrameworkCore.Tools
  - AutoMapper
    - AutoMapper.Extensions.Microsoft.DependencyInjection
   
- ClientesAPI.Teste(xUnit)
  - Microsoft.AspNetCore.Mvc
  - Moq
  - FluentAssertions
 
