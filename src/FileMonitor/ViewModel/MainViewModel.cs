using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace FileMonitor.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		public MainViewModel()
		{
			ScanCommand = new RelayCommand(Scan);
			FileGroups = new ObservableCollection<FileGroupViewModel>();
		}

		public ICommand ScanCommand { get; }

		public string Filter
		{
			get => Get<string>();
			set => Set(value);
		}

		public ObservableCollection<FileGroupViewModel> FileGroups { get; }

		private void Scan()
		{
			FileGroups.Clear();

			Match match = Regex.Match(Filter, @"^(?<path>(?<root>\w\:\\[^\*]*)(?:\*\*\\)*[^\*]*)(?<ext>\*\..+)$");

			if (!match.Success)
			{
				MessageBox.Show("Filter contains no wildcard");
			}

			string path = match.Groups["path"].Value;
			string pathPattern = path
				.Replace(@"\", @"\\")
				.Replace(@"**\\", @"(?:[^\\]+\\)*");
			pathPattern = $"^{pathPattern}$";

			string rootPath = match.Groups["root"].Value;
			string extension = match.Groups["ext"].Value;


			foreach (var fileGroup in Directory.EnumerateFiles(rootPath, extension, SearchOption.AllDirectories)
				.Select(filePath => new { Path = Path.GetDirectoryName(filePath) + @"\", Name = Path.GetFileName(filePath) })
				.Where(file => Regex.IsMatch(file.Path, pathPattern))
				.GroupBy(file => file.Path)
				.OrderBy(fileGroup => fileGroup.Key))
			{
				FileGroupViewModel fileGroupViewModel = new FileGroupViewModel(fileGroup.Key, Delete);
				FileGroups.Add(fileGroupViewModel);

				foreach (var file in fileGroup)
				{
					fileGroupViewModel.AddFile(file.Name);
				}
			}
		}

		private void Delete(string filePath)
		{
			//File.Delete(filePath);
			Debug.WriteLine(filePath);
		}
	}
}
