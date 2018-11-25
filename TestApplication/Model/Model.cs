using System.Collections.Generic;

namespace TestApplication.Model
{
	public class Model
	{
		private readonly IDictionary<string, object> objects;

		public Model()
		{
			objects = new Dictionary<string, object>();
		}

		public object this[string key]
		{
			get => objects[key];
			set => objects[key] = value;
		}

		public void Add(string key, object value)
		{
			objects.Add(key, value);
		}
	}
}
