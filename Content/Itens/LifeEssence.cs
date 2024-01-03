using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Itens
{
    public class LifeEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 4;
            Item.height = 4;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Item.position - new Vector2(2f, 2f), Item.width * 2, Item.height * 2, DustID.VioletMoss, Item.velocity.X, Item.velocity.Y, 100);
                Main.dust[dust].noGravity = true;
            }

            Lighting.AddLight(Item.position, 1f, 0.09f, 0.09f);


            if (Item.timeSinceItemSpawned > 600)
            {
                Item.active = false;
            }

            Vector2 moveTo = Main.LocalPlayer.Center;

            float speed = 20f;
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
            int amount = 1 + Main.rand.Next(4);

            player.GetModPlayer<Common.Players.BloodHunter>().blood += amount;
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, 10, 10), new Color(200, 0, 255), amount);
            SoundEngine.PlaySound(SoundID.AbigailUpgrade, player.position);
            Item.active = false;
            return false;
        }
    }
}
