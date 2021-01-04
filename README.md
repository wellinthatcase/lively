# Lively
In the future, this will be a modern Notepad for Windows. 

# Intentions
Lively intends on being a plug & play replacement for the default Notepad application on Windows. 
The only additional functionality intended is the ability to edit content font (inc. size, color), and support of toggable live WYSIWYG markdown. 
 
# Using Lively
In the main editor, Lively has good support of rich formatting. Most notably, CTRL+B to enable bold text, and CTRL+I to use italic. 
Alternatively, you can press CTRL+P to display your text in full HTML Markdown in an embedded browser. 
As of now, this browser has no style. However, the markdown does work to full extent. 

# What's it look like so far? 
It's still quite base, so stay weary. 

- Here's an [image](https://ibb.co/6HGrd8L) as of 12/6/20  (US)
- Here's an [image](https://ibb.co/g34Rtn9) as of 12/9/20  (US)
- Here's an [image](https://ibb.co/pncbnwD) as of 12/19/20 (US)
- Here's an [image](https://ibb.co/4f9B9N6) as of 12/20/20 (US) (EDITOR)

# Platform support
As of now, Lively is only supported on Windows 8+ by default. On other versions, you may be able to manually install .NET Framework 4.8 or compatible versions.
In the future, cross-platform to even mobile may be implemented with the recent release of .NET MPAUI.

# When is release? 
Until Lively is completed, the binaries are private. Early 2021 fits as a good time to expect a release.

# Dependencies
The only dependency that may need to be installed is the most recent Microsoft Edge runtime. 
When the markdown viewer becomes an optional module, this depenedency will be optional with that.

# Known issues
- The top-right gradient is hardly visible under bright lighting conditions in both the splash-screen & editor. 
- In the editor, the first line of the RTB word-wraps prematurely. 
- In the Markdown WebView2, you must click the box for style & html to appear. 
- In the Markdown WebView2, dots for ordered lists, among others, are invisible. 
- Saved/unsaved notification works inconsistantly with the 'save as' context menu dialog, but works fine with keybinder. 
- Automatic character ellipsis on note name works oddly. On long text, it doesn't work. On short text, it does, but too early. 

# Other
- If Lively is fully protected by copyright, how come it's open source?
   - Two reasons:
     - I intend on building some portfolio. 
     - GitHub serves as a platform to easily report issues & make suggestions.
