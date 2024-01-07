using CodeBlog.API.Data;
using CodeBlog.API.Models.Domain;
using CodeBlog.API.Repositories.Interface;

namespace CodeBlog.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext DbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();

            return category;
        }
    }
}
