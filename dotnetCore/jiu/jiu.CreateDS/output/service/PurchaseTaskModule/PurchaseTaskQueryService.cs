using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.BusinessServices.PurchaseTaskModule {
public class PurchaseTaskQueryService :IPurchaseTaskQueryService {
private IPurchaseTaskDapperQueryRepository _repository;
public PurchaseTaskQueryService(IPurchaseTaskDapperQueryRepository repository) {
_repository=repository;
       }
   }
}
