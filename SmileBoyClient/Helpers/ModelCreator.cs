using SmileBoyClient.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DisplayAttribute = SmileBoy.Client.Core.Attributes.DisplayAttribute;

namespace SmileBoyClient.Helpers
{
    public sealed class ModelCreator
    {
        public static TModel Create<TModel>(IEnumerable<PropertyViewModel<TModel>> properties)
            where TModel : class, new()
        {
            var result = new TModel();
            var type = result.GetType();

            string propertyName = string.Empty;

            foreach (var item in properties)
            {
                propertyName = item.Title;
                var property = type.GetProperty(propertyName);

                if (property == null)
                {
                    propertyName = type.GetProperties().FirstOrDefault(prop =>
                    {
                        var attribute = prop.GetCustomAttribute(typeof(DisplayAttribute), true);
                        return (attribute as DisplayAttribute)?.Name?.Equals(item.Title) ?? false;

                    }).Name;

                    property = type.GetProperty(propertyName);

                }

                property.SetValue(result, item.Text);
            }

            return result;

        }
    }
}
