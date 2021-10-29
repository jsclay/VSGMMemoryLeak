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
    public partial class NonLeakingPage2 : ContentPage
    {
        INavigationService _navService;
        
        public NonLeakingPage2()
        {
            _navService = SimpleIoc.Default.GetInstance<INavigationService>();

            InitializeComponent();
        }

        ~NonLeakingPage2()
        {
            System.Diagnostics.Debug.WriteLine("NonLeakingPage2 Destructed");
        }

        private void NavToLeakPage(object sender, EventArgs e)
        {
            _navService.NavigateTo(PageRoutes.LeakyPage);
        }

        private void NavToNonLeakPage1(object sender, EventArgs e)
        {
            _navService.NavigateTo(PageRoutes.NonLeakyPage1);
        }
    }
}