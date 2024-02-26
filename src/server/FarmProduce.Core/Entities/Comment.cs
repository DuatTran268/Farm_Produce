using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
    public class Comment:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string CommentText { get; set; }
        public bool Status { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

    }
}
