using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Terminal.Gui;

namespace slimeget.ViewModels
{
    internal partial class ToplevelViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MenuBarItem> _menuBarItems = new();

        public ToplevelViewModel()
        {
            MenuBarItems = new()
            {
                new MenuBarItem("_File", new MenuItem[]
                {
                    new MenuItem("_Close", "", () =>
                    {
                        Application.RequestStop();
                    })
                    }
                )
            };
        }
    }
}
