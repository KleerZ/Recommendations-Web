﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common.Algolia;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Domain;

namespace Recommendations.Persistence.DbContexts;

public sealed class RecommendationsDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>,
    IRecommendationsDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Image> Images { get; set; }
    
    private readonly IServiceProvider _serviceProvider;

    public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options,
        IServiceProvider serviceProvider)
        : base(options)
    {
        _serviceProvider = serviceProvider;
        Database.Migrate();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        await _serviceProvider.GetRequiredService<AlgoliaDbSync>().Synchronize();
        return await base.SaveChangesAsync(cancellationToken);
    }
}