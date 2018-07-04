using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTaskApi.EF.Entities;

namespace TestTaskApi.Models
{
    public class TestTaskApiContext : DbContext
    {
        public TestTaskApiContext (DbContextOptions<TestTaskApiContext> options)
            : base(options)
        {
        }

        public DbSet<TestTaskApi.EF.Entities.Resource> Resource { get; set; }
    }
}
