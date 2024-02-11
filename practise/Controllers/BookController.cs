using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using practise.DTO;
using practise.Entitys;
using practise.Services;

namespace practise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService BookService;

        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, IMapper mapper, ILogger<BookController> logger)
        {
            this.BookService = bookService;
            _mapper = mapper;
            this._logger = logger;
        }
        [HttpGet, Route("GetAllBooks")]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                List<Book>  books =  BookService.GetAllBooks();
                List<BookDTO> booksDTO = _mapper.Map<List<BookDTO>>(books);
                return StatusCode(200, booksDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("AddBook")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBook(BookDTO bookDTO)
        {
            try
            {
                Book book = _mapper.Map<Book>(bookDTO);
                BookService.AddBook(book);
                return StatusCode(200, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetBookById/{bookId}")]
        [Authorize]


        public IActionResult Get(int bookId)
        {
            try
            {
                Book book = BookService.GetBookById(bookId);
                BookDTO bookDTO = _mapper.Map<BookDTO>(book);
                if (book != null)
                    return StatusCode(200, bookDTO);
                else
                    return StatusCode(404, new JsonResult("Invalid Id"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("EditBook")]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(BookDTO bookDTO)
        {
            try
            {
                Book book = _mapper.Map<Book>(bookDTO);
                BookService.UpdateBook(book);
                return StatusCode(200, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteBook/{bookId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int bookId)
        {
            try
            {
                BookService.DeleteBook(bookId);
                return StatusCode(200, new JsonResult($"Book with Id {bookId} is Deleted"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }
        [HttpGet, Route("GetBookByName")]
        [AllowAnonymous]
        public IActionResult GetBookByName(string search)
        {
            try
            {
                List<Book> books = BookService.GetBookByName(search);
                List<BookDTO> bookDTO = _mapper.Map<List<BookDTO>>(books);
                

                if (books.Count > 0)
                    return StatusCode(200, bookDTO);
                else
                    return StatusCode(404, new JsonResult("No Books found with the specified title"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }
    }
}
