using Microsoft.EntityFrameworkCore;
using NiksoftCore.DataAccess;

namespace NiksoftCore.FormBuilder.Service
{
    public class FbDbContext : NikDbContext, IFbUnitOfWork
    {

        public FbDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<FormCategroy> FormCategroies { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormControl> formControls { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<FormAnswer> FormAnswers { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FormCategroyMap());
            builder.ApplyConfiguration(new FormMap());
            builder.ApplyConfiguration(new FormControlMap());
            builder.ApplyConfiguration(new ListItemMap());
            builder.ApplyConfiguration(new FormAnswerMap());

        }
    }
}
