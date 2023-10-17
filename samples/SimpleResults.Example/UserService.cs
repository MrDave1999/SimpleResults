namespace SimpleResults.Example;

public class UserService
{
    private readonly List<User> _users;

    public UserService(List<User> users)
    {
        ArgumentNullException.ThrowIfNull(nameof(users));
        _users = users;
    }

    public PagedResult<User> GetPagedList(int pageNumber, int pageSize)
    {
        if(pageNumber <= 0)
        {
            return Result.Invalid("PageNumber must be greater than zero");
        }

        int itemsToSkip = (pageNumber - 1) * pageSize;
        var data = _users
            .Skip(itemsToSkip)
            .Take(pageSize);

        if (data.Any())
        {
            var pagedInfo = new PagedInfo(pageNumber, pageSize, _users.Count);
            return Result.Success(data, pagedInfo);
        }

        return Result.Failure("No results found");
    }

    public ListedResult<User> GetAll()
    {
        if(_users.Count == 0)
        {
            return Result.Failure("No user found");
        }

        return Result.ObtainedResources(_users);
    }

    public Result<User> GetById(string id)
    {
        if(string.IsNullOrWhiteSpace(id))
        {
            return Result.Invalid("ID is required");
        }

        var user = _users.Find(u => u.Id == id);
        if(user is null)
        {
            return Result.NotFound();
        }

        return Result.Success(user, "User found");
    }

    public Result<CreatedGuid> Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            return Result.Invalid("Name is required");
        }

        var guid = Guid.NewGuid();
        _users.Add(new User { Id = guid.ToString(), Name = name });
        return Result.CreatedResource(guid);
    }

    public Result Update(string id, string name)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Result.Invalid("ID is required");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Invalid("Name is required");
        }

        var user = _users.Find(u => u.Id == id);
        if (user is null)
        {
            return Result.NotFound();
        }

        user.Name = name;
        return Result.UpdatedResource();
    }

    public Result Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Result.Invalid("ID is required");
        }

        int index = _users.FindIndex(u => u.Id == id);
        if(index == -1)
        {
            return Result.NotFound();
        }

        _users.RemoveAt(index);
        return Result.DeletedResource();
    }
}
