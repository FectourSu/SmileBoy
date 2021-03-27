using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileBoyClient.ViewModels
{
    public class PropertyViewModel<TModel> : ViewModelBase
        where TModel : new()
    {
        public PropertyViewModel(string propertyName)
        {
            NameProperty = propertyName;
        }
        
        public string NameProperty { get; }

        public bool IsValid { get; private set; }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set {
                ValidateProperty(value, NameProperty);
                Set(ref _text, value); 
            }
        }
        private void ValidateProperty<T>(T value, string nameProperty)
        {
            try
            {
                var property = typeof(TModel).GetProperty(nameProperty);
                var cvalue = Convert.ChangeType(value, property.PropertyType);

                Validator.ValidateProperty(cvalue, new ValidationContext(new TModel(), null, null)
                {
                    MemberName = nameProperty
                });

                IsValid = true;
            }
            catch (Exception)
            {
                IsValid = false;
                throw;
            }
        }
    }
}
