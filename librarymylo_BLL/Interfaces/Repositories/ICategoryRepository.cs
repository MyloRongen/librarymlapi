using librarymylo_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarymylo_BLL.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync(string userId);
    }
}
