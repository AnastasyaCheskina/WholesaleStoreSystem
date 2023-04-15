﻿using System;
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
        private static List<Products> productsList = WorkWithFiles.getDataAtFiles(pathTableProducts); //все товары
        private static List<Cart> allProductsAtCart = new List<Cart>(); //товары в корзине
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
                Console.WriteLine("Доступные действия:\n1-Показать список товаров\n2-Добавить новый товар в список\n3-Удалить товар из списка\n4-Поиск\n5-Выгрузить изменения\nВыберите номер действия: ");
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
                Console.WriteLine("Доступные действия:\n1-Показать список товаров\n2-Добавить товар в корзину и перейти\n3-Поиск\nВыберите номер действия: ");
                int numberFunc = int.Parse(Console.ReadLine());
                switch (numberFunc)
                {
                    case 1:
                        showAllProducts();
                        break;
                    case 2:
                        showCart();
                        break;
                    case 3:
                        findProduct();
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
            else Console.WriteLine("Товар успешно добавлен в корзину!");
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
            List<Cart> allProductsAtCart = addAtCart();
            double sum = 0;
            if (allProductsAtCart.Count <= 0) Console.WriteLine("Корзина пуста");
            else
            {
                foreach (var cart in allProductsAtCart)
                {
                    sum += cart.Price;
                    Console.WriteLine(cart);
                }
                Console.WriteLine("Итоговая стоимость: "+sum);
            }
        }
        private static List<Products> deleteProductAtList() //удалить товар из списка
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
        private static List<Products> addNewProduct() //добавить новый товар в список
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
        private static void findProduct() //поиск по полному совпадению
        {
            Console.WriteLine("Введите поисковой запрос: (только точное совпадение по названию)");
            string text = Console.ReadLine();
            List<Products> findText = productsList.Where(x=>x.Name.Contains(text)).ToList();

            //List<Products> findText = new List<Products>();
            //foreach (var item in productsList)
            //{
            //    if (item.Name == text) findText.Add(item);
            //}
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
        private static void WriteCSV<T>(IEnumerable<T> items, string path)
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
    }
}
