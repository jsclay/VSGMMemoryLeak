using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VSGMMemoryLeak
{
    public interface INavigationService 
    {
        void NavigateTo(string pageKey, object parameter = null, HistoryBehavior historyBehavior = HistoryBehavior.ClearHistory);
    }

    /// <summary>
    /// Navigation Service taken/adapted from https://marcominerva.wordpress.com/2016/07/11/a-simple-navigationservice-for-xamarin-forms/
    /// </summary>
    public class NavigationService : INavigationService
    {
        private Dictionary<string, Type> pages { get; }
            = new Dictionary<string, Type>();

        public Page MainPage => Application.Current.MainPage;

        public void Configure(string key, Type pageType) => pages[key] = pageType;

        public void GoBack() => MainPage.Navigation.PopAsync();

        //HistoryBehavior is ClearHistory by default in order to pop previous pages off of the stack
        public void NavigateTo(string pageKey, object parameter = null,
            HistoryBehavior historyBehavior = HistoryBehavior.ClearHistory)
        {
            Type pageType;
            if (pages.TryGetValue(pageKey, out pageType))
            {
                var displayPage = (Page)Activator.CreateInstance(pageType);
                displayPage.SetNavigationArgs(parameter);

                if (historyBehavior == HistoryBehavior.ClearHistory)
                {
                    if (MainPage.Navigation.NavigationStack.Count > 0)
                    {
                        MainPage.Navigation.InsertPageBefore(displayPage,
                            MainPage.Navigation.NavigationStack[0]);
                    }

                    MainPage.Navigation.PopToRootAsync();
                    //var existingPages = MainPage.Navigation.NavigationStack.ToList();
                    //for (int i = 1; i < existingPages.Count; i++)
                    //    MainPage.Navigation.RemovePage(existingPages[i]);
                }
                else
                {
                    MainPage.Navigation.PushAsync(displayPage);
                }
            }
            else
            {
                throw new ArgumentException($"No such page: {pageKey}.",
                    nameof(pageKey));
            }
        }
    }

    public enum HistoryBehavior
    {
        Default,
        ClearHistory
    }

    public static class NavigationExtensions
    {
        private static ConditionalWeakTable<Page, object> arguments
            = new ConditionalWeakTable<Page, object>();

        public static object GetNavigationArgs(this Page page)
        {
            object argument = null;
            arguments.TryGetValue(page, out argument);

            return argument;
        }

        public static void SetNavigationArgs(this Page page, object args)
            => arguments.Add(page, args);
    }
}
