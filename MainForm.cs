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
using System.Security.Cryptography;

namespace usb_cleaner
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	/// 
	
	

	public partial class MainForm : Form
	{
		public static string insered_drive;
		public readonly string[] suspecte_file_list = {
			"*.lnk",
			"~$*.*",
			"*.a3x",
			"*.tmp",
			"*.qgb",
			"auto.exe",
			"autoit3.exe",
			"autorun.ini",
			"autorun.inf",
			"autorun.pif",
			"autorun.exe",
			"autorun.bat",
			"autorun.cmd",
			"autorun.hta",
			"avpo.exe",
			"BOOTEX.log",
			"ctfmon.exe",
			"copy.exe",
			"file*.chk",
			"host.exe",
			"Heap41a",
			"host.exe",
			"imvo.exe",
			"IndexerVolumeGuid",
			"just.exe",
			"Macromedia_Setup.exe",
			"Microsoft.exe",
			"mscalc.exe",
			"msvcr71.dl",
			"My Pictures.lnk",
			"notepad.vbe",
			"ntde1ect.com",
			"nideiect.com",
			"ntdelect.com",
			"Nouveau dossier.lnk",
			"New Folder.exe",
			"New Folder",
			"Nouveau.exe",
			"oso.exe",
			"Porn.exe",
			"Ravmon.exe",
			"RavMonE.exe",
			"RVHost.exe",
			"spoclsv.exe",
			"showmyhey.exe",
			"soundmix.exe",
			"semo2X.exe",
			"Thumbs.db",
			"utdetect.com",
			"VBS_RESULOWS.A",
			"windows.scr",
			"x.mpeg"
		};
		public readonly string[] suspecte_folder_list = {
			"System Volume Information",
			"RECYCLER",
			"RECYCLED",
			"$Recycler",
			"$RECYCLE.BIN",
			"$RECYCLEBIN",
			"My Pictures",
			"FILE.*",
			"FOUND.*",
			"Skype",
			"Skypee",
			"Google"
		};
		
		public readonly string[] suspecte_MD5file_list = {
			"08c5ed1731fde99dcf34019529d01381", // eicar.txt virus test
		};
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			InitializeComponent();
			
			this.WindowState = FormWindowState.Minimized;
			this.ShowInTaskbar = false;
			
			// restore setting	
			
			
			var watcher = new ManagementEventWatcher();
			var query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType=2");
			watcher.EventArrived += new EventArrivedEventHandler(USBAdded);
			watcher.Query = query;
			watcher.Start();
		}
		
		public string CalculatMD5Hash(string CalculatMD5Hash)
		{
			//using (var md5 = MD5.Create())
			//:	return md5.ComputeHash(File.ReadAllBytes(filename)).toString();
			
  			// MD5 Hash
			MD5 md5 = MD5.Create();
			byte[] hash = md5.ComputeHash(File.ReadAllBytes(CalculatMD5Hash));
			string sb = null;
			for (int i = 0; i< hash.Length; i++)
			{
				sb += hash[i].ToString("X2");
			}
			return sb.ToLower();

		}
		
		void USBAdded(object sender, EventArgs e)
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE InterfaceType = \"USB\"");
			foreach (ManagementObject queryObj in searcher.Get())
			{
				foreach (ManagementObject b in queryObj.GetRelated("Win32_DiskPartition")){
					foreach (ManagementBaseObject c in b.GetRelated("Win32_LogicalDisk")){
						Debug.WriteLine ("{0}", c ["Name"]);
						insered_drive = c ["Name"].ToString() + @"\";
					}
				}
			}
		}

		void CleanUSB(string drive)
		{
			if (comboBox1.Text != "")
			{
				button1.Enabled = false;
				comboBox1.Enabled = false;
				bool hide = false;
				if (this.WindowState == FormWindowState.Minimized)
				{
					this.WindowState = FormWindowState.Normal;
					hide = true;
				}
				DirectoryInfo usb_path = new DirectoryInfo(drive);
				
				Process p = new Process();
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.FileName = "cmd";
				progressBar1.Value = 0;
							
				// unload
				if (cb1.Checked){
					textBox.Text += "Task KILL: \r\n";
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
					textBox.Text += "Delete TMP: \r\n";
					//cb2.Font = new Font(cb2.Font, FontStyle.Bold);
					p.StartInfo.Arguments = @"/C del %temp%\. %windir%\prefetch\. %windir%\temp\. /F /Q /S";
					p.Start(); p.WaitForExit();
					progressBar1.Value = 20;
					//cb2.Font = new Font(cb2.Font, FontStyle.Regular);
				}
				
				// chkdsk
				if (cb3.Checked){
					textBox.Text += "CHKDSK: \r\n";
					label1.Font = new Font(label1.Font, FontStyle.Bold);
					Application.DoEvents();
					p.StartInfo.Arguments = @"/C chkdsk /f /x " + comboBox1.Text + ":";
					p.Start();
					string StandardOutput = p.StandardOutput.ReadToEnd();
					p.WaitForExit();
					textBox.Text += StandardOutput + "\r\n";
					progressBar1.Value = 30;
					label1.Font = new Font(label1.Font, FontStyle.Regular);
					Application.DoEvents();
				}
				
				// copy
				if (cb4.Checked){
					textBox.Text += "Copy FILE: \r\n";
					//cb4.Font = new Font(cb4.Font, FontStyle.Bold);
					//textBox.Text += "COPY " + System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + " to USB\r\n";
					try{
					    File.Copy(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, usb_path + System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", true);
					}
					catch (IOException copyError){
					    Console.WriteLine(copyError.Message);
					}
					
					progressBar1.Value = 40;
					//cb4.Font = new Font(cb4.Font, FontStyle.Regular);
				}
				
	
				
				// delete suspect file
				if (cb5.Checked){
					textBox.Text += "Suspect FILE: \r\n";
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
					textBox.Text += "Suspect DIR: \r\n";					
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
				
				if (cb7.Checked){
					
					textBox.Text += "MD5: \r\n";					
					label2.Font = new Font(label2.Font, FontStyle.Bold);
					Application.DoEvents();					

					FileInfo[] MD5files = usb_path.GetFiles("*.*", SearchOption.AllDirectories);
					foreach(var item in MD5files){
						if ((new FileInfo(item.FullName).Length) < 1000000)
						{
							textBox.Text += item.FullName;
							//Debug.WriteLine(":" + CalculatMD5Hash(item.FullName));
							try{
								if (Array.Exists(suspecte_MD5file_list, x => x == CalculatMD5Hash(item.FullName)))
								{
									//File.Delete(item.FullName);
									textBox.Text += "------------[ fond ]";
								}
							}
							catch(InvalidCastException i)
							{
								textBox.Text += "------------[ Err :" + i.Source + " ]";
							}
							textBox.Text += "\r\n";
							Application.DoEvents();
						}
					}

					progressBar1.Value = 70;
					label2.Font = new Font(label2.Font, FontStyle.Regular);
					Application.DoEvents();
				}
				
				
				// unhide all
				if (cb8.Checked){
					textBox.Text += "Unhide FILE: \r\n";
					label4.Font = new Font(label4.Font, FontStyle.Bold);
					Application.DoEvents();
					FileInfo[] unhide_files = usb_path.GetFiles("*.*", SearchOption.AllDirectories);
					foreach(var item in unhide_files){
						try{
							if ((File.GetAttributes(item.FullName) & FileAttributes.Hidden)==FileAttributes.Hidden)
							{
								File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Hidden );
								textBox.Text = textBox.Text + item.FullName + "\r\n";
							}
						}
						catch(InvalidCastException i)
						{
							textBox.Text += "Error: " + i.Source + "\r\n";
						}
					}
					progressBar1.Value = 80;
					label4.Font = new Font(label4.Font, FontStyle.Regular);
					Application.DoEvents();
				}
				
				progressBar1.Value = 0;
				textBox.Text = textBox.Text + "=[ THE END ]============\r\n\r\n\r\n";
				comboBox1.Enabled = true;
				button1.Enabled = true;
				timer1.Enabled = true;
				if (hide)
				{
					this.WindowState = FormWindowState.Minimized;
				}
				//this.ShowInTaskbar = false;
				insered_drive = null;
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			CleanUSB(comboBox1.Text);			
		}
		
				
		void MainFormLoad(object sender, EventArgs e)
		{
			this.Size = new System.Drawing.Size(300, 210);
			comboBox1.Items.Clear();
			DriveInfo[] allDrives = DriveInfo.GetDrives();
			foreach(DriveInfo d in allDrives)
				if (d.Name != @"C:\") comboBox1.Items.Add(d.Name);
			

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
			if (insered_drive != null)
			{
				timer1.Enabled = false;
				comboBox1.Text = insered_drive;
				CleanUSB(insered_drive);
			}
		}
		
		void PictureBox1Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/hbendalibraham/usbcleaner");
		}

		void Button2Click(object sender, EventArgs e)
		{
			if (this.Size.Height == 210)
				this.Size = new System.Drawing.Size(300, 530);
			else 
				this.Size = new System.Drawing.Size(300, 210);
			
		}
		void Button3Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
			this.ShowInTaskbar = false;
		}
		void MainFormMinimumSizeChanged(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
			this.ShowInTaskbar = false;
		}
		void NotifyIcon1MouseClick(object sender, MouseEventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal){
				this.WindowState = FormWindowState.Minimized;
				this.ShowInTaskbar = false;
			}else{
				this.WindowState = FormWindowState.Normal;
				this.ShowInTaskbar = true;
			}
		}
		void TextBoxDoubleClick(object sender, EventArgs e)
		{
			textBox.Text = "";
		}
	}
}
