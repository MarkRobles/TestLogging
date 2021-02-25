using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestLogging.Domain;

namespace TestLogging.Data
{
    public class TestDbContext:DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {

        }

        public DbSet<Pokemon> Pokemones { get; set; }
    }
}
