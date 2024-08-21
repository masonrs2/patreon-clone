using System.Text;
using System;
using System.Collections.Generic;

namespace Paramatic.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string ImagePreview { get; set; } = String.Empty;
        public string Description { get; set; } = string.Empty;
        public int Likes { get; set; }
        public int Comments { get; set; }

         // Foreign key for Creator
        public int CreatorId { get; set; }
    }
}