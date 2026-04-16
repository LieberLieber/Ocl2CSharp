namespace Ocl2CSharp
{
	public class OrderedSet<T>:  SortedSet<T>
	{
		public OrderedSet() 
		{
		}
		public OrderedSet(params object[] objects)
		{
			foreach (T obj in objects.OfType<T>())
			{
				this.Add(obj);
			}
		}
	}
}
