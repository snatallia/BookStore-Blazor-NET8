using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.API.Data;
using AutoMapper;
using BookStore.API.Models.Book;
using AutoMapper.QueryableExtensions;
using BookStore.API.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using BookStore.API.Repositories;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository booksRepository;
        private readonly IMapper mapper;
        private readonly ILogger<BooksController> logger;
        private readonly IWebHostEnvironment webHost;

        public BooksController(IBooksRepository booksRepository, IMapper mapper, ILogger<BooksController> logger, IWebHostEnvironment webHost)
        {
            this.booksRepository = booksRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.webHost = webHost;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            try
            {
                var books = await booksRepository.GetAllBooksAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performing GET in {nameof(GetBooks)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBookById(int id)
        {
            try
            {
               
                var book = await booksRepository.GetBookAsync(id);

                if (book == null)
                {
                    logger.LogWarning($"Record Not Found: {nameof(GetBookById)} - ID: {id}");
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performing GET in {nameof(GetBookById)}");
                return StatusCode(500, Messages.Error500Message);
            }
            
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            if (id != bookDto.Id)
            {
                logger.LogWarning($"Update ID invalid in {nameof(PutBook)} - ID: {id}");
                return BadRequest();
            }
            

            if (!string.IsNullOrEmpty(bookDto.ImageData))
            {
                var bookold = await booksRepository.GetAsync(id);

                var picName = Path.GetFileName(bookold?.Image);
                var path = $"{webHost.WebRootPath}\\bookcoverimages\\{picName}";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }


                bookDto.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
            }

            var book = mapper.Map<Book>(bookDto);

            try
            {
                await booksRepository.UpdateAsync(book);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError(ex, $"Error Performing GET in {nameof(PutBook)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Book>> PostBook(BookCreateDto bookDto)
        {
            try
            {
                var book = mapper.Map<Book>(bookDto);
                book.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
                await booksRepository.AddAsync(book);

                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing POST in {nameof(PostBook)}", bookDto);
                return StatusCode(500, Messages.Error500Message);
            }
            
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await booksRepository.GetAsync(id);
                if (book == null)
                {
                    logger.LogWarning($"Record Not Found: {nameof(GetBookById)} - ID: {id}");
                    return NotFound();
                }

                await booksRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> BookExists(int id)
        {
            return await booksRepository.Exits(id);
        }

        private string CreateFile(string imageBase64, string imageName)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var filename = $"{ Guid.NewGuid().ToString()}{ext}";
            var path = $"{webHost.WebRootPath}\\bookcoverimages\\{filename}";

            byte[] image = Convert.FromBase64String(imageBase64);


            using var fileStream = System.IO.File.Create(path);
            fileStream.Write(image, 0, image.Length);
            fileStream.Close();

            return $"https://{url}/bookcoverimages/{filename}";
        }
    }
}
