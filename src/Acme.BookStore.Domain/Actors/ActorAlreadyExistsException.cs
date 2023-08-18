using Volo.Abp;

namespace Acme.BookStore.Actors;

public class ActorAlreadyExistsException : BusinessException
{
    public ActorAlreadyExistsException(string name)
        : base(BookStoreDomainErrorCodes.ActorAlreadyExists)
    {
        WithData("name", name);
    }
}
