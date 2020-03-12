using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Plugin.SimpleAudioPlayer;

namespace PrimeAssault.Helpers
{
    public class AudioHelper
    {
        //Creates a SimpleAudioPlayer object to be used for sound effects
        ISimpleAudioPlayer BattleMusic;
        ISimpleAudioPlayer AttackSE;
        ISimpleAudioPlayer DeathSE;
        ISimpleAudioPlayer MissSE;
        ISimpleAudioPlayer FeelGoodSE;

        //Assembles audioplayer to play the file that is included in the parameter
        public Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("PrimeAssault." + filename);
            return stream;
        }

        public AudioHelper(bool Music = false)
        {
            BattleMusic = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            if (Music && BattleMusic != null)
            {
                var stream = GetStreamFromFile("ff_music.mp3");
                BattleMusic.Load(stream);
            }
            else
            {
                //Defining the audioplayers
                AttackSE = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                DeathSE = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                MissSE = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                FeelGoodSE = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                if (AttackSE != null)
                {
                    //Assigning soundeffects to be played by AudioPlayer objects
                    var stream = GetStreamFromFile("attack_se.ogg");
                    AttackSE.Load(stream);

                    stream = GetStreamFromFile("miss_se.ogg");
                    MissSE.Load(stream);

                    stream = GetStreamFromFile("death_se.ogg");
                    DeathSE.Load(stream);

                    stream = GetStreamFromFile("feelgood.mp3");
                    FeelGoodSE.Load(stream);
                }

            }

        }

        public void Battle_Music()
        {
            if (BattleMusic != null)
                BattleMusic.Play();
        }

        public void Battle_Music_Pause()
        {
            if (BattleMusic != null)
                BattleMusic.Stop();
        }
        public void Attack_Sound()
        {
            if (AttackSE != null)
                AttackSE.Play();
        }

        public void Miss_Sound()
        {
            if (MissSE != null)
                MissSE.Play();
        }

        public void Death_Sound()
        {
            if (DeathSE != null)
                DeathSE.Play();
        }

        public void FeelGood_Sound()
        {
            if (FeelGoodSE != null)
                FeelGoodSE.Play();
        }

    }
}
