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
                var index = CalculateHash(data.Id, i++);
                if (!(_cells[index] is null)) continue;
                _cells[index] = data;
                return index;
            } while (i < _size);
            return -1;
        }

        public UserData Find(string id)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(id, i++);
                if (_cells[index] == null || _cells[index].Id != id) continue;
                return _cells[index];
            } while (i < _size);
            return null;
        }

        public bool Remove(string id)
        {
            var data = Find(id);
            var i = 0;
            do
            {
                var index = CalculateHash(id, i++);
                if (_cells[index] == null || _cells[i].Id != id) continue;
                _cells[index] = null;
                return true;
            } while (i < _size);
            return false;
        }

        private int CalculateHash(string key, int i)
        {
            unchecked
            {
                /*return Math.Abs(GetStringHash(key) + i * key.GetHashCode()) % _size;*/
                return key[0] - 'a' + i;
            }
        }

        private static int GetStringHash(string s)
        {
            var seed = 967;
            unchecked
            {
                /*var hash = 1;
                foreach (var c in s)
                    hash = hash * seed + c.GetHashCode();*/
                return s[0] - 'a';
            }
        }
    }
}