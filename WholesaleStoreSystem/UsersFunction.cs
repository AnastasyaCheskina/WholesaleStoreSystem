using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WholesaleStoreSystem
{
    internal class UsersFunction
    {
        private static string pathTableProducts = "Products.csv";
        private static string pathUserCart = "Cart.csv";
        private static string pathOrders = "Order.csv";
        private static List<Products> productsList = WorkWithFiles.getDataAtFiles(pathTableProducts); //все товары
        private static List<Cart> allProductsAtCart = WorkWithFiles.getDataAtFilesCart(pathUserCart); //товары в корзине
        private static List<Cart> allOrders = WorkWithFiles.getDataAtFilesCart(pathOrders); //заказанные товары
        public static void startProgram() //старт программы
        {
            Console.WriteLine("ИС Оптовый склад\nВыберите тип пользователя:\n0-Администратор, 1-Клиент");
            int typeUser = int.Parse(Console.ReadLine());
            switch (typeUser)
            {
                case 0: 
                    functionalForAdmin();
                    break;
                case 1:
                    functionalForClient();
                    break;
                default : 
                    Console.WriteLine("Недопустимый тип пользователя. Перезапустите систему и повторите ввод");
                    break;
            }
        }
        private static void functionalForAdmin() //метод вызова функционала для админа
        {
            bool flag = true;
            int znachFlag = 0;
            while (flag)
            {
                Console.WriteLine("Доступные действия:\n1-Показать список товаров\n2-Добавить новый товар в список\n3-Удалить товар из списка\n4-Поиск\n5-Выгрузить изменения\n6-Посмотреть историю заказов\nВыберите номер действия: ");
                int numberFunc = int.Parse(Console.ReadLine());
                switch (numberFunc)
                {
                    case 1:
                        showAllProducts();
                        break;
                    case 2:
                        addNewProduct();
                        break;
                    case 3:
                        deleteProductAtList();
                        break;
                    case 4:
                        findProduct();
                        break;
                    case 5:
                        addChanges();
                        break;
                    case 6:
                        showHistoryOrders();
                        break;
                    default:
                        Console.WriteLine("Неверное значение, повторите попытку");
                        break;
                }
                Console.WriteLine("Желаете продолжить или завершить программу?\n0-Завершить\n1-Продолжить");
                znachFlag = int.Parse(Console.ReadLine());
                if (znachFlag == 1) flag = true;
                else
                {
                    flag = false;
                    Console.WriteLine("Работа программы завершена, нажмите любую клавишу чтобы выйти");
                    Console.ReadKey();
                }
            }
        }
        private static void functionalForClient() //метод вызова функционала для клиента
        {
            bool flag = true;
            int znachFlag = 0;
            while (flag)
            {
                Console.WriteLine("Доступные действия:\n1-Показать список товаров\n2-Добавить товар в корзину и перейти\n3-Поиск\n4-Очистить корзину\n5-Перейти в корзину");
                Console.WriteLine("6-Оформить заказ\n7-Посмотреть историю заказов\nВыберите номер действия:");
                int numberFunc = int.Parse(Console.ReadLine());
                switch (numberFunc)
                {
                    case 1:
                        showAllProducts();
                        break;
                    case 2:
                        addAtCart();
                        showCart();
                        break;
                    case 3:
                        findProduct();
                        break;
                    case 4:
                        clearCart();
                        break;
                    case 5:
                        showCart();
                        break;
                    case 6:
                        placeOrder();
                        break;
                    case 7:
                        showHistoryOrders();
                        break;
                    default:
                        Console.WriteLine("Неверное значение, повторите попытку");
                        break;
                }
                Console.WriteLine("Желаете продолжить или завершить программу?\n0-Завершить\n1-Продолжить");
                znachFlag = int.Parse(Console.ReadLine());
                if (znachFlag == 1) flag = true;
                else
                {
                    flag = false;
                    WriteCSV(allProductsAtCart, pathUserCart);
                    Console.WriteLine("Работа программы завершена, нажмите любую клавишу чтобы выйти");
                }
            }
        }
        private static void showAllProducts() //вывод всех продуктов на экран
        {
            foreach (var product in productsList)
            {
                Console.WriteLine(product);
            }
        }
        private static int dialogAddAtCart() //метод для реализации диалога с пользователем в корзине
        {
            Console.WriteLine("Введите id нужного товара:");
            int id = int.Parse(Console.ReadLine());
            if (id < 0 || id >= productsList.Count)
            {
                Console.WriteLine("Товар не найден");
                id = -1;
            }
            else Console.WriteLine("Операция выполнена!");
            return id;
        }
        private static List<Cart> addAtCart() //добавление товаров в корзину 
        {
            int id = dialogAddAtCart();
            Cart cartUser = new Cart();
            if (id >= 0)
            {
                foreach (var item in productsList)
                {
                    if (item.Id == id)
                    {
                        cartUser.Id = item.Id;
                        cartUser.Name = item.Name;
                        cartUser.Price = item.Price;
                        allProductsAtCart.Add(cartUser);
                        break;
                    }
                    else continue;
                }
            }
            return allProductsAtCart;
        }
        private static void showCart() //вывод корзины на экран с подсчетом итоговой стоимости
        {
            if (allProductsAtCart.Count <= 0) Console.WriteLine("Корзина пуста");
            else
            {
                foreach (var cart in allProductsAtCart)
                {
                    Console.WriteLine(cart);
                }
            }
        }
        private static List<Products> deleteProductAtList() //удалить товар из списка всех товаров
        {
            Console.WriteLine("Введите id нужного товара:");
            int id = int.Parse(Console.ReadLine());
            if (!(id < 0 || id >= productsList.Count))
            {
                List<Products> changeProducts = productsList.FindAll(item => item.Id == id);
                productsList.Remove(changeProducts.First());
                Console.WriteLine("Товар под номером {0} был удален",id);
            }
            else Console.WriteLine("Товар не найден");
            return productsList;
        }
        private static List<Products> addNewProduct() //добавить новый товар в список всех товаров
        {
            Console.WriteLine("Введите наименование продукции:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите цену продукции:");
            double price = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество продукции на складе:");
            int count = int.Parse(Console.ReadLine());
            int id = productsList.Last().Id + 1;
            Products products = new Products(id, name, price, count);
            productsList.Add(products);
            Console.WriteLine("Товар был успешно добавлен!");
            return productsList;
        }
        private static void findProduct() //поиск товаров по имени
        {
            Console.WriteLine("Введите поисковой запрос: ");
            string text = Console.ReadLine();
            List<Products> findText = productsList.Where(x=>x.Name.Contains(text)).ToList();
            if (findText.Count > 0)
            {
                Console.WriteLine("Найденные запросы:");
                foreach (var item in findText)
                {
                    Console.WriteLine(item);
                }
            }
            else Console.WriteLine("По вашему запросу ничего не найдено");
        }
        private static void WriteCSV<T>(IEnumerable<T> items, string path) //записать изменения в файл
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            File.WriteAllText(path, string.Empty);
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join("; ", props.Select(p => p.Name)));
                foreach (var item in items)
                {
                    writer.WriteLine(string.Join("; ", props.Select(p => p.GetValue(item, null))));
                }
            }
        }
        private static void addChanges() //синхронизация изменений с базой
        {
            var people = productsList;
            WriteCSV(people, pathTableProducts);
            Console.WriteLine("Выгрузка данных завершена");
        }
        private static List<Cart> clearCart() //удаление товаров из корзины (все возможные варианты)
        {
            Console.WriteLine("Введите 0, если нужно очистить корзину полностью\nВведите 1, если хотите удалить конкретный товар");
            int enter = int.Parse(Console.ReadLine());
            switch (enter)
            {
                case 0:
                    allProductsAtCart.Clear(); //очистить все
                    Console.WriteLine("Все товары из корзины удалены!");
                    break;
                case 1:
                    allProductsAtCart = findProductAtCart(); //очистить конкретный
                    break;
            }
            return allProductsAtCart;
        }
        private static List<Cart> findProductAtCart() //удаление конкретного товара из корзины
        {
            Console.WriteLine("Введите id нужного товара:");
            int id = int.Parse(Console.ReadLine());
            if (!(id < 0 || id >= productsList.Count))
            {
                List<Cart> changeProducts = allProductsAtCart.FindAll(item => item.Id == id);
                allProductsAtCart.Remove(changeProducts.First());
                Console.WriteLine("Товар под номером {0} был удален", id);
            }
            return allProductsAtCart;
        }
        private static List<Cart> placeOrder() //оформить заказ
        {
            double sum = 0;
            if (allProductsAtCart.Count == 0) Console.WriteLine("Корзина пуста и Вы не можете оформить заказ");
            else
            {
                foreach (var product in allProductsAtCart)
                {
                    sum += product.Price;
                }
                Console.WriteLine("Состав заказа:");
                showCart();
                Console.WriteLine("Итоговая стоимость: " + sum);
                Console.WriteLine("Дата оформления заказа: " + DateTime.Now);
                Console.WriteLine("Заказ успешно оформлен");
                WriteCSV(allProductsAtCart, pathOrders);
                allProductsAtCart.Clear();
            }
            return allProductsAtCart;
        }
        private static void showHistoryOrders() //показать историю заказов
        {
            double sum = 0;
            if (allOrders.Count > 0)
            {
                Console.WriteLine("Список заказанных товаров:");
                foreach (var product in allOrders)
                {
                    sum += product.Price;
                    Console.WriteLine(product);
                }
                Console.WriteLine("Всего было куплено товаров на сумму: " + sum);
            }
            else Console.WriteLine("Вы не приобрели ни одного товара");
        }
    }
}
