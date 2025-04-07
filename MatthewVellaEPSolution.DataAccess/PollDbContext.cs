using Microsoft.EntityFrameworkCore;
using MatthewVellaEPSolution.Domain;
using System.Collections.Generic;

namespace MatthewVellaEPSolution.DataAccess
{
    public class PollDbContext : DbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options)
            : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
    }
}
