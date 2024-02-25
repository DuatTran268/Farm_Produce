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
			var buyers = AddBuyers();
			var orders = AddOrders(buyers);
			var orderStatus = AddOrdersStatus(orders);
			var paymentOptions = AddPaymentOptions(orders);
			var categories = AddCategories();
			var products = AddProducts(categories, orders);
			var comments = AddComments(products);
			var collectionImages = AddCollectionImages(products);

		}


		private IList<Admin> AddAdmins()
		{
			var admins = new List<Admin>()
			{
				new()
				{
					FullName = "Trần Nhật Duật",
					Password = "123456",
					Email = "duattran36@gmail.com",
					UrlSlug = "tran-nhat-duat",
				},
				new()
				{
					FullName = "Nguyễn Xuân Hưng",
					Password = "234567",
					Email = "xuanhung42@gmail.com",
					UrlSlug = "nguyen-xuan-hung",

				},
			};
			foreach (var admin in admins)
			{
				if (!_dbContext.Admins.Any(a => a.UrlSlug == admin.UrlSlug))
				{
					_dbContext.Admins.Add(admin);
				}
			}
			_dbContext.SaveChanges();

			return admins;

		}

		private IList<Buyer> AddBuyers()
		{
			var buyers = new List<Buyer>()
			{
				new()
				{
					Name = "Trần Duật",
					UrlSlug = "tran-duat",
					Email = "tranduat123@gmail.com",
					Address = "108 Lò Lu, Trường Thạnh, Quận 9, TPHCM",
					Phone = "0922223333",

				},
				new()
				{
					Name = "Tiến Nguyễn",
					UrlSlug = "tien-nguyen",
					Email = "tiennguyen123@gmail.com",
					Address = "10 Đống Đa, Phường 2, Thành phố Đà Lạt",
					Phone = "0922224444",

				},
				new()
				{
					Name = "Xuân Hưng",
					UrlSlug = "xuan-hung",
					Email = "xuanhung234@gmail.com",
					Address = "9 Trại Mát, Phường 11, Thành phố Đà Lạt",
					Phone = "0922225555",

				},
			};
			foreach (var buyer in buyers)
			{
				if (_dbContext.Buyers.Any(b => b.Id == buyer.Id))
				{
					_dbContext.Buyers.Add(buyer);
				}
			}
			_dbContext.SaveChanges();
			return buyers;

		}

		private IList<Category> AddCategories()
		{
			var categories = new List<Category>()
			{
				new()
				{
					Name = "Tất cả sản phẩm",
					UrlSlug = "tat-ca-san-pham",
					UrlIcon = "",

				},
				new()
				{
					Name = "Rau, củ , quả",
					UrlSlug = "rau-cu-qua",
					UrlIcon = "",

				},
				new()
				{
					Name = "Trái cây tươi",
					UrlSlug = "trai-cay-say-deo",
					UrlIcon = "",

				},
				new()
				{
					Name = "Hoa tươi - cây giống",
					UrlSlug = "hoa-tuoi-cay-giong",
					UrlIcon = "",

				},
			};
			foreach (var category in categories)
			{
				if (_dbContext.Categories.Any(c => c.Id == category.Id))
				{
					_dbContext.Categories.Add(category);
				}

			}
			_dbContext.SaveChanges();
			return categories;
		}

		private IList<CollectionImage> AddCollectionImages(IList<Products> products)
		{
			var collectionImages = new List<CollectionImage>()
			{
				new()
				{
					Name = "Rau Bắp cải",
					UrlSlug = "rau-bap-cai",
					Product = products[0],
				},
				new()
				{
					Name = "Hoa Chậu Đà Lạt",
					UrlSlug = "hoa-chau-da-lat",
					Product = products[1],
				}
			};
			foreach (var collectionImage in collectionImages)
			{
				if (_dbContext.CollectionImages.Any(i => i.UrlSlug == collectionImage.UrlSlug))
				{
					_dbContext.CollectionImages.Add(collectionImage);
				}
			}
			_dbContext.SaveChanges();
			return collectionImages;
		}


		private IList<Comment> AddComments(IList<Products> products)
		{
			var comments = new List<Comment>()
			{
				new()
				{
					UserName = "Trần Duật",
					UrlSlug = "tran-duat",
					Content = "Rau củ quả ngon",
					Created = new DateTime(2024,02,25),
					Status = true,
					Product = products[0],
				},
				new()
				{
					UserName = "Xuân Hưng",
					UrlSlug = "xuan-hung",
					Content = "Hoa rất tươi",
					Created = new DateTime(2024,02,26),
					Status = true,
					Product = products[1],
				},
			};
			foreach (var comment in comments)
			{
				if (_dbContext.Comments.Any(cm => cm.UrlSlug == comment.UrlSlug))
				{
					_dbContext.Comments.Add(comment);
				}
			}
			_dbContext.SaveChanges();
			return comments;

		}


		private IList<Order> AddOrders(IList<Buyer> buyers)
		{
			var orders = new List<Order>()
			{
				new()
				{
					Name = "Đơn hàng rau",
					UrlSlug = "don-hang-rau",
					DateOrder = new DateTime(2024,02,26),
					Note = "Giao nhanh nha em",
					Buyer = buyers[0],

				},
				new()
				{
					Name = "Đơn hàng hoa",
					UrlSlug = "don-hang-hoa",
					DateOrder = new DateTime(2024,02,26),
					Note = "Giao nhanh cho anh nha em",
					Buyer = buyers[1],
				},
			};
			foreach (var order in orders)
			{
				if (!_dbContext.Orders.Any(od => od.Id == order.Id))
				{
					_dbContext.Orders.Add(order);
				}
			}
			_dbContext.SaveChanges();
			return orders;
		}


		private IList<OrderStatus> AddOrdersStatus(IList<Order> orders)
		{
			var orderStatus = new List<OrderStatus>()
			{
				new()
				{
					Name = "Đặt hàng",
					UrlSlug = "dat-hang",
					Order = orders[0]
				},
				new()
				{
					Name = "Xác nhận",
					UrlSlug = "xac-nhan",
					Order = orders[0]
				},
				new()
				{
					Name = "Đang vận chuyển",
					UrlSlug = "dang-van-chuyen",
					Order = orders[0]
				},
				new()
				{
					Name = "Giao thành công",
					UrlSlug = "giao-thanh-cong",
					Order = orders[0]
				},
				new()
				{
					Name = "Hoàn trả",
					UrlSlug = "hoan-tra",
					Order = orders[0]

				},
				new()
				{
					Name = "Đã hoàn thành",
					UrlSlug = "da-hoan-thanh",
					Order = orders[0]

				}
			};
			foreach (var orderStatuses in orderStatus)
			{
				if (_dbContext.OrderStatuses.Any(od => od.UrlSlug == orderStatuses.UrlSlug))
				{
					_dbContext.OrderStatuses.Add(orderStatuses);

				}
			}
			_dbContext.SaveChanges();
			return orderStatus;
		}

		private IList<PaymentOption> AddPaymentOptions(IList<Order> orders)
		{
			var paymentOptions = new List<PaymentOption>()
			{
				new()
				{
					Name = "Chuyển khoản",
					UrlSlug = "chuyen-khoan",
					Order = orders[0]
				},
				new()
				{
					Name = "Trả tiền mặt khi nhận hàng",
					UrlSlug = "tra-tien-mat-khi-nhan-hang",
					Order = orders[0]
				},
			};
			foreach (var paymentOption in paymentOptions)
			{
				if (_dbContext.PaymentOptions.Any(pm => pm.UrlSlug == pm.UrlSlug))
				{
					_dbContext.PaymentOptions.Add(paymentOption);
				}
			}
			_dbContext.SaveChanges();
			return paymentOptions;
		}

		private IList<Products> AddProducts(IList<Category> categories, IList<Order> orders)
		{
			var products = new List<Products>()
			{
				new ()
				{
					Name = "Rau cải bắp",
					UrlSlug = "rau-cai-bap",
					Price = 20000,
					PriceDiscount = 18000,
					Image = "",
					ShortDescription = "Rau cải bắp xanh sạch từ Đà Lạt",
					Description = "Rau cải bắp xanh sạch tươi ngon được trồng từ vùng đất Đà Lạt",
					DateCreate = new DateTime(2024,02,25),
					DateUpdate = new DateTime(2024, 02, 26),
					Status = true,
					Category = categories[1],
					Order = orders[0],

				},
				new ()
				{
					Name = "Chậu hoa Lan Đà Lạt",
					UrlSlug = "chau-hoa-lan-da-lat",
					Price = 300000,
					PriceDiscount = 280000,
					Image = "",
					ShortDescription = "Chậu hoa Lan Đà Lạt",
					Description = "Chậu hoa Lan đẹp được trồng từ nhà kính ở Đà Lạt",
					DateCreate = new DateTime(2024,02,25),
					DateUpdate = new DateTime(2024, 02, 26),
					Status = true,
					Category = categories[1],
					Order = orders[0],

				},
			};
			foreach (var product in products)
			{
				if (_dbContext.Products.Any(pd => pd.Id == product.Id && pd.UrlSlug == product.UrlSlug))
				{
					_dbContext.Products.Add(product);
				}
			}
			_dbContext.SaveChanges();
			return products;
		}
	}
}
