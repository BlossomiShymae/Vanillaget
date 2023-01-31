namespace Vanillaget.Extensions
{
	public static class StringExtensions
	{
		public static string AppendPad(this string str, string text)
		{
			return str + " " + text;
		}
	}
}
