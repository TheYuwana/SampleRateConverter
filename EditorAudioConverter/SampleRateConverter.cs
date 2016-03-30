using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class SampleRateConverter
{
    int lowSamplerate = 16000;
    int highSampleRate = 44000;
    string dirPath = "";
    string tempDirPath = "";
    DirectoryInfo dirInfo;
    List<string> newWavNames = new List<string>();

    public SampleRateConverter(string wavPath)
    {
        dirPath = wavPath;
        tempDirPath = dirPath + "\\temp\\";
        
        // Create Temporary Directory
        if (Directory.Exists(tempDirPath))
        {
            Directory.Delete(tempDirPath, true);
            dirInfo = Directory.CreateDirectory(tempDirPath);
        }
        else {

            dirInfo = Directory.CreateDirectory(tempDirPath);
        }
    }

    public void replaceWavFiles() {

        string[] filesInDir = Directory.GetFiles(dirPath);

        foreach (string dirFileName in newWavNames) {

            Console.WriteLine("Looking for " + dirPath + dirFileName + "...");
            if (filesInDir.Contains<string>(dirPath + dirFileName)) {
                Console.WriteLine("WE HAVE A MATCH!");
                Console.WriteLine("Temp file: " + tempDirPath + dirFileName);
                Console.WriteLine("replace file: " + dirPath + dirFileName);
                File.Replace(tempDirPath+ dirFileName, dirPath + dirFileName, null);
            }
        }

        dirInfo.Delete(true);
    }

    public void convertLowSample(string waveFile)
    {
        Console.WriteLine(" ==== Converting to LOW " + waveFile + " ==== ");
        convertAudioSample(lowSamplerate, waveFile);
    }

    public void convertHighSample(string waveFile)
    {
        Console.WriteLine(" ==== Converting to HIGH " + waveFile + " ==== ");
        convertAudioSample(highSampleRate, waveFile);
    }


    private void convertAudioSample(int sampleRate, string waveFile)
    {
        string inPath = dirPath + waveFile;
        string outPath = tempDirPath + "/" + waveFile;

        using (var reader = new WaveFileReader(inPath))
        {
            var outFormat = new WaveFormat(sampleRate, reader.WaveFormat.Channels);

            using (var resampler = new MediaFoundationResampler(reader, outFormat))
            {

                WaveFileWriter.CreateWaveFile(outPath, resampler);

                using (var newSample = new WaveFileReader(outPath))
                {
                    newWavNames.Add(waveFile);
                    Console.WriteLine("New sample rate: " + newSample.SampleCount + " FOR " + waveFile);
                }
            }
        }
    }


}