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
			var orderdetails = AddOrderDetails();
			var orderStatuses = AddOrderStatuses();
			var paymentMethods = AddPaymentMethods();
            var orders = AddOrders(paymentMethods, orderdetails, orderStatuses);

            var customers = AddCustomers(comments,orders);
			var products = AddProducts(discounts, images, comments, orderdetails,caterories);
            var carts = AddCarts(products);
          

        }

        private List<Cart> AddCarts(IList<Product> products)
        {
            throw new NotImplementedException();
        }

        private IList<Order> AddOrders(IList<PaymentMethod> paymentMethods, IList<OrderDetail> orderdetails, object orderStatuses)
        {
            throw new NotImplementedException();
        }

        private IList<Product> AddProducts(IList<Discount> discounts, IList<Image> images, object comments, object orderdetails, object caterories)
        {
            throw new NotImplementedException();
        }

  
        private IList<Customer> AddCustomers(IList<Comment> comments, IList<Order> orders)
        {
            throw new NotImplementedException();
        }

        private IList<PaymentMethod> AddPaymentMethods()
        {
            throw new NotImplementedException();
        }

        private IList<OrderStatus> AddOrderStatuses()
        {
            throw new NotImplementedException();
        }

        private IList<OrderDetail> AddOrderDetails()
        {
            throw new NotImplementedException();
        }

       

       

       

        private IList<Image> AddImages()
        {
            throw new NotImplementedException();
        }

        private IList<Discount> AddDiscounts()
        {
            throw new NotImplementedException();
        }

        private IList<Category> AddCategories()
        {
            throw new NotImplementedException();
        }

        private IList<Comment> AddComments()
        {
            throw new NotImplementedException();
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

