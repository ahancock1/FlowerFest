// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Repository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;

    public abstract class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly string _path;
        private readonly List<T> _store;

        protected BaseRepository(string path)
        {
            _path = path;
            _store = new List<T>();

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            Load();
        }

        public IEnumerable<T> All(
            Func<T, bool> predicate = null,
            Func<T, object> orderby = null,
            int? skip = null,
            int? take = null)
        {
            var items = _store.AsEnumerable();

            if (predicate != null)
            {
                items = items.Where(predicate);
            }

            if (orderby != null)
            {
                items = items.OrderBy(orderby);
            }

            if (skip.HasValue)
            {
                items = items.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                items = items.Take(take.Value);
            }

            return items.ToList();
        }

        public bool Create(T item)
        {
            if (item == null) return false;

            try
            {
                item.Id = Guid.NewGuid();

                Serialize(item);

                if (!_store.Contains(item))
                {
                    _store.Add(item);
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error occurred creating entity: {e.Message}");
            }

            return false;
        }

        public bool Delete(T item)
        {
            if (!ValidateItem(item)) return false;

            var filepath = GetFilePath(item);

            try
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                if (_store.Contains(item))
                {
                    _store.Remove(item);
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error occurred deleting entity: {e.Message}");
            }

            return false;
        }

        public T Get(Func<T, bool> predicate)
        {
            return predicate != null ? _store.FirstOrDefault(predicate) : default(T);
        }

        public bool Update(T item)
        {
            if (!ValidateItem(item)) return false;

            try
            {
                Serialize(item);

                var existing = Get(i => i.Id.Equals(item.Id));

                if (existing != null)
                {
                    _store.Remove(existing);
                }

                _store.Add(item);

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error occurred updating entity: {e.Message}");
            }

            return false;
        }

        private void Serialize(T item)
        {
            var filepath = GetFilePath(item);

            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(writer, item);
            }
        }

        private T Deserialize(string filepath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(filepath))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        private void Load()
        {
            foreach (var filepath in Directory.GetFiles(_path))
            {
                try
                {
                    _store.Add(Deserialize(filepath));
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error occured deserialising file: {filepath}, {e.Message}");
                }
            }
        }

        private string GetFilePath(T item)
        {
            return Path.Combine(_path, $"{item.Id}.xml");
        }

        private bool ValidateItem(T item)
        {
            return item != null && item.Id != default(Guid);
        }
    }
}