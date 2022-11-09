using Terminal.Gui;

namespace slimeget.Views
{
    internal class MainWindow
    {
        public Toplevel Toplevel { get; set; }

        public MenuBar MenuBar { get; set; }

        public Window Window { get; set; }

        public MainWindow()
        {
            Toplevel = new()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            MenuBar = new(new MenuBarItem[]
            {
                new MenuBarItem("_File", new MenuItem[]
                {
                    new MenuItem("_Close", "", () =>
                    {
                        Application.RequestStop();
                    })
                    }
                )
            });

            Window = new()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
        }
    }
}
