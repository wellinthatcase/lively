// =============================================== //
//		    © 2020 - All rights reserved.          // 
// =============================================== //

using System;
using System.IO;
using System.Text;
using System.Linq;
using StaticExtensions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

/**
 * Class: Lmd
 *
 * Basic structure & operations related to LMD, or 'Lively metadata'. This class cannot be inherited.
 *
 * Author: wellinthatcase
 *
 * Date: 12/31/2020
 */

[DataContract]
public sealed class Lmd
{
    /**
     * Property: Size
     *
     * The font-size of the text.
     *
     * Returns: The size.
     */

    [DataMember(Name = "fsze")] public string Size   { get; set; }

    /**
     * Property: Style
     *
     * The font-style of the text.
     *
     * Returns: The style.
     */

    [DataMember(Name = "fstl")] public string Style  { get; set; }

    /**
     * Property: Color
     *
     * The font-color of the text.
     *
     * Returns: The color.
     */

    [DataMember(Name = "fclr")] public string Color  { get; set; }

    /**
     * Property: Family
     *
     * The font-family of the text.
     *
     * Returns: The family.
     */

    [DataMember(Name = "ffam")] public string Family { get; set; }

    /**
     * Property: Weight
     *
     * The font-weight of the text.
     *
     * Returns: The weight.
     */

    [DataMember(Name = "fwgh")] public string Weight { get; set; }

    /**
     * Function: IsLmd
     *
     * Queryies of alledgedLmd is indeed a string of LMD. 
     *
     * Author: wellinthatcase
     *
     * Date: 12/31/2020
     *
     * Parameters:
     * alledgedLmd -  The alledged lmd.
     *
     * Returns: True if lmd, false if not.
     */

    public static bool IsLmd(string alledgedLmd)
    {
        return alledgedLmd.StartsWith("{ \"fsze\":");
    }

    /**
     * Function: IsLmdFile
     *
     * Query if 'filePath' is an LMD file.
     *
     * Author: wellinthatcase
     *
     * Date: 12/31/2020
     *
     * Parameters:
     * filePath -  Full pathname of the file.
     *
     * Returns: True if lmd file, false if not.
     */

    public static bool IsLmdFile(string filePath)
    {
        return Path.GetExtension(filePath) == ".lmd";
    }

    /**
     * Function: Parse
     *
     * Parses a generic string into LMD. 
     *
     * Author: wellinthatcase
     *
     * Date: 12/29/2020
     *
     * Parameters:
     * s -  A MemoryStream to process.
     *
     * Returns: Lively metadata
     */

    public static Lmd Parse(string content)
    {
        using (MemoryStream mockStream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
        {
            return new DataContractJsonSerializer(typeof(Lmd)).ReadObject(mockStream) as Lmd;
        }
    }

    /**
     * Function: ProcessStyles
     *
     * Processes the LMD styles into the note text editor. 
     *
     * Author: wellinthatcase
     *
     * Date: 12/31/2020
     *
     * Parameters:
     * TextEntry -  [in,out] The text entry.
     */

    public void ProcessStyles(ref RichTextBox TextEntry)
    {
        TextEntry.FontSize   = Size.IntoFontSize();
        TextEntry.FontStyle  = Style.IntoFontStyle();
        TextEntry.FontFamily = Family.IntoFontFamily();
        TextEntry.FontWeight = Weight.IntoFontWeight();
        TextEntry.Foreground = Color.AsSolidColorBrush();
    }

    /**
     * Function: ParseFromFile
     *
     * Extracts LMD from the file described by filePath.
     *
     * Author: wellinthatcase
     *
     * Date: 12/31/2020
     *
     * Parameters:
     * filePath -  Full pathname of the file.
     *
     * Returns: The LMD generated from the file, or null.
     */

    public static Lmd ParseFromFile(string filePath)
    {
        string[] content = File.ReadAllLines(filePath);

        if (content.Length > 0)
        {
            if (IsLmd(content[0]))
            {
                return Parse(content[0].Replace(Environment.NewLine, string.Empty));
            }
        }

        return null;
    }

    /**
     * Function: StripLmdFromFile
     *
     * Strips LMD from the beginning of the file described by filePath.
     *
     * Author: wellinthatcase
     *
     * Date: 12/31/2020
     *
     * Parameters:
     * filePath -  Full pathname of the file.
     *
     * Returns: The file contents without the LMD.
     */

    public static string StripLmdFromFile(string filePath)
    {
        string[] content = File.ReadAllLines(filePath);

        if (content.Length > 0)
        {
            if (IsLmd(content[0]))
            {
                content = content.Skip(1).ToArray();
            }
        }

        return string.Join(Environment.NewLine, content);
    }

    /**
     * Function: Embed
     *
     * Embeds LMD into the passed content. Typically for the content to
     * be inserted into the file described by filePath. 
     *
     * Author: wellinthatcase
     *
     * Date: 12/31/2020
     *
     * Parameters:
     * filePath -  Full pathname of the file.
     * content -   The content.
     *
     * Returns: The content with embedded LMD. 
     */

    public static string Embed(string filePath, string content, RichTextBox TextEntry)
    {
        if (!IsLmdFile(filePath))
            return content;

        string lmd = string.Format("{{ \"fsze\": \"{0}\", \"fstl\": \"{1}\", \"ffam\": \"{2}\", \"fclr\": \"{3}\", \"fwgh\": \"{4}\" }}",
            TextEntry.FontSize.ToString(),
            TextEntry.FontStyle.ToString(),
            TextEntry.FontFamily.ToString(),
            TextEntry.Foreground.ToString(),
            TextEntry.FontWeight.ToString()
        );

        return string.Format("{0}{1}{2}", lmd, Environment.NewLine, content);
    }

    /**
     * Function: FullProcess
     *
     * Parses LMD from the described filePath, then modifies the supplied TextEntry with the attained LMD. 
     *
     * Author: wellinthatcase
     *
     * Date: 12/31/2020
     *
     * Parameters:
     * filePath -  Full pathname of the file.
     */

    public static string Process(string filePath, ref RichTextBox TextEntry)
    {
        string text = StripLmdFromFile(filePath);

        TextEntry.Document.Blocks.Clear();
        TextEntry.Document.Blocks.Add(new Paragraph(new Run(text)));

        Lmd possibleLmd = ParseFromFile(filePath);
        if (possibleLmd is Lmd)
            possibleLmd.ProcessStyles(ref TextEntry);

        return text;
    }
}
