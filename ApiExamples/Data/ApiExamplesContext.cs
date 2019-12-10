using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiExamples.Models
{
    public class ApiExamplesContext : DbContext
    {
        public ApiExamplesContext (DbContextOptions<ApiExamplesContext> options)
            : base(options)
        {
        }

        public DbSet<ApiExamples.Models.Documents> Documents { get; set; }
        public DbSet<ApiExamples.Models.User> Users { get; set; }
        public DbSet<ApiExamples.Models.OrganizationalProfile> OrganizationalProfiles { get; set; }
    }
}
