DataMcRunner
============

Tidy little abstracted data streamer

Tired of writing crappy boiler plate data streaming code? Do this instead. 

Hate writing this...

```
public void StreamCrap()
{

	// 	TOOOO MUCH CODE!!!!!!!!!!!
	try
	{

		// gross - boiler plate is boring
		using (var connection = new SqlConnection(_connectionString))
		using (var command = connection.CreateCommand()) // yawn
		{
			string query = "select * from whatever";
			command.CommandText = query;
			connection.Open(); // why do I have to do this every time? JUST DO IT FOR ME!

			while (reader.Read())
			{
				try
				{
					long id = reader.IsDBNull(0) ? 0 : reader.GetInt64(0);
					long x = reader.IsDBNull(1) ? -1 : reader.GetInt64(1);
					long y = reader.IsDBNull(2) ? -1 : reader.GetInt64(2);
					long foo_bar = reader.IsDBNull(3) ? -1 : reader.GetInt64(3);
				}
				catch (Exception e)
				{
        			// ACK!
				}
				finally
				{
					// who cares?				
				}
			}
		}
	}
	catch (Exception ex)
	{
		// ACK!
	}
}
```

Write this instead...

```
static void Main(string[] args)
{

	// it's so easy!
	YieldReader<whatever> reader = new YieldReader<whatever>();

	// ONE LINE!!!
	foreach (var w in reader.YieldRead(query))
		Console.WriteLine("{0}, {1}, {2}, {3}", w.id, w.x, w.y, w.foo_bar);
}

public class whatever
{
	public int id { get; set; }
	public int x { get; set; }
	public int y { get; set; }
	public int foo_bar { get; set; }
}
```
