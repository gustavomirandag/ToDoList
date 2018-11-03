using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoListWebAPI.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDateTime { get; set; }
    }
}