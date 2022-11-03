using System;
using BatzUtils.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace BatzUtils.Content.Buffs;


public class BlindingLightDebuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blinding light Debuff");
        Description.SetDefault("Your wealth.... its too much");
        Main.debuff[Type] = true;
        Main.buffNoSave[Type] = true;
        Main.pvpBuff[Type] = false;

        
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<BlindingLightDebuffNpc>().BlindingLightDebuff = true;
        if (Main.GameUpdateCount % 10 == 0)
        {
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            dust = Dust.NewDustDirect(new Vector2(npc.Center.X - (npc.width/2),npc.Center.Y - npc.height/2), npc.width, npc.height, 6, 0, -0, 0, new Color(255,255,255), 2f);
            dust.noGravity = true;



        }
    }
}

public class BlindingLightDebuffNpc : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public bool BlindingLightDebuff;
    public float timer = 0f;
    public bool wasBlinded;
    public override void ResetEffects(NPC npc)
    {
        BlindingLightDebuff = false;
    }
    public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
    {
        if (BlindingLightDebuff)
        {
           // Helpers.PrintText("Has Debuff",Color.Aqua);
           if (wasBlinded)
           {
               // Helpers.PrintText("Is Blind", Color.Red);
               timer++;
               // 2 seconds 
               if (timer > 180)
               {
                   CombatText.NewText(npc.getRect(), Color.LightYellow, "Unblinded");
                   wasBlinded = false;
                   timer = 0;
                   
               }
               else
               {
                   return false;
               }
           }
        }
        return base.CanHitPlayer(npc, target, ref cooldownSlot);
    }
}


public class BlindingLightModPlayer : ModPlayer 
{
    public bool ApplyDebuffToEnemies;

    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage,
        ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        if (damageSource.SourceNPCIndex > -1)
        {
            var npc = Main.npc[damageSource.SourceNPCIndex];
           // Helpers.PrintText("Valid Npc", Color.Red);
            if (npc.HasBuff<BlindingLightDebuff>())
            {
                //Helpers.PrintText("Npc has Debuff", Color.Red);
                //if debuff rolls 25% chance  dont take damage
                if (Main.rand.Next(0, 101) >= 75)
                {
                    //Helpers.PrintText("Npc blinded", Color.Red);
                    npc.GetGlobalNPC<BlindingLightDebuffNpc>().wasBlinded = true;
                    CombatText.NewText(npc.getRect(), Color.LightYellow, "Blinded!");
                    return false;
                }
            }
        }

        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
    }
    public override void ResetEffects()
    {
        ApplyDebuffToEnemies = false;
    }

    public override void OnHitByNPC(NPC npc, int damage, bool crit)
    {
        if (ApplyDebuffToEnemies)
        {
            npc.AddBuff(ModContent.BuffType<BlindingLightDebuff>(),600);
            //Helpers.PrintText($"Buff Added to {npc.TypeName}",Color.Cyan);
        }
    }
    
}
