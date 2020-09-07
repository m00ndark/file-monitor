using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FileMonitor.ViewModel
{
	public class FolderViewModel : TreeViewModel
	{
		private readonly Action<Node> _delete;

		public FolderViewModel(Node node, Action<Node> delete)
			: base(node, delete)
		{
			_delete = delete;
			//DeleteAllCommand = new RelayCommand(DeleteAll, HasFiles);

			Nodes = new ObservableCollection<TreeViewModel>(node.Nodes
				.Select(subNode => subNode.IsFile
					? new FileViewModel(subNode, _delete) as TreeViewModel
					: new FolderViewModel(subNode, _delete)));
		}

		//public ICommand DeleteAllCommand { get; }

		public ObservableCollection<TreeViewModel> Nodes { get; }

		//public bool HasFiles() => Files.Any() || Folders.Any(folder => folder.HasFiles());

		//private void DeleteAll()
		//{
		//	foreach (FileViewModel file in Files.ToList())
		//	{
		//		Delete(file);
		//	}
		//}

		//private void Delete(FileViewModel file)
		//{
		//	_delete(System.IO.Path.Combine(Name, file.Name));
		//	Files.Remove(file);
		//}
	}
}
