using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.Constant.Enum;
using Store.Domain.Entities.Contacts;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Roll> Rolls { get; set; }
        public DbSet<UserInRoll> UserInRolls { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Query Fillter
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);

            //Insert Base Rolls
            modelBuilder.Entity<Roll>().HasData(new Roll{ Id=1, InsertTime=DateTime.Now, IsRemoved=false, RollName=RollsName.Admin, Title=RollsTitle.Admin });
            modelBuilder.Entity<Roll>().HasData(new Roll { Id=2, InsertTime=DateTime.Now, IsRemoved=false, RollName=RollsName.Customer, Title=RollsTitle.Customer });

            //Insert Base Contactstypes
            modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=1, InsertTime=DateTime.Now, IsRemoved=false, Icon="smartphone", Value=ContactTypeNames.Mobile, Title=ContactTypeTitle.Mobile });
            modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=2, InsertTime=DateTime.Now, IsRemoved=false, Icon="phone", Value=ContactTypeNames.Phone, Title=ContactTypeTitle.Phone });
            modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=3, InsertTime=DateTime.Now, IsRemoved=false, Icon="mail", Value=ContactTypeNames.Email, Title=ContactTypeTitle.Email });
            modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=4, InsertTime=DateTime.Now, IsRemoved=false, Icon="map-pin", Value=ContactTypeNames.Address, Title=ContactTypeTitle.Address });
            modelBuilder.Entity<ContactType>().HasData(new ContactType { Id=5, InsertTime=DateTime.Now, IsRemoved=false, Icon="home", Value=ContactTypeNames.PostalCode, Title=ContactTypeTitle.PostalCode });

        }
    }
}
