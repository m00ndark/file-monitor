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
			Nodes = new ObservableCollection<TreeViewModel>();
		}

		public ICommand ScanCommand { get; }

		public string Filter
		{
			get => Get<string>();
			set => Set(value);
		}

		public ObservableCollection<TreeViewModel> Nodes { get; }

		private void Scan()
		{
			Nodes.Clear();

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

			foreach (Node node in Directory.EnumerateFiles(rootPath, extension, SearchOption.AllDirectories)
				.Where(filePath => Regex.IsMatch(Path.GetDirectoryName(filePath) + @"\", pathPattern))
				.Traverse())
			{
				Nodes.Add(new FolderViewModel(node.Compact(), Delete));
			}
		}

		private void Delete(Node node)
		{
			//File.Delete(filePath);
			Debug.WriteLine(node.FullPath);
		}
	}
}
