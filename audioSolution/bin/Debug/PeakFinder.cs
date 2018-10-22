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
		public static void run(string filename, long channels, Boolean testing) 
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
            string test = "C:\\Users\\s166268\\Documents\\SharpDevelop Projects\\audioSolution\\audioSolution\\beep.wav";
            
           
            // initialize file readers			
            var readerFL = new AudioFileReader(fileFL);
			var readerFR = new AudioFileReader(fileFR);
			var readerFC = new AudioFileReader(fileFC);
			var readerBS = new AudioFileReader(fileBS);
			var readerBL = new AudioFileReader(fileBL);
			var readerBR = new AudioFileReader(fileBR);
			var readerSL = new AudioFileReader(fileSL);
			var readerSR = new AudioFileReader(fileSR);
            var readerTest = new AudioFileReader(test);
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
            var playerTest = new WaveOut();
			// initialize volume meters
			var meterFL = new MeteringSampleProvider(readerFL);
			var meterFR = new MeteringSampleProvider(readerFR);
			var meterFC = new MeteringSampleProvider(readerFC);
			var meterBS = new MeteringSampleProvider(readerBS);
			var meterBL = new MeteringSampleProvider(readerBL);
			var meterBR = new MeteringSampleProvider(readerBR);
			var meterSL = new MeteringSampleProvider(readerSL);
			var meterSR = new MeteringSampleProvider(readerSR);
            var meterTest = new MeteringSampleProvider(readerTest);
			// start volume meters
            if (!testing)
            {
                meterFL.StreamVolume += (s, e) => value(e.MaxSampleValues[0], "Front Left active");
                meterFR.StreamVolume += (s, e) => value(e.MaxSampleValues[0], "Front Right active");
                meterFC.StreamVolume += (s, e) => value(e.MaxSampleValues[0], "Front Center active");
                meterBS.StreamVolume += (s, e) => value(e.MaxSampleValues[0], "Bass active");
                meterBL.StreamVolume += (s, e) => value(e.MaxSampleValues[0], "Back Left active");
                meterBR.StreamVolume += (s, e) => value(e.MaxSampleValues[0], "Back right active");
                meterSL.StreamVolume += (s, e) => value(e.MaxSampleValues[0], "Side left active");
                meterSR.StreamVolume += (s, e) => value(e.MaxSampleValues[0], "Side right active");
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
			else
            {
                /*
                meterTest.StreamVolume += (s, e) => value(e.MaxSampleValues[0], "testing");
                playerTest.Init(new SampleToWaveProvider(meterTest));
                playerTest.Play();
                */
                demo();
            }

			
			
			
		}
		
		
		public static void value(float value, string text) 
		{
            if (value > 0.1) 
			{ 
				Console.WriteLine(DateTime.Now + " : " + value);

                double max = 0.2;
                
                
                //driver.Send(new ToneRequest(8, 200, 1000));
            }
		}

        public static double intensityVolume(float volume, double maxVolume, double minVolume, double minIntensity)
        {
            if (volume <= minVolume)
            {
                return 0;
            }
            else
            {
                double intensity = ((1 - minIntensity) * volume + ((1 - minIntensity) * maxVolume) + (maxVolume - minVolume)) / (maxVolume - minVolume);
                return intensity;
            }
        }

        public static double intensityDemo(double distance, double maxDistance, double minIntensity)
        {
            if (distance >= maxDistance)
            {
                return 0;
            }
            else
            {
                double intensity = (((minIntensity - 1) * distance) / maxDistance) + 1;
                return intensity;
            }
        }

        public static void demo()
        {
            double a = 2;
            double b = 1.5;
            double interval = 2 * Math.PI / 7.0;
            double[] smallX = new double[8];
            double[] smallY = new double[8];

            double[] bigX = new double[8];
            double[] bigY = new double[8];

            int i = 0;            
            for (double t = 0.5 * Math.PI; t <= (2.5 * Math.PI); t += interval)
            {
                double x = a*1.5 * Math.Sin(t + Math.PI / 2);
                double y = b*1.5 * Math.Sin(t);
                //Console.WriteLine("(" + x + "," + y + ")");
                bigX[i] = x;
                bigY[i] = y;
                i++;
                System.Threading.Thread.Sleep(161);
            }
            i = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            double tFast = -0.2*Math.PI;
            //for (double t = 1.5 * Math.PI; t <= 3.5 * Math.PI; t += interval)
            for (double t = 0; t >= -6*Math.PI; t -= 0.0096)
            {                double x = a * Math.Sin(t + Math.PI / 2);
                double y = b * Math.Sin(t);
                double xFast = a * Math.Sin(tFast + Math.PI / 2);
                double yFast = b * Math.Sin(tFast);
                //Console.WriteLine("(" + x + "," + y + ")");
                
                distance(x, y, bigX[0], bigY[0], "front center");
                distance(x, y, bigX[1], bigY[1], "left front side");
                distance(x, y, bigX[2], bigY[2], "left side");
                distance(x, y, bigX[3], bigY[3], "left back side");
                distance(x, y, bigX[4], bigY[4], "right back side");
                distance(x, y, bigX[5], bigY[5], "right side");
                distance(x, y, bigX[6], bigY[6], "right front side");
                
                distance(xFast, yFast, bigX[0], bigY[0], "fast front center");
                distance(xFast, yFast, bigX[1], bigY[1], "fast left front side");
                distance(xFast, yFast, bigX[2], bigY[2], "fast left side");
                distance(xFast, yFast, bigX[3], bigY[3], "fast left back side");
                distance(xFast, yFast, bigX[4], bigY[4], "fast right back side");
                distance(xFast, yFast, bigX[5], bigY[5], "fast right side");
                distance(xFast, yFast, bigX[6], bigY[6], "fast right front side");

                tFast -= 2 * 0.0096;
                i++;
                System.Threading.Thread.Sleep(15);
            }
            stopwatch.Stop();
            var elapsed_time = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Time elapsed: " + elapsed_time);
        }
        
        public static void distance (double x, double y, double bigX, double bigY, String motor)
        {
            double d = Math.Sqrt(Math.Pow((x - bigX), 2) + Math.Pow((y - bigY), 2));
            if (d <= 1.7)
            {
                Console.WriteLine("distance " + motor + " : " + d + " intensity: " + intensityDemo(d, 1.7, 0.4));
            }
        }
	}
}
	
