/*
 * Created by SharpDevelop.
 * User: Tom van der Velden
 * Date: 24-9-2018
 * Time: 13:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using audioSolution;


namespace audioSolution
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		string file = "";
		long channels;
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
		void BtnSplitWavFilesClick(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog()
            {
                Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*",
                FilterIndex = 0,
                Multiselect = true
            };
			
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            	textOutputPath.Text = dlg.FileName;

		    
		}
		void Button1Click(object sender, EventArgs e)
		{
			if (textOutputPath.Text.Length > 1) 
			{
				file = textOutputPath.Text;
				textBox1.Text += "File split" + "\r\n";
	        	channels = WavFileSplitter.SplitWavFile(
	            textOutputPath.Text,
	            textOutputPath.Text);
				textBox1.Text += "Amount of channels: " + channels + "\r\n";
			} else 
			{ textBox1.Text += "Select a file first" + "\r\n"; }	
           
		}
		void Label1Click(object sender, EventArgs e)
		{
	
		}
		void GoClick(object sender, EventArgs e)
		{
            
            if (file.Length > 1)
			{
                axVLCPlugin21.playlist.play();
                PeakFinder.run(file, channels);
			} else 
			{
				textBox1.Text += "No file split yet" + "\r\n";
			}
		}

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openVideo = new OpenFileDialog();
            if (openVideo.ShowDialog() == DialogResult.OK)
            {
                var uri = new System.Uri(openVideo.FileName);
                var converted = uri.AbsoluteUri;
                axVLCPlugin21.playlist.add(converted, openVideo.SafeFileName, null);
            }
        }

        public void AppendText(String text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendText), new object[] { text });
                return;
            }
            this.textBox1.Text += text;
        }
    }
}
