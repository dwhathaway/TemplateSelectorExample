﻿using System;
using System.ComponentModel;

namespace TemplateSelectorExample.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public string Title { get; set; }
		public bool IsInitialized { get; set; }

		private bool isBusy;
		/// <summary>
		/// Gets or sets if VM is busy working
		/// </summary>
		public bool IsBusy
		{
			get { return isBusy; }
			set { isBusy = value; OnPropertyChanged("IsBusy"); }
		}

        private bool pullToRefresh;
		/// <summary>
		/// Gets or sets if VM is busy working
		/// </summary>
		public bool PullToRefresh
		{
			get { return pullToRefresh; }
			set { pullToRefresh = value; OnPropertyChanged("PullToRefresh"); }
		}

		//INotifyPropertyChanged Implementation
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
