using SmileBoy.Client.Core.Attributes;
using SmileBoyClient.Core.IContract;
using System;
using System.Linq;

namespace SmileBoy.Client.Core.Dto
{
    public class OrderCreateDto : IModel
    {
        [DisplayIgnore]
        public Guid Id { get; set; }

        public string Number => GenerateNumber();

        private string GenerateNumber()
        {
            var r = new Random();
            return (char)r.Next('A', 'Z' + 1) + "-" + string.Join("", Enumerable.Repeat(0, 8).Select(i => r.Next(10)));
        }
    }
}
