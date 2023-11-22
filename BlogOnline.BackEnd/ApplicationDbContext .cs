using BlogOnline.BackEnd.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace BlogOnline.BackEnd
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<FileUpload> FileUploads { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public IDbConnection CreateConnection()
        {
            var connectionString = "Data Source=DESKTOP-T3FD2G7;Initial Catalog=BlogOnline;Integrated Security=True;TrustServerCertificate=True";
            return new SqlConnection(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
            .Entity<Blog>()
            .Ignore(e => e.FileIds)
            .HasOne(typeof(User))
            .WithMany()
            .HasForeignKey("CreatedBy"); ;

            modelBuilder.Entity<User>()
                .ToTable("AspNetUsers");
        }
    }
}
