using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Monocle
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            var navPage = new NavigationPage() { Title = "App Content" };
            navPage.PushAsync(new Monocle.Login());

			MainPage = navPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
