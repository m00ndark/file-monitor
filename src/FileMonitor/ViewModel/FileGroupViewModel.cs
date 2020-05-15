using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FileMonitor.ViewModel
{
	public class FileGroupViewModel : ViewModelBase
	{
		public FileGroupViewModel()
		{
			DeleteAllCommand = new RelayCommand(DeleteAll);
			Files = new ObservableCollection<FileViewModel>();
		}

		public ICommand DeleteAllCommand { get; }

		public string Path
		{
			get => Get<string>();
			set => Set(value);
		}

		public ObservableCollection<FileViewModel> Files { get; }

		private void DeleteAll()
		{
		}
	}
}
