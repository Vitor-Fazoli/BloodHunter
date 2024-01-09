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
        //private UIText text;

        public override void OnInitialize()
        {
            point = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/Blood"));
            point.Left.Set(0, Precent);
            point.Top.Set(0, Precent);
            point.Width.Set(50, Precent);
            point.Height.Set(42, Precent);

            frame = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/BloodFrame"));
            frame.Left.Set(460, Precent);
            frame.Top.Set(20, Precent);
            frame.Width.Set(50, Precent);
            frame.Height.Set(42, Precent);

            //text = new UIText("");
            //text.Width.Set(50, Precent);
            //text.Height.Set(42, Precent);
            //text.Left.Set(0, Precent);
            //text.Top.Set(55, Precent);

            frame.Append(point);
            //frame.Append(text);
            Append(frame);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();
            if (!player.bloodHunter)
                return;

            base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Rectangle hitbox = frame.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 30;
            hitbox.Height -= 40;
        }
        public override void Update(GameTime gameTime)
        {
            var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();

            float quotient = (float)player.bloodCurrent / player.bloodMax2;
            quotient = Utils.Clamp(quotient, 0f, 1f);

            //if (player.bloodCurrent <= player.bloodMax)
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


            frame.Left.Set(20, Precent);
            frame.Top.Set(Main.screenHeight / 3.2f, Precent);
            point.ImageScale = quotient;


            base.Update(gameTime);
        }
    }
}
