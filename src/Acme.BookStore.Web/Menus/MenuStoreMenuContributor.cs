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

public class MenuStoreMenuContributor : IMenuContributor
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
                MenuStoreMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0

        ));

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
         "MoviesStore",
         l["Menu:MovieStore"],
         icon: "fa fa-Movie"
     ).AddItem(
         new ApplicationMenuItem(
             "MoviesStore.Movies",
             l["Menu:Movies"],
             url: "/Movies"
         ).RequirePermissions(BookStorePermissions.Movies.Default)
     ).AddItem( // ADDED THE NEW "AUTHORS" MENU ITEM UNDER THE "Movie STORE" MENU
         new ApplicationMenuItem(
             "MoviesStore.Authors",
             l["Authors"],
             url: "/Authors"
         ).RequirePermissions(BookStorePermissions.Authors.Default)
     //)

     //.AddItem(
     //    new ApplicationMenuItem(
     //        "BooksStore.Movies",
     //        l["Movies"],
     //        url: "/Movies"
     //    ).RequirePermissions(BookStorePermissions.Movies.Default)

    //.AddItem( // ADDED THE NEW "AUTHORS" MENU ITEM UNDER THE "BOOK STORE" MENU
    //    new ApplicationMenuItem(
    //        "BooksStore.Authors",
    //        l["Authors"],
    //        url: "/Authors"
    //    ).RequirePermissions(BookStorePermissions.Authors.Default)
    //)

    )
 );

        return Task.CompletedTask;
    }
}

