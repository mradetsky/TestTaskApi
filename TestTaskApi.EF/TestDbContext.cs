using System;
using System.Data.Entity;
using TestTaskApi.EF.Entities;

namespace TestTaskApi.EF
{
    public class TestDbContext : DbContext
    {
        public TestDbContext() 
        {
            this.Configuration.ProxyCreationEnabled = false;

        }


        public static TestDbContext Create()
        {
            return new TestDbContext();
        }


        public DbSet<Resource> Resources { get; set; }

       



    }
}
