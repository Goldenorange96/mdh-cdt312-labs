using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdt312_assign_4
{
    class ListUtilities
    {
        //public static List<City> ShuffleList(List<City> listToShuffle, int start, int end)
        //{
        //    List<City> randomisedList = new List<City>(listToShuffle);
        //    int j = rngGlobal.Next(start, end);
        //    City tmp = new City();
        //    for (int i = start; i < end; i++)
        //    {
        //        tmp = listToShuffle[j];
        //        listToShuffle[j] = listToShuffle[i];
        //        listToShuffle[i] = tmp;
        //        j = rngGlobal.Next(start, end);
        //    }
        //    return randomisedList;
        //}

        public static void PrintList(List<Passenger> listToPrint)
        {
            foreach (Passenger passenger in listToPrint)
            {
                Console.WriteLine("Class: {0}, Age: {1}, Sex: {2}, Survived: {3};", passenger.Class, passenger.Age, passenger.Sex, passenger.Survived);
            }
        }
    }
}
