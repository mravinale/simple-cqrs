using System.Web.Script.Serialization;

namespace Cronos.Infrastructure.Infractructure
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// Convert the object to JSON string
		/// </summary>
		/// <param name="o">The object which is to be converted to JSON string.</param>
		/// <returns>Returns the JSON string.</returns>
		public static string ToJSON(this object o)
		{
			return new JavaScriptSerializer().Serialize(o);
		}

	}
}
