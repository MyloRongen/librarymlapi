using librarymylo_BLL.Interfaces.Repositories;
using librarymylo_BLL.Models;
using librarymylo_DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarymylo_DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetCategoriesAsync(string userId)
        {
            return await _dbContext.Categories.ToListAsync();
        }

    }
}
