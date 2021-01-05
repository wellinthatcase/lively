// =============================================== //
//		    © 2020 - All rights reserved.          // 
// =============================================== //

using System;
using Markdig;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using StaticExtensions;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;

namespace Editor
{
    // wf; WinForm
    using wfFont = System.Drawing.Font;
    using wfColor = System.Drawing.Color;
    using wfBitmap = System.Drawing.Bitmap;
    using wfGfxUnit = System.Drawing.GraphicsUnit;
    using wfGraphics = System.Drawing.Graphics;
    using wfFontStyle = System.Drawing.FontStyle;
    using wfFontFamily = System.Drawing.FontFamily;
    using wfFontDialog  = System.Windows.Forms.FontDialog;
    using wfDialogResult = System.Windows.Forms.DialogResult;

    public partial class MainWindow : Window
    {
        private static bool WebView2Active = true;
        private Dictionary<string, string> Metadata = new Dictionary<string, string>(4);
        private MarkdownPipeline pipe;

        /**
         * Function: HideEditor
         *
         * Hides the editor.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020.
         */

        private void HideEditor()
        {
            MarkdownW.BringIntoView();
            TextEntry.Visibility = Visibility.Hidden;
        }

        /**
         * Function: HideMDView
         *
         * Hides the md view.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         */

        private void HideMDView()
        {
            TextEntry.BringIntoView();
            ToggleMD.IsChecked = false;
            MarkdownW.Visibility = Visibility.Hidden;
        }

        /**
         * Function: ShowEditor
         *
         * Shows the editor.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         */

        private void ShowEditor()
        {
            HideMDView();
            TextEntry.Visibility = Visibility.Visible;
            TextEntry.Focus();
        }

        /**
         * Function: ShowMDView
         *
         * Shows the md view.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         */

        private void ShowMDView()
        {
            HideEditor();
            ToggleMD.IsChecked = true; 
            MarkdownW.Visibility = Visibility.Visible;
            MarkdownW.Focus();
        }

        /**
         * Function: ShowUnsve
         *
         * Shows the unsaved notification.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         */

        private void ShowUnsve() => UnsavedNotification.Visibility = Visibility.Visible;

        /**
         * Function: HideUnsve
         *
         * Hides the unsaved notification.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         */

        private void HideUnsve() => UnsavedNotification.Visibility = Visibility.Hidden;

        /**
         * Function: IsNoteSaved
         *
         * Determines if the current note is saved.
         *
         * Author: wellinthatcase
         *
         * Date: 12/27/2020
         *
         * Returns: True if the note is saved, false if not.
         */

        private bool IsNoteSaved() => !UnsavedNotification.IsVisible;

        /**
         * Function: StartMDView
         *
         * Starts md view. This pre-loads boilerplate HTML & CSS into the WebView2. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         */

        private async void StartMDView()
        {
            if (!UnloadedWv2Check())
                return;

            await MarkdownW.EnsureCoreWebView2Async();

            string cssString = ":not(h1,h2,h3,h4,h5,h6) { font-size: 11px; } :not(br) { overflow: hidden visible; background-color: rgb(15, 2, 2); color: rgb(242, 238, 229); font-family: Segoe UI,Frutiger,Frutiger Linotype,Dejavu Sans,Helvetica Neue,Arial,sans-serif; margin-top: 1.5px; margin-bottom: 3px; }";
            string htContent = string.Format("<!DOCTYPE html><html><head><link rel='stylesheet' type='text/css'"
                                    + "href='data:text/css;charset=UTF-8,{0}'></head><body></body></html>",
                                    Uri.EscapeUriString(cssString));

            MarkdownW.NavigateToString(htContent);
        }

        /**
         * Function: FocusNoteName
         *
         * May the name decieve, but this just scrolls to the start of the note name.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         */

        private void FocusNoteName() => NoteName.ScrollToLine(0);

        /**
         * Function: BufferEditor
         *
         * Exlusively used to edit the buffer defined in MeasureNoteName. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/27/2020
         *
         * Parameters:
         * str -  The string.
         *
         * Returns: A bool indicating if str was successfully appended to the buffer.
         */

        private bool EditBuffer(ref string buff, string str, int maxLen, wfFont font)
        {
            int totalSegmentLength = str.Length + buff.Length;
            int totalContentLength = maxLen - "...".WidthInPixels(font);

            return totalSegmentLength < totalContentLength ? (buff += str).Length > 0 : false;
        }

