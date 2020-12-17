using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
namespace Persistence.DesignTimeDbFactory
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HospitalDb;Trusted_Connection=True;");
            var context = new ApplicationContext(builder.Options);
            return context;
        }
    }
}
