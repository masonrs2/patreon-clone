using System;
using System.Collections.Generic;

namespace Paramatic.Models
{
    public class Creator
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public About About { get; set; } = new About();
        public List<Post> Posts { get; set; } = new List<Post>();
    }

    public class About
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}