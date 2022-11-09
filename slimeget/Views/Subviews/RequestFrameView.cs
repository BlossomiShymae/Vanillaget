using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class RequestFrameView : FrameView
    {
        public RequestFrameView()
        {
            Title = "Requests";
            X = 0;
            Y = 1; // For menu
            Width = Dim.Percent(25f);
            Height = Dim.Fill();
        }
    }
}
