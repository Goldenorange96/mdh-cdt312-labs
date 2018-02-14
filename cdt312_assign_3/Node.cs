namespace Cdt312_assign_3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Node
    {
        public int Id;
        public double X;
        public double Y;
        public Node(int newId, double newX, double newY)
        {
            Id = newId;
            X = newX;
            Y = newY;
        }
    }
}
