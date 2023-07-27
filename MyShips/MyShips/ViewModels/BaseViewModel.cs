using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using MyShips.Models;
using MyShips.Services;

namespace MyShips.ViewModels
{
    /// <summary>
    /// Base class for the other ViewModels
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets a reference to the datastore if it exists; otherwise creates a mock data store.
        /// </summary>
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        Boolean m_IsBusy = false;
        String m_Title = string.Empty;

        /// <summary>
        /// Gets or Sets true if busy, false if not.
        /// </summary>
        public Boolean IsBusy
        {
            get { return m_IsBusy; }
            set { SetProperty(ref m_IsBusy, value); }
        }

        /// <summary>
        /// Gets/Sets the Title value.
        /// </summary>
        public String Title
        {
            get { return m_Title; }
            set { SetProperty(ref m_Title, value); }
        }

        /// <summary>
        /// Sets the name property, and raises an event to notify about the change.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
        protected Boolean SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]String propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
