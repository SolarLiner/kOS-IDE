using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ScintillaNET;


[assembly: AssemblyVersion("0.37.*")] // TODO: Change before release

namespace kOS_IDE
{
    public partial class TextEditor : Form
    {
        bool _faststart = false;
        public bool FastStart
        {
            set { _faststart = value; }
        }
        
        private string _fn;
        string filename
        {
            get { return _fn; }
            set
            {
                _fn = value;
                string version = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() +"."+ Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
                try
                {
                    if (!String.IsNullOrWhiteSpace(value)) this.Text = "kOS IDE v" + version + " - " + Path.GetFileName(value);
                    else this.Text = "kOS IDE v" + version + " - Unknown";
                }
                catch { }
            }
        }

        bool _optshw;
        bool OptionsShown
        {
            get { return _optshw; }
            set
            {
                optionsToolStripMenuItem.Checked = value;
                opts.Visible = value;
                _optshw = value;
            }
        }

        bool CommentStripper;

        char[] EventChars = { ' ', '\n', ':' };
        List<string> KeysAndVars;
        List<string> Subitems;

        // Syntax words
        string[] args1 =
            { "set", "print", "until", "if", "switch", "copy", "from", "delete", "declare", "edit", "list", "lock", "on", "off", "rename", "run", "toggle", "unlock",
                "wait", "when", "sin", "cos", "tan", "arcsin", "arccos", "arctan", "arctan2", "abs", "R"};
        string[] args2 = 
            {   "target", "break", "clearscreen", "reboot", "shutdown", "stage", "all", "vesselname", "altitude", "radar", "missiontime", "velocity",
                "then", "abort", "ag1", "ag2", "ag3", "ag4", "ag5", "ag6", "ag7", "ag8", "ag9", "ag10", "volume", "volumes", "file", "files", "parts",
                "resources", "engines", "targets", "bodies", "parameter", "at", "to", "VESSEL",
                "landed", "splashed", "flying", "sub_orbital", "orbiting", "escaping", "docked",
                "liquidfuel", "oxidizer", "electriccharge", "intakeair", "solidfuel",
                "major", "minor",
                "throttle", "steering", "wheelthrottle", "wheelsteering", "brakes", "gear", "legs", "chutes", "lights", "rcs", "sas", "altitude", "alt",
                "apoapsis", "periapsis", "eta", "sessiontime", "warp", "angularmomentum", "angularvel", "surfacespeed", "verticalspeed",
                "facing", "geoposition", "heading", "latitude", "longitude", "mag", "node", "north", "prograde",
                "retrograde", "up", "body", "mass", "maxthrust", "status", "commrange", "incommrange", "inlight",
                "version"};

        void InitAutoComplete()
        {
            KeysAndVars = new List<string>();
            
            
            //string autocompletion = "";

            foreach (string arg in args1)
            {
                KeysAndVars.Add(arg + "?0");
            }

            foreach (string arg in args2)
            {
                KeysAndVars.Add(arg + "?1");
            }

            // Register Images
            var imgs = new ImageList();
            imgs.Images.Add(Properties.Resources.keyword);
            imgs.Images.Add(Properties.Resources._class);
            imgs.Images.Add(Properties.Resources.var);

            Editor.AutoComplete.RegisterImages(imgs, Color.Black);

            Subitems = new List<string>(new string[] {"distance?2", "apoapsis?2", "periapsis?2", "up?2", "pitch?2", "yaw?2", "roll?2", "mag?2", "deltav?2",
                                                        "burnvector?2", "time?2", "clock?2", "calendar?2", "year?2", "day?2", "hour?2", "minute?2", "second?2"});
            KeysAndVars.Sort();
            Subitems.Sort();
        }


