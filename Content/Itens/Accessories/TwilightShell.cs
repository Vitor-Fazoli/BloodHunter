﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Itens.Accessories
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
            return Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>().bloodHunter;
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

                if (p.blood != 0)
                {
                    p.blood -= info.Damage;
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
