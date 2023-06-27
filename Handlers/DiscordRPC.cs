/*

! This file is part of the Sun™ Gaming Project.
! Licensed under the Apache License, Version 1.0

? Author: Jay

*/

using UnityEngine;
using Discord;

public class DiscordRPC : MonoBehaviour
{
    private Discord.Discord discord;
    private ActivityManager activityManager;
    private Activity currentActivity;
    private long startTime;

    private void Start()
    {
        // Initialize Discord GameSDK
        discord = new Discord.Discord(1123297707323314188, (ulong)CreateFlags.Default);
        activityManager = discord.GetActivityManager();

        // Set initial presence
        UpdatePresence("https://discord.gg/HVugrk6u6a", "Discord");

        // Start tracking time elapsed
        startTime = (long)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
    }

    private void OnDestroy()
    {
        // Dispose the Discord GameSDK resources
        discord.Dispose();
    }

    private void Update()
    {
        // Update the presence every frame (optional)
        discord.RunCallbacks();
    }

    private void UpdatePresence(string buttonUrl, string buttonText)
    {
        // Create a new presence object
        var presence = new Discord.Activity
        {
            State = "Scripting 🥱",
            Details = "Coming Soon",
            Timestamps =
            {
                Start = startTime
            },
            Assets =
            {
                LargeImage = "icon",
                LargeText = "Sun™"
            }
        };

        // Update the presence
        activityManager.UpdateActivity(presence, result =>
        {
            if (result == Result.Ok)
            {
                Debug.Log("Presence updated successfully.");
            }
            else
            {
                Debug.LogError($"Failed to update presence: {result}");
            }
        });
    }
}
