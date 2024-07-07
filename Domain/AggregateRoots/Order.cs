using Domain.Entities;
using Domain.SharedKernel;

namespace Domain.AggregateRoots
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime? CreationDate { get; set; }
        public Customer Customer { get; set; }
        public string LocationName { get; set; }
        public string LocationId { get; set; }
        public string EndpointId { get; set; }
        public string AdapterId { get; set; }
        public string Assignee { get; set; }
        public string AssignedToDepartment { get; set; }
        public string CrmRootName { get; set; }
        public string CrmTariffName { get; set; }
        public string OrderClassification { get; set; }
        public string Status { get; set; }
        public DateTime? StatusChangedDate { get; set; }
        public string OrderType { get; set; }
        public string Priority { get; set; }
        public bool IsOnHold { get; set; }
        public string Technology { get; set; }
        public string ServiceRequestId { get; set; }
        public string OnHoldReason { get; set; }
        public string CrmOrderNumber { get; set; }
    }
}
