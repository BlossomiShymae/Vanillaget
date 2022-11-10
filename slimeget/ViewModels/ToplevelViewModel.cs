using CommunityToolkit.Mvvm.ComponentModel;
using slimeget.Models;
using System.Collections.ObjectModel;
using Terminal.Gui;

namespace slimeget.ViewModels
{
    internal partial class ToplevelViewModel : ObservableObject
    {
        public ObservableCollection<MenuBarItem> MenuBarItems = new();

        public ObservableCollection<RequestMethodCollection> RequestMethodCollections = new();

        public event EventHandler<MenuItemClickedEventArgs>? MenuItemClicked;

        public ToplevelViewModel()
        {
            MenuBarItems = new()
            {
                new MenuBarItem("_File", new MenuItem[]
                {
                    new MenuItem("_Close", "", () => MenuItemClicked?.Invoke(this, new () { MenuItem = MenuItems.FileClose }))
                }),
                new MenuBarItem("_Server", new MenuItem[]
                {
                    new MenuItem("_New", "", () => MenuItemClicked?.Invoke(this, new () { MenuItem = MenuItems.ServerNew }))
                }),
                new MenuBarItem("_Request", new MenuItem[]
                {
                    new MenuItem("_New", "", () => MenuItemClicked?.Invoke(this, new() { MenuItem = MenuItems.RequestNew }))
                })
            };
        }
    }

    internal enum MenuItems
    {
        FileClose,
        ServerNew,
        RequestNew,
    }

    internal class MenuItemClickedEventArgs : EventArgs
    {
        public MenuItems MenuItem { get; set; }
    }
}
