using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.BusinessServices.PurchaseTaskModule {
public class PurchaseTaskSearch : ISearch {
public Query CreateQuery(){
var query = Query.Create();

return query;
       }
   }
}
