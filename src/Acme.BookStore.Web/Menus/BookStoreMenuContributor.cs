using System;
using System.Threading.Tasks;
using Acme.BookStore.Localization;
using Acme.BookStore.MultiTenancy;
using Acme.BookStore.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Acme.BookStore.Web.Menus;

public class BookStoreMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<BookStoreResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BookStoreMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
        context.Menu.AddItem(
     new ApplicationMenuItem(
         "BooksStore",
         l["Menu:BookStore"],
         icon: "fa fa-book"
     ).AddItem(
         new ApplicationMenuItem(
             "BooksStore.Books",
             l["Menu:Books"],
             url: "/Books"
         ).RequirePermissions(BookStorePermissions.Books.Default)
     ).AddItem( // ADDED THE NEW "AUTHORS" MENU ITEM UNDER THE "BOOK STORE" MENU
         new ApplicationMenuItem(
             "BooksStore.Authors",
             l["Authors"],
             url: "/Authors"
         ).RequirePermissions(BookStorePermissions.Authors.Default)
     )


 );

        context.Menu.AddItem(
            new ApplicationMenuItem(
         "MoviesStore",
         l["Movie Store"],
         icon: "fa fa-film"
     )
     .AddItem(
         new ApplicationMenuItem(
             "MoviesStore.Movies",
             l["Movies"],
             url: "/Movies"
         ).RequirePermissions(BookStorePermissions.Movies.Default)
     ).AddItem( // ADDED THE NEW "AUTHORS" MENU ITEM UNDER THE "BOOK STORE" MENU
         new ApplicationMenuItem(
             "MoviesStore.Actors",
             l["Actors"],
             url: "/Actors"
         ).RequirePermissions(BookStorePermissions.Actors.Default)
     ).AddItem( // ADDED THE NEW "AUTHORS" MENU ITEM UNDER THE "BOOK STORE" MENU
         new ApplicationMenuItem(
             "MoviesStore.Directors",
             l["Directors"],
             url: "/Directors"
         ).RequirePermissions(BookStorePermissions.Directors.Default)
     ));

        return Task.CompletedTask;
    }
}