        /**
         * Function: MeasureNoteName
         *
         * Measures the proposed text's length to see if it would fit as a note name. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/27/2020
         *
         * Parameters:
         * text -  The text.
         *
         * Returns: The trunacated string to fit in the note name text box, if required.
         */

        private string MeasureNoteName(string text)
        {
            wfFont font = new wfFont(
                TextEntry.FontFamily.ToString(),
                (float)TextEntry.FontSize,
                wfFontStyle.Regular,
                wfGfxUnit.Point);

            int pxMaxContentLen = (int)NoteName.ActualWidth;

            if (text.WidthInPixels(font) >= pxMaxContentLen)
            {
                int chunkSize = 3;
                string buffer = string.Empty;

                IEnumerable<string> segments = Enumerable.Range(0, text.Length / chunkSize)
                    .Select(index => text.Substring(index * chunkSize, chunkSize))
                    .TakeWhile(st => EditBuffer(ref buffer, st, pxMaxContentLen, font))
                    .ToArray();

                text = string.Join(string.Empty, segments) + "...";
            }

            return text;
        }

        /**
         * Function: AlertUnloaded
         *
         * Alerts the user that WebView2 has been unloaded.
         *
         * Author: wellinthatcase
         *
         * Date: 12/27/2020
         *
         * Returns: false
         */

        private static bool AlertUnloaded()
        {
            MessageBox.Show("WebView2 features are locked after you unload WebView2.", "Locked");
            return false;
        }

        /**
         * Function: UnloadedWv2Check
         *
         * Determines if WebView2 is activated/loaded, if not, alert the user.
         *
         * Author: wellinthatcase
         *
         * Date: 12/27/2020
         *
         * Returns: True if it succeeds, false if it fails.
         */

        private bool UnloadedWv2Check() => WebView2Active ? true : AlertUnloaded();

        /**
         * Function: ExecScript
         *
         * Executes a JS script inside the WebView2 embedded browser.
         *
         * Author: wellinthatcase
         *
         * Date: 12/28/2020
         *
         * Parameters:
         * code -  The code.
         *
         * Returns: True if it succeeds, false if it fails.
         */
         
        private bool ExecScript(string code) => MarkdownW.ExecuteScriptAsync(code).IsCompleted;

        /**
         * Function: RunMDScript
         *
         * Safely runs a JS script inside of the WebView2 embedded browser.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * code -  The code.
         */

        private bool RunMDScript(string code) => UnloadedWv2Check() ? ExecScript(code) : false;

        /**
         * Function: MainWindow
         *
         * Default constructor. Besides this, it also launches WebView2,
         * sets the max window margins, resize mode, metadata key defaults, and markdown pipe configuration.
         *
         * Author: wellinthatcase
         *
         * Date: 12/27/2020
         */

        public MainWindow()
        {
            InitializeComponent();

            MaxWidth = SystemParameters.WorkArea.Width;
            MaxHeight = SystemParameters.WorkArea.Height;
            ResizeMode = ResizeMode.NoResize;

            HideMDView();
            StartMDView();

            Metadata.Add("LastSavedCntnt", string.Empty);
            Metadata.Add("AlreadyRunFile", string.Empty);
            Metadata.Add("ActualNoteName", string.Empty);
            Metadata.Add("DialogFilters", "Lively Markdown file (*.lmd)|*.lmd|Text file (*.txt)|*.txt|Markdown file (*.md)|*.md|All files (*.*)|*.*");

            pipe = new MarkdownPipelineBuilder()
                .ConfigureNewLine("<br />")
                .UseAdvancedExtensions()
                .UseEmojiAndSmiley()
                .Build();
        }

        /**
         * Function: DragWindow
         *
         * Control: Exitappw
         * 
         * Event: MouseLeftButtonDown
         * 
         * Adds drag support to the title bar. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/27/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Mouse button event information.
         */

        private void DragWindow(object sender, MouseButtonEventArgs e) => DragMove();

        /**
         * Function: MinimizeApplication
         *
         * Control: Minimize
         * 
         * Event: MouseLeftButtonDown
         * 
         * Handles actual minimization for the minimize button.
         *
         * Author: wellinthatcase
         *
         * Date: 12/27/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Mouse button event information.
         */

        private void MinimizeApplication(object sender, MouseButtonEventArgs e) => WindowState = WindowState.Minimized;

        /**
         * Function: ExitApplication
         *
         * Control: ExitAppw
         * 
         * Event: MouseLeftButtonDown
         * 
         * Exit the application.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Mouse button event information.
         */

