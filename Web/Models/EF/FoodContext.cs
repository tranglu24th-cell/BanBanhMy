using Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Web.Models.EF
{
    public class FoodContext: DbContext
    {
        public FoodContext(DbContextOptions<FoodContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(
                new Group()
                {
                    Id = Guid.Parse("D0CFDF00-AFC9-4567-A9EC-0F0DB44A18BD"),
                    Name = "Quản trị viên" 
                }
            );
            modelBuilder.Entity<Member>().HasData(
                new Member()
                {
                    Id = Guid.Parse("FD48367D-F4A1-4E0B-A1F6-9D72AFCEBCC9"),
                    Name = "Lâm Uyên Trang",
                    Picture ="/img/users/uyentrang.jpg",
                    LoginName="tranglu",
                    Password= "c4ca4238a0b923820dcc509a6f75849b",
                    Email="tranglu.24th@sv.dla.edu.vn",
                    CreatedOn= DateTime.Now,
                    GroupId = Guid.Parse("D0CFDF00-AFC9-4567-A9EC-0F0DB44A18BD")
                }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = Guid.Parse("5EA90800-EC76-4E3C-83F5-0D7446510385"),
                    Name="Root",
                    CreatedBy= Guid.Parse("FD48367D-F4A1-4E0B-A1F6-9D72AFCEBCC9"),
                    CreatedOn = DateTime.Now

                },
                 new Category()
                 {
                     Id = Guid.Parse("D951DD74-A153-408C-AB60-44E51BB51F47"),
                     Name = "Authorized",
                     CreatedBy = Guid.Parse("FD48367D-F4A1-4E0B-A1F6-9D72AFCEBCC9"),
                     CreatedOn = DateTime.Now,
                     ParentId = Guid.Parse("5EA90800-EC76-4E3C-83F5-0D7446510385"),

                 }, 
                 new Category()
                 {
                     Id = Guid.Parse("74D373C3-DCF6-4634-8A46-7700B82DBE4D"),
                     Name = "Nhóm quyền",
                     CreatedBy = Guid.Parse("FD48367D-F4A1-4E0B-A1F6-9D72AFCEBCC9"),
                     CreatedOn = DateTime.Now,
                     ParentId = Guid.Parse("D951DD74-A153-408C-AB60-44E51BB51F47")

                 },

                 new Category()
                 {
                     Id = Guid.Parse("86700B84-DE54-426A-9748-DA1BCE88E424"),
                     Name = "Article",
                     CreatedBy = Guid.Parse("FD48367D-F4A1-4E0B-A1F6-9D72AFCEBCC9"),
                     CreatedOn = DateTime.Now,
                     ParentId = Guid.Parse("5EA90800-EC76-4E3C-83F5-0D7446510385"),

                 },
                 

                 new Category()
                 {
                     Id = Guid.Parse("6B04BD80-C414-4836-AC8C-CA215B574F41"),
                     Name = "Product",
                     CreatedBy = Guid.Parse("FD48367D-F4A1-4E0B-A1F6-9D72AFCEBCC9"),
                     CreatedOn = DateTime.Now,
                     ParentId = Guid.Parse("5EA90800-EC76-4E3C-83F5-0D7446510385"),

                 }
            );
            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id= Guid.Parse("B5F8852E-1A0F-4FEE-A821-1C982C33F9AA"),
                    Name="Xem danh sách",
                    Code="view-groups",
                    CategoryId = Guid.Parse("74D373C3-DCF6-4634-8A46-7700B82DBE4D")
                },
                new Role()
                {
                    Id = Guid.Parse("1C229F8C-8D58-4DE2-B7A5-96DDE1BB9263"),
                    Name = "Cập nhật",
                    Code = "edit-group",
                    CategoryId = Guid.Parse("74D373C3-DCF6-4634-8A46-7700B82DBE4D")
                },
                new Role()
                {
                    Id = Guid.Parse("A03BA290-D4A5-45CD-A255-BE0A8ED3851D"),
                    Name = "Lưu",
                    Code = "save-group",
                    CategoryId = Guid.Parse("74D373C3-DCF6-4634-8A46-7700B82DBE4D")
                },
                new Role()
                {
                    Id = Guid.Parse("70B8D810-029D-438F-9B41-F5ED8CBD7A31"),
                    Name = "Xóa",
                    Code = "delete-group",
                    CategoryId = Guid.Parse("74D373C3-DCF6-4634-8A46-7700B82DBE4D")
                }
            );
            // Đảm bảo tên đăng nhập (Email) là duy nhất khi đã có giá trị dưới database thực tế
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.LoginName)
                .IsUnique()
                .HasFilter("[LoginName] IS NOT NULL")
                .HasDatabaseName("IX_Customer_LoginName");

            modelBuilder.Entity<Authorized>().HasData(
                new Authorized()
                {
                    Id= Guid.NewGuid(),
                    GroupId= Guid.Parse("D0CFDF00-AFC9-4567-A9EC-0F0DB44A18BD"),
                    RoleId= Guid.Parse("B5F8852E-1A0F-4FEE-A821-1C982C33F9AA")

                }
                );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Authorized> Authorizeds { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Details> Details { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
