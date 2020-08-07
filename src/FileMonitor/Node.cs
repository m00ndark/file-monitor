using System.Collections.Generic;
using System.Linq;

namespace FileMonitor
{
	public class Node
	{
		public Node(string name, IEnumerable<Node> nodes)
		{
			Name = name;
			Nodes = nodes.ToList();
		}

		public string Name { get; set; }
		public List<Node> Nodes { get; set; }
	}
}
