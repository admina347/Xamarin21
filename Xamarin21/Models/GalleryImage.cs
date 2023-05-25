using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin21.Models
{
    public class GalleryImage
    {
        public GalleryImage(Guid id, string name, string image = null, string description = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Image = image;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
