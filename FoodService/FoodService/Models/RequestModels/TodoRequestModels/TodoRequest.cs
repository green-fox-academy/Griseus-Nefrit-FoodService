using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.RequestModels.TodoRequestModels
{
    public class TodoRequest
    {
        [Required]
        public string Description { get; set; }
        public string Owner { get; set; }
        public bool IsDone { get; set; }
    }
}
