using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorAudioConverter
{
    class Program
    {
        static void Main(string[] args)
        {

            SampleRateConverter converter = new SampleRateConverter(@"D:\Documents\visual studio 2015\Projects\EditorAudioConverter\EditorAudioConverter\wavs\");

            //converter.convertHighSample("16k.wav");
            //converter.convertHighSample("44k.wav");

            converter.convertLowSample("16k.wav");
            converter.convertLowSample("44k.wav");

            converter.replaceWavFiles();

            Console.ReadLine();
            
        }


    }
}
