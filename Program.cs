using System;
using System.Collections.Generic;
using System.Linq;
using Fastenshtein;

namespace ListComparer
{
    class Program
    {
        /// <summary>
        /// Interview question about filtering two similar lists with minor string differences. Short proof of concept by Christiaan Theron
        /// This is a simple proof of concept to demonstrate comparing lists containing string elements by using Levenshtein distances https://en.wikipedia.org/wiki/Levenshtein_distance
        /// It works by computing the minimum 'distance between two strings, a.k.a  minium number of steps (in this case char changes) required to match two strings.
        /// I used an implementation of Levenshtein distance calculation courtesy of Dan Harltey on GitHub https://github.com/DanHarltey/Fastenshtein)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //I simplified the two groups of values to simple lists containing names. Just to demonstrate the principle
            //Generate 2 lists of names
            //Normal list
            List<string> myListA = new List<string>(new string[] { "General Electric", "VISA", "Zara", "NTT Data", "Smirnoff", "3M", "Burberry", "Toyota Motor Corporation", "Heineken Brewery", "MTV", "Morgan Stanley", "Porsche", "Samsung Group", "AT&T", "Allianz", "Caterpillar Inc.", "BlackBerry", "Home Depot", "Moët et Chandon", "United Parcel Service" });
            //This one will contain errors
            List<string> myListB = new List<string>(new string[] { "General Electric", "VISA", "Zara", "NTT Data", "Smarnoff Inc", "3M", "Burberry", "Toyota Motor Corporation", "Heineken Brewery", "MTV", "Morgan Stanley", "Porsche", "Samsung Group", "AT&T", "Allianz", "Caterpillar Inc.", "BlackBerry", "Home Depot", "Moet et Chandon", "United Parcel Service" });

            //Find items with simple LINQ query that occur in List A but have no perfect match in List B( with misspelled name and edits)
            var listANotinListB = (from stringInA in myListA
                                where !myListB.Any(stringInB => stringInA == stringInB)
                                select stringInA).ToList();

            //Only loop through list of words without perfect matches in List B
            foreach (string compareString in listANotinListB)
            {
                Levenshtein lev = new Levenshtein(compareString);
                int minLevDist = 0;
                string closest = null; 

                for (int i = 0; i < myListB.Count; i++)
                {
                    if (i == 0)
                    {
                        minLevDist = lev.Distance(myListB[i]);
                        closest = myListB[i];
                        continue;
                    }
                    else
                    {
                        int levenshteinDistance = lev.Distance(myListB[i]);

                        if (levenshteinDistance < minLevDist)
                        {
                            minLevDist = lev.Distance(myListB[i]);
                            closest = myListB[i];
                        }
                    }

                }

                //For now just printing the closest matches out
                Console.WriteLine("Closest match in list B for \"{0}\" is \"{1}\"", compareString, closest);
            }

            Console.ReadLine();
        }
    }
}

