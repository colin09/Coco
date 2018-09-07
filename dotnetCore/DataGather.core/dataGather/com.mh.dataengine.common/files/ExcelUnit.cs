using System;
using System.IO;
using System.Text;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

using OfficeOpenXml;

using com.mh.model.mongo.dbMh;
using System.Data;

namespace com.mh.dataengine.common.files
{


    public class ExcelUnit
    {

        public List<T> ReadDataToList<T>(string filePath,Dictionary<string, string> mapping) where T : PluginOrderBase , new()
        {
            var file = new FileInfo(filePath);
            if (!file.Exists)
                return null;
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
                
                var convert = new ModelConvert<T>(mapping);
                //table 2 list
                var dataErr = new Dictionary<int, string>();
                var list = convert.ToList(table, out dataErr);
                return list;
            }

            return null;
        }
    }

}



/**

    EPPlus.Core

    Sample8.cs
    Sample13.cs

 */
