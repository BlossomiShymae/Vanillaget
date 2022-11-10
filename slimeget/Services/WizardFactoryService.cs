using Terminal.Gui;
using static Terminal.Gui.Wizard;

namespace slimeget.Services
{
    internal class WizardFactoryService
    {
        private readonly int _rowMargin = 1;
        public WizardFactoryService()
        {

        }

        public Wizard CreateServerNewWizard(Action<(TextField nameField, TextField hostnameField, TextField portField)> onFinished)
        {
            var wizard = new Wizard
            {
                Title = "New Server"
            };
            var step = new WizardStep("Create Collection")
            {
                HelpText = "Create a new request collection for a web server.",
            };
            wizard.AddStep(step);

            // Name form
            var nameLabel = new Label
            {
                Text = "Name:",
                AutoSize = true,
            };
            step.Add(nameLabel);
            var nameField = new TextField
            {
                X = Pos.Right(nameLabel),
                Width = Dim.Fill() - 1,
            };
            step.Add(nameField);

            // Hostname form
            var hostnameLabel = new Label
            {
                Y = Pos.Bottom(nameLabel) + _rowMargin,
                Text = "Hostname:",
                AutoSize = true,
            };
            step.Add(hostnameLabel);
            var hostnameField = new TextField
            {
                X = Pos.Right(hostnameLabel),
                Y = Pos.Bottom(nameLabel) + _rowMargin,
                Width = Dim.Fill() - 1,
                Height = 1
            };
            step.Add(hostnameField);

            // Port form
            var portLabel = new Label
            {
                Y = Pos.Bottom(hostnameLabel) + _rowMargin,
                Text = "Port:",
                AutoSize = true,
            };
            step.Add(portLabel);
            var portField = new TextField
            {
                X = Pos.Right(portLabel),
                Y = Pos.Bottom(hostnameLabel) + _rowMargin,
                Width = Dim.Fill() - 1,
                Height = 1,
            };
            step.Add(portField);

            wizard.Finished += (args) =>
            {
                onFinished?.Invoke((nameField, hostnameField, portField));
            };

            return wizard;
        }
    }
}
