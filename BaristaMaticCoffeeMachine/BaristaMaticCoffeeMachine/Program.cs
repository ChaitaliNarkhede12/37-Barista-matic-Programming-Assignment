using System;
using System.Collections.Generic;
using System.Linq;

namespace BaristaMaticCoffeeMachine
{
    class Program
    {
        private static List<Drink> drinkList = new List<Drink>();
        private static List<Ingredient> ingredientList = new List<Ingredient>();

        static void Main(string[] args)
        {
            AddAllIngredients();
            AddAllDrinks();
            UpdateCosts();
            UpdateMakeable();
            Display();
            UserInput();
        }

        public static void UserInput()
        {
            string input = "";
            while (true)
            {
                try
                {

                    input = Console.ReadLine();

                    if (input.Equals(""))
                    {
                        continue;
                    }
                    else if (input.Equals("q"))
                    {
                        break;
                    }
                    else if (input.Equals("r"))
                    {
                        RestockIngredients();
                        UpdateMakeable();
                    }
                    else if (Convert.ToInt16(input) > 0 && Convert.ToInt16(input) <= drinkList.Count)
                    {
                        //dynamic drink menu selection
                        MakeDrink(drinkList[Convert.ToInt32(input) - 1]);
                    }
                    else
                    {
                        //Invalid Input
                        throw new Exception("Error");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid selection: " + input + "\n");
                }
            }//running loop     
        }




        public static void Display()
        {
            //Sort & Print Inventory List
            ingredientList = ingredientList.OrderBy(x => x.Name).ToList();
            Console.WriteLine("Inventory:\n");

            foreach (var item in ingredientList)
            {
                Console.WriteLine(item.GetName() + "," + item.GetStock() + "\n");
            }

            //Sort & Print Menu List
            drinkList = drinkList.OrderBy(x => x.Name).ToList();

            Console.WriteLine("Menu:\n");
            int count = 1;

            foreach (var item in drinkList)
            {
                Console.WriteLine(count + "," + item.GetName() + ", $" + item.GetCost() + "," + item.GetMakeable());
                count++;
            }
        }

        public static void UpdateMakeable()
        {
            //Drink loop
            foreach (var d in drinkList)
            {
                Dictionary<string, int> currRecipe = d.GetRecipe();
                //Ingredient loop
                foreach (var i in ingredientList)
                {
                    if (currRecipe.ContainsKey(i.GetName()) && i.GetStock() < currRecipe[i.GetName()])
                    {
                        d.SetMakeable(false);
                        break;//check next drink
                    }
                    d.SetMakeable(true);
                }
            }
        }

        public static void UpdateCosts()
        {
            //Drink Loop
            foreach (var d in drinkList)
            {
                double currCost = 0;
                Dictionary<string, int> currRecipe = d.GetRecipe();
                //Ingredient loop
                foreach (var i in ingredientList)
                {
                    if (currRecipe.ContainsKey(i.GetName()))
                    {
                        //Updating the cost
                        currCost += i.GetCost() * currRecipe[i.GetName()];
                    }
                }
                d.SetCost(currCost);
            }
        }

        public static void MakeDrink(Drink drink)
        {
            if (drink.GetMakeable())
            {
                Console.WriteLine("Dispensing: " + drink.GetName() + "\n");
                foreach (var i in ingredientList)
                {
                    if (drink.GetRecipe().ContainsKey(i.GetName()))
                    {
                        //updating the stock
                        i.SetStock(i.GetStock() - drink.GetRecipe()[i.GetName()]);
                    }
                }
            }
            else
            {
                Console.WriteLine("Out of stock: " + drink.GetName() + "\n");
            }
            UpdateMakeable();
            Display();
        }

        public static void RestockIngredients()
        {
            foreach (var i in ingredientList)
            {
                i.SetStock(10);
            }
            UpdateMakeable();
            Display();
        }


        //Master Data For Ingredients
        public static void AddAllIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient{Name="Coffee", Cost=0.75, Stock=10 },
                new Ingredient{Name="Decaf Coffee", Cost=0.75, Stock=10},
                new Ingredient{Name="Sugar", Cost=0.25, Stock=10},
                new Ingredient{Name="Cream", Cost=0.25, Stock=10},
                new Ingredient{Name="Steamed Milk", Cost=0.35, Stock=10},
                new Ingredient{Name="Foamed Milk", Cost=0.35, Stock=10},
                new Ingredient{Name="Espresso", Cost=1.10, Stock=10},
                new Ingredient{Name="Cocoa", Cost=0.90, Stock=10},
                new Ingredient{Name="Whipped Cream", Cost=1.00, Stock=10}
            };

            ingredients = ingredients.OrderBy(x => x.Name).ToList();
            ingredientList = ingredients;

        }


        //Add drinks through AddAllDrinks
        public static void AddDrink(string name, string[] recipe)
        {
            drinkList.Add(new Drink(name, recipe));
        }

        //Master Data Fro Drinks
        public static void AddAllDrinks()
        {
            AddDrink("Coffee", new string[] { "Coffee", "Coffee", "Coffee", "Sugar", "Cream" });
            AddDrink("Decaf Coffee", new string[] { "Decaf Coffee", "Decaf Coffee", "Decaf Coffee", "Sugar", "Cream" });
            AddDrink("Caffe Latte", new string[] { "Espresso", "Espresso", "Steamed Milk" });
            AddDrink("Caffe Americano", new string[] { "Espresso", "Espresso", "Espresso" });
            AddDrink("Caffe Mocha", new string[] { "Espresso", "Cocoa", "Steamed Milk", "Whipped Cream" });
            AddDrink("Cappuccino", new string[] { "Espresso", "Espresso", "Steamed Milk", "Foamed Milk" });
        }

    }
}
