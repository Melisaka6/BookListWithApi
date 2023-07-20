using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private static List<Book> BookList = new List<Book>()
        {
           new Book
           {
               Id=1,
               Title="Lean Startup",
               GenreId = 1,
               PageCount = 200,
               PublishDate = new DateTime(2001,06,12)
           },

            new Book
           {
               Id=2,
               Title="Herland",
               GenreId = 2,
               PageCount = 250,
               PublishDate = new DateTime(2001,12,01)
           },

             new Book
           {
               Id=3,
               Title="Dune",
               GenreId = 3,
               PageCount = 300,
               PublishDate = new DateTime(2003,10,03)
           }

        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var booklist= BookList.OrderBy(x => x.Id).ToList<Book>();
            return booklist;
        }




        [HttpGet("{id}")]
        
        public Book GetById(int id)
        {
            var books = BookList.Where(Book => Book.Id == id).SingleOrDefault();
            return books; 
        }


        //Post örneği ekleme
        [HttpPost]

        public IActionResult AddBook([FromBody] Book newbook)
        {
            var Book = BookList.SingleOrDefault(x => x.Title == newbook.Title);

            if (Book is not null)
                return BadRequest();

            BookList.Add(newbook);
            return Ok();

        }
        //Put örneği güncelleme

        [HttpPut("{id}")]
       
        public IActionResult UpdateBook(int id, [FromBody] Book updatedbook)
        {
            var book=BookList.SingleOrDefault(x=>x.Id==id);

            if(book is null)
                return BadRequest();

            book.GenreId= updatedbook.GenreId != default ? updatedbook.GenreId : book.GenreId;
            book.PageCount = updatedbook.PageCount != default ? updatedbook.PageCount : book.PageCount;
            book.PublishDate = updatedbook.PublishDate != default ? updatedbook.PublishDate : book.PublishDate;
            book.Title = updatedbook.Title != default ? updatedbook.Title : book.Title;

            return Ok();
        }



        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteBook (int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
             if (book is null)
                return BadRequest();

             BookList.Remove(book);
            return Ok();
            
        }

    }
}
