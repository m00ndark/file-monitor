using System.Windows.Input;

namespace FileMonitor.ViewModel
{
	public class FileViewModel : ViewModelBase
	{
		public FileViewModel()
		{
			DeleteCommand = new RelayCommand(Delete);
		}

		public ICommand DeleteCommand { get; }

		public string Name
		{
			get => Get<string>();
			set => Set(value);
		}

		private void Delete()
		{
		}
	}
}
