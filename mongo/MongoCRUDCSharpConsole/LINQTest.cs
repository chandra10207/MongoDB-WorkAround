using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoCRUDCSharpConsole
{
	public class LINQTest
	{
		public LINQTest()
		{
            //Language Integrated Query (LINQ)

            // Specify the data source.
            int[] scores = { 97, 92, 81, 60 };

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            // Execute the query.
            foreach (int i in scoreQuery)
            {
                Console.Write(i + " ");
            }

            // Output: 97 92 81



            // Data source
            List<string> my_list = new List<string>() {
                "This is my Dog",
                "Name of my Dog is Robin",
                "This is my Cat",
                "Name of the cat is Mewmew"
        };

            // Creating LINQ Query
            // Using Method syntax
            var res = my_list.Where(a => a.Contains("Dog"));






        }


    }
}

