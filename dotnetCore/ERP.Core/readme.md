

Packages :

    dotnet add package Microsoft.Extensions.Configuration --version 2.1.1
    dotnet add package Microsoft.Extensions.Configuration.Json --version 2.1.1
 
    dotnet add package System.Data.SqlClient --version 4.5.1  // SqlClient
    
    dotnet add package Dapper --version 1.50.5
    dotnet add package log4net --version 2.0.8
    dotnet add package Newtonsoft.Json --version 11.0.2
    
    dotnet add package Autofac --version 4.8.1
    dotnet add package Autofac.Configuration --version 4.1.0
    dotnet add package Autofac.Extensions.DependencyInjection --version 4.2.2
    
    dotnet add package AutoMapper --version 7.0.1
    dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 5.0.1

    EntityFramework -->
        dotnet add package Microsoft.EntityFrameworkCore --version 2.1.1
        dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 2.1.1

        dotnet add package Toolbelt.EntityFrameworkCore.IndexAttribute --version 1.0.0  // for Data Annotations index



Project 
    [dotnet add reference]

    erp.common
        erp.common\ERP.Common.Infrastructure\ERP.Common.Infrastructure.csproj
        erp.common\ERP.Common.IocRegister\ERP.Common.IocRegister.csproj
        erp.common\ERP.Common.Mappers\ERP.Common.Mappers.csproj

    erp.domain
        erp.domain\ERP.Domain\ERP.Domain.csproj
        erp.domain\ERP.Domain.Enums\ERP.Domain.Enums.csproj
        erp.domain\ERP.Domain.Contract\ERP.Domain.Contract.csproj
        
        erp.domain\ERP.Domain.Repository\ERP.Domain.Repository.csproj
        erp.domain\ERP.Domain.IService\ERP.Domain.IService.csproj

        erp.domain\ERP.Domain.Dapper\ERP.Domain.Dapper.csproj
        erp.domain\ERP.Domain.Services\ERP.Domain.Services.csproj




Micro-Service:
    http://localhost:5006/.well-known/openid-configuration