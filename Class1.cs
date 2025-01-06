using System;
using System.Collections.Generic;
using Work;

namespace Work
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание менеджера меню
            var menuManager = new MenuManager();
            var spaghetti = new Dish { Name = "Спагетти", Price = 12.99 };
            var salad = new Dish { Name = "Салат", Price = 5.99 };
            var cola = new Drink { Name = "Кола", Price = 1.99 };
            var water = new Drink { Name = "Вода", Price = 0.99 };

            // Добавление в меню
            menuManager.AddDish(spaghetti);
            menuManager.AddDish(salad);
            menuManager.AddDrink(cola);
            menuManager.AddDrink(water);

            // Вывод меню после добавлений
            Console.WriteLine("Меню после добавлений:");
            foreach (var item in menuManager.GetMenu())
            {
                Console.WriteLine($"Товар: {item.Name}, Цена: {item.Price}");
            }

            // Удаление из меню
            menuManager.RemoveDish(salad);
            menuManager.RemoveDrink(water);

            // Вывод меню после удалений
            Console.WriteLine("\nМеню после удалений:");
            foreach (var item in menuManager.GetMenu())
            {
                Console.WriteLine($"Товар: {item.Name}, Цена: {item.Price}");
            }

            // Тест заказов
            var order = new Order { Status = "Ожидает" };
            Console.WriteLine($"\nСтатус заказа: {order.Status}");
            order.ChangeStatus("В процессе");
            Console.WriteLine($"Статус заказа после обновления: {order.Status}");

            // Тест программы лояльности
            var loyaltyProgram = new LoyaltyProgram();
            loyaltyProgram.AddPoints(100);
            Console.WriteLine($"\nБаланс лояльности: {loyaltyProgram.PointsBalance}");

            // Тест сотрудников
            var waiter = new Waiter { Name = "Алиса", Role = "Официант" };
            waiter.HandleTask("Обслужить клиента");
            var manager = new Manager { Name = "Боб", Role = "Менеджер" };
            manager.HandleTask("Контролировать кухню");
            var chef = new Chef { Name = "Чарли", Role = "Шеф-повар" };
            chef.HandleTask("Приготовить еду");

            // Тест системы оплаты
            var payment = new Card();
            payment.ProcessPayment(100.50);
            Console.WriteLine("\nПлатеж обработан с использованием карты.");

            // Тест инвентаря и поставщиков
            var supplier = new Supplier { Name = "Поставщик 1", ContactInfo = "Контакты" };
            supplier.Supply("Томаты", 50);
            Console.WriteLine("\nТовары поставлены. Количество: 50");

            // Тест отчётов
            var salesReport = new SalesReport();
            salesReport.GenerateReport();
            var orderReport = new OrderReport();
            orderReport.GenerateReport();
            Console.WriteLine("\nТесты завершены.");
        }
    }
    
    
}
