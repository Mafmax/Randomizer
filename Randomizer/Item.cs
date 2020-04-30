using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer
{
    public class Item
    {

        private int id;
        public int ID { get { return id; } }
        private string name;
        public string Name { get { return name; } }
        private float probability;
        public float Probability { get { return probability; }  }

        private float field;


        public Item(int id, string name, float chance)
        {
            this.id = id;
            this.name = name;
            this.probability = chance;
        }



    }
}
