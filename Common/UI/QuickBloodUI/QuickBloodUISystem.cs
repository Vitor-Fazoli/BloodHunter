using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace BloodHunter.Common.UI.QuickBloodUI
{
    public class QuickBloodUISystem : ModSystem
    {
        internal QuickBloodUIState barActive;

        private UserInterface _barActive;

        public override void Load()
        {
            barActive = new QuickBloodUIState();
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
