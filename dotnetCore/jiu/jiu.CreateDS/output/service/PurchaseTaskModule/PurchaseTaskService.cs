using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.BusinessServices.PurchaseTaskModule {
public class PurchaseTaskService :IPurchaseTaskService {
private IUnitOfWork _uow;
private IPurchaseTaskDapperRepository _repository;
public PurchaseTaskService(IUnitOfWork uow) {
_uow = uow;
_repository = _uow.GetRepository<IPurchaseTaskDapperRepository>();
       }
   }
}
