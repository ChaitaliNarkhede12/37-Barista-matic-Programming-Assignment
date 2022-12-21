using System;
using System.Collections.Generic;
using System.Text;

namespace BaristaMaticCoffeeMachine
{
    public class Drink
    {
        //Recipe list
        public Dictionary<string, int> Recipe = new Dictionary<string, int>();
        public string Name;

        public double TotalCost = 0;
        public bool Makeable = false;

        public Drink() { }
        public Drink(string Name, string[] Recipe)
        {
            this.Name = Name;
            SetRecipe(Recipe);
        }

        public void SetRecipe(string[] Recipe)
        {
            foreach (var s in Recipe)
            {
                if (this.Recipe.ContainsKey(s))
                {
                    //increment if multiple units
                    this.Recipe[s] = this.Recipe[s] + 1;
                }
                else
                {
                    //insert first occurrence of ingredient
                    this.Recipe.Add(s, 1);
                }
            }
        }

        public void SetCost(double totalCost)
        {
            this.TotalCost = totalCost;
        }

        public void SetMakeable(bool makeable)
        {
            this.Makeable = makeable;
        }

        public Dictionary<string, int> GetRecipe()
        {
            return Recipe;
        }

        public double GetCost()
        {
            return TotalCost;
        }

        public string GetName()
        {
            return Name;
        }

        public bool GetMakeable()
        {
            return Makeable;
        }

    }
}
