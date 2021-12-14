using QuinaNadalClient;
using QuinaNadalServer.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace ClientTests
{
    class Program
    {
        const string ApiUrl = "http://localhost:44080";
        static void Main(string[] args)
        {
            Taulell taulell = new Taulell();
            taulell.Marcats = new List<int> { 1, 10, 20 };
            ApiClient api = new ApiClient(new Uri(ApiUrl));
            api.SetTaulell(taulell);
            taulell = api.GetTaulell();

            Console.WriteLine(taulell.Marcats.Contains(1));
            Console.WriteLine(taulell.Marcats.Contains(10));
            Console.WriteLine(taulell.Marcats.Contains(20));
        }
    }
}
