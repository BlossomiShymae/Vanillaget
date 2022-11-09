using slimeget.Views.Subviews;
using Terminal.Gui;

namespace slimeget.Views
{
    internal class ToplevelView : Toplevel
    {
        private readonly MenuBar _menuBar;

        private readonly FrameView _serverFrame;

        private readonly FrameView _requestFrame;

        public ToplevelView(ServerFrameView serverFrame, RequestFrameView requestFrame)
        {
            X = 0;
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Fill();

            _menuBar = new(new MenuBarItem[]
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

            _serverFrame = serverFrame;
            _requestFrame = requestFrame;

            Add(_serverFrame, _requestFrame, _menuBar);
        }
    }
}
