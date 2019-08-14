using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.BusinessServices.ThridInventoryNoteModule {
public class ThridInventoryNoteService :IThridInventoryNoteService {
private IUnitOfWork _uow;
private IThridInventoryNoteDapperRepository _repository;
public ThridInventoryNoteService(IUnitOfWork uow) {
_uow = uow;
_repository = _uow.GetRepository<IThridInventoryNoteDapperRepository>();
       }
   }
}
