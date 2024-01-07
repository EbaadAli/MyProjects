using CodeBlog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.API.Data
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        //dbset class represents collection of entities of a particular type in a relational db
        // part of the ef core orm as a way to interact with the db tables
        // creates the tables based off models in a code-first approach to database
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
