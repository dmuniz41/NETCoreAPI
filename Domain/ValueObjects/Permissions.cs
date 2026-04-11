using System;

namespace Domain.ValueObjects;

public static class Permissions
{
    public const string ViewProducts = "view_products";
    public const string CreateProducts = "create_products";
    public const string EditProducts = "edit_products";
    public const string DeleteProducts = "delete_products";
    public const string ViewUsers = "view_users";
    public const string ManageUsers = "manage_users";
    public const string ViewOrders = "view_orders";
    public const string ManageOrders = "manage_orders";

    public static IReadOnlyList<string> All => new[]
    {
        ViewProducts, CreateProducts, EditProducts, DeleteProducts,
        ViewUsers, ManageUsers, ViewOrders, ManageOrders
    };
}
