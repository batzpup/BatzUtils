using BatzUtils.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace BatzUtils.Content.Items;

public class BlingShield : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Blind enemies by with your enormous wealth");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.sellPrice(gold: 12);
        Item.rare = ItemRarityID.Yellow;
        Item.accessory = true;
        Item.defense = 6;
        
    }
    

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GoldBar, 20)
            .AddIngredient(ItemID.Diamond, 10)
            .AddTile(TileID.Anvils)
            .Register();
        CreateRecipe()
            .AddIngredient(ItemID.DirtBlock, 1)
            .Register();

    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BlindingLightModPlayer>().ApplyDebuffToEnemies = true;
    }
}





