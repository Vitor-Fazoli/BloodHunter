using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BloodHunter.Common.UI.ClassSelectionUI
{
    internal class ClassSelectionUIState : UIState
    {

        public static bool visible;
        public DragableUIPanel panel;
        private UIText title;
        private UIText content;
        private UIText sanguineMarksman;
        private UIText darkBloodMagus;

        public override void OnInitialize()
        {
            panel = new DragableUIPanel();
            panel.Left.Set(800, 0);
            panel.Top.Set(100, 0);
            panel.Width.Set(1000, 0);
            panel.Height.Set(400, 0);

            title = new UIText("Blood Hunter")
            {
                HAlign = 0.5f,
                VAlign = 0.1f
            };
            content = new UIText("Choose your specialization")
            {
                HAlign = 0.5f,
                VAlign = 0.15f
            };
            sanguineMarksman = new UIText("Sanguine Marksman")
            {
                HAlign = 0.5f,
                VAlign = 0.3f
            };  
            darkBloodMagus = new UIText("Dark Blood Magus")
            {
                HAlign = 0.5f,
                VAlign = 0.4f
            };

            // ignore these extra 0s

            panel.Append(sanguineMarksman);
            panel.Append(darkBloodMagus);
            panel.Append(title); 
            panel.Append(content);
            Append(panel); //appends the panel to the UIState
        }
        public override void Update(GameTime gameTime)
        {
            if (sanguineMarksman.IsMouseHovering)
            {
                sanguineMarksman.TextColor = new Color(0, 255, 0);
                Main.instance.MouseText("Ranged Class \nOn hit foes with ranged weapons spawn a blood orb, it move ahead to player to get a blood\n\n" +
                    "When you get maximum blood, receive a buff and reset your bar");
            }
            else
            {
                sanguineMarksman.TextColor = Color.White;
            }

            if (darkBloodMagus.IsMouseHovering)
            {
                darkBloodMagus.TextColor = new Color(0, 255, 0);
                Main.instance.MouseText("Magic & Summon Class \nAutomatically gains blood, can be used as excess mana");
            }
            else
            {
                darkBloodMagus.TextColor = Color.White;
            }




        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var player = Main.LocalPlayer.GetModPlayer<Players.BloodHunter>();
            if (!player.initialSelection)
                return;

            base.Draw(spriteBatch);
        }
    }
}
