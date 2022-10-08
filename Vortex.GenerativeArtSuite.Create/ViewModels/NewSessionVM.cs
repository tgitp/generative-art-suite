﻿using System;
using System.Windows.Input;
using Prism.Commands;
using Vortex.GenerativeArtSuite.Create.Models;

namespace Vortex.GenerativeArtSuite.Create.ViewModels
{
    public class NewSessionVM : SessionSettingsVM
    {
        private readonly Func<string, bool> validateName;
        private string name;

        public NewSessionVM(Func<string, bool> validateName, Action<string, SessionSettings> onCreate)
            : base(new SessionSettings())
        {
            this.validateName = validateName;

            name = string.Empty;

            var create = new DelegateCommand(() => onCreate(name, Settings), CanCreate);
            PropertyChanged += (s, e) =>
            {
                create.RaiseCanExecuteChanged();
            };

            Create = create;
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand Create { get; }

        private bool CanCreate()
        {
            return validateName(name) &&
                !string.IsNullOrWhiteSpace(OutputFolder) &&
                !string.IsNullOrWhiteSpace(NamePrefix) &&
                !string.IsNullOrWhiteSpace(DescriptionTemplate) &&
                !string.IsNullOrWhiteSpace(BaseURI) &&
                CollectionSize > 0;
        }
    }
}
