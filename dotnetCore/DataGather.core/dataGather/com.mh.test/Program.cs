using System;
using System.Text;
using System.Linq;
//using ExcelDataReader;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using com.mh.test.ioc;
using com.mh.common.Logger;
using com.mh.common.extension;
using com.mh.common.configuration;
using com.mh.mongo.iservice;
using com.mh.mysql;
using com.mh.mysql.factory;
using com.mh.mysql.repository.dbSession;
using com.mh.model.mysql.entity;
using System.IO;
using System.Collections.Generic;


using OfficeOpenXml;
using System.Data;
using com.mh.model.enums;

namespace com.mh.test
{
    class Program
    {
        static void Main(string[] args)
        {
            //DIBootStrapper.Init();

            var date = new DateTime(2017, 9, 11);
            Console.WriteLine(date.AddHours(1).ToOADate());

            logTest();
        }





        static void logTest()
        {
            var log = LoggerManager.Current();
            log.Info("log.info--------------------------------------------");
        }


        static void AppSettingTest()
        {
            var setting = ConfigManager.TestJson;
            System.Console.WriteLine(setting);

            var log = LoggerManager.Current();
            log.Info(setting);
        }

        static void MongoTest()
        {

            // System.Console.WriteLine(ConfigManager.MongoDBConnectionString);
            // System.Console.WriteLine(ConfigManager.MongoDbDataBase);
            // System.Console.WriteLine(ConfigManager.MongoRankAndKittyDataBase);
            // System.Console.WriteLine(ConfigManager.MongoMagicalHorseStatDataBase);
            // System.Console.WriteLine(ConfigManager.MongoThirdSourceDataBase);

            var _pluginDataService = DIBootStrapper.Container.GetRequiredService<IPluginDataService>();
            var rules = _pluginDataService.GetStoreDataRules();

            System.Console.WriteLine(rules.ToJson());
        }


        static void MySqlTest()
        {
            using (var context = new HuiTestContext())
            {
                context.Database.EnsureCreated();

                // Adds a publisher
                var publisher = new Publisher
                {
                    Name = "Mariner Books"
                };
                context.Publisher.Add(publisher);

                // Adds some books
                context.Book.Add(new Book
                {
                    ISBN = "978-05440" + DateTime.Now.ToOADate(),
                    Title = "The Lord of the Rings",
                    Author = "J.R.R. Tolkien",
                    Language = "English",
                    Pages = 1216,
                    Publisher = publisher
                });
                context.Book.Add(new Book
                {
                    ISBN = "978-05472" + (DateTime.Now.ToOADate() + 1),
                    Title = "The Sealed Letter",
                    Author = "Emma Donoghue",
                    Language = "English",
                    Pages = 416,
                    Publisher = publisher
                });

                // Saves changes
                context.SaveChanges();
            }

            PrintData();
        }
        static void PrintData()
        {
            // Gets and prints all books in database
            using (var context = new HuiTestContext())
            {
                var books = context.Book.Include(p => p.Publisher);
                foreach (var book in books)
                {
                    var data = new StringBuilder();
                    data.AppendLine($"ISBN: {book.ISBN}");
                    data.AppendLine($"Title: {book.Title}");
                    data.AppendLine($"Publisher: {book.Publisher.Name}");
                    Console.WriteLine(data.ToString());
                }
            }
        }

        static void EfTest()
        {
            var session = DbSessionFactory.GetCurrentDbSession("MagicHorse") as MagicHorseSession;
            var list = session.StoreRepository.Where(s => s.Id > 100 && s.Id < 120).ToList();
            System.Console.WriteLine(list.ToJson());
                        
            var startDate = new DateTime(2016, 1, 1);
            var stores = new List<int>{189};
            var sections = session.SectionRepository.Where(s => s.Status != DataStatus.Deleted && s.CreateDate >= startDate && stores.Contains(s.StoreId.Value));
            System.Console.WriteLine(sections.ToList().ToJson());
        }


