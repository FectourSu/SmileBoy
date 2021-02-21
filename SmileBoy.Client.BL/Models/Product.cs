using SmileBoyClient.Core.IContract;

namespace SmileBoyClient.Core.Models
{
    class Product : IModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public decimal CurrentPrice { get; set; }

        public string Img { get; set; }
    }
}