        public TextEditor()
        {
            OpenedForms.NewForm = this;
            AppOptions.Default();
            InitializeComponent();

            opts.Owner = this;

            string version = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() +"."+ Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            filename = "";

            InitAutoComplete();

            // Set up Scintilla
            Editor.Lexing.Lexer = Lexer.CppNoCase;
            string arg1 = "";
            foreach (string arg in args1) arg1 += arg + " ";
            string arg2 = "";
            foreach (string arg in args2) arg2 += arg + " ";
            string arg3 = "";

            Editor.Lexing.SetKeywords(0, arg1);
            Editor.Lexing.SetKeywords(1, arg2);
            Editor.Lexing.SetKeywords(2, arg3);
            Editor.Lexing.LineCommentPrefix = "//";

            Editor.Styles[Editor.Lexing.StyleNameMap["DOCUMENT_DEFAULT"]].ForeColor = System.Drawing.Color.Black;
            Editor.Styles[Editor.Lexing.StyleNameMap["NUMBER"]].ForeColor = System.Drawing.Color.Orange;
            Editor.Styles[Editor.Lexing.StyleNameMap["WORD"]].ForeColor = System.Drawing.Color.Blue;
            Editor.Styles[Editor.Lexing.StyleNameMap["WORD2"]].ForeColor = System.Drawing.Color.Purple;
            Editor.Styles[Editor.Lexing.StyleNameMap["STRING"]].ForeColor = System.Drawing.Color.FromArgb(200, 0, 0);
            Editor.Styles[Editor.Lexing.StyleNameMap["CHARACTER"]].ForeColor = System.Drawing.Color.Red;
            Editor.Styles[Editor.Lexing.StyleNameMap["PREPROCESSOR"]].ForeColor = System.Drawing.Color.Brown;
            Editor.Styles[Editor.Lexing.StyleNameMap["OPERATOR"]].ForeColor = System.Drawing.Color.Black;
            Editor.Styles[Editor.Lexing.StyleNameMap["IDENTIFIER"]].ForeColor = System.Drawing.Color.Black;
            Editor.Styles[Editor.Lexing.StyleNameMap["COMMENT"]].ForeColor = System.Drawing.Color.Green;

            Editor.AutoComplete.List = KeysAndVars;
            Editor.AutoComplete.MaxHeight = 15;
            Editor.AutoComplete.MaxWidth = 40;
            Editor.AutoComplete.IsCaseSensitive = false;
            Editor.AutoComplete.DropRestOfWord = false;
            Editor.AutoComplete.AutoHide = false;
            Editor.AutoComplete.AutomaticLengthEntered = true;
            Editor.AutoComplete.StopCharacters = "{SPACE}";
            Editor.AutoComplete.FillUpCharacters = "{TAB}:.";

            Editor.Indentation.BackspaceUnindents = true;
            Editor.Indentation.IndentWidth = 4;
            Editor.Indentation.SmartIndentType = SmartIndent.CPP2;
            Editor.Indentation.TabIndents = true;
            Editor.Indentation.TabWidth = 4;
            Editor.Indentation.UseTabs = true;
            Editor.Indentation.ShowGuides = true;
        }

        private void TextEditor_Load(object sender, EventArgs e)
        {
#if DEBUG
            //string args = "";
            //for (int i = 0; i < Environment.GetCommandLineArgs().Count(); i++)
            //{
            //    if (i == 0)
            //    {
            //        args = Environment.GetCommandLineArgs()[i];
            //        continue;
            //    }

            //    args += " " + Environment.GetCommandLineArgs()[i];
            //}
            //Status.Text = args;
            Status.Text = String.Format("{0}.{1}, build {2} (rev {3})", Assembly.GetExecutingAssembly().GetName().Version.Major, Assembly.GetExecutingAssembly().GetName().Version.Minor, Assembly.GetExecutingAssembly().GetName().Version.Build, Assembly.GetExecutingAssembly().GetName().Version.Revision);
#else
            // Just for Laughs :D
            string[] msgs = { "Ready.", "Hello!", "Ready to xplode some rockets?", "Looks like some kerbals need autopiloting ...", "Ready.", "AP FTW !",
                                "MechJeb's for the weaks!", "Have you ever looked to the \"?\" menu?" };
            Random r = new Random();
            int next = r.Next(1, msgs.Length) - 1;
            Status.Text = msgs[next];
#endif
            
            if (!_faststart)
            {
                string[] envarg = Environment.GetCommandLineArgs();
                if (envarg.Count() > 1)
                {
                    Editor.Text = File.ReadAllText(envarg[1]);
                    RefreshVars(Editor.Text);
                    filename = envarg[1];
#if RELEASE
                    Status.Text = "Loaded \"" + filename + "\".";
#endif
                    // Should be done with the filename property
                    //string version = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
                    //this.Text = "kOS IDE v" + version + " - " + filename;
                }
            }
        }

