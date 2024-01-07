using CodeBlog.API.Models.Domain;

namespace CodeBlog.API.Repositories.Interface
{
    //since this is an interface we cant define the methods, were just setting the terms of the contract here
    //repositories is a design pattern to add an abstract layer to communicate with the database instead of accessing it from the controller directly
    public interface ICategoryRepository
    {
        //anyone that uses this interface must define this method: 
        Task<Category> CreateAsync(Category category); 
    }
}
