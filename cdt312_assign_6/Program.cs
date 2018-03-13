namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    class Program
    {
        static void Main(string[] args)
        {
            List<City> allCities = new List<City>();
            Utils.ReadFileAndGenerateList(ref allCities);
        }
    }
}
