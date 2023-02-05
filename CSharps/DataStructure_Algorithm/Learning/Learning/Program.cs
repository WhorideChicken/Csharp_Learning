using System;

namespace Learning
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i =0; i < 25; i ++)
            {
                for(int j = 0; j < 25; j++)
                    Console.Write('\u25cf');

                Console.WriteLine();
            }

        }
    }
}
