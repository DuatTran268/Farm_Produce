using System;
using System.Linq;
using FarmProduce.Data.Contexts;
using FarmProduce.Data.Seeders;

var context = new FarmDbContext();
var seeder = new DataSeeder(context);
seeder.Initialize();

var admins = context.Admins.ToList();
//foreach (var item in admins)
//{
//	Console.WriteLine("{0},{1},{2},{3},{4}", item.Id, item.FullName, item.Password, item.Email, item.UrlSlug);
//}