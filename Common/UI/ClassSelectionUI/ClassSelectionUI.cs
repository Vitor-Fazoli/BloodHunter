using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;

namespace BloodHunter.Common.UI.ClassSelectionUI
{
    class ClassSelectionUIState : UIState
    {
        private const float Precent = 0f;
        private UIImage icon;
        private UIText text;
        private ReLogic.Content.Asset<Texture2D> rangerIcon;
        private ReLogic.Content.Asset<Texture2D> magicIcon;

        private readonly int cooldown;
        private const int COOLDOWN_MAX = 60 * 30;

        public override void OnInitialize()
        {
            rangerIcon = ModContent.Request<Texture2D>("BloodHunter/Assets/RangerIcon");
            magicIcon = ModContent.Request<Texture2D>("BloodHunter/Assets/MagicIcon");
            
            icon = new UIImage(rangerIcon);
            icon.Left.Set(3, Precent);
            icon.Top.Set((Main.screenHeight / 2.4f), Precent);
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
            var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();

            if (player.isItRanger)
            {
                icon.SetImage(rangerIcon);
            }
            else
            {
                icon.SetImage(magicIcon);
            }


            if (player.classCooldown >= 0)
            {
                text.SetText((player.classCooldown / 60).ToString());
                icon.Color = Color.Gray;
                player.classCooldown--;
            }
            else
            {
                icon.Color = Color.White;
                text.SetText("");
            }


            

            if (icon.IsMouseHovering)
            {
                if (player.classCooldown >= 0)
                {
                    Main.instance.MouseText("Cooldown in progress");
                }
                else
                {
                    if (player.isItRanger)
                    {
                        Main.instance.MouseText("Artemis Seal: All hits generate life essence for you", 0, 0);
                    }
                    else
                    {
                        Main.instance.MouseText("Chronos Seal: While you have blood, use it as your mana, but at half the cost\nClick to change your passive class", 0, 0);
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

                player.classCooldown = COOLDOWN_MAX;
            }
        }
    }
}
