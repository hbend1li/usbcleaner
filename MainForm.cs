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
		public readonly string app_titel = "USB Cleaner build 10";
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
			"08c5ed1731fde99dcf34019529d01381",//::eicar.txt virus test
			"ce61092591917a1a0b658dd347db59d7",//:224256:BackDoor.IRC.NgrBot.42
			"5094afba53d17c0f36d79feecd64c98c",//:789230:HEUR.Trojan-Downloader.Script.Generic
			"d1ab72db2bedd2f255d35da3da0d4b16",//:141824:runsc.exe
			"902a52357b522df16adb57a1222a5db6",//:669:Trojan.WinLNK.Agent.jb
			"9e267f980ca27799369497d6831779ae",//:32768:04
			"a2fecccd2f987f4463b598a1ee6bc5c3",//:393208:Worm.VBS.Agent.ft
			"84a5746202decee74d907a37015a01d4",//:136505:googleupdate.a3x
			"76d8da3d285176a523e605dc5b9e7bef",//:347648:Photo03.scr
			"1d1d18589a688579faf026dfe822798f",//:191314:1d1d18589a688579faf026dfe822798f.exe
		};
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			InitializeComponent();
			
			this.Text = app_titel;
			
			//this.WindowState = FormWindowState.Minimized;
			//this.ShowInTaskbar = false;
			Hide();
			
			// install to C:\USB Cleaner
			try
			{
				string path= @"%ProgramFiles%\USB Cleaner";
				string file= @"\USB Cleaner.exe";

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				
				//var di = new DirectoryInfo(path);
				//di.Attributes |= FileAttributes.Hidden;
				
				if (!File.Exists (path + file))
					File.Copy(Application.ExecutablePath, path + file);

				Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
				key.SetValue("USB Cleaner", path + file);
				
			}
			catch
			{
				textBox.Text += "Field to install !!!\r\n\r\n";
			}
			
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
			
		public static List<string> GetAllFilesFromFolder(string root, bool searchSubfolders) {
		    Queue<string> folders = new Queue<string>();
		    List<string> files = new List<string>();
		    folders.Enqueue(root);
		    while (folders.Count != 0) {
		        string currentFolder = folders.Dequeue();
		        try {
		            string[] filesInCurrent = System.IO.Directory.GetFiles(currentFolder, "*.*", System.IO.SearchOption.TopDirectoryOnly);
		            files.AddRange(filesInCurrent);
		        }
		        catch {
		            // Do Nothing
		        }
		        try {
		            if (searchSubfolders) {
		                string[] foldersInCurrent = System.IO.Directory.GetDirectories(currentFolder, "*.*", System.IO.SearchOption.TopDirectoryOnly);
		                foreach (string _current in foldersInCurrent) {
		                    folders.Enqueue(_current);
		                }
		            }
		        }
		        catch {
		            // Do Nothing
		        }
		    }
		    return files;
	    }

		void CleanUSB(string drive)
		{
			label3.Font = new Font(label3.Font, FontStyle.Regular);
			if (comboBox1.Text != "")
			{
				button1.Enabled = false;
				comboBox1.Enabled = false;
				bool hide = false;
				if (this.WindowState == FormWindowState.Minimized)
				{
					//this.ShowInTaskbar = true;
					//this.WindowState = FormWindowState.Normal;
					Show();
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
					    File.Copy(Application.ExecutablePath, usb_path + @"\USB Cleaner.exe", true);
					}
					catch (IOException copyError){
					    Console.WriteLine(copyError.Message);
					}
					
					progressBar1.Value = 40;
					//cb4.Font = new Font(cb4.Font, FontStyle.Regular);
				}
											
				label2.Font = new Font(label2.Font, FontStyle.Bold);
						
				int i = 0;
				List<string> files = GetAllFilesFromFolder(comboBox1.Text, true );
						
				foreach (var element in files) {
					
					// Debug.WriteLine(element);
				
					try
					{
						
						this.Text = element;
						progressBar1.Value = (i*100)/files.Count;
						i += 1;
						Application.DoEvents();
								
						try{
							
							if (cb5.Checked)
								if (Array.Exists(suspecte_file_list, x => x == element ))
								{
									File.Delete(element);
									textBox.Text += "DELETE FILE: " + element + "\r\n";
								}
							
							if (cb6.Checked)
								if (Array.Exists(suspecte_folder_list, x => x == element ))
								{
									File.Delete(element);
									textBox.Text += "DELETE FOLDER: " + element + "\r\n";
								}
							
							if (cb7.Checked)
								if ((new FileInfo(element).Length) < 1000000)
								if (Array.Exists(suspecte_MD5file_list, x => x == CalculatMD5Hash(element)))
								{
									File.Delete(element);
									textBox.Text += "MD5 Detect: " + element + "\r\n";
								}
							
							if(cb8.Checked)
								if ((File.GetAttributes(element) & FileAttributes.Hidden)==FileAttributes.Hidden)
								{
									File.SetAttributes(element, File.GetAttributes(element) & ~FileAttributes.Hidden );
									textBox.Text += "Unhide: " + element + "\r\n";
								}
						}
						catch(InvalidCastException z)
						{
							textBox.Text += "Error: " + z.Source + "\r\n";
						}						
					}
					catch
					{
						textBox.Text += "Access denied: " + element + "\r\n";
					}
				}
				
				label2.Font = new Font(label2.Font, FontStyle.Regular);

				
				label3.Font = new Font(label3.Font, FontStyle.Bold);
				
				progressBar1.Value = 0;
				this.Text = app_titel;
				textBox.Text = textBox.Text + "=[ THE END ]============\r\n\r\n\r\n";
				comboBox1.Enabled = true;
				button1.Enabled = true;
				timer1.Enabled = true;
				if (hide)
				{
					Hide();
				}
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
		void NotifyIcon1MouseClick(object sender, MouseEventArgs e)
		{
			Show();
		}
		void TextBoxDoubleClick(object sender, EventArgs e)
		{
			textBox.Text = "";
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}
	}
}
