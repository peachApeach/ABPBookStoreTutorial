using BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BookStore.Permissions;
//권한정의

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        ////var myGroup = context.AddGroup(BookStorePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));

        var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));
        var bookPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books"));
        bookPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books.Create"));
        bookPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
        bookPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books.Delete"));
        //default 권한이 선택된 경우에만 하위 권한을 선택할 수 있음


        //Add Author Permission
        var authorspermission = bookStoreGroup.AddPermission(BookStorePermissions.Authors.Default, L("Permission:Authors"));
        authorspermission.AddChild(BookStorePermissions.Authors.Create, L("Permission:Authors.Create"));
        authorspermission.AddChild(BookStorePermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authorspermission.AddChild(BookStorePermissions.Authors.Delete, L("Permission:Authors.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
