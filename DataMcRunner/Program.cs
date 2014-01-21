using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMcRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			string query = @"select top 100 * from products;";
			YieldReader<Product> reader = new YieldReader<Product>();
			var results = reader.YieldRead(query);
			foreach (var x in results)
				Console.WriteLine("{0}, {1}", x.productid, x.title);

			Console.ReadLine();
		}
	}

	public class YieldReader<T>
	{
		public IEnumerable<T> YieldRead(string query)
		{
			using (var connection = new SqlConnection(Config.ConnectionString))
			using (var command = connection.CreateCommand())
			{
				command.CommandText = query;
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					var type = typeof(T);
					var instance = Activator.CreateInstance(typeof(T));
					var props = type.GetProperties();
					foreach (var p in props)
					{
						var r = reader[p.Name];
						p.SetMethod.Invoke(instance, new object[] { r });
					}

					yield return (T)instance;
				}
			}
		}
	}

	public static class Config
	{
		public static string ConnectionString { get { return @"data source=YOUR_DATA_SOURCE;initial catalog=YOUR_CATALOG;user id =YOUR_USERNAME;password=YOUR_PASSWORD;MultipleActiveResultSets=True;"; ; } }
	}

	public class Product
	{		
		/// <summary>
		/// ADD your properties here. 
		/// As long as the property names match up to columns, you're in the clear 
		/// </summary>
		public long productid { get; set; }

		public string title { get; set; }
	}
}
