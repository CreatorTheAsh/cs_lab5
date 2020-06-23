using System;
using System.Dynamic;

namespace cs_lab4
{
    public static class ProductHolder
    {
        public static int product_Id { get; private set; } = 0;
        static public void Increase()
        {
            product_Id++;
        }
    }
    public class Product
    {
        public int Product_Id;
        public string Product_name;
        public int Product_priсe;
        public int Product_quantity;
        public string Product_producer;
        public DateTime DataBecomeToStock = new DateTime();
        public Product()
        {
            Product_Id = ProductHolder.product_Id; ProductHolder.Increase();
            Product_name = "Milk";
            Product_priсe = 50;
            Product_producer = "Cow";
            Product_quantity = 2;
            DataBecomeToStock = DateTime.Now;
        }
        public Product(
            string Product_name,
            int Product_priсe,
            int Product_quantity,
            string Product_producer,
            DateTime DataBecomeToStock)
        {
            Product_Id = ProductHolder.product_Id; ProductHolder.Increase();
            this.Product_name = Product_name;
            this.Product_priсe = Product_priсe;
            this.Product_producer = Product_producer;
            this.Product_quantity = Product_quantity;
            this.DataBecomeToStock = DataBecomeToStock;
        }
        public Product(
            string Product_name,
            int Product_priсe,
            int Product_quantity,
            string Product_producer)
        {
            Product_Id = ProductHolder.product_Id; ProductHolder.Increase();
            this.Product_name = Product_name;
            this.Product_priсe = Product_priсe;
            this.Product_producer = Product_producer;
            this.Product_quantity = Product_quantity;
            this.DataBecomeToStock = DateTime.Now;
        }
        public void ProductInfo()
        {
            Console.WriteLine
                ("ProductInfo\nProduct_name:\t\t{0}\nProduct_priсe:\t\t{1}\nProduct_quantity:\t{2}\nProduct_producer:\t{3}\nDataBecomeToStock:\t{4}\n",
                  Product_name, Product_priсe, Product_quantity, Product_producer, DataBecomeToStock);
        }
        public void ProductOnelineInfo()
        {
            Console.WriteLine
                ("Product_name: {0}\tProduct_priсe: {1}\tProduct_quantity: {2}\tProduct_producer: {3}\tDataBecomeToStock: {4}\n",
                  Product_name, Product_priсe, Product_quantity, Product_producer, DataBecomeToStock);
        }
        public void ProductShortInfo()
        {
            Console.WriteLine
                ("Product_id: {0}\tProduct_name: {1}\tDataBecomeToStock: {2}\n",
                 Product_Id, Product_name, DataBecomeToStock);
        }
        public override string ToString()
        {
            return "(Product_Id=" + this.Product_Id.ToString() + "; Product_producer=" + this.Product_producer + "; Product_priсe=" + this.Product_priсe + ")";
        }
    }
}
