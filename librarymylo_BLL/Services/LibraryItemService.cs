using librarymylo_BLL.Interfaces.Repositories;
using librarymylo_BLL.Interfaces.Services;
using librarymylo_BLL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarymylo_BLL.Services
{
    public class LibraryItemService : ILibraryItemService
    {
        private readonly ILibraryItemRepository _libraryItemRepository;

        public LibraryItemService(ILibraryItemRepository libraryItemRepository) 
        {
            _libraryItemRepository = libraryItemRepository;
        }

        public async Task<List<LibraryItem>> GetLibraryItemsAsync()
        {
            return await _libraryItemRepository.GetLibraryItemsAsync();
        }

        public async Task<List<LibraryItem>> GetLibraryItemsByCategory(int categoryId)
        {
            return await _libraryItemRepository.GetLibraryItemsByCategory(categoryId);
        }

        public async Task<LibraryItem> GetLibraryItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new LibraryItem {};
            }

            LibraryItem? libraryItem = await _libraryItemRepository.GetLibraryItemByIdAsync(id);

            if (libraryItem == null)
            {
                return new LibraryItem { };
            }

            return libraryItem;
        }

        public async Task<LibraryItem> CreateLibraryItemAsync(LibraryItem libraryItem)
        {
            libraryItem = await _libraryItemRepository.CreateLibraryItemAsync(libraryItem);

            return libraryItem;
        }

        public async Task<LibraryItem> UpdateLibraryItemAsync(LibraryItem libraryItem)
        {
            if (libraryItem.Id <= 0)
            {
                return libraryItem;
            }

            if (string.IsNullOrEmpty(libraryItem.Name))
            {
                return libraryItem;
            }

            libraryItem = await _libraryItemRepository.UpdateLibraryItemAsync(libraryItem);

            return libraryItem;
        }

        public async Task<bool> DeleteLibraryItemAsync(LibraryItem libraryItem)
        {
            bool success = await _libraryItemRepository.DeleteLibraryItemAsync(libraryItem);

            return success;
        }
    }
}
