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
                "[2].Product",
                "[3].Raw Product",
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
                } else if (selected == "[2].Product") {
                    DrawProduct();
                } else if (selected == "[3].Raw Product") {
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
                "Insert Data",
                "Read Data",
                "Select Data",
                "Update Data",
                "Delete Data",
                "Back",
            };

            while (true) {
                var selected = MenuSelection(menuitem);
                Console.CursorVisible = false;
                if (selected == MenuAction.Create) {
                    Console.Clear();
                    var temp = product.Read();

                    if(temp.Any()) {
                        Console.WriteLine(product.Info());
                        Console.WriteLine();

                        Console.CursorVisible = true;
                        Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());

                        dish = product.Read(id);
                        if (dish != null) {
                            Console.Write("Masukkan Customer: "); customer = Console.ReadLine();
                            Console.Write("Masukkan Qty: "); orders.Model.Qty = Convert.ToInt32(Console.ReadLine());

                            order = new Orders() {
                                Id = id,
                                Customer = customer,
                                CurrDate = DateTime.Now.ToString("dd/MM/yyyy"),
                            };

                            orders.Model.Id = id;

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
                    } else {
                        Console.Write("Empty List");
                        Console.ReadKey();
                    }

                } else if (selected == MenuAction.Read) {
                    Console.Clear();
                    int i;
                    var temp = orders.ReadDetail();
                    if (temp.Any()) {
                        foreach (OrdersDetail item in orders.ReadDetail().ToArray()) {
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine($"ID : {item.Id}");
                            Console.WriteLine($"Customer : {item.Orders.Customer}");
                            Console.WriteLine($"Name : {item.Product.Name}");
                            Console.WriteLine($"Price : {item.Product.Price}");
                            Console.WriteLine($"Current Date : {item.Orders.CurrDate}");
                            Console.WriteLine($"Qty : {item.Qty}");
                            item.Amount = item.Qty;
                            i = item.Id;
                            Console.WriteLine($"Amount : {orders.SubTotal(i)}");
                            Console.WriteLine("-----------------------------------");
                        }
                    } else {
                        Console.WriteLine("Empty List");
                    }
                    Console.ReadKey();

                } else if (selected == MenuAction.Selected) {
                    Console.Clear();
                    var temp = orders.ReadDetail();
                    if (temp.Any()) {
                        int _id;
                        Console.CursorVisible = true;
                        Console.Write("Masukkan ID: "); _id = Convert.ToInt32(Console.ReadLine());
                        var select = orders.ReadDetail(_id);
                        if (select == null) {
                            Console.Write("Data not found");
                        } else {
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine($"ID : {select.Product.Id}");
                            Console.WriteLine($"Customer : {select.Orders.Customer}");
                            Console.WriteLine($"Name : {select.Product.Name}");
                            Console.WriteLine($"Price : {select.Product.Price}");
                            Console.WriteLine($"Current Date : {select.Orders.CurrDate}");
                            Console.WriteLine($"Qty : {select.Qty}");
                            select.Amount = select.Qty;
                            Console.WriteLine($"Amount : {orders.SubTotal(_id)}");
                            Console.WriteLine("-----------------------------------");
                        }
                    } else {
                        Console.WriteLine("Can't selected, its has been empty");
                    }

                    Console.ReadKey();

                } else if (selected == MenuAction.Update) {
                    Console.Clear();
                    var temp = orders.ReadDetail();

                    if (temp.Any()) {
                        Console.WriteLine(product.Info());
                        Console.WriteLine();

                        Console.CursorVisible = true;
                        Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());

                        dish = product.Read(id);
                        if (dish != null) {
                            Console.Write("Masukkan Customer: "); customer = Console.ReadLine();
                            Console.Write("Masukkan Qty: "); orders.Model.Qty = Convert.ToInt32(Console.ReadLine());

                            order = new Orders() {
                                Id = id,
                                Customer = customer,
                                CurrDate = DateTime.Now.ToString("dd/MM/yyyy"),
                            };

                            dish = product.Read(id);

                            orders.Model.Product = dish;
                            orders.Model.Orders = order;

                            orders.UpdateDetail(id);
                        } else {
                            Console.Write("Tidak dapat melakukan order");
                            Console.ReadKey();
                        }
                    } else {
                        Console.WriteLine("Can't Update, its has been empty");
                        Console.ReadKey();
                    }

                } else if (selected == MenuAction.Delete) {
                    Console.Clear();
                    Console.CursorVisible = true;
                    var temp = orders.ReadDetail();
                    if (temp.Any()) {
                        Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                        if(orders.Read(id) != null) {
                            orders.Delete(id);
                            Console.WriteLine("Remove success");
                        } else {
                            Console.WriteLine("Can't remove, data not found");
                        }
                    } else {
                        Console.WriteLine("Can't removed, its has been empty");
                    }
                    Console.ReadKey();

                } else if (selected == MenuAction.Back) {
                    index = 0;
                    break;
                }
            }

        }

        private static void DrawProduct() {
            string name, desc;
            int id, price;

            index = 0;
            var menuitem = new List<string>() {
               "Insert Data",
                "Read Data",
                "Select Data",
                "Update Data",
                "Delete Data",
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
                    Console.Clear();
                    var temp = product.Read();
                    if (temp.Any()) {
                        foreach (Dishes item in product.Read().ToArray()) {
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine($"ID : {item.Id}");
                            Console.WriteLine($"Name : {item.Name}");
                            Console.WriteLine($"Description : {item.Description}");
                            Console.WriteLine($"Price : {item.Price}");
                            Console.WriteLine("-----------------------------------");
                        }
                    } else {
                        Console.WriteLine("Empty List");
                    }

                    Console.ReadKey();

                } else if (selected == MenuAction.Selected) {
                    Console.Clear();
                    var temp = product.Read();
                    if (temp.Any()) {
                        int _id;
                        Console.CursorVisible = true;
                        Console.Write("Masukkan ID: "); _id = Convert.ToInt32(Console.ReadLine());
                        var select = product.Read(_id);
                        if (select == null) {
                            Console.Write("Data not found");
                        } else {
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine($"ID : {select.Id}");
                            Console.WriteLine($"Name : {select.Name}");
                            Console.WriteLine($"Description : {select.Description}");
                            Console.WriteLine($"Price : {select.Price}");
                            Console.WriteLine("-----------------------------------");
                        }
                    } else {
                        Console.WriteLine("Can't selected, its has been empty");
                    }

                    Console.ReadKey();

                } else if (selected == MenuAction.Update) {
                    Console.Clear();
                    var temp = product.Read();
                    if (temp.Any()) {
                        Console.CursorVisible = true;
                        Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                        if(product.Read(id) == null) {
                            Console.WriteLine("Data not found");
                            Console.ReadKey();
                        } else {
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
                        }
                    } else {
                        Console.WriteLine("Can't Update, its has been empty");
                        Console.ReadKey();
                    }

                } else if (selected == MenuAction.Delete) {
                    Console.Clear();
                    var temp = product.Read();
                    if (temp.Any()) {
                        Console.CursorVisible = true;
                        Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                        if(product.Read(id) != null) {
                            product.Delete(id);
                            Console.WriteLine("Remove success");
                        } else {
                            Console.WriteLine("Can't remove, data not found");
                        }
                    } else {
                        Console.WriteLine("Can't removed, its has been empty");
                    }
                    Console.ReadKey();

                } else if (selected == MenuAction.Back) {
                    index = 0;
                    break;
                }
            }
        }

        private static void DrawProductRaw() {
            string name, expired, barcode;
            int id;

            index = 0;
            var menuitem = new List<string>() {
               "Insert Data",
                "Read Data",
                "Select Data",
                "Update Data",
                "Delete Data",
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

                    var temp = productraw.Read();
                    if (temp.Any()) {
                        foreach (Ingredients item in productraw.Read().ToArray()) {
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine($"ID : {item.Id}");
                            Console.WriteLine($"Name : {item.Name}");
                            Console.WriteLine($"Expired : {item.Expired}");
                            Console.WriteLine($"Barcode : {item.Barcode}");
                            Console.WriteLine("-----------------------------------");
                        }
                    } else {
                        Console.WriteLine("Empty List");
                    }
                    Console.ReadKey();

                } else if(selected == MenuAction.Selected) {
                    int _id;
                    Console.Clear();

                    var temp = productraw.Read();
                    if (temp.Any()) {
                        Console.CursorVisible = true;
                        Console.Write("Masukkan ID: "); _id = Convert.ToInt32(Console.ReadLine());
                        var select = productraw.Read(_id);
                        if (select == null) {
                            Console.Write("Data not found");
                        } else {
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine($"ID : {select.Id}");
                            Console.WriteLine($"Name : {select.Name}");
                            Console.WriteLine($"Expired : {select.Expired}");
                            Console.WriteLine($"Barcode : {select.Barcode}");
                            Console.WriteLine("-----------------------------------");
                        }
                    } else {
                        Console.WriteLine("Can't selected, its has been empty");
                    }
                    Console.ReadKey();

                } else if (selected == MenuAction.Update) {
                    Console.Clear();

                    var temp = productraw.Read();
                    if (temp.Any()) {
                        Console.CursorVisible = true;
                        Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                        if(productraw.Read(id) == null) {
                            Console.WriteLine("Data not found");
                            Console.ReadKey();
                        } else {
                            Console.Write("Masukkan Nama: "); name = Console.ReadLine();
                            Console.Write("Masukkan Expired: "); expired = Console.ReadLine();
                            Console.Write("Masukkan Barcode: "); barcode = Console.ReadLine();

                            ingredient = new Ingredients() {
                                Id = id,
                                Name = name,
                                Barcode = barcode,
                                Expired = expired,
                            };

                            productraw.Update(id, ingredient);
                        }
                    } else {
                        Console.WriteLine("Can't Update, its has been empty");
                        Console.ReadKey();
                    }

                } else if (selected == MenuAction.Delete) {
                    Console.Clear();
                    var temp = productraw.Read();
                    if (temp.Any()) {
                        Console.CursorVisible = true;
                        Console.Write("Masukkan ID: "); id = Convert.ToInt32(Console.ReadLine());
                        if(productraw.Read(id) != null) {
                            productraw.Delete(id);
                            Console.WriteLine("Remove success");
                        } else {
                            Console.WriteLine("Can't removed, Data not found");
                        }
                    } else {
                        Console.WriteLine("Can't removed, its has been empty");
                    }
                    Console.ReadKey();

                } else if (selected == MenuAction.Back) {
                    index = 0;
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
        Selected,
        Update,
        Delete,
        Back,
        Default,
    }
}
