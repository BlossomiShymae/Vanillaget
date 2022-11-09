using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class ServerFrameView : FrameView
    {
        public ServerFrameView()
        {
            Title = "Server";
            X = 0;
            Y = 1;
            Width = Dim.Fill();
            Height = Dim.Fill();
        }
    }
}
