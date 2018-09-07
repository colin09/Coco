using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace jiu.CreateDS.executer
{

    public class CreateDapper:iExecuter
    {

        private string module_path = "";

        private string[] usingC =new string[]{
            "using System;",
            "using System.Collections.Generic;"
        };
        
        //public void Execute(string nameSpace ,string modelName){
        public void Execute(string[] args){
            string nameSpace = args[0];
            string modelName = args[1];

            module_path = $"{System.Environment.CurrentDirectory}\\output\\dapper\\{modelName}Module";

            if(!File.Exists(module_path))
                Directory.CreateDirectory(module_path);
            else{
                DirectoryInfo dir = new DirectoryInfo(module_path);
                dir.Delete(true);
            }

            CreateInterFace(nameSpace,modelName);
            CreateRepository(nameSpace,modelName);
            CreateSqlResource(nameSpace,modelName);
            CreateAutofac(nameSpace,modelName);
        }



        public void CreateInterFace(string nameSpace ,string modelName){
            var iQuery = $"I{modelName}DapperQueryRepository";
            var iRepository = $"I{modelName}DapperRepository";

            var context = new ArrayList();
            context.AddRange(usingC);
            
            context.Add($"namespace {nameSpace}.{modelName}Module {{");
            context.Add($"public interface {iRepository} : IDapperRepository<{modelName}> {{");
            context.Add("}");
            context.Add("}");

            WriteFile($"{module_path}\\{iRepository}.cs",context);

            context.Clear();
            context.AddRange(usingC);
            
            context.Add($"namespace {nameSpace}.{modelName}Module {{");
            context.Add($"public interface {iQuery} : {iRepository} {{");
            context.Add("}");
            context.Add("}");

            WriteFile($"{module_path}\\{iQuery}.cs",context);
        }

        public void CreateRepository(string nameSpace ,string modelName){
            var iQuery = $"I{modelName}DapperQueryRepository";
            var iRepository = $"I{modelName}DapperRepository";

            var sql = $"{modelName}SqlResource";
            var repository = $"{modelName}DapperRepository";

            var context = new ArrayList();
                context.AddRange(usingC);
                context.Add ("using LDTech.ERP.Domain.Context;");
            
            context.Add($"namespace {nameSpace}.{modelName}Module {{");
            context.Add($"public class {repository} : DapperRepositoryBase<{modelName}>,{iRepository},{iQuery} {{");
            
            context.Add($"private {sql} sqlResource = new {sql}();");
            context.Add($"public {repository}(IERPContext conn):base(conn)  {{}}");
            context.Add("public override string SelectSql { get { return this.sqlResource.Select; } }");
            context.Add("public override string InsertSql { get { return this.sqlResource.Insert; } }");
            context.Add("public override string UpdateSql { get { return this.sqlResource.Update; } }");
            context.Add("public override string DeleteSql { get { return this.sqlResource.Delete; } }");
            
            context.Add("}");
            context.Add("}");

            WriteFile($"{module_path}\\{repository}.cs",context);
        }

        public void CreateSqlResource(string nameSpace ,string modelName){            
            var sql = $"{modelName}SqlResource";

            var context = new ArrayList();
            context.AddRange(usingC);
            
            context.Add($"namespace {nameSpace}.{modelName}Module {{");
            context.Add($"public class {sql} : SqlResourceBase {{");
            
            context.Add($"public override string FileName {{get {{ return \"{sql}\"; }} }}");
            context.Add("public string Select { get { return GetSql(\"Select\"); } }");
            context.Add("public string Insert { get { return GetSql(\"Insert\"); } }");
            context.Add("public string Update { get { return GetSql(\"Update\"); } }");
            context.Add("public string Delete { get { return GetSql(\"Delete\"); } }");

            context.Add("}");
            context.Add("}");

            WriteFile($"{module_path}\\{sql}.cs",context);


            context.Clear();
            
            context.Add("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            context.Add($"<{sql}>");
            context.Add("<Insert></Insert>");
            context.Add("<Update></Update>");
            context.Add("<Delete></Delete>");
            context.Add("<Select></Select>");
            context.Add($"</{sql}>");
            WriteFile($"{module_path}\\{sql}.xml",context);
        }

        public void CreateAutofac(string nameSpace ,string modelName){
            var iQuery = $"I{modelName}DapperQueryRepository";
            var iRepository = $"I{modelName}DapperRepository";
            var repository = $"{modelName}DapperRepository";

            var autofac = $"{modelName}AutofacModule";

            var context = new ArrayList();            
            context.Add("using Autofac;");
            context.Add ("using LDTech.ERP.Domain.Context;");
            context.AddRange(usingC);
            
            context.Add($"namespace {nameSpace}.{modelName}Module {{");
            context.Add($"public class {autofac} :  Autofac.Module {{");

            context.Add("protected override void Load(Autofac.ContainerBuilder builder) {");
            context.Add($"builder.RegisterType<{repository}>().As<{iRepository}>();");
            context.Add($"builder.Register<{repository}>");
            context.Add($"((c)=>{{return new {repository}(c.Resolve<IERPSlaveContext>());}}).As<{iQuery}>();");
            //context.Add("");
            context.Add("}");
            
            context.Add("}");
            context.Add("}");

            WriteFile($"{module_path}\\{autofac}.cs",context);
        }



        public void WriteFile(string path, ArrayList context){
            
            using( var write = new StreamWriter(path, true, Encoding.UTF8)){

                foreach (var line in context)
                {
                    write.WriteLine(line);
                }
            }
         
        }


    }
    
}