﻿using Microsoft.EntityFrameworkCore;

namespace Worky.Users
{
    public class UserDbContext:DbContext,IUsersCollection
    {
        ///Add-Migration IsBlock -context UserDbContext
      
        ///Update-Database -context UserDbContext

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public void AddUser(User user)
        {
            this.Users.Add(user);
            this.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            User u = Users.Where(i => i.Id == user.Id).FirstOrDefault();
            Users.Remove(u);
            this.SaveChanges();
        }

        public User GetUser(int id)
        {
            return this.Users.Where(i => i.Id == id).First();
        }

        public User GetUser(string Email)
        {


            return this.Users.Where(i => i.Email == Email).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return this.Users.ToList();
        }

      

        public void UpdateUser(User user)
        {
            User existUser = Users.Where(i => i.Id == user.Id).FirstOrDefault();
            existUser.Pass = user.Pass;
            existUser.IsConfirmed = user.IsConfirmed;
            existUser.UserName = user.UserName;
            existUser.Email = user.Email;
            existUser.IsBlock = user.IsBlock;
            this.SaveChanges();
        }
    }
}
