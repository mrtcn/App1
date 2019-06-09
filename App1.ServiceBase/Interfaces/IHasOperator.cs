using App1.ServiceBase.ServiceBase.Models;

namespace App1.ServiceBase.Interfaces
{
    public interface IHasOperator
    {
        int OperatorId { get; set; }
        OperatorType OperatorType { get; set; }
    }
}
