using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Calendar
{
	public partial class App : Application
	{
        public static EventDatabase eventDB { get; private set; }
        public App (string dbPath)
		{
			InitializeComponent();

            eventDB = new EventDatabase(dbPath);

			MainPage = new MainPage();
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
