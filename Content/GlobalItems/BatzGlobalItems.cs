using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BatzUtils;

public class BatzGlobalItems : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        if (item.maxStack > 1)
        {
            item.maxStack = 9999;
        }
        //try make it delete the item after
        if (IsBossItem(item))
        {
            item.consumable = false;
        }
    }

    bool IsBossItem(Item item)
    {
        return item.type == ItemID.SlimeCrown || item.type == ItemID.SuspiciousLookingEye || item.type == ItemID.WormFood || item.type == ItemID.BloodySpine || 
               item.type == ItemID.Abeemination || item.type == 5120 || item.type == 4988 || item.type == ItemID.MechanicalEye || item.type == ItemID.MechanicalSkull || 
               item.type == ItemID.MechanicalWorm || item.type == ItemID.LihzahrdPowerCell || 
               item.type == ItemID.CelestialSigil || item.type == 4961 || item.type == ItemID.TruffleWorm;
    }
}
