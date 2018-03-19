using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;
using com.mh.common.Logger;
using com.mh.common.ioc;


namespace com.mh.dataengine.common.files
{
    public class ModelConvert<T> where T : PluginOrderBase, new()
    {

        private Dictionary<string, string> Mapping;
        private static ILog log => IocProvider.GetService<ILog>();

        public ModelConvert() { }
        public ModelConvert(Dictionary<string, string> mapping)
        {
            Mapping = new Dictionary<string, string>();
            foreach (var key in mapping.Keys)
            {
                //Mapping[key.ToLower()] = mapping[key];
                Mapping.Add(key.ToLower(), mapping[key]);
            }

            //Mapping = mapping;
        }

        /// <summary>
        /// 将DataRow行转换成Entity
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public T ToEntity(DataRow row, out string error)
        {
            T entity = new T();
            Type info = typeof(T);
            error = "";
            var members = info.GetMembers();
            try
            {
                foreach (var mi in members)
                {
                    if (mi.MemberType == MemberTypes.Property)
                    {
                        var prp = mi.Name.ToLower();

                        if (!Mapping.ContainsKey(prp))
                            continue;

                        var colName = Mapping[prp];
                        if (string.IsNullOrEmpty(colName)) continue;

                        if (row.Table.Columns.Contains(colName))
                        {
                            var propInfo = info.GetProperty(mi.Name);
                            //根据ColumnName，将dr中的相对字段赋值给Entity属性
                            propInfo.SetValue(entity, Convert.ChangeType(row[colName], propInfo.PropertyType), null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return entity;
        }



        public string CheckMapping(DataRow row)
        {
            T entity = new T();
            Type info = typeof(T);
            var members = info.GetMembers();
            var errorMsg = "";

            foreach (var mi in members)
            {
                if (mi.MemberType == MemberTypes.Property)
                {
                    var prp = mi.Name.ToLower();

                    if (!Mapping.ContainsKey(prp)) continue;

                    var colName = Mapping[prp];

                    //Log.Info($"chekc ==> {prp} --> {colName}");

                    if (!string.IsNullOrEmpty(colName) && !row.Table.Columns.Contains(colName))
                    {
                        //映射字段不存在
                        errorMsg += $"[{prp}==>{colName}],数据源不存在列名：{colName}; ";
                    }
                }
            }
            return errorMsg;
        }



        /// <summary>
        /// 将DataTable转换成Entity列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> ToList(DataTable dt, out Dictionary<int, string> errorDic)
        {
            List<T> list = new List<T>();
            errorDic = new Dictionary<int, string>();
            //var errorTable = new DataTable();
            var msg = "";
            //var initIndex = int.Parse(DateTime.Now.ToString("HHmmss")) * 1000;
            var initIndex = (DateTime.Now - DateTime.Now.Date).TotalSeconds * 10000;

            foreach (DataRow row in dt.Rows)
            {
                //Log.Info($"dataRow : {row.ToJson()}");
                var m = ToEntity(row, out msg);
                if (string.IsNullOrEmpty(msg))
                {
                    if (m.index == 0) m.index = (int)initIndex++;
                    list.Add(m);
                }
                else
                {
                    //errorTable.Rows.Add(row);
                    log.Info($"index : {row[0]} , msg : {msg}");
                    var index = 0;
                    Int32.TryParse(row[0].ToString(), out index);
                    if (index > 0)
                        errorDic.Add(Convert.ToInt32(row[0]), msg);
                }
            }
            return list;
        }
    }



}