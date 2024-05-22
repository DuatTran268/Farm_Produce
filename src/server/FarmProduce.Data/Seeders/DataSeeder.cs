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
			var orders = AddOrders(users, paymentMethods, orderStatuses, discounts);
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
					Name ="Hộp",
					UrlSlug="hop",
				}
				,
				new()
				{
					Name ="gam",
					UrlSlug="gam",
				}
				,
				new()
				{
					Name ="cây",
					UrlSlug="cay",
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
					Name="Cải bó xôi Đà Lạt",
					UrlSlug="cai-bo-xoi-da-lat",
				   QuantityAvailable=66,
				   Price=50000,
				   PriceVirtual=60000,
				   Description="Cải bó xôi hay còn gọi là rau chân vịt, được trồng nhiều ở Đà Lạt, khí hậu ôn đới nơi đây giúp những cây rau bó xôi phát triển mạnh mẽ và nó mang trong mình nhiều dưỡng chất nên được mệnh danh là siêu thực phẩm. DaLaVi cam kết rau bó xôi do chúng tôi cung cấp là sản phẩm sạch và chắc chắn không sử dụng các loại thuốc hóa học có hại.",
				   ViewCount=1,
				   DateCreate= new DateTime(2023,12,12),
				   Unit=units[0],
				   DateUpdate=DateTime.Now,
				   Category= caterories[0],

				},
				new(){
					Name="Cải bắp trắng Đà Lạt",
					UrlSlug="rau-cai-bap",
				   QuantityAvailable=38,
				   Price=45000,
				   PriceVirtual=0,
				   Description="Bắp cải trắng Đà Lạt có giá trị dinh dưỡng cao, lượng vitamin nhiều gấp 4,5 lần với cà rốt, 3,6 lần so với khoai tây, hành tây và đặc biệt có thể hạn chế bệnh ung thư nhờ các chất Sulforaphane,…là những chất có tác dụng chống ung thư mà không phải loại rau củ quả nào cũng có. DaLaVi mang bắp cải sạch đến với mọi gia đình Việt, được chăm sóc cẩn thận từ bàn tay người nông dân và các kỹ sư nông nghiệp, DaLaVi cam kết sản phẩm 100% sạch, không chất hóa học độc hại.",
				   ViewCount=5,
				   DateCreate= new DateTime(2023,08,12),
				   DateUpdate=DateTime.Now,
				   Unit=units[0],
				   Category= caterories[0],


				},
					new(){
					Name="Củ cải đỏ Baby Đà Lạt",
					UrlSlug="cu-cai-do-baby",
				   QuantityAvailable=8,
				   Price=80000,
				   PriceVirtual=0,
				   Description="Củ cải đỏ baby được mệnh danh là thực phầm vàng với nhiều chất dinh dưỡng có trong nó. Củ cải đỏ được trồng nhiều ở Đà Lạt, thuộc họ nhà cải và có màu đỏ đậm, hình tròn nhỏ và có đường kính 3 – 4cm, thịt bên trong màu trắng, giòn, có vị cay và hơi đắng, khi nấu chín có vị ngọt. DaLaVi là đơn vị cung cấp củ cải đỏ baby Đà Lạt sạch với tiêu chí vun trồng không chứa hóa chất độc hại.",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[0],
				   Unit=units[0],

				},
					new(){
					Name="Xà lách lô lô tím",
					UrlSlug="xa-lach-lo-lo-tim",
				   QuantityAvailable=100,
				   Price=55000,
				   PriceVirtual=60000,
				   Description="Xà lách lô lô tím hay còn gọi là xà lách lola rossa, có thân mềm, lá xếp rời rạc, kết cấu lá giòn, tán lá rộng và xoăn, được nhiều gia đình yêu thích vì độ ngon và giòn. DaLaVi cung cấp rau sạch với phương pháp thủy canh đảm bảo chất lượng sản phẩm và an toàn cho người sử dụng.",
				   ViewCount=5,
				   DateCreate= new DateTime(2023,08,12),
				   DateUpdate=DateTime.Now,
				   Unit=units[0],
				   Category= caterories[0],


				}, new(){
					Name="Xà lách Mỹ",
					UrlSlug="xa-lach-my",
				   QuantityAvailable=0,
				   Price=70000,
				   PriceVirtual=0,
				   Description="Xà lách mỹ là món ăn thường nhật trong bữa cơm mỗi gia đình Việt bởi tính phổ dụng, dễ ăn và đem lại nhiều dưỡng chất thiết yếu cho cơ thể. Món rau trộn từ xà lách cũng tạo cảm giác ngon miệng không chỉ cho người lớn mà còn cả trẻ em.",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[0],
				   Unit=units[0],

				},
					 new(){
					Name="Xà lách Romaine",
					UrlSlug="xa-lach-romaine",
				   QuantityAvailable=10,
				   Price=8000,
				   PriceVirtual=0,
				   Description="Xà lách Romaine có tác dụng hỗ trợ tiêu hóa và tốt cho gan, giảm nguy cơ mắc bệnh tim mạch, các cơn nhồi máu cơ tim, ung thư, nứt cột sống, thiếu máu, chứng mất ngủ do căng thẳng, hỗ trợ tiêu hóa và tốt cho gan, giảm nguy cơ mắc bệnh tim mạch, các cơn nhồi máu cơ tim, ung thư, nứt cột sống, thiếu máu, chứng mất ngủ do căng thẳng. Ngoài ra Vitamin C và Beta Carotene kết hợp với nhau để phòng chống sự oxy hóa cholesterol. ",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[0],
				   Unit=units[2],

				},
					  new(){
					Name="Xà lách mỡ",
					UrlSlug="xa-lach-mo",
				   QuantityAvailable=0,
				   Price=4000,
				   PriceVirtual=5000,
				   Description="Xà lách mỡ Đà Lạt được trồng theo phương pháp thủy canh nên có vị ngọt mát, có tác dụng giải nhiệt, lọc máu, khai vị (vào đầu bữa ăn, nó kích thích các tuyến tiêu hóa), cung cấp chất khoáng, giảm đau, gây ngủ, chống ho…",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[0],
				   Unit=units[2],

				},
				 new(){
					Name="Rau xà lách thuỷ tinh ",
					UrlSlug="xa-lach-mo",
				   QuantityAvailable=10,
				   Price=4500,
				   PriceVirtual=5000,
				   Description="Xà lách mỡ Đà Lạt được trồng theo phương pháp thủy canh nên có vị ngọt mát, có tác dụng giải nhiệt, lọc máu, khai vị (vào đầu bữa ăn, nó kích thích các tuyến tiêu hóa), cung cấp chất khoáng, giảm đau, gây ngủ, chống ho…",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[0],
				   Unit=units[2],

				},
				  new(){
					Name="Vú sữa hoàng kim",
					UrlSlug="vu-sua-hoang-kim",
				   QuantityAvailable=10,
				   Price=240000,
				   PriceVirtual=250000,
				   Description="Vú sữa hoàng kim – “nữ hoàng” trái cây nhiệt đới với hương vị thơm ngon, độc đáo cùng giá trị dinh dưỡng cao, vú sữa hoàng kim đang dần trở thành “nữ hoàng” trong các loại trái cây cao cấp.…",
				   ViewCount=8,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[0],
				   Unit=units[0],

				},
				   new(){
					Name="Quả dưa Pepino",
					UrlSlug="qua-dua-penino",
				   QuantityAvailable=10,
				   Price=16000,
				   PriceVirtual=0,
				   Description="Quả dưa Pepino là giống quả lai giữa dưa hấu và quả lê. Chúng được mệnh danh là một trong những loại dưa hấu ngon nhất thế giới với vị ngọt dịu. Dưa pepino thường được sử dụng để ăn tráng miệng, làm salad hay sinh tố. Một trái Pepino cung cấp rất nhiều vitamin, chất chống lão hóa và ung thư.",
				   ViewCount=9,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[2],
				   Unit=units[2],

				},new(){
					Name="Dưa pepino tím",
					UrlSlug="dua-pepino-tim",
				   QuantityAvailable=0,
				   Price=130000,
				   PriceVirtual=140000,
				   Description="Dưa pepino tím không chỉ có ngoại hình đẹp mà còn ngọt hơn, vị đậm hơn, mùi thơm dễ chịu hơn dưa pepino màu vàng. Hơn nữa, sắc tố tím còn mang đến hàm lượng chất chống oxy hóa vượt trội nhờ chứa lượng flavonoid và phenol dồi dào.",
				   ViewCount=11,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[2],
				   Unit=units[1],

				},new(){
					Name="Phúc bồn tử đỏ",
					UrlSlug="phuc-bon-tu-do",
				   QuantityAvailable=10,
				   Price=380000,
				   PriceVirtual=400000,
				   Description="Phúc bồn tử hay còn gọi là quả mâm xôi, là một loại trái cây giàu chất dinh dưỡng, vitamin có lợi cho sức khỏe và điều trị bệnh lý,.. Được trẻ em và người lớn yêu thích bởi vị ngọt và chua nhẹ.",
				   ViewCount=31,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[2],
				   Unit=units[0],

				}
				   ,new(){
					Name="Phúc bồn tử đen",
					UrlSlug="phuc-bon-tu-den",
				   QuantityAvailable=10,
				   Price=380000,
				   PriceVirtual=400000,
				   Description="Phúc bồn tử đen là một loại trái cây nhỏ, ngọt và có vị chua. Chúng là một nguồn cung cấp vitamin C tuyệt vời để hỗ trợ hệ thống miễn dịch và giúp chống lại nhiễm trùng.",
				   ViewCount=20,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[2],
				   Unit=units[0],

				}
					,new(){
					Name="Ớt trái cây Sweet Palermo",
					UrlSlug="palermo",
				   QuantityAvailable=10,
				   Price=180000,
				   PriceVirtual=0,
				   Description="Ớt trái cây Sweet Palermo được mệnh danh là vua của các loại ớt có hình dáng thuôn dài không cay, không hăng, có vị giòn, ngọt và mọng nước, có thể ăn trực tiếp như một loại trái cây.",
				   ViewCount=13,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[2],
				   Unit=units[1],

				}, new(){
					Name="Trái tầm bóp",
					UrlSlug="carolina-reaper",
				   QuantityAvailable=10,
				   Price=13000,
				   PriceVirtual=15000,
				   Description="Tầm bóp (thù lù) Nam Mỹ có tên khoa học là Physalis Peruviana có nguồn gốc từ dài núi Andes ở Nam Mỹ, có cùng họ hàng với cà chua bi và có đặc điểm là có vỏ bảo vệ tự nhiên trùm ở ngoài.",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[2],
				   Unit=units[2],

				},
				   new(){
					Name="Tinh chất phúc bồn tử đen",
					UrlSlug="tinh-chat-phuc-bon-tu-den",
				   QuantityAvailable=10,
				   Price= 600000,
				   PriceVirtual=0,
				   Description="Nước tinh chất Phúc bồn tử đen DaLaVi được chế biến từ 100% trái phúc bồn tử đen hữu cơ với đường phèn, sản phẩm được đánh giá cao dành cho sức khỏe với rất nhiều công dụng đi kèm.",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[1],
				   Unit=units[0],

				},
					  new(){
					Name="Hồng treo gió thượng hạng",
					UrlSlug="hong-treo-gio-thuong-hang",
				   QuantityAvailable=100,
				   Price=170000,
				   PriceVirtual=180000,
				   Description="Hồng treo gió Đà Lạt là sản phẩm thượng hạng do DaLaVi cung cấp với công nghệ treo gió hoshigaki được chuyển giao trực tiếp từ Nhật Bản. Trải qua 30 ngày công phu với cái nắng và gió tự nhiên mới cho ra những mẻ hồng treo đạt chất lượng tuyệt hảo.",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[1],
				   Unit=units[1], // hop

				},
						 new(){
					Name="Xoài sấy dẻo",
					UrlSlug="xoai-say-deo",
				   QuantityAvailable=10,
				   Price=75000,
				   PriceVirtual=0,
				   Description="Xoài sấy dẻo Đà Lạt từ lâu đã trở thành món “ăn vặt” được phần lớn chị em ưa chuộng và không khó để lựa chọn món quà biếu mỗi lần tết đến – xuân về, đặc biệt là mỗi lần ghé du lịch đến thành phố ngàn hoa.",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[1],
				   Unit=units[1],

				},

							new(){
					Name="Cafe đông trùng hạ thảo",
					UrlSlug="cafe-dong-trung",
				   QuantityAvailable=10,
				   Price=180000,
				   PriceVirtual=0,
				   Description="Cà phê Đông trùng Hạ Thảo DaLaVi dạng hòa tan (5 in 1), là sản phẩm dinh dưỡng được nhiều khách hàng/đối tác tin dùng. Với những ai ít hoặc không uống được cà phê truyền thống thì đến với Cà phê Đông trùng Hạ Thảo chắc chắn sẽ làm thay đổi thói quen bởi tính tiện dụng, phù hợp với mọi lứa tuổi.",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[1],
				   Unit=units[1],
							},

				   new(){
					Name="Đông trùng Hạ Thảo sấy thăng hoa",
					UrlSlug="dong-trung-ha-thao",
				   QuantityAvailable=10,
				   Price=490000,
				   PriceVirtual=500000,
				   Description="Đông trùng Hạ Thảo sấy thăng hoa là sản phẩm từ Đông trùng Hạ Thảo được nhân giống và nuôi trồng trong điều kiện tự nhiên tại Đà Lạt, kết hợp với công nghệ sản xuất môi trường giá thể nhân tạo giúp giá thành sản xuất loại nấm đắt đỏ này giảm đáng kể nhưng vẫn đảm bảo chất lượng của nấm.\r\n\r\nTrọng lư",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[1],
				   Unit=units[1],

				},

				   new(){
					Name="Bột cần tây",
					UrlSlug="bot-can-tay",
				   QuantityAvailable=10,
				   Price=140000,
				   PriceVirtual=0,
				   Description="Bột cần tây có công dụng loại bỏ độc tố, chất thải và các chất gây ô nhiễm khỏi cơ thể bạn. Bên cạnh việc hỗ trợ duy trì vóc dáng và đẹp da, sử dụng bột cần tây lâu ngày còn giúp ổn định huyết áp, phòng ngừa tim mạch, detox cơ thể và giúp tiêu hóa tốt.",
				   ViewCount=3,
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[3],
				   Unit=units[1],
			},

					new(){
					Name="Bột rau bó xôi",
					UrlSlug="bot-rau-bo-xoi",
				   QuantityAvailable=10,
				   Price=150000,
				   PriceVirtual=0,
				   Description="Hộp Bột Rau Bó Xôi Dalahouse là sản phẩm được làm từ 100% rau bó xôi tươi nguyên chất, được sấy lạnh theo công nghệ hiện đại giúp giữ nguyên hương vị và hàm lượng dinh dưỡng. Sản phẩm được đóng gói tiện lợi, dễ dàng sử dụng và bảo quản.",
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[3],
				   Unit=units[1],
			},
					new(){
					Name="Bột rau má đậu xanh cốt dừa",
					UrlSlug="bot-rau-ma-xanh-cot-dua",
				   QuantityAvailable=10,
				   Price=180000,
				   PriceVirtual=200000,
				   Description="Bột rau má đậu xanh cốt dừa là sự kết hợp hoàn hảo các hương vị thuần tự nhiên của rau má hữu cơ giàu vitamin và khoáng chất, đậu xanh giàu protein, nước cốt dừa giàu dầu béo lành mạnh và đường hoa dừa giàu chất điện giải.",
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[3],
				   Unit=units[1],
			},
					new(){
					Name="Bột diếp cá",
					UrlSlug="bot-diep-ca",
				   QuantityAvailable=10,
				   Price=130000,
				   PriceVirtual=0,
				   Description="Bột diếp cá sử dụng công nghệ sấy lạnh hiện đại tiêu chuẩn Châu Âu và công nghệ nghiền thực phẩm ở nhiệt độ thấp giúp bảo toàn các enzym và chất dinh dưỡng của rau củ, đảm bảo uy tín về chất lượng cũng như nguồn gốc với quy trình khép kín đạt chứng nhận FDA. Sản phẩm phù hợp bổ sung rau củ cho cả gia đình ngay cả khi bận rộn.",
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[3],
				   Unit=units[1],
			},
					new(){
					Name="Bột rau má đậu xanh cốt dừa",
					UrlSlug="bot-rau-ma-dau-xanh",
				   QuantityAvailable=10,
				   Price=180000,
				   PriceVirtual=0,
				   Description="Bột rau má đậu xanh cốt dừa là sự kết hợp hoàn hảo các hương vị thuần tự nhiên của rau má hữu cơ giàu vitamin và khoáng chất, đậu xanh giàu protein, nước cốt dừa giàu dầu béo lành mạnh và đường hoa dừa giàu chất điện giải.",
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[3],
				   Unit=units[1],
			},
					new(){
					Name="Cây giống chanh dây ngọt Colombia",
					UrlSlug="cay-giong-chanh-day-ngot",
				   QuantityAvailable=10,
				   Price=250000,
				   PriceVirtual=0,
				   Description="cây giống chanh dây ngọt Comlombia tại vùng khí hậu mát mẻ Đà Lạt, Lâm Đồng. Cây con khoẻ mạnh, cam kết chất lượng, đóng gói và giao hàng tận nơi trên toàn quốc.",
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[4],
				   Unit=units[3],
				},
					new(){
					Name="Cây giống phúc bồn tử đỏ",
					UrlSlug="cay-giong-phuc-bon-tu",
				   QuantityAvailable=10,
				   Price=250000,
				   PriceVirtual=270000,
				   Description="cây giống Phúc Bồn Tử đỏ với chất lượng cao, giá sỉ cạnh tranh. Cây giống được tuyển chọn kỹ lưỡng, khỏe mạnh, phát triển tốt, cho trái to, mọng nước, chất lượng cao.",
				   DateCreate= new DateTime(2023,03,12),
				   DateUpdate=DateTime.Now,
				   Category= caterories[4],
				   Unit=units[3],
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
				Name = "user",
				Email = "user@gmail.com",
				PhoneNumber = "0979797979",
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
				TotalPrice = 0,
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
					Name="Thanh toán trực tiếp",
					Description="Thanh toán trực tiếp khi nhận hàng",
				},
				new(){
					Name="QR Pay",
					Description="QR",
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
					CodeName="Discount50",
				},
			 new(){
					DiscountPrice=20,
					StartDate= DateTime.Now,
					EndDate=(DateTime.Now).AddDays(10),
					CodeName="Discount20",
				},
			  new(){
					DiscountPrice=100,
					StartDate= DateTime.Now,
					EndDate=(DateTime.Now).AddDays(10),
					CodeName="Discount0",
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

				Name = "Rau-Củ-Quả",
				UrlSlug= "rau-cu-qua",

				},
				   new()
				{

				Name = "Đặc sản Đà Lạt",
				UrlSlug= "dac-san-da-lat",

				},
						new()
				{

				Name = "Trái cây",
				UrlSlug= "trai-cay",

				},
						new()
				{

				Name = "Bột Rau, Củ, Quả",
				UrlSlug= "bot-rau-cu-qua",

				},
						new()
				{

				Name = "Hoa tươi cây giống",
				UrlSlug= "hoa-tuoi-cay-giong",

				},


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
				Name = "Xuân Hưng",
				CommentText = "Sản phẩm này rất tốt",
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
				Order = orders[0],
				Product = products[0],
				Quantity = 1000,
			};

			// Thêm danh sách OrderItems vào DbContext và lưu thay đổi vào cơ sở dữ liệu
			_dbContext.AddRange(orderItem);
			_dbContext.SaveChanges();

			return orderItems;
		}



	}
}

