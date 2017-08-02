using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Monocle.Core.Navigation;

namespace Monocle.Core.ViewModels
{
    public class LoginViewModel : BaseNavigationViewModel
    {
        public ICommand SSO_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new WebViewLocal("http://www.xamarin.com/"));
        }

        public override async Task InitAsync()
        {
            IsBusy = true;
        }
        }
}
