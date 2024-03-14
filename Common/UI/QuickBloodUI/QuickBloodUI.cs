using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace BloodHunter.Common.UI.QuickBloodUI
{
    class QuickBloodUIState : UIState
    {
        private const float Precent = 0f;
        private UIImage point;
        private UIImage frame;

        public override void OnInitialize()
        {
            point = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/Blood"));
            point.Left.Set(0, Precent);
            point.Top.Set(0, Precent);
            point.Width.Set(50, Precent);
            point.Height.Set(42, Precent);

            frame = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/BloodFrame"))
            {
                HAlign = 0.25f,
                VAlign = 0.02f
            };
            frame.Width.Set(60, Precent);
            frame.Height.Set(60, Precent);

            frame.Append(point);
            Append(frame);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();
            if (!player.isBloodHunter)
                return;

            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();

            float quotient = (float)player.bloodCurrent / player.bloodMax2;
            quotient = Utils.Clamp(quotient, 0f, 1f);

            if (Main.playerInventory)
            {
                frame.HAlign = 0.31f;
                frame.VAlign = 0.09f;
            }
            else
            {
                frame.HAlign = 0.25f;
                frame.VAlign = 0.02f;
            }

            //if (player.bloodCurrent <= player.xpMax)
            //{
            //    text.SetText(player.bloodCurrent.ToString());
            //}
            //else
            //{
            //    text.SetText("Max");
            //}

            if (frame.IsMouseHovering)
            {
                Main.instance.MouseText(player.bloodCurrent + "/" + player.bloodMax2);
            }

            point.ImageScale = quotient;


            base.Update(gameTime);
        }
    }
}
