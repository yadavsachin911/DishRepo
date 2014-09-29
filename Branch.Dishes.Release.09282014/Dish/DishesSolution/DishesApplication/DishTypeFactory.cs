using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishesApplication
{

    public interface IDishTypeFactory
    {
        DishType SetDishTypeFor(DishProvider provider, string input);
    }

    public class DishTypeFactory : IDishTypeFactory
    {
        public DishTypeFactory()
        {
        }

        public DishType SetDishTypeFor(DishProvider provider, string input)
        {
            DishType dishType = null;
            if (input.ToLower().Trim() == "night")
            {
                dishType = new NightDish(provider);
            }
            else if (input.ToLower().Trim() == "morning")
            {
                dishType = new MorningDish(provider);
            }

            return dishType;
        }

    }

}
