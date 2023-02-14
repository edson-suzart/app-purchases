namespace AppPurchases.Shared.Extensions
{
    public static class RandomEnum
    {
        static readonly Random random = new();
        public static T RandomEnumValue<T>()
        {
            var value = Enum.GetValues(typeof(T));
            return (T)value.GetValue(random.Next(value.Length));
        }
    }
}
