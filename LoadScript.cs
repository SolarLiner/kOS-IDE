using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace kOS_IDE
{
    public partial class LoadScript : Form
    {
        string _ksp;
        string _fName;
        public string FileName
        {
            get { return _fName; }
        }
        
        public LoadScript()
        {
            InitializeComponent();

            _fName = null;
            
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "Locate your KSP directory";
            if (fd.ShowDialog() != DialogResult.OK) return;
            _ksp = fd.SelectedPath;
            AppOptions.KSPfolder = fd.SelectedPath;
        }

        public LoadScript(string KSPdir)
        {
            InitializeComponent();

            _fName = null;
            if (String.IsNullOrWhiteSpace(KSPdir))
            {
                FolderBrowserDialog fd = new FolderBrowserDialog();
                fd.Description = "Locate your KSP directory";
                if (fd.ShowDialog() != DialogResult.OK) return;
                _ksp = fd.SelectedPath;
                AppOptions.KSPfolder = fd.SelectedPath;
            }
            else _ksp = KSPdir;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Close();
        }

        private void LoadScript_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_ksp))
            {
                MessageBox.Show("Cannot get the KSP Directory.", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = System.Windows.Forms.DialogResult.Abort;

                this.Close();
            }

            string ArchivePath = Path.Combine(_ksp, @"Plugins\PluginData\Archive");

            if (!Directory.Exists(ArchivePath))
            {
                MessageBox.Show("KSP does not include a functionnal archive for kOS. Aborting.", "Archive Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = System.Windows.Forms.DialogResult.Abort;

                this.Close();
            }
            ShowKSPdir.Text = "Kerbal Space Program directory: " + _ksp;

            foreach (string filepath in Directory.GetFiles(ArchivePath))
            {
                if (Path.GetExtension(filepath) != ".txt") continue;
                string name = Path.GetFileNameWithoutExtension(filepath);
                long size = new FileInfo(filepath).Length;

                ListViewItem added = listView1.Items.Add(name);
                added.Tag = filepath;
                if (size < 1024) // 2^10 = 1024 b
                {
                    added.SubItems.Add(size + " b");
                }
                else
                {
                    added.SubItems.Add(Math.Round(size / 1024.0, 3) + " kb");
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) OK.Enabled = false;
            else { OK.Enabled = true; _fName = (string)listView1.SelectedItems[0].Tag; }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) return;

            _fName = (string)listView1.SelectedItems[0].Tag;
            OK_Click(null, e);
        }
    }
}
