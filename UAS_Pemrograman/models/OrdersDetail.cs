using System;
namespace UAS_Pemrograman.models {
    public class OrdersDetail {
        public int Id { get; set; }
        public Orders Orders { get; set; } = new Orders();
        public Product Product { get; set; }
        public int Qty { get; set; }

        public double Amount {
            get {
                return _amount;
            }
            set {
                _amount = 0;
                if(Product != null) {
                    if (value == 0) return;
                    _amount = Product.Price * value;
                }
            }
        }

        private double _amount;
    }
}
