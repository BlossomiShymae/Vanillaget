using slimeget.Interfaces;

namespace slimeget.Services
{
    interface IRepositoryChanged<T>
    {
        event EventHandler RepositoryChanged;
    }

    internal class RepositoryService<T> : IRepositoryChanged<T>
        where T : IIndexable
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

        public T Find(int id)
        {
            var data = _dataList.Find(x => x.Id == id);
            if (data == null)
                throw new Exception("Failed to find data in repository with provided id");
            return data;
        }

        public void Update(T data)
        {
            var index = _dataList.FindIndex(x => x.Id == data.Id);
            if (index == -1)
                throw new Exception("Failed to update data in repository as provided id was not found");
            _dataList[index] = data;
            RepositoryChanged?.Invoke(this, EventArgs.Empty);
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
