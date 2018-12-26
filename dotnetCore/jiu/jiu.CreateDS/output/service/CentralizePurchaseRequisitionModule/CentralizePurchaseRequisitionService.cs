using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.BusinessServices.CentralizePurchaseRequisitionModule {
public class CentralizePurchaseRequisitionService :ICentralizePurchaseRequisitionService {
private IUnitOfWork _uow;
private ICentralizePurchaseRequisitionDapperRepository _repository;
public CentralizePurchaseRequisitionService(IUnitOfWork uow) {
_uow = uow;
_repository = _uow.GetRepository<ICentralizePurchaseRequisitionDapperRepository>();
       }
   }
}
