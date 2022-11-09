using slimeget.Views.Subviews;
using Terminal.Gui;

namespace slimeget.Views
{
    internal class MainWindow
    {
        public Toplevel Toplevel { get; set; }

        public MenuBar MenuBar { get; set; }

        public FrameView ServerFrame { get; set; }

        public MainWindow(ServerFrameView serverFrame)
        {
            Toplevel = new()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
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

            ServerFrame = serverFrame;

            Toplevel.Add(ServerFrame, MenuBar);
        }
    }
}
