using System;
using System.Collections.Generic;


// Декораторы для блюд и напитков
namespace Work
{
    
    // Singleton for MenuManager
    public class MenuManager
    {
        private static MenuManager _instance;
        private static readonly object _lock = new object();

        public List<Dish> Dishes { get; private set; } = new List<Dish>();
        public List<Drink> Drinks { get; private set; } = new List<Drink>();

        private MenuManager() { }

        public static MenuManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new MenuManager();
                }
            }
            return _instance;
        }

        public void AddDish(Dish dish) => Dishes.Add(dish);
        public void RemoveDish(Dish dish) => Dishes.Remove(dish);
        public void AddDrink(Drink drink) => Drinks.Add(drink);
        public void RemoveDrink(Drink drink) => Drinks.Remove(drink);

        public List<MenuItem> GetMenu()
        {
            List<MenuItem> menu = new List<MenuItem>();
            menu.AddRange(Dishes);
            menu.AddRange(Drinks);
            return menu;
        }
    }

    // Base class for menu items
    public abstract class MenuItem
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public abstract double GetCost();
    }

    // Dish and Drink classes
    public class Dish : MenuItem
    {
        public string Type { get; set; }

        public override double GetCost() => Price;
    }

    public class Drink : MenuItem
    {
        public string Type { get; set; }

        public override double GetCost() => Price;
    }

    // Decorator for Dish and Drink
    public class DishOptionDecorator : Dish
    {
        private readonly Dish _baseDish;
        private readonly string _option;
        private readonly double _extraCost;

        public DishOptionDecorator(Dish baseDish, string option, double extraCost)
        {
            _baseDish = baseDish;
            _option = option;
            _extraCost = extraCost;
            Name = $"{_baseDish.Name} + {_option}";
            Price = _baseDish.Price + _extraCost;
        }

        public override double GetCost() => _baseDish.GetCost() + _extraCost;

        public override string ToString() => $"{_baseDish.Name} + {_option} (${_extraCost})";
    }

    // Order class with Observer
    public class Order
    {
        public List<MenuItem> Items { get; private set; } = new List<MenuItem>(); // Универсальный тип
        public DateTime CreationTime { get; set; }
        public string Status { get; private set; } = "Ожидание";

        public void AddItem(MenuItem item) => Items.Add(item);
        public void ChangeStatus(string status) => Status = status;
    }

    public interface IOrderObserver
    {
        void UpdateStatus(string status);
    }

    public class Client
    {
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public List<Order> Orders { get; private set; } = new List<Order>();
        public Order CreateOrder()
        {
            var order = new Order();
            Orders.Add(order);
            Console.WriteLine($"Клиент {Name} создал новый заказ.");
            return order;
        }
        public Reservation CreateReservation(string type, DateTime time, int numberOfPeople)
        {
            var reservation = Reservation.CreateReservation(type);
            reservation.Time = time;
            reservation.NumberOfPeople = numberOfPeople;
            reservation.Client = this; // Связываем клиента с резервацией
            Console.WriteLine($"Клиент {Name} создал резервацию типа {type} на {numberOfPeople} человек.");
            return reservation;
        }

        public void ReceiveNotification(string message)
        {
            Console.WriteLine($"Клиент {Name} получил уведомление: {message}");
        }
    }
    public class OrderStatusObserver : IOrderObserver
    {
        private readonly Client _client;

        public OrderStatusObserver(Client client)
        {
            _client = client;
        }

        public void UpdateStatus(string status)
        {
            _client.ReceiveNotification($"Статус вашего заказа изменён на: {status}");
        }
    }

    // Factory Method for Reservation
    public abstract class Reservation
    {
        public DateTime Time { get; set; }
        public int NumberOfPeople { get; set; }
        public string ReservationType { get; set; }
        public Client Client { get; set; } // Связь с клиентом

        public static Reservation CreateReservation(string type)
        {
            return type switch
            {
                "Standard" => new StandardReservation { ReservationType = "Стандартная" },
                "VIP" => new VIPReservation { ReservationType = "VIP" },
                "Banquet" => new BanquetReservation { ReservationType = "Банкет" },
                _ => throw new ArgumentException("Неизвестный тип резервации")
            };
        }
    }

    public class StandardReservation : Reservation { }

    public class VIPReservation : Reservation { }

    public class BanquetReservation : Reservation { }

    // Employee and Chain of Responsibility
    public abstract class Employee
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public Employee Next { get; set; }


        public int Experience { get; set; }

        public void SetNext(Employee employee) => Next = employee;

        public abstract void HandleTask(string task);
    }

    public class Waiter : Employee
    {
        public override void HandleTask(string task)
        {
            if (task == "Принять заказ")
                Console.WriteLine($"Официант {Name} выполняет задачу: {task}");
            else
                Next?.HandleTask(task);
        }
    }

    public class Chef : Employee
    {
        public override void HandleTask(string task)
        {
            if (task == "Приготовить блюдо")
                Console.WriteLine($"Шеф-повар {Name} выполняет задачу: {task}");
            else
                Next?.HandleTask(task);
        }
    }

    public class Manager : Employee
    {
        public override void HandleTask(string task)
        {
            Console.WriteLine($"Менеджер {Name} выполняет задачу: {task}");
        }
    }

    // Inventory and Strategy
    public interface IReplenishStrategy
    {
        void Execute();
    }

    public class RegularReplenish : IReplenishStrategy
    {
        public void Execute() => Console.WriteLine("Выполнено регулярное пополнение запасов.");
    }

    public class AutoReplenish : IReplenishStrategy
    {
        public void Execute() => Console.WriteLine("Выполнено автоматическое пополнение запасов.");
    }

    public class Supplier
    {
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public void Supply(Inventory inventory, int quantity)
        {
            Console.WriteLine($"{Name} поставил {quantity} единиц {inventory.ItemName}.");
            inventory.Quantity += quantity;
        }
    }

    public class Inventory
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public bool CheckStock(int requiredQuantity)
        {
            if (Quantity >= requiredQuantity)
            {
                Console.WriteLine($"Достаточно {ItemName} на складе: {Quantity} единиц.");
                return true;
            }
            else
            {
                Console.WriteLine($"Недостаточно {ItemName} на складе. Доступно: {Quantity}, требуется: {requiredQuantity}.");
                return false;
            }
        }
        public void Replenish(IReplenishStrategy strategy)
        {
            strategy.Execute();
            Console.WriteLine($"{ItemName} теперь имеет {Quantity} единиц.");
        }
    }

    // Reports and Template Method
    public abstract class Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void GenerateReport()
        {
            PrepareData();
            AnalyzeData();
            OutputReport();
        }

        protected abstract void PrepareData();
        protected abstract void AnalyzeData();
        protected abstract void OutputReport();
    }

    // Расширенный отчет о продажах
    public class DetailedSalesReport : Report
    {
        private Statistics _statistics;

        public DetailedSalesReport(Statistics statistics, DateTime startDate, DateTime endDate)
        {
            _statistics = statistics;
            StartDate = startDate;
            EndDate = endDate;
        }

        protected override void PrepareData()
        {
            Console.WriteLine("Подготовка данных для детализированного отчета о продажах...");
        }

        protected override void AnalyzeData()
        {
            Console.WriteLine("Анализ данных...");
            var totalOrders = _statistics.CountOrders(StartDate, EndDate);
            var totalRevenue = _statistics.CalculateTotalRevenue(StartDate, EndDate);
            var statusCounts = _statistics.CountOrdersByStatus(StartDate, EndDate);

            Console.WriteLine($"**Период:** {StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}");
            Console.WriteLine($"- **Количество заказов:** {totalOrders}");
            Console.WriteLine($"- **Общий доход:** {totalRevenue:C}");
            Console.WriteLine();
            Console.WriteLine("**Детализированный анализ:**");
            foreach (var status in statusCounts)
            {
                Console.WriteLine($"  - {status.Key}: {status.Value}");
            }
        }

        protected override void OutputReport()
        {
            Console.WriteLine("Детализированный отчет о продажах завершен.\n");
        }
    }

    // Отчет о запасах
    public class InventoryReport : Report
    {
        private List<Inventory> _inventories;

        public InventoryReport(List<Inventory> inventories)
        {
            _inventories = inventories;
        }

        protected override void PrepareData()
        {
            Console.WriteLine("Подготовка данных о запасах...");
        }

        protected override void AnalyzeData()
        {
            Console.WriteLine("Анализ данных о запасах...");
            foreach (var inventory in _inventories)
            {
                var status = inventory.CheckStock(30) ? "Достаточно" : "Недостаточно";
                Console.WriteLine($"- **Позиция:** {inventory.ItemName}");
                Console.WriteLine($"  - Доступно: {inventory.Quantity}");
                Console.WriteLine($"  - Состояние: {status}");
            }
        }

        protected override void OutputReport()
        {
            Console.WriteLine("Отчет о запасах сгенерирован.\n");
        }
    }

    // Статистика для анализа данных
    public class Statistics
    {
        private List<Order> _orders;

        public Statistics(List<Order> orders)
        {
            _orders = orders;
        }

        public int CountOrders(DateTime startDate, DateTime endDate)
        {
            return _orders.Count(order => order.CreationTime >= startDate && order.CreationTime <= endDate);
        }

        public double CalculateTotalRevenue(DateTime startDate, DateTime endDate)
        {
            return _orders
                .Where(order => order.CreationTime >= startDate && order.CreationTime <= endDate)
                .Sum(order => order.Items.Sum(item => item.GetCost()));
        }

        public Dictionary<string, int> CountOrdersByStatus(DateTime startDate, DateTime endDate)
        {
            return _orders
                .Where(order => order.CreationTime >= startDate && order.CreationTime <= endDate)
                .GroupBy(order => order.Status)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }

    public interface PaymentMethod
    {
        void ProcessPayment(double amount);
    }

    public class Cash : PaymentMethod
    {
        public void ProcessPayment(double amount) => Console.WriteLine($"Оплачено {amount} с использованием наличных.");
    }

    public class Card : PaymentMethod
    {
        public void ProcessPayment(double amount) => Console.WriteLine($"Оплачено {amount} с использованием карты.");
    }

    public class OnlinePayment : PaymentMethod
    {
        public void ProcessPayment(double amount) => Console.WriteLine($"Оплачено {amount} с использованием онлайн-оплаты.");
    }

    public class PaymentSystem
    {
        public PaymentMethod PaymentMethod { get; set; }

        public void Pay(double amount) => PaymentMethod.ProcessPayment(amount);

        public void Refund(double amount)
        {
            Console.WriteLine($"Возврат {amount} через {PaymentMethod.GetType().Name}.");
        }
    }

    // Loyalty Program and Composite
    public interface ILoyaltyComponent
    {
        int GetPoints();
        void Add(ILoyaltyComponent component);
        void Remove(ILoyaltyComponent component);
    }

    public class BasicLoyalty : ILoyaltyComponent
    {
        public int GetPoints() => 10;
        public void Add(ILoyaltyComponent component) { }
        public void Remove(ILoyaltyComponent component) { }
    }

    public class PremiumLoyalty : ILoyaltyComponent
    {
        public int GetPoints() => 50;
        public void Add(ILoyaltyComponent component) { }
        public void Remove(ILoyaltyComponent component) { }
    }

    public class LoyaltyProgram
    {
        public Client Client { get; set; }
        public int PointsBalance { get; private set; }

        public void AddPoints(int points)
        {
            PointsBalance += points;
            Console.WriteLine($"{Client.Name} заработал {points} очков. Всего: {PointsBalance} очков.");
        }

        public void RedeemPoints(int points)
        {
            if (PointsBalance >= points)
            {
                PointsBalance -= points;
                Console.WriteLine($"{Client.Name} использовал {points} очков. Осталось: {PointsBalance} очков.");
            }
            else
            {
                Console.WriteLine($"{Client.Name} недостаточно очков для использования.");
            }
        }
        public interface ICommand
        {
            void Execute();
        }

        // Команда для принятия заказа официантом
        public class TakeOrderCommand : ICommand
        {
            private readonly Waiter _waiter;
            private readonly Order _order;

            public TakeOrderCommand(Waiter waiter, Order order)
            {
                _waiter = waiter;
                _order = order;
            }

            public void Execute()
            {
                Console.WriteLine($"Официант {_waiter.Name} принимает заказ.");
                _waiter.HandleTask("Принять заказ");
            }
        }

        // Команда для приготовления заказа шефом
        public class PrepareOrderCommand : ICommand
        {
            private readonly Chef _chef;
            private readonly Order _order;

            public PrepareOrderCommand(Chef chef, Order order)
            {
                _chef = chef;
                _order = order;
            }

            public void Execute()
            {
                Console.WriteLine($"Шеф-повар {_chef.Name} готовит заказ.");
                _chef.HandleTask("Приготовить блюдо");
            }
        }

        // Invoker для управления выполнением команд
        public class OrderInvoker
        {
            private readonly List<ICommand> _commands = new List<ICommand>();

            public void AddCommand(ICommand command) => _commands.Add(command);

            public void ExecuteCommands()
            {
                foreach (var command in _commands)
                    command.Execute();
                _commands.Clear();
            }
        }
    }

}
    
