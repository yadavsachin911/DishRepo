using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishesApplication
{
    public abstract class DishType
    {
        protected DishProvider dishProvider;
        protected DishType(DishProvider provider)
        {
            this.dishProvider = provider;
        }
        public abstract void GetEntreeDish();
        public abstract void GetSideDish();
        public abstract void GetDrink();
        public abstract void GetDessert();
        public abstract bool Validate(int count);
    }
    public class NightDish : DishType
    {
        private string _dish = string.Empty;
        public NightDish(DishProvider provider)
            : base(provider)
        {

        }

        public override void GetEntreeDish()
        {
            _dish = "steak";
            dishProvider.SetDishTypeOutput(_dish);
        }

        public override void GetSideDish()
        {
            _dish = "potato";
            dishProvider.SetDishTypeOutput(_dish);
        }

        public override void GetDrink()
        {
            _dish = "wine";
            dishProvider.SetDishTypeOutput(_dish);
        }

        public override void GetDessert()
        {
            _dish = "cake";
            dishProvider.SetDishTypeOutput(_dish);
        }

        public override bool Validate(int count)
        {
            if (_dish != "potato" && count > 1)
                return false;
            else
                return true;
        }
    }


    public class MorningDish : DishType
    {
        private string _dish = string.Empty;
        public MorningDish(DishProvider provider)
            : base(provider)
        {

        }

        public override void GetEntreeDish()
        {
            _dish = "eggs";
            dishProvider.SetDishTypeOutput(_dish);
        }

        public override void GetSideDish()
        {
            _dish = "toast";
            dishProvider.SetDishTypeOutput(_dish);
        }

        public override void GetDrink()
        {
            _dish = "coffee";
            dishProvider.SetDishTypeOutput(_dish);
        }

        public override void GetDessert()
        {
            throw new Exception("Invalid Dish");
            //_dish = "error";
            //dishProvider.SetDishTypeOutput(_dish);
        }

        public override bool Validate(int count)
        {
            if (_dish != "coffee" && count > 1)
                return false;
            else
                return true;
        }
    }
}
