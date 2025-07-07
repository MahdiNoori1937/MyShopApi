namespace MyShop.Application.Extensions;

public static class ModelExtensions
{
    public static bool ModelIsNull<T>(this T value)
    => value is null;
}

public static class ListExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? list)
        => list == null || !list.Any();
}