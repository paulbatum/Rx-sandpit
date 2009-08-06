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

            var observerSelections = Observable.Merge(
                Observable.FromEvent<RoutedEventArgs>(rbSelectMany, "Checked")
                    .Select(_ => from s1 in choiceControl1.OptionSelections
                                 from s2 in choiceControl2.OptionSelections
                                 from s3 in choiceControl3.OptionSelections
                                 select new[] { s1, s2, s3 }),
                Observable.FromEvent<RoutedEventArgs>(rbForkJoin, "Checked")
                    .Select(_ => Observable.ForkJoin(choiceControl1.OptionSelections, choiceControl2.OptionSelections, choiceControl3.OptionSelections)),
                Observable.FromEvent<RoutedEventArgs>(rbCombineLatest, "Checked")
                    .Select(_ => choiceControl1.OptionSelections
                                    .CombineLatest(choiceControl2.OptionSelections, (i, j) => new[] { i, j })
                                    .CombineLatest(choiceControl3.OptionSelections, (array, k) => new[] { array[0], array[1], k }))
                );

            IDisposable subscription = null;
            observerSelections.Subscribe(observer =>
                                         {
                                             if (subscription != null)
                                                 subscription.Dispose();

                                             choiceControl1.ClearAll();
                                             choiceControl2.ClearAll();
                                             choiceControl3.ClearAll();
                                             statusText.Text = string.Empty;

                                             subscription = observer.Subscribe(UpdateSelectedOptions);                                                                               
                                         });


        }

        private void UpdateSelectedOptions(int[] values)
        {
            statusText.Text = string.Format("Option {0}, Option {1}, Option {2}",
                                            values[0], values[1], values[2]);
        }
    }

}
