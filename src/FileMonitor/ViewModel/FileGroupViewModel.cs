using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FileMonitor.ViewModel
{
	public class FileGroupViewModel : ViewModelBase
	{
		private readonly Action<string> _delete;

		public FileGroupViewModel(string path, Action<string> delete)
		{
			_delete = delete;
			Path = path;
			DeleteAllCommand = new RelayCommand(DeleteAll, () => Files.Any());
			Files = new ObservableCollection<FileViewModel>();
		}

		public ICommand DeleteAllCommand { get; }

		public string Path
		{
			get => Get<string>();
			set => Set(value);
		}

		public ObservableCollection<FileViewModel> Files { get; }

		public void AddFile(string fileName)
		{
			Files.Add(new FileViewModel(fileName, Delete));
		}

		private void DeleteAll()
		{
			foreach (FileViewModel file in Files.ToList())
			{
				Delete(file);
			}
		}

		private void Delete(FileViewModel file)
		{
			_delete(System.IO.Path.Combine(Path, file.Name));
			Files.Remove(file);
		}
	}
}
