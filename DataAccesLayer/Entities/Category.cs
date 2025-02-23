﻿namespace FashKartWebsite.DataAccesLayer.Entities
{
    public class Category:Entity
    {
        public string? Name { get; set; }
        public string ImageUrl {  get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>(); 
    }
}
