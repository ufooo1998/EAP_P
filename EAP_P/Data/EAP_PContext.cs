using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EAP_P.Models
{
    public class EAP_PContext : DbContext
    {
        public EAP_PContext (DbContextOptions<EAP_PContext> options)
            : base(options)
        {
        }

        public DbSet<EAP_P.Models.Employee> Employee { get; set; }
    }
}
