using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kOS_IDE
{
    public partial class Options : UserControl
    {
        private TextEditor _own;
        public TextEditor Owner
        {
            get { return _own; }
            set
            {
                _own = value;
                //ZoomBar.Enabled = (value != null);
            }
        }
        
        public Options()
        {
            InitializeComponent();
            //ZoomBar.Enabled = false;
            try
            {
                ZoomBar.Value = _own.Editor.Zoom;
                SmartIndent.Checked = (_own.Editor.Indentation.SmartIndentType == ScintillaNET.SmartIndent.CPP2);
            }
            catch { }
        }

        private void SmartIndent_CheckedChanged(object sender, EventArgs e)
        {
            if (SmartIndent.Checked) _own.Editor.Indentation.SmartIndentType = ScintillaNET.SmartIndent.CPP2;
            else _own.Editor.Indentation.SmartIndentType = ScintillaNET.SmartIndent.None;
        }

        private void SetFont_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog(this) != DialogResult.OK) return;

            foreach (TextEditor frm in OpenedForms.Forms)
            {
                frm.Editor.Styles.Default.Font = fontDialog.Font;
                frm.Editor.Lexing.Colorize();
            }
        }

        private void ZoomBar_Scroll(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                float zm = (float)ZoomBar.Value;
                if (zm < 0) zm = -(float)(1 / zm);
                else zm /= 10.0f;
                int add = (ZoomBar.Value < 0 ? 0 : 100);
                Zoom.Text = "Zoom: " + (100 * zm + add) + "%";
                Owner.Editor.Zoom = ZoomBar.Value;
            }
            else
            {
                foreach (TextEditor frm in OpenedForms.Forms)
                {
                    float zm = (float)ZoomBar.Value;
                    if (zm < 0) zm = -(float)(1 / zm);
                    else zm /= 10.0f;
                    int add = (ZoomBar.Value < 0 ? 0 : 100);
                    Zoom.Text = "Zoom: " + (100 * zm+add) + "%";
                    frm.Editor.Zoom = ZoomBar.Value;
                }
            }
        }
    }

    public static class OpenedForms
    {
        private static List<TextEditor> _frms;

        public static TextEditor[] Forms { get { return _frms.ToArray<TextEditor>(); } }
        public static List<TextEditor> ListForms { get { return _frms; } }
        public static TextEditor NewForm { set { _frms.Add(value); } }
        public static TextEditor[] NewForms { set { foreach (TextEditor frm in value) _frms.Add(frm); } }

        public static void Init() { _frms = new List<TextEditor>(); }
    }
}
