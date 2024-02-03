using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Accessories
{
    public class TwilightShell : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.sellPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Main.LocalPlayer.GetModPlayer<TwilightShellP>().isTwilightShell = true;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>().isBloodHunter;
        }
    }
    public class TwilightShellP : ModPlayer
    {
        public bool isTwilightShell = false;

        public override void OnHurt(Player.HurtInfo info)
        {
            if (isTwilightShell)
            {
                var p = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();

                if (p.bloodCurrent != 0)
                {
                    p.bloodCurrent -= info.Damage;
                    info.Damage = 0;
                }
            }
        }
        public override void ResetEffects()
        {
            isTwilightShell = false;
        }
    }
}
