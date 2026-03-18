using hometask.Entities;
using Microsoft.EntityFrameworkCore;

namespace hometask.Data {
    public class DatabaseContext : DbContext{
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<HouseTaskParticipant>()
                .HasKey(x => new { x.HouseTaskId, x.PersonId });

            modelBuilder.Entity<HouseTaskCompletion>()
                .HasIndex(x => new { x.HouseTaskId, x.WeekStart })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<HouseTask> HouseTasks { get; set; }
        public virtual DbSet<HouseArea> HouseAreas { get; set; }
        public virtual DbSet<HouseTaskCompletion> HouseTasksCompletions { get; set; }
        public virtual DbSet<HouseTaskParticipant> HouseTasksParticipants { get;set; }

    }
}
