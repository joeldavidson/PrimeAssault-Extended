﻿using System;
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
        ISimpleAudioPlayer AttackSE;
        ISimpleAudioPlayer DeathSE;
        ISimpleAudioPlayer MissSE;

        //Assembles audioplayer to play the file that is included in the parameter
        public Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("PrimeAssault." + filename);
            return stream;
        }

        public AudioHelper()
        {
            //Defining the audioplayers
            AttackSE = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            DeathSE = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            MissSE = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            //Assigning soundeffects to be played by AudioPlayer objects
            var stream = GetStreamFromFile("attack_se.ogg");
            //AttackSE.Load(stream);

            stream = GetStreamFromFile("miss_se.ogg");
            //MissSE.Load(stream);

            stream = GetStreamFromFile("death_se.ogg");
            //DeathSE.Load(stream);

        }

        public void Attack_Sound()
        {
            //AttackSE.Play();
        }

        public void Miss_Sound()
        {
            //MissSE.Play();
        }

        public void Death_Sound()
        {
            //DeathSE.Play();
        }

    }
}