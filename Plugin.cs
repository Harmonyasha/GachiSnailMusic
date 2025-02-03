using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using BepInEx.Logging;
using GachiSnailMusic;
using HarmonyLib;
using LCSoundTool;
using LethalConfig;
using UnityEngine;

namespace GachiSnailMusic
{


    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    //[BepInDependency("no00ob-LCSoundTool-1.5.1",BepInDependency.DependencyFlags.SoftDependency)]
   
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("Snowlance.SoThisIsImmortalSnail");

        public static string ProjectNamespace = "GachiSnailMusic";

        public static ConfigEntry<float> configVolume;

        public static ConfigEntry<int> configDistance;

        public static List<AudioClip> LoadedMusic = new();

        public static Plugin PluginInstance { get; private set; }

        public static ManualLogSource LoggerInstance { get; private set; }
        private async Task WaitForLcSound()
        {
            while (true)
            {

             if (Chainloader.PluginInfos.ContainsKey("LCSoundTool"))
                {
                    LoggerInstance.LogInfo("LCSoundTool found!");
                    break;
                }

                await Task.Delay(1000);
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private async void Awake()
        {
            if (PluginInstance == null)
            {
                PluginInstance = this;
            }
            LoggerInstance = PluginInstance.Logger;
            var bundle = GachiSnailMusic.Utils.Bundle.LoadBundle("gachisnail");
            Sprite aVeryCoolIconAsset = bundle.LoadAsset<Sprite>("kent");
            try
            {
                LethalConfigManager.SetModIcon(aVeryCoolIconAsset);
            }
            catch { }
            await WaitForLcSound();
            LoggerInstance.LogInfo("Plugin SoThisIsImmortalSnail loaded successfully.");
          
            configVolume = Config.Bind("Volume", "MusicVolume", .3f, "Volume of the music. Must be between 0 and 1.");
            LoggerInstance.LogInfo("configVolume");

            //  Plugin.configPlayWhenLookingAtSnail = base.Config.Bind<bool>("Looking Mechanic", "PlayWhenLookingAtSnail", true, "Play the music only when the player is looking at the snail. Everything below this only works if this is set to true.");
            // Plugin.LoggerInstance.LogInfo("configPlayWhenLookingAtSnail");

            configDistance = Config.Bind("Sound Mechanic", "Distance", 30, "Play the music only when the player is looking at a certain distance of the snail.");
            LoggerInstance.LogInfo("configDistance");

            /*          Plugin.configPauseWhenNotLooking = base.Config.Bind<bool>("Looking Mechanic", "PauseWhenNotLooking", true, "Wether to pause the music when not looking at the snail, or stop it and start it over again when looking again. true = Pause, false = Stop.");
                      Plugin.LoggerInstance.LogDebug("configPauseWhenNotLooking");*/
            
            var MusicDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LoggerInstance.LogInfo($"Target Music Directory : {MusicDirectory}");
            foreach (string filepath in Directory.GetFiles(MusicDirectory))
            {
                if (filepath.EndsWith(".mp3"))
                {
                    var splitted = filepath.Split('\\');
                    string soundname = splitted[splitted.Length-1];
                    LoggerInstance.LogInfo($"Loading Music :{soundname}");
                    LoadedMusic.Add(
                        SoundTool.GetAudioClip(
                            MusicDirectory, soundname
                            )
                        );
                    LoggerInstance.LogInfo($"Music Loaded :{soundname}");
                }

            }

            LoggerInstance.LogDebug("Pre PATCH");
            harmony.PatchAll();
            LoggerInstance.LogInfo("Snowlance.SoThisIsImmortalSnail v1.0.0 has loaded!");
        }

        // Token: 0x04000005 RID: 5

    }
}
