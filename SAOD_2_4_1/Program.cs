using System;

namespace Program
{
    internal static class Program
    {
        private static void Main()
        {
            int choice = 0 ; bool work = true;
            string[] HashTable = new string[10];
            HashTable = CreateHashTable(HashTable);
            PrintHashTable(HashTable);
            while (work)
            {
                Console.Write("\nВыберите дальнейшее действие.\n1 - Вывести хэш-таблицу\n2 - Найти ключ в массиве\n3 - Завершение работы\nВыбор: ");
                choice = CheckedInput(1, 3);
                switch (choice)
                {
                    case 1:
                        PrintHashTable(HashTable);
                        break;

                    case 2:
                        string searchKey;
                        int index = 0;
                        Console.Write("Введите значение ключа: ");
                        searchKey = Console.ReadLine();
                        if (searchKey == String.Empty)
                        {
                            Console.WriteLine("Вы ввели пустую строку.");
                            break;
                        }
                        index = Search(HashTable, searchKey);
                        if (index == -1)
                        {
                            Console.WriteLine("Такого ключа в таблице нет.");
                            break;
                        }

                        Console.WriteLine($"Ключ {searchKey} найден. Его индекс: {index}");
                        break;

                    case 3:
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

        private static void PrintHashTable(string[] HashTable)
        {
            Console.WriteLine("Хэш-таблица: ");
            for (int i = 0; i < HashTable.Length; i++)
            {
                Console.WriteLine($"Ключ: {HashTable[i]}, индекс: {i}");
            }
        }

        private static int Search(string[] table, string key)
        {
            if (table[GetHashCode(key)] == key)
            {
                return GetHashCode(key);
            }
            return - 1;
        }
        
        private static int GetHashCode(string key)
        {
            int code = 0;
            for (int i = 0; i < key.Length; i++)
            {
                code += (int)key[i];
            }
            return code % 10;
        }

        private static string ToUpperCapital(string key) => key.Substring(0,1).ToUpper() + key.Substring(1, key.Length-1);

        private static string[] CreateHashTable(string[] HashTable)
        {
            int size = HashTable.Length;
            string currentKey;
            Console.WriteLine("Введите 10 ключевых слов языка Pascal, которые будут являться ключами.");
            int index, mainIndex;
            for (int i = 0; i < size; i++)
            {
                int total = 0;
                bool isConflict = false;
                while (!isConflict)
                {
                    Console.Write("Введите значение ключа (ключевое слово из ЯП Pascal): ");
                    currentKey = Console.ReadLine();
                    mainIndex = GetHashCode(currentKey);

                    if (HashTable[mainIndex] == null)
                    {
                        HashTable[mainIndex] = currentKey;
                        Console.WriteLine($"Index: {mainIndex}");
                        break;
                    }

                    else
                    {
                        string newKey = currentKey.ToLower();
                        index = GetHashCode(newKey);
                        if (HashTable[index] == null)
                        {
                            Console.WriteLine($"Введённый вами ключ нарушал беcконфликтность.Вместо него {newKey}");
                            HashTable[index] = newKey;
                            Console.WriteLine($"Index: {index}");
                            break;
                        }

                        newKey = currentKey.ToUpper();
                        index = GetHashCode(newKey);
                        if (HashTable[index] == null)
                        {
                            Console.WriteLine($"Введённый вами ключ нарушал беcконфликтность.Вместо него {newKey}");
                            HashTable[index] = newKey;
                            Console.WriteLine($"Index: {index}");
                            break;
                        }

                        newKey = ToUpperCapital(currentKey);
                        index = GetHashCode(newKey);
                        if (HashTable[index] == null)
                        {
                            Console.WriteLine($"Введённый вами ключ нарушал беcконфликтность.Вместо него {newKey}");
                            HashTable[index] = newKey;
                            Console.WriteLine($"Index: {index}");
                            break;
                        }

                        int nearFree = 0;
                        for (int j = 0; j < HashTable.Length; j++)
                        {
                            if (HashTable[j] == null)
                            {
                                nearFree = j;
                                break;
                            }
                        }

                        int thisIndex = 40 + (nearFree < mainIndex ? 10 - mainIndex + nearFree : nearFree - mainIndex);
                        currentKey = String.Concat(currentKey, Convert.ToString(Convert.ToChar(thisIndex)));
                        Console.WriteLine($"К сожалению введённый Вами ключ нарушал бесконфликтность ключей, поэтому я автоматически добавл в конец" +
                            $"спец. символ, результат: {currentKey}");
                        HashTable[GetHashCode(currentKey)] = currentKey;
                        Console.WriteLine($"Index: {GetHashCode(currentKey)}");
                        break;
                    }
                }
            }
            return HashTable; 
        }
    }
}
