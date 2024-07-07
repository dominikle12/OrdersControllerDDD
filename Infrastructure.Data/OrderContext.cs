using Domain.AggregateRoots;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Customer> Customers => Set<Customer>();
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(order =>
            {
                order.HasKey(o => o.Id);
                order.Property(o => o.CreationDate).IsRequired(false);
                order.Property(o => o.LocationName).HasMaxLength(200);
                order.Property(o => o.LocationId).HasMaxLength(50);
                order.Property(o => o.EndpointId).HasMaxLength(50);
                order.Property(o => o.AdapterId).HasMaxLength(50);
                order.Property(o => o.Assignee).HasMaxLength(100);
                order.Property(o => o.AssignedToDepartment).HasMaxLength(100);
                order.Property(o => o.CrmRootName).HasMaxLength(100);
                order.Property(o => o.CrmTariffName).HasMaxLength(100);
                order.Property(o => o.OrderClassification).HasMaxLength(50);
                order.Property(o => o.Status).HasMaxLength(50);
                order.Property(o => o.StatusChangedDate).IsRequired(false);
                order.Property(o => o.OrderType).HasMaxLength(50);
                order.Property(o => o.Priority).HasMaxLength(50);
                order.Property(o => o.IsOnHold).IsRequired();
                order.Property(o => o.Technology).HasMaxLength(50);
                order.Property(o => o.ServiceRequestId).HasMaxLength(50);
                order.Property(o => o.OnHoldReason).HasMaxLength(200);
                order.Property(o => o.CrmOrderNumber).HasMaxLength(50);

                order.HasOne(o => o.Customer)
                    .WithOne()
                    .HasForeignKey<Order>(o => o.CustomerId);
            });

            modelBuilder.Entity<Customer>(customer =>
            {
                customer.HasKey(c => c.Id);
                customer.Property(c => c.Name).IsRequired().HasMaxLength(100);
                customer.Property(c => c.OIB).IsRequired().HasMaxLength(20);
                customer.Property(c => c.OibName).HasMaxLength(100);
                customer.Property(c => c.CustomerContactName).HasMaxLength(100);
                customer.Property(c => c.CustomerContactEmail).HasMaxLength(100);
                customer.Property(c => c.CustomerContactPhone).HasMaxLength(20);
                customer.Property(c => c.TechnicalContactName).HasMaxLength(100);
                customer.Property(c => c.TechnicalContactEmail).HasMaxLength(100);
                customer.Property(c => c.TechnicalContactPhone).HasMaxLength(20);
                customer.Property(c => c.Segment).HasMaxLength(50);
                customer.Property(c => c.KAM).HasMaxLength(100);
                customer.Property(c => c.TAM).HasMaxLength(100);
                customer.Property(c => c.CustomerCode).HasMaxLength(50);
                customer.OwnsOne(c => c.Address, address =>
                {
                    address.Property(a => a.Street).HasMaxLength(200);
                    address.Property(a => a.City).HasMaxLength(100);
                    address.Property(a => a.HouseNumber).HasMaxLength(20);
                    address.Property(a => a.PostalCode).HasMaxLength(20);
                    address.Property(a => a.Country).HasMaxLength(100);
                    address.Property(a => a.Phone).HasMaxLength(30);
                });
            });
        }
    }
}
