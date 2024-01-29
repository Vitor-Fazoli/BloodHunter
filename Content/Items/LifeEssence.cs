using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items
{
    public class LifeEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 1;
            Item.height = 1;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(Item.position - new Vector2(2f, 2f), Item.width * 2, Item.height * 2, DustID.Blood, 0, 0, 100, Scale: 1.5f);
                Main.dust[dust].noGravity = true;
            }

            Lighting.AddLight(Item.position, 1f, 0.09f, 0.09f);


            if (Item.timeSinceItemSpawned > 600)
            {
                Item.active = false;
            }

            Vector2 moveTo = Main.LocalPlayer.Center;

            float speed = 3f;

            for (int i = 0; i < 600; i++)
            {
                switch (i)
                {
                    case 150:
                        speed = 10f;
                        break;
                    case 300:
                        speed = 20f;
                        break;
                    case 600:
                        speed = 50f;
                        break;
                }
            }

            Vector2 move = moveTo - Item.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 50f; //the larger this is, the slower the npc will turn
            move = (Item.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            Item.velocity = move;

        }
        public override bool OnPickup(Player player)
        {
            var p = player.GetModPlayer<Common.Players.BloodHunter>();

            int amount = p.essence;

            p.ReceiveBlood(player, amount);
            Item.active = false;
            return false;
        }
    }
}
