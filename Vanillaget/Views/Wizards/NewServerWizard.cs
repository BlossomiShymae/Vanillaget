using Terminal.Gui;

namespace Vanillaget.Views.Wizards
{
    internal class NewServerWizard : Wizard
    {
        private static readonly int _rowMargin = 1;
        private readonly TextField _name;
        private readonly TextField _hostname;
        private readonly TextField _port;
        public string Name
        {
            get => _name.Text.ToString() ?? string.Empty;
        }
        public string Hostname
        {
            get => _hostname.Text.ToString() ?? string.Empty;
        }
        public ushort Port
        {
            get => ushort.Parse(_port.Text.ToString() ?? "80");
        }

        public NewServerWizard()
        {
            Title = "New Server";

            var step = new WizardStep("Create Collection")
            {
                HelpText = "Create a new request collection for a web server.",
            };
            AddStep(step);

            // _name form
            var nameLabel = new Label
            {
                Text = "Name:",
                AutoSize = true,
            };
            step.Add(nameLabel);
            _name = new TextField
            {
                X = Pos.Right(nameLabel),
                Width = Dim.Fill() - 1,
            };
            step.Add(_name);

            // _hostname form
            var hostnameLabel = new Label
            {
                Y = Pos.Bottom(nameLabel) + _rowMargin,
                Text = "Hostname:",
                AutoSize = true,
            };
            step.Add(hostnameLabel);
            _hostname = new TextField
            {
                X = Pos.Right(hostnameLabel),
                Y = Pos.Bottom(nameLabel) + _rowMargin,
                Width = Dim.Fill() - 1,
                Height = 1
            };
            step.Add(_hostname);

            // _port form
            var portLabel = new Label
            {
                Y = Pos.Bottom(hostnameLabel) + _rowMargin,
                Text = "Port:",
                AutoSize = true,
            };
            step.Add(portLabel);
            _port = new TextField
            {
                X = Pos.Right(portLabel),
                Y = Pos.Bottom(hostnameLabel) + _rowMargin,
                Width = Dim.Fill() - 1,
                Height = 1,
            };
            step.Add(_port);
        }
    }
}
