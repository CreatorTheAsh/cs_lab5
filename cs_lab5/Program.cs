using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace cs_lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkDB MyDB = new WorkDB();
            MyDB.Linq15Variants();//lab 4

            int temp; 
            do
            {
                Console.WriteLine("0 - Exit\n1 - Add\n2 - Show\n3 - Save\n4 - Load");
                int.TryParse(Console.ReadLine(), out temp);
                switch (temp)
                {
                    case 0: break;
                    case 1: MyDB.AddProductFast();break;
                    case 2: MyDB.Show();break;
                    case 3: MyDB.Save();break;
                    case 4: MyDB.Load();break;
                    default:break;
                }
            }
            while (0 < temp);
            Console.ReadKey();
        }
    }
}
