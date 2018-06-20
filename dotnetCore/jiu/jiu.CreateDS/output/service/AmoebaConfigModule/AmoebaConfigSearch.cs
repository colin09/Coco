using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.Dapper.Repositories.AmoebaModule.AmoebaConfigModule {
public class AmoebaConfigSearch : ISearch {
public Query CreateQuery(){
var query = Query.Create();

return query;
       }
   }
}
