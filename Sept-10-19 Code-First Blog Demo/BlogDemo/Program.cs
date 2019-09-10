
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BlogDemo.Entities;
using System.Data.Entity;
using BlogDemo.DAL;

namespace BlogDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create and save a new blog
                // Console.Write("Enter a name for a new blog: ");
                // var name = Console.ReadLine();

                // var blog = new Blog { Name = name };
                // db.Blogs.Add(blog);
                // db.SaveChanges();

                // Display all the blogs from the database
                var allBlogs = db.Blogs.ToList();

                Console.WriteLine($"There are {allBlogs.Count} blogs in the database.");
                foreach(var item in allBlogs)
                    Console.WriteLine($"{item.BlogID} {item.Name}");

                // TODO: Get the user to choose a blog, and then add a post to that blog.

                Console.WriteLine("Choose a blog (enter blog number): ");
                var blogID = Console.ReadLine();

                foreach (var item in allBlogs)
                {
                    if (blogID == item.BlogID.ToString())
                    {
                        Console.Clear();
                        Console.WriteLine($"You chose {item.Name}.");

                        Console.WriteLine("Enter the title of your blog post: ");
                        var postTitle = Console.ReadLine();
                        Console.WriteLine("Enter the content of your blog post: ");
                        var postContent = Console.ReadLine();

                        var post = new Post
                        {
                            Title = postTitle,
                            Content = postContent,
                            BlogID = int.Parse(blogID)
                        };

                        db.Posts.Add(post);
                        db.SaveChanges();
                    }



                }

            }
        }
    }

    namespace Entities
    {
        // TODO: Follow up by making changes to entities and trying out Database Migrations
        // https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database

        public class Blog
        {
            public int BlogID { get; set; }
            public string Name { get; set; }

            public virtual ICollection<Post> Posts { get; set; }
        }

        public class Post
        {
            public int PostID { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public int BlogID { get; set; }


            public virtual Blog Blog { get; set; }
        }
    }

    namespace DAL
    {
        public class BloggingContext : DbContext
        {
            public BloggingContext() : base("name=BlogDb")
            {

            }
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }
        }
    }
}
