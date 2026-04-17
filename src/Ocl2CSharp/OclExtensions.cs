using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocl2CSharp
{
	public static class OclExtensions
	{
		public static IEnumerable<T> Closure<T>(this T obj, Func<T, T> next)
		{
			HashSet<T> visited = new HashSet<T>();	

			for (T current = obj; current != null; current = next(current))
			{
				if (visited.Contains(current))
				{
					continue;
				}
				visited.Add(current);
				yield return current;
			}
		}

		public static bool Includes<T>(this IEnumerable<T> objects, IEnumerable<T> otherObjects)
		{
			if (objects == null)
			{
				return false;
			}

			return otherObjects.All((T item) => objects.Contains(item));
		}

		public static IEnumerable<T> Excluding<T>(this IEnumerable<T> objects, T obj)
		{
			if (objects == null)
			{
				return Enumerable.Empty<T>();
			}

			return objects.Except(new T[1] { obj });
		}

		public static IEnumerable<T> Excluding<T>(this IEnumerable<T> objects, IEnumerable<T> otherObjects)
		{
			if (objects == null)
			{
				return Enumerable.Empty<T>();
			}

			if (otherObjects == null)
			{
				return objects;
			}

			return objects.Except(otherObjects);
		}

		public static bool Includes<T>(this IEnumerable<T> objects, T otherObject)
		{
			return objects?.Contains(otherObject) ?? false;
		}

		public static IEnumerable<T> SelectAsKind<T>(this IEnumerable<object> objects)
		{
			if (objects == null)
			{
				return Enumerable.Empty<T>();
			}

			return objects.OfType<T>();
		}

		public static bool IsOfType<T>(this object obj)
		{
			return obj is T;
		}

		public static T AsType<T>(this object obj) where T : class
		{
			return obj as T;
		}

		public static int IndexOf<T>(this IEnumerable<T> objects, T searchObj)
		{
			if (objects == null)
			{
				return -1;
			}

			int num = 1;
			foreach (T @object in objects)
			{
				if (@object.Equals(searchObj))
				{
					return num;
				}

				num++;
			}

			return -1;
		}

		public static Type Supertype(this object a)
		{
			return a?.GetType().BaseType;
		}

		public static bool Specializes(this object a, object b)
		{
			if (a == null || b == null)
			{
				return false;
			}

			return b.GetType().IsSubclassOf(a.GetType());
		}

		public static bool Exists<T>(this IEnumerable<T> objects, Func<T, bool> predicate)
		{
			return objects?.Any(predicate) ?? false;
		}

		public static bool Exist<T>(this IEnumerable<T> objects, Func<T, bool> predicate)
		{
			return objects?.Any(predicate) ?? false;
		}

		public static IEnumerable<T> Prepend<T>(this T objA, T objB)
		{
			return new T[2] { objA, objB };
		}

		public static IEnumerable<T> Reject<T>(this IEnumerable<T> objects, Func<T, bool> predicate)
		{
			if (objects == null)
			{
				return Enumerable.Empty<T>();
			}

			return objects.Where((T item) => !predicate(item));
		}
		public static bool IsUnique<T, TKey>(this IEnumerable<T> objects, Func<T, TKey> predicate)
		{
			return objects.GroupBy(predicate).All(item => item.Count() == 1);
		}

		public static bool IsEmpty<T>(this IEnumerable<T> objects)
		{
			return !objects.NotEmpty();
		}

		public static int Size<T>(this IEnumerable<T> objects)
		{
			return objects?.Count() ?? 0;
		}

		public static T At<T>(this IEnumerable<T> objects, int pos)
		{
			if (objects == null)
			{
				return default(T);
			}

			pos--;
			return objects.Skip(pos).First();
		}

		public static bool NotEmpty<T>(this IEnumerable<T> objects)
		{
			return objects?.Any() ?? false;
		}

		public static bool OclIsKindOf<T>(this object obj)
		{
			return obj is T;
		}

		public static bool OclKindOf<T>(this T obj, Type type)
		{
			return obj?.GetType() == type;
		}

		public static Type OclType<T>(this T obj)
		{
			return typeof(T);
		}

		public static IEnumerable<T> Including<T>(this IEnumerable<T> objects, T obj)
		{
			if (objects == null)
			{
				return Enumerable.Empty<T>();
			}

			return objects.Union(new T[1] { obj });
		}

		public static bool ForAll<T>(this IEnumerable<T> objects, Func<T, bool> predicate)
		{
			return objects?.All(predicate) ?? false;
		}

		public static IEnumerable<T> SubSequence<T>(this IEnumerable<T> objects, int startPos, int length)
		{
			if (objects == null)
			{
				return Enumerable.Empty<T>();
			}

			return objects.Skip(startPos - 1).Take(length);
		}

		public static OrderedSet<T> AsOrderedSet<T>(this IEnumerable<T> objects)
		{
			if (objects == null)
			{
				return new OrderedSet<T>();
			}

			return new OrderedSet<T>(objects.Distinct());
		}

		public static Set<T> AsSet<T>(this IEnumerable<T> objects)
		{
			if (objects == null)
			{
				return new Set<T>();
			}

			return new Set<T>(objects);
		}
	}
}
