using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models
{
    public class Todo
    {
        public long TodoId { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public bool IsDone { get; set; }
    }
}
