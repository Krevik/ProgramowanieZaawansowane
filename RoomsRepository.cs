using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramowanieZaawansowane
{
    static class RoomsRepository
    {
        //przechowuje loginy i hasła do pokojów
        public static Dictionary<string, string> roomsWithPasswords = new Dictionary<string, string>();


        static RoomsRepository()
        {

        }

    }
}
