using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ChoiceControl
    {
        public ChoiceControl()
        {
            InitializeComponent();
            DataContext = this;

            OptionSelections = Observable.Merge(
                Observable.FromEvent<RoutedEventArgs>(optionButton1, "Checked").Select(_ => 1),
                Observable.FromEvent<RoutedEventArgs>(optionButton2, "Checked").Select(_ => 2),
                Observable.FromEvent<RoutedEventArgs>(optionButton3, "Checked").Select(_ => 3)
                );
        }

        public IObservable<int> OptionSelections { get; private set; }

        public string Heading
        {
            get { return (string)GetValue(HeadingProperty); }
            set { SetValue(HeadingProperty, value); }
        }

        public static readonly DependencyProperty HeadingProperty =
            DependencyProperty.Register("HeadingProperty", typeof(string), typeof(ChoiceControl), null);

        public void ClearAll()
        {
            optionButton1.IsChecked = false;
            optionButton2.IsChecked = false;
            optionButton3.IsChecked = false;
        }
    }
}
