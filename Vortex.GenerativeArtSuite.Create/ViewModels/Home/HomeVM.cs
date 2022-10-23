﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Prism.Regions;
using Vortex.GenerativeArtSuite.Create.Models;
using Vortex.GenerativeArtSuite.Create.Services;
using Vortex.GenerativeArtSuite.Create.ViewModels.Base;

namespace Vortex.GenerativeArtSuite.Create.ViewModels.Home
{
    public class HomeVM : NavigationAware
    {
        private readonly IFileSystem fileSystem;
        private readonly INavigationService navigationService;

        public HomeVM(IFileSystem fileSystem, INavigationService navigationService)
        {
            this.fileSystem = fileSystem;
            this.navigationService = navigationService;

            NewSession = new NewSessionVM(NameIsValid, OpenNewSession);
            RecentSessions = new ObservableCollection<RecentSessionVM>();
        }

        public NewSessionVM NewSession { get; }

        public ObservableCollection<RecentSessionVM> RecentSessions { get; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            NewSession.Clear();
            RecentSessions.Clear();
            RecentSessions.AddRange(fileSystem.RecentSessions().Select(s => new RecentSessionVM(s, OpenRecentSession)));
        }

        private void OpenNewSession(string name, SessionSettings sessionSettings)
        {
            var session = new Session(name, sessionSettings);
            fileSystem.SaveSession(session);
            navigationService.OpenSession(session);
        }

        private void OpenRecentSession(string name)
        {
            var session = fileSystem.LoadSession(name);
            navigationService.OpenSession(session);
        }

        private bool NameIsValid(string name)
        {
            if (RecentSessions.Any(rs => rs.Name == name))
            {
                return false;
            }

            var rg = new Regex(@"^(?!(?:CON|PRN|AUX|NUL|COM[1-9]|LPT[1-9])(?:\.[^.]*)?$)[^<>:""\/\\|?*\x00-\x1F]*[^<>:""\/\\|?*\x00-\x1F\ .]$");
            return rg.IsMatch(name);
        }
    }
}
