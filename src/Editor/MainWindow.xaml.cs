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
        static string DialogFilters = "All files (*.*)|*.*|Markdown file (*.md)|*.md|Text file (*.txt)|*.txt";

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
                            SaveFileDialog dialog = new SaveFileDialog();

                            dialog.Filter = DialogFilters;
                            dialog.FileName = NoteName.Content.ToString();
                            dialog.AddExtension = true;

                            TextRange rng = new TextRange(
                                TextEntry.Document.ContentStart,
                                TextEntry.Document.ContentEnd);

                            bool? res = dialog.ShowDialog();

                            if (res == true)
                            {
                                File.WriteAllText(dialog.FileName, rng.Text);
                            }

                            e.Handled = true; 
                            break;
                        }

                    case Key.O:
                        {
                            OpenFileDialog dialog = new OpenFileDialog();
                            dialog.Filter = DialogFilters;
                            bool? res = dialog.ShowDialog();

                            if (res == true)
                            {
                                string filName = dialog.FileName;
                                string content = File.ReadAllText(filName);
                                string nNoteId = Path.GetFileNameWithoutExtension(filName);

                                if (nNoteId.Length >= 20)
                                {
                                    nNoteId = nNoteId.Substring(0, 15) + "...";
                                }

                                NoteName.Content = nNoteId;
                                TextEntry.Document.Blocks.Clear();
                                TextEntry.Document.Blocks.Add(new Paragraph(new Run(content)));
                            }

                            e.Handled = true; 
                            break;
                        }
                }
            }
        }
    }
}
