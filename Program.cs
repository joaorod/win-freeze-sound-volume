using System;
using System.Threading;
using NAudio.CoreAudioApi;

namespace FixMicVol
{
    class Program
    {
        static void CheckResetMicVolume(float targetVolume = 0.4f)
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
                if (device != null)
                {
                    var volume = device.AudioEndpointVolume;
                    if (volume.MasterVolumeLevelScalar != targetVolume)
                    {
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Changing {device.FriendlyName} volume from {volume.MasterVolumeLevelScalar} to {targetVolume}");
                        volume.MasterVolumeLevelScalar = targetVolume;
                    }
                }
            }
        }

        static void Main(string[] args)
        {

            int targetVolume = 40;

            if (args.Length > 0 && int.TryParse(args[0], out targetVolume) && (targetVolume < 0 || targetVolume > 100))
                targetVolume = 40;

            for (; ; )
            {
                CheckResetMicVolume(targetVolume / 100f);
                Thread.Sleep(1000);
            }

        }
    }
}
