using System.Net;
using System.Net.Sockets;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using Server;

namespace ProgramowanieZaawansowane
{
    public class WsServer
    {
        private readonly IPAddress _address;
        private readonly int _port;
        private readonly WebSocketServer _server;

        public WsServer() : this(IPAddress.Loopback, 8080)
        {
        }

        public WsServer(IPAddress address, int port)
        {
            _address = address ?? throw new ArgumentNullException(nameof(address)); ;
            _port = port;
            _server = new WebSocketServer($"ws://{_address}:{_port}");
        }

        private void InitServices()
        {
            _server.AddWebSocketService<CreateChat>("/CreateChat");
            _server.AddWebSocketService<Echo>("/Echo");
            _server.AddWebSocketService<Registration>("/Register");
            _server.AddWebSocketService<Login>("/Login");
        }

        public void Start()
        {
            InitServices();
            _server.Start();
            Console.WriteLine($"WebSocket Server Started on address {_address}:{_port}!");
        }

        public void Stop()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _server.Stop();
            }
            Console.WriteLine($"WebSocket Server stopped!");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }


    //Test connectivity
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            WebSocketLogger.Log("Echo from websocket server");
            Send(e.Data);
            //base.OnMessage(e);
        }
    }


    //Login user
    public class Login : WebSocketBehavior
    {

        public struct LoginDTO
        {
            public string userName { get; }
            public string password { get; }

        }

        protected override void OnMessage(MessageEventArgs message)
        {

            LoginDTO dto = Parser.DeserializeJson<LoginDTO>(message);

            if (dto.userName.IsNullOrEmpty())
            {
                Send("Invalid username!");
                return;
            }
            if (dto.userName.IsNullOrEmpty())
            {
                Send("Invalid password!");
                return;
            }

            AuthenticateUser(dto.userName, dto.password);

        }

        private bool AuthenticateUser(string userName, string password)
        {
            if (!UsersRepository.usersWithPasswords.ContainsKey(userName))
            {
                Send("There's no such username in our db!");
                return false;
            }
            if (UsersRepository.usersWithPasswords[userName] != password)
            {
                Send("Invalid password");
                return false;
            }

            return true;
        }
    }

    //Register user
    public class Registration : WebSocketBehavior
    {
        public struct RegistrationDTO
        {
            public string? userName { get; }
            public string? password { get; }
        }

        protected override void OnMessage(MessageEventArgs message)
        {
            RegistrationDTO dto = Parser.DeserializeJson<RegistrationDTO>(message);
            if (dto.userName.IsNullOrEmpty())
            {
                Send("Invalid username!");
                return;
            }

            if (dto.userName.IsNullOrEmpty())
            {
                Send("Invalid password!");
                return;
            }

            UsersRepository.usersWithPasswords.Add(dto.userName, dto.password);
            Send("User created successfully");
            WebSocketLogger.Log($"New user was added: {dto.userName} with password: ${dto.password}");
        }
    }



    public class CreateChat : WebSocketBehavior
    {
        public struct CreateChatDTO
        {
            public string? roomName { get; }
            public string? password { get; }
        }

        protected override void OnMessage(MessageEventArgs message)
        {
            CreateChatDTO dto = Parser.DeserializeJson<CreateChatDTO>(message);
            if (dto.roomName.IsNullOrEmpty())
            {
                Send("Invalid room name!");
                return;
            }

            if (dto.password.IsNullOrEmpty())
            {
                Send("Invalid password!");
                return;
            }

            RoomsRepository.roomsWithPasswords.Add(dto.roomName, dto.password);
            Send("Room created successfully");
            WebSocketLogger.Log($"New room was created: {dto.roomName} with password: ${dto.password}");
        }
    }

}
