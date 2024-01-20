﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BloodHunter.Content.Items.Accessories
{
    public class SacrificialDagger : ModItem
    {
        private int timer = 0;
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 32;
            Item.height = 32;
            Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Common.Players.BloodHunter p = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();

            timer++;
            if (!p.IsBloodFull())
            {
                if (timer > 600)
                {
                    int amount = (player.statLife / 4);

                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, 10, 10), CombatText.DamagedFriendly, amount);

                    p.bloodCurrent += 5;
                    timer = 0;
                    p.UpdateBadLifeRegen();
                    player.statLife -= amount;
                }
            }
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>().bloodHunter;
        }
    }
}