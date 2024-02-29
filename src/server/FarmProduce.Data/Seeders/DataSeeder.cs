using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Data.Seeders
{
	public class DataSeeder : IDataSeeder
	{
		private readonly FarmDbContext _dbContext;

		public DataSeeder(FarmDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void Initialize()
		{
			_dbContext.Database.EnsureCreated();
			if (_dbContext.Products.Any())
				return;

			var admins = AddAdmins();
            var caterories = AddCategories();
			var comments = AddComments();
			var images = AddImages();
			
			var discounts = AddDiscounts();
			
			var orderStatuses = AddOrderStatuses();
			var paymentMethods = AddPaymentMethods();
          

			var products = AddProducts(discounts, images, comments,caterories);
            var orders = AddOrders(paymentMethods, orderStatuses, products);
            var customers = AddCustomers(comments, orders);

            var carts = AddCarts(products);
           


        }

        private List<Cart> AddCarts(IList<Product> products)
        {
            throw new NotImplementedException();
        }

        private IList<Order> AddOrders(IList<PaymentMethod> paymentMethods,  IList<OrderStatus> orderStatuses, IList<Product> products)
        {
            throw new NotImplementedException();
        }

        private IList<Product> AddProducts(IList<Discount> discounts, IList<Image> images, IList<Comment> comments,  IList<Category> caterories)
        {
            throw new NotImplementedException();
        }

  
        private IList<Customer> AddCustomers(IList<Comment> comments, IList<Order> orders)
        {
            var addCustomer = new List<Customer>()
            {
                new()
                {
                    Name = "Duật Trần",
                    Email = "duattn2@fpt.com",
                    Address = "Quận 9, Thành phố Hồ Chí Minh",
                    Phone = "0922223333",
                }

            };
			foreach (var addCustomers in addCustomer)
			{
				if (!_dbContext.Customers.Any(p => p.Name == addCustomers.Name))
				{
					_dbContext.Add(addCustomers);
				}

			}
			_dbContext.SaveChanges();
			return addCustomer;
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
                if(!_dbContext.PaymentMethods.Any(p=> p.Name==paymentMethod.Name))
                {
                    _dbContext.Add(paymentMethod);
                }
                
            }
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
                if(!_dbContext.OrderStatuses.Any(o=> o.StatusCode== orderStatus.StatusCode))
                {
                    _dbContext.Add(orderStatus);
                }
            }
            _dbContext.SaveChanges();
            return orderStatuses;
        }

      







        private IList<Image> AddImages()
        {
            var images = new List<Image>() {
                new(){
                    Name="Hinh1",
                    UrlImage="",
                    Caption="caption",
                    
                },
                 new(){
                    Name="Hinh2",
                    UrlImage="",
                    Caption="caption",

                },
                  new(){
                    Name="Hinh3",
                    UrlImage="",
                    Caption="caption",

                },
            };
            foreach (var image in images)
            {
                if(!_dbContext.Images.Any(i=> i.Name== image.Name))
                {
                   _dbContext.Add(image);
                }
            }
            _dbContext.SaveChanges();
            return images;
        }

        private IList<Discount> AddDiscounts()
        {
            var discounts = new List<Discount>() {

            new(){
                    DiscountPrice=50,
                    StartDate= DateTime.Now,
                    EndDate=(DateTime.Now).AddDays(7),
                    Status="Ongoing",
                },
             new(){
                    DiscountPrice=20,
                    StartDate= DateTime.Now,
                    EndDate=(DateTime.Now).AddDays(10),
                    Status="Ongoing",
                }
            };
            foreach (var discount in discounts)
            {
                if(!_dbContext.Discounts.Any(d=> d.Id== discount.Id))
                {
                    _dbContext.Add(discount);
                }
               
            }
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
                if(!_dbContext.Categories.Any(c=> c.UrlSlug== cate.UrlSlug))
                {
                    _dbContext.Add(cate);
                }
            }
            _dbContext.SaveChanges();
            return categories;
        }

        private IList<Comment> AddComments()
        {
            var comments = new List<Comment>()
            {
                new(){
                    Name="comment1",
                    CommentText="Hay qua",
                    Created= DateTime.Now,
                    Status=false,
                    Rating=5,
                    
                    
                },
                 new(){
                    Name="comment2",
                    CommentText="Hay ghr",
                    Created= DateTime.Now,
                    Status=false,
                    Rating=5,


				},
            };
            foreach (var comment in comments)
            {
             if(!_dbContext.Images.Any(c=> c.Name== comment.Name))
                {
                    _dbContext.Add(comment);
                }   
            }
			_dbContext.SaveChanges();
           return comments;
        }



        private IList<Admin> AddAdmins()
		{
			var admins = new List<Admin>()
			{
				new()
				{
					Name = "Trần Nhật Duật",
					Password = "123456",
					Email = "duattran36@gmail.com",
					Role = "admin",
				},
				new()
				{
					Name = "Nguyễn Xuân Hưng",
					Password = "234567",
					Email = "xuanhung42@gmail.com",
					Role = "admin",

				},
			};
			foreach (var admin in admins)
			{
				if (!_dbContext.Admins.Any(a => a.Name == admin.Name))
				{
					_dbContext.Admins.Add(admin);
				}
			}
			_dbContext.SaveChanges();

			return admins;

		}
	

	}
	}

