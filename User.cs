namespace ProgramowanieZaawansowane;

public class User : IIdentifiable
{
    private readonly long _id;
    private readonly string _username;
    private readonly ISet<Role> _roles;
    private string _email;
    private bool _active;


    public string Username => _username;

    public ISet<Role> Roles => _roles;

    public string Email
    {
        get => _email;
        set => _email = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool Active
    {
        get => _active;
        set => _active = value;
    }

    public User(long id, string username, ISet<Role> roles, string email, string passwordHash, bool active = true)
    {
        _id = id;
        _username = username ?? throw new ArgumentNullException(nameof(username));
        _roles = roles ?? throw new ArgumentNullException(nameof(roles));
        _email = email ?? throw new ArgumentNullException(nameof(email));
        _active = active;
    }

    public void Disable()
    {
        _active = false;
    }

    public void Enable()
    {
        _active = true;
    }

    public bool IsActive()
    {
        return _active;
    }

    public Role AddRole(Role newRole)
    {
        if (!_active)
        {
            throw new ArgumentException($"Cannot add Role {newRole} to inactive user {_username}");
        }

        _roles.Add(newRole);
        return newRole;
    }

    long IIdentifiable.Id()
    {
        return _id;
    }

}
