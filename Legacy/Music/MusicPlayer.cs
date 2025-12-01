using System;
using System.IO;
using System.Reflection;
using NAudio.Wave;
namespace Legacy.Music{


    public static class MusicPlayer
    {
        private static WaveOutEvent _waveOut;
        private static AudioFileReader _audioFile;
        private static string _tempFilePath;
        private static string _currentResourceName;

        public static void Play(string resourceName = "")
        {
            if (resourceName == "")
                resourceName = $"Floor{Random.Shared.Next(1, 6)}.mp3";

            if (_waveOut != null)
            {
                _waveOut.PlaybackStopped -= OnPlaybackStopped;
                _waveOut.Stop();
                _waveOut.Dispose();
                _waveOut = null;
            }

            _audioFile?.Dispose();
            _audioFile = null;

            if (File.Exists(_tempFilePath))
            {
                try { File.Delete(_tempFilePath); } catch { }
            }

            _currentResourceName = resourceName;
            var assembly = Assembly.GetExecutingAssembly();
            string fullResourceName = assembly.GetName().Name + ".Music." + resourceName;

            var resourceStream = assembly.GetManifestResourceStream(fullResourceName);
            _tempFilePath = Path.GetTempFileName() + Path.GetExtension(resourceName);

            using (var fileStream = File.Create(_tempFilePath))
            {
                resourceStream.CopyTo(fileStream);
            }
            _audioFile = new AudioFileReader(_tempFilePath);
            _waveOut = new WaveOutEvent();
            _waveOut.Init(_audioFile);
            _waveOut.PlaybackStopped += OnPlaybackStopped;
            _waveOut.Play();

        }

        private static void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentResourceName))
            {
                _audioFile?.Dispose();
                _waveOut?.Dispose();
                if (!File.Exists(_tempFilePath))
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    string fullResourceName = assembly.GetName().Name + ".Music." + _currentResourceName;

                    using (var resourceStream = assembly.GetManifestResourceStream(fullResourceName))
                    {
                        if (resourceStream != null)
                        {
                            using (var fileStream = File.Create(_tempFilePath))
                            {
                                resourceStream.CopyTo(fileStream);
                            }
                        }
                    }
                }

                _audioFile = new AudioFileReader(_tempFilePath);
                _waveOut = new WaveOutEvent();
                _waveOut.Init(_audioFile);
                _waveOut.PlaybackStopped += OnPlaybackStopped;
                _waveOut.Play();
            }
        }
    } }

