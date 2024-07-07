using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
            optionsBuilder.UseSqlServer(
             "Data Source= (local)\\MSSQLSERVER01; Initial Catalog=PubContractDataMigrationsTest; TrustServerCertificate=True; User Id=Domo; Password=Dominik5123!;");
            return new OrderContext(optionsBuilder.Options);
        }
    }
}
