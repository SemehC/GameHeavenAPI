using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories.Genres
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetGenresAsync();
        Task<Genre> GetGenreByIdAsync(int id);
        Task<Genre> CreateGenreAsync(Genre genre);
        Task DeleteGenreAsync(int id);
        Task UpdateGenreAsync(Genre genre);
    }
}
