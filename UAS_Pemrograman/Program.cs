using System;
using UAS_Pemrograman.Processes;
using UAS_Pemrograman.models;
using System.Collections.Generic;
using System.Linq;

namespace UAS_Pemrograman{
    internal class MainClass{

        private static IProductProcessing<Dishes> product = new ProductProcessing<Dishes>();
        private static IProductProcessing<Ingredients> productraw = new ProductProcessing<Ingredients>();
        private static OrdersExtendProcessing orders = new OrdersExtendProcessing();
        private static int increment = 0;
        private static int index = 0;
        private static Dishes dish = new Dishes();
        private static Ingredients ingredient = new Ingredients();

        public static void Main(string[] args) {
            index = 0;
            var menuitem = new List<string>() {
                "[1].Orders Apply",
                "[2].Menu List",
                "[3].Base Product",
                "[4].Exit",
            };

            Console.Clear();
            product.Index();
            productraw.Index();
            orders.Index();
            while (true) {
                var selected = MainMenu(menuitem);
                if (selected == "[1].Orders Apply") {
                    DrawOrders();
                } else if (selected == "[2].Menu List") {
                    DrawProduct();
                } else if (selected == "[3].Base Product") {
                    DrawProductRaw();
                } else if (selected == "[4].Exit") {
                    orders.Dispose();
                    product.Dispose();
                    productraw.Dispose();
                    Environment.Exit(0);
                }
            }
        }

        private static void DrawOrders() {
            var order = new Orders();
            string customer;
            int id;

            index = 0;
            var menuitem = new List<string>() {
                "Create",
                "Read",
                "Update",
                "Delete",
                "Back",
            };

            while (true) {
                var selected = MenuSelection(menuitem);
                Console.CursorVisible = false;
                if (selected == MenuAction.Create) {
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());

                    dish = product.Read(id);
                    if(dish != null) {
                        Console.Write("Masukkan Customer: "); customer = Console.ReadLine();

                        order = new Orders() {
                            Id = id,
                            Customer = customer,
                            CurrDate = DateTime.Now.ToString("dd/MM/yyyy"),
                        };

                        dish = product.Read(id);

                        orders.Model.Product = dish;
                        orders.Model.Orders = order;

                        orders.Create(order);
                        increment++;
                        orders.CreateDetail();
                    } else {
                        Console.Write("Tidak dapat melakukan order");
                        Console.ReadKey();
                    }

                } else if (selected == MenuAction.Read) {
                    Console.Clear();
                    int i=0;
                    foreach (OrdersDetail item in orders.ReadDetail().ToArray()) {
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine($"ID : {item.Product.Id}");
                        Console.WriteLine($"Customer : {item.Orders.Customer}");
                        Console.WriteLine($"Name : {item.Product.Name}");
                        Console.WriteLine($"Price : {item.Product.Price}");
                        Console.WriteLine($"Current Date : {item.Orders.CurrDate}");
                        Console.WriteLine($"Qty : {item.Qty}");
                        item.Amount = item.Qty;
                        Console.WriteLine($"Amount : {orders.SubTotal(i)}");
                        Console.WriteLine("-----------------------------------");
                        i++;
                    }
                    Console.ReadKey();

                } else if (selected == MenuAction.Update) {
                    orders.Update(0, order);

                } else if (selected == MenuAction.Delete) {
                    Console.CursorVisible = true;
                    Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                    orders.Delete(id);

                } else if (selected == MenuAction.Back) {
                    index = 0;
                    //orders.Dispose();
                    break;
                }
            }

        }

