using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholesaleStoreSystem
{
    internal class UsersFunction
    {
        public static void startProgram()
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
                default : Console.WriteLine("Недопустимый тип пользователя. Перезапустите систему и повторите ввод");
                    break;
            }
        }
        static void functionalForAdmin()
        {
            bool flag = true;
            int znachFlag = 0;
            while (flag)
            {
                Console.WriteLine("Доступные действия:\n1-\n2-\n3-\nВыберите номер действия: ");
                int numberFunc = int.Parse(Console.ReadLine());
                switch (numberFunc)
                {
                    case 1:
                        
                    break;
                    case 2:
                        
                    break;
                    case 3:
                        
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
        static void functionalForClient()
        {
            bool flag = true;
            int znachFlag = 0;
            while (flag)
            {
                Console.WriteLine("Доступные действия:\n1-\n2-\n3-\nВыберите номер действия: ");
                int numberFunc = int.Parse(Console.ReadLine());
                switch (numberFunc)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

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
    }
}
