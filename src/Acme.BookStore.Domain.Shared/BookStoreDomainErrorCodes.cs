using Nito.AsyncEx;

namespace Acme.BookStore;

public static class BookStoreDomainErrorCodes
{
    public const string AuthorAlreadyExists = "BookStore:00001";
    public const string ActorAlreadyExists = "BookStore:00002";
    public const string DirectorAlreadyExists = "BookStore:00002";

}
