using System;
using System.Windows.Input;

namespace FileMonitor.ViewModel
{
	public class FileViewModel : ViewModelBase
	{
		public FileViewModel(string name, Action<FileViewModel> delete)
		{
			Name = name;
			DeleteCommand = new RelayCommand(() => delete(this));
		}

		public ICommand DeleteCommand { get; }

		public string Name
		{
			get => Get<string>();
			set => Set(value);
		}
	}
}
