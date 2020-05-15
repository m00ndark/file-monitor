using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Controls = System.Windows.Controls;

namespace FileMonitor
{
	public static class Grid
	{
		public static readonly DependencyProperty _rowsProperty =
			DependencyProperty.RegisterAttached("Rows", typeof(string), typeof(Controls.Grid), new PropertyMetadata(OnRowsChanged));

		public static readonly DependencyProperty _columnsProperty =
			DependencyProperty.RegisterAttached("Columns", typeof(string), typeof(Controls.Grid), new PropertyMetadata(OnColumnsChanged));

		public static string GetRows(DependencyObject dependencyObject)
		{
			return (string) dependencyObject.GetValue(_rowsProperty);
		}

		public static void SetRows(DependencyObject dependencyObject, string value)
		{
			dependencyObject.SetValue(_rowsProperty, value);
		}

		public static string GetColumns(DependencyObject dependencyObject)
		{
			return (string) dependencyObject.GetValue(_columnsProperty);
		}

		public static void SetColumns(DependencyObject dependencyObject, string value)
		{
			dependencyObject.SetValue(_columnsProperty, value);
		}

		private static void OnRowsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			Controls.Grid grid = dependencyObject as Controls.Grid;
			string lengths = e.NewValue as string;

			if (grid == null)
			{
				return;
			}

			grid.RowDefinitions.Clear();

			foreach (GridLength rowLength in ToGridLengths(lengths))
			{
				grid.RowDefinitions.Add(new RowDefinition { Height = rowLength });
			}
		}

		private static void OnColumnsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			Controls.Grid grid = dependencyObject as Controls.Grid;
			string lengths = e.NewValue as string;

			if (grid == null)
			{
				return;
			}

			grid.ColumnDefinitions.Clear();

			foreach (GridLength columnLength in ToGridLengths(lengths))
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition { Width = columnLength });
			}
		}

		private static IEnumerable<GridLength> ToGridLengths(string lengths)
		{
			List<GridLength> gridLengths = new List<GridLength>();

			if (string.IsNullOrEmpty(lengths))
			{
				return gridLengths;
			}

			string[] lengthSplit = lengths.Split(',');
			GridLengthConverter gridLengthConverter = new GridLengthConverter();

			foreach (string length in lengthSplit)
			{
				object convertFromString = gridLengthConverter.ConvertFromString(length);
				if (convertFromString != null)
				{
					gridLengths.Add((GridLength) convertFromString);
				}
			}

			return gridLengths;
		}
	}
}
