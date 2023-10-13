using librarymylo_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarymylo_BLL.Interfaces.Repositories
{
    public interface ILibraryItemRepository
    {
        Task<List<LibraryItem>> GetLibraryItemsAsync();
        Task<LibraryItem> GetLibraryItemByIdAsync(int id);
        Task<LibraryItem> CreateLibraryItemAsync(LibraryItem libraryItem);
        Task<LibraryItem> UpdateLibraryItemAsync(LibraryItem LibraryItem);
        Task<bool> DeleteLibraryItemAsync(LibraryItem LibraryItem);
        Task<List<LibraryItem>> GetLibraryItemsByCategory(int categoryId);
    }
}