        private static void DrawProduct() {
            string name, desc;
            int id, price;

            index = 0;
            var menuitem = new List<string>() {
               "Create",
               "Read",
               "Update",
               "Delete",
               "Back",
            };
            while (true) {
                var selected = MenuSelection(menuitem);
                Console.CursorVisible = false;
                if (selected == MenuAction.Create) {
                    Console.CursorVisible = true;
                    Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Masukkan Nama: "); name = Console.ReadLine();
                    Console.Write("Masukkan Harga: "); price = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Masukkan Deskripsi: "); desc = Console.ReadLine();

                    dish = new Dishes() {
                        Id = id,
                        Name = name,
                        Description = desc,
                        Price = price,
                    };
                    product.Create(dish);

                } else if (selected == MenuAction.Read) {
                    foreach (Dishes item in product.Read().ToArray()) {
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine($"ID : {item.Id}");
                        Console.WriteLine($"Name : {item.Name}");
                        Console.WriteLine($"Description : {item.Description}");
                        Console.WriteLine($"Price : {item.Price}");
                        Console.WriteLine("-----------------------------------");
                    }
                    Console.ReadKey();

                } else if (selected == MenuAction.Update) {
                    Console.CursorVisible = true;
                    Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Masukkan Nama: "); name = Console.ReadLine();
                    Console.Write("Masukkan Harga: "); price = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Masukkan Deskripsi: "); desc = Console.ReadLine();

                    dish = new Dishes() {
                        Id = id,
                        Name = name,
                        Description = desc,
                        Price = price,
                    };

                    product.Update(id, dish);

                } else if (selected == MenuAction.Delete) {
                    Console.CursorVisible = true;
                    Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                    product.Delete(id);

                } else if (selected == MenuAction.Back) {
                    index = 0;
                    //product.Dispose();
                    break;
                }
            }
        }

        private static void DrawProductRaw() {
            string name, expired, barcode;
            int id;

            index = 0;
            var menuitem = new List<string>() {
               "Create",
               "Read",
               "Update",
               "Delete",
               "Back",
            };
            while (true) {
                var selected = MenuSelection(menuitem);
                Console.CursorVisible = false;
                if (selected == MenuAction.Create) {
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Masukkan Nama: "); name = Console.ReadLine();
                    Console.Write("Masukkan Expired: "); expired = Console.ReadLine();
                    Console.Write("Masukkan Barcode: "); barcode = Console.ReadLine();

                    ingredient = new Ingredients() {
                        Id = id,
                        Name = name,
                        Barcode = barcode,
                        Expired = expired,
                    };
                    productraw.Create(ingredient);

                } else if (selected == MenuAction.Read) {
                    Console.Clear();
                    foreach (Ingredients item in productraw.Read().ToArray()) {
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine($"ID : {item.Id}");
                        Console.WriteLine($"Name : {item.Name}");
                        Console.WriteLine($"Expired : {item.Expired}");
                        Console.WriteLine($"Barcode : {item.Barcode}");
                        Console.WriteLine("-----------------------------------");
                    }
                    Console.ReadKey();

                } else if (selected == MenuAction.Update) {
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Masukkan Nama: "); name = Console.ReadLine();
                    Console.Write("Masukkan Expired: "); expired = Console.ReadLine();
                    Console.Write("Masukkan Barcode: "); barcode = Console.ReadLine();

                    ingredient = new Ingredients() {
                        Id = id,
                        Name = name,
                        Barcode = barcode,
                        Expired = expired,
                    };
                    productraw.Create(ingredient);

                    productraw.Update(0, ingredient);

                } else if (selected == MenuAction.Delete) {
                    Console.CursorVisible = true;
                    Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                    productraw.Delete(id);

                } else if (selected == MenuAction.Back) {
                    index = 0;
                    //productraw.Dispose();
                    break;
                }
            }
        }

        private static MenuAction MenuSelection(List<string> items) {
            Console.Clear();
            Console.CursorVisible = false;
            for(var i = 0; i < items.Count; i++) {
                if(i == index) {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(items[i]);
                Console.ResetColor();
            }
            var ckey = Console.ReadKey();
            if(ckey.Key == ConsoleKey.DownArrow) {
                index = (index >= items.Count - 1) ? 0 : index + 1;
            }else if(ckey.Key == ConsoleKey.UpArrow) {
                index = (index <= 0) ? items.Count-1 : index - 1;
            } else if(ckey.Key == ConsoleKey.Enter) {
                return (MenuAction)index;
            }
            Console.Clear();
            return MenuAction.Default;
        }

        private static string MainMenu(List<string> items) {
            Console.Clear();
            Console.CursorVisible = false;
            for (var i = 0; i < items.Count; i++) {
                if (i == index) {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(items[i]);
                Console.ResetColor();
            }
            var ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow) {
                index = (index >= items.Count-1) ? 0 : index + 1;
            } else if (ckey.Key == ConsoleKey.UpArrow) {
                index = (index <= 0) ? items.Count-1 : index - 1;
            } else if (ckey.Key == ConsoleKey.Enter) {
                return items.ElementAt(index);
            }
            Console.Clear();
            return "";
        }
    }

    public enum MenuAction {
        Create,
        Read,
        Update,
        Delete,
        Back,
        Selected,
        Default,
    }
}
