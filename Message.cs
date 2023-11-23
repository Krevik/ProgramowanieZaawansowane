namespace ProgramowanieZaawansowane;

public class Message : IIdentifiable
{
    private readonly long _id;
    private readonly long _roomId;
    private readonly User _sender;
    private readonly string _body;
    private readonly long _sentTime;


    public long RoomId => _roomId;

    public User Sender => _sender;

    public string Body => _body;

    public long SentTime => _sentTime;

    private static string ValidateBody(string? body)
    {
        if (body == null)
        {
            throw new ArgumentNullException(nameof(body));
        }

        if (body.Length == 0 || body.Trim().Length == 0)
        {
            throw new ArgumentException("Empty body!");
        }

        return body;
    }

    private static void ValidateSender(User sender)
    {
        if (!sender.IsActive())
        {
            throw new ArgumentException($"Cannot send message user {sender.Email} is disabled");
        }
    }

    public Message(long id, long roomId, User sender, string? body)
    {
        _id = id;
        _roomId = roomId;
        _sender = sender;
        _body = ValidateBody(body);
        _sentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    long IIdentifiable.Id()
    {
        return _id;
    }
}
