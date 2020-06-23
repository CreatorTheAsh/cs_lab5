using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace cs_lab4
{
    class WorkDB
    {
        List<Product> Stoc = new List<Product>();
        public void AddProduct(Product newProduct)
        {
            ProductHolder.Increase();
            Stoc.Add(newProduct);
        }
        public void AddProductFast()
        {
            Console.Write("Fast add product\n");
            Console.Write("Product id:\t\t{0}\n", ProductHolder.product_Id); ProductHolder.Increase();
            Console.Write("Product name:\t\t");
            string tempProduct_name = Console.ReadLine();
            Console.Write("Product priсe:\t\t");
            int tempProduct_priсe = Convert.ToInt32(Console.ReadLine());
            Console.Write("Product quantity:\t");
            int tempProduct_quantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Product producer:\t");
            string tempProduct_producer = Console.ReadLine();
            Product newProduct = new Product(tempProduct_name, tempProduct_priсe, tempProduct_quantity, tempProduct_producer);
            Stoc.Add(newProduct);
        }
        public void RemoveProduct(Product delProduct)
        {
            if (Stoc.Remove(delProduct))
            {
                Console.WriteLine("Delleting sucsessful");
            }
            else { Console.WriteLine("Delleting error"); }
        }
        public void Show()
        {
            foreach (Product p in Stoc)
            {
                p.ProductInfo();
            }
        }

        public void Save()
        {
            var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), //Linq to XML
                new XElement("Stoc",
                    Stoc.Select(
                       item => new XElement("Product", new XAttribute("Product_Id", item.Product_Id),
                    new XElement("Product_name", item.Product_name),
                    new XElement("Product_priсe", item.Product_priсe),
                    new XElement("Product_quantity", item.Product_quantity),
                    new XElement("Product_producer", item.Product_producer),
                    new XElement("DataBecomeToStock", item.DataBecomeToStock)
                 ))));
            xmlDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc2.xml"));
        }

        public void Load()
        {

            var std = from Prod in XDocument.Load(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml")).Descendants("Product")
                      select new Product
                      {

                          Product_Id = (int)Prod.Attribute("Product_Id"),
                          Product_name = Prod.Element("Product_name").Value.ToString(),
                          Product_priсe = (int)Prod.Attribute("Product_priсe"),
                          Product_quantity = (int)Prod.Attribute("Product_quantity"),
                          Product_producer = Prod.Element("Product_name").Value.ToString(),
                          DataBecomeToStock = (DateTime)Prod.Attribute("Product_quantity"),               
                      };
            foreach(var Rnd in std)
            {
                Console.WriteLine(Rnd.ToString());
            }
        }
        public void Linq15Variants()
        {
            Random rnd = new Random();
            List<Product> d1 = new List<Product>();
            List<Product> d2 = new List<Product>();
            for (int x = 0; x < 10; x++)
            {
                Random r = new Random();
                string nameProduct1 = "";
                string nameProduct2 = "";
                for (int i = 0; i < 5; i++)
                {
                    char a = Convert.ToChar(r.Next(97, 122));
                    char b = Convert.ToChar(r.Next(97, 122));
                    nameProduct1 += a;
                    nameProduct2 += a;
                }
                Product p1 = new Product(nameProduct1, rnd.Next(0, 100), rnd.Next(0, 100), "Farm");
                Product p2 = new Product(nameProduct2, rnd.Next(0, 100), rnd.Next(0, 100), "Factory");
                d1.Add(p1);
                d2.Add(p2);
            }

            //----------------------------------------------

            Console.WriteLine("Простая выборка элементов");
            var q1 = from x in d1
                     select x;
            foreach (Product x in d1)
                x.ProductOnelineInfo();

            Console.WriteLine("Выборка отдельного поля (проекция)");
            var q2 = from x in d1
                     select x.Product_priсe;
            foreach (var x in q2)
                Console.WriteLine(x);

            Console.WriteLine("Создание нового объекта анонимного типа");
            var q3 = from x in d1
                     select new { IDENTIFIER = x.Product_Id, VALUE = x.Product_priсe };
            foreach (var x in q3)
                Console.WriteLine(x);

            Console.WriteLine("Условия");
            var q4 = from x in d1
                     where x.Product_Id > 1
                     select x;
            foreach (var x in q4)
                x.ProductOnelineInfo();

            Console.WriteLine("Сортировка");
            var q5 = from x in d1
                     where x.Product_Id > 1
                     orderby x.Product_producer descending, x.Product_Id descending
                     select x;
            foreach (var x in q5)
                x.ProductOnelineInfo();

            Console.WriteLine("Использование SkipWhile и TakeWhile");
            int[] intArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var qw = intArray.SkipWhile(x => (x < 4)).TakeWhile(x => x <= 7);
            foreach (var x in qw)
                Console.WriteLine(x);
            Console.WriteLine("Декартово произведение");
            var q6 = from x in d1
                     from y in d2
                     select new { v1 = x.Product_priсe, v2 = y.Product_priсe };
            foreach (var x in q6)
                Console.WriteLine(x);
            Console.WriteLine("Inner Join с использованием Where");
            var q7 = from x in d1
                     from y in d2
                     where Convert.ToBoolean((x.Product_Id + y.Product_Id) % 2)
                     select new { v1 = x.Product_priсe, v2 = y.Product_priсe };
            foreach (var x in q7)
                Console.WriteLine(x);
            Console.WriteLine("Cross Join (Inner Join) с использованием Join");
            var q8 = from x in d1
                     join y in d2 on (x.Product_priсe % 8) equals (y.Product_priсe % 8)
                     select new { v1 = x.Product_priсe, v2 = y.Product_priсe };
            foreach (var x in q8)
                Console.WriteLine(x);

            Console.WriteLine("Group Join");
            var q9 = from x in d1
                     join y in d2 on (x.Product_priсe % 6) equals (y.Product_priсe % 6) into temp
                     select new { v1 = x.Product_priсe, d2Group = temp };
            foreach (var x in q9)
            {
                Console.WriteLine(x.v1);
                foreach (var y in x.d2Group)
                    Console.WriteLine("   " + y);
            }

            Console.WriteLine("Cross Join и Group Join");
            var q10 = from x in d1
                      join y in d2 on (x.Product_priсe % 6) equals (y.Product_priсe % 6) into temp
                      from t in temp
                      select new { v1 = x.Product_priсe, v2 = t.Product_priсe, cnt = temp.Count() };
            foreach (var x in q10)
                Console.WriteLine(x);

            Console.WriteLine("Группировка - Any");
            var q11 = from x in d1.Union(d2)
                      group x by x.Product_producer into g
                      where g.Any(x => x.Product_Id > 3)
                      select new { Key = g.Key, Values = g };
            foreach (var x in q11)

                Console.WriteLine("Outer Join");
            var q12 = from x in d1
                      join y in d2 on (x.Product_priсe % 6) equals (y.Product_priсe % 6) into temp
                      from t in temp.DefaultIfEmpty()
                      select new { v1 = x.Product_priсe, v2 = ((t == null) ? "null" : t.Product_name) };
            foreach (var x in q12)
                Console.WriteLine(x);

            Console.WriteLine("Distinct - неповторяющиеся значения");
            var q13 = (from x in d1 select x.Product_producer).Distinct();
            foreach (var x in q13)
                Console.WriteLine(x);
            Console.WriteLine("Distinct - повторяющиеся значения для объектов");
            var q14 = (from x in d1 select x).Distinct();
            foreach (var x in q14)
                Console.WriteLine(x);

            var q15 = from x in d1.Union(d2) group x by x.Product_producer into g select new { Key = g.Key, Values = g };
            foreach (var x in q15)
            {
                Console.WriteLine(x.Key);
                foreach (var y in x.Values)
                    Console.WriteLine("   " + y);
            }
        }
    }
}
