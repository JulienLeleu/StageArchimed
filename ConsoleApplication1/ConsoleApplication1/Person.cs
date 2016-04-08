using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public interface vertebra
    {
        Boolean haveLegs();
        Boolean haveArms();
        void walk();
    }

    public abstract class Human : vertebra
    {
        private int age;

        public Human()
        {
            this.age = 0;
        }

        public Human(int age)
        {
            this.age = age;
        }

        public abstract bool haveArms();
        public abstract bool haveLegs();

        override
        public string ToString()
        {
            return "Je suis un humain";
        }

        public abstract void walk();
    }

    public class Man : Human
    {

        public Man()
            : base()
        {

        }

        public Man(int age)
            : base(age)
        {

        }

        override
        public bool haveArms()
        {
            return true;
        }

        override
        public bool haveLegs()
        {
            return true;
        }

        public override void walk()
        {

        }

        public string sayHi(string name)
        {
            name = "Alexis";
            return "hi " + name;
        }

        override
        public string ToString()
        {
            return "Je suis un homme";
        }
    }
}
