// =============================================== //
//		    © 2020 - All rights reserved.          // 
// =============================================== //

using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Lively
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void FocusInput(object sender, RoutedEventArgs e)
        {
            if (!NoteName.Focus())
            {
                MessageBox.Show(
                    "Hi there,\r\n\r\nI tried focusing onto the text box " +
                    "you're about to see, but it failed. For reference," +
                    " click right above the dark red line :)");
            }
        }

        private void LegalizeCharacters(object sender, TextChangedEventArgs e)
        {
            string txt = NoteName.Text;

            SubmitBtn.Visibility =
                Convert.ToBoolean(txt.Length) && LegalizationFunc(txt)
                    ? Visibility.Visible
                    : Visibility.Hidden;
        }

        private bool HideSubmitButton()
        {
            SubmitBtn.Visibility = Visibility.Visible;
            return false; 
        }

        private bool LegalizationFunc(string txt)
        {
            bool successfullyPassed = true; 

            if (txt == "NUL"
                || txt == "AUX"
                || txt == "PRN"
                || txt == "CON")
            {
                successfullyPassed = false;
            }

            if (txt.Length == 4
                && char.IsDigit(txt.Last())
                && txt.StartsWith("COM") || txt.StartsWith("LPT"))
            {
                successfullyPassed = false; 
            }

            if (txt.Contains("<")
                || txt.Contains(">")
                || txt.Contains("|")
                || txt.Contains(":")
                || txt.Contains("?")
                || txt.Contains("*")
                || txt.Contains("/")
                || txt.Contains("\\")
                || txt.Contains("\""))
            {
                successfullyPassed = false; 
            }

            return successfullyPassed ? true : HideSubmitButton();
        }
    }
}
