using System;

namespace App1.ServiceBase.Interfaces
{
    public interface IOperatorFields {
        DateTime CreatedDate { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
