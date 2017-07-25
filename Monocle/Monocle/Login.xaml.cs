using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Monocle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
		}

        private void SSO_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new Page1("http://www.xamarin.com/"));
            /*var SSO = DependencyService.Get<ISSO>();
            if(SSO != null)
            {

            }*/
        }
    }
}
