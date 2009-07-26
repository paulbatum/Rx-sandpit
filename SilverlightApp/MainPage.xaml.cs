using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApp
{
    public partial class MainPage : UserControl
    {

        public MainPage()
        {
            InitializeComponent();

            
            IObservable<Event<RoutedEventArgs>> clicks = Observable.FromEvent<RoutedEventArgs>(button1, "Click");

            int count = 0;
            clicks.Subscribe(() => count++);

            IObservable<string> messages = from c in clicks
                                           select string.Format("Clicked {0} time{1}", count, count > 1 ? "s" : "");
            messages.Subscribe(s => button1.Content = s);

        }
    }

    public static class Extensions
    {
        public static IObservable<Event<RoutedEventArgs>> GetClicks(this Button button)
        {
            return Observable.FromEvent((EventHandler<RoutedEventArgs> genericHandler) => new RoutedEventHandler(genericHandler),
                                        routedHandler => button.Click += routedHandler,
                                        routedHandler => button.Click -= routedHandler);
        }

    }

}
