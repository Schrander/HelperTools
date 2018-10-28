using System;
using System.Linq;

namespace HelperTools
{
   public static class NullableHelper
   {
      /// <summary>
      /// All items are null.
      /// </summary>
      /// <param name="items"></param>
      /// <returns><c>true</c> if so</returns>
      public static bool AllAreNull(params object[] items)
      {
         return items.All(item => item == null);
      }

      public static bool AllAreNullOrZero<T>(params T?[] items) where T : struct
      {
         if (typeof(T) == typeof(DateTime))
            throw new ArgumentException("DateTime not allowed.");

         return items.All(item => !item.HasValue || Convert.ToInt64(item.Value) == 0);
      }

      /// <summary>
      /// All items has a value.
      /// </summary>
      /// <param name="items"></param>
      /// <returns><c>true</c> if so otherwise <c>false</c></returns>
      public static bool AllHasValue(params object[] items)
      {
         return items.All(item => item != null);
      }


      /// <summary>
      /// One or more items are null
      /// </summary>
      /// <param name="items"></param>
      /// <returns><c>true</c> if so</returns>
      public static bool AnyIsNull(params object[] items)
      {
         return items.Any(item => item == null);
      }

      /// <summary>
      /// One or more items has a Value
      /// </summary>
      /// <param name="items"></param>
      /// <returns><c>true</c> if so otherwise <c>false</c></returns>
      public static bool AnyHasValue(params object[] items)
      {
         return items.Any(item => item != null);
      }

      public static bool AnyIsNullOrZero<T>(params T?[] items) where T : struct
      {
         if (typeof(T) == typeof(DateTime))
            throw new ArgumentException("DateTime not allowed.");

         return items.Any(item => !item.HasValue || Convert.ToInt64(item.Value) == 0);
      }

   }
}
