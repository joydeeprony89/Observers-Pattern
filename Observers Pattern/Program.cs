using System;
using System.Collections.Generic;

namespace Observers_Pattern
{

    public abstract class Vegetable
    {
        int price;
        readonly List<IResturant> observers = new List<IResturant>();
        protected Vegetable(int price)
        {
            this.price = price;
        }

        public void Attach(IResturant resturant)
        {
            Console.WriteLine($"{resturant.Name} is your new subscriber");
            observers.Add(resturant);
        }

        public void Detatch(IResturant resturant)
        {
            Console.WriteLine($"{resturant.Name} no more is a subscriber");
            observers.Remove(resturant);
        }

        public int Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    Notify();
                }
            }
        }

        private void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }
    }

    public class Carrot : Vegetable
    {
        public Carrot(int price) : base(price)
        {

        }

    }

    public interface IResturant
    {
        string Name { get; }
        void Update(Vegetable vegetable);
    }

    public class Resturant : IResturant
    {
        readonly string name;
        readonly int expectedBuyingPrice;
        public string Name
        {
            get { return name; }
        }
        public Resturant(string name, int expectedBuyingPrice)
        {
            this.name = name;
            this.expectedBuyingPrice = expectedBuyingPrice;
        }
        public void Update(Vegetable vegetable)
        {
            Console.WriteLine($"Resturant {name} expected buying price is {expectedBuyingPrice}, Vegetable current price is {vegetable.Price}");
            if(expectedBuyingPrice == vegetable.Price)
                Console.WriteLine($"{name} would like to purchase at rate {vegetable.Price}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Resturant jWMarriot = new Resturant("JWMarriot", 5);
            Resturant taj = new Resturant("Taj", 4);
            Vegetable carrot = new Carrot(10);
            carrot.Attach(jWMarriot);
            carrot.Attach(taj);
            carrot.Price = 9;
            carrot.Price = 8;
            carrot.Price = 7;
            carrot.Price = 6;
            carrot.Price = 5;
            carrot.Detatch(jWMarriot);
            carrot.Price = 4;
            Console.ReadKey();
        }
    }
}