        private void ExitApplication(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoteSaved())
            {
                MessageBoxResult res = MessageBox.Show(
                    "Are you sure you wanna exit?",
                    "Confirm",
                    MessageBoxButton.YesNoCancel);

                if (res == MessageBoxResult.Yes)
                    Application.Current.Shutdown();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        /**
         * Function: RenderPotentialNoteName
         *
         * Control: NoteName
         * 
         * Event: Loaded
         * 
         * Renders the potential note name. Both from the Splash-Screen & Windows. 
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
                Metadata["ActualNoteName"] = lpCmdLineArgs[1];
                Metadata["AlreadyRunFile"] = lpCmdLineArgs[1];

                if (File.Exists(Metadata["AlreadyRunFile"]))
                {
                    Lmd.Process(Metadata["AlreadyRunFile"], ref TextEntry);
                    HideUnsve();
                }
            }
            catch (IndexOutOfRangeException)
            {
                Metadata["ActualNoteName"] = "Untitled note";
            }

            NoteName.Text = MeasureNoteName(Path.GetFileNameWithoutExtension(Metadata["ActualNoteName"]));
        }

        /**
         * Function: SaveAsExtern
         *
         * Serves as 'save as' functionality. See the function documentation below this one. 
         * 
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * file -      The file.
         * content -   The content.
         *
         * Returns: True if it succeeds, false if it fails.
         */

        private string SaveAsExtern(string file, string content)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = Metadata["DialogFilters"],
                FileName = Metadata["ActualNoteName"],
                AddExtension = true,
                ValidateNames = true,
                OverwritePrompt = true
            };

            if (dialog.ShowDialog() == true)
            {
                file = dialog.FileName;

                using (FileStream stream = File.OpenWrite(file))
                {
                    content = Lmd.Embed(file, content, TextEntry);

                    byte[] encodedContent = Encoding.UTF8.GetBytes(content);
                    int    encodedMaxByte = Encoding.UTF8.GetByteCount(content);
                    int    encodedOffset  = 0;

                    stream.Write(encodedContent, encodedOffset, encodedMaxByte);
                }
            }

            return file; 
        }

        /**
         * Function: Save
         *
         * If the note corresponds to a file, then this saves the content into the file.
         * This serves much more as 'save' than 'save as' functionality.
         * 
         * If there is no file to correspond with, then 'save as' functionality is invoked. 
         * 
         * In addition to saving the note, we also may inject LMD, or 'Lively metadata'
         * into the beginning of the file. 
         * 
         * This sort of functionality is only supported among files with the '.lmd' extension.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * file -      The file.
         * content -   The content.
         *
         * Returns: True if it succeeds, false if it fails.
         */

        private string Save(string file, string content)
        {
            if (file != string.Empty)
            {
                File.WriteAllText(file, Lmd.Embed(file, content, TextEntry));
                return file; 
            }
            else
            {
                return SaveAsExtern(file, content);
            }
        }

        /**
         * Function: KeybinderSave
         *
         * Opens & processes the save file dialog for the keybinder. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * e -  Key event information.
         */

        private string KeybinderSave()
        {
            string fileId = Metadata["AlreadyRunFile"];
            TextRange rng = new TextRange(TextEntry.Document.ContentStart, TextEntry.Document.ContentEnd);

            return Save(fileId, rng.Text);
        }

        /**
         * Function: KeybinderOpen
         *
         * Opens & processes the open file dialog for the keybinder. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         */

