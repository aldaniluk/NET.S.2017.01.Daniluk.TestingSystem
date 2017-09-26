namespace Domain.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=TestingSystem")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Statistic> Statistics { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UsersToRoles").MapLeftKey("RoleId").MapRightKey("UserId"));
        }
    }
}
