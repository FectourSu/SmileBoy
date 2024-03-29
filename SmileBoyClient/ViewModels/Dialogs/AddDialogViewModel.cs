﻿using MaterialDesignThemes.Wpf;
using SmileBoyClient.Command;
using SmileBoyClient.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels.Dialogs
{
    public class AddDialogViewModel<TModel> : ViewModelBase
        where TModel : class, new()
    {
        private BindingList<PropertyViewModel<TModel>> _someCollection;
        public BindingList<PropertyViewModel<TModel>> SomeCollection
        {
            get => _someCollection;
            set => Set(ref _someCollection, value);
        }

        public ICommand AddCommand { get; }
        
        public AddDialogViewModel()
        {
            var properties = PropertyCreator.Create<TModel>();
            _someCollection = new BindingList<PropertyViewModel<TModel>>(properties.ToList());

            AddCommand = new DelegateCommand(
                _ => DialogHost.CloseDialogCommand.Execute(ModelCreator.Create(_someCollection), null),
                _ => _someCollection.All(i => i.IsValid));
        }
    }
}
