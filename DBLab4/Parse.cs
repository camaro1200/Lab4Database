using System.IO;
using System;
using System.Collections.Generic;

namespace DBLab4
{
    public class Parse
    {
        public List<Tuple<int, string>> t = new List<Tuple<int, string>>();
        public List<Tuple<string, int, int, int>> v = new List<Tuple<string, int, int, int>>();
        public int key;

        public void selectOption(string line)
        {
            if (line == "1")
            {
                Console.WriteLine("we read from Config");
                key = 1;
            }

            else if (line == "0")
            {
                Console.WriteLine("we read from Database");
                key = 2;
            }


        }

        public void getShopAnalysis(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ',')
                {
                    int shopId = Convert.ToInt32(line.Substring(0, i));
                    string name = line.Substring(i + 1, line.Length - 2);
                    t.Add(Tuple.Create(shopId, name));
                }
            }
        }

        public void getProductAnalyis(string line)
        {
            int idInd = 0;
            int amountInd = 0;
            int priceInd = 0;
            String name;
            String[] lines = line.Split(',');
            int s = lines.Length / 3;
            name = lines[0];
            //Console.WriteLine(name);
            int l = 0;
            for (int i = 0; i < s; i++)
            {
                idInd = Int32.Parse(lines[l + 1]);
                amountInd = Int32.Parse(lines[l + 2]);
                priceInd = (int) Double.Parse(lines[l + 3]);
                v.Add(Tuple.Create<string, int, int, int>(name, idInd, amountInd, priceInd));
                //Console.WriteLine(idInd + "\n" + amountInd +"\n"+  priceInd);
                l += 3;
            }
        }

        /* try 
        {
            using (StreamReader sr = new StreamReader("/Users/paulshaburov/Documents/DatabaseLab4/DBLab4/Shops.txt")) 
            {
                string line;
                int k = 0;
                while ((line = sr.ReadLine()) != null) 
                {
                    p1.getShopAnalysis(line);
                    c.getShopsConfig(p1.t[k].Item1,p1.t[k].Item2 );
                    for (int i = 0; i < p1.t.Count; i++)
                    {
                        //n.CreateShop(p1.t[i].Item1, p1.t[i].Item2);
                    }
                }
            }
        }
        catch (Exception e) 
        {
            //Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

    }
    */
    }
}