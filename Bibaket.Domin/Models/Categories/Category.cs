using Bibaket.Domin.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Models.Categories
{
    public class Category:BaseEntity
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string? ImageName { get; set; }

        public Category? Parent { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
