using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileMonitor
{
	public class Node
	{
		public Node(string name, IEnumerable<Node> nodes)
		{
			Name = name;
			Nodes = nodes
				.Select(node => node.SetParent(this))
				.ToList();
		}

		public string Name { get; private set; }
		public List<Node> Nodes { get; private set; }
		public Node Parent { get; private set; }
		public bool IsFile => Nodes.Count == 0;
		public string FullPath => Parent != null ? Path.Combine(Parent.FullPath, Name) : Name;

		public Node Compact()
		{
			if (!IsFile)
			{
				while (Nodes.Count == 1 && !Nodes[0].IsFile)
				{
					Name = $@"{Name}\{Nodes[0].Name}";
					Nodes = Nodes[0].Nodes;
				}

				foreach (Node node in Nodes)
				{
					node.SetParent(this);
					node.Compact();
				}
			}

			return this;
		}

		private Node SetParent(Node parent)
		{
			Parent = parent;
			return this;
		}
	}
}
