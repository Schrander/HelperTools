namespace HelperTools.Helpers
{
   public static class RegexMaskHelper
   {
      public static string DateTimeMask
      {
         get { return @"(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[012])-([1-9]{1}[0-9]{3})"; }
      }

      public static string JaarMask
      {
         get { return @"([12]{1}[90]{1}[0-9]{2})"; }
      }

      public static string HuisnummerMask
      {
         get { return @"([1-9][0-9]{0,4})"; }
      }

      public static string GetAlfanumeriekMask(int? amount)
      {
         return AlfanumeriekMask + (amount.HasValue ? "{0," + string.Format("{0}", amount) + "}" : @"*");
      }

      public static string GetAlfaMask(int? amount)
      {
         return AlfaMask + (amount.HasValue ? "{0," + string.Format("{0}", amount) + "}" : @"*");
      }

      public static string NumeriekMask
      {
         get { return @"0#,###,###"; }
      }

      public static string FractionalMask
      {
         get { return @"0#,###,###.##"; }
      }

      public static string KoersMask
      {
         get { return @"0#,###.00#"; }
      }

      public static string CurrencyMask
      {
         get { return "¢£¤¥$€"; }
      }

      public static string DiacriticMask
      {
         get { return @"ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüµýÿþ"; }
      }

      private static string AlfanumeriekMask
      {
         get { return @"[A-Za-z0-9" + DiacriticMask + CurrencyMask + LeestekensMask + @"]"; }
      }

      public static string LeestekensMask
      {
         get { return @"&!¡#%^\*\(\)\|:?¿,<>«»\\~`\)\-_'@.·\+§°=\{\}\[\]:;¶¦©ª¼½¾×÷±º¹²³¬­ " + '"'; }
      }

      public static string LeestekensNaamMask
      {
         get { return @"\-'. "; }
      }

      public static string MemoFieldMask
      {
         get { return @"[A-Za-z0-9" + DiacriticMask + CurrencyMask + LeestekensMask + @"]*"; }
      }

      public static string PostcodeMask
      {
         get { return @"[1-9]{1}[0-9]{3} [A-Za-z]{2}"; }
      }

      //Cijfers zijn weggelaten
      private static string AlfaMask
      {
         get { return @"[A-Za-z" + DiacriticMask + CurrencyMask + LeestekensNaamMask + @"]"; }
      }
   }
}