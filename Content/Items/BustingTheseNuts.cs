
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Items;

public class BustingTheseNuts : ModItem
{

    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Hit shit with it");
    }
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 100;
        Item.useAnimation = 100;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;

        Item.damage = 280;
        Item.knockBack = 7;
        Item.crit = 35;
        Item.shootSpeed = 12;
        Item.scale = 1.9f;

        Item.value = Item.sellPrice(silver: Main.rand.Next(69, 420));
        Item.UseSound = new SoundStyle($"{nameof(BatzUtils)}/Assets/Sounds/Items/BustingTheseNuts") {
            Volume = 0.9f,
            PitchVariance = 0.2f,
            MaxInstances = 3,
        };

    }
    
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.FireworksRGB);
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        CombatText.NewText(player.getRect(), Color.Ivory, "GG EZ");
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Ruby,3)
            .AddIngredient(ItemID.MythrilBar,20)
            .AddIngredient(ItemID.Obsidian,10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
