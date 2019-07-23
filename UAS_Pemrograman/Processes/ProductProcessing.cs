using System;
using System.Collections.Generic;
using UAS_Pemrograman.models;
using System.Linq;
using System.Text.RegularExpressions;
namespace UAS_Pemrograman.Processes {

    public interface IProductProcessing<T> {
        void Index();
        bool Create(T item);
        T Read(int id);
        List<T> Read();
        bool Update(int id, T item);
        bool Delete(int id);
        string Info();
        void Dispose();
    }

    public class ProductProcessing<T> : IProductProcessing<T> where T: Product {
        private List<T> _list;

        public void Index() {
            _list = new List<T>();
        }

        public bool Create(T item) {
            _list.Add(item);
            return true;
        }

        public List<T> Read() {
            return _list;
        }

        public T Read(int id) {
            return _list.Where(model => model.Id.Equals(id)).SingleOrDefault();
        }

        public bool Update(int id, T item) {
            if (_list == null) return false;
            var data = Read().Where(model => model.Id.Equals(id)).SingleOrDefault();
            if(data != null) {
                _list.Remove(data);
                _list.Add(item);
                return true;
            } else {
                return false;
            }
        }

        public bool Delete(int id) {
            if (_list == null) return false;
            var data = Read().Where(model => model.Id.Equals(id)).SingleOrDefault();
            if (data != null) {
                _list.Remove(data);
                return true;
            } else {
                return false;
            }
        }

        public string Info() {
            var info = "List Product: ";
            foreach(var item in Read().ToArray()) {
                info += $"{item.Name},";
            }
            return info.Remove(info.Length - 1);
        }

        public void Dispose() {
            _list = null;
        }
    }
}
