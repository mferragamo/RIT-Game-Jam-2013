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

namespace Other_Eyes_2010
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AudioManager am;

        // Storage
        Player _player;
        List<Character> _characters;

        // Input handling
        KeyboardState kbState;
        KeyboardState prevKbState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize keyboard states
            kbState = Keyboard.GetState();
            prevKbState = kbState;

            // Create lists
            _characters = new List<Character>();

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

            ConstantsApp.IMAGES["player"] = this.Content.Load<Texture2D>("elements/temp_player");
            ConstantsApp.IMAGES["character"] = this.Content.Load<Texture2D>("elements/temp_character");

            _player = new Player("player", "elements/temp_player", new Vector2(300, 300));

            Character tCharacter = new Character("character", "elements/temp_character", new Vector2(100, 100));
            _characters.Add(tCharacter);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            am.Update();

            // Update keyboard states
            prevKbState = kbState;
            kbState = Keyboard.GetState();

            _player.HandleInput(kbState, prevKbState, GraphicsDevice);
            _player.Update(gameTime);
            foreach (Character c in _characters) {
                c.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            _player.DrawCharacter(spriteBatch);

            foreach(Character c in _characters){
                c.DrawCharacter(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
