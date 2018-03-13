namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    class Utils
    {
        /*
         * input: reference to a list representing the space.
         * output: reference to a list containing all cities.
         * 
         * Function reads from hardcoded file and generates a Individual list containing file contents
         */
        public static void ReadFileAndGenerateList(ref List<City> allCities)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:.\berlin52.tsp");
            string line = null;
            string[] subStrings;
            int id = 0;
            while ((line = file.ReadLine()) != null)
            {
                subStrings = line.Split(null);
                if (int.TryParse(subStrings[0], out id))
                {
                    City newCity = new City(id, float.Parse(subStrings[1]), float.Parse(subStrings[2]), 0.0);
                    allCities.Add(newCity);
                }
            }
            file.Close();
            allCities.Add(allCities[0]);

        }
    }
}
