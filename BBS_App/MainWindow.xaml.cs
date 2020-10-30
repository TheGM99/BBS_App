using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BBS_Lib;

namespace BBS_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Funkcja uruchamiana przy naciśnięciu przycisku "Generate"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] array;
            StringBuilder array_text = new StringBuilder();
            array = BBS_Generator.Generate(Convert.ToInt32(Convert.ToInt32(20000)));

            var SB_Test = BBS_Tests.SingleBitTest(array);
            var Series_Test = BBS_Tests.series_test(array);
            var LS_Test = BBS_Tests.long_series_test(array);
            var Poker_test = BBS_Tests.poker_test(array);

            SingularBit_result.Text = (SB_Test.result ? "Passed" : "Failed");
            Series_Result.Text = (Series_Test.result ? "Passed" : "Failed");
            Longseries_result.Text = (LS_Test.result ? "Passed" : "Failed");
            Poker_result.Text = (Poker_test.result ? "Passed" : "Failed");

            Singular_Count.Text = SB_Test.value.ToString();
            LongSeries_count.Text = LS_Test.value.ToString();
            Poker_X.Text = "2.16 < " + Math.Round(Poker_test.value,2).ToString() + " < 46.17";

            StringBuilder series_text = new StringBuilder();
            series_text.Append($"1 - {Series_Test.value[1]} \t\t\t<2315 - 2685>\n");
            series_text.Append($"2 - {Series_Test.value[2]} \t\t\t<1114 - 1386>\n");
            series_text.Append($"3 - {Series_Test.value[3]} \t\t\t<527 - 723>\n");
            series_text.Append($"4 - {Series_Test.value[4]} \t\t\t<240 - 384>\n");
            series_text.Append($"5 - {Series_Test.value[5]} \t\t\t<103 - 209>\n");
            series_text.Append($"6 or higher - {Series_Test.value[6]} \t<103 - 209>\n");
            Series_Count.Text = series_text.ToString();

            foreach (var i in array) array_text.Append(i.ToString());
            Series_Display.Text = array_text.ToString();
        }
    }
}
