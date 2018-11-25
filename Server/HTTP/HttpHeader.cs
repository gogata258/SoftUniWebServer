namespace Server.HTTP
{
	public class HttpHeader
	{
		public const string ContentType = "Content-Type";
		public const string Host = "Host";
		public const string Location = "Location";
		public const string Cookie = "Cookie";
		public const string SetCookie = "Set-Cookie";

		public HttpHeader(string key, string value)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			CustomValidator.ThrowIfNullOrEmpty(value, nameof(value));

			Key = key;
			Value = value;
		}
		public string Key { get; private set; }
		public string Value { get; private set; }

		public override string ToString()
		{
			return $"{Key}: {Value}";
		}
	}
}
