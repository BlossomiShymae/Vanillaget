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

        public Wizard CreateRequestNewWizard(Action<(TextField nameField, TextField pathField, RadioGroup methodRadioGroup)> onFinished)
        {
            var wizard = new Wizard
            {
                Title = "New Request"
            };
            var step = new WizardStep("Create Request Method")
            {
                HelpText = "Create a new request method to send."
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

            // Resource path form
            var pathLabel = new Label
            {
                Text = "Resource Path:",
                Y = Pos.Bottom(nameLabel) + _rowMargin,
                AutoSize = true,
            };
            step.Add(pathLabel);
            var pathField = new TextField
            {
                X = Pos.Right(pathLabel),
                Y = Pos.Bottom(nameField) + _rowMargin,
                Width = Dim.Fill() - 1,
                Height = 1,
            };
            step.Add(pathField);

            // Http method form
            var methodLabel = new Label
            {
                Text = "Http Method:",
                Y = Pos.Bottom(pathLabel) + _rowMargin,
                AutoSize = true,
            };
            step.Add(methodLabel);
            NStack.ustring[] options = { "GET", "POST", "PUT", "DELETE", "PATCH" };
            var methodRadioGroup = new RadioGroup(options)
            {
                X = Pos.Right(methodLabel),
                Y = Pos.Bottom(pathField) + _rowMargin,
                Width = Dim.Fill() - 1,
                Height = 1,
                DisplayMode = DisplayModeLayout.Horizontal
            };
            step.Add(methodRadioGroup);

            wizard.Finished += (args) =>
            {
                onFinished.Invoke((nameField, pathField, methodRadioGroup));
            };

            return wizard;
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
