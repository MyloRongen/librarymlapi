using librarymylo_BLL.Interfaces.Repositories;
using librarymylo_BLL.Interfaces.Services;
using librarymylo_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarymylo_BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetCategoriesAsync(string userId)
        {
            return await _categoryRepository.GetCategoriesAsync(userId);
        }
    }
}
