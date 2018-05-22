//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SqlAdapter
{
    using System;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Domain.Users;
    using Domain.Posts;
    
    
    public class EventStoreContext : DbContext
    {
        
        public DbSet<DomainEventBase> EventHistory { get; private set; }
        
        public DbSet<User> Users { get; private set; }
        
        public DbSet<UserUpdateAgeEvent> UserUpdateAgeEvents { get; private set; }
        
        public DbSet<UserUpdateNameEvent> UserUpdateNameEvents { get; private set; }
        
        public DbSet<UserAddPostEvent> UserAddPostEvents { get; private set; }
        
        public DbSet<UserAddPinnedPostEvent> UserAddPinnedPostEvents { get; private set; }
        
        public DbSet<UserCreateEvent> UserCreateEvents { get; private set; }
        
        public DbSet<Post> Posts { get; private set; }
        
        public DbSet<PostUpdateTitleEvent> PostUpdateTitleEvents { get; private set; }
        
        public DbSet<PostCreateEvent> PostCreateEvents { get; private set; }
        
        public EventStoreContext(DbContextOptions<EventStoreContext> options) : 
                base(options)
        {
        }
    }
}
