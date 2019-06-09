
namespace App1.ServiceBase.Interfaces
{
    public interface IHasParent<T>
    {
        T BaseEntity { get; set; }
        int BaseEntityId { get; set; }
    }
}
