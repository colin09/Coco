Grains  1

Setup
Before you write code to implement a grain class, create a new Class Library project targeting .NET Standard (preferred) or .NET Framework 4.6.1 or higher (if you cannot use .NET Standard due to dependencies). Grain interfaces and grain classes can be defined in the same Class Library project or in two different projects for better separation of interfaces from implementation. In either case, the projects need to reference Microsoft.Orleans.Core.Abstractions and Microsoft.Orleans.CodeGenerator.MSBuild NuGet packages.

在编写实现grain类的代码之前，先创建一个.NET Standard（首选）或.NET Framework 4.6.1或更高版本的新类库项目（如果由于依赖项而无法使用.NET Standard）。 Grain接口和grain类可以在同一个Class Library项目中定义，也可以在两个不同的项目中定义，以便更好地将接口与实现分离。 在任何一种情况下，项目都需要引用Microsoft.Orleans.Core.Abstractions和Microsoft.Orleans.CodeGenerator.MSBuild NuGet包。

For more thorough instructions, see the Project Setup section of Tutorial One – Orleans Basics.

Grain Interfaces and Classes
Grains interact with each other and get called from outside by invoking methods declared as part of the respective grain interfaces. A grain class implements one or more previously declared grain interfaces. All methods of a grain interface must return a Task (for void methods), a Task<T> or a ValueTask<T>(for methods returning values of type T).

Grains通过调用声明的接口实现彼此交互调用和被外部调用。 grain类实现一个或多个先前声明的grain接口。 grain接口的所有方法都必须返回Task（对于void方法），Task <T>或ValueTask <T>（对于返回T类型值的方法）。

The following is an excerpt from the Orleans version 1.5 Presence Service sample:












Orleans 2.0
2.0 is a major release of Orleans with the main goal of making it .NET Standard 2.0 compatible and cross-platform (via .NET Core). As part of that effort, several modernizations of Orleans APIs were made to make it more aligned with how technologies like ASP.NET are configured and hosted today.

Because it is compatible with .NET Standard 2.0, Orleans 2.0 can be used by applications targeting .NET Core or full .NET Framework. The emphasis of testing by the Core team for this release is on full .NET Framework to ensure that existing applications can easily migrate from 1.5 to 2.0, and with full backward compatibility.

The most significant changes in 2.0 are as follows:

Completely moved to programmatic configuration leveraging Dependency Injection with a fluid builder pattern API.
The old API based on configuration objects and XML files is preserved for backward compatibility, but will not move forward and will get deprecated in the future. See more details in the Configuration section.

Explicit programmatic specification of application assemblies that replaces automatic scanning of folders by the Orleans runtime upon silo or client initialization.
Orleans will still automatically find relevant types, such as grain interfaces and classes, serializers, etc. in the specified assemblies, but it will no longer try to load every assembly it can find in the folder. An optional helper method for loading all assemblies in the folder is provided for backward compatibility: IApplicationPartManager.AddFromApplicationBaseDirectory().

See Configuration and Migration sections for more details.

Overhaul of code generation.
While mostly invisible for the developer, code generation became much more robust in handling serialization of various possible types. Special handling is required for F# assemblies. See the Code generation section for more details.

Created a Microsoft.Orleans.Core.Abstractions NuGet package and moved/refactored several types into it.
Grain code would most likely need to reference only these abstractions, whereas the silo host and client will reference more of the Orleans packages. We plan to update this package less frequently.

Add support for Scoped services.
This means that each grain activation gets its own scoped service provider, and Orleans registers a contextual IGrainActivationContext object that can be injected into a Transient or Scoped service to get access to activation specific information and grain activation lifecycle events. This is similar to how ASP.NET Core 2.0 creates a scoped context for each Request, but in the case of Orleans, it applies to the entire lifetime of a grain activation. See Service Lifetimes and Registration Options in the ASP.NET Core documentation for more information about service lifetimes.

Migrated the logging infrastructure to use Microsoft.Extensions.Logging (same abstractions as ASP.NET Core 2.0).

2.0 includes a beta version of support for ACID distributed cross-grain transactions.

The functionality will be ready for prototyping and development, and will graduate for production use sometime after the 2.0 release. See Transactions for more details.






Code Generation in Orleans 2.0

Code generation has been improved in Orleans 2.0, improving startup times and providing a more deterministic, debuggable experience. As with earlier versions, Orleans provides both build-time and run-time code generation.
Orleans 2.0中的代码生成得到了改进，缩短了启动时间并提供了更具确定性，可调试性的体验。与早期版本一样，Orleans提供构建时和运行时代码生成。

During Build - This is the recommended option and only supports C# projects. In this mode, code generation will run every time your project is compiled. A build task is injected into your project's build pipeline and the code is generated in the project's intermediate output directory. To activate this mode, add one of the packages Microsoft.Orleans.CodeGenerator.MSBuild or Microsoft.Orleans.OrleansCodeGenerator.Build to all projects which contain Grains, Grain interfaces, serializers, or types which require serializers. Differences between the packages and additional codegen information could be found in the corresponding Code Generation section. Additional diagnostics can be emitted at build-time by specifying value for OrleansCodeGenLogLevel in the target project's csproj file. For example, <OrleansCodeGenLogLevel>Trace</OrleansCodeGenLogLevel>.
在构建期间 - 这是推荐的选项，仅支持C＃项目。在此模式下，每次编译项目时都会运行代码生成。构建任务将注入到项目的构建管道中，代码将在项目的中间输出目录中生成。要激活此模式，请将Microsoft.Orleans.CodeGenerator.MSBuild或Microsoft.Orleans.OrleansCodeGenerator.Build软件包中的一个添加到包含Grains，Grain接口，序列化程序或需要序列化程序的类型的所有项目中。可以在相应的代码生成部分中找到包和附加代码生成器信息之间的差异。通过在目标项目的csproj文件中指定OrleansCodeGenLogLevel的值，可以在构建时发出其他诊断。例如，<OrleansCodeGenLogLevel> Trace </ OrleansCodeGenLogLevel>。

During Configuration - This is the only supported option for F#, Visual Basic, and other non-C# projects. This mode generates code during the configuration phase. To access this, see the Configuration documentation.
在配置期间 - 这是F＃，Visual Basic和其他非C＃项目唯一受支持的选项。此模式在配置阶段生成代码。要访问它，请参阅配置文档。

Both modes generate the same code, however run-time code generation can only generate code for publicly accessible types.
两种模式都生成相同的代码，但运行时代码生成只能生成可公开访问类型的代码。



