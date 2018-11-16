using Microsoft.Extensions.Configuration;
using System;

namespace ConfigSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                 .AddJsonFile("class.json");

            var configuration = builder.Build();
            
            //读取json文件
            Console.WriteLine($"ClassNo:{configuration["ClassNo"]}");
            Console.WriteLine($"ClassDesc:{configuration["ClassDesc"]}");

            Console.WriteLine(configuration["Students:0:name"]);
            Console.WriteLine(configuration["Students:0:age"]);
            Console.WriteLine(configuration["Students:1:name"]);
            Console.WriteLine(configuration["Students:1:age"]);

            Console.ReadKey();
        }
    }
}
