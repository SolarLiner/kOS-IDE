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
                //SmartIndent.Checked = (_own.Editor.Indentation.SmartIndentType == ScintillaNET.SmartIndent.CPP2);
            }
            catch { }
        }

        private void SmartIndent_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void SetFont_Click(object sender, EventArgs e)
        {
            fontDialog.FontMustExist = true;
            fontDialog.AllowVectorFonts = false;
            fontDialog.AllowVerticalFonts = false;

            if (fontDialog.ShowDialog(this) != DialogResult.OK) return;
            AppOptions.Font = fontDialog.Font.FontFamily.Name;

            foreach (TextEditor frm in OpenedForms.Forms)
            {
                frm.Editor.Font = fontDialog.Font;
                frm.Editor.OnTextChanged();
            }
        }

        private void ZoomBar_Scroll(object sender, EventArgs e)
        {
            if (Owner != null)
            {                
                Zoom.Text = "Zoom: " + (ZoomBar.Value) + "%";
                Owner.Editor.Zoom = ZoomBar.Value;
            }
            else
            {
                foreach (TextEditor frm in OpenedForms.Forms)
                {
                    Zoom.Text = "Zoom: " + (ZoomBar.Value) + "%";
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
