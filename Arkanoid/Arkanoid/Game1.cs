using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Arkanoid
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D tlo;
        Texture2D texturaBloku;
        Song muzyka;

        Rectangle pozycjaOkna;

        int szerokoscBloku = 16;
        int wysokoscBloku = 6;
        public int blokiIstniejace;
        

        public Pi³ka Pilka { get; set; }

        public Gracz Gracz { get; set; }


        public Blok[,] bloki { get; set; }

        public HUD hud { get; set; }

        enum GameState { Przegrana, Wygrana, wTrakcie };
        GameState stanGry;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            pozycjaOkna = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

  
        protected override void Initialize()
        {
            base.Initialize();

        }

   
        protected override void LoadContent()
        {
       
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tlo = Content.Load<Texture2D>(@"PlikiGraficzne/tlo"); 
 
            muzyka = Content.Load<Song>("muzyka");           

            Texture2D tempTexture = Content.Load<Texture2D>(@"PlikiGraficzne/belka");
            Gracz = new Gracz(tempTexture, pozycjaOkna);
            
            hud = new HUD(this);
            
            tempTexture = Content.Load<Texture2D>(@"PlikiGraficzne/pilka");
            Pilka = new Pi³ka(tempTexture, pozycjaOkna);

            texturaBloku = Content.Load<Texture2D>(@"PlikiGraficzne/blok");

            MediaPlayer.Play(muzyka);
            MediaPlayer.IsRepeating = true;
            
            StartGame();

        }

        public void StartGame()
        {
            
            Gracz.pkt = 0;
            Gracz.zycia = 3;
            blokiIstniejace = 90;

            stanGry = GameState.wTrakcie;

            Gracz.PozycjaStartowa();
            Pilka.StartowaPozycja(Gracz.RetStartPoz());            
            
            bloki = new Blok[szerokoscBloku, wysokoscBloku];

            for (int y = 0; y < wysokoscBloku; y++)
            {
                Color kolor = Color.White;

                switch (y)
                {
                    case 0:
                        kolor = Color.Purple;
                        break;
                    case 1:
                        kolor = Color.Aqua;
                        break;
                    case 2:
                        kolor = Color.Red;
                        break;
                    case 3:
                        kolor = Color.Orange;
                        break;
                    case 4:
                        kolor = Color.Yellow;
                        break;
                    case 5:
                        kolor = Color.GhostWhite;
                        break;
                }

                for (int x = 0; x < szerokoscBloku; x++)
                {
                    bloki[x, y] = new Blok(
                        texturaBloku,
                        new Rectangle(
                            x * texturaBloku.Width,
                            y * texturaBloku.Height,
                            texturaBloku.Width,
                            texturaBloku.Height),
                        kolor);
                  
                }  
            }
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Gracz.Update();
            Pilka.Update();          

            foreach (Blok blok in bloki)
            {
                blok.SprawdzKolizje(Pilka);
                if (blok.blokiIstniejace < blokiIstniejace)
                {
                    blokiIstniejace=blokiIstniejace-1;
                }
                                                  
            }

            Pilka.KolizjaGracz(Gracz.RetStartPoz());

            if (Pilka.OdDolu())
            {
                Gracz.zycia--;
                Gracz.PozycjaStartowa();
                Pilka.StartowaPozycja(Gracz.RetStartPoz()); 
            }              

            if (stanGry == GameState.Przegrana)
                StartGame();
            if (stanGry == GameState.Wygrana)
                Wygrana();

            if ( blokiIstniejace<= 0)
                stanGry = GameState.Wygrana;
            if (Gracz.zycia <= 0)
            {
                stanGry = GameState.Przegrana;
                hud.Menu("Przegrales");
            }
               
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            spriteBatch.Begin();
            spriteBatch.Draw(tlo, GraphicsDevice.Viewport.TitleSafeArea, Color.White); 

            foreach (Blok blok in bloki)
                blok.Draw(spriteBatch);

            Pilka.Draw(spriteBatch);
            Gracz.Draw(spriteBatch);

            spriteBatch.End();
 
            base.Draw(gameTime);
        }

        
        internal void Wygrana()
        {            
            hud.Menu("Wygrales!");
        }
    }
}
