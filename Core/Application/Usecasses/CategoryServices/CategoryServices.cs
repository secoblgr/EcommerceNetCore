using Application.Dtos.CategoryDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.CategoryServices
{
    public class CategoryServices : ICategoryServices                                  // category servicemizi implemet ettik.
    {

        private readonly IRepository<Category> _repository;                           // çalışacagımız modele bağlantı .

        public CategoryServices(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto model)
        {
            await _repository.CreateAsync( new Category
            {
                CategoryName = model.CategoryName,
            });
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(category);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(x => new ResultCategoryDto
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
            }).ToList();
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return new GetByIdCategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto model)
        {
            var category = await _repository.GetByIdAsync(model.CategoryId);          //ilk önce güncelleyecegimiz modeli bulduk.
            category.CategoryId = model.CategoryId;                                   // güncelleceneck kısımlaır modelden gelen ile eşitledik.
            category.CategoryName = model.CategoryName;
            await _repository.UpdateAsync(category);
        }
    }
}
