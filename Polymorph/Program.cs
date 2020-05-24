using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Polymorph
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = ConvertToDataTable(GetData("foobar"));

            Console.WriteLine("D I S P L A Y    P O P U L A T E D    T A B L E");

            foreach (DataColumn column in dt.Columns)
            {
                Console.Write(column.ColumnName + "\t");
            }
            Console.WriteLine();
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    Console.Write(row[column.ColumnName] + "\t");
                }
                Console.WriteLine();
            }
            var table = ConvertToDataTable<IFoo>(GetData("foobar"));
            // Pause
            Console.ReadKey();
        }
        private static List<IFoo> GetData(string key)
        {
            return new List<IFoo> { new FooA(), new FooB() };
        }
        private static DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            Console.WriteLine("I N S I D E    G E N E R I C   M E T H O D");

            PropertyInfo[] properties = typeof(T).GetProperties();

            DataTable table = new DataTable();
            foreach (var prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType).DataType = prop.PropertyType;
            }
            foreach (var item in data)
            {
                Console.WriteLine(item.GetType().FullName);
                object[] values = properties.Select(property => property.GetValue(item)).ToArray();
                table.Rows.Add(values);
            }

            Console.WriteLine();
            return table;
        }
    }
    class FooA : IFoo
    {
        public int ID { get; } = 1;
        public string StringValue
        {
            get => Calc();
        }          

        string Calc()
        {
            return "I am FooA";
        }
    }
    class FooB : IFoo
    {
        public int ID { get; } = 2;
        public string StringValue
        {
            get => Calc();
        }

        string Calc()
        {
            return "I am FooB";
        }
    }
    internal interface IFoo
    {
        int ID { get; }
        string StringValue { get; } 
    }
}
