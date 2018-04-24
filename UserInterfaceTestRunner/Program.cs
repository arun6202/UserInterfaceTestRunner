using System;
using XamarinFormsStarterKit.UserInterfaceVisualizer;

namespace XamarinFormsStarterKit.UserInterfaceTestRunner
{
    class Program
    {
        static void Main(string[] args)
        {



            AppiumRunner.Execute();
            Console.WriteLine("Reached End!");
            Console.Read();
        }
    }
}
