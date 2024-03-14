using BloodHunter.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace BloodHunter.Content.Items
{
    public class VampiricPotion : ModItem
    {
        //todo: need to make a effect when drinked
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
            ItemID.Sets.IsFood[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, ModContent.BuffType<BloodPlague>(), 1);
            Item.value = Item.buyPrice(gold: 3);
            Item.consumable = true;
        }
        public override void OnConsumeItem(Player player)
        {
            Common.Players.BloodHunter p = player.GetModPlayer<Common.Players.BloodHunter>();

            player.AddBuff(ModContent.BuffType<BloodPlague>(), 1);
            p.eyeColor = player.eyeColor;
            p.isBloodHunter = true;
        }
        public override void PostUpdate()
        {
            if(Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Item.position, Item.width, Item.height, DustID.LifeDrain, 1 - Main.rand.Next(2), -0.5f, 100) ;
                dust.noGravity = true;
            }

            Lighting.AddLight(Item.Center, new Color(255, 100, 100).ToVector3() * 1.2f);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
            {
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            }
            else
            {
                frame = texture.Frame();
            }

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer * 2) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 2f).RotatedBy(radians) * time, frame, new(255, 220, 220, 0), rotation, frameOrigin, 1 + (time / 3), SpriteEffects.None, 0);
            }



            return true;
        }
    }
}
