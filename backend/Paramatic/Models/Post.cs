using System.ComponentModel;
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

        // [Column(TypeName = "bytea")]
        // public byte[] Content { get; set; } = Array.Empty<byte>();

        public string VideoUrl { get; set; } = string.Empty; // Store S3 URL Here

         // Foreign key for Creator
        public int CreatorId { get; set; }
    }
}