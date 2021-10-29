using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VSGMMemoryLeak.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefaultPage : ContentPage
    {
        INavigationService _navService;

        public DefaultPage()
        {
            _navService = SimpleIoc.Default.GetInstance<INavigationService>();

            InitializeComponent();
        }

        private void NavToLeakPage(object sender, EventArgs e)
        {
            _navService.NavigateTo(PageRoutes.LeakyPage);
        }

        private void NavToNonLeakPage1(object sender, EventArgs e)
        {
            _navService.NavigateTo(PageRoutes.NonLeakyPage1);
        }

        private void NavToNonLeakPage2(object sender, EventArgs e)
        {
            _navService.NavigateTo(PageRoutes.NonLeakyPage2);
        }
    }
}