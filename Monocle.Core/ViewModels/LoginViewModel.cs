using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Monocle.Core.ViewModels
{
    public class LoginViewModel
    {
        public ICommand SSO_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new WebView("http://www.xamarin.com/"));
        }
    }
}
