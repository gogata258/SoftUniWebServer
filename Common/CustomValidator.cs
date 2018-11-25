using System;

public static class CustomValidator
{
	public static void ThrowIfNull(object obj, string name)
	{
		if (obj == null)
			throw new ArgumentNullException(name);
	}
	public static void ThrowIfNullOrEmpty(string obj, string name)
	{
		if (string.IsNullOrEmpty(obj))
			throw new ArgumentException($"{name} cannot be null or empty");
	}
	public static void ThrowIfNullOrWhiteSpaced(string obj, string name)
	{
		if (string.IsNullOrWhiteSpace(obj))
			throw new ArgumentException($"{name} cannot be null or whitespaced");
	}
}
