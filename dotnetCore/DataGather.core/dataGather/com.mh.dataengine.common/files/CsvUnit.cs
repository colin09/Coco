using System;
using System.IO;
using System.Text;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;

namespace com.mh.dataengine.common.files
{


    public class CsvUnit
    {

        public List<T> ReadDataToList<T, TMap>(string filePath) where T : PluginOrderBase where TMap : ClassMap
        {
            var file = new FileInfo(filePath);
            if (!file.Exists)
                return null;

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<TMap>();
                var list = csv.GetRecords<T>().ToList();

                return list;
            }
        }
    }

}