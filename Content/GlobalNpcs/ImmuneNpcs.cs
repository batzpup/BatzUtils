using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BatzUtils;

public class ImmuneNpcs : GlobalNPC
{
    public override void SetDefaults(NPC npc)
    {
        if (npc.friendly && npc.type != NPCID.DD2EterniaCrystal)
        {
            npc.dontTakeDamageFromHostiles = true;
            npc.lavaImmune = true;
            npc.trapImmune = true;
        }
           
    }
}
