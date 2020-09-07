using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FileMonitor
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		private readonly ConcurrentDictionary<string, object> _propertyValues = new ConcurrentDictionary<string, object>();

		public event PropertyChangedEventHandler PropertyChanged;

		protected T Get<T>([CallerMemberName] string name = "")
		{
			return GetProperty<T>(name);
		}

		protected void Set<T>(T value, [CallerMemberName] string name = "")
		{
			SetProperty(value, name);
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			VerifyPropertyName(e.PropertyName);
			PropertyChanged?.Invoke(this, e);
		}

		private void OnPropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected void NotifyAllPropertiesChanged()
		{
			OnPropertyChanged(string.Empty);
		}

		private T GetProperty<T>(string name)
		{
			if (_propertyValues.TryGetValue(name, out object value))
			{
				return (T) value;
			}

			return default;
		}

		private void SetProperty<T>(T value, string name)
		{
			object previousValue = default(T);

			_propertyValues.AddOrUpdate(name,
				key =>
					{
						previousValue = default(T);
						return value;
					},
				(key, storedValue) =>
					{
						previousValue = storedValue;
						return value;
					});

			if (!Equals(previousValue, value))
			{
				OnPropertyChanged(name);
			}
		}

		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		private void VerifyPropertyName(string propertyName)
		{
			if (!string.IsNullOrEmpty(propertyName)
				&& GetType().GetProperties().FirstOrDefault(x => x.Name == propertyName) == null)
			{
				throw new ArgumentException(propertyName + " is not a property on this view model", nameof(propertyName));
			}
		}
	}
}
