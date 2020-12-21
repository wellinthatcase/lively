// =============================================== //
//		    © 2020 - All rights reserved.          // 
// =============================================== //

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Editor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitApplication(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show(
                "Are you sure you wanna quit?", 
                "Confirm", 
                MessageBoxButton.YesNoCancel);

            if (res == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void MinimizeApplication(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void RenderPotentialNoteName(object sender, RoutedEventArgs e)
        {
            string[] lpCmdLineArgs = Environment.GetCommandLineArgs();

            try
            {
                string tmp = lpCmdLineArgs[1];

                if (tmp.Length >= 20)
                {
                    NoteName.Content = tmp.Substring(0, 15) + "...";
                }
                else
                {
                    NoteName.Content = tmp; 
                }
            }
            catch (IndexOutOfRangeException)
            {
                NoteName.Content = "Untitled note.";
                MessageBox.Show("A note name should be passed on the command line.");
            }
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BringTitleBarToTop(object sender, RoutedEventArgs e)
        {
            TitleBar.BringIntoView();
        }

        private void Keybinder(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                switch (e.Key)
                {
                    case Key.S:
                        {
                            SaveFileDialog res = new SaveFileDialog();

                            res.Filter = "Text file (*.txt)|*.txt";
                            res.FileName = NoteName.Content.ToString();
                            res.AddExtension = true;

                            TextRange rng = new TextRange(
                                TextEntry.Document.ContentStart,
                                TextEntry.Document.ContentEnd);

                            if (res.ShowDialog() == true)
                            {
                                File.WriteAllText(res.FileName, rng.Text);
                            }

                            break;
                        }
                }
            }
        }
    }
}
