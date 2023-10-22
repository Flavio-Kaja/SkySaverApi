namespace SkySaver.Domain;

using System.Reflection;

public static class Permissions
{
    public const string CanUpdate = "CanUpdate";
    public const string CanReadPreferences = "CanReadPreferences";
    public const string CanDeleteUsers = "CanDeleteUsers";
    public const string CanUpdateUsers = "CanUpdateUsers";
    public const string CanAddUsers = "CanAddUsers";
    public const string CanReadUsers = "CanReadUsers";
    public const string CanDeleteRolePermissions = "CanDeleteRolePermissions";
    public const string CanUpdateRolePermissions = "CanUpdateRolePermissions";
    public const string CanAddRolePermissions = "CanAddRolePermissions";
    public const string CanReadRolePermissions = "CanReadRolePermissions";
    public const string CanRemoveUserRoles = "CanRemoveUserRoles";
    public const string CanAddUserRoles = "CanAddUserRoles";
    public const string CanGetRoles = "CanGetRoles";
    public const string CanGetPermissions = "CanGetPermissions";


    public const string CanCreateProduct = "CanCreateProduct";
    public const string CanReadProduct = "CanReadProduc";
    public const string CanUpdateProduct = "CanUpdateProduct";
    public const string CanDeleteProduct = "CanDeleteProduct";
    public const string CanFilterProducts = "CanFilterProducts";


    public const string CanCreateTransportation = "CanCreateTransportation";
    public const string CanReadTransportation = "CanReadTransportation";
    public const string CanUpdateTransportation = "CanUpdateTransportation";
    public const string CanDeleteTransportation = "CanDeleteTransportation";


    public const string CanViewMonthlyRouteDistance = "CanViewMonthlyRouteDistance";

    public static List<string> GetUserPermissions()
    {
        return new List<string>
        {
            CanCreateProduct,CanReadProduct,CanUpdateProduct,CanDeleteProduct
        };
    }
    public static List<string> List()
    {
        return typeof(Permissions)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => (string)x.GetRawConstantValue())
            .ToList();
    }
}
