using SmileBoy.Client.Core.Attributes;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.Core.IContract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmileBoy.Client.Core.Dto
{
    public class OrderDto : IModel
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        private string? _number;

        public string Number
        {
            get
            {
                if (string.IsNullOrEmpty(_number))
                    _number = GetNumber();

                return _number;
            }
            set
            {
                _number = value;
            }
        }
        private DateTime _datetime;
        public DateTime DeliveryDate
        {
            get
            {
                if (_datetime == default)
                    _datetime = DateTime.Now.AddDays(5); // order date

                return _datetime;
            }
            set
            {
                _datetime = value;
            }
        }

        public CustomerDto Customer { get; set; }

        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();

        public bool Delivered => GetProgress();

        //generate random product code
        private bool GetProgress()
        {
            if (DateTime.Now.Day == DeliveryDate.Day)
                return true;
            else
                return false;
        }

        //generate order code
        private string GetNumber() =>
            (char)new Random().Next('A', 'Z' + 1)
            + "-" + string.Join("", Enumerable.Range(0, 8).Select(i => new Random().Next(10)));
    }
}
