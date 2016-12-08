using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Arkanoid
{
    public class HUD
    {
        public const int wysokosc = 20;

        Game1 game;

        public HUD(Game1 game)
        {
            this.game = game;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        
        }

        public void Menu(String Message)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Chcesz zagrac ponownie?",
                Message,
                MessageBoxButtons.YesNo
                );

            if (dialogResult == DialogResult.Yes)
            {
                game.StartGame();                
            }
            else if (dialogResult == DialogResult.No)
            {
                game.Exit();
            }
        }
    }
}
