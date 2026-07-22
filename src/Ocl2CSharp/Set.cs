namespace Ocl2CSharp
{
	public class Set<T>:  HashSet<T>
	{
		public Set() 
		{
		}	

		public Set(params object[] objects)
		{
			foreach (T obj in objects.OfType<T>())
			{
				Add(obj);
			}
		}
	}
}
