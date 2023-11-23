namespace ProgramowanieZaawansowane;

public class Room : IIdentifiable
{
    private readonly long _id;
    private readonly long _ownerId;
    private readonly string _name;
    private readonly string? _description;
    private readonly IList<Message> _messages;
    private short _currentUsersCount;
    private readonly short _maxUsersCount;


    public long OwnerId => _ownerId;

    public string Name => _name;

    public string? Description => _description;

    public IList<Message> Messages => _messages;

    public short MaxUsersCount => _maxUsersCount;

    private static short ValidateMaxUsersCount(short maxUsersCount)
    {
        if (maxUsersCount <= 0)
        {
            throw new ArgumentException(
                $"Cannot create Room with maxUserCount = {maxUsersCount}, maxUsersCount must be <= 0");
        }

        return maxUsersCount;
    }

    public Room(long id, long ownerId, string name, string? description = null, short currentUsersCount = 0,
        short maxUsersCount = 8)
    {
        _id = id;
        _ownerId = ownerId;
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _messages = new List<Message>();
        _description = description;
        _currentUsersCount = currentUsersCount;
        _maxUsersCount = ValidateMaxUsersCount(maxUsersCount);
    }

    public bool CanJoin()
    {
        return _currentUsersCount < _maxUsersCount;
    }

    public void Join()
    {
        if (!CanJoin())
        {
            throw new ArgumentException(
                $"Cannot join, room is full {_currentUsersCount}/{_maxUsersCount} (curr/max)");
        }

        _currentUsersCount += 1;
    }

    public void SendMessage(Message message)
    {
        _messages.Add(message);
    }

    long IIdentifiable.Id()
    {
        return _id;
    }
}
