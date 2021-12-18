using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorEllo.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Cover { get; set; }//img
    }
}
