using System;
using System.Collections.Generic;
namespace LDTech.ERP.Domain.BusinessServices.ThridInventoryNoteModule {
public class ThridInventoryNoteQueryService :IThridInventoryNoteQueryService {
private IThridInventoryNoteDapperQueryRepository _repository;
public ThridInventoryNoteQueryService(IThridInventoryNoteDapperQueryRepository repository) {
_repository=repository;
       }
   }
}