        private string KeybinderOpen()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = Metadata["DialogFilters"]
            };

            if (dialog.ShowDialog() == true)
            {
                string filName = dialog.FileName;
                string content = Lmd.Process(filName, ref TextEntry);
                string nNoteId = Path.GetFileNameWithoutExtension(filName);

                Metadata["ActualNoteName"] = nNoteId;
                NoteName.Text = MeasureNoteName(nNoteId);

                return filName; 
            }

            return string.Empty; 
        }

        /**
         * Function: KeybinderView
         *
         * Opens & handles the markdown viewer for the keybinder. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * e -  Key event information.
         */

        private void KeybinderView()
        {
            if (!UnloadedWv2Check())
                return; 

            if (!MarkdownW.IsVisible)
            {
                TextRange rng = new TextRange(
                    TextEntry.Document.ContentStart,
                    TextEntry.Document.ContentEnd);

                string jsScript = string.Format(
                    "document.querySelector('body').innerHTML = '{0}';",
                    Markdown.ToHtml(rng.Text, pipe));

                RunMDScript(jsScript);
                ShowMDView();
            }
            else
            {
                ShowEditor();
            }
        }

        /**
         * Function: CallKeybinder
         *
         * Handles main keybinder logic.
         * The main reason for implementing this is to allow context menu
         * invoked functions to perform EXACTLY the same as keybinds. 
         * 
         * The `open` button on the context menu for example, it simulates a CTRL+O press
         * by passing Key.O into this function. 
         * 
         * The previous function in the method resolution is the Keybinder, which 
         * handles when to declare an event finished, and also barrs entry to only keybinds. 
         * 
         * To summarize, unlike Keybinder alone, CallKeybinder allows context menu invocations
         * & Keybinder invocations to flow through.
         *
         * Author: wellinthatcase
         *
         * Date: 12/26/2020
         *
         * Parameters:
         * simulatedKey -  The simulated key.
         *
         * Returns: True if it succeeds, false if it fails.
         */

        private bool CallKeybinder(Key key)
        {
            TextRange range = new TextRange(TextEntry.Document.ContentStart, TextEntry.Document.ContentEnd);
            
            bool passed = false; 
            string text = range.Text;

            switch (key)
            {
                case Key.S:
                    {
                        string call = KeybinderSave();
                        passed = call != string.Empty;

                        if (passed)
                        {
                            Metadata["AlreadyRunFile"] = call;
                            Metadata["LastSavedCntnt"] = text;
                            HideUnsve();
                        }

                        break;
                    }

                case Key.O:
                    {
                        string call = KeybinderOpen();
                        passed = call != string.Empty;

                        if (passed)
                        {
                            Metadata["AlreadyRunFile"] = call;
                            Metadata["LastSavedCntnt"] = text;
                            HideUnsve();
                        }

                        break;
                    }

                case Key.P:
                    {
                        KeybinderView();

                        passed = true; 
                        break;
                    }
            }

            return passed; 
        }

        /**
         * Function: Keybinder
         *
         * Control: TextEntry
         * 
         * Event: KeyDown
         * 
         * Handles common keybinds for the Editor, such as save & open files.
         * On the side, this also handles "save" icon notification logic.
         *
         * The reason we can't call this for context menu functions is due to
         * the type of the parameters. So, only keyboard-invoked keybinds flow through this gate.
         * 
         * Author: wellinthatcase
         *
         * Date: 12/22/2020
         *
         * Param:
         * sender -  Source of the event.
         * e -       Key event information.
         */

        private void Keybinder(object sender, KeyEventArgs e) => e.Handled = Keyboard.IsKeyDown(Key.LeftCtrl) && CallKeybinder(e.Key);

        /**
         * Function: ShowTitlebar
         *
         * Control: Editor
         * 
         * Event: Loaded
         * 
         * Shows the titlebar. Prevents a Z-Index overlap.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Routed event information.
         */

        private void ShowTitlebar(object sender, RoutedEventArgs e) => TitleBar.BringIntoView();

        /**
         * Function: DodgePrintPrompt
         *
         * Control: MarkdownW
         * 
         * Event: KeyDown
         * 
         * Prevents the print prompt from displaying when CTRL+P is pressed on the markdown viewer.
         *
         * Author: wellinthatcase
         *
         * Date: 12/23/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Key event information.
         */

        private void DodgePrintPrompt(object sender, KeyEventArgs e) => Keybinder(sender, e);

        /**
         * Function: ShortenOversizedNames
         *
         * Control: NoteName
         * 
         * Event: LostFocus
         * 
         * Shorten oversized note names when the text box loses focus.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Routed event information.
         */

        private void ShortenOversizedNames(object sender, RoutedEventArgs e)
        {
            FocusNoteName();
            Metadata["ActualNoteName"] = !string.IsNullOrEmpty(NoteName.Text) ? NoteName.Text : "Untitled note";
            NoteName.Text = MeasureNoteName(Metadata["ActualNoteName"]);
        }

        /**
         * Function: ShowActualNoteName
         *
         * Control: NoteName
         * 
         * Event: GotFocus
         * 
         * Shows the actual, un-shortened note name. On the side, this also scrolls to the start of the text box,
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Routed event information.
         */

        private void ShowActualNoteName(object sender, RoutedEventArgs e)
        {
            FocusNoteName();
            NoteName.Text = Metadata["ActualNoteName"];
        }

        /**
         * Function: OpenCtxDialog
         *
         * Control: Logo
         * 
         * Event: Click
         * 
         * Context menu button to proxy invoke KeybinderOpen.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Mouse button event information.
         */

        private void OpenCtxDialog(object sender, RoutedEventArgs e) => e.Handled = CallKeybinder(Key.O);

        /**
         * Function: SaveCtxDialog
         *
         * Control: Logo
         * 
         * Event: Click
         * 
         * Context menu button to proxy invoke KeybinderSave.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Mouse button event information.
         */

        private void SaveCtxDialog(object sender, RoutedEventArgs e) => e.Handled = CallKeybinder(Key.S);

        /**
         * Function: ViewCtxDialog
         *
         * Control: Logo
         * 
         * Event: Click
         * 
         * Context menu button to proxy invoke KeybinderView.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Mouse button event information.
         */

        private void ViewCtxDialog(object sender, RoutedEventArgs e) => e.Handled = CallKeybinder(Key.P);

        /**
         * Function: SaveAsExternCtxDialog
         *
         * Control: Logo
         * 
         * Event: Click
         * 
         * Directly invokes the 'save as' functionality. 
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Routed event information.
         */

        private void SaveAsExternCtxDialog(object sender, RoutedEventArgs e)
        {
            TextRange range = new TextRange(TextEntry.Document.ContentStart, TextEntry.Document.ContentEnd);

            SaveAsExtern(Metadata["AlreadyRunFile"], range.Text);
        }

        /**
         * Function: ToggleSpellCheck
         *
         * Control: Logo
         * 
         * Event: Click
         * 
         * Toggle the spell checker.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Routed event information.
         */

        private void ToggleSpellCheck(object sender, RoutedEventArgs e) => TextEntry.SpellCheck.IsEnabled = !TextEntry.SpellCheck.IsEnabled;

        /**
         * Function: UnloadWebView2
         *
         * Control: Logo
         * 
         * Event: Click
         * 
         * Unload WebView2. Slices memory usage in half.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Routed event information.
         */

        private void UnloadWebView2(object sender, RoutedEventArgs e)
        {
            MarkdownW.Dispose();
            WebView2Active = false; 
        }

        /**
         * Function: DetectAlteration
         *
         * Control: TextEntry
         * 
         * Event: TextChanged
         * 
         * Displays a notfication beside the note name to let the user know that
         * the current text is not yet saved.
         *
         * Author: wellinthatcase
         *
         * Date: 12/25/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Text composition event information.
         */

        private void DetectAlteration(object sender, TextChangedEventArgs e)
        {
            if (UnsavedNotification != null)
                UnsavedNotification.Visibility = Visibility.Visible;
        }

        /**
         * Function: ShowFontDialog
         *
         * Control: Logo
         * 
         * Event: Click
         * 
         * Shows the font dialog from the context menu.
         *
         * Author: wellinthatcase
         *
         * Date: 12/27/2020
         *
         * Parameters:
         * sender -    Source of the event.
         * e -         Routed event information.
         */

        private void ShowFontDialog(object sender, RoutedEventArgs e)
        {
            float curFontSize = (float)TextEntry.FontSize;

            Color        oldColor  = TextEntry.Foreground.ToString().AsSolidColorBrush().Color;
            wfColor      curColor  = wfColor.FromArgb(oldColor.A, oldColor.R, oldColor.G, oldColor.B);
            wfFontFamily curFamily = new wfFontFamily(TextEntry.FontFamily.ToString());
            wfFont       curFont   = new wfFont(curFamily, curFontSize);

            wfFontDialog dialog = new wfFontDialog
            {
                Font = curFont,
                Color = curColor,
                ShowColor = true,
            };

            if (dialog.ShowDialog() != wfDialogResult.Cancel)
            {
                byte clrAlp = dialog.Color.A;
                byte clrRed = dialog.Color.R;
                byte clrGrn = dialog.Color.G;
                byte clrBlu = dialog.Color.B;

                TextEntry.Foreground = new SolidColorBrush(Color.FromArgb(clrAlp, clrRed, clrGrn, clrBlu));
                TextEntry.FontFamily = dialog.Font.Name.IntoFontFamily();
                TextEntry.FontWeight = dialog.Font.Bold   ? FontWeights.Bold  : FontWeights.Normal;
                TextEntry.FontStyle  = dialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                TextEntry.FontSize   = dialog.Font.Size;
            }
        }
    }
}
