using App1.ServiceBase.Interfaces;

namespace App1.ServiceBase.ServiceBase.Models
{
    public class HasOperator : IHasOperator
    {
        public int OperatorId { get; set; }
        public OperatorType OperatorType { get; set; }
    }
}
