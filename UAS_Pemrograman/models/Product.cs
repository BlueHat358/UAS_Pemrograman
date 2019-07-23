using System;
namespace UAS_Pemrograman.models {
    public abstract class Product {
        public abstract int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
