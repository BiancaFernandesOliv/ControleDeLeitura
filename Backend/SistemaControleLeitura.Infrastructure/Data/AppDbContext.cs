using Microsoft.EntityFrameworkCore;

namespace SistemaControleLeitura.Infrastructure.Data;
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }


