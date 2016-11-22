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
		public static string insered_drive;
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
			
			if (comboBox1.Text != "")
			{
				button1.Enabled = false;
				comboBox1.Enabled = false;
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
							
				// unload
				if (cb1.Checked){
					//cb1.Font = new Font(cb1.Font, FontStyle.Bold);
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
					progressBar1.Value = 10;
					//cb1.Font = new Font(cb1.Font, FontStyle.Regular);
				}
				
				// clean %temp%
				if (cb2.Checked){
					//cb2.Font = new Font(cb2.Font, FontStyle.Bold);
					p.StartInfo.Arguments = @"/C del %temp%\. %windir%\prefetch\. %windir%\temp\. /F /Q /S";
					p.Start(); p.WaitForExit();
					progressBar1.Value = 20;
					//cb2.Font = new Font(cb2.Font, FontStyle.Regular);
				}
				
				// chkdsk
				if (cb3.Checked){
					label1.Font = new Font(label1.Font, FontStyle.Bold);
					Application.DoEvents();
					p.StartInfo.Arguments = @"/C chkdsk /f /x " + comboBox1.Text + ":";
					p.Start(); p.WaitForExit();
					progressBar1.Value = 30;
					label1.Font = new Font(label1.Font, FontStyle.Regular);
					Application.DoEvents();
				}
				
				// copy
				if (cb4.Checked){
					//cb4.Font = new Font(cb4.Font, FontStyle.Bold);
					if (File.Exists(usb_path + "USB Cleaner.exe"))
						File.Delete(usb_path + "USB Cleaner.exe");
					textBox.Text += "COPY " + System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + " to USB\r\n";
					File.Copy(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, usb_path + System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe");
					
					progressBar1.Value = 40;
					//cb4.Font = new Font(cb4.Font, FontStyle.Regular);
				}
				
	
				
				// delete suspect file
				if (cb5.Checked){
					label2.Font = new Font(label2.Font, FontStyle.Bold);
					Application.DoEvents();
					foreach(var suspected_file in suspecte_file_list){
						FileInfo[] files = usb_path.GetFiles(suspected_file, SearchOption.AllDirectories);
						foreach(var item in files){
							try{
								File.Delete(item.FullName);
								textBox.Text += "DELETE FILE " + item.FullName + "\r\n";
							}
							catch(InvalidCastException i)
							{
								textBox.Text += "Error: " + i.Source + "\r\n";
							}
							
						}
					}
					progressBar1.Value = 50;
					label2.Font = new Font(label2.Font, FontStyle.Regular);
					Application.DoEvents();
				}
				
				
	
				// delete suspect folder
				if (cb6.Checked){
					label3.Font = new Font(label3.Font, FontStyle.Bold);
					Application.DoEvents();
					foreach(var suspected_folder in suspecte_folder_list){
						FileInfo[] folders = usb_path.GetFiles(suspected_folder, SearchOption.AllDirectories);
						foreach(var item in folders){
							try{
								File.Delete(item.FullName);
								textBox.Text += "DELETE FOLDER" + item.FullName + "\r\n";	
							}
							catch(InvalidCastException i)
							{
								textBox.Text += "Error: " + i.Source + "\r\n";
							}
							
						}
					}
					progressBar1.Value = 60;
					label3.Font = new Font(label3.Font, FontStyle.Regular);
					Application.DoEvents();
					
				}
				
				
				
				// unhide all
				if (cb7.Checked){
					label4.Font = new Font(label4.Font, FontStyle.Bold);
					Application.DoEvents();
					FileInfo[] unhide_files = usb_path.GetFiles("*.*", SearchOption.AllDirectories);
					foreach(var item in unhide_files){
						try{
							File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Hidden );
							textBox.Text = textBox.Text + "UNHIDE " + item.FullName + "\r\n";
						}
						catch(InvalidCastException i)
						{
							textBox.Text += "Error: " + i.Source + "\r\n";
						}
					}
					progressBar1.Value = 70;
					label4.Font = new Font(label4.Font, FontStyle.Regular);
					Application.DoEvents();
				}
				
				progressBar1.Value = 100;
				textBox.Text = textBox.Text + "[ THE END ]\r\n";
				comboBox1.Enabled = true;
				button1.Enabled = true;
			}
		}
		
		
		void USBAdded(object sender, EventArgs e)
		{
			
			//usb_cleaner.MainForm.ActiveForm.com
			//MainForm.comboBox1.Text = "USB";
			//textBox.Text += " USB Detect ";
			//Debug.WriteLine ("detect ");
			//select_usb(" USB Detect ");
			

			ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE InterfaceType = \"USB\"");
			foreach (ManagementObject queryObj in searcher.Get())
			{
				foreach (ManagementObject b in queryObj.GetRelated("Win32_DiskPartition")){
					foreach (ManagementBaseObject c in b.GetRelated("Win32_LogicalDisk")){
						
						Debug.WriteLine ("{0}", c ["Name"]);
						
						insered_drive = c ["Name"].ToString() + @"\";
						
						//comboBox1.Text = c ["Name"].ToString() + @"\";
						//textBox.Text += c ["Name"].ToString() + "\\ \r\n";
					}
				}
			}
		}	
				
		void MainFormLoad(object sender, EventArgs e)
		{
			this.Size = new System.Drawing.Size(300, 210);
			comboBox1.Items.Clear();
			DriveInfo[] allDrives = DriveInfo.GetDrives();
			foreach(DriveInfo d in allDrives)
				if (d.Name != @"C:\") comboBox1.Items.Add(d.Name);
			
			var watcher = new ManagementEventWatcher();
			var query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType=2");
			watcher.EventArrived += new EventArrivedEventHandler(USBAdded);
			watcher.Query = query;
			watcher.Start();	
		}
		
		void ComboBox1Click(object sender, EventArgs e)
		{
			comboBox1.Items.Clear();
			DriveInfo[] allDrives = DriveInfo.GetDrives();
			foreach(DriveInfo d in allDrives)
				if (d.Name != @"C:\") comboBox1.Items.Add(d.Name);
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			if (insered_drive != "")
			{
				comboBox1.Text = insered_drive;
				this.WindowState = FormWindowState.Normal;
				Button1Click(sender, e);
				insered_drive = "";
			}
		}
		void PictureBox1Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/hbendalibraham/usbcleaner");
		}
		void NotifyIcon1MouseDoubleClick(object sender, MouseEventArgs e)
		{
			//this.Show();
			//this.MainForm.WindowState == FormWindowState.Normal;
			
		}
		void Button2Click(object sender, EventArgs e)
		{
		
			if (this.Size.Height == 210)
				this.Size = new System.Drawing.Size(300, 511);
			else 
				this.Size = new System.Drawing.Size(300, 210);
			
		}
		void Button3Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
