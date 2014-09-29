using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishesApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DishProvider provider = null;
            try
            {
                Console.WriteLine("Input : ");
                string inputs = Console.ReadLine();
                var array = inputs.Split(new char[] { ',' }).OrderBy(x => x);
                List<string> list = array.ToList();
                string dishType = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                IDishTypeFactory objFactory = new DishTypeFactory();
                provider = new DishProvider(dishType, objFactory);
                provider.GetAvailableDish(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                string output = provider.Report();
                Console.WriteLine("Output : " + output);
                Console.ReadLine();
            }

        }
    }
}
