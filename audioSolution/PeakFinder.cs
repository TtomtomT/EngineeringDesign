/*
 * Created by SharpDevelop.
 * User: s166268
 * Date: 25-9-2018
 * Time: 11:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Timers;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.MediaFoundation;
using NAudio;
using System.Diagnostics;
using System.Windows.Forms;
using ArduinoDriver;
using ArduinoUploader;
using ArduinoDriver.SerialProtocol;
using ArduinoUploader.Hardware;


namespace audioSolution
{
	/// <summary>
	/// CH01 = left front
	/// CH02 = right front
	/// CH03 = front center
	/// CH04 = bass
	/// CH05 = left back
	/// CH06 = right back
	/// CH07 = left side
	/// CH08 = right side
	/// </summary>
	public class PeakFinder
	{
		public static void run(string filename, long channels) 
		{
			// initialize files
			string fileFL = "C:\\Users\\s166268\\Documents\\SharpDevelop Projects\\audioSolution\\audioSolution\\minecraft.wavminecraft.CH01.wav";
			string fileFR = "C:\\Users\\s166268\\Documents\\SharpDevelop Projects\\audioSolution\\audioSolution\\minecraft.wavminecraft.CH02.wav";
			string fileFC = "C:\\Users\\s166268\\Documents\\SharpDevelop Projects\\audioSolution\\audioSolution\\minecraft.wavminecraft.CH03.wav";
			string fileBS = "C:\\Users\\s166268\\Documents\\SharpDevelop Projects\\audioSolution\\audioSolution\\minecraft.wavminecraft.CH04.wav";	
			string fileBL = "C:\\Users\\s166268\\Documents\\SharpDevelop Projects\\audioSolution\\audioSolution\\minecraft.wavminecraft.CH05.wav";
			string fileBR = "C:\\Users\\s166268\\Documents\\SharpDevelop Projects\\audioSolution\\audioSolution\\minecraft.wavminecraft.CH06.wav";
			string fileSL = "C:\\Users\\s166268\\Documents\\SharpDevelop Projects\\audioSolution\\audioSolution\\minecraft.wavminecraft.CH07.wav";	
			string fileSR = "C:\\Users\\s166268\\Documents\\SharpDevelop Projects\\audioSolution\\audioSolution\\minecraft.wavminecraft.CH08.wav";		
			// initialize file readers			
			var readerFL = new AudioFileReader(fileFL);
			var readerFR = new AudioFileReader(fileFR);
			var readerFC = new AudioFileReader(fileFC);
			var readerBS = new AudioFileReader(fileBS);
			var readerBL = new AudioFileReader(fileBL);
			var readerBR = new AudioFileReader(fileBR);
			var readerSL = new AudioFileReader(fileSL);
			var readerSR = new AudioFileReader(fileSR);
			// output sample rate (just for info)
			float[] buffer = new float[readerFL.WaveFormat.SampleRate];
			Console.WriteLine("sample rate: " + buffer.Length);
			// create sound players
			var playerFL = new WaveOut();
			var playerFR = new WaveOut();
			var playerFC = new WaveOut();
			var playerBS = new WaveOut();
			var playerBL = new WaveOut();
			var playerBR = new WaveOut();
			var playerSL = new WaveOut();
			var playerSR = new WaveOut();
			// initialize volume meters
			var meterFL = new MeteringSampleProvider(readerFL);
			var meterFR = new MeteringSampleProvider(readerFR);
			var meterFC = new MeteringSampleProvider(readerFC);
			var meterBS = new MeteringSampleProvider(readerBS);
			var meterBL = new MeteringSampleProvider(readerBL);
			var meterBR = new MeteringSampleProvider(readerBR);
			var meterSL = new MeteringSampleProvider(readerSL);
			var meterSR = new MeteringSampleProvider(readerSR);
			// start volume meters
			meterFL.StreamVolume += (s,e) => value(e.MaxSampleValues[0], "Front Left active");
			meterFR.StreamVolume += (s,e) => value(e.MaxSampleValues[0], "Front Right active");
			meterFC.StreamVolume += (s,e) => value(e.MaxSampleValues[0], "Front Center active");
			meterBS.StreamVolume += (s,e) => value(e.MaxSampleValues[0], "Bass active");
			meterBL.StreamVolume += (s,e) => value(e.MaxSampleValues[0], "Back Left active");
			meterBR.StreamVolume += (s,e) => value(e.MaxSampleValues[0], "Back right active");
			meterSL.StreamVolume += (s,e) => value(e.MaxSampleValues[0], "Side left active");
			meterSR.StreamVolume += (s,e) => value(e.MaxSampleValues[0], "Side right active");
			// initialize players
			playerFL.Init(new SampleToWaveProvider(meterFL));
			playerFR.Init(new SampleToWaveProvider(meterFR));
			playerFC.Init(new SampleToWaveProvider(meterFC));
			playerBS.Init(new SampleToWaveProvider(meterBS));
			playerBL.Init(new SampleToWaveProvider(meterBL));
			playerBR.Init(new SampleToWaveProvider(meterBR));
			playerSL.Init(new SampleToWaveProvider(meterSL));
			playerSR.Init(new SampleToWaveProvider(meterSR));
			// start sound players
			playerFL.Play();
			playerFR.Play();
			playerFC.Play();
			playerBS.Play();
			playerBL.Play();
			playerBR.Play();
			playerSL.Play();
			playerSR.Play();
			
		}
		
		
		public static void value(float maxValue, string text) 
		{
            if (maxValue > 0.1) 
			{ 
				Console.WriteLine(DateTime.Now + " : " + text);
                const ArduinoModel AttachedArduino = ArduinoModel.Micro;
                var driver = new ArduinoDriver(ArduinoModel.Micro, true);
                //driver.Send(new ToneRequest(8, 200, 1000));
            }
		}
	}
}
	
