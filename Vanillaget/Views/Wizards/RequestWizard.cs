using Terminal.Gui;
using Vanillaget.Extensions;

namespace Vanillaget.Views.Wizards
{
    internal class RequestWizard : Wizard
    {
        private static readonly int _rowMargin = 1;
        private readonly TextField _name;
        private readonly TextField _path;
        private readonly RadioGroup _methods;
        public string Name
        {
            get => _name.Text.ToString() ?? string.Empty;
        }
        public string Path
        {
            get => _path.Text.ToString() ?? string.Empty;
        }
        public HttpMethod Method
        {
            get => _methods.SelectedItem switch
            {
                0 => HttpMethod.Get,
                1 => HttpMethod.Post,
                2 => HttpMethod.Put,
                3 => HttpMethod.Delete,
                4 => HttpMethod.Patch,
                _ => throw new InvalidCastException("Selected method is invalid")
            };
        }
        public enum WizardType { New, Edit }

        public static RequestWizard CreateFromType(WizardType type)
        {
            var title = "Request";
            var subtitle = "Request Method";

            return type switch
            {
                WizardType.New => new("New".AppendPad(title), "New".AppendPad(subtitle), "Create a new request method to send."),
                WizardType.Edit => new("Edit".AppendPad(title), "Edit".AppendPad(subtitle), "Edit the current request method."),
                _ => throw new ArgumentException("Invalid WizardType"),
            };
        }

        private RequestWizard(string title, string subtitle, string helpText)
        {
            Title = title;

            var step = new WizardStep(subtitle)
            {
                HelpText = helpText
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

            // Resource path form
            var pathLabel = new Label
            {
                Text = "Resource Path:",
                Y = Pos.Bottom(nameLabel) + _rowMargin,
                AutoSize = true,
            };
            step.Add(pathLabel);
            _path = new TextField
            {
                X = Pos.Right(pathLabel),
                Y = Pos.Bottom(_name) + _rowMargin,
                Width = Dim.Fill() - 1,
                Height = 1,
            };
            step.Add(_path);

            // Http method form
            var methodLabel = new Label
            {
                Text = "Http Method:",
                Y = Pos.Bottom(pathLabel) + _rowMargin,
                AutoSize = true,
            };
            step.Add(methodLabel);
            NStack.ustring[] options = { "GET", "POST", "PUT", "DELETE", "PATCH" };
            _methods = new RadioGroup(options)
            {
                X = Pos.Right(methodLabel),
                Y = Pos.Bottom(_path) + _rowMargin,
                Width = Dim.Fill() - 1,
                Height = 1,
                DisplayMode = DisplayModeLayout.Horizontal
            };
            step.Add(_methods);
        }
    }
}
