using System;
using System.Collections.Generic;
using UAS_Pemrograman.models;
using System.Linq;

namespace UAS_Pemrograman.Processes {
    public class OrdersExtendProcessing : OrdersProcessing{
        private List<OrdersDetail> _list;

        public OrdersDetail Model { get; set; } = new OrdersDetail();

        public override void Index() {
            _list = new List<OrdersDetail>();
            base.Index();
        }

        public override bool CreateDetail() {
            Console.Write("Masukkan Qty: "); Model.Qty = Convert.ToInt32(Console.ReadLine());
            _list.Add(Model);

            return true;
        }

        public override OrdersDetail ReadDetail(int id) {
            return _list.Where(model => model.Id.Equals(id)).SingleOrDefault();
        }

        public override List<OrdersDetail> ReadDetail() {
            return _list;
        }

        public override bool UpdateDetail(int id) {
            if (_list == null) return false;
            var data = ReadDetail().Where(model => model.Id.Equals(id)).SingleOrDefault();
            if (data != null) {
                _list.Remove(data);

                return true;
            } else {
                return false;
            }
        }

        public override bool DeleteDetail(int id) {
            if (_list == null) return false;
            var data = ReadDetail().Where(model => model.Id.Equals(id)).SingleOrDefault();
            if (data != null) {
                _list.Remove(data);
                return true;
            } else {
                return false;
            }
        }

        public override void Dispose() {
            _list = null;
        }

        public new string Info() {
            return "";
        }

        public double SubTotal(int id) {
            if (_list == null) return 0;
            var data = ReadDetail().Where(model => model.Id.Equals(id)).SingleOrDefault();
            if (data != null) {
                return Model.Amount;
            } else {
                return 0;
            }
        }

        internal void Update(int v, object order) {
            throw new NotImplementedException();
        }
    }
}
