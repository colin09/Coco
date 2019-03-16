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
                
                dotnet run CreateDapper LDTech.ERP.Domain.Dapper.Repositories.HQSettlementModule HQSettlementNote
                dotnet run CreateService LDTech.ERP.Domain.SettingServices.HQSettlementModule HQSettlementNote

                
                dotnet run CreateService LDTech.ERP.Domain.BasicBusinessServices 
                dotnet run CreateService LDTech.ERP.Domain.BusinessServices 
                dotnet run CreateService LDTech.ERP.Domain.Services 
                dotnet run CreateService LDTech.ERP.Domain.SettingServices 
                
                dotnet run CreateDapper LDTech.ERP.Domain.Dapper.Repositories CentralizePurchaseRequisition
                dotnet run CreateService LDTech.ERP.Domain.BusinessServices CentralizePurchaseRequisition


                dotnet run CreateDapper LDTech.ERP.Domain.Dapper.Repositories CityProviderProductSettlement
                dotnet run CreateDapper LDTech.ERP.Domain.Dapper.Repositories CityProviderSettlementNote
                
                dotnet run CreateService LDTech.ERP.Domain.BusinessServices CityProviderProductSettlement
                dotnet run CreateService LDTech.ERP.Domain.BusinessServices CityProviderSettlementNote               
                


                
                dotnet run CreateDapper LDTech.ERP.Domain.Dapper.Repositories PurchaseTask
                dotnet run CreateService LDTech.ERP.Domain.BusinessServices PurchaseTask
            
             */

            var exe = assembly.CreateInstance($"jiu.CreateDS.executer.{key}") as iExecuter;
            exe.Execute(args);
        }



    }
}
