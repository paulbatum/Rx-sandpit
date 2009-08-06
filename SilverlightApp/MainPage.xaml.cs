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

            string formatString = "Option {0}, Option {1}, Option {2}";

            //var selections = from s1 in choiceControl1.OptionSelections
            //                 from s2 in choiceControl2.OptionSelections
            //                 from s3 in choiceControl3.OptionSelections
            //                 select new[] { s1, s2, s3 };

            //var selections = Observable
            //    .ForkJoin(choiceControl1.OptionSelections, choiceControl2.OptionSelections, choiceControl3.OptionSelections);

            var selections = choiceControl1.OptionSelections
                .CombineLatest(choiceControl2.OptionSelections, (i, j) => new[] { i, j })
                .CombineLatest(choiceControl3.OptionSelections, (array, k) => new[] { array[0], array[1], k });

            selections.Subscribe(values => statusText.Text = string.Format(formatString, values[0], values[1], values[2]));
            

        }

    }

}