        static void LinqToExcel()
        {
            var filePath = @"D:\Users\Joy10\Desktop\sijiatu-2017-9-4-3.xlsx";

            // var excel = new ExcelQueryFactory(filePath);

            // var data = from c in excel.Worksheet<dynamic>(0) select c;
            // var list = data.ToList();

            // System.Console.WriteLine(list.ToJson());
        }

        /*
        static void TestExcelDataReader()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var filePath = @"D:\Users\Joy10\Desktop\sijiatu-2017-9-4-3.xlsx";
            var csvPath = @"D:\Users\Joy10\Desktop\146_order_list_20170909.csv";
            var xlsPath = @"D:\Users\Joy10\Desktop\146_order_list_20170909.xls";
            using (var stream = File.Open(xlsPath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
                {
                    FallbackEncoding = Encoding.UTF8
                }))
                //using (var reader = ExcelReaderFactory.CreateBinaryReader(stream))
                {
                    //reader.IsFirstRowAsColumnNames = True;


                    // Choose one of either 1 or 2:
                    /*
                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());
                    * /

                    // 2. Use the AsDataSet extension method
                    var result = reader.AsDataSet();

                    var table = result.Tables[0];

                    foreach (System.Data.DataRow row in table.Rows)
                    {
                        System.Console.WriteLine("---------------------------------------------");
                        foreach (var item in row.ItemArray)
                        {
                            System.Console.Write(" | ");
                            System.Console.Write(item);
                        }
                        System.Console.WriteLine(" | ");
                    }

                    // The result of each spreadsheet is in result.Tables
                    //System.Console.WriteLine(result.ToJson());
                }
            }
        } */


        static void TestCsvHelper()
        {
            var csvPath = @"D:\Users\Joy10\Desktop\146_order_list_20170909.csv";

            using (var stream = File.Open(csvPath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<CsvClassMap>();
                var list = csv.GetRecords<CsvClass>().ToList();

                foreach (var row in list)
                {
                    System.Console.WriteLine(row.ToJson());
                }

            }
        }




        static void TesEPPlusExcel()
        {
            var filePath = @"D:\Users\Joy10\Desktop\sijiatu-2017-9-4-3.xlsx";
            var file = new FileInfo(filePath);
            if (!file.Exists)
                return;

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[1];

                var rows = sheet.Dimension.Rows;
                var cols = sheet.Dimension.Columns;

                DataTable table = new DataTable();

                for (int row = 1; row < rows + 1; row++)
                {
                    var tRow = table.NewRow();
                    for (int col = 1; col < cols + 1; col++)
                    {
                        var value = sheet.Cells[row, col].Value;
                        if (row == 1)
                        {
                            var tCol = new DataColumn(value.ToString());
                            table.Columns.Add(tCol);
                        }
                        else
                        {
                            tRow[col] = value;
                        }

                        Console.Write($"{sheet.Cells[row, col].Value}\t");
                    }
                    System.Console.WriteLine(" ");
                }
                //Select all cells in column d between 9990 and 10000
                /*
                var query1 = (from cell in sheet.Cells["d:d"]
                              where cell.Value is double && (double)cell.Value >= 9990 && (double)cell.Value <= 10000
                              select cell);

                var query2 = (from cell in sheet.Cells[sheet.Dimension.Address] where cell.Style.Font.Bold select cell);
                */
                var query = (from cell in sheet.Cells["a:d"] select cell);
                var count = 0;
                foreach (var cell in query.ToList())
                {
                    //System.Console.WriteLine($"{count++} --> {cell.Address} , {cell.Text}, {cell.Value}");
                }
            }
        }







    }

    class CsvClass
    {
        public string id { set; get; }
        public string pNo { set; get; }
        public string pName { set; get; }
    }

    sealed class CsvClassMap : ClassMap<CsvClass>
    {
        private Dictionary<string, string> mapping = new Dictionary<string, string>();

        public CsvClassMap()
        {
            mapping.Add("id", "index");
            mapping.Add("pNo", "item_no");

            Map(m => m.id).Name(mapping["id"]);
            Map(m => m.pNo).Name(mapping["pNo"]);
            if (mapping.ContainsKey("pName"))
                Map(m => m.pName).Name(mapping["pName"]);


        }
    }

}
