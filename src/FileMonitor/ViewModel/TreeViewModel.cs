using System;
using System.Windows.Input;

namespace FileMonitor.ViewModel
{
	public class TreeViewModel : ViewModelBase
	{
		public TreeViewModel(Node node, Action<Node> delete)
		{
			Node = node;
			Name = node.Name;
			DeleteCommand = new RelayCommand(() => delete(Node));
			IsExpanded = true;
		}

		public Node Node { get; }
		public ICommand DeleteCommand { get; }

		public bool IsExpanded
		{
			get => Get<bool>();
			set => Set(value);
		}

		public string Name
		{
			get => Get<string>();
			set => Set(value);
		}
	}
}
