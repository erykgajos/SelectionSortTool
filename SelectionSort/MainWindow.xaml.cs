using SelectionSortLib;
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
using System.Windows.Threading;


///////////////////////////////////////////////////////////////
// Please open Functions.cs to change random table properties//
///////////////////////////////////////////////////////////////


namespace SelectionSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> table;
        bool bigTable = true;
        
        public MainWindow()
        {
            InitializeComponent();
            mainArrayTextBox.Text = String.Empty;
            table = Functions.GetArray();
            foreach (var item in table)
            {
                if (table.Count <= 100)
                {
                    bigTable = false;
                    mainArrayTextBox.Text += item + " ";
                }
                else
                {
                    mainArrayTextBox.Text = "Table is too big to show";
                    mainArrayTextBox.IsEnabled = false;
                }
            }
        }

        List<int> result = new List<int>();
        private List<int> ReadTable()
        {
            if(bigTable)
            {                
                return table;
            }

            if (!mainArrayTextBox.IsEnabled)
            {
                return result;
            }

            String tableString = mainArrayTextBox.Text;
            List<string> stringList = mainArrayTextBox.Text.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            result = stringList.Select(int.Parse).ToList();

            mainArrayTextBox.IsEnabled = false;

            return result;

        }

        private void SwitchButtonsState()
        {
            if (Button1.IsEnabled)
            {
                Button1.IsEnabled = false;
                Button2.IsEnabled = false;
                Button3.IsEnabled = false;
                Button4.IsEnabled = false;
            }
            else
            {
                Button1.IsEnabled = true;
                Button2.IsEnabled = true;
                Button3.IsEnabled = true;
                Button4.IsEnabled = true;
            }

            Button1.Refresh();
            Button2.Refresh();
            Button3.Refresh();
            Button4.Refresh();
        }

        private void InitializeResult(List<int> table)
        {
            if (!bigTable)
            {
                ResultTableTextBox.Text = String.Empty;
                foreach (var item in table)
                {
                    ResultTableTextBox.Text += item + " ";
                }
            }
            else
            {
                ResultTableTextBox.Text = "Sorted table is too big to show (has more than 100 elements). \nPlease debug to see results";
            }
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            SwitchButtonsState(); // Disable buttons in the UI
            ISelectionSort s1 = new SelectionSortNormal(ReadTable());

            double result = s1.Sort();
            Result1.Content = String.Format("{0:0.00} ms", result);

            InitializeResult(s1.GetSortedArray()); // Initialize result in the window
            SwitchButtonsState();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            SwitchButtonsState();
            ISelectionSort s2 = new SelectionSortSplitSeq(ReadTable());

            double result = s2.Sort();
            Result2.Content = String.Format("{0:0.00} ms", result);

            InitializeResult(s2.GetSortedArray());
            SwitchButtonsState();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            SwitchButtonsState();
            ISelectionSort s3 = new SelectionSortSplitConcurrent(ReadTable());

            double result = s3.Sort();
            Result3.Content = String.Format("{0:0.00} ms", result);

            InitializeResult(s3.GetSortedArray());
            SwitchButtonsState();
        }
        private void Buttonk4_Click(object sender, RoutedEventArgs e)
        {
            SwitchButtonsState();
            ISelectionSort s4 = new SelectionSortSplitParallel(ReadTable());

            double result = s4.Sort();
            Result4.Content = String.Format("{0:0.00} ms", result);

            InitializeResult(s4.GetSortedArray());
            SwitchButtonsState();
        }
    }

    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate () { };
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}
