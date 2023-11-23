using WebSocketSharp;

namespace ProgramowanieZaawansowane
{
    static class UsersRepository
    {

        //przechowuje loginy i hasła użytkownika w postaci Dictionary
        public static Dictionary<string, string> usersWithPasswords = new();

        static UsersRepository()
        {
            usersWithPasswords.Add("bg", "123");
            usersWithPasswords.Add("kw", "123");
            usersWithPasswords.Add("jk", "123");
        }

    }
}
