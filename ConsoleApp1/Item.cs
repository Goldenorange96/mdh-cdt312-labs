using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Item
    {
        public int itemBenefit;
        public int itemNo;
        public int itemWeight;
        public Item(int newItemBenefit, int newItemNo, int newItemWeight)
        {
            itemBenefit = newItemBenefit;
            itemNo = newItemNo;
            itemWeight = newItemWeight;
        }
    }
}
