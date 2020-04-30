using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Randomizer
{
    class Program
    {
        static void Main(string[] args)
        {
            const int iterations = 10000000;
            Item tempItem;
            StringBuilder tempString = new StringBuilder();
            int counter=0;
            List<Item> drop = new List<Item>();
            Item randomItem;

            #region Instance items
            Item item_1 = new Item(0,"Шляпа каридорного мстителя", 13f);
            Item item_2 = new Item(1,"Меч бродяги", 25.5f);
            Item item_3 = new Item(2,"Поломанный доспех лейтенанта", 38.2f);
            Item item_4 = new Item(3,"Магическая сфера", 7f);
            Item item_5 = new Item(4,"Реликвия пожирателя душ", 0.3f);
            Item item_6 = new Item(5,"Мешочек с золотом", 15f);
            Item nothing = new Item(int.MaxValue,"Ничего", 0f);
            drop.Add(item_1);
            drop.Add(item_2);
            drop.Add(item_3);
            drop.Add(item_4);
            drop.Add(item_5);
            drop.Add(item_6);
            drop.Add(nothing);
            #endregion


            Dictionary<string, int> massCounter = new Dictionary<string, int>();
            foreach(var item in drop)
            {
                massCounter[item.Name] = 0;
            }
            try
            {
                
                FieldInfo field = typeof(Item).GetField("probability", BindingFlags.NonPublic | BindingFlags.Instance);

              
                while (counter < iterations)
                {
                    randomItem = Randomizer.GetRandom<Item>(drop, field);


                    massCounter[randomItem.Name]++;


                    counter++;
                }

                Console.WriteLine("Общее количество выпавших предметов из "+counter+": ");
                foreach(var item in drop)
                {
                    tempItem = drop.Where(x => x.ID == item.ID).FirstOrDefault();
                    tempString.Clear();
                    tempString.Append("Предмет: \"" + tempItem.Name + "\"" );
                    tempString.Append(" с вероятностью " + tempItem.Probability +"%");
                    tempString.Append(": " + massCounter[tempItem.Name] + " раз.");
                    tempString.Append("Итоговая вероятность: "+ massCounter[tempItem.Name] / (float)iterations*100+"%.");
                    Console.WriteLine(tempString.ToString());
                    
                }
                Console.WriteLine(new string('-',20));
            }
            catch(Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }

        }
    }
}
