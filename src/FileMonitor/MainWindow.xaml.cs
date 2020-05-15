using System.Windows;
using FileMonitor.ViewModel;

namespace FileMonitor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel
				{
					Filter = @"F:\git\axis\**\(Debug|Databases)\*.fdb"
			};
		}
	}
}
