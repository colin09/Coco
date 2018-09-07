using System;
using System.Reflection;
using jiu.CreateDS.executer;

namespace jiu.CreateDS
{
    class Program
    {

       static Assembly assembly = Assembly.GetExecutingAssembly();

        static void Main(string[] args)
        {
            Console.WriteLine("start...");


            var newArgs = new string[args.Length-1];
            Array.Copy(args,1,newArgs,0,args.Length-1);

            execute(args[0],newArgs);

            System.Console.WriteLine("over!");
        }





        static void execute(string key,string[] args){

            /*
                dotnet run CreateDapper LDTech.ERP.Domain.Dapper.Repositories FDBSetting
                dotnet run CreateService LDTech.ERP.Domain.SettingServices FDBSetting
            
             */

            var exe = assembly.CreateInstance($"jiu.CreateDS.executer.{key}") as iExecuter;
            exe.Execute(args);


        }



    }
}
