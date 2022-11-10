using QuinaNadalClient;
using QuinaNadalServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientTests
{
    class Program
    {
        const string ApiUrl = "http://localhost:44080";
        static void Main(string[] args)
        {
            Random rnd = new Random();
            var valorsMarcats = Enumerable.Range(1, 10).Select(_ => rnd.Next(90) + 1).Distinct().ToArray();
            MarcaValors(valorsMarcats);
            ComprovaTaulell(valorsMarcats);
        }

        private static void MarcaValors(int[] valors)
        {
            Taulell taulell = new Taulell();
            Console.WriteLine("Marca valors " + string.Join(",", valors.Select(x => x.ToString())));
            taulell.Marcats = valors.ToList();
            ApiClient api = new ApiClient(new Uri(ApiUrl));
            api.SetTaulell(taulell);
        }

        private static void ComprovaTaulell(int[] valors)
        {
            ApiClient api = new ApiClient(new Uri(ApiUrl));
            Taulell taulell = api.GetTaulell();

            foreach (var valor in valors)
            {
                bool trobat = taulell.Marcats.Contains(valor);
                Console.WriteLine($"Comprova valor {valor} : " + (trobat ? "OK":  "ERR"));
            }
        }
    }
}
