namespace slimeget.Views
{
    internal interface IObservingView
    {
        void OnPropertyChanged(object? sender, EventArgs args);
    }
}
