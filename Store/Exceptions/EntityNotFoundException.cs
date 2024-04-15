using System;

namespace Store.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }

        public static void ThrowIfNull<T>(T obj, string message = "Not found")
        {
            if (obj is null)
            {
                throw new EntityNotFoundException(message);
            }
        }
    }
}