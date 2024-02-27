using FarmProduce.Core.Contracts;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
    public class Cart: IEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate {  get; set; }
       
        public IList<Product> Products { get; set; }
    }
}
