using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Arkanoid
{
    public class Gracz
    {
        public Vector2 pozycjaGracza;
        Vector2 ruch;

        float predkoscGracza = 8f;
        public int pkt = 0;
        public int zycia = 3;

        KeyboardState keyboardState;
        GamePadState gamePadState;

        Texture2D texturaGracza;
        Rectangle pozycjaOkna;

        public Gracz(Texture2D texturaGracza, Rectangle pozycjaOkna)
        {
            this.texturaGracza = texturaGracza;
            this.pozycjaOkna = pozycjaOkna;
            PozycjaStartowa();

        }

        public void Update()
        {
            ruch = Vector2.Zero;

            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);

            if (keyboardState.IsKeyDown(Keys.Left) ||
                gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                gamePadState.IsButtonDown(Buttons.DPadLeft))
                ruch.X = -1;

            if (keyboardState.IsKeyDown(Keys.Right) ||
                gamePadState.IsButtonDown(Buttons.LeftThumbstickRight) ||
                gamePadState.IsButtonDown(Buttons.DPadRight))
                ruch.X = 1;

            ruch.X *= predkoscGracza;
            pozycjaGracza += ruch;
            BlokadaGracza();
        }

        private void BlokadaGracza()
        {
            if (pozycjaGracza.X < 0)
                pozycjaGracza.X = 0;
            if (pozycjaGracza.X + texturaGracza.Width > pozycjaOkna.Width)
                pozycjaGracza.X = pozycjaOkna.Width - texturaGracza.Width;
        }

        public void PozycjaStartowa()
        {
            pozycjaGracza.X = (pozycjaOkna.Width - texturaGracza.Width) / 2;
            pozycjaGracza.Y = pozycjaOkna.Height - texturaGracza.Height - 5;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texturaGracza, pozycjaGracza, Color.White);
        }

        public Rectangle RetStartPoz()
        {
            return new Rectangle(
            (int)pozycjaGracza.X,
            (int)pozycjaGracza.Y,
            texturaGracza.Width,
            texturaGracza.Height);
        }
        public int Pkt
        {
            get { return pkt; }
            set { pkt = value; }
        }
       
    }
}   
