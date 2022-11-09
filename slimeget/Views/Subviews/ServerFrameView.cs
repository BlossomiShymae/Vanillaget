using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class ServerFrameView : FrameView
    {
        public ServerFrameView(RequestFrameView requestFrame)
        {
            Title = "Server";
            X = Pos.Right(requestFrame);
            Y = 1; // For menu
            Height = Dim.Fill();
            Width = Dim.Fill();
        }
    }
}
