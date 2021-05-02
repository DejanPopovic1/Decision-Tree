using System;

namespace myNameSpace
{
    class Program
    {
        private void method()
        {
            Console.WriteLine("Hello World!");
        }

        static void Main(string[] args)
        {
            method();//<-- Compile Time error because an instantiation of the Program class doesnt exist
            Program p = new Program();
            p.method();//Now it works (You could also make method() static to get it to work)
        }
    }
}
