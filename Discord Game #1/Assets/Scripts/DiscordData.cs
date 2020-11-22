using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscordData : MonoBehaviour
{
    /*
     * Use this for setting EXACT values for Rich Presence
     * Otherwise, just set data in the level manager
     * If you have a DiscordData and LevelManager in your scene,
     * the Discord Controller will prefer the LevelManager
     */
    public string state;
    public string details;
    public Int64 timestampStart;
    public Int64 timestampEnd;
    public string assetLargeImage;
    public string assetLargeText;
    public string assetSmallImage;
    public string assetSmallText;
    public string partyId;
    public Int32 partySizeCurrentSize;
    public Int32 partySizeMaxSize;
    public string secretMatch;
    public string secretJoin;
    public string secretSpectate;
    public bool instance;

    public void RefeshActivity()
    {
        var activity = new Discord.Activity
        {
            State = state,
            Details = details,
            Timestamps =
                {
                    Start = timestampStart,
                    End = timestampEnd
                },
            Assets =
                {
                    LargeImage = assetLargeImage,
                    LargeText = assetLargeText,
                    SmallImage = assetSmallImage,
                    SmallText = assetSmallText
                },
            Party =
                {
                    Id = partyId,
                    Size =
                    {
                        CurrentSize = partySizeCurrentSize,
                        MaxSize = partySizeMaxSize
                    }
                },
            Secrets =
                {
                    Match = secretMatch,
                    Join = secretJoin,
                    Spectate = secretSpectate
                }
        };

        GameObject.Find("GameManager").GetComponent<DiscordController>().UpdateActivity(activity);
    }
}
