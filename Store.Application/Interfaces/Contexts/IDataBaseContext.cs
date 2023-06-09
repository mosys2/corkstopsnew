﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Contacts;
using Store.Domain.Entities.Post;
using Store.Domain.Entities.Products;
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
        DbSet<Category> Categories { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<Feature> Features { get; set; }
        DbSet<ItemTag> ItemTags { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Rate> Rates { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Media> Medias { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<Province> Provinces { get; set; }


        int SaveChanges(bool acceptAllChangeOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangeOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
