using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio;
using NAudio.Wave;
using System.Threading;

namespace AutoPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    

    public partial class MainWindow : Window
    {
        MediaFoundationReader mediaReader;
        WaveOutEvent waveOut = new WaveOutEvent();
        SolidColorBrush defaultButtonColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD"));
        SolidColorBrush playingButtonColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFA47CA2"));
        public Thread FadeOutThread = null;

        bool Playing = false;

        public MainWindow()
        {
            InitializeComponent();
            ResetButtonColor();
            
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            //Playing = false;
            //on stop click, fades out current sound from the current volume level
            ResetButtonColor();
            if(FadeOutThread == null)
            {
                FadeOutThread = new Thread(FadeOut);
                FadeOutThread.Start();
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //handles successful abort of threads if closed when still running
            waveOut.Dispose();
            if(mediaReader != null)
            {
                mediaReader.Dispose();
            }
            if(FadeOutThread != null)
            {
                FadeOutThread.Abort();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //changes volume based on the slider
            waveOut.Volume = (float)volumeSlider.Value;
        }

        //function calls for the pads in all 12 keys
        private void AButton_Click(object sender, RoutedEventArgs e)
        {
            aButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            aButton.Background = playingButtonColor;    
            PlaySong("A");

        }

        private void ASharpButton_Click(object sender, RoutedEventArgs e)
        {
            aSharpButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            aSharpButton.Background = playingButtonColor; 
            PlaySong("A#");
            
            
        }

        private void BButton_Click(object sender, RoutedEventArgs e)
        {
            bButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            bButton.Background = playingButtonColor;     
            PlaySong("B");
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            cButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            cButton.Background = playingButtonColor;
            PlaySong("C");         
        }

        private void CSharpButton_Click(object sender, RoutedEventArgs e)
        {
            cSharpButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            cSharpButton.Background = playingButtonColor;
            PlaySong("C#");        
        }

        private void DButton_Click(object sender, RoutedEventArgs e)
        {
            dButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            dButton.Background = playingButtonColor;
            PlaySong("D");
        }

        private void DSharpButton_Click(object sender, RoutedEventArgs e)
        {
            dSharpButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            dSharpButton.Background = playingButtonColor;
            PlaySong("D#");
        }

        private void EButton_Click(object sender, RoutedEventArgs e)
        {
            eButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            eButton.Background = playingButtonColor;
            PlaySong("E");
        }

        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            fButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            fButton.Background = playingButtonColor;
            PlaySong("F");
        }

        private void FSharpButton_Click(object sender, RoutedEventArgs e)
        {
            fSharpButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            fSharpButton.Background = playingButtonColor;
            PlaySong("F#");
        }

        private void GButton_Click(object sender, RoutedEventArgs e)
        {
            gButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            gButton.Background = playingButtonColor;
            PlaySong("G");
        }

        private void GSharpButton_Click(object sender, RoutedEventArgs e)
        {
            gSharpButton.Background = playingButtonColor;
            Run();
            ResetButtonColor();
            gSharpButton.Background = playingButtonColor;
            PlaySong("G#");
        }

        private void PlaySong(String key)
        {
            //handles playing new song
            if (Playing == true)
            {
                waveOut.Stop();
                ResetButtonColor();
                //fades out old pad then starts new sound
                if(FadeOutThread != null)
                {
                    FadeOutThread.Abort();
                    FadeOutThread = null;
                }              
            }
            switch (key)
            {
                case "A":
                    mediaReader = new MediaFoundationReader("A Major.mp3");
                    break;
                case "A#":
                    mediaReader = new MediaFoundationReader("Bb Major.mp3");
                    break;
                case "B":
                    mediaReader = new MediaFoundationReader("B Major.mp3");
                    break;
                case "C":
                    mediaReader = new MediaFoundationReader("C Major.mp3");
                    break;
                case "C#":
                    mediaReader = new MediaFoundationReader("Db Major.mp3");
                    break;
                case "D":
                    mediaReader = new MediaFoundationReader("D Major.mp3");
                    break;
                case "D#":
                    mediaReader = new MediaFoundationReader("Eb Major.mp3");
                    break;
                case "E":
                    mediaReader = new MediaFoundationReader("E Major.mp3");
                    break;
                case "F":
                    mediaReader = new MediaFoundationReader("F Major.mp3");
                    break;
                case "F#":
                    mediaReader = new MediaFoundationReader("Gb Major.mp3");
                    break;
                case "G":
                    mediaReader = new MediaFoundationReader("G Major.mp3");
                    break;
                case "G#":
                    mediaReader = new MediaFoundationReader("Ab Major.mp3");
                    break;
            }   
            //starts playback on sound
            waveOut.Init(mediaReader);
            Playing = true;
            waveOut.Play();
            waveOut.Volume = (float)volumeSlider.Value;
        }

        //resets all buttons
        private void ResetButtonColor()
        {
            aButton.Background = defaultButtonColor;
            aSharpButton.Background = defaultButtonColor;
            bButton.Background = defaultButtonColor;
            cButton.Background = defaultButtonColor;
            cSharpButton.Background = defaultButtonColor;
            dButton.Background = defaultButtonColor;
            dSharpButton.Background = defaultButtonColor;
            eButton.Background = defaultButtonColor;
            fButton.Background = defaultButtonColor;
            fSharpButton.Background = defaultButtonColor;
            gButton.Background = defaultButtonColor;
            gSharpButton.Background = defaultButtonColor;
        }

        private void Run()
        {
            if (FadeOutThread == null && Playing == false)
            {
                return;
                
            }
            else if (FadeOutThread == null)
            {
                FadeOutThread = new Thread(FadeOut);
                FadeOutThread.Start();
                FadeOutThread.Join();
            }     
        }

        private void FadeOut()
        { 
                float currentVolume = (float)waveOut.Volume;
                while(true)
                {
                Thread.Sleep(500);
                currentVolume -= (float).1;
                //fades out from current volume of slider
                if (currentVolume <= 0 || waveOut.Volume == 0)
                {
                    waveOut.Stop();
                    FadeOutThread = null;
                    Playing = false;
                    break;
                }
                //after fade out, resets volume to the volume slider. 
                waveOut.Volume = currentVolume;
                 
                }
            }
            
    }

        


}
