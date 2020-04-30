using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Randomizer
{
    public static class Randomizer
    {


        public static T GetRandom<T>(List<T> drop, FieldInfo chanceField)
        {
            int tempItemId, resultItemId=0;

            float sum=0;
            Dictionary<int, float> items = new Dictionary<int, float>();


            if (chanceField == null)
            {
                throw new MissingFieldException("У коллекции предметов не найдено поле с вероятностью. ");
            }

            foreach (var item in drop)
            {

                items[(int)typeof(T).GetField("id",BindingFlags.NonPublic | BindingFlags.Instance).GetValue(item)]= (float)chanceField.GetValue(item);

            }



            float random = (float)(new Random()).NextDouble() * 100;
            foreach(var key in items.Keys)
            {
                sum += items[key];
                
            }

            if(sum<100)
            {
                items[int.MaxValue] = 100 - sum;
                foreach(var item in drop)
                {
                    if((int)typeof(T).GetField("id", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(item) == int.MaxValue)
                    {
                        chanceField.SetValue(item,100 - sum);
                    }
                }
            }
            else if(sum>100)
            {
                var coeff = 100 / sum;
                foreach(var key in items.Keys)
                {
                    items[key] *= coeff;
                }
            }


            foreach(var key in items.Keys)
            {
                if (items[key] >= random)
                {
                    resultItemId = key;
                    break;
                }
                random -= items[key];
            }

            
            foreach(var item in drop)
            {
                tempItemId = (int)typeof(T).GetField("id", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(item);
                //Console.WriteLine(tempItemId + "<---->" + resultItemId);
                if (tempItemId == resultItemId)
                {

                    
                    return item;
                }

            }


            return default(T);
        }

    }
}
