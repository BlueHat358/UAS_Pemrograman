using System;
namespace UAS_Pemrograman.models {
    public class Ingredients : Product{
        public override int Id { get; set; }
        public string Barcode { get; set; }
        public string Expired { get; set; }
    }
}
