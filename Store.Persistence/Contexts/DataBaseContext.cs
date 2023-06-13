using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Domain.Entities.Contacts;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Contexts
{
    public class DataBaseContext : IdentityDbContext<User,Role,string>,IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
           
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<ItemTag> ItemTags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(b => b.HasQueryFilter(p => !p.IsRemoved));
            builder.Entity<Rate>(b =>
            {
                b.HasOne(r => r.User)
                .WithMany(r => r.Rates)
                .HasForeignKey(r => r.UserId)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
            });

            builder.Entity<Comment>(b =>
            {
                b.HasOne(r => r.User)
                .WithMany(r => r.Comments)
                .HasForeignKey(r => r.UserId)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
            });

            builder.Entity<User>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);
                b.HasQueryFilter(p => !p.IsRemoved);
                // Indexes for "normalized" username and email, to allow efficient lookups
                b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

                // Maps to the AspNetUsers table
                b.ToTable("Users");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                // The relationships between User and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each User can have many UserClaims
                b.HasMany<IdentityUserClaim<string>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                b.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                b.HasMany<IdentityUserToken<string>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                // Primary key
                b.HasKey(uc => uc.Id);

                // Maps to the AspNetUserClaims table
                b.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                // Composite primary key consisting of the LoginProvider and the key to use
                // with that provider
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);

                // Maps to the AspNetUserLogins table
                b.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserToken<string>>(b =>
            {
                // Composite primary key consisting of the UserId, LoginProvider and Name
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                // Limit the size of the composite key columns due to common DB restrictions

                // Maps to the AspNetUserTokens table
                b.ToTable("UserTokens");
            });

            builder.Entity<IdentityRole<string>>(b =>
            {
                // Primary key
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                // Maps to the AspNetRoles table
                b.ToTable("Rolse");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany<IdentityRoleClaim<string>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

                //b.HasData(new Role { InsertTime=DateTime.Now, IsRemoved=false, Name =RollsName.Admin, Description=RollsTitle.Admin, NormalizedName="ADMIN" });
                //b.HasData(new Role { InsertTime=DateTime.Now, IsRemoved=false, Name =RollsName.Customer, Description=RollsTitle.Customer, NormalizedName = "CUSTOMER" });

            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                // Primary key
                b.HasKey(rc => rc.Id);

                // Maps to the AspNetRoleClaims table
                b.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserRole<string>>(b =>
            {
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });

                // Maps to the AspNetUserRoles table
                b.ToTable("UserRoles");
            });

            //modelBuilder.Entity<User>().Property(a => a.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
            //modelBuilder.Entity<Roll>().Property(a => a.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
            //modelBuilder.Entity<UserInRoll>().Property(a => a.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
            //modelBuilder.Entity<Login>().Property(a => a.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
            //modelBuilder.Entity<Contact>().Property(a => a.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
            //modelBuilder.Entity<ContactType>().Property(a => a.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();

            //Query Fillter

            //modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);

            //Insert Base Rolls
            //modelBuilder.Entity<>().HasData(new Roll{ Id=1, InsertTime=DateTime.Now, IsRemoved=false, RollName=RollsName.Admin, Title=RollsTitle.Admin });
            //modelBuilder.Entity<Roll>().HasData(new Roll { Id=2, InsertTime=DateTime.Now, IsRemoved=false, RollName=RollsName.Customer, Title=RollsTitle.Customer });

            ////Insert Base Contactstypes
            //modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=1, InsertTime=DateTime.Now, IsRemoved=false, Icon="smartphone", Value=ContactTypeNames.Mobile, Title=ContactTypeTitle.Mobile });
            //modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=2, InsertTime=DateTime.Now, IsRemoved=false, Icon="phone", Value=ContactTypeNames.Phone, Title=ContactTypeTitle.Phone });
            //modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=3, InsertTime=DateTime.Now, IsRemoved=false, Icon="mail", Value=ContactTypeNames.Email, Title=ContactTypeTitle.Email });
            //modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=4, InsertTime=DateTime.Now, IsRemoved=false, Icon="map-pin", Value=ContactTypeNames.Address, Title=ContactTypeTitle.Address });
            //modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=5, InsertTime=DateTime.Now, IsRemoved=false, Icon="home", Value=ContactTypeNames.PostalCode, Title=ContactTypeTitle.PostalCode });

            builder.Entity<Role>().HasData(new Role { Name = RollsName.Admin, NormalizedName="ADMIN" });
            builder.Entity<Role>().HasData(new Role { Name = RollsName.Customer, NormalizedName="CUSTOMER" });
        }

    }
}
