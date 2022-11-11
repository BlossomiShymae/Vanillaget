namespace slimeget.Services
{
    interface IRepositoryChanged<T>
    {
        event EventHandler RepositoryChanged;
    }

    internal class RepositoryService<T> : IRepositoryChanged<T>
    {
        private readonly List<T> _dataList = new();

        public event EventHandler? SelectionChanged;

        private int _selection;

        public int Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler? RepositoryChanged;

        public List<T> Get()
        {
            return _dataList;
        }

        public void Add(T data)
        {
            _dataList.Add(data);
            RepositoryChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Remove(T data)
        {
            _dataList.Remove(data);
            RepositoryChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
