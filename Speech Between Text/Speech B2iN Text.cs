using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.IO;



namespace Speech_Between_Text
{
    public partial class Form1 : Form
    {

        Gateway ob = new Gateway();
        SpeechSynthesizer ss;
        SpeechRecognizer sRecognise = new SpeechRecognizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist;

        public Form1()
        {
           
            

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ss = new SpeechSynthesizer();
           // sRecognise.SpeechRecognized += sRecognise_SpeechRecognized;

        }
      /*  void sRecognise_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            inputTextBox.AppendText(e.Result.Text.ToString() + " ");
        }*/
        private void playButton_Click(object sender, EventArgs e)
        {
            if (inputTextBox.Text != "")
            {
                ss.Dispose();
                ss = new SpeechSynthesizer();
                ss.Rate = speedTrackBar.Value;
                ss.Volume = volumeTrackBar.Value;
                ss.SpeakAsync(inputTextBox.Text);
            }
            else
            {
                MessageBox.Show("Enter some Text!");
            }

        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {

                if (ss.State == SynthesizerState.Speaking)
                {
                   
                    ss.Pause();
                }
        
            }
            
          

        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                if (ss.State == SynthesizerState.Paused)
                {
                    ss.Resume();
                }
            }
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            ss.Rate = speedTrackBar.Value;
            ss.Volume = volumeTrackBar.Value;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Wave Files| *.wav";
            sfd.ShowDialog(); 
            ss.SetOutputToWaveFile(sfd.FileName);
            ss.Speak(inputTextBox.Text);
            ss.SetOutputToDefaultAudioDevice();
            MessageBox.Show("Recording Completed..", "T2S");
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                ss.Dispose();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (ss != null)
            {
                ss.Dispose();
            }
            inputTextBox.Clear();
            inputTextBox.Focus();
            startButton.Enabled = false;
            stopButtonRecognise.Enabled = true;
            clist = new Choices();






            //string readtext[] = ob.RetrieveInfoFrmoTextTable();
           // clist.Add(new string[] { ob.RetrieveInfoFrmoTextTable()});
                Grammar gr = new Grammar(new GrammarBuilder(clist));
            
                try
                {
                    
                    sre.RequestRecognizerUpdate();
                    sre.LoadGrammar(gr);
                    sre.SpeechRecognized += sre_SpeechRecognized;
                    sre.SetInputToDefaultAudioDevice();
                    sre.RecognizeAsync(RecognizeMode.Multiple);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            
           
            
        }
        
        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "dot":
                    inputTextBox.Text =".";
                    break;
                case "close":
                    Application.Exit();
                    break;
            }
            inputTextBox.Text+=e.Result.Text.ToString()+" ";
        }

        private void stopButtonRecognise_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            startButton.Enabled = true;
            stopButtonRecognise.Enabled = false;
        }

        private void inputButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog inputedFile = new OpenFileDialog();
            inputedFile.Filter = "Text Files|*.txt";

            if (inputedFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                inputTextBox.LoadFile(inputedFile.FileName, RichTextBoxStreamType.PlainText);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.csharphelp.com");
        }



      
    }
}
