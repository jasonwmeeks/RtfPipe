using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RtfPipe.Tests
{
  /// <summary>
  /// Additional examples in https://github.com/phprtflite/PHPRtfLite/tree/master/samples/generated
  /// </summary>
  [TestClass]
  public class ParseRender
  {
    //[TestMethod]
    //public void Prototype()
    //{
    //  TestParse("RtfPipe.Tests.Files.Hyperlink");
    //}

    /// <summary>
    /// Many examples are taken from https://github.com/paulhtremblay/rtf2xml/tree/master/test/test_files/good
    /// </summary>
    [TestMethod]
    public void RtfToHtml()
    {
      TestConvert("RtfPipe.Tests.Files.rtf2xml.bullet_list");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.caps_mixed");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.char_style");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.char_upper_ranges");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.color");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.diff_types_border");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.escaped_text");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.heading_with_section");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.headings_mixed");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.Hyperlink");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.inline_mix");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.inline_over_para");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.italics_plain");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.list_in_table");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.lists_with_breaks");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.nested_lists_indents");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.outline_list");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.simplest");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.table_different_cell_widths");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.table_empty_row");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.table_simple");
      TestConvert("RtfPipe.Tests.Files.rtf2xml.table_with_header");

      TestConvert("RtfPipe.Tests.Files.phprtflite.nested_tables");

      TestConvert("RtfPipe.Tests.Files.Test01");
      TestConvert("RtfPipe.Tests.Files.minimal");
      TestConvert("RtfPipe.Tests.Files.nested_table_report");
      TestConvert("RtfPipe.Tests.Files.RtfParserTest_0");
      TestConvert("RtfPipe.Tests.Files.RtfParserTest_1");
      TestConvert("RtfPipe.Tests.Files.RtfParserTest_2");
      TestConvert("RtfPipe.Tests.Files.RtfParserTest_3");
    }

    private void TestParse(string path)
    {
      using (var stream = File.Exists(path)
        ? new FileStream(path, FileMode.Open)
        : Assembly.GetExecutingAssembly().GetManifestResourceStream(path + ".rtf"))
      {
        var actual = Rtf.ToHtml(stream);
        Assert.Fail();
      }
    }

    private void TestConvert(string path)
    {
      using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path + ".rtf"))
      using (var expectedReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(path + ".html")))
      {
        var actual = Rtf.ToHtml(stream);
        var expected = expectedReader.ReadToEnd();
        Assert.AreEqual(expected, actual);
      }
    }

    [TestMethod]
    public void StreamReader()
    {
      var expected = default(string);
      var actual = default(string);

      using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RtfPipe.Tests.Files.Test01.rtf"))
      using (var reader = new StreamReader(stream))
      {
        expected = reader.ReadToEnd();
      }

      using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RtfPipe.Tests.Files.Test01.rtf"))
      using (var reader = new RtfStreamReader(stream))
      {
        actual = reader.ReadToEnd();
      }

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EncapsulatedHtml()
    {
      // FROM: https://msdn.microsoft.com/en-us/library/ee204432(v=exchg.80).aspx
      var rtf = @"{\rtf1\ANSI\ansicpg1251\fromhtml1 \deff0
{\fonttbl {\f0\fmodern Courier New;}{\f1\fswiss Arial;}{\f2\fswiss\fcharset0 Arial;}}
{\colortbl\red0\green0\blue0;\red0\green0\blue255;} 
{\*\htmltag64}
\uc1\pard\plain\deftab360 \f0\fs24
{\*\htmltag <HTML><head>\par
<style>\par
<!--\par
 /* Style Definitions */\par
 p.MsoNormal, li.MsoNormal \{font-family:Arial;\}\par
-->\par
</style>\par
\tab <!-- This is a HTML comment.\par
There is a horizontal tab (%x09) character before the comment, \par
and some new lines inside the comment. -->\par
</head>\par
<body>\par
<p\par
class=""MsoNormal"">}
{\htmlrtf \f1 \htmlrtf0 Note the line break inside a P tag. {\*\htmltag <b>}{\htmlrtf \b \htmlrtf0 This is a bold text{\*\htmltag </b>}} \htmlrtf\par\htmlrtf0}
\htmlrtf \par \htmlrtf0
{\*\htmltag </p>\par
<p class=""MsoNormal"">\par}
{\htmlrtf \f1 \htmlrtf0 This is a normal text with a character references:
{\*\htmltag &nbsp;}\htmlrtf \'a0\htmlrtf0  {\*\htmltag &lt;}\htmlrtf <\htmlrtf0  {\*\htmltag &uml;}\htmlrtf {\f2\'a8}\htmlrtf0{\*\htmltag <br>\par}\htmlrtf\line\htmlrtf0
characters which have special meaning in RTF: \{\}\\{\*\htmltag <br>\par}\htmlrtf\line\htmlrtf0\htmlrtf\par\htmlrtf0}
{\*\htmltag </p>\par
<ol>\par
    <li class=""MsoNormal"">}{\htmlrtf {{\*\pn\pnlvlbody\pndec\pnstart1\pnindent360{\pntxta.}}\li360\fi-360{\pntext 1.\tab} \f1 \htmlrtf0 This is a list item}\htmlrtf\par\htmlrtf0}
{\*\htmltag \par
</ol>\par
</body>\par
</HTML>\par }}";
      var html = Rtf.ToHtml(rtf);
      var expected = @" <HTML><head>
<style>
<!--
 /* Style Definitions */
 p.MsoNormal, li.MsoNormal {font-family:Arial;}
-->
</style>
<!-- This is a HTML comment.
There is a horizontal tab (%x09) character before the comment, 
and some new lines inside the comment. -->
</head>
<body>
<p
class=""MsoNormal"">Note the line break inside a P tag. <b>This is a bold text</b> </p>
<p class=""MsoNormal"">
This is a normal text with a character references:&nbsp; &lt; &uml;<br>
characters which have special meaning in RTF: {}\<br>
</p>
<ol>
    <li class=""MsoNormal"">This is a list item
</ol>
</body>
</HTML>
";
      Assert.AreEqual(expected, html);
    }

    [TestMethod]
    public void ImageSize()
    {
      const string rtf = @"{\rtf1\ansi\ansicpg1251\deff0\nouicompat\deflang1049{\fonttbl{\f0\fnil\fcharset0 Calibri;}}
{\*\generator Riched20 10.0.14393}\viewkind4\uc1 
\pard\sa200\sl240\slmult1\f0\fs22\lang9{\pict{\*\picprop}\wmetafile8\picw1323\pich265\picwgoal750\pichgoal150 
010009000003f60000000000cd00000000000400000003010800050000000b0200000000050000
000c020a003200030000001e0004000000070104000400000007010400cd000000410b2000cc00
0a003200000000000a0032000000000028000000320000000a0000000100040000000000000000
000000000000000000000000000000000000000000ffffff003300ff000033ff00000000000000
000000000000000000000000000000000000000000000000000000000000000000000000000000
000000222222222222222222222222222222222222222222222222220202022222222222222222
222222222222222222222222222222222202020222222222222222222222222222222222222222
222222222222020202222222222222222222222222222222222222222222222222220202022222
222222222222222222222222222222222222222222222202020222222222222222222222222222
222222222222222222222222020202222222222222222222222222222222222222222222222222
220202022222222222222222222222222222222222222222222222222202020222222222222222
222222222222222222222222222222222222020202222222222222222222222222222222222222
22222222222232020202040000002701ffff030000000000
}\par
\par
\par
{\pict{\*\picprop}\wmetafile8\picw1323\pich265\picwgoal750\pichgoal150 
0100090000037600000000004d00000000000400000003010800050000000b0200000000050000
000c020a003200030000001e00040000000701040004000000070104004d000000410b2000cc00
0a003200000000000a0032000000000028000000320000000a0000000100010000000000000000
000000000000000000000000000000000000000000ffffff00ffffffffffffc001ffffffffffff
c001ffffffffffffc001ffffffffffffc001ffffffffffffc001ffffffffffffc001ffffffffff
ffc001ffffffffffffc001ffffffffffffc001ffffffffffffc001040000002701ffff03000000
0000
}\par

\pard\sa200\sl276\slmult1\par
}";
      var html = Rtf.ToHtml(rtf);
      const string expected = "<div style=\"font-size:12pt;font-family:Calibri;\"><p style=\"font-size:11pt;margin:0 0 13.3px 0;\"><img width=\"50\" height=\"10\" src=\"data:windows/metafile;base64,AQAJAAAD9gAAAAAAzQAAAAAABAAAAAMBCAAFAAAACwIAAAAABQAAAAwCCgAyAAMAAAAeAAQAAAAHAQQABAAAAAcBBADNAAAAQQsgAMwACgAyAAAAAAAKADIAAAAAACgAAAAyAAAACgAAAAEABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA////ADMA/wAAM/8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIgICAiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiICAgIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiAgICIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIgICAiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiICAgIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiAgICIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIgICAiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiICAgIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiAgICIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiIiMgICAgQAAAAnAf//AwAAAAAA\"></p><p style=\"font-size:11pt;margin:0 0 13.3px 0;\"><br></p><p style=\"font-size:11pt;margin:0 0 13.3px 0;\"><br></p><p style=\"font-size:11pt;margin:0 0 13.3px 0;\"><img width=\"50\" height=\"10\" src=\"data:windows/metafile;base64,AQAJAAADdgAAAAAATQAAAAAABAAAAAMBCAAFAAAACwIAAAAABQAAAAwCCgAyAAMAAAAeAAQAAAAHAQQABAAAAAcBBABNAAAAQQsgAMwACgAyAAAAAAAKADIAAAAAACgAAAAyAAAACgAAAAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA////AP///////8AB////////wAH////////AAf///////8AB////////wAH////////AAf///////8AB////////wAH////////AAf///////8ABBAAAACcB//8DAAAAAAA=\"></p><p style=\"font-size:11pt;margin:0 0 13.3px 0;\"><br></p></div>";
      Assert.AreEqual(expected, html);
    }

    [TestMethod]
    public void AttachmentRender()
    {
      const string rtf = @"{\rtf1\adeflang1025\ansi\ansicpg1252\uc1\adeff31507\deff0\stshfdbch31506\stshfloch31506\stshfhich31506\stshfbi31507\deflang1033\deflangfe1033\themelang1033\themelangfe0\themelangcs0{\fonttbl{\f0\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman{\*\falt Times New Roman};}{\f34\fbidi \froman\fcharset1\fprq2{\*\panose 02040503050406030204}Cambria Math{\*\falt Calisto MT};}{\f39\fbidi \fswiss\fcharset0\fprq2{\*\panose 020f0502020204030204}Calibri{\*\falt Times New Roman};}{\f31500\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman{\*\falt Times New Roman};}{\f31501\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman{\*\falt Times New Roman};}{\f31502\fbidi \fswiss\fcharset0\fprq2{\*\panose 020f0302020204030204}Calibri Light{\*\falt Calibri};}{\f31503\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman{\*\falt Times New Roman};}{\f31504\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman{\*\falt Times New Roman};}{\f31505\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman{\*\falt Times New Roman};}{\f31506\fbidi \fswiss\fcharset0\fprq2{\*\panose 020f0502020204030204}Calibri{\*\falt Times New Roman};}{\f31507\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman{\*\falt Times New Roman};}{\f348\fbidi \froman\fcharset238\fprq2 Times New Roman CE{\*\falt Times New Roman};}{\f349\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr{\*\falt Times New Roman};}{\f351\fbidi \froman\fcharset161\fprq2 Times New Roman Greek{\*\falt Times New Roman};}{\f352\fbidi \froman\fcharset162\fprq2 Times New Roman Tur{\*\falt Times New Roman};}{\f353\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew){\*\falt Times New Roman};}{\f354\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic){\*\falt Times New Roman};}{\f355\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic{\*\falt Times New Roman};}{\f356\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese){\*\falt Times New Roman};}{\f688\fbidi \froman\fcharset238\fprq2 Cambria Math CE{\*\falt Calisto MT};}{\f689\fbidi \froman\fcharset204\fprq2 Cambria Math Cyr{\*\falt Calisto MT};}{\f691\fbidi \froman\fcharset161\fprq2 Cambria Math Greek{\*\falt Calisto MT};}{\f692\fbidi \froman\fcharset162\fprq2 Cambria Math Tur{\*\falt Calisto MT};}{\f695\fbidi \froman\fcharset186\fprq2 Cambria Math Baltic{\*\falt Calisto MT};}{\f696\fbidi \froman\fcharset163\fprq2 Cambria Math (Vietnamese){\*\falt Calisto MT};}{\f738\fbidi \fswiss\fcharset238\fprq2 Calibri CE{\*\falt Times New Roman};}{\f739\fbidi \fswiss\fcharset204\fprq2 Calibri Cyr{\*\falt Times New Roman};}{\f741\fbidi \fswiss\fcharset161\fprq2 Calibri Greek{\*\falt Times New Roman};}{\f742\fbidi \fswiss\fcharset162\fprq2 Calibri Tur{\*\falt Times New Roman};}{\f745\fbidi \fswiss\fcharset186\fprq2 Calibri Baltic{\*\falt Times New Roman};}{\f746\fbidi \fswiss\fcharset163\fprq2 Calibri (Vietnamese){\*\falt Times New Roman};}{\f31508\fbidi \froman\fcharset238\fprq2 Times New Roman CE{\*\falt Times New Roman};}{\f31509\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr{\*\falt Times New Roman};}{\f31511\fbidi \froman\fcharset161\fprq2 Times New Roman Greek{\*\falt Times New Roman};}{\f31512\fbidi \froman\fcharset162\fprq2 Times New Roman Tur{\*\falt Times New Roman};}{\f31513\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew){\*\falt Times New Roman};}{\f31514\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic){\*\falt Times New Roman};}{\f31515\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic{\*\falt Times New Roman};}{\f31516\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese){\*\falt Times New Roman};}{\f31518\fbidi \froman\fcharset238\fprq2 Times New Roman CE{\*\falt Times New Roman};}{\f31519\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr{\*\falt Times New Roman};}{\f31521\fbidi \froman\fcharset161\fprq2 Times New Roman Greek{\*\falt Times New Roman};}{\f31522\fbidi \froman\fcharset162\fprq2 Times New Roman Tur{\*\falt Times New Roman};}{\f31523\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew){\*\falt Times New Roman};}{\f31524\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic){\*\falt Times New Roman};}{\f31525\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic{\*\falt Times New Roman};}{\f31526\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese){\*\falt Times New Roman};}{\f31528\fbidi \fswiss\fcharset238\fprq2 Calibri Light CE{\*\falt Calibri};}{\f31529\fbidi \fswiss\fcharset204\fprq2 Calibri Light Cyr{\*\falt Calibri};}{\f31531\fbidi \fswiss\fcharset161\fprq2 Calibri Light Greek{\*\falt Calibri};}{\f31532\fbidi \fswiss\fcharset162\fprq2 Calibri Light Tur{\*\falt Calibri};}{\f31535\fbidi \fswiss\fcharset186\fprq2 Calibri Light Baltic{\*\falt Calibri};}{\f31536\fbidi \fswiss\fcharset163\fprq2 Calibri Light (Vietnamese){\*\falt Calibri};}{\f31538\fbidi \froman\fcharset238\fprq2 Times New Roman CE{\*\falt Times New Roman};}{\f31539\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr{\*\falt Times New Roman};}{\f31541\fbidi \froman\fcharset161\fprq2 Times New Roman Greek{\*\falt Times New Roman};}{\f31542\fbidi \froman\fcharset162\fprq2 Times New Roman Tur{\*\falt Times New Roman};}{\f31543\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew){\*\falt Times New Roman};}{\f31544\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic){\*\falt Times New Roman};}{\f31545\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic{\*\falt Times New Roman};}{\f31546\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese){\*\falt Times New Roman};}{\f31548\fbidi \froman\fcharset238\fprq2 Times New Roman CE{\*\falt Times New Roman};}{\f31549\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr{\*\falt Times New Roman};}{\f31551\fbidi \froman\fcharset161\fprq2 Times New Roman Greek{\*\falt Times New Roman};}{\f31552\fbidi \froman\fcharset162\fprq2 Times New Roman Tur{\*\falt Times New Roman};}{\f31553\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew){\*\falt Times New Roman};}{\f31554\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic){\*\falt Times New Roman};}{\f31555\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic{\*\falt Times New Roman};}{\f31556\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese){\*\falt Times New Roman};}{\f31558\fbidi \froman\fcharset238\fprq2 Times New Roman CE{\*\falt Times New Roman};}{\f31559\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr{\*\falt Times New Roman};}{\f31561\fbidi \froman\fcharset161\fprq2 Times New Roman Greek{\*\falt Times New Roman};}{\f31562\fbidi \froman\fcharset162\fprq2 Times New Roman Tur{\*\falt Times New Roman};}{\f31563\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew){\*\falt Times New Roman};}{\f31564\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic){\*\falt Times New Roman};}{\f31565\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic{\*\falt Times New Roman};}{\f31566\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese){\*\falt Times New Roman};}{\f31568\fbidi \fswiss\fcharset238\fprq2 Calibri CE{\*\falt Times New Roman};}{\f31569\fbidi \fswiss\fcharset204\fprq2 Calibri Cyr{\*\falt Times New Roman};}{\f31571\fbidi \fswiss\fcharset161\fprq2 Calibri Greek{\*\falt Times New Roman};}{\f31572\fbidi \fswiss\fcharset162\fprq2 Calibri Tur{\*\falt Times New Roman};}{\f31575\fbidi \fswiss\fcharset186\fprq2 Calibri Baltic{\*\falt Times New Roman};}{\f31576\fbidi \fswiss\fcharset163\fprq2 Calibri (Vietnamese){\*\falt Times New Roman};}{\f31578\fbidi \froman\fcharset238\fprq2 Times New Roman CE{\*\falt Times New Roman};}{\f31579\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr{\*\falt Times New Roman};}{\f31581\fbidi \froman\fcharset161\fprq2 Times New Roman Greek{\*\falt Times New Roman};}{\f31582\fbidi \froman\fcharset162\fprq2 Times New Roman Tur{\*\falt Times New Roman};}{\f31583\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew){\*\falt Times New Roman};}{\f31584\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic){\*\falt Times New Roman};}{\f31585\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic{\*\falt Times New Roman};}{\f31586\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese){\*\falt Times New Roman};}}{\colortbl;\red0\green0\blue0;\red0\green0\blue255;\red0\green255\blue255;\red0\green255\blue0;\red255\green0\blue255;\red255\green0\blue0;\red255\green255\blue0;\red255\green255\blue255;\red0\green0\blue128;\red0\green128\blue128;\red0\green128\blue0;\red128\green0\blue128;\red128\green0\blue0;\red128\green128\blue0;\red128\green128\blue128;\red192\green192\blue192;\red5\green99\blue193;\red149\green79\blue114;}{\*\defchp \f31506\fs22 }{\*\defpap \ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 }\noqfpromote {\stylesheet{\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af31507\afs22\alang1025 \ltrch\fcs0 \f31506\fs22\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \snext0 \sqformat \spriority0 Normal;}{\*\cs10 \additive \ssemihidden \sunhideused \spriority1 Default Paragraph Font;}{\*\ts11\tsrowd\trftsWidthB3\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tblind0\tblindtype3\tsvertalt\tsbrdrt\tsbrdrl\tsbrdrb\tsbrdrr\tsbrdrdgl\tsbrdrdgr\tsbrdrh\tsbrdrv \ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af31507\afs22\alang1025 \ltrch\fcs0 \f31506\fs22\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \snext11 \ssemihidden \sunhideused Normal Table;}{\*\cs15 \additive \rtlch\fcs1 \af0 \ltrch\fcs0 \ul\cf17 \sbasedon10 \ssemihidden \sunhideused \styrsid16650015 Hyperlink;}{\*\cs16 \additive \rtlch\fcs1 \af0 \ltrch\fcs0 \ul\cf18 \sbasedon10 \ssemihidden \sunhideused \styrsid16650015 FollowedHyperlink;}{\*\cs17 \additive \rtlch\fcs1 \af31507\afs22 \ltrch\fcs0 \f31506\fs22\cf0 \sbasedon10 \ssemihidden \spriority0 \spersonal \scompose \styrsid16650015 EmailStyle17;}}{\*\revtbl {Unknown;}}{\*\rsidtbl \rsid5455638\rsid7608263\rsid16650015}{\mmathPr\mmathFont34\mbrkBin0\mbrkBinSub0\msmallFrac0\mdispDef1\mlMargin0\mrMargin0\mdefJc1\mwrapIndent1440\mintLim0\mnaryLim1}{\*\xmlnstbl {\xmlns1 http://schemas.microsoft.com/office/word/2003/wordml}}\paperw12240\paperh15840\margl1440\margr1440\margt1440\margb1440\gutter0\ltrsect \widowctrl\ftnbj\aenddoc\trackmoves0\trackformatting1\donotembedsysfont1\relyonvml0\donotembedlingdata0\grfdocevents0\validatexml1\showplaceholdtext0\ignoremixedcontent0\saveinvalidxml0\showxmlerrors1\noxlattoyen\expshrtn\noultrlspc\dntblnsbdb\nospaceforul\formshade\horzdoc\dgmargin\dghspace180\dgvspace180\dghorigin150\dgvorigin0\dghshow1\dgvshow1\jexpand\viewkind5\viewscale100\pgbrdrhead\pgbrdrfoot\splytwnine\ftnlytwnine\htmautsp\nolnhtadjtbl\useltbaln\alntblind\lytcalctblwd\lyttblrtgr\lnbrkrule\nobrkwrptbl\snaptogridincell\allowfieldendsel\wrppunct\asianbrkrule\rsidroot7608263\newtblstyruls\nogrowautofit\usenormstyforlist\noindnmbrts\felnbrelev\nocxsptable\indrlsweleven\noafcnsttbl\afelev\utinl\hwelev\spltpgpar\notcvasp\notbrkcnstfrctbl\notvatxbx\krnprsnet\cachedcolbal \nouicompat \fet0{\*\wgrffmtfilter 2450}\nofeaturethrottle1\ilfomacatclnup0\ltrpar \sectd \ltrsect\linex0\endnhere\sectdefaultcl\sftnbj {\*\pnseclvl1\pnucrm\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl2\pnucltr\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl3\pndec\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl4\pnlcltr\pnstart1\pnindent720\pnhang {\pntxta )}}{\*\pnseclvl5\pndec\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl6\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl7\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl8\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl9\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}\pard\plain \ltrpar\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid16650015 \rtlch\fcs1 \af31507\afs22\alang1025 \ltrch\fcs0 \f31506\fs22\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 {\rtlch\fcs1 \af31507 \ltrch\fcs0 \cf0\insrsid16650015 Testing with an inline image
\par 
\par }{\rtlch\fcs1 \af31507 \ltrch\fcs0 \cf0\lang1024\langfe1024\noproof\insrsid7608263\charrsid14577418 \objattph  }{\rtlch\fcs1 \af31507 \ltrch\fcs0 \cf0\insrsid16650015 
\par }\sectd \ltrsect\linex0\endnhere\sectdefaultcl\sftnbj {\*\pnseclvl1\pnucrm\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl2\pnucltr\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl3\pndec\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl4\pnlcltr\pnstart1\pnindent720\pnhang {\pntxta )}}{\*\pnseclvl5\pndec\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl6\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl7\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl8\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl9\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}\pard\plain \ltrpar\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid16650015 \rtlch\fcs1 \af31507\afs22\alang1025 \ltrch\fcs0 \f31506\fs22\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 {\rtlch\fcs1 \af31507 \ltrch\fcs0 \cf0\insrsid16650015 
\par And a file}{\rtlch\fcs1 \af31507 \ltrch\fcs0 \cf0\insrsid16650015 
\par 
\par }{\pard\plain \ltrpar\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid16650015 \rtlch\fcs1 \af31507\afs22\alang1025 \ltrch\fcs0 \f31506\fs22\lang1033\langfe1033\cgrid\langnp1033\langfenp1033\insrsid16650015 {{\objattph  {\rtlch\fcs1 \af31507 \ltrch\fcs0 \cf0\insrsid16650015 }}}}\sectd \ltrsect\linex0\endnhere\sectdefaultcl\sftnbj {\rtlch\fcs1 \af31507 \ltrch\fcs0 \cf0\insrsid16650015\charrsid16650015 
\par }}";
      const string html = "<div style=\"font-size:12pt;font-family:&quot;Times New Roman&quot;, serif;\"><p style=\"text-align:left;font-family:Calibri, sans-serif;font-size:11pt;margin:0;\">Testing with an inline image</p><p style=\"text-align:left;font-family:Calibri, sans-serif;font-size:11pt;margin:0;\"><br></p><p style=\"text-align:left;font-family:Calibri, sans-serif;font-size:11pt;margin:0;\"><span data-index=\"0\"></span> </p><p style=\"text-align:left;font-family:Calibri, sans-serif;font-size:11pt;margin:0;\"><br></p><p style=\"text-align:left;font-family:Calibri, sans-serif;font-size:11pt;margin:0;\">And a file</p><p style=\"text-align:left;font-family:Calibri, sans-serif;font-size:11pt;margin:0;\"><br></p><p style=\"text-align:left;font-family:Calibri, sans-serif;font-size:11pt;margin:0;\"><span data-index=\"1\"></span> </p></div>";

      var output = Rtf.ToHtml(rtf);

      Assert.AreEqual(html, output);
    }

    [TestMethod]
    public void NonBreaking()
    {
      const string rtf = "{\\rtf1\\ansicpg1252\r\n{\r\n\\fonttbl\r\n{\\f0 Times New Roman;}\r\n{\\f1 Calibri;}\r\n}\r\n{\r\n\\colortbl\r\n;\r\n\\red0\\green0\\blue255;\r\n\\red0\\green0\\blue0;\r\n}\r\n\\nouicompat\\splytwnine\\htmautsp{\\pard\\plain\\ql{\\f1\\fs24\\cf2 <p><span style=\"color:#404040;font-family:calibri;font-size:12pt;\">On d&#233;termine la couverture de votre objectif de revenu d&#8217;invalidit&#233; en analysant dans quelle mesure vos rentr&#233;es de fonds couvrent vos sorties de fonds dans l'&#233;ventualit&#233; d&#8217;une invalidit&#233;. La couverture actuelle de votre objectif est de </span><strong><span style=\"color:#3265a4;font-family:calibri;font-size:12pt;\">79 %</span></strong><span style=\"color:#404040;font-family:calibri;font-size:12pt;\">. Votre besoin en revenu d'invalidit&#233; est de </span><strong><span style=\"color:#3265a4;font-family:calibri;font-size:12pt;\">3\\~320\\~853 $</span></strong><span style=\"color:#404040;font-family:calibri;font-size:12pt;\"> au cours de la p&#233;riode d'invalidit&#233; projet&#233;e de </span><strong><span style=\"color:#3265a4;font-family:calibri;font-size:12pt;\">192</span></strong><span style=\"color:#404040;font-family:calibri;font-size:12pt;\"> mois. Ce besoin inclut toutes les d&#233;penses mensuelles n&#233;cessaires comme la nourriture, les v&#234;tements et le logement. French blah.</span></p>}\\fs24\\par}\r\n}\r\n";
      var output = Rtf.ToHtml(rtf);
      const string expected = "<div style=\"font-size:12pt;\"><p style=\"text-align:left;font-family:Calibri;margin:0;\">&lt;p&gt;&lt;span style=\"color:#404040;font-family:calibri;font-size:12pt;\"&gt;On d&amp;#233;termine la couverture de votre objectif de revenu d&amp;#8217;invalidit&amp;#233; en analysant dans quelle mesure vos rentr&amp;#233;es de fonds couvrent vos sorties de fonds dans l'&amp;#233;ventualit&amp;#233; d&amp;#8217;une invalidit&amp;#233;. La couverture actuelle de votre objectif est de &lt;/span&gt;&lt;strong&gt;&lt;span style=\"color:#3265a4;font-family:calibri;font-size:12pt;\"&gt;79 %&lt;/span&gt;&lt;/strong&gt;&lt;span style=\"color:#404040;font-family:calibri;font-size:12pt;\"&gt;. Votre besoin en revenu d'invalidit&amp;#233; est de &lt;/span&gt;&lt;strong&gt;&lt;span style=\"color:#3265a4;font-family:calibri;font-size:12pt;\"&gt;3&nbsp;320&nbsp;853 $&lt;/span&gt;&lt;/strong&gt;&lt;span style=\"color:#404040;font-family:calibri;font-size:12pt;\"&gt; au cours de la p&amp;#233;riode d'invalidit&amp;#233; projet&amp;#233;e de &lt;/span&gt;&lt;strong&gt;&lt;span style=\"color:#3265a4;font-family:calibri;font-size:12pt;\"&gt;192&lt;/span&gt;&lt;/strong&gt;&lt;span style=\"color:#404040;font-family:calibri;font-size:12pt;\"&gt; mois. Ce besoin inclut toutes les d&amp;#233;penses mensuelles n&amp;#233;cessaires comme la nourriture, les v&amp;#234;tements et le logement. French blah.&lt;/span&gt;&lt;/p&gt;</p></div>";
      Assert.AreEqual(expected, output);
    }
  }
}
