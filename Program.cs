using EFCore01.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFCore01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext context = new AppDbContext();

            Console.WriteLine($"Database Exists: {context.Database.CanConnect()}");

            if (!context.Books.Any())
            {
                Author author = new Author()
                {
                    FirstName = "Naguib",
                    LastName = "Mahfouz",
                    Email = "naguib@example.com",
                    Biography = "Egyptian writer and Nobel laureate.",
                    DateOfBirth = new DateTime(1911, 12, 11)
                };

                Category category = new Category()
                {
                    Name = "Novels",
                    Description = "Fiction novels",
                    IsActive = true
                };

                Book book = new Book()
                {
                    Title = "Palace Walk",
                    ISBN = "9780385264662",
                    Price = 15.99m,
                    NumberOfPages = 498,
                    PublicationYear = 1956,
                    InStock = true,
                    Author = author,
                    Category = category
                };

                context.Books.Add(book);
                context.SaveChanges();
            }

            var books = context.Books.Include(b => b.Author).Include(b => b.Category).ToList();

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title} | {b.ISBN} | {b.Price} | {b.NumberOfPages} | {b.PublicationYear} | {b.InStock}");
                Console.WriteLine($"Author: {b.Author.FirstName} {b.Author.LastName} | Category: {b.Category.Name}");
            }
        }
    }
}