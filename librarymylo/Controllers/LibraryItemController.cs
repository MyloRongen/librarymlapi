using librarymylo.WebApi.Models;
using librarymylo_BLL.Interfaces.Services;
using librarymylo_BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace librarymylo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryItemController : ControllerBase
    {
        private readonly ILibraryItemService _libraryItemService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LibraryItemController(ILibraryItemService libraryItemService, IWebHostEnvironment webHostEnvironment)
        {
            _libraryItemService = libraryItemService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("GetLibraryItem")]
        public async Task<IEnumerable<LibraryItemViewModel>> GetLibraryItems()
        {
            List<LibraryItem> libraryItems = await _libraryItemService.GetLibraryItemsAsync();

            List<LibraryItemViewModel> libraryItemViewModels = libraryItems.Select(libraryItem => new LibraryItemViewModel
            {
                Id = libraryItem.Id,
                Name = libraryItem.Name,
                ImageUrl = libraryItem.ImageUrl,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, libraryItem.ImageUrl)
            }).ToList();

            return libraryItemViewModels;
        }

        [HttpGet]
        [Route("GetLibraryItemByCategory")]
        public async Task<IEnumerable<LibraryItemViewModel>> GetLibraryItemsByCategory(int categoryId)
        {
            List<LibraryItem> libraryItems = await _libraryItemService.GetLibraryItemsByCategory(categoryId);

            List<LibraryItemViewModel> libraryItemViewModels = libraryItems.Select(libraryItem => new LibraryItemViewModel
            {
                Id = libraryItem.Id,
                Name = libraryItem.Name,
                ImageUrl = libraryItem.ImageUrl,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, libraryItem.ImageUrl)
            }).ToList();

            return libraryItemViewModels;
        }

        // GET api/<LibraryItemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("AddLibraryItem")]
        public async Task<LibraryItemViewModel> AddLibraryItem([FromForm] LibraryItemViewModel libraryItemViewModel)
        {
            libraryItemViewModel.ImageUrl = await SaveImageAsync(libraryItemViewModel.ImageFile);

            LibraryItem libraryItem = new()
            {
                Id = libraryItemViewModel.Id,
                Name = libraryItemViewModel.Name,
                ImageUrl = libraryItemViewModel.ImageUrl,
                CategoryId = 1,
            };

            libraryItem = await _libraryItemService.CreateLibraryItemAsync(libraryItem);

            libraryItemViewModel = new()
            {
                Id = libraryItem.Id,
                Name = libraryItemViewModel.Name,
                ImageUrl = libraryItemViewModel.ImageUrl,
                CategoryId = libraryItemViewModel.CategoryId,
            };

            return libraryItemViewModel;
        }

        [HttpPut]
        [Route("UpdateLibraryItem/{id}")]
        public async Task<LibraryItemViewModel> UpdateLibraryItem(int id, [FromForm] LibraryItemViewModel libraryItemViewModel)
        {
            if (libraryItemViewModel.ImageFile != null)
            {
                DeleteImage(libraryItemViewModel.ImageUrl);
                libraryItemViewModel.ImageUrl = await SaveImageAsync(libraryItemViewModel.ImageFile);
            }

            LibraryItem libraryItem = new()
            {
                Id = libraryItemViewModel.Id,
                Name = libraryItemViewModel.Name,
                ImageUrl = libraryItemViewModel.ImageUrl,
            };

            libraryItem = await _libraryItemService.UpdateLibraryItemAsync(libraryItem);

            libraryItemViewModel = new()
            {
                Id = libraryItem.Id,
                Name = libraryItemViewModel.Name,
                ImageUrl = libraryItemViewModel.ImageUrl,
            };

            return libraryItemViewModel;
        }

        [HttpDelete]
        [Route("DeleteLibraryItem/{id}")]
        public async Task<bool> DeleteLibraryItem(int id)
        {
            LibraryItem libraryItem = await _libraryItemService.GetLibraryItemByIdAsync(id);
            bool success = await _libraryItemService.DeleteLibraryItemAsync(libraryItem);

            return success;
        }

        [NonAction]
        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}
