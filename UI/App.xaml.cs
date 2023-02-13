﻿using Microsoft.UI.Xaml;

namespace Ganymede.UI
{
    public partial class App : Application
    {
        private Window window;

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            window = new MainWindow();
            window.Activate();
        }

    }
}
