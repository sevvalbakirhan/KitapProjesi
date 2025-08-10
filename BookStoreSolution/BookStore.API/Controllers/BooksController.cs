using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookStoreContext _context;

    public BooksController(BookStoreContext context)
    {
        _context = context;
    }

   
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _context.Books.Include(b => b.Category).ToListAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
            return NotFound(new { message = "Kitap bulunamadı" });

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound(new { message = "Kitap bulunamadı" });

        book.Title = updatedBook.Title;
        book.Price = updatedBook.Price;
        book.ImageUrl = updatedBook.ImageUrl;
        book.CategoryId = updatedBook.CategoryId;

        await _context.SaveChangesAsync();
        return Ok(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound(new { message = "Kitap bulunamadı" });

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Kitap silindi" });
    }
}