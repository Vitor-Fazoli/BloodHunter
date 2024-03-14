using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace BloodHunter.Common.UI
{
    public class SpecializationUIState : UIState
    {

        private const float Precent = 0f;
        private UIText classTitle;
        private UIImage background;
        private UIImage specIcon;
        private UIImage specPassiveIcon1;
        private UIImage specPassiveIcon2;
        private UIImage specPassiveIcon3;
        private UIImage specPassiveIcon4;
        private UIImage specPassiveIcon5;
        private UIImage specPassiveIcon6;

        public override void OnInitialize()
        {
            classTitle = new("Sanguine Marksman", 0.70f)
            {
                HAlign = 0.5f,
            };

            classTitle.Top.Set(30, Precent);


            background = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/SpecializationBackground"))
            {
                VAlign = 0.5f,
                HAlign = 0.5f
            };
            background.Width.Set(136, Precent);
            background.Height.Set(200, Precent);


            specIcon = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/SpecializationIcon"));
            specIcon.Width.Set(60, Precent);
            specIcon.Height.Set(60, Precent);
            specIcon.Left.Set(38, Precent);
            specIcon.Top.Set(46, Precent);

            specPassiveIcon1 = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/SpecializationPassiveIcon"));
            specPassiveIcon1.Width.Set(32, Precent);
            specPassiveIcon1.Height.Set(32, Precent);
            specPassiveIcon1.HAlign = 0.15f;
            specPassiveIcon1.Top.Set(125, Precent);

            specPassiveIcon2 = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/SpecializationPassiveIcon"));
            specPassiveIcon2.Width.Set(32, Precent);
            specPassiveIcon2.Height.Set(32, Precent);
            specPassiveIcon2.HAlign = 0.5f;
            specPassiveIcon2.Top.Set(125, Precent);

            specPassiveIcon3 = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/SpecializationPassiveIcon"));
            specPassiveIcon3.Width.Set(32, Precent);
            specPassiveIcon3.Height.Set(32, Precent);
            specPassiveIcon3.HAlign = 0.85f;
            specPassiveIcon3.Top.Set(125, Precent);

            specPassiveIcon4 = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/SpecializationPassiveIcon"));
            specPassiveIcon4.Width.Set(32, Precent);
            specPassiveIcon4.Height.Set(32, Precent);
            specPassiveIcon4.HAlign = 0.15f;
            specPassiveIcon4.Top.Set(160, Precent);

            specPassiveIcon5 = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/SpecializationPassiveIcon"));
            specPassiveIcon5.Width.Set(32, Precent);
            specPassiveIcon5.Height.Set(32, Precent);
            specPassiveIcon5.HAlign = 0.5f;
            specPassiveIcon5.Top.Set(160, Precent);

            specPassiveIcon6 = new UIImage(ModContent.Request<Texture2D>("BloodHunter/Assets/SpecializationPassiveIcon"));
            specPassiveIcon6.Width.Set(32, Precent);
            specPassiveIcon6.Height.Set(32, Precent);
            specPassiveIcon6.HAlign = 0.85f;
            specPassiveIcon6.Top.Set(160, Precent);

            background.Append(classTitle);
            background.Append(specPassiveIcon1);
            background.Append(specPassiveIcon2);
            background.Append(specPassiveIcon3);
            background.Append(specPassiveIcon4);
            background.Append(specPassiveIcon5);
            background.Append(specPassiveIcon6);
            background.Append(specIcon);
            Append(background);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();
            //if (player.isBloodHunter)
            //    return;

           // base.Draw(spriteBatch);
        }
        override public void Update(GameTime gameTime)
        {
        }    
    }
    public class SpecializationUISystem : ModSystem
    {
        internal SpecializationUIState barActive;

        private UserInterface _barActive;

        public override void Load()
        {
            barActive = new SpecializationUIState();
            barActive.Activate();
            _barActive = new UserInterface();
            _barActive.SetState(barActive);
        }
        public override void UpdateUI(GameTime gameTime)
        {

            _barActive?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {

            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    Mod.DisplayName + ": making a interface for blood hunters",
                    delegate
                    {

                        _barActive.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
