using System;

namespace Program
{
    internal static class Program
    {
        private const int m = 5;
        private static int count = 0;
        private static void Main()
        {
            int choice = 0; bool work = true;
            string[] HashTable = new string[m];
            while (work)
            {
                Console.Write("\nВыберите действие.\n1 - Вывести хэш-таблицу\n2 - Найти ключ в массиве\n3 - Добавить ключ в хэш-таблицу\n4 - Завершение работы\nВыбор: ");
                choice = CheckedInput(1, 4);
                switch (choice)
                {
                    case 1:
                        if (count == 0)
                        {
                            Console.WriteLine("Таблица пуста.");
                            break;
                        }
                        PrintHashTable(HashTable);
                        break;

                    case 2:
                        if (count == 0)
                        {
                            Console.WriteLine("Таблица пуста.");
                            break;
                        }
                        string searchKey;
                        int index = 0;
                        int counter = 0;
                        Console.Write("Введите значение ключа: ");
                        searchKey = Console.ReadLine();
                        if (searchKey == String.Empty)
                        {
                            Console.WriteLine("Вы ввели пустую строку.");
                            break;
                        }
                        index = Search(HashTable, searchKey, ref counter);
                        if (index == -1)
                        {
                            Console.WriteLine("Такого ключа в таблице нет.");
                            break;
                        }

                        Console.WriteLine($"Ключ {searchKey} найден. Его индекс: {index}. Число сравнений: {counter}");
                        break;

                    case 3:
                        if (count == m)
                        {
                            Console.WriteLine("Таблица переполнена. Добавить нечего"); break;
                        }
                        string newKey;
                        Console.Write("Введите новый ключ - слово (например, фамилию): ");
                        newKey = Console.ReadLine();

                        if (newKey == String.Empty)
                        {
                            Console.WriteLine("Вы ввели пустую строку.");
                            break;
                        }

                        Add(HashTable, newKey);

                        break;

                    case 4:
                        for (int i = 0; i < HashTable.Length; i++)
                        {
                            HashTable[i] = null;
                        }
                        HashTable = null;
                        work = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private static int CheckedInput(int begin, int end)
        {
            bool isRightInt = false;
            int result;
            while (true)
            {
                isRightInt = Int32.TryParse(Console.ReadLine(), out result);
                if (!isRightInt)
                {
                    Console.Write("Вы ввели не число, попробуйте еще раз: ");
                    continue;
                }
                if (!(result <= end && result >= begin))
                {
                    Console.Write($"Вы ввели число не в пределах от {begin} до {end}. Попробуйте ещё раз");
                    continue;
                }
                return result;
                break;
            }
        }

        private static void Add(string[] table, string newKey)
        {
            int counter = 0;
            int index = GetHashCode(newKey);
            while (table[index] != null)
            {
                if (index == table.Length - 1)
                {
                    index = -1;
                }
                index++;
                counter++;
            }

            counter++;
            table[index] = newKey;

            count++;
            Console.WriteLine(count);

            Console.WriteLine($"Число сравнений при добавлении: {counter}");

        }

        private static void PrintHashTable(string[] HashTable)
        {
            Console.WriteLine("Хэш-таблица: ");
            for (int i = 0; i < HashTable.Length; i++)
            {
                if (HashTable[i] != null)
                {
                    Console.WriteLine($"Ключ: {HashTable[i]}, индекс: {i}");
                }
            }
        }

        private static int Search(string[] table, string key, ref int counter)
        {
            int index = GetHashCode(key);
            int startIndex = index;
            counter++;
            if (table[index] == key)
            {
                return index;
            }
            index++;
            if (index == table.Length)
            {
                index = 0;
            }
            while (index != startIndex)
            {
                counter++;
                if (table[index] == key)
                {
                    return index;
                }
                index++;
                if (index == table.Length)
                {
                    index = 0;
                }
            }
            return -1;
        }

        private static int GetHashCode(string key)
        {
            int code = 0;
            for (int i = 0; i < key.Length; i++)
            {
                code += (int)key[i];
            }
            return code % m;
        }
    }
}

