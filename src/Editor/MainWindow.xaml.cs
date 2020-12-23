// =============================================== //
//		    © 2020 - All rights reserved.          // 
// =============================================== //

using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Editor
{
    public partial class MainWindow : Window
    {
        static string DialogFilters = "All files (*.*)|*.*|Markdown file (*.md)|*.md|Text file (*.txt)|*.txt";

        public MainWindow()
        {
            InitializeComponent();

            Markdown.Visibility = Visibility.Hidden;
            TextEntry.BringIntoView();

            MaxWidth   = SystemParameters.WorkArea.Width;
            MaxHeight  = SystemParameters.WorkArea.Height;
            ResizeMode = ResizeMode.NoResize;

            Markdown.LoadCompleted       += delegate { Markdown.InvokeScript("execScript", new object[] { "document.body.style.overflow ='hidden'", "JavaScript" }); };
            TitleBar.MouseLeftButtonDown += delegate { DragMove(); };
            Minimize.MouseLeftButtonDown += delegate { WindowState = WindowState.Minimized; };
            ExitAppw.MouseLeftButtonDown += delegate
            {
                MessageBoxResult res = MessageBox.Show(
                    "Are you sure you wanna quit?",
                    "Confirm",
                    MessageBoxButton.YesNoCancel);

                if (res == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            };
        }

        /**
         * Function: RenderPotentialNoteName
         *
         * Renders the potential note name.
         *
         * Author: wellinthatcase.
         *
         * Date: 12/22/2020.
         *
         * Param:
         * sender -  Source of the event.
         * e -       Routed event information.
         */

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

        /**
         * Function: BringTitleBarToTop
         *
         * Brings the title bar to the top to prevent grid overlapping.
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         *
         * Param:
         * sender -  Source of the event.
         * e -       Routed event information.
         */

        private void BringTitleBarToTop(object sender, RoutedEventArgs e)
        {
            TitleBar.BringIntoView();
        }

        /**
         * Function: Keybinder
         *
         * Handles common keybinds for the Editor, such as save & open files.
         *
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         *
         * Param:
         * sender -  Source of the event.
         * e -       Key event information.
         */

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

                    case Key.P:
                        {
                            if (!Markdown.IsVisible)
                            {
                                TextRange rng = new TextRange(
                                    TextEntry.Document.ContentStart,
                                    TextEntry.Document.ContentEnd);

                                Markdown.Focus(); 
                                Markdown.BringIntoView();
                                Markdown.Visibility = Visibility.Visible;
                                Markdown.NavigateToString(Markdig.Markdown.ToHtml(rng.Text));
                            }
                            else
                            {
                                TextEntry.BringIntoView();
                                Markdown.Visibility = Visibility.Hidden;
                            }

                            e.Handled = true; 
                            break; 
                        }
                }
            }
        }
    }
}
