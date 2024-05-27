using Microsoft.EntityFrameworkCore;

using ExemploORM.Models;

namespace ExemploORM.Data{

    public class UserDbContext : DbContext{
        public UserDbContext(DbContextOptions<UserDbContext> options):base(options){}
        public DbSet<User> User{get;set;}
    }

}