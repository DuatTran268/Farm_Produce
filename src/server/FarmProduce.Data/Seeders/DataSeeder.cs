﻿using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
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
			
			
			
		
			
			
		

            var customUI = AddCustomUI();

          
            var customers = AddCustomers();
            var orders = AddOrders(customers);
            var products = AddProducts(caterories, orders);
            var comments = AddComments(customers,products);
           
            var images = AddImages(products);
            var carts = AddCarts(products);
            var discounts = AddDiscounts(products);
            var orderStatuses = AddOrderStatuses(orders);
            var paymentMethods = AddPaymentMethods(orders);


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

        private List<Cart> AddCarts(IList<Product> products)
        {
           var carts = new List<Cart>() {
               new()
               {
                  Quantity = 1,
                  AddedDate = new DateTime(2023,12,12),
                  Products = new List<Product>()
                  {
                      products[0]
                  }
               }
           };
            _dbContext.AddRange(carts);
            _dbContext.SaveChanges();
            return carts;
        }

        private IList<Order> AddOrders(IList<Customer> customers)
        {
            var orders = new List<Order>() {

            new(){
                DateOrder=new DateTime(2023, 12, 28),
                TotalPrice= 0,
                Customer=customers[0]
              
               
            }

            };
            _dbContext.AddRange(orders);
            _dbContext.SaveChanges();
            return orders;
        }
       
        private IList<Product> AddProducts(   IList<Category> caterories, IList<Order> orders)
        {
            var products = new List<Product>() {

                new(){
                    Name="Rau muống",
                    UrlSlug="rau-muong",
                   QuanlityAvailable=6,
                   Unit="kg",
                   Price=2000,
                   Description="rau sach",
                   DateCreate= new DateTime(2023,12,12),
                   DateUpdate=DateTime.Now,
                   Category= caterories[0],
                  
                  Orders= new List<Order>()
                  {
                      orders[0]
                  }
                },
				new(){
					Name="Rau cải bắp",
					UrlSlug="rau-cai-bap",
				   QuanlityAvailable=3,
				   Unit="kg",
				   Price=2000,
				   Description="Rau cải bắp sạch",
				   DateCreate= new DateTime(2023,08,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[0],

				  Orders= new List<Order>()
				  {
					  orders[0]
				  }
				},

				new(){
					Name="Củ cải",
					UrlSlug="cu-cai",
				   QuanlityAvailable=8,
				   Unit="kg",
				   Price=4000,
				   Description="Củ cải",
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[1],

				  Orders= new List<Order>()
				  {
					  orders[0]
				  }
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


        private IList<Customer> AddCustomers()
        {
            var customers = new List<Customer>(){

                new(){
                Name="Hung",
                Email="hung@gmail.com",
                Phone="0979797979",
                Address="Đà Lạt",
              }
            };
            foreach (var customer in customers)
            {
                if(_dbContext.Customers.Any(c=>c.Email== customer.Email))
                {
                    _dbContext.Add(customer);
                }
            }
           _dbContext.SaveChanges();
            return customers;
        }

        private IList<PaymentMethod> AddPaymentMethods(IList<Order> orders)
        {
            var paymentMethods = new List<PaymentMethod>() {

                new(){
                    Name="QR Pay",
                    Description="QR",
                    Order= orders[0]
                },
                 new(){
                    Name="Thanh toán trực tiếp",
                    Description="Thanh toán trực tiếp khi nhận hàng",
                    Order= orders[0]


                }
            };
            foreach (var paymentMethod in paymentMethods)
            {
                if(!_dbContext.PaymentMethods.Any(p=> p.Name==paymentMethod.Name))
                {
                    _dbContext.Add(paymentMethod);
                }
                
            }
            _dbContext.AddRange(paymentMethods);
            _dbContext.SaveChanges();
            return paymentMethods;
        }

        private IList<OrderStatus> AddOrderStatuses(IList<Order> orders) 
        {
            var orderStatuses = new List<OrderStatus>() {
                new(){
                   StatusCode="Chờ xác nhận",
                   Description="",
                   StatusDate=new DateTime(2024,2, 27),
                   Order= orders[0],
                   
                },
                 new(){
                   StatusCode="Đã xác nhận",
                   Description="",
                   StatusDate=new DateTime(2024,2, 27),
                   Order= orders[0],
                },
                  new(){
                   StatusCode="Đang giao",
                   Description="",
                   StatusDate=new DateTime(2024,2, 27),
                   Order= orders[0],
                },
                   new(){
                   StatusCode="Đã giao",
                   Description="",
                   StatusDate=new DateTime(2024,2, 27),
                   Order= orders[0],
                }
               
            };
            foreach (var orderStatus in orderStatuses)
            {
                if(!_dbContext.OrderStatuses.Any(o=> o.StatusCode== orderStatus.StatusCode))
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
                    Caption="caption",
                    Product= products[0]
                    
                },
                 new(){
                    Name="Hinh2",
                    UrlImage="",
                    Caption="caption",
                     Product= products[0]
                },
                  new(){
                    Name="Hinh3",
                    UrlImage="",
                    Caption="caption",
                     Product= products[0]
                },
            };
            foreach (var image in images)
            {
                if(!_dbContext.Images.Any(i=> i.Name== image.Name))
                {
                   _dbContext.Add(image);
                }
            }
            _dbContext.AddRange(images);
            _dbContext.SaveChanges();
            return images;
        }

        private IList<Discount> AddDiscounts(IList<Product> products)
        {
            var discounts = new List<Discount>() {

            new(){
                    DiscountPrice=50,
                    StartDate= DateTime.Now,
                    EndDate=(DateTime.Now).AddDays(7),
                    Status="Ongoing",
                    Products= products[0]
                },
             new(){
                    DiscountPrice=20,
                    StartDate= DateTime.Now,
                    EndDate=(DateTime.Now).AddDays(10),
                    Status="Ongoing",
                    Products=products[0]
                }
            };
            foreach (var discount in discounts)
            {
                if(!_dbContext.Discounts.Any(d=> d.Id== discount.Id))
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
                if(!_dbContext.Categories.Any(c=> c.UrlSlug== cate.UrlSlug))
                {
                    _dbContext.Add(cate);
                }
            }
            _dbContext.AddRange(categories);
            _dbContext.SaveChanges();
            return categories;
        }

        private IList<Comment> AddComments( IList<Customer> customers,IList<Product> products )
        {
            var comments = new List<Comment>()
            {
                new(){
                    Name="comment1",
                    CommentText="Hay qua",
                    Created= DateTime.Now,
                    Status=false,
                    Rating=5,
                    Customer =  customers[0],
                    Product= products[0]
                    
                    
                },
                 new(){
                    Name="comment2",
                    CommentText="Hay ghr",
                    Created= DateTime.Now,
                    Status=false,
                    Rating=5,
                    Customer =  customers[0],
                          Product= products[0]

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

