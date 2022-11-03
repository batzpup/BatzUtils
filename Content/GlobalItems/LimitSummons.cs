using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace BatzUtils;

public class LimitSummons : GlobalItem
{
    public override bool CanUseItem(Item item, Player player)
    {
        if (item.DamageType == DamageClass.Summon && item.type != ItemID.StardustDragonStaff)  
        {
            Projectile shotProjectile = ContentSamples.ProjectilesByType[item.shoot];
            if (shotProjectile.minion)
            {
                if (player.ownedProjectileCounts[shotProjectile.type] > 0)
                {
                    Helpers.ClearText();
                    Helpers.PrintText("You can only summon one of each minion", Color.Firebrick);
                    return false;
                }
            }
        }   
        return base.CanUseItem(item, player);
    }
}
