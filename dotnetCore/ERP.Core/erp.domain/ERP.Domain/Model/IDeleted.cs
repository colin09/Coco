using System;


namespace ERP.Domain.Model
{
    public interface IDeleted
    {
        bool IsDeleted { get; set; }
    }
}
