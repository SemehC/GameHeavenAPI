using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories.Genres
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenreRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Genre> CreateGenreAsync(Genre genre)
        {
            var createdGenre = (await _applicationDbContext.Genres.AddAsync(genre)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return createdGenre;
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _applicationDbContext.Genres
                .FirstOrDefaultAsync(genre => genre.Id == id);
            _applicationDbContext.Genres.Remove(genre);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _applicationDbContext.Genres
                    .FirstOrDefaultAsync(genre => genre.Id == id);
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await _applicationDbContext.Genres
                .ToListAsync();
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            var genreToBeUpdated = await _applicationDbContext.Genres.FirstOrDefaultAsync(genreInDb => genreInDb.Id == genre.Id);
            if(genreToBeUpdated is not null)
            {
                genreToBeUpdated.Name = genre.Name;
                genreToBeUpdated.Description = genre.Description;
                _applicationDbContext.Genres.Update(genreToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
