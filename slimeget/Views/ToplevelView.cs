using slimeget.ViewModels;
using slimeget.Views.Subviews;
using Terminal.Gui;

namespace slimeget.Views
{
    internal class ToplevelView : Toplevel
    {
        private readonly ToplevelViewModel _viewModel;

        private readonly MenuBar _menuBar;

        private readonly FrameView _serverFrame;

        private readonly FrameView _requestFrame;

        private readonly FrameView _responseFrame;

        private readonly View _leftPanel;

        private readonly View _rightPanel;

        public ToplevelView(ToplevelViewModel viewModel, ServerFrameView serverFrame, RequestFrameView requestFrame, ResponseFrameView responseFrame)
        {
            _viewModel = viewModel;
            _viewModel.MenuItemClicked += OpenDialog;

            X = 0;
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Fill();

            _serverFrame = serverFrame;
            _requestFrame = requestFrame;
            _responseFrame = responseFrame;

            _menuBar = new(_viewModel.MenuBarItems.ToArray());

            // Left panel the user sees
            _leftPanel = new()
            {
                X = 0,
                Y = 1, // Account for menu
                Width = Dim.Percent(25f),
                Height = Dim.Fill()
            };
            _serverFrame.X = 0;
            _serverFrame.Y = 0;
            _serverFrame.Height = Dim.Percent(25f);
            _serverFrame.Width = Dim.Fill();
            _requestFrame.X = 0;
            _requestFrame.Y = Pos.Bottom(_serverFrame);
            _requestFrame.Height = Dim.Fill();
            _requestFrame.Width = Dim.Fill();
            _leftPanel.Add(_serverFrame, _requestFrame);

            // Right panel the user sees
            _rightPanel = new()
            {
                X = Pos.Right(_leftPanel),
                Y = 1, // Account for menu
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            _responseFrame.X = 0;
            _responseFrame.Y = 0;
            _responseFrame.Height = Dim.Fill();
            _responseFrame.Width = Dim.Fill();
            _rightPanel.Add(_responseFrame);

            Add(_leftPanel, _rightPanel, _menuBar);
        }

        private void OpenDialog(object? sender, MenuItemClickedEventArgs args)
        {
            switch (args.MenuItem)
            {
                case MenuItems.FileClose:
                    Application.RequestStop();
                    break;
                case MenuItems.ServerNew:
                    break;
                case MenuItems.RequestNew:
                    break;
                default:
                    break;
            }
        }
    }
}
