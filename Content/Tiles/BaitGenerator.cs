using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BatzUtils.Content.Tiles;



public class BaitGenerator : ModTile
{
    //SomehowMake this instance based
    int _generatedApprenticeBait = 0;
    int _generatedJourneymanBait = 0;
    int _generatedMasterBait = 0;
    float _timer = 0f;
    public override void SetStaticDefaults()
    {
        
        Main.tileSolid[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2); // this style already takes care of direction for us
        TileObjectData.addTile(Type);
        
        ModTranslation name = CreateMapEntryName();
        name.SetDefault("Bait Generator");
        AddMapEntry(new Color(200, 200, 200), name);
        
    }

 
    public override bool RightClick(int i, int j)
    {
        
        Player player = Main.LocalPlayer;
        if (_generatedApprenticeBait > 0)
        {
            player.QuickSpawnItem(null, ItemID.ApprenticeBait, _generatedApprenticeBait);
            _generatedApprenticeBait = 0;
        }
        if (_generatedJourneymanBait > 0)
        {
            player.QuickSpawnItem(null, ItemID.JourneymanBait, _generatedJourneymanBait);
            _generatedJourneymanBait = 0;
        }
        if (_generatedMasterBait > 0)
        {
            player.QuickSpawnItem(null, ItemID.MasterBait, _generatedMasterBait);
            _generatedMasterBait = 0;
        }
        
        return base.RightClick(i, j);
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        _timer++;
        if (_timer > 60)
        {
            var roll = Main._rand.Next(0, 101);
            if (roll < 51)
            {
                _generatedApprenticeBait++;
            }
            else if(roll < 86)
            {
                _generatedJourneymanBait++;
            }
            else
            {
                _generatedMasterBait++;
            }

            _timer = 0;
        }
        
        base.AnimateTile(ref frame, ref frameCounter);
    }
    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 48, ModContent.ItemType<Items.BaitGenerator>());
    }
   
}
