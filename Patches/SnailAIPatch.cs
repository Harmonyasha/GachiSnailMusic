using System;
using System.Runtime.CompilerServices;
using BepInEx.Logging;
using GachiSnailMusic;
using HarmonyLib;
using UnityEngine;

namespace SoThisIsImmortalSnail.Patches
{
    [HarmonyPatch]
    internal class SnailAIPatch
    {
        static Type targetType = AccessTools.TypeByName("ImmortalSnail.SnailAI");

        [HarmonyPatch(typeof(EnemyAI), "Start")]
        [HarmonyPostfix]
        private static void StartPatch(object __instance)
        {
            if (__instance.GetType() != targetType) return;

     
            GameObject gameObject = Traverse.Create(__instance).Property("gameObject").GetValue<GameObject>();
            if (gameObject == null)
            {
                LoggerInstance.LogError("SnailAI::Start -> gameObject is null!");
                return;
            }
            if (gameObject == null) return;
            AudioSource creatureSFX = gameObject.AddComponent<AudioSource>();
            System.Random random = new System.Random();
            creatureSFX.clip = Plugin.LoadedMusic[random.Next(Plugin.LoadedMusic.Count)];
            creatureSFX.loop = true;
            creatureSFX.volume = Plugin.configVolume.Value; 
            creatureSFX.spatialBlend = 1f; 
            creatureSFX.minDistance = 1f; 
            creatureSFX.maxDistance = Plugin.configDistance.Value; 
            creatureSFX.rolloffMode = AudioRolloffMode.Linear;
 

       

     /*       SnailMusicController musicController = gameObject.AddComponent<SnailMusicController>();
            musicController.snailInstance =  gameObject;*/
        

          /*  if (!Plugin.configPlayWhenLookingAtSnail.Value)
            {
                creatureSFX.Play();
            }*/
            creatureSFX.Play();
        }

        private static ManualLogSource LoggerInstance = Plugin.LoggerInstance;
    }
}
