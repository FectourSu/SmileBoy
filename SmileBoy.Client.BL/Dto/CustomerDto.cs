using SmileBoy.Client.Core.Attributes;
using SmileBoyClient.Core.IContract;
using System;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SmileBoy.Client.Core.Attributes.DisplayAttribute;


namespace SmileBoy.Client.Core.Dto
{
    public class CustomerDto : IModel
    {
        [DisplayIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display("Full name")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "This is a required field")]
        public string Email { get; set; }

        [Phone]
        [Required(ErrorMessage = "This is a required field")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        public string Address { get; set; }

        [Display("Image")]
        public string Img { get; set; }
    }
}
