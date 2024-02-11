using Microsoft.EntityFrameworkCore;
using practise.Entitys;

namespace practise.Database
{
    public class BookContext :DbContext
    {
        public DbSet<Book>  Books { get; set; }

        public DbSet<User> Users { get; set; }

        private IConfiguration config = null;
        public BookContext(IConfiguration cfg)
        {
            config = cfg;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config["ConnectionString"]);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
