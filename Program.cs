using System;
using System.Collections.Generic;
using Work;

// Декораторы для блюд и напитков
namespace Work
{
    // Menu Management
    public class MenuManager
    {
        public List<Dish> Dishes { get; set; } = new List<Dish>();
        public List<Drink> Drinks { get; set; } = new List<Drink>();

        // Add a dish to the menu
        public void AddDish(Dish dish)
        {
            Dishes.Add(dish);
        }

        // Add a drink to the menu
        public void AddDrink(Drink drink)
        {
            Drinks.Add(drink);
        }

        // Remove a dish from the menu
        public void RemoveDish(Dish dish)
        {
            if (Dishes.Contains(dish))
            {
                Dishes.Remove(dish);}
            else
            {
                Console.WriteLine($"Dish '{dish.Name}' is not on the menu.");
            }
        }

        // Remove a drink from the menu
        public void RemoveDrink(Drink drink)
        {
            if (Drinks.Contains(drink))
            {
                Drinks.Remove(drink);}
            else
            {
                Console.WriteLine($"Drink '{drink.Name}' is not on the menu.");
            }
        }

        // Get the menu with all dishes and drinks
        public List<MenuItem> GetMenu()
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            menuItems.AddRange(Dishes);  // Assuming Dish is derived from MenuItem
            menuItems.AddRange(Drinks);  // Assuming Drink is derived from MenuItem
            return menuItems;
        }

        // Get a single instance of MenuManager
        public static MenuManager GetInstance()
        {
            return new MenuManager();
        }
    }

    // Base class for menu items (Dish, Drink)
    public abstract class MenuItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public abstract double GetCost();
    }

    // Dish class
    public class Dish : MenuItem
    {
        public string Type { get; set; }

        public override double GetCost() => Price;

        public void AddOption(DishOptionDecorator option)
        {
            // Implementation for adding an option (not fully implemented here)
        }
    }

    // Drink class
    public class Drink : MenuItem
    {
        public string Type { get; set; }

        public override double GetCost() => Price;

        public void AddOption(DrinkOptionDecorator option)
        {
            // Implementation for adding an option (not fully implemented here)
        }
    }

    // Decorators for Dishes and Drinks (no changes)
    public class DishOptionDecorator
    {
        public string Option { get; set; }
        public float GetCost() => 0f;
    }

    public class DrinkOptionDecorator
    {
        public string Option { get; set; }
        public float GetCost() => 0f;
    }

    // Dish and Drink classes
    
    // Reservation classes
    public abstract class Reservation
    {
        public DateTime Time { get; set; }
        public int NumberOfPeople { get; set; }
        public string ReservationType { get; set; }

        public static Reservation CreateReservation() => null;
    }

    public class StandardReservation : Reservation { }
    public class VIPReservation : Reservation { }
    public class BanquetReservation : Reservation { }

    // Loyalty System
    public interface ILoyaltyComponent
    {
        void Add(ILoyaltyComponent component);
        void Remove(ILoyaltyComponent component);
        int GetPoints();
    }

    public class BasicLoyalty : ILoyaltyComponent
    {
        public void Add(ILoyaltyComponent component) { }
        public void Remove(ILoyaltyComponent component) { }
        public int GetPoints() => 0;
    }

    public class PremiumLoyalty : ILoyaltyComponent
    {
        public void Add(ILoyaltyComponent component) { }
        public void Remove(ILoyaltyComponent component) { }
        public int GetPoints() => 0;
    }

    public class LoyaltyProgram
    {
        public Client Client { get; set; }
        public int PointsBalance { get; set; }

        public void AddPoints(int points) { }
        public void RedeemPoints(int points) { }
    }


    // Client and Orders
    public class Client
    {
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public void CreateOrder(Order order) { }
        public void ReceiveNotification(string message) { }
    }

    public class Order
    {
        public List<Dish> Items { get; set; } = new List<Dish>();
        public DateTime CreationTime { get; set; }
        public string Status { get; set; }

        public void AddDish(Dish dish) { }
        public void ChangeStatus(string status) { }
        public void NotifyClient() { }
    }

    public interface IOrderObserver
    {
        void UpdateStatus();
    }

    // Employee Management
    public abstract class Employee
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public int Experience { get; set; }
        public Employee Next { get; set; }

        public abstract void HandleTask(string task);
        public void SetNext(Employee employee) { }
    }

    public class Waiter : Employee
    {
        public override void HandleTask(string task) { }
    }

    public class Manager : Employee
    {
        public override void HandleTask(string task) { }
    }

    public class Chef : Employee
    {
        public override void HandleTask(string task) { }
    }

    // Payment System
    public class PaymentSystem
    {
        public string PaymentMethod { get; set; }
        public double Amount { get; set; }

        public void Pay() { }
        public void Refund() { }
    }
    public interface PaymentMethod
    {
        void ProcessPayment(double amount);
    }

    public class Cash : PaymentMethod
    {
        public void ProcessPayment(double amount) { }
    }

    public class Card : PaymentMethod
    {
        public void ProcessPayment(double amount) { }
    }

    public class OnlinePayment : PaymentMethod
    {
        public void ProcessPayment(double amount) { }
    }

   

    // Inventory and Supplier
    public class Supplier
    {
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public void Supply(string item, int quantity) { }
    }

    public class Inventory
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        public void CheckStock() { }
        public void ReplenishStock(IReplenishStrategy strategy) { }
    }

    public interface IReplenishStrategy
    {
        void Execute();
    }

    public class RegularReplenish : IReplenishStrategy
    {
        public void Execute() { }
    }

    public class AutoReplenish : IReplenishStrategy
    {
        public void Execute() { }
    }

    // Reports
    public abstract class Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReportType { get; set; }

        public abstract void GenerateReport();
    }

    public class SalesReport : Report
    {
        public override void GenerateReport() { }
    }

    public class OrderReport : Report
    {
        public override void GenerateReport() { }
    }


}
