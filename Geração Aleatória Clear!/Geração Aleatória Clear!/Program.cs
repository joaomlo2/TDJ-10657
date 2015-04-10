using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geração_Aleatória_Clear_
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] level = new int[5, 5];
            List<int> RandomNumbers=new List<int>();
            for (int i=0;i<5;i++)
            {
                for(int j=0;j<5;j++)
                {
                    int num;
                    do num=new Random().Next(0,41);
                    while(RandomNumbers.Contains(num));

                    RandomNumbers.Add(num);
                    level[i, j] = num;
                    Console.Write(num+" ");
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(level[i,j]+" ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
