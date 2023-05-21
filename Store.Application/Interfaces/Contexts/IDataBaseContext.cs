using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.Contacts;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Roll> Rolls { get; set; }
        DbSet<UserInRoll> UserInRolls { get; set; }
        DbSet<Login> Logins { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<ContactType> ContactTypes { get; set; }

        int SaveChanges(bool acceptAllChangeOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangeOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
