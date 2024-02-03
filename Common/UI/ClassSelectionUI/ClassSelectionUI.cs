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
        private UIState place;
        private UIImage icon1;
        private UIImage icon2;
        private UIText text1;
        private UIText text2;
        private ReLogic.Content.Asset<Texture2D> rangerIcon;
        private ReLogic.Content.Asset<Texture2D> magicIcon;

        public override void OnInitialize()
        {
            rangerIcon = ModContent.Request<Texture2D>("BloodHunter/Assets/RangerSimbol");
            magicIcon = ModContent.Request<Texture2D>("BloodHunter/Assets/MagicSimbol");

            icon1 = new UIImage(rangerIcon);
            icon1.Left.Set(-150, Precent);
            icon1.Top.Set(0, Precent);
            icon1.Width.Set(30, Precent);
            icon1.Height.Set(30, Precent);

            icon2 = new UIImage(magicIcon);
            icon2.Left.Set(150, Precent);
            icon2.Top.Set(0, Precent);
            icon2.Width.Set(30, Precent);
            icon2.Height.Set(30, Precent);

            text1 = new UIText("");
            text1.Width.Set(50, Precent);
            text1.Height.Set(42, Precent);
            text1.Left.Set(0, Precent);
            text1.Top.Set(6, Precent);

            text2 = new UIText("");
            text2.Width.Set(50, Precent);
            text2.Height.Set(42, Precent);
            text2.Left.Set(0, Precent);
            text2.Top.Set(6, Precent);

            place = new UIState();
            place.Left.Set(Main.screenWidth / 2, Precent);
            place.Top.Set(Main.screenHeight / 2, Precent);


            place.Append(icon1);
            place.Append(icon2);
            Append(place);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();
            if (!player.initialSelection)
                return;

            base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Rectangle hitbox = icon1.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 30;
            hitbox.Height -= 40;
        }
        public override void Update(GameTime gameTime)
        {
            var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();

            while (player.initialSelection)
            {
                Main.LocalPlayer.creativeGodMode = true;
                Main.LocalPlayer.stoned = true;
            }

            if (player.specialization == Specialization.SanguineMarksman)
            {
                icon1.SetImage(rangerIcon);
            }
            else
            {
                icon1.SetImage(magicIcon);
            }

            icon1.Color = Color.White;
            text1.SetText("");
            

            if (icon1.IsMouseHovering)
            {
                icon1.Color = Color.Cornsilk;
            }

            if (icon2.IsMouseHovering)
            {
                icon2.Color = Color.Cornsilk;   
            }

            base.Update(gameTime);
        }
        public override void LeftClick(UIMouseEvent evt)
        {

        }
    }
}
