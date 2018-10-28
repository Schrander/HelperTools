using System.Collections.Specialized;

namespace HelperTools.Extensions
{

   public static class NameValueExt
   {

      public static bool Any(this NameValueCollection coll)
      {
         return coll.Count > 0;
      }
   }
}
