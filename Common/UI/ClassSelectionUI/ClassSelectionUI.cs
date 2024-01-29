using BloodHunter.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace BloodHunter.Common.UI.ClassSelectionUI
{
    class ClassSelectionUIState : UIState
    {
        private const float Precent = 0f;
        private UIImage icon;
        private UIText text;
        private ReLogic.Content.Asset<Texture2D> rangerIcon;
        private ReLogic.Content.Asset<Texture2D> magicIcon;

        public override void OnInitialize()
        {
            rangerIcon = ModContent.Request<Texture2D>("BloodHunter/Assets/RangerIcon");
            magicIcon = ModContent.Request<Texture2D>("BloodHunter/Assets/MagicIcon");

            icon = new UIImage(rangerIcon);
            icon.Left.Set(Main.screenWidth / 2.7f, Precent);
            icon.Top.Set(140, Precent);
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
            if (!player.isBloodHunter || !Main.playerInventory)
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

            if (player.specialization == Specialization.SanguineMarksman)
            {
                icon.SetImage(rangerIcon);
            }
            else
            {
                icon.SetImage(magicIcon);
            }

            icon.Color = Color.White;
            text.SetText("");
            

            if (icon.IsMouseHovering)
            {
               
            }

            base.Update(gameTime);
        }
        public override void LeftClick(UIMouseEvent evt)
        {

        }
    }
}
