/*using System;
using GachiSnailMusic;
using HarmonyLib;
using UnityEngine;

public class SnailMusicController : MonoBehaviour
{
    public GameObject snailInstance;
    private static Type snailAIType = AccessTools.TypeByName("ImmortalSnail.SnailAI");

    private void Update()
    {
        Plugin.LoggerInstance.LogDebug("SnailMusicController.Update");

        if (!Plugin.configPlayWhenLookingAtSnail.Value || snailInstance == null)
            return;
        Plugin.LoggerInstance.LogInfo("Start");


        *//*        Plugin.Logger.LogInfo(hasLOS)
                 bool hasLOS = GameNetworkManager.Instance.localPlayerController
                    .HasLineOfSightToPosition(snailTransform.position, 70f, Plugin.configDistance.Value, -1f);
                ;*//*

       // Plugin.Logger.LogInfo("1");
        AudioSource creatureSFX = snailInstance.GetComponent<AudioSource>();
        if (creatureSFX == null) return;
       // Plugin.Logger.LogInfo("2");
        //if (hasLOS)
        //{
        if (!creatureSFX.isPlaying)
            {
                Plugin.LoggerInstance.LogInfo("Play");
                creatureSFX.Play();
               
            }
       // }
        else
        {
            if (Plugin.configPauseWhenNotLooking.Value)
            {
                Plugin.LoggerInstance.LogInfo("Pause");
                creatureSFX.Pause();
            }
            else
            {
                Plugin.LoggerInstance.LogInfo("Stop");
                creatureSFX.Stop();
            }
        }
    }


}
*/