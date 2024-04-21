using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly FarmDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly List<IdentityUserRole<string>> _userRoles;

        public DataSeeder(FarmDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, List<IdentityUserRole<string>> userRoles)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRoles = userRoles;
        }
       
        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();
            if (_dbContext.Products.Any())
                return;

            var caterories = AddCategories();
            var customUI = AddCustomUI();
            var units = AddUnits();
            var users = AddUsers();
            var roles = SeedRoles();
            var paymentMethods = AddPaymentMethods();
            var orderStatuses = AddOrderStatuses();
            var discounts = AddDiscounts();
            var orders = AddOrders(users, paymentMethods,orderStatuses, discounts );
            var userRole = AddUserRoles(users, roles);
            var products = AddProducts(caterories, units);
            var comments = AddComments(products, users);
            var images = AddImages(products);
            var orderItems = AddOrderItems(products, orders);
        }

        private List<IdentityUserRole<string>> AddUserRoles(List<ApplicationUser> users, List<IdentityRole> roles)
        {
            // Seed UserRoles
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Admin").Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "User").Id
            });

            return userRoles;
        }

        private List<Unit> AddUnits()
        {
            var units = new List<Unit>() {
                new()
                {
                    Name ="kg",
                    UrlSlug="kg",
                },
                new()
                {
                    Name ="cái",
                    UrlSlug="cai",
                }
            };
            foreach (var unit in units)
            {
                if (!_dbContext.Units.Any(x => x.UrlSlug == unit.UrlSlug))
                {
                    _dbContext.Add(unit);
                }
            }
            _dbContext.AddRange(units);
            _dbContext.SaveChanges();
            return units;

        }

        private List<CustomUI> AddCustomUI()
        {
            var customUIs = new List<CustomUI>() {
            new(){
                Title= "Test",
                Color = "red",
                Image="",

            },
             new(){
                Title= "GreenTest",
                Color = "green",
                Image="",

            }
           };
            _dbContext.AddRange(customUIs);
            _dbContext.SaveChanges();
            return customUIs;
        }


        private IList<Product> AddProducts(IList<Category> caterories, IList<Unit> units)
        {
            var products = new List<Product>() {

                new(){
                    Name="Rau muống",
                    UrlSlug="rau-muong",
                   QuantityAvailable=66,
                   Price=20000,
                   PriceVirtual=25000,
                   Description="rau sach",
                   ViewCount=1,
                   DateCreate= new DateTime(2023,12,12),
                   Unit=units[0],
                   DateUpdate=DateTime.Now,
                   Category= caterories[0],

                },
                new(){
                    Name="Rau cải bắp",
                    UrlSlug="rau-cai-bap",
                   QuantityAvailable=38,
                   Price=45000,
				   PriceVirtual=50000,
				   Description="Rau cải bắp sạch",
				   ViewCount=5,
				   DateCreate= new DateTime(2023,08,12),
                   DateUpdate=DateTime.Now,
                   Unit=units[0],
                   Category= caterories[0],


                },
                    new(){
                    Name="Củ cải",
                    UrlSlug="cu-cai",
                   QuantityAvailable=8,
                   Price=20000,
				   PriceVirtual=25000,
				   Description="Củ cải",
                   ViewCount=3,
                   DateCreate= new DateTime(2023,03,12),
                   DateUpdate=DateTime.Now,
                   Category= caterories[1],
                   Unit=units[0],

                },
            };
            foreach (var product in products)
            {
                if (!_dbContext.Products.Any(p => p.UrlSlug == product.UrlSlug))
                {
                    _dbContext.Add(product);
                }
            }
            _dbContext.AddRange(products);
            _dbContext.SaveChanges();
            return products;
        }
        private List<IdentityRole> SeedRoles()
        {
            var adminRole = new IdentityRole("Admin");
            adminRole.NormalizedName = adminRole.Name!.ToUpper();

            var memberRole = new IdentityRole("User");
            memberRole.NormalizedName = memberRole.Name!.ToUpper();

            List<IdentityRole> roles = new List<IdentityRole>() {
           adminRole,
           memberRole
        };

            return roles;
        }

        private List<ApplicationUser> AddUsers()
        {
            string pwd = "Amin@123";
            var passwordHasher = new PasswordHasher<IdentityUser>();

            // Seed Users
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };
            adminUser.NormalizedUserName = adminUser.UserName.ToUpper();
            adminUser.NormalizedEmail = adminUser.Email.ToUpper();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, pwd);

            var memberUser = new ApplicationUser
            {
                UserName = "user",
                Name = "hung",
                Email = "user@gmail.com",
                PhoneNumber = "097979797",
                Address = "Da Lat",
                EmailConfirmed = true,
            };
            memberUser.NormalizedUserName = memberUser.UserName.ToUpper();
            memberUser.NormalizedEmail = memberUser.Email.ToUpper();
            memberUser.PasswordHash = passwordHasher.HashPassword(memberUser, pwd);

            List<ApplicationUser> users = new List<ApplicationUser>() {
           adminUser,
           memberUser,
        };

            _dbContext.ApplicationUsers.Add(memberUser);
            _dbContext.ApplicationUsers.Add(adminUser);

            _dbContext.SaveChanges();
            return users;
        }


        private List<Order> AddOrders(List<ApplicationUser> users, IList<PaymentMethod> paymentMethods, IList<OrderStatus> orderStatuses, IList<Discount> discounts)
        {
            var orders = new List<Order>();

           
                var order = new Order
                {
                    ApplicationUser = users[1],
                    TotalPrice = 400000,
                    DateOrder = DateTime.Now,
                    OrderStatus = orderStatuses[1],
                    PaymentMethod = paymentMethods[1],
                    Discount = discounts[1],

                };

                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();
                orders.Add(order);
            

            return orders;
        }
        private IList<PaymentMethod> AddPaymentMethods()
        {
            var paymentMethods = new List<PaymentMethod>() {

                new(){
                    Name="QR Pay",
                    Description="QR",
                  
                },
                 new(){
                    Name="Thanh toán trực tiếp",
                    Description="Thanh toán trực tiếp khi nhận hàng",


                }
            };
            foreach (var paymentMethod in paymentMethods)
            {
                if (!_dbContext.PaymentMethods.Any(p => p.Name == paymentMethod.Name))
                {
                    _dbContext.Add(paymentMethod);
                }
            }
            _dbContext.AddRange(paymentMethods);
            _dbContext.SaveChanges();
            return paymentMethods;
        }
        private IList<OrderStatus> AddOrderStatuses()
        {
            var orderStatuses = new List<OrderStatus>() {
                new(){
                   StatusCode="Chờ xác nhận",
                   Description="",
                   StatusDate=new DateTime(2024,2, 27),

                },
                 new(){
                   StatusCode="Đã xác nhận",
                   Description="",
                   StatusDate=new DateTime(2024,2, 27),
                },
                  new(){
                   StatusCode="Đang giao",
                   Description="",
                   StatusDate=new DateTime(2024,2, 27),
                },
                   new(){
                   StatusCode="Đã giao",
                   Description="",
                   StatusDate=new DateTime(2024,2, 27),
                }

            };
            foreach (var orderStatus in orderStatuses)
            {
                if (!_dbContext.OrderStatuses.Any(o => o.StatusCode == orderStatus.StatusCode))
                {
                    _dbContext.Add(orderStatus);
                }
            }
            _dbContext.AddRange(orderStatuses);
            _dbContext.SaveChanges();
            return orderStatuses;
        }









        private IList<Image> AddImages(IList<Product> products)
        {
            var images = new List<Image>() {
                new(){
                    Name="Hinh1",
                    UrlImage="",
                    Caption="Hình sản phẩm 1",
                    Product= products[0]

                },
                 new(){
                    Name="Hinh2",
                    UrlImage="",
                    Caption="Hình sản phẩm 2",
                     Product= products[1]
                },
                  new(){
                    Name="Hinh3",
                    UrlImage="",
                    Caption="Hình sản phẩm 3",
                     Product= products[2]
                },
            };
            foreach (var image in images)
            {
                if (!_dbContext.Images.Any(i => i.Name == image.Name))
                {
                    _dbContext.Add(image);
                }
            }
            _dbContext.AddRange(images);
            _dbContext.SaveChanges();
            return images;
        }

        private IList<Discount> AddDiscounts()
        {
            var discounts = new List<Discount>() {
            new(){
                    DiscountPrice=50000,
                    StartDate= DateTime.Now,
                    EndDate=(DateTime.Now).AddDays(7),
                    CodeName="Discount50",
                },
             new(){
                    DiscountPrice=20000,
                    StartDate= DateTime.Now,
                    EndDate=(DateTime.Now).AddDays(10),
                    CodeName="Discount20",
                }
            };
            foreach (var discount in discounts)
            {
                if (!_dbContext.Discounts.Any(d => d.Id == discount.Id))
                {
                    _dbContext.Add(discount);
                }

            }
            _dbContext.AddRange(discounts);
            _dbContext.SaveChanges();
            return discounts;
        }

        private IList<Category> AddCategories()
        {
            var categories = new List<Category>() {

                new()
                {

                Name = "Rau",
                UrlSlug= "rau",

                },
                 new()
                {

                Name = "Củ",
                UrlSlug= "cu",

                },
                  new()
                {

                Name = "Quả",
                UrlSlug= "qua",

                },
                   new()
                {

                Name = "Gia vị",
                UrlSlug= "gia-vi",

                }
            };
            foreach (var cate in categories)
            {
                if (!_dbContext.Categories.Any(c => c.UrlSlug == cate.UrlSlug))
                {
                    _dbContext.Add(cate);
                }
            }
            _dbContext.AddRange(categories);
            _dbContext.SaveChanges();
            return categories;
        }

        private IList<Comment> AddComments(IList<Product> products, List<ApplicationUser> users)
        {
            var comments = new List<Comment>();

            
                var comment = new Comment()
                {
                    Name = "comment1",
                    CommentText = "Hay qua",
                    Created = DateTime.Now,
                    Status = false,
                    Rating = 5,
                    ApplicationUser = users[1],
                    Product = products[0]
                };

                _dbContext.Add(comment);
                _dbContext.SaveChanges();
                comments.Add(comment);
            

            return comments;
        }

        private List<OrderItem> AddOrderItems(IList<Product> products, IList<Order> orders)
        {
            var r = new Random();
            var orderItems = new List<OrderItem>();

            var orderItem = new OrderItem()
            {
                Order= orders[0],
                Product = products[0],
                Quantity = 1000,
                Price= 0
            };

            // Thêm danh sách OrderItems vào DbContext và lưu thay đổi vào cơ sở dữ liệu
            _dbContext.AddRange(orderItem);
            _dbContext.SaveChanges();

            return orderItems;
        }



    }
}