        private void Editor_TextChanged(object sender, EventArgs e)
        {
            int wordsCount, lines, bytes, ln, cl;
            wordsCount = Editor.Text.Split(' ').Count();
            lines = Editor.Lines.Count;
            bytes = ASCIIEncoding.ASCII.GetByteCount(Editor.Text);

            ln = Editor.Lines.Current.Number;
            cl = Editor.Selection.Start - Editor.Lines.Current.StartPosition;

            Stats.Text = String.Format("Bytes: {0} | Words: {1} | Lines: {2} | L{3} C{4}", bytes, wordsCount, lines, ln, cl);

            if (bytes > 10000)
            {
                Status.Text = "More than 10k bytes. Toggled comment sttriper at save. (but not removed on the editor)";
                CommentStripper = true;
            }

            // Scintilla stuff
            if (Editor.Text.Length < 1) return;

            char last = Editor.Text[Editor.Text.Length - 1];
            if (EventChars.Any(s => s == last))
            {
                switch (last)
                {
                    case ' ':
                        string[] words = Editor.Text.Split(' ').Where(s => !String.IsNullOrWhiteSpace(s)).ToArray<string>();
                        int minTwo = words.Length - 2;
                        int minOne = minTwo + 1;
                        if (minTwo < 0) break;

                        if (("set" == words[minTwo] || "lock" == words[minTwo]))
                        {
                            if (KeysAndVars.Any(s => s.Substring(0, s.Length-2) == words[minOne])) break;
                            KeysAndVars.Add(words[minOne] + "?2");
                            KeysAndVars.Sort();
                            Editor.AutoComplete.List = KeysAndVars;
                        }
                        break;

                    case ':':
                        Editor.AutoComplete.Show(Subitems);
                        break;

                    default:
                        //Editor.AutoComplete.Show(KeysAndVars);
                        break;
                }
            }
        }

        private void RefreshVars(string text)
        {
            InitAutoComplete();
            string[] words = text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "set" || words[i] == "lock")
                {
                    i++;
                    if (KeysAndVars.Any(s => s == words[i])) continue;
                    KeysAndVars.Add(words[i] + "?2");
                }
            }
            KeysAndVars.Sort();
            Editor.AutoComplete.List = KeysAndVars;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor temp = new TextEditor();
            temp.Show();
            temp.filename = null;
            temp.Editor.Text = "";
            temp.Status.Text = "New window !";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadScript ls = new LoadScript(AppOptions.KSPfolder);
            if (ls.ShowDialog() != DialogResult.OK) return;

            filename = ls.FileName;
            Editor.Text = System.IO.File.ReadAllText(filename, Encoding.ASCII);

            RefreshVars(Editor.Text);
            
