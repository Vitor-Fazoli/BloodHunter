using Microsoft.Xna.Framework;
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
        private UIText sanguineMarksman;
        private UIText darkBloodMagus;

        public override void OnInitialize()
        {
            panel = new DragableUIPanel();
            panel.Left.Set(800, 0);
            panel.Top.Set(100, 0);
            panel.Width.Set(500, 0);
            panel.Height.Set(400, 0);

            title = new UIText("Choose your specialization")
            {
                HAlign = 0.5f,
                VAlign = 0.1f
            };
            sanguineMarksman = new UIText("Sanguine Marksman")
            {
                HAlign = 0.5f,
                VAlign = 0.3f
            };  
            darkBloodMagus = new UIText("Dark Blood Magus")
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            // ignore these extra 0s

            panel.Append(sanguineMarksman);
            panel.Append(darkBloodMagus);
            panel.Append(title); //appends the text to the panel
            Append(panel); //appends the panel to the UIState
        }
        public override void Update(GameTime gameTime)
        {
            if (sanguineMarksman.IsMouseHovering)
            {
                sanguineMarksman.TextColor = new Color(0, 255, 0);
                Main.instance.MouseText("On hit foes with ranged weapons spawn a blood orb, it move ahead to player to get a blood\n\n" +
                    "When you get maximum blood, receive a buff and reset your bar");
            }
            else
            {
                sanguineMarksman.TextColor = Color.White;
            }

            if (darkBloodMagus.IsMouseHovering)
            {
                darkBloodMagus.TextColor = new Color(0, 255, 0);
                Main.instance.MouseText("Automatically gains blood, can be used as excess mana");
            }
            else
            {
                darkBloodMagus.TextColor = Color.White;
            }




        }
    }
}
