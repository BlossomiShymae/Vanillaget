using slimeget.Services;
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

        private readonly WizardFactoryService _wizardFactoryService;

        public ToplevelView(ToplevelViewModel viewModel,
            ServerFrameView serverFrame,
            RequestFrameView requestFrame,
            ResponseFrameView responseFrame,
            WizardFactoryService wizardFactoryService)
        {
            _viewModel = viewModel;
            _viewModel.MenuItemClicked += OnMenuItemClicked;

            _wizardFactoryService = wizardFactoryService;

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
            var scrollBarFrame = new ScrollBarView(_responseFrame, true, true);
            _rightPanel.Add(scrollBarFrame);

            Add(_leftPanel, _rightPanel, _menuBar);
        }

        private void OnMenuItemClicked(object? sender, MenuItemClickedEventArgs args)
        {
            Wizard? wizard = null;
            switch (args.MenuItem)
            {
                case MenuItems.FileClose:
                    Application.RequestStop();
                    break;
                case MenuItems.ServerNew:
                    wizard = _wizardFactoryService.CreateServerNewWizard(x => TryErrorQuery(() =>
                    {
                        _viewModel.ServerName = x.nameField.Text.ToString() ?? String.Empty;
                        _viewModel.ServerHostname = x.hostnameField.Text.ToString() ?? String.Empty;
                        _viewModel.ServerPort = UInt32.Parse(x.portField.Text.ToString() ?? "80");
                        _viewModel.SaveRequestMethodCollectionCommand.Execute(this);
                    }));
                    break;
                case MenuItems.RequestNew:
                    Func<int, HttpMethod> selectedHttpMethod = (int select) =>
                    {
                        return select switch
                        {
                            0 => HttpMethod.Get,
                            1 => HttpMethod.Post,
                            2 => HttpMethod.Put,
                            3 => HttpMethod.Delete,
                            4 => HttpMethod.Patch,
                            _ => throw new ArgumentException("Selected integer is invalid"),
                        };
                    };
                    wizard = _wizardFactoryService.CreateRequestNewWizard(x => TryErrorQuery(() =>
                    {
                        _viewModel.RequestName = x.nameField.Text.ToString() ?? String.Empty;
                        _viewModel.RequestResourcePath = x.pathField.Text.ToString() ?? String.Empty;
                        _viewModel.RequestHttpMethod = selectedHttpMethod(x.methodRadioGroup.SelectedItem);
                        _viewModel.SaveRequestMethodCommand.Execute(this);
                    }));
                    break;
                case MenuItems.RequestSendNow:
                    _viewModel.SendRequestNowCommand.Execute(this);
                    break;
                default:
                    break;
            }

            if (wizard != null)
                Application.Run(wizard);
        }

        private static void TryErrorQuery(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.ErrorQuery($"Error - {e.GetType().Name}", $"{e.Message}\n{e.StackTrace}", "Ok");
            }
        }
    }
}
