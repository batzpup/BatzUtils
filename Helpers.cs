using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;



namespace BatzUtils;

public static class Helpers
{
    public static void PrintText(string text, Color color)
    {
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText(text, new Color?(color));
            return;
        }
        if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), color, -1);
        }
    }
    public static void ClearText()
    {
        List<Tuple<string, Color>> cachedMessages =  (List<Tuple<string, Color>>) typeof(ChatHelper).GetField("_cachedMessages",BindingFlags.Static|BindingFlags.NonPublic)?.GetValue(null);
        if (cachedMessages != null)
        {
            cachedMessages.Clear();
            typeof(ChatHelper).GetField("_cachedMessages",BindingFlags.Static|BindingFlags.NonPublic)?.SetValue(null,cachedMessages);
            PrintText("Messages Cleared", Color.Purple);
        }
        else
        {
            PrintText("CachedMessages not found", Color.Purple);
        }
    }
}
