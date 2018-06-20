using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.Dapper.Repositories.AmoebaModule.AmoebaConfigModule {
public class AmoebaConfigService :IAmoebaConfigService {
private IUnitOfWork _uow;
private IAmoebaConfigService _repository;
public AmoebaConfigService(IUnitOfWork uow) {
_uow = uow;
_repository = _uow.GetRepository<IAmoebaConfigService>();
       }
   }
}
