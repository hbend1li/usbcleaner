/*
 * Created by SharpDevelop.
 * User: freeman
 * Date: 11/21/2016
 * Time: 11:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace usb_cleaner
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	/// 
	
	public partial class MainForm : Form
	{
		public readonly string[] suspecte_file_list = {"*.lnk","~$*.*","*.a3x","*.tmp","*.qgb","auto.exe","autoit3.exe","autorun.ini","autorun.inf","autorun.pif","autorun.exe","autorun.bat","autorun.cmd","autorun.hta","avpo.exe","BOOTEX.log","ctfmon.exe","copy.exe","file*.chk","host.exe","Heap41a","host.exe","imvo.exe","IndexerVolumeGuid","just.exe","Macromedia_Setup.exe","Microsoft.exe","mscalc.exe","msvcr71.dl","My Pictures.lnk","notepad.vbe","ntde1ect.com","nideiect.com","ntdelect.com","Nouveau dossier.lnk","New Folder.exe","New Folder","Nouveau.exe","oso.exe","Porn.exe","Ravmon.exe","RavMonE.exe","RVHost.exe","spoclsv.exe","showmyhey.exe","soundmix.exe","semo2X.exe","Thumbs.db","utdetect.com","VBS_RESULOWS.A","windows.scr","x.mpeg"};
		public readonly string[] suspecte_folder_list = {"System Volume Information","RECYCLER","RECYCLED","$Recycler","$RECYCLE.BIN","$RECYCLEBIN","My Pictures","FILE.*","FOUND.*","Skype","Skypee","Google"};
	
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		
		void Button1Click(object sender, EventArgs e)
		{
			DirectoryInfo usb_path = new DirectoryInfo(comboBox1.Text);
			
			Process p = new Process();
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			//p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.FileName = "cmd";
			
			// "cmd", @"/C del %temp%\. %windir%\prefetch\. %windir%\temp\. /F /Q /S"
			textBox.Text = "";
			progressBar1.Value = 0;
			/*
			cb1.Checked = false; // clean %temp%
			cb2.Checked = false; // unload
			cb3.Checked = false; // chkdsk
			cb4.Checked = false; // copy
			cb5.Checked = false; // del file
			cb6.Checked = false; // del folder
			cb7.Checked = false; // unhide all
			*/
			
			
			// clean %temp%
			if (cb1.Checked){
				cb1.Font = new Font(cb1.Font, FontStyle.Bold);
				p.StartInfo.Arguments = @"/C del %temp%\. %windir%\prefetch\. %windir%\temp\. /F /Q /S";
				p.Start(); p.WaitForExit();
				progressBar1.Value += 10;
				cb1.Font = new Font(cb1.Font, FontStyle.Regular);
			}
			
			
			
			// unload
			if (cb2.Checked){
				cb2.Font = new Font(cb2.Font, FontStyle.Bold);
				p.StartInfo.Arguments = @"/C taskkill /im wscript.exe /f /t";
				p.Start(); p.WaitForExit();
				p.StartInfo.Arguments = @"/C taskkill /im showmyhey.exe /f /t";
				p.Start(); p.WaitForExit();
				p.StartInfo.Arguments = @"/C taskkill /im microsoft.exe /f /t";
				p.Start(); p.WaitForExit();
				p.StartInfo.Arguments = @"/C taskkill /im msiexec.exe /f /t";
				p.Start(); p.WaitForExit();
				p.StartInfo.Arguments = @"/C taskkill /im mspaint.exe /f /t";
				p.Start(); p.WaitForExit();
				p.StartInfo.Arguments = @"/C taskkill /im mscalc.exe /f /t";
				p.Start(); p.WaitForExit();
				p.StartInfo.Arguments = @"/C taskkill /im calc.exe /f /t";
				p.Start(); p.WaitForExit();
				p.StartInfo.Arguments = @"/C taskkill /im autoit3.exe /f /t";
				p.Start(); p.WaitForExit();
				progressBar1.Value += 10;
				cb2.Font = new Font(cb2.Font, FontStyle.Regular);
			}
			
			// chkdsk
			if (cb3.Checked){
				cb3.Font = new Font(cb3.Font, FontStyle.Bold);
				//p.StartInfo.Arguments = @"/C chkdsk /f /x " + comboBox1.Text + ":";
				//p.Start(); p.WaitForExit();
				progressBar1.Value += 10;
				cb3.Font = new Font(cb3.Font, FontStyle.Regular);
			}
			
			// copy
			if (cb4.Checked){
				cb4.Font = new Font(cb4.Font, FontStyle.Bold);		
					//p.StartInfo.Arguments = @"/C chkdsk /f /x " + comboBox1.Text + ":";
					//p.Start(); p.WaitForExit();
				progressBar1.Value += 10;
				cb4.Font = new Font(cb4.Font, FontStyle.Regular);
			}
			

			
			// delete suspect file
			if (cb5.Checked){
				cb5.Font = new Font(cb5.Font, FontStyle.Bold);
				foreach(var suspected_file in suspecte_file_list){
					FileInfo[] files = usb_path.GetFiles(suspected_file, SearchOption.AllDirectories);
					foreach(var item in files){
						//File.Delete(item.FullName);
						textBox.Text = textBox.Text + "DELETE FILE" + item.FullName + "\r\n";				
					}
				}
				progressBar1.Value += 10;
				cb5.Font = new Font(cb5.Font, FontStyle.Regular);
			}
			
			

			// delete suspect folder
			if (cb6.Checked){
				cb6.Font = new Font(cb6.Font, FontStyle.Bold);
				foreach(var suspected_folder in suspecte_folder_list){
					FileInfo[] folders = usb_path.GetFiles(suspected_folder, SearchOption.AllDirectories);
					foreach(var item in folders){
						//File.Delete(item.FullName);
						textBox.Text = textBox.Text + "DELETE FOLDER" + item.FullName + "\r\n";				
					}
				}
				progressBar1.Value += 10;
				cb6.Font = new Font(cb6.Font, FontStyle.Regular);
				
			}
			
			
			
			// unhide all
			if (cb7.Checked){
				cb7.Font = new Font(cb7.Font, FontStyle.Bold);
				FileInfo[] unhide_files = usb_path.GetFiles("*.*", SearchOption.AllDirectories);
				foreach(var item in unhide_files){
					try{
						File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Hidden );
						textBox.Text = textBox.Text + "UNHIDE " + item.FullName + "\r\n";
					}
					catch(InvalidCastException i)
					{
						textBox.Text = textBox.Text + "Error: " + i.Source + "\r\n";
					}
				}
				progressBar1.Value += 10;
				cb7.Font = new Font(cb7.Font, FontStyle.Regular);
			}
			
			progressBar1.Value = 0;
			textBox.Text = textBox.Text + "[ THE END ]\r\n";
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			DriveInfo[] allDrives = DriveInfo.GetDrives();
			foreach(DriveInfo d in allDrives)
				comboBox1.Items.Add(d.Name);
			
			//var watcher = new ManagementEventWatcher();
	
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked){
				cb1.Checked = true; // clean %temp%
				cb2.Checked = true; // unload
				cb3.Checked = true; // chkdsk
				cb4.Checked = true; // copy
				cb5.Checked = true; // del file
				cb6.Checked = true; // del folder
				cb7.Checked = true; // unhide all
			}else{
				cb1.Checked = false; // clean %temp%
				cb2.Checked = false; // unload
				cb3.Checked = false; // chkdsk
				cb4.Checked = false; // copy
				cb5.Checked = false; // del file
				cb6.Checked = false; // del folder
				cb7.Checked = false; // unhide all
			}
		}
		
	}
}
