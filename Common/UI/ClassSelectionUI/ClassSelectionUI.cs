using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using Humanizer;

namespace BloodHunter.Common.UI.ClassSelectionUI
{
    class ClassSelectionUIState : UIState
    {
        private const float Precent = 0f;
        private UIImage icon;
        private UIText text;
        private ReLogic.Content.Asset<Texture2D> rangerIcon;
        private ReLogic.Content.Asset<Texture2D> magicIcon;

        private int cooldown;
        private const int COOLDOWN_MAX = 360;

        public override void OnInitialize()
        {
            rangerIcon = ModContent.Request<Texture2D>("BloodHunter/Assets/RangerIcon");
            magicIcon = ModContent.Request<Texture2D>("BloodHunter/Assets/MagicIcon");


            icon = new UIImage(magicIcon);
            icon.Left.Set(65, Precent);
            icon.Top.Set((Main.screenHeight / 3.2f) + 30, Precent);
            icon.Width.Set(30, Precent);
            icon.Height.Set(30, Precent);

            text = new UIText("");
            text.Width.Set(50, Precent);
            text.Height.Set(42, Precent);
            text.Left.Set(0, Precent);
            text.Top.Set(6, Precent);

            Append(icon);
            icon.Append(text);
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
            Rectangle hitbox = icon.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 30;
            hitbox.Height -= 40;
        }
        public override void Update(GameTime gameTime)
        {
            if (cooldown >= 0)
            {
                text.SetText((cooldown / 60).ToString());
                icon.Color = Color.Gray;
                cooldown--;
            }
            else
            {
                icon.Color = Color.White;
                text.SetText("");
            }


            var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();

            if (icon.IsMouseHovering)
            {
                if (cooldown >= 0)
                {
                    Main.instance.MouseText("Cooldown in progress");
                }
                else
                {
                    if (player.isItRanger)
                    {
                        Main.instance.MouseText("Click to change your passive class\n" +
                        "Ranger: All critical hits generate life essence for you", 0, 0);
                    }
                    else
                    {
                        Main.instance.MouseText("Click to change your passive class\n" +
                        "Magic: While you have blood, use it as your mana, but at half the cost", 0, 0);
                    }
                }
            }
            

            base.Update(gameTime);
        }
        public override void LeftClick(UIMouseEvent evt)
        {
            if (cooldown <= 0 && icon.IsMouseHovering)
            {
                var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();
                if (player.isItRanger)
                {
                    icon.SetImage(magicIcon);
                    player.isItRanger = false;
                }
                else
                {
                    icon.SetImage(rangerIcon);
                    player.isItRanger = true;
                }

                cooldown = COOLDOWN_MAX;
            }
        }
    }
}
