using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace kOS_IDE
{
    public partial class GlobalOptions : Form
    {
        bool Is64OS;
        
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);
        
        public GlobalOptions()
        {
            InitializeComponent();
            Is64OS = (IntPtr.Size == 8) || InternalCheckIsWow64();
        }

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                KSPdir.Enabled = false;
                FindKSP.Enabled = false;

                string pFiles = (Is64OS ? "Program Files (x86)" : "Program Files");

                KSPdir.Text = @"C:\" + pFiles + @"\Steam\SteamApps\common\Kerbal Space Program";
            }
            else
            {
                KSPdir.Enabled = true;
                FindKSP.Enabled = true;
                KSPdir.Text = "";
            }
        }

        private void KSPdir_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(KSPdir.Text)) AppOptions.KSPfolder = KSPdir.Text;
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            Connect.Enabled = false;
            Connect.Text = "Connecting ...";

            // Do connection stuff here

            if (true) // If success
            {
                AppOptions.Connection.Username = Username.Text;
                AppOptions.Connection.Password = new Psswd(Password.Text);

                Connect.Text = "Connected.";
            }
        }

        private void ZoomBar_Scroll(object sender, EventArgs e)
        {
            float zm = (float)ZoomBar.Value;
            if (zm < 0) zm = -(float)(1 / zm);
            else zm /= 10.0f;
            int add = (ZoomBar.Value < 0 ? 0 : 100);
            ZoomLabel.Text = "Zoom: " + (100 * zm + add) + "%";

            AppOptions.Zoom = ZoomBar.Value;
        }

        private void SmartIndent_CheckedChanged(object sender, EventArgs e)
        {
            AppOptions.SmartIndent = SmartIndent.Checked;
        }
    }

    public class Psswd
    {
        public string MD5hash;
        public byte CharsCount;

        public bool Ready;

        public Psswd(string md5, byte Length)
        {
            MD5hash = md5;
            CharsCount = Length;

            Ready = true;
        }

        public Psswd(string input)
        {
            string result = "";

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                foreach (byte bit in data)
                    result += bit.ToString("X2");
            }

            MD5hash = result;
            CharsCount = (byte)input.Length;
            Ready = true;
        }

        public Psswd()
        {
            MD5hash = "";
            CharsCount = 0;

            Ready = false;
        }
    }

    public static class AppOptions
    {
        public static string KSPfolder;
        public static int Zoom;
        public static bool SmartIndent;

        public static class Connection
        {
            public static string Username;
            public static bool Remember;
            public static Psswd Password;
        }

        public static void Load(string filepath)
        {
            foreach (string s in System.IO.File.ReadAllLines(filepath))
            {
                string[] line = s.Split('=');

                switch (line[0])
                {
                    case "KSP":
                        KSPfolder = line[1];
                        continue;

                    case "User":
                        Connection.Username = line[1];
                        continue;

                    case "Password":
                        Connection.Remember = true;
                        string[] split = line[1].Split('|');
                        Psswd psswd = new Psswd(split[0], byte.Parse(split[1]));
                        continue;

                    case "SmartIndent":
                        SmartIndent = Boolean.Parse(line[1]);
                        continue;

                    case "Zoom":
                        int zm = int.Parse(line[1]);
                        Zoom = (zm > -5 && zm < 50 ? zm : 0);
                        continue;

                    default:
                        continue;
                }
            }
        }

        public static void Save(string filepath)
        {
            List<string> Lines = new List<string>();

            Lines.Add("KSP=" + KSPfolder);
            Lines.Add("User=" + Connection.Username);
            Lines.Add("Password=" + Connection.Password.MD5hash + "|" + Connection.Password.CharsCount);
            Lines.Add("SmartIndent=" + SmartIndent);
            Lines.Add("Zoom=" + Zoom);

            System.IO.File.WriteAllLines(filepath, Lines.ToArray<string>());
        }

        public static void Default()
        {
            KSPfolder = null;
            SmartIndent = true;
            Zoom = 0;
            Connection.Password = new Psswd();
        }
    }
}
