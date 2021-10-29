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
    public partial class NonLeakingPage1 : ContentPage
    {
        INavigationService _navService;

        public NonLeakingPage1()
        {
            _navService = SimpleIoc.Default.GetInstance<INavigationService>();

            InitializeComponent();
        }

        ~NonLeakingPage1()
        {
            System.Diagnostics.Debug.WriteLine("NonLeakingPage1 Destructed");
        }

        private void NavToLeakPage(object sender, EventArgs e)
        {
            _navService.NavigateTo(PageRoutes.LeakyPage);
        }

        private void NavToNonLeakPage2(object sender, EventArgs e)
        {
            _navService.NavigateTo(PageRoutes.NonLeakyPage2);
        }
    }
}