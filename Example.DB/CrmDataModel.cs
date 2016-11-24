namespace Example.DB
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    using Example.DB.Migrations;

    public class CrmDataModel : DbContext
    {
        public CrmDataModel()
            : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        static CrmDataModel()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CrmDataModel, Configuration>());
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Rolle> Roles { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().
                HasKey(c => c.Id).
                HasMany(e => e.Contacts).
                WithRequired(e => e.Customer);

            modelBuilder.Entity<Customer>().
                HasOptional(c => c.CreatedBy).
                WithMany( c => c.Customers);

           modelBuilder.Entity<Customer>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Contact>().
                HasKey(x => x.Id).
                HasRequired(e => e.Customer).
                WithMany(e => e.Contacts);

            modelBuilder.Entity<Contact>().
               HasOptional(c => c.CreatedBy).
               WithMany(c => c.Contacts);

            modelBuilder.Entity<Contact>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>().
               HasKey(x => x.Id).
               HasMany(e => e.Customers).
               WithOptional(c => c.CreatedBy);

            modelBuilder.Entity<User>().
               HasMany(e => e.Contacts).
               WithOptional(c => c.CreatedBy);

            modelBuilder.Entity<User>().
               HasMany(e => e.UserRoles).
               WithOptional(c => c.User);
            modelBuilder.Entity<User>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Rolle>().
               HasKey(x => x.Id).
               HasMany(e => e.UserRoles).
               WithOptional(c => c.Role);
            modelBuilder.Entity<Rolle>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<UserRole>().
                HasOptional(c => c.Role);

            modelBuilder.Entity<UserRole>().
               HasOptional(c => c.User);
            modelBuilder.Entity<UserRole>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            base.OnModelCreating(modelBuilder);
        }
    }
}
