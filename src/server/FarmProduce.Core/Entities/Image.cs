using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
    public class Image: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlImage { get; set; }
        public string Caption { get; set; }
        public int ProductId { get; set; }

    }
}
