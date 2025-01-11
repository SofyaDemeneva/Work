using System;
using System.Collections.Generic;
using Work;

namespace Work
{
    class Program
    {
        static void Main(string[] args)
        {
            // Тестирование MenuManager (Singleton)
            var menuManager = MenuManager.GetInstance();
            var dish = new Dish { Name = "Паста", Price = 10.0, Type = "Основное блюдо" };
            var drink = new Drink { Name = "Кофе", Price = 3.5, Type = "Напиток" };
            menuManager.AddDish(dish);
            menuManager.AddDrink(drink);
            Console.WriteLine("--- Меню ---");
            foreach (var item in menuManager.GetMenu())
            {
                Console.WriteLine($"{item.Name} - ${item.GetCost()}");
            }

            // Тестирование Decorator
            var decoratedDish = new DishOptionDecorator(dish, "Сыр", 2.0);
            Console.WriteLine($"Декорированное блюдо: {decoratedDish.Name} - ${decoratedDish.GetCost()}");

            // Тестирование Chain of Responsibility
            var waiter = new Waiter { Name = "Иван" };
            var chef = new Chef { Name = "Алексей" };
            var manager = new Manager { Name = "Мария" };
            waiter.SetNext(chef);
            chef.SetNext(manager);

            waiter.HandleTask("Принять заказ");
            waiter.HandleTask("Приготовить блюдо");
            waiter.HandleTask("Управление заведением");

            // Тестирование Command
            var order = new Order();
            order.AddItem(dish);
            var orderInvoker = new OrderInvoker();
            orderInvoker.AddCommand(new TakeOrderCommand(waiter, order));
            orderInvoker.AddCommand(new PrepareOrderCommand(chef, order));
            orderInvoker.ExecuteCommands();

            // Тестирование Observer
            var client = new Client { Name = "Ольга", ContactInfo = "olga@example.com" };
            var orderObserver = new OrderStatusObserver(client);
            order.ChangeStatus("Готовится");
            orderObserver.UpdateStatus(order.Status);

            // Тестирование Reservation (Factory Method)
            var reservation = Reservation.CreateReservation("VIP") as VIPReservation;
            reservation.Time = DateTime.Now;
            reservation.NumberOfPeople = 4;
            reservation.Client = client;
            Console.WriteLine($"Резервация: {reservation.ReservationType} для {reservation.NumberOfPeople} человек");

            // Тестирование Inventory и Strategy
            var inventory = new Inventory { ItemName = "Томаты", Quantity = 5 };
            inventory.CheckStock(10);
            var supplier = new Supplier { Name = "ООО Поставщик", ContactInfo = "supply@example.com" };
            supplier.Supply(inventory, 20);
            inventory.Replenish(new RegularReplenish());

            // Тестирование Payment
            var paymentSystem = new PaymentSystem();
            paymentSystem.PaymentMethod = new Cash();
            paymentSystem.Pay(15.0);
            paymentSystem.PaymentMethod = new Card();
            paymentSystem.Pay(20.0);

            // Тестирование Loyalty Program
            var loyaltyProgram = new LoyaltyProgram { Client = client };
            loyaltyProgram.AddPoints(30);
            loyaltyProgram.RedeemPoints(20);
            loyaltyProgram.RedeemPoints(15);

            // Тестирование Template Method (Reports)
            var salesReport = new SalesReport();
            salesReport.GenerateReport();
            var orderReport = new OrderReport();
            orderReport.GenerateReport();
        }
    }
}
