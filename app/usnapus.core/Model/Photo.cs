using System;

namespace uSnapUs.Core.Model
{
    public class Photo
    {
        [PrimaryKey,AutoIncrement]
        public int PhotoId { get; set; }

        [Indexed]
        public int EventId { get; set; }

        public string ImageLocation { get; set; }

        public DateTime DateTaken { get; set; }

        public int Id { get; set; }
        
        public bool IsUploaded { get; set; }

        public int Size { get; set; }

        public int Uploaded { get; set; }

        public string ThumbnailPath { get; set; }

        public string Url { get; set; }
    }
}