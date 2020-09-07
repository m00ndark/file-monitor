using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileMonitor
{
	public static class Extensions
	{
		public static IEnumerable<Node> Traverse(this IEnumerable<string> paths)
		{
			return paths
				.Where(path => path != null)
				.Select(path => path.Split(new[] { Path.DirectorySeparatorChar }, 2))
				.GroupBy(pathSplit => pathSplit[0], pathSplit => pathSplit.Length > 1 ? pathSplit[1] : null)
				.Select(pathGroup => new Node(pathGroup.Key, pathGroup.Traverse()))
				.OrderBy(node => node.Name);
		}
	}
}
