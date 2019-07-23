using System;
using System.Collections.Generic;
using System.Linq;
using UAS_Pemrograman.models;
namespace UAS_Pemrograman.Processes{
    public abstract class OrdersProcessing{
        private string _invoice;
        protected List<Orders> ListOrders;

        public string Invoice{
            get{
                _invoice = "N01/2019/" + _invoice.PadLeft(4, '0');
                return _invoice;
            }
            set{
                _invoice = value.ToString();
            }
        }

        public virtual void Index(){
            ListOrders = new List<Orders>();
        }

        public string Info(){
            var info = "List Orders: ";
            foreach(var item in Read().ToArray()){
                info += $"{item.Customer},";
            }
            return info.Remove(info.Length - 1);
        }

        public bool Create(Orders item) { 
            ListOrders.Add(item);
            return true;
        }

        public Orders Read(int id) {
            return ListOrders.Where(model => model.Id.Equals(id)).SingleOrDefault();
        }

        public List<Orders> Read() {
            return ListOrders;
        }

        public bool Update(int id, Orders item) {
            if (ListOrders == null) return false;
            var data = Read().Where(model => model.Id.Equals(id)).SingleOrDefault();
            if(data != null) {
                ListOrders.Remove(data);
                ListOrders.Add(item);
                return true;
            } else {
                return false;
            }
        }

        public bool Delete(int id) {
            if (ListOrders == null) return false;
            var data = Read().Where(model => model.Id.Equals(id)).SingleOrDefault();
            if (data != null) {
                ListOrders.Remove(data);
                return DeleteDetail(id);
            } else {
                return false;
            }
        }

        public abstract bool CreateDetail();
        public abstract OrdersDetail ReadDetail(int id);
        public abstract List<OrdersDetail> ReadDetail();
        public abstract bool UpdateDetail(int id);
        public abstract bool DeleteDetail(int id);
        public abstract void Dispose();
    }
}
