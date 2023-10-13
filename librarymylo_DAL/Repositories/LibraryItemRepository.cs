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
    public class LibraryItemRepository : ILibraryItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LibraryItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LibraryItem>> GetLibraryItemsAsync()
        {
            return await _dbContext.LibraryItems.ToListAsync();
        }

        public async Task<List<LibraryItem>> GetLibraryItemsByCategory(int categoryId)
        {
            List<LibraryItem> libraryItems = await _dbContext.LibraryItems
                .Where(item => item.CategoryId == categoryId)
                .ToListAsync();

            return libraryItems;
        }

        public async Task<LibraryItem> GetLibraryItemByIdAsync(int id)
        {
            return await _dbContext.LibraryItems.FindAsync(id);
        }

        public async Task<LibraryItem> CreateLibraryItemAsync(LibraryItem libraryItem)
        {
            try
            {
                _dbContext.LibraryItems.Add(libraryItem);
                await _dbContext.SaveChangesAsync();

                return libraryItem;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<LibraryItem> UpdateLibraryItemAsync(LibraryItem libraryItem)
        {
            _dbContext.LibraryItems.Update(libraryItem);
            await _dbContext.SaveChangesAsync();

            return libraryItem;
        }

        public async Task<bool> DeleteLibraryItemAsync(LibraryItem libraryItem)
        {
            _dbContext.LibraryItems.Remove(libraryItem);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
