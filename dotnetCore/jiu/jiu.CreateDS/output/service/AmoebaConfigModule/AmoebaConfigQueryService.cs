using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.Dapper.Repositories.AmoebaModule.AmoebaConfigModule {
public class AmoebaConfigQueryService :IAmoebaConfigQueryService {
private IAmoebaConfigQueryService _repository;
public AmoebaConfigQueryService(IAmoebaConfigQueryService repository) {
_repository=repository;
       }
   }
}
