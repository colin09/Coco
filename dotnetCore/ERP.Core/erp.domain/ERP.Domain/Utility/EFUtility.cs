using System;
using System.Collections.Generic;
// using System.Data.Entity;
// using System.Data.Entity.Core.Mapping;
// using System.Data.Entity.Core.Metadata.Edm;
// using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace ERP.Domain.Utility
{
    public class EFUtility
    {
        /*
        /// <summary>
        /// EF预热
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void WarmUpContext<T>() where T : DbContext, new()
        {
            using (T context = new T())
            {
                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
        }
        */
    }
}
