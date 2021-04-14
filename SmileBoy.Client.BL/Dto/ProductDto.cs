using SmileBoy.Client.Core.Attributes;
using SmileBoyClient.Core.IContract;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using DisplayAttribute = SmileBoy.Client.Core.Attributes.DisplayAttribute;

namespace SmileBoyClient.Core.Entites
{
    public class ProductDto : IModel
    {
        [DisplayIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="This is a required field")]
        [Display("Full name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "This is a required field")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        public decimal CurrentPrice { get; set; }

        [Display("Image")]
        public string Img { get; set; }

        //generate random product code
        private int? _productCode;

        [DisplayIgnore]
        public int? ProductCode
        {
            get 
            {
                if (_productCode is null) 
                    _productCode = GenerateNumber();

                return _productCode;
            }
            set 
            {
                _productCode = value;
            }
        }

        private int GenerateNumber() =>
            int.Parse(string.Join("", Enumerable.Range(0, 6).Select(i => new Random().Next(1, 10))));

    }
}
