using System;

namespace FileMonitor.ViewModel
{
	public class FileViewModel : TreeViewModel
	{
		public FileViewModel(Node node, Action<Node> delete)
			: base(node, delete) { }
	}
}
