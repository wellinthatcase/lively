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

            SplashScreen.MouseLeftButtonDown += delegate { DragMove(); };
        }

        /**
         * Function: FocusInput
         *
         * Focuses the user's view onto the main text box on launch.
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         *
         * Param:
         * sender -  Source of the event.
         * e -       Routed event information.
         */

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

        /**
         * Function: LegalizeCharacters
         *
         * Actually calls for a legalization check & determines whether or not to show the submit button.
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         *
         * Param:
         * sender -  Source of the event.
         * e -       Text changed event information.
         */

        private void LegalizeCharacters(object sender, TextChangedEventArgs e)
        {
            string txt = NoteName.Text;

            SubmitBtn.Visibility =
                Convert.ToBoolean(txt.Length) && LegalizationFunc(txt)
                    ? Visibility.Visible
                    : Visibility.Hidden;
        }

        /**
         * Function: HideSubmitButton
         *
         * Hides the submit button.
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         *
         * Returns: Always false to serve as a failure return type to LegalizationFunc().
         */

        private bool HideSubmitButton()
        {
            SubmitBtn.Visibility = Visibility.Hidden;
            return false; 
        }

        /**
         * Function: FalsifyTest
         *
         * Template delegate type. Will be used to declare a delegate that asserts if legalization has failed.
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         */

        private delegate void FalsifyTest();

        /**
         * Function: LegalizationFunc
         *
         * Legalization function.
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         *
         * Param:
         * txt -  The text.
         *
         * Returns: True if it succeeds, false if it fails.
         */

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

        /**
         * Function: LaunchEditorProcess
         *
         * Actually launches the editor. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         */

        private void LaunchEditorProcess()
        {
            Process proc = new Process();

            proc.StartInfo.FileName = "bin\\Editor.exe";
            proc.StartInfo.Arguments = string.Format("\"{0}\"", NoteName.Text);
            proc.Start();

            Application.Current.Shutdown();
        }

        /**
         * Function: LaunchEditor
         *
         * Serves as a bridge wiring UI interactions that may call for the editor to be launched. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         *
         * Param:
         * sender -  Source of the event.
         * e -       Mouse button event information.
         */

        private void LaunchEditor(object sender, MouseButtonEventArgs e)
        {
            LaunchEditorProcess();
        }

        /**
         * Function: EnsureEnterAsAlternativeToSubmitBtn
         *
         * Ensures that the Enter key is an alternative to the submit button.
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         *
         * Param:
         * sender -  Source of the event.
         * e -       Key event information.
         */

        private void EnsureEnterAsAlternativeToSubmitBtn(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SubmitBtn.IsVisible)
            {
                LaunchEditorProcess();
            }
        }
    }
}
