using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace jiu.CreateDS.executer {

    public class CreateService : iExecuter {

        private string module_path = "";

        private string[] usingC = new string[] {
            "using System;",
            "using System.Collections.Generic;"
        };

        //public void Execute(string nameSpace ,string modelName){
        public void Execute (string[] args) {
            string nameSpace = args[0];
            string modelName = args[1];

            module_path = $"{System.Environment.CurrentDirectory}\\output\\service\\{modelName}Module";

            if (!File.Exists (module_path))
                Directory.CreateDirectory (module_path);
            else {
                DirectoryInfo dir = new DirectoryInfo (module_path);
                dir.Delete (true);
            }

            CreateInterFace (nameSpace, modelName);
            CreateServices (nameSpace, modelName);
            CreateSearch (nameSpace, modelName);
        }

        public void CreateInterFace (string nameSpace, string modelName) {
            var iQuery = $"I{modelName}QueryService";
            var iService = $"I{modelName}Service";

            var context = new ArrayList ();
            context.AddRange (usingC);

            context.Add ($"namespace {nameSpace}.{modelName}Module {{");
            context.Add ($"public interface {iService}  {{");
            context.Add ("}");
            context.Add ("}");

            WriteFile ($"{module_path}\\{iService}.cs", context);

            context.Clear ();
            context.AddRange (usingC);

            context.Add ($"namespace {nameSpace}.{modelName}Module {{");
            context.Add ($"public interface {iQuery} {{");
            context.Add ("}");
            context.Add ("}");

            WriteFile ($"{module_path}\\{iQuery}.cs", context);
        }

        public void CreateServices (string nameSpace, string modelName) {
            var iQuery = $"I{modelName}QueryService";
            var iService = $"I{modelName}Service";

            var iQueyrRepository = $"I{modelName}DapperQueryRepository";
            var iRepository = $"I{modelName}DapperRepository";

            var sql = $"{modelName}SqlResource";
            var query = $"{modelName}QueryService";
            var service = $"{modelName}Service";

            var context = new ArrayList ();
            context.AddRange (usingC);

            context.Add ($"namespace {nameSpace}.{modelName}Module {{");
            context.Add ($"public class {query} :{iQuery} {{");

            context.Add ($"private {iQueyrRepository} _repository;");
            context.Add ($"public {query}({iQueyrRepository} repository) {{");

            context.Add ("_repository=repository;");

            context.Add ("       }");
            context.Add ("   }");
            context.Add ("}");

            WriteFile ($"{module_path}\\{query}.cs", context);

            context.Clear ();
            context.AddRange (usingC);

            context.Add ($"namespace {nameSpace}.{modelName}Module {{");
            context.Add ($"public class {service} :{iService} {{");

            context.Add ("private IUnitOfWork _uow;");
            context.Add ($"private {iRepository} _repository;");
            context.Add ($"public {service}(IUnitOfWork uow) {{");

            context.Add ("_uow = uow;");
            context.Add ($"_repository = _uow.GetRepository<{iRepository}>();");

            context.Add ("       }");
            context.Add ("   }");
            context.Add ("}");

            WriteFile ($"{module_path}\\{service}.cs", context);

        }

        public void CreateSearch (string nameSpace, string modelName) {
            var search = $"{modelName}Search";
            var mapper = $"{modelName}AutoMapperConfig";

            var context = new ArrayList ();
            context.AddRange (usingC);

            context.Add ($"namespace {nameSpace}.{modelName}Module {{");
            context.Add ($"public class {search} : ISearch {{");
            context.Add ("public Query CreateQuery(){");
            context.Add ("var query = Query.Create();");
            context.Add ("");
            context.Add ("return query;");
            context.Add ("       }");
            context.Add ("   }");
            context.Add ("}");

            WriteFile ($"{module_path}\\{search}.cs", context);

            context.Clear ();
            context.Add ("using AutoMapper;");
            context.AddRange (usingC);
            context.Add ($"namespace {nameSpace}.{modelName}Module {{");
            context.Add ($"public class {mapper} : IAutoMapperConfig {{");
            context.Add ("public void Map(IMapperConfigurationExpression config) {");
            context.Add ("");
            context.Add ("       }");
            context.Add ("   }");
            context.Add ("}");

            WriteFile ($"{module_path}\\{mapper}.cs", context);
        }

        public void CreateAutofac (string nameSpace, string modelName) {
            var iQuery = $"I{modelName}DapperQueryRepository";
            var iRepository = $"I{modelName}DapperRepository";
            var repository = $"{modelName}DapperRepository";

            var autofac = $"{modelName}AutofacModule";

            var context = new ArrayList ();
            context.Add ("using Autofac;");
            context.Add ("using LDTech.ERP.Common.AutoMappers;");
            context.AddRange (usingC);

            context.Add ($"namespace {nameSpace}.{modelName}Module {{");
            context.Add ($"public class {autofac} :  Autofac.Module {{");

            context.Add ("protected override void Load(Autofac.ContainerBuilder builder) {");
            context.Add ($"builder.RegisterType<{repository}>().As<{iRepository}>();");
            context.Add ($"builder.Register<{repository}>");
            context.Add ($"((c)=>{{return new {repository}(c.Resolve<IERPSlaveContext>());}}).As<{iQuery}>();");
            //context.Add("");
            context.Add ("}");

            context.Add ("}");
            context.Add ("}");

            WriteFile ($"{module_path}\\{autofac}.cs", context);
        }

        public void WriteFile (string path, ArrayList context) {

            using (var write = new StreamWriter (path, true, Encoding.UTF8)) {

                foreach (var line in context) {
                    write.WriteLine (line);
                }
            }

        }

    }

}