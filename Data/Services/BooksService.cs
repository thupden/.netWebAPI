﻿using my_books.Data.Models;
using my_books.Data.ViewModels;
using my_books.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        //This function add data to the database
        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId

            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach(var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };

                _context.Book_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        //This function returns the list of all books
        public List<Book> GetAllBooks()
        {

            var allBooks = _context.Books.ToList();
            return allBooks;
        }

        //This function returns books on the basis of book Id
        public BookWithAuthorVM GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(n=>n.Id == bookId).Select(book => new BookWithAuthorVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()

            }).FirstOrDefault();

            return _bookWithAuthors;
        }

        //This function update books 
        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if(_book !=null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
            }

            return _book;
        }

        //This function delete books
        public void DeleteBookById(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if(_book!=null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
