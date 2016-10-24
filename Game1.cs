using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace Teka
{

    // ----------------------------------------------------------------------------------INTRO------------------------------------------------------------------------------------------------
    // Alexandre Dufresne et Samuel Joyal
    // Projet MonoGame
    // TEKA
    // "The enchanted kingdom of Astria"
    //-----------------------------------------------------------------------------------INTRO------------------------------------------------------------------------------------------------

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        KeyboardState keys = new KeyboardState();

        KeyboardState previousKeys = new KeyboardState();

        Rectangle fenetre;

        GameObjectAnime persoPrincipal;

        GameObject mapIntro;

        GameObject maisonPrincipal;
        GameObject maisonPrincipalOuverte;

        GameObject interieurMaison;

        GameObject transparente;

        SoundEffect sonAlex;
        SoundEffectInstance alexSong;

        SoundEffect sonPorte;
        SoundEffectInstance bruitPorte;

        GameObject curseur;
        Vector2 m_mousePos;

        GameObject buisson;

        GameObject sceneBataille;


        Vector2 positionProjectile;


        Random rnd = new Random();

        GameObject introWall;

        GameObject flecheDroite1;

        GameObject flecheDroite2;

        GameObject flecheGauche1;

        GameObject flecheGauche2;

        bool intro = true;
        bool jeu = false;
        int choix = 1;




        int chancesBataille =0;

        bool battle = false;

        int monde = 0;

        int compteur = 0;

        int direction = 0;

        string changementPiece = "off";

        int time = 0;

        int intersection;

        int temps;


        // ---------------------------------------- Variable caractéristiques perso principal ---------------------------------------------

        int niveau = 1;
        int exp = 0;
        int expGagne = 0;
        int expProchainNiveau = 30;
        string nomPersonnage;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
           fenetre.Width = graphics.PreferredBackBufferWidth = 1920;
           fenetre.Height = graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            persoPrincipal = new GameObjectAnime();


            persoPrincipal.estVivant = true;  //seulement pour l'instant
            persoPrincipal.vitesse = 0;   //c'est la map qui bouge
            persoPrincipal.sprite = Content.Load<Texture2D>("SpriteSheet.png"); // SpriteSheet
            persoPrincipal.position = new Rectangle(0, 0, 50, 72);
            persoPrincipal.direction = Vector2.Zero;
            persoPrincipal.etat = GameObjectAnime.Etat.AttenteBas;
            persoPrincipal.position.Offset(fenetre.Width / 2 - 26, fenetre.Height / 2 - 36);


            mapIntro = new GameObject();
            mapIntro.estVivant = true;
            mapIntro.vitesse = 5;
            mapIntro.sprite = Content.Load<Texture2D>("MapIntro.png");
            mapIntro.position = mapIntro.sprite.Bounds;
            mapIntro.position.Offset(0,0);

            maisonPrincipal = new GameObject();
            maisonPrincipal.estVivant = true;
            maisonPrincipal.vitesse = 0;
            maisonPrincipal.sprite = Content.Load<Texture2D>("MaisonPrincipale.png");
            maisonPrincipal.position = maisonPrincipal.sprite.Bounds;
            maisonPrincipal.position.Offset(mapIntro.position.X +300, mapIntro.position.Y + 1150);

            maisonPrincipalOuverte = new GameObject();
            maisonPrincipalOuverte.estVivant = true;
            maisonPrincipalOuverte.vitesse = 0;
            maisonPrincipalOuverte.sprite = Content.Load<Texture2D>("MaisonPrincipaleOuverte.png");
            maisonPrincipalOuverte.position = maisonPrincipalOuverte.sprite.Bounds;
            maisonPrincipalOuverte.position.Offset(mapIntro.position.X + 300, mapIntro.position.Y + 1150);

            interieurMaison = new GameObject();
            interieurMaison.estVivant = true;
            interieurMaison.vitesse = 0;
            interieurMaison.sprite = Content.Load<Texture2D>("InterieurMaison.png");
            interieurMaison.position = interieurMaison.sprite.Bounds;
            interieurMaison.position.Offset(mapIntro.position.X + 300, mapIntro.position.Y + 470);

            transparente = new GameObject();
            transparente.estVivant = true;
            transparente.vitesse = 0;
            transparente.sprite = Content.Load<Texture2D>("Transparente.png");
            transparente.position = transparente.sprite.Bounds;
            transparente.position.Offset(mapIntro.position.X + 560, mapIntro.position.Y + 1680);

            sonAlex = Content.Load<SoundEffect>("SongTeka");
            alexSong = sonAlex.CreateInstance();

            sonPorte = Content.Load<SoundEffect>("Click");
            bruitPorte = sonPorte.CreateInstance();

            curseur = new GameObject();
            curseur.sprite = Content.Load<Texture2D>("SwordCursor.png");
            curseur.position = curseur.sprite.Bounds;
            curseur.position.Offset(-760, 235);

            buisson = new GameObject();
            buisson.estVivant = true;
            buisson.sprite = Content.Load<Texture2D>("Bushes.png");
            buisson.position = buisson.sprite.Bounds;
            buisson.position.Offset(mapIntro.position.X + 1350, mapIntro.position.Y + 25);

            sceneBataille = new GameObject();
            sceneBataille.estVivant = true;
            sceneBataille.sprite = Content.Load<Texture2D>("FightingScene.png");
            sceneBataille.position = sceneBataille.sprite.Bounds;
            sceneBataille.position.Offset(0,0);

            flecheDroite1 = new GameObject();
            flecheDroite1.estVivant = true;
            flecheDroite1.sprite = Content.Load<Texture2D>("FlecheDroiteUp");
            flecheDroite1.position = flecheDroite1.sprite.Bounds;
            flecheDroite1.position.Offset(persoPrincipal.position.X + flecheDroite1.sprite.Width, persoPrincipal.position.Y);

            flecheDroite2 = new GameObject();
            flecheDroite2.estVivant = false;
            flecheDroite2.sprite = Content.Load<Texture2D>("FlecheDroiteDown");
            flecheDroite2.position = flecheDroite2.sprite.Bounds;
            flecheDroite2.position.Offset(persoPrincipal.position.X + flecheDroite2.sprite.Width, persoPrincipal.position.Y);

            flecheGauche1 = new GameObject();
            flecheGauche1.estVivant = true;
            flecheGauche1.sprite = Content.Load<Texture2D>("FlecheGaucheUp");
            flecheGauche1.position = flecheGauche1.sprite.Bounds;
            flecheGauche1.position.Offset(persoPrincipal.position.X - flecheGauche1.sprite.Width - 55, persoPrincipal.position.Y);

            flecheGauche2 = new GameObject();
            flecheGauche2.estVivant = false;
            flecheGauche2.sprite = Content.Load<Texture2D>("FlecheGaucheDown");
            flecheGauche2.position = flecheGauche2.sprite.Bounds;
            flecheGauche2.position.Offset(persoPrincipal.position.X - flecheGauche2.sprite.Width - 55, persoPrincipal.position.Y);

            introWall = new GameObject();
            introWall.sprite = Content.Load<Texture2D>("IntroWall");
            introWall.position = introWall.sprite.Bounds;
            introWall.position.Offset(fenetre.X, fenetre.Y);





            // TODO: use this.Content to load your game content here
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

  /*---------------------------------------------------------------------------------------------------------------------------------------------------------------
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   *                                                                Update --- Update --- Update
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        protected override void Update(GameTime gameTime)
        {


            /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

            * 
            *                                                         Intro --- Intro --- Intro
            *                                                         
            *                                                         
            * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/



            if (intro == true)
            {
                flecheDroite1.estVivant = true;
                flecheDroite2.estVivant = false;
                flecheGauche1.estVivant = true;
                flecheGauche2.estVivant = false;
                persoPrincipal.etat = GameObjectAnime.Etat.AttenteBas;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    flecheGauche2.estVivant = true;
                    flecheGauche1.estVivant = false;
                    if (choix != 0)
                    {
                        choix--;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    flecheDroite2.estVivant = true;
                    flecheDroite1.estVivant = false;
                    if (choix != 2) // 3 quand il vas y avoir le juggertnaut
                    {
                        choix++;
                    }
                }
                if (choix == 1)
                {
                    persoPrincipal.sprite = Content.Load<Texture2D>("SpriteSheet.png");
                    //aussi ajouter ici quand on vas avoir la vie l attaque defense et tout le kit
                }
                if (choix == 2)
                {
                    persoPrincipal.sprite = Content.Load<Texture2D>("WarriorPersoPrincipal.png");
                }




                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    intro = false;
                    jeu = true;
                }



            }

           if (jeu == true)
            {
                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

           * 
           *                                                         Souris --- Souris --- Souris
           *                                                         
           *                                                         
           * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

                MouseState mouseState = Mouse.GetState();
                m_mousePos.X = mouseState.X;
                m_mousePos.Y = mouseState.Y;

                curseur.position.X = mouseState.X;
                curseur.position.Y = mouseState.Y;
                /*
                            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                            {

                                projectile.estVivant = true;

                                projectile.position.X = persoPrincipal.position.X;
                                projectile.position.Y = persoPrincipal.position.Y;

                            }else if (Keyboard.GetState().IsKeyUp(Keys.Space))
                            {

                                positionProjectile = m_mousePos;

                                projectile.position.X += curseur.position.X;
                                projectile.position.Y -= curseur.position.Y;
                            }

                            */





                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Chanson --- Musique --- Son
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                alexSong.Play();



                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Exit --- Exit --- Exit
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/



                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();


                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                              Animation Personnage --- Animation Personnage --- Animation personnage
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/



                if (changementPiece != "on")
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {

                        persoPrincipal.etat = GameObjectAnime.Etat.RunHaut;
                        direction = 1;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {

                        persoPrincipal.etat = GameObjectAnime.Etat.RunBas;
                        direction = 2;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {

                        persoPrincipal.etat = GameObjectAnime.Etat.RunGauche;
                        direction = 3;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {

                        persoPrincipal.etat = GameObjectAnime.Etat.RunDroite;
                        direction = 4;
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                    {
                        direction = 0;

                        if (persoPrincipal.etat == GameObjectAnime.Etat.RunHaut)
                        {
                            persoPrincipal.etat = GameObjectAnime.Etat.AttenteHaut;
                        }
                        if (persoPrincipal.etat == GameObjectAnime.Etat.RunBas)
                        {
                            persoPrincipal.etat = GameObjectAnime.Etat.AttenteBas;
                        }
                        if (persoPrincipal.etat == GameObjectAnime.Etat.RunGauche)
                        {
                            persoPrincipal.etat = GameObjectAnime.Etat.AttenteGauche;
                        }
                        if (persoPrincipal.etat == GameObjectAnime.Etat.RunDroite)
                        {
                            persoPrincipal.etat = GameObjectAnime.Etat.AttenteDroite;
                        }
                    }
                }

                keys = Keyboard.GetState();

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {

                    mapIntro.vitesse = 10;

                }






                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

               * 
               *                                   Changement de piece --- Changement de piece --- Changement de piece
               *                                                         
               *                                                         
               * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                if (persoPrincipal.position.Intersects(transparente.position) && compteur == 0 && Keyboard.GetState().IsKeyDown(Keys.W))
                {

                    changementPiece = "on";


                    direction = 1;
                    persoPrincipal.etat = GameObjectAnime.Etat.RunHaut;
                    monde = 1;


                    bruitPorte.Play();


                }



                if (persoPrincipal.position.Intersects(transparente.position) && compteur == 1 && Keyboard.GetState().IsKeyDown(Keys.S))
                {

                    changementPiece = "on";


                    direction = 2;
                    persoPrincipal.etat = GameObjectAnime.Etat.RunBas;
                    monde = 0;


                    bruitPorte.Play();


                }



                if (persoPrincipal.position.Intersects(transparente.position))
                {


                }
                else if (changementPiece == "on")
                {

                    if (compteur == 1)
                    {
                        monde = 0;
                        compteur = 0;
                    }


                    else
                    {
                        monde = 2;
                        compteur = 1;
                    }

                    changementPiece = "off";
                }

                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                *                                                                                      ___
                *                                              Rince --- Wash --- Repeat --- VICTORY  | 0 |  ------  (Bataille)
                *                                                                                     |_-_|                     
                *                                                                                     |_|_|
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                if (persoPrincipal.position.Intersects(buisson.position) && time + 0.5 <= gameTime.TotalGameTime.Seconds)
                {

                    time = gameTime.TotalGameTime.Seconds;



                    chancesBataille = rnd.Next(1, 10);


                    if (chancesBataille == 7)
                    {



                        Battle();


                    }


                }









                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Direction --- Direction --- Direction
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                switch (direction)
                {

                    case 1:

                        mapIntro.position.Y += mapIntro.vitesse;
                        maisonPrincipal.position.Y += mapIntro.vitesse;
                        transparente.position.Y += mapIntro.vitesse;
                        maisonPrincipalOuverte.position.Y += mapIntro.vitesse;
                        interieurMaison.position.Y += mapIntro.vitesse;
                        buisson.position.Y += mapIntro.vitesse;


                        break;

                    case 2:
                        mapIntro.position.Y -= mapIntro.vitesse;
                        maisonPrincipal.position.Y -= mapIntro.vitesse;
                        transparente.position.Y -= mapIntro.vitesse;
                        maisonPrincipalOuverte.position.Y -= mapIntro.vitesse;
                        interieurMaison.position.Y -= mapIntro.vitesse;
                        buisson.position.Y -= mapIntro.vitesse;


                        break;

                    case 3:
                        mapIntro.position.X += mapIntro.vitesse;
                        maisonPrincipal.position.X += mapIntro.vitesse;
                        transparente.position.X += mapIntro.vitesse;
                        maisonPrincipalOuverte.position.X += mapIntro.vitesse;
                        interieurMaison.position.X += mapIntro.vitesse;
                        buisson.position.X += mapIntro.vitesse;


                        break;

                    case 4:
                        mapIntro.position.X -= mapIntro.vitesse;
                        maisonPrincipal.position.X -= mapIntro.vitesse;
                        transparente.position.X -= mapIntro.vitesse;
                        maisonPrincipalOuverte.position.X -= mapIntro.vitesse;
                        interieurMaison.position.X -= mapIntro.vitesse;
                        buisson.position.X -= mapIntro.vitesse;


                        break;

                }

                ChangementDeMonde();
                if (changementPiece != "on")
                {

                    BoundsMaisonPersoPrincipal();
                }





            }
            persoPrincipal.Update(gameTime);
            base.Update(gameTime);
        }


/*---------------------------------------------------------------------------------------------------------------------------------------------------------------
* 
* 
* 
* 
* 
* 
* 
* 
* 
*                                                                Fonction --- Fonction --- Fonction
* 
* 
* 
* 
* 
* 
* 
* 
* 
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/




        protected void Battle()
        {

            monde = 3;


        }



/*---------------------------------------------------------------------------------------------------------------------------------------------------------------

* 
*                                                         Bounds --- Maison Principal --- Repositionnement des images
*                                                         
*                                                         
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/



        protected void BoundsMaisonPersoPrincipal()
        {
            if (mapIntro.estVivant == true)
            {


                if (persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width > maisonPrincipal.position.Left && persoPrincipal.position.X < maisonPrincipal.position.Right && persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height > maisonPrincipal.position.Top && persoPrincipal.position.Y < maisonPrincipal.position.Bottom)
                {

                    mapIntro.vitesse = 0;
                    if (persoPrincipal.position.X > maisonPrincipal.position.Left && direction == 3)
                    {
                        maisonPrincipal.position.X = persoPrincipal.position.X - maisonPrincipal.position.Width;
                        mapIntro.position.X = maisonPrincipal.position.X - 300;
                        transparente.position.X = mapIntro.position.X + 560;
                        interieurMaison.position.X = mapIntro.position.X + 300;
                        maisonPrincipalOuverte.position.X = mapIntro.position.X + 300;
                        buisson.position.X = mapIntro.position.X + 1350;
                    }
                    if (persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width < maisonPrincipal.position.Right && direction == 4)
                    {

                        maisonPrincipal.position.X = persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width;
                        mapIntro.position.X = maisonPrincipal.position.X - 300;
                        transparente.position.X = mapIntro.position.X + 560;
                        interieurMaison.position.X = mapIntro.position.X + 300;
                        maisonPrincipalOuverte.position.X = mapIntro.position.X + 300;
                        buisson.position.X = mapIntro.position.X + 1350;
                    }
                }
                if (persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height > maisonPrincipal.position.Top && persoPrincipal.position.Y < maisonPrincipal.position.Bottom && persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width > maisonPrincipal.position.Left && persoPrincipal.position.X < maisonPrincipal.position.Right)
                {

                    mapIntro.vitesse = 0;
                    if (persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height > maisonPrincipal.position.Top && direction == 2)
                    {

                        maisonPrincipal.position.Y = persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height;
                        mapIntro.position.Y = maisonPrincipal.position.Y - 1150;
                        transparente.position.Y = mapIntro.position.Y + 1680;
                        interieurMaison.position.Y = mapIntro.position.Y + 470;
                        maisonPrincipalOuverte.position.Y = mapIntro.position.Y + 1150;
                        buisson.position.Y = mapIntro.position.Y + 25;
                    }
                    if (persoPrincipal.position.Y < maisonPrincipal.position.Bottom && direction == 1)
                    {

                        maisonPrincipal.position.Y = persoPrincipal.position.Y - maisonPrincipal.position.Height;
                        mapIntro.position.Y = maisonPrincipal.position.Y - 1150;
                        transparente.position.Y = mapIntro.position.Y + 1680;
                        interieurMaison.position.Y = mapIntro.position.Y + 470;
                        maisonPrincipalOuverte.position.Y = mapIntro.position.Y + 1150;
                        buisson.position.Y = mapIntro.position.Y + 25;
                    }
                }


                mapIntro.vitesse = 5;



            }


        }



/*---------------------------------------------------------------------------------------------------------------------------------------------------------------

* 
*                                     Changement de monde --- Differentes Places --- Differents Mondes
*                                                         
*                                                         
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/



        protected void ChangementDeMonde()
        {

            mapIntro.estVivant = false;
            maisonPrincipal.estVivant = false;
            transparente.estVivant = true;
            maisonPrincipalOuverte.estVivant = false;
            interieurMaison.estVivant = false;
            buisson.estVivant = false;
            sceneBataille.estVivant = false;

            switch (monde)
            {

                case 0: // la map / monde d'origine

                    mapIntro.estVivant = true;
                    maisonPrincipal.estVivant = true;
                    transparente.estVivant = true;
                    buisson.estVivant = true;

                    break;

                case 1: // interlude quand on entre dans la maison principal

                    maisonPrincipalOuverte.estVivant = true;
                    mapIntro.estVivant = true; 

                    break;

                case 2: // dans la maison principal

                    interieurMaison.estVivant = true;

                    break;

                case 3:

                    sceneBataille.estVivant = true;

                    break;
            }



        }



/*---------------------------------------------------------------------------------------------------------------------------------------------------------------
* 
* 
* 
* 
* 
* 
* 
* 
*
* 
*                                                                Drawing --- des beaux --- Dessins
* 
* 
* 
* 
* 
* 
* 
* 
* 
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if(jeu == true)
            {
                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

* 
*                                                         Le --- Monde --- Zero
*                                                         
*                                                         
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

                if (mapIntro.estVivant == true)
                {
                    spriteBatch.Draw(mapIntro.sprite, mapIntro.position, Color.White);

                }


                if (transparente.estVivant == true)
                {

                    spriteBatch.Draw(transparente.sprite, transparente.position, Color.White);

                }

                if (buisson.estVivant == true)
                {

                    spriteBatch.Draw(buisson.sprite, buisson.position, Color.White);
                }


                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Maison --- et ses --- Composantes
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                if (interieurMaison.estVivant == true)
                {
                    spriteBatch.Draw(interieurMaison.sprite, interieurMaison.position, Color.White);

                }

                if (maisonPrincipalOuverte.estVivant == true)
                {
                    spriteBatch.Draw(maisonPrincipalOuverte.sprite, maisonPrincipalOuverte.position, Color.White);

                }


                if (maisonPrincipal.estVivant == true)
                {

                    spriteBatch.Draw(maisonPrincipal.sprite, maisonPrincipal.position, Color.White);
                }






                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Scene --- de --- Batailles
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/






                if (sceneBataille.estVivant == true)
                {

                    spriteBatch.Draw(sceneBataille.sprite, sceneBataille.position, Color.White);

                }

                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Personnages --- tant gentil --- que mechant
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                if (persoPrincipal.estVivant == true)
                {

                    spriteBatch.Draw(persoPrincipal.sprite, persoPrincipal.position, persoPrincipal.spriteAffiche, Color.White);
                }


                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Curseur --- epee --- qui se deplace
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                spriteBatch.Draw(curseur.sprite, curseur.position, Color.White);
            }


            /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

            * 
            *                                                         Intro --- dessin --- bruhh
            *                                                         
            *                                                         
            * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

            if (intro == true)
            {
                spriteBatch.Draw(introWall.sprite, introWall.position, Color.White);
                if (flecheDroite1.estVivant == true)
                {
                    spriteBatch.Draw(flecheDroite1.sprite, flecheDroite1.position, Color.White);
                }
                if (flecheDroite2.estVivant == true)
                {
                    spriteBatch.Draw(flecheDroite2.sprite, flecheDroite2.position, Color.White);
                }
                if (flecheGauche1.estVivant == true)
                {
                    spriteBatch.Draw(flecheGauche1.sprite, flecheGauche1.position, Color.White);
                }
                if (flecheGauche2.estVivant == true)
                {
                    spriteBatch.Draw(flecheGauche2.sprite, flecheGauche2.position, Color.White);
                }
                spriteBatch.Draw(persoPrincipal.sprite, persoPrincipal.position, persoPrincipal.spriteAffiche, Color.White);

            }
            

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}