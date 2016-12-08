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
    public class Blok
    {
        Rectangle pozycjaBloku;
        Texture2D texturaBloku;
        Color kolor;

        public bool istnieje;
        public int blokiIstniejace;

        public Rectangle PozycjaBloku
        {
            get { return pozycjaBloku; }
            
        }

        public Blok(Texture2D texturaBloku, Rectangle pozycjaBloku, Color kolor)
        {

            this.texturaBloku = texturaBloku;
            this.pozycjaBloku = pozycjaBloku;
            this.kolor = kolor;
            this.istnieje = true;

            blokiIstniejace = 90;
            
        }

        public void SprawdzKolizje(Piłka pilka)
        {
            if (istnieje && pilka.PozycjaPilki.Intersects(pozycjaBloku))
            {
                istnieje = false;
                pilka.Niszczenie(this);
                blokiIstniejace = blokiIstniejace - 1;
                
            }
        } 
        public void Draw(SpriteBatch spriteBatch)
        {
            if (istnieje)
                spriteBatch.Draw(texturaBloku, pozycjaBloku, kolor);
        }
    }
}
