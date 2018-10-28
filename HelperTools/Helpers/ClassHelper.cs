
namespace HelperTools.Helpers.CSharp
{
   public static class ClassHelper
   {
      public static string GetClassName(object obj)
      {
         string[] splitted = obj.ToString().Split('.');
         return splitted[splitted.Length - 1];
      }
   }
}
