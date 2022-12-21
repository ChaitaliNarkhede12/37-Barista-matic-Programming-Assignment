using System;
using System.Collections.Generic;
using System.Text;

namespace BaristaMaticCoffeeMachine
{
    public class Ingredient
    {
        public string Name = "";
        public double Cost = 0.00;
        public int Stock = 0;

        public Ingredient() { }
        public Ingredient(string Name, double Cost)
        {
            this.Name = Name;
            this.Cost = Cost;
            this.Stock = 10;
        }

        public int GetStock()
        {
            return Stock;
        }

        public void SetStock(int stock)
        {
            this.Stock = stock;
        }

        public string GetName()
        {
            return Name;
        }

        public double GetCost()
        {
            return Cost;
        }

    }
}
