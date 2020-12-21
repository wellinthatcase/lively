// =============================================== //
//		    © 2020 - All rights reserved.          // 
// =============================================== //

using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Diagnostics;
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
        static string[] ForbiddenKeywords = { "NUL", "PRN", "CON", "AUX" };

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
            SubmitBtn.Visibility = Visibility.Hidden;
            return false; 
        }

        private delegate void FalsifyTest();
        private bool LegalizationFunc(string txt)
        {
            txt = txt.Trim();
            bool successfullyPassed = true;

            FalsifyTest failed = delegate ()
            {
                successfullyPassed = false;
            };

            if (ForbiddenKeywords.Contains(txt))
            {
                failed();
            }

            if (txt.Length == 4
                && char.IsDigit(txt.Last())
                && txt.StartsWith("COM") || txt.StartsWith("LPT"))
            {
                failed();
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
                failed();
            }

            return successfullyPassed ? true : HideSubmitButton();
        }

        private void LaunchEditorProcess()
        {
            Process proc = new Process();

            proc.StartInfo.FileName = "bin\\Editor.exe";
            proc.StartInfo.Arguments = string.Format("\"{0}\"", NoteName.Text);
            proc.Start();

            Application.Current.Shutdown();
        }

        private void LaunchEditor(object sender, MouseButtonEventArgs e)
        {
            LaunchEditorProcess();
        }

        private void EnsureEnterAsAlternativeToSubmitBtn(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && LegalizationFunc(NoteName.Text))
            {
                LaunchEditorProcess();
            }
        }
    }
}
