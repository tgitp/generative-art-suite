﻿using System.Reflection;
using System.Windows;
using Vortex.GenerativeArtSuite.Inspect.ViewModels;
using static Microsoft.Requires;

namespace Vortex.GenerativeArtSuite.Inspect.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowVM viewModel)
        {
            DataContext = NotNull(viewModel, nameof(viewModel));
            InitializeComponent();

            Title = Strings.AppName;

            var version = Assembly.GetEntryAssembly()?.GetName().Version;
            if (version != null)
            {
                Title += $" v{version}";
            }
        }
    }
}
