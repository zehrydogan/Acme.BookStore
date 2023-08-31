using Volo.Abp;

namespace Acme.BookStore.Directors;

public class DirectorAlreadyExistsException : BusinessException
{
    public DirectorAlreadyExistsException(string name)
        : base(BookStoreDomainErrorCodes.DirectorAlreadyExists)
    {
        WithData("name", name);
    }
}
