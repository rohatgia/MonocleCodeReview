using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Monocle.Core.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		bool isBusy;

		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				RaiseAndUpdate(ref isBusy, value);
				Raise(nameof(IsNotBusy));
			}
		}

		public bool IsNotBusy => !IsBusy;

		public virtual Task InitAsync() => Task.FromResult(true);

		protected void RaiseAndUpdate<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			field = value;
			Raise(propertyName);
		}

		protected void Raise(string propertyName)
		{
			if (!string.IsNullOrEmpty(propertyName) && PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
