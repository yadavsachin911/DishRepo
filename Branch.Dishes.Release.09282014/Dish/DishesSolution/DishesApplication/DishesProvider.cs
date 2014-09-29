using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishesApplication
{
        public class DishProvider
        {
            private string oldDish = string.Empty;
            StringBuilder sb = new StringBuilder();
            int count = 1;
            int remainingitem = 0;
            public DishType DishTypeProvider { get; private set; }
            public DishProvider(string dishType, IDishTypeFactory dishFactory)
            {
                DishTypeProvider = dishFactory.SetDishTypeFor(this, dishType);
            }

            public void SetDishTypeOutput(string newDish)
            {
                if (oldDish == newDish)
                {
                    count++;
                    if (!DishTypeProvider.Validate(count))
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    if (count > 1)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(string.Format("(*{0}),", count));
                    }
                    sb.Append(newDish + ",");
                    count = 1;
                }

                if (count > 1 && remainingitem == 1)
                {
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(string.Format("(*{0}),", count));
                }
                oldDish = newDish;
            }

            public void GetAvailableDish(List<string> inputs)
            {
                remainingitem = inputs.Count;
                try
                {
                    foreach (string input in inputs)
                    {
                        switch (input)
                        {
                            case "1":
                                DishTypeProvider.GetEntreeDish();
                                break;
                            case "2":
                                DishTypeProvider.GetSideDish();
                                break;
                            case "3":
                                DishTypeProvider.GetDrink();
                                break;
                            case "4":
                                DishTypeProvider.GetDessert();
                                break;
                            default:
                                throw new Exception("Invalid Input");
                        }
                        remainingitem--;
                    }
                }
                catch(Exception ex)
                {
                    sb.Append("error");
                    throw ex;
                }
            }

            public string Report()
            {
                if (sb.ToString().LastIndexOf(',') == sb.ToString().Length - 1)
                {
                    sb.Remove(sb.ToString().LastIndexOf(','), 1);
                }
                return sb.ToString();
            }

        }

    }


