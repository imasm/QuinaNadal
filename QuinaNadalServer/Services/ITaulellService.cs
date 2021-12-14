using QuinaNadalServer.Models;
using System.Collections.Generic;

namespace QuinaNadalServer.Services
{
    public interface ITaulellService
    {
        Taulell GetTaulell();
        void SetTaulell(Taulell taulell);
    }
}