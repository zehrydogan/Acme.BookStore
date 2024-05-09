using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));

        var booksPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books.Delete"));
        booksPermission.AddChild(BookStorePermissions.Books.Comment, L("Permission:Books.Comment"));

        var moviesPermission = bookStoreGroup.AddPermission(BookStorePermissions.Movies.Default, L("Permission:Movies"));
        moviesPermission.AddChild(BookStorePermissions.Movies.Create, L("Permission:Movies.Create"));
        moviesPermission.AddChild(BookStorePermissions.Movies.Edit, L("Permission:Movies.Edit"));
        moviesPermission.AddChild(BookStorePermissions.Movies.Delete, L("Permission:Movies.Delete"));
        moviesPermission.AddChild(BookStorePermissions.Movies.Comment, L("Permission:Movies.Comment"));

        var authorsPermission = bookStoreGroup.AddPermission(BookStorePermissions.Authors.Default, L("Permission:Authors"));
        authorsPermission.AddChild(BookStorePermissions.Authors.Create, L("Permission:Authors.Create"));
        authorsPermission.AddChild(BookStorePermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authorsPermission.AddChild( BookStorePermissions.Authors.Delete, L("Permission:Authors.Delete"));

        var actorsPermission = bookStoreGroup.AddPermission(BookStorePermissions.Actors.Default, L("Permission:Actors"));
        actorsPermission.AddChild(BookStorePermissions.Actors.Create, L("Permission:Actors.Create"));
        actorsPermission.AddChild(BookStorePermissions.Actors.Edit, L("Permission:Actors.Edit"));
        actorsPermission.AddChild(BookStorePermissions.Actors.Delete, L("Permission:Actors.Delete"));

        var directorsPermission = bookStoreGroup.AddPermission(BookStorePermissions.Directors.Default, L("Permission:Directors"));
        directorsPermission.AddChild(BookStorePermissions.Directors.Create, L("Permission:Directors.Create"));
        directorsPermission.AddChild(BookStorePermissions.Directors.Edit, L("Permission:Directors.Edit"));
        directorsPermission.AddChild(BookStorePermissions.Directors.Delete, L("Permission:Directors.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }

}