            Status.Text = "Loaded \"" + System.IO.Path.GetFileNameWithoutExtension(filename) + "\".";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == null)
            {
                if (SaveFile.ShowDialog() != DialogResult.OK) return;

                filename = SaveFile.FileName;
            }
            if (CommentStripper)
            {
                Status.Text = "Stripping comments and saving ...";
                
                string[] text = Editor.Text.Split(new char[] { '\n' }, StringSplitOptions.None);
                List<string> Output = new List<string>();
                bool added = false;

                for (int j = 0; j < text.Length; j++)
                {
                    added = false;
                    text[j] = new string(text[j].Where(c => c != '\r' && c != '\n').ToArray());
                    string line = text[j].Trim();
                    for (int i = 0; i < line.Length; i++)
                    {
                        try
                        {
                            if (line.Substring(i, 2) == "//")
                            {
                                if (i != 0) Output.Add(text[j].Substring(0, i - 1));
                                added = true;
                                break;
                            }
                        }
                        catch { }
                    }

                    if (!added) Output.Add(text[j]);
                }
                System.IO.File.WriteAllLines(filename, Output.ToArray(), Encoding.ASCII);
                Status.Text = "Saved stripped file as \"" + Path.GetFileNameWithoutExtension(filename) + "\".";
                return;
            }

            System.IO.File.WriteAllText(filename, Editor.Text, Encoding.ASCII);

            Status.Text = "Saved as \"" + Path.GetFileNameWithoutExtension(filename) + "\".";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Editor_Enter(object sender, EventArgs e)
        {
            if (Editor.Text.Length == 0) Editor.AutoComplete.Show(KeysAndVars);
        }

        void Editor_LinesNeedShown(object sender, ScintillaNET.LinesNeedShownEventArgs e)
        {
            const uint SCI_SETMARGINTYPEN = 0x08C0;
            const uint SCI_MARGINSETTEXT = 0x09E2;

            Editor.Margins.Margin1.Width = 30;
            Editor.NativeInterface.SendMessageDirect(SCI_SETMARGINTYPEN, 0, 4);
            for (int i = e.FirstLine - 1; i < e.LastLine - 1; i++)
            {
                // Enter custom Scintilla message here for margin number
                Editor.NativeInterface.SendMessageDirect(SCI_MARGINSETTEXT, i, i.ToString());
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string version = String.Format("{0}.{1}, build {2}", Assembly.GetExecutingAssembly().GetName().Version.Major, Assembly.GetExecutingAssembly().GetName().Version.Minor, Assembly.GetExecutingAssembly().GetName().Version.Build);
            MessageBox.Show("Brought to you by SolarLiner, via a GeoKerbol RemoteTech satellite.\n Version " + version + ".", "About the kOS IDE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportHTML()
        {
            string RandomPath = Path.GetTempFileName();
            TextWriter stream = new StreamWriter(RandomPath, false, Encoding.ASCII, 1024);
            Editor.ExportHtml(stream, "kOS Script", false);
            stream.Close(); stream.Dispose();

            TextEditor result = new TextEditor();
            result.FastStart = true;
            result.Editor.Lexing.Lexer = Lexer.Xml;
            result.Editor.Text = File.ReadAllText(RandomPath, Encoding.ASCII);
            result.Show();
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkWithColorizedDocument(ExportHTML);
        }

        private void refreshAutoCompletionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshVars(Editor.Text);
            Status.Text = "Refreshed Auto Completion list.";
        }

        private void saveNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveFile.ShowDialog() != DialogResult.OK) return;
            filename = SaveFile.FileName;

            if (CommentStripper)
            {
                Status.Text = "Stripping comments and saving ...";

                string[] text = Editor.Text.Split(new char[] { '\n' }, StringSplitOptions.None);
                List<string> Output = new List<string>();
                bool added = false;

                for (int j = 0; j < text.Length; j++)
                {
                    added = false;
                    text[j] = new string(text[j].Where(c => c != '\r' && c != '\n').ToArray());
                    string line = text[j].Trim();
                    for (int i = 0; i < line.Length; i++)
                    {
                        try
                        {
                            if (line.Substring(i, 2) == "//")
                            {
                                if (i != 0) Output.Add(text[j].Substring(0, i - 1));
                                added = true;
                                break;
                            }
                        }
                        catch { }
                    }

                    if (!added) Output.Add(text[j]);
                }
                System.IO.File.WriteAllLines(filename, Output.ToArray(), Encoding.ASCII);
                Status.Text = "Saved stripped file as \"" + Path.GetFileNameWithoutExtension(filename) + "\".";
                return;
            }

            System.IO.File.WriteAllText(filename, Editor.Text, Encoding.ASCII);

            Status.Text = "Saved as \"" + Path.GetFileNameWithoutExtension(filename) + "\".";
        }

        #region External Access

        public void Append(char chr)
        {
            this.Editor.Text += chr;
        }

        public void Append(string text)
        {
            this.Editor.Text += text;
        }

        public void Append(string text, params object[] args)
        {
            this.Editor.Text += String.Format(text, args);
        }
        
        public void AppendLine()
        {
            Append("\n");
        }

        public void AppendLine(string text)
        {
            if (String.IsNullOrWhiteSpace(this.Editor.Lines.Current.Text))
                Append(text + '\n');
            else
                Append('\n' + text + '\n');
        }

        public void AppendLine(string text, params object[] args)
        {
            Append(text + '\n', args);
        }

        public void AppendAllLines(string[] lines)
        {
            foreach (string line in lines)
                AppendLine(line);
        }

        public void AppendAllLines(string[] lines, params object[] args)
        {
            foreach (string line in lines)
                AppendLine(line, args);
        }

        public void WorkWithColorizedDocument(Action func)
        {
            Editor.IsCustomPaintingEnabled = false; // To allow lexing of all the document
            Editor.Lexing.Colorize();

            func(); // Do things

            Editor.IsCustomPaintingEnabled = true; // Restore paint
        }

        public void WorkWithColorizedDocument<T>(Func<T> function, out T result)
        {
            Editor.IsCustomPaintingEnabled = false; // To allow lexing of all the document
            Editor.Lexing.Colorize();

            result = function();

            Editor.IsCustomPaintingEnabled = true; // Restore paint
        }

        #endregion

        private void ExportBBCode()
        {
            TextEditor result = new TextEditor();
            result.FastStart = true;
            result.Editor.AutoComplete.List = null;
            result.Editor.Lexing.Lexer = Lexer.Null;
            result.Editor.Text = "[CODE]\n";

            INativeScintilla NativeInterface = (INativeScintilla)Editor;
            int length = NativeInterface.GetLength();
            bool[] stylesUsed = new bool[(int)StylesCommon.Max + 1];

            for (int i = 0; i < length; i++)
            {
                stylesUsed[Editor.Styles.GetStyleAt(i) & (int)StylesCommon.Max] = true;
            }

            int TabWidth = Editor.Indentation.TabWidth;

            char lc;
            char c = '\0';
            int LastStyle = -1;
            for (int i = 0; i < length; i++)
            {
                lc = c;
                c = NativeInterface.GetCharAt(i);
                int style = Editor.Styles.GetStyleAt(i);
                if (style != LastStyle && c != ' ')
                {
                    if (LastStyle != -1)
                        result.Append("[/COLOR]");

                    result.Append("[COLOR=\"{0}\"]", (Editor.Styles[style].ForeColor.Name == "ffc80000" ? "Red" : Editor.Styles[style].ForeColor.Name));
                    LastStyle = style;
                }

                if (c != '\0') result.Append(c);
                else result.Append("\\0");
            }
            if (LastStyle != -1) result.Append("[/COLOR]");
            result.AppendLine("[/CODE]");

            result.Show();
            result.filename = null;
        }
        
        private void bBCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkWithColorizedDocument(ExportBBCode);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsShown = !OptionsShown;
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new GlobalOptions().ShowDialog();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.FindReplace.ShowFind();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.FindReplace.ShowReplace();
        }

        void SaveHtml(string fname)
        {

        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "txt"; sfd.Filter = "Text File|*.txt|HTML Document|*.html;*.htm|Rich Text File|*.rtf";
            sfd.FilterIndex = 0; sfd.Title = "Export file ...";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            //if (sfd.FilterIndex == 1)
            //{
            //    System.IO.File.WriteAllText(sfd.FileName, Editor.Text); // RTF Mode not yet implemented
            //}
            //else
            if (sfd.FilterIndex == 2)
            {
                using (TextWriter tw = new StreamWriter(sfd.FileName))
                {
                    Editor.IsCustomPaintingEnabled = false; // To allow lexing of all the document
                    Editor.Lexing.Colorize();

                    Editor.ExportHtml(tw, System.IO.Path.GetFileNameWithoutExtension(filename), false);

                    Editor.IsCustomPaintingEnabled = true; // Restore paint
                }
            }
            else
            {
                System.IO.File.WriteAllText(sfd.FileName, Editor.Text);
            }
        }
    }
}
