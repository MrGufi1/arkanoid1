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
    public class Piłka
    {
        Vector2 ruch;
        Vector2 pozycja;
        public Rectangle pozycjaPilki;
        bool kolizja;

        const float startowaPredkoscPilki = 7;
        float predkoscPilki;

        Texture2D texturaPilki;
        Rectangle pozycjaOkna;

        public Rectangle PozycjaPilki
        {
            get
            {
                pozycjaPilki.X = (int)pozycja.X;
                pozycjaPilki.Y = (int)pozycja.Y;
                return pozycjaPilki;
            }
        }

        public Piłka(Texture2D texturaPilki, Rectangle pozycjaOkna)
        {
            pozycjaPilki = new Rectangle(0, 0, texturaPilki.Width, texturaPilki.Height);
            this.texturaPilki = texturaPilki;
            this.pozycjaOkna = pozycjaOkna;
        }

        public void Update()
        {
            kolizja = false;
            pozycja += ruch * predkoscPilki;
            predkoscPilki += 0.001f;

            scianaKolizja();
        }

        private void scianaKolizja()
        {
            if (pozycja.X < 0)
            {
                pozycja.X = 0;
                ruch.X *= -1;
            }
            if (pozycja.X + texturaPilki.Width > pozycjaOkna.Width)
            {
                pozycja.X = pozycjaOkna.Width - texturaPilki.Width;
                ruch.X *= -1;
            }
            if (pozycja.Y < 0)
            {
                pozycja.Y = 0;
                ruch.Y *= -1;
            }
        }

        public void StartowaPozycja(Rectangle lokacjaGracza)
        {
            Random rand = new Random();

            ruch = new Vector2(rand.Next(2, 6), -rand.Next(2, 6));
            ruch.Normalize();

            predkoscPilki = startowaPredkoscPilki;

            pozycja.Y = lokacjaGracza.Y - texturaPilki.Height;
            pozycja.X = lokacjaGracza.X + (lokacjaGracza.Width - texturaPilki.Width) / 2;
        }

        public bool OdDolu()
        {
            if (pozycja.Y > pozycjaOkna.Height)
            {              
                return true;
            }
            return false;
        }

        public void KolizjaGracz(Rectangle lokacjaGracza)
        {
            Rectangle lokacjaPilki = new Rectangle(
                (int)pozycja.X,
                (int)pozycja.Y,
                texturaPilki.Width,
                texturaPilki.Height);

            if (lokacjaGracza.Intersects(lokacjaPilki))
            {
                pozycja.Y = lokacjaGracza.Y - texturaPilki.Height;
                ruch.Y *= -1;
            }
        }

        public void Niszczenie(Blok blok)
        {
            if (!kolizja)
            {
                ruch.Y *= -1;
                kolizja = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texturaPilki, pozycja, Color.White);
        }
        void Reset(Rectangle lokacjaGracza)
        {
            StartowaPozycja(lokacjaGracza);
        }
    }
}
