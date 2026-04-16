namespace Ocl2CSharp
{
	public class Set<T>:  HashSet<T> where T : class
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
