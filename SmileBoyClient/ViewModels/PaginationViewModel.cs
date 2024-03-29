﻿using SmileBoyClient.Command;
using System;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    public class PaginationViewModel : ViewModelBase
    {
        private int _pageSize;

        private int _index = 1;

        public int Index
        {
            get => _index;
            private set => Set(ref _index, value);
        }

        private int _count;

        public int PageCount
        {
            get => _count;
            private set => Set(ref _count, value);
        }

        private bool PreviousRules => Index > 1;
        private bool NextRules => Index < PageCount;

        public ICommand LeftCommand { get; set; }

        public ICommand RightCommand { get; set; }

        public PaginationViewModel(Action<int, int> action, int pageSize)
        {
            _pageSize = pageSize;

            LeftCommand  = new DelegateCommand(_ => action?.Invoke(Previous(), pageSize), _ => PreviousRules);
            RightCommand = new DelegateCommand(_ => action?.Invoke(Next(),     pageSize), _ => NextRules);
        }

        private int Next()
        {
            if (NextRules)
                Index += 1;

            return Index;
        }

        public void Reset() => Index = 1;

        public int Previous()
        {
            if (PreviousRules)
                Index -= 1;

            return Index;
        }

        public void SetCount(long value)
        {
            var count = Convert.ToInt32(Math.Ceiling((double)value / _pageSize));
            PageCount = count < 1 ? 1 : count;
        }

    }
}
