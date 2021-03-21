using SmileBoyClient.Helpers;
using System;

namespace SmileBoy.Client.Core.Attributes
{
    public class DisplayAttribute : Attribute
    {
        public string Name { get; set; }

        public DisplayAttribute(string name)
        {
            Name = Has.NotNullOrEmpty(name);
        }
    }
}
