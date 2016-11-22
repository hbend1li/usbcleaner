/*
 * Created by SharpDevelop.
 * User: freeman
 * Date: 11/21/2016
 * Time: 11:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace usb_cleaner
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox cb4;
		private System.Windows.Forms.CheckBox cb5;
		private System.Windows.Forms.CheckBox cb6;
		private System.Windows.Forms.CheckBox cb7;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.CheckBox cb3;
		private System.Windows.Forms.CheckBox cb2;
		private System.Windows.Forms.CheckBox cb1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Timer timer1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.cb4 = new System.Windows.Forms.CheckBox();
			this.cb5 = new System.Windows.Forms.CheckBox();
			this.cb6 = new System.Windows.Forms.CheckBox();
			this.cb7 = new System.Windows.Forms.CheckBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.cb3 = new System.Windows.Forms.CheckBox();
			this.cb2 = new System.Windows.Forms.CheckBox();
			this.cb1 = new System.Windows.Forms.CheckBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.textBox = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1MouseDoubleClick);
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(163, 205);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(105, 41);
			this.button1.TabIndex = 1;
			this.button1.Text = "Clean";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// cb4
			// 
			this.cb4.Checked = true;
			this.cb4.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb4.Location = new System.Drawing.Point(12, 102);
			this.cb4.Name = "cb4";
			this.cb4.Size = new System.Drawing.Size(178, 24);
			this.cb4.TabIndex = 2;
			this.cb4.Text = "Copy USB Cleaner to USB";
			this.cb4.UseVisualStyleBackColor = true;
			// 
			// cb5
			// 
			this.cb5.Checked = true;
			this.cb5.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb5.Location = new System.Drawing.Point(12, 132);
			this.cb5.Name = "cb5";
			this.cb5.Size = new System.Drawing.Size(147, 24);
			this.cb5.TabIndex = 4;
			this.cb5.Text = "Delete suspect file";
			this.cb5.UseVisualStyleBackColor = true;
			// 
			// cb6
			// 
			this.cb6.Checked = true;
			this.cb6.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb6.Location = new System.Drawing.Point(12, 162);
			this.cb6.Name = "cb6";
			this.cb6.Size = new System.Drawing.Size(147, 24);
			this.cb6.TabIndex = 5;
			this.cb6.Text = "Delete suspect folder";
			this.cb6.UseVisualStyleBackColor = true;
			// 
			// cb7
			// 
			this.cb7.Checked = true;
			this.cb7.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb7.Location = new System.Drawing.Point(12, 192);
			this.cb7.Name = "cb7";
			this.cb7.Size = new System.Drawing.Size(147, 24);
			this.cb7.TabIndex = 6;
			this.cb7.Text = "Unhide folder";
			this.cb7.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(153, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(133, 76);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.PictureBox1Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 252);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(256, 10);
			this.progressBar1.TabIndex = 8;
			// 
			// cb3
			// 
			this.cb3.Checked = true;
			this.cb3.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb3.Location = new System.Drawing.Point(12, 72);
			this.cb3.Name = "cb3";
			this.cb3.Size = new System.Drawing.Size(147, 24);
			this.cb3.TabIndex = 9;
			this.cb3.Text = "CHKDSK";
			this.cb3.UseVisualStyleBackColor = true;
			// 
			// cb2
			// 
			this.cb2.Checked = true;
			this.cb2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb2.Location = new System.Drawing.Point(12, 42);
			this.cb2.Name = "cb2";
			this.cb2.Size = new System.Drawing.Size(147, 24);
			this.cb2.TabIndex = 10;
			this.cb2.Text = "Clean %TEMP%";
			this.cb2.UseVisualStyleBackColor = true;
			// 
			// cb1
			// 
			this.cb1.Checked = true;
			this.cb1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb1.Location = new System.Drawing.Point(12, 12);
			this.cb1.Name = "cb1";
			this.cb1.Size = new System.Drawing.Size(147, 24);
			this.cb1.TabIndex = 11;
			this.cb1.Text = "Unload suspect process";
			this.cb1.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(163, 178);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(105, 21);
			this.comboBox1.TabIndex = 12;
			this.comboBox1.Click += new System.EventHandler(this.ComboBox1Click);
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(274, 12);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox.Size = new System.Drawing.Size(307, 250);
			this.textBox.TabIndex = 13;
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(12, 222);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(147, 24);
			this.checkBox1.TabIndex = 14;
			this.checkBox1.Text = "All scan";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button1;
			this.ClientSize = new System.Drawing.Size(593, 276);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.cb1);
			this.Controls.Add(this.cb2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.cb3);
			this.Controls.Add(this.cb7);
			this.Controls.Add(this.cb6);
			this.Controls.Add(this.cb5);
			this.Controls.Add(this.cb4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "USB Cleaner";
			this.Load += new System.EventHandler(this.MainFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
