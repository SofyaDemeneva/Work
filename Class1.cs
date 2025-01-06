using System;
using System.Collections.Generic;
using Work;

namespace Work
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuManager menuManager = MenuManager.GetInstance();

            // Creating sample dishes and drinks
            Dish pizza = new Dish { Name = "Pizza", Type = "Main", Price = 12.5 };
            Drink coke = new Drink { Name = "Coke", Type = "Beverage", Price = 2.5 };

            // Adding items to the menu
            menuManager.AddDish(pizza);
            menuManager.AddDrink(coke);

            // Displaying the menu
            Console.WriteLine("Menu after adding items:");
            foreach (var item in menuManager.GetMenu())
            {
                Console.WriteLine($"{item.Name} - {item.Price}");
            }

            // Removing a dish and a drink
            menuManager.RemoveDish(pizza);
            menuManager.RemoveDrink(coke);

            // Displaying the menu after removal
            Console.WriteLine("\nMenu after removing items:");
            foreach (var item in menuManager.GetMenu())
            {
                Console.WriteLine($"{item.Name} - {item.Price}");
            }
        }
    }
}