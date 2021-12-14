using QuinaNadalServer.Models;
using System.Collections.Generic;

namespace QuinaNadalServer.Services
{
    public class TaulellService : ITaulellService
    {
        const int MAX = 100;
        bool[] _taulell = new bool[MAX];
        private static readonly object objlock = new object();

        public Taulell GetTaulell()
        {
            List<int> marcats = new List<int>();
            lock (objlock)
            {
                for (int i = 0; i < MAX; i++)
                    if (_taulell[i])
                        marcats.Add(i);
            }
            var taulell = new Taulell();
            taulell.Marcats = marcats;
            return taulell;
        }

        public void SetTaulell(Taulell taulell)
        {
            lock (objlock)
            {
                for (int i = 0; i < MAX; i++)
                    _taulell[i] = false;

                foreach (int marcat in taulell.Marcats)
                    _taulell[marcat] = true;
            }
        }
    }
}