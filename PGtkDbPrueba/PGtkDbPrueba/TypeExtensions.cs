using System;
using System.Collections.Generic;

namespace PGtkDbPrueba
{
	public static class TypeExtensions
	{
	/// <summary>
	/// Gets the types.
	/// </summary>
	/// <returns>
	/// The types.
	/// </returns>
	/// <param name='type'>
	/// Type.
	/// </param>
	/// <param name='count'>
	/// Count.
	/// </param>
		public static Type[] GetTypes(Type type, int count)
		{
			List<Type> types = new List<Type>();
			for (int i = 0; i < count; i++)
				 types.Add(type);
			return types.ToArray();
		
		}
	}
}

