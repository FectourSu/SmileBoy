using SmileBoy.Client.Core.Attributes;
using SmileBoyClient.Core.IContract;
using System;
using System.ComponentModel.DataAnnotations;
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
    }
}
