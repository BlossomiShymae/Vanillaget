using Terminal.Gui;

namespace Vanillaget.Views.Controls
{
	internal class ButtonMenu : View
	{
		private readonly List<Button> _buttons = new();
		private readonly int _height = 1;

		public ButtonMenu()
		{
			X = 0;
			Y = 0;
			Height = _height;
			Width = Dim.Fill();
		}

		public Button Add(string text)
		{
			Button button = new()
			{
				X = 0,
				Y = 0,
				AutoSize = true,
				Text = text
			};

			if (_buttons.Count > 0)
			{
				var previous = _buttons.ElementAtOrDefault(_buttons.Count - 1);
				if (previous == null) throw new NullReferenceException("Previous button is null");

				button.X = Pos.Right(previous);
			}

			_buttons.Add(button);
			Add(button);

			return button;
		}
	}
}
