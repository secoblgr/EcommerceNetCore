using Application.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.CategoryServices
{
    public interface ICategoryServices
    {
        // burada liste dönüşü olarak oluşturdugumuz dto göre işlem yapacagımız için burda resultcategorydto kullandık.

        Task<List<ResultCategoryDto>> GetAllCategoryAsync();                             // tüm kategoerileri getirme.
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(int id);                           // id göre category getirme.
        Task CreateCategoryAsync(CreateCategoryDto model);          // gönderilen dto göre create işlemi yapılır.
        Task UpdateCategoryAsync(UpdateCategoryDto model);          //gönderilen dto göre update işlemi yapılır.
        Task DeleteCategoryAsync(int id);                           // id ile getbyid ile bulup silme işlemi.
    }
}
