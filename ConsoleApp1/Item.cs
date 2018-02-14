

namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Item
    {
        public int itemNo;
        public int itemBenefit;
        public int itemWeight;
        public Item(int newItemBenefit, int newItemNo, int newItemWeight)
        {
            itemBenefit = newItemBenefit;
            itemNo = newItemNo;
            itemWeight = newItemWeight;
        }
    }
}
