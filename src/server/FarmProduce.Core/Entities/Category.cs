﻿using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
    public class Category : IEntity
        
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string ImageUrl { get; set; }
        public IList<Product> Products { get; set; }    

    }
}
