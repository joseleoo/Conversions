using Microsoft.EntityFrameworkCore;

namespace CoinsApi.Models
{
    public class QuoContext : DbContext
    {
        public QuoContext(DbContextOptions<QuoContext> options): base(options)
        {
        }

        public DbSet<Quotation> Quotations { get; set; }
    }
}