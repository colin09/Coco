using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.BusinessServices.CentralizePurchaseRequisitionModule {
public class CentralizePurchaseRequisitionQueryService :ICentralizePurchaseRequisitionQueryService {
private ICentralizePurchaseRequisitionDapperQueryRepository _repository;
public CentralizePurchaseRequisitionQueryService(ICentralizePurchaseRequisitionDapperQueryRepository repository) {
_repository=repository;
       }
   }
}
