using System;
using System.Speech.Synthesis;
using System.IO;

namespace BarneyErrorGenerator
{
    # I initially planned to have multiple characters
    class Character
    {
        private Identity who { get; set; }
        private Identity whonew;

        private SpeechSynthesizer characterVoice = new SpeechSynthesizer();
        
        private int chances = 30;

        private StreamReader scriptOutput = new StreamReader("Barney.txt");

        public Character(string userWho)
        {
            Enum.TryParse<Identity>(userWho, out whonew);
            who = whonew;
            characterVoice.SetOutputToDefaultAudioDevice();
        }

        private void setWhoProperties()
        {
            switch (who)
            {
                case Identity.Barney:
                    {

                        PromptBuilder voiceSSML = new PromptBuilder();
                        
                        characterVoice.SelectVoiceByHints(VoiceGender.Male);
                        
                        
                        break;
                    }

                case Identity.Riff:
                    {
                        characterVoice.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Child);

                        break;
                    }
                case Identity.Brandon:
                    {

                        characterVoice.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);

                        break;
                    }
                default:
                    break;
            }

        }

        public void Speak()
        {
            //get the line of the script
            scriptOutput.DiscardBufferedData();
            scriptOutput.BaseStream.Seek(0, SeekOrigin.Begin);
            scriptOutput.BaseStream.Position = 0;
            PromptBuilder words = new PromptBuilder();
            words.AppendSsmlMarkup("<prosody pitch=\"+800Hz\">");
            //discard lines till we get the one we need

            string line = null;
            for (int i = 0; i <= -(chances - 30); i++)
            {
                line = scriptOutput.ReadLine();
            }
            words.AppendText(line);

            words.AppendSsmlMarkup("</prosody>");

            // pass it into the synth

            if (chances == 0)
            {
                chances = 31;
                characterVoice.Speak(words);
            }
            else
                characterVoice.SpeakAsync(words);
        }

        public void SyncSpeak()
        {
            //get the line of the script
            scriptOutput.DiscardBufferedData();
            scriptOutput.BaseStream.Seek(0, SeekOrigin.Begin);
            scriptOutput.BaseStream.Position = 0;
            PromptBuilder words = new PromptBuilder();
            if(chances!=30)
            words.AppendSsmlMarkup("<prosody pitch=\"+800Hz\">");

            //discard lines till we get the one we need

            string line = null;
            for (int i = 0; i <= -(chances - 30); i++)
            {
                line = scriptOutput.ReadLine();
            }
            words.AppendText(line);
            if(chances!=30)
            words.AppendSsmlMarkup("</prosody>");

            // pass it into the synth
            characterVoice.Speak(words);
        }
        public void HideSpeak()
        {
            PromptBuilder words = new PromptBuilder();
            if(chances>25)
                words.AppendSsmlMarkup("<prosody pitch =\"+800Hz\">That's not the code! Don't mess up again!</prosody>");
            else
                words.AppendSsmlMarkup("<prosody pitch =\"+800Hz\">I'm taking away the code box so you can't enter any bad codes!</prosody>");
            characterVoice.Speak(words);

        }
        public void PowerSpeak()
        {
            PromptBuilder words = new PromptBuilder();
            words.AppendSsmlMarkup("<prosody pitch=\"+800Hz\"> Stop pressing the power button right now!</prosody>");
            characterVoice.SpeakAsync(words);

        }

        public void ChanceLoss()
        {
            chances--;
        }
        public void ChanceLoss(int number)
        {
            if (chances <= 5)
                chances = 0;
            else
                chances -= number;
        }

        public int GetChance()
        {
            return chances;
        }

    }


    enum Identity
    {
        Barney,
        Riff,
        Brandon,
    }
}
