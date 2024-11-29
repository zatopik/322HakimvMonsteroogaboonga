using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praktos.DatabaseContext;
using Praktos.Interfaces;
using Praktos.Model;
using Praktos.Requests;
using Praktos.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Praktos.Services
{
    public class BooksService : IBooksService
    {
        readonly TestApiDB _context;
        public BooksService(TestApiDB context) 
        {
            _context = context;
        }
        public async Task<IActionResult> CreateNewBooks([FromQuery] CreateNewBooks newBooks)
        {
            try
            {
                var books = new Books()
                {
                    Title = newBooks.Title,
                    Id_Autors = newBooks.Id_Autors,
                    Id_Genre = newBooks.Id_Genre,
                    AvailableCopies = newBooks.AvailableCopies,
                    YearOfPublication = newBooks.YearOfPublication,
                    Description = newBooks.Description,
                };
            await _context.Books.AddAsync(books);
            await _context.SaveChangesAsync();
            return new OkResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id <= 0)
            {
                return new BadRequestObjectResult("Некорректный идентификатор книги.");
            }

            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return new NotFoundObjectResult("Книга с указанным идентификатором не найдена.");
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetAvailableCopies([FromRoute, Required] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return new BadRequestObjectResult("Название книги обязательно для поиска.");
            }

            try
            {
                var books = await _context.Books
                    .Where(b => b.Title.Contains(title))
                    .Select(b => new BookAvailableCopies
                    {
                        Id_Books = b.Id_Books,
                        Title = b.Title,
                        AvailableCopies = b.AvailableCopies
                    })
                    .ToListAsync();

                if (books == null || books.Count == 0)
                {
                    return new NotFoundObjectResult("Книги с указанным названием не найдены.");
                }

                return new OkObjectResult(books);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetBookById(int id)
        {
            if (id <= 0)
            {
                return new BadRequestObjectResult("Некорректный идентификатор книги.");
            }
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return new NotFoundObjectResult("Книга с указанным идентификатором не найдена.");
                }

                var bookDto = new GetAllBooksId
                {
                    Id_Books = book.Id_Books,
                    Title = book.Title,
                    Id_Autors = book.Id_Autors,
                    Id_Genre = book.Id_Genre,
                    AvailableCopies = book.AvailableCopies,
                    YearOfPublication = book.YearOfPublication,
                    Description = book.Description
                };
                return new OkObjectResult(bookDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetBooks(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return new BadRequestObjectResult("Параметры страницы и размера страницы должны быть положительными.");
            }

            try
            {
                var totalBooks = await _context.Books.CountAsync();
                var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);

                var books = await _context.Books
                    .Include(b => b.Autors)
                    .Include(b => b.Genres)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var booksDto = books.Select(b => new GetAllBooksName
                {
                    Id_Books = b.Id_Books,
                    Title = b.Title,
                    Autor = b.Autors.FName + " " + b.Autors.LName,
                    Genre = b.Genres.Title,
                    AvailableCopies = b.AvailableCopies,
                    YearOfPublication = b.YearOfPublication,
                    Description = b.Description
                });

                var result = new
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    TotalBooks = totalBooks,
                    Books = booksDto
                };

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetBooksByGenre(int genreId)
        {
            if (genreId <= 0)
            {
                return new BadRequestObjectResult("Некорректный идентификатор жанра.");
            }
            try
            {
                var books = await _context.Books
                    .Where(b => b.Id_Genre == genreId)
                    .ToListAsync();

                if (books == null || !books.Any())
                {
                    return new NotFoundObjectResult("Книги с указанным жанром не найдены.");
                }

                var booksDto = books.Select(b => new GetAllBooksId
                {
                    Id_Books = b.Id_Books,
                    Title = b.Title,
                    Id_Autors = b.Id_Autors,
                    Id_Genre = b.Id_Genre,
                    AvailableCopies = b.AvailableCopies,
                    YearOfPublication = b.YearOfPublication,
                    Description = b.Description
                });

                return new OkObjectResult(booksDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> SearchBooks(string title = null, string author = null, string genre = null, int? yearOfPublication = null)
        {
            int criteriaCount = 0;
            if (!string.IsNullOrEmpty(title)) criteriaCount++;
            if (!string.IsNullOrEmpty(author)) criteriaCount++;
            if (!string.IsNullOrEmpty(genre)) criteriaCount++;
            if (yearOfPublication.HasValue) criteriaCount++;

            if (criteriaCount != 1)
            {
                return new BadRequestObjectResult("Необходимо указать хотя бы один критерий поиска.");
            }

            try
            {
                var query = _context.Books.Include(b => b.Autors).Include(b => b.Genres).AsQueryable();

                if (!string.IsNullOrEmpty(title))
                {
                    query = query.Where(b => b.Title.Contains(title));
                }

                if (!string.IsNullOrEmpty(author))
                {
                    query = query.Where(b => b.Autors.FName.Contains(author) || b.Autors.LName.Contains(author));
                }

                if (!string.IsNullOrEmpty(genre))
                {
                    query = query.Where(b => b.Genres.Title.Contains(genre));
                }

                if (yearOfPublication.HasValue)
                {
                    query = query.Where(b => b.YearOfPublication == yearOfPublication.Value);
                }

                var books = await query.ToListAsync();

                if (books == null || !books.Any())
                {
                    return new NotFoundObjectResult("Книги с указанным запросом не найдены.");
                }

                var booksDto = books.Select(b => new GetAllBooksName
                {
                    Id_Books = b.Id_Books,
                    Title = b.Title,
                    Autor = b.Autors.FName + " " + b.Autors.LName,
                    Genre = b.Genres.Title,
                    AvailableCopies = b.AvailableCopies,
                    YearOfPublication = b.YearOfPublication,
                    Description = b.Description
                });

                return new OkObjectResult(booksDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> UpdateBook(int id, [FromQuery] CreateNewBooks updateBooks)
        {
            if (id <= 0 || updateBooks == null)
            {
                return new BadRequestObjectResult("Некорректные данные для обновления книги.");
            }
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return new NotFoundObjectResult("Книга с указанным идентификатором не найдена.");
                }

                book.Title = updateBooks.Title;
                book.YearOfPublication = updateBooks.YearOfPublication;
                book.Description = updateBooks.Description;
                book.AvailableCopies = updateBooks.AvailableCopies;
                book.Id_Genre = updateBooks.Id_Genre;
                book.Id_Autors = updateBooks.Id_Autors;

                _context.Books.Update(book);
                await _context.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
