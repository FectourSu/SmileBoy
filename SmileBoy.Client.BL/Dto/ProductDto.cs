using SmileBoyClient.Core.IContract;
using System;

namespace SmileBoyClient.Core.Entites
{
    public class ProductDto : IModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public decimal CurrentPrice { get; set; }

        public string Img { get; set; }
    }
}
