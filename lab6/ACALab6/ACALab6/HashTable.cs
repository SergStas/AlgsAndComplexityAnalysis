using System;

namespace ACALab6
{
    public class HashTable
    {
        private readonly UserData[] _cells;
        private readonly int _size;

        public HashTable(int size)
        {
            _size = size;
            _cells = new UserData[_size];
        }
        
        public int Add(UserData data)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(data, i++);
                if (!(_cells[index] is null)) continue;
                _cells[index] = data;
                return index;
            } while (i < _size);
            return -1;
        }

        public int Find(UserData data)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(data, i++);
                if (!_cells[index].Equals(data)) continue;
                return index;
            } while (i < _size);
            return -1;
        }

        public bool Remove(UserData data)
        {
            var key = Find(data);
            if (key < 0 || key >= _size)
                return false;
            _cells[key] = null;
            return true;
        }

        public UserData Get(int key) => _cells[key];

        private int CalculateHash(UserData data, int i)
        {
            unchecked
            {
                return Math.Abs(data.GetExtraHash() + i * data.GetHashCode()) % _size;
            }
        }
    }
}