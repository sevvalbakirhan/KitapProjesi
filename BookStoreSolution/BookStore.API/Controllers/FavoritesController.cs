using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly BookStoreContext _context;

    public FavoritesController(BookStoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetFavorites()
    {
        var favorites = await _context.Favorites.Include(f => f.User).Include(f => f.Book).ToListAsync();
        return Ok(favorites);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFavorite(int id)
    {
        var favorite = await _context.Favorites.Include(f => f.User).Include(f => f.Book).FirstOrDefaultAsync(f => f.Id == id);
        if (favorite == null)
            return NotFound(new { message = "Favori bulunamadı" });

        return Ok(favorite);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] Favorite favorite)
    {
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFavorite), new { id = favorite.Id }, favorite);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFavorite(int id)
    {
        var favorite = await _context.Favorites.FindAsync(id);
        if (favorite == null)
            return NotFound(new { message = "Favori bulunamadı" });

        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Favori silindi" });
    }
}