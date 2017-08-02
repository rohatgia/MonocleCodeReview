using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Monocle.Core.ViewModels;

namespace Monocle.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : BaseContentPage<LoginViewModel>
    {
        public Login()
        {
            InitializeComponent();
        }
    }
}
