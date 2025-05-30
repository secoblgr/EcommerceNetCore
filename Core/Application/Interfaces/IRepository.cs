using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : class         // bizim gönderdiğimiz entity T modelilne göre işlemler yapmamıızı sağlar. category,product vs.
    {
        Task<List<T>> GetAllAsync();                        // bütün hepsini listeleme.
        Task<T> GetByIdAsync(int id);                       // id ye göre data getirme işlemi. verilen id ye göre yapar.
        Task CreateAsync(T entity);                         // T modeline göre create işlmei yapmızı sağlar.
        Task UpdateAsync(T entity);                         // T modeline göre update işlemi yapılır, modelin update dto da olan verileri güncellenir.
        Task DeleteAsync(T entity);                         // id ye göre bulunan T modelinin silinmesini sağlar.
    }
}
