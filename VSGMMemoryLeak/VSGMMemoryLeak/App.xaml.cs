using GalaSoft.MvvmLight.Ioc;
using System;
using System.Linq;
using VSGMMemoryLeak.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VSGMMemoryLeak
{
    public partial class App : Application
    {
        public static bool IsInitialized { get; private set; }

        public App()
        {
            if(!Application.Current.Resources.MergedDictionaries.Any((rd) => rd is StyleResources))
                Application.Current.Resources.MergedDictionaries.Add(new StyleResources());

            InitializeComponent();

            InitNavigationService();

            IsInitialized = true;
        }

        private void InitNavigationService()
        {
            NavigationService navService;
            if (!IsInitialized)
            {
                navService = new NavigationService();
                navService.Configure(PageRoutes.LeakyPage, typeof(LeakingPage));
                navService.Configure(PageRoutes.NonLeakyPage1, typeof(NonLeakingPage1));
                navService.Configure(PageRoutes.NonLeakyPage2, typeof(NonLeakingPage2));

                SimpleIoc.Default.Register<INavigationService>(() => navService);
            }
            else
            {
                navService = SimpleIoc.Default.GetInstance<INavigationService>() as NavigationService;
            }

            MainPage = new NavigationPage(new DefaultPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
