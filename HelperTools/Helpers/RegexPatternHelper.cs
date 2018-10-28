namespace HelperTools.Helpers
{
	public static class RegexPatternHelper
	{
		public static string DateTimePattern => @"(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[012])-([1-9]{1}[0-9]{3})";
		public static string YearPattern => @"?<year>([1-9][0-9]{3})";
		public static string HouseNumberPattern => @"([1-9][0-9]{0,5})";
		public static string NumberPattern => @"0#,###,###";
		public static string FractionalPattern => @"0#,###,###.##";
		public static string KoersMask => @"0#,###.00#";
		public static string CurrencySymbolsPattern => "¢£¤¥$€";
		public static string DiacriticPattern => @"ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüµýÿþ";
		public static string AlfanumericPattern => @"[A-Za-z0-9" + DiacriticPattern + CurrencySymbolsPattern + PunctuationPattern + @"]";
		public static string PunctuationPattern => @"&!¡#%^\*\(\)\|:?¿,<>«»\\~`\)\-_'@.·\+§°=\{\}\[\]:;¶¦©ª¼½¾×÷±º¹²³¬­ " + '"';
		public static string NamePunctuationPattern => @"\-'. ";
		public static string MemoFieldMask => @"[A-Za-z0-9" + DiacriticPattern + CurrencySymbolsPattern + PunctuationPattern + @"]*";
		public static string ZipCodeNLPattern => @"?<zipcode>(?<number>([1-9]{1}[0-9]{3}) ?(?<letter>/P{Lu}{2})";

		private static string AlfaMask => @"[A-Za-z" + DiacriticPattern + CurrencySymbolsPattern + NamePunctuationPattern + @"]";	
		
			public static string GetAlfanumeriekMask(int? amount)
		{
			return AlfanumericPattern + (amount.HasValue ? "{0," + $"{amount}" + "}" : @"*");
		}

		public static string GetAlfaMask(int? amount)
		{
			return AlfaMask + (amount.HasValue ? "{0," + $"{amount}" + "}" : @"*");
		}

	}
}