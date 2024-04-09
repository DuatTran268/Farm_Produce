﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string CommentText { get; set; }
        public bool Status { get; set; }
        public string ApplicationUserId { get; set; }
    }
}