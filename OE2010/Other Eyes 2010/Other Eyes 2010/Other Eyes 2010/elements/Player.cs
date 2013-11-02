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

namespace Other_Eyes_2010
{
    /*****************************************************************************************
     * @author - Frankie Libbon
     * @description - This is the player class. It extends from the Character class and
     * handles player-specific information, such as input handling
     *****************************************************************************************/
    class Player : Character
    {
        private KeyboardState _keyState;
        private KeyboardState _prevKeyState;

        private int _speed;

        /*********************************************************************
         * Constructor
         *********************************************************************/
        public Player(string pName, string pAsset, Vector2 pPos) :
            base(pName, pAsset, pPos)
        {
            _speed = 5;
        }

        public void HandleInput(KeyboardState pState, KeyboardState pPrevState, GraphicsDevice pGD)
        {
            _keyState = pState;
            _prevKeyState = pPrevState;

            if (_keyState.IsKeyDown(Keys.Up))
            {
                SetTarget(new Vector2(pos.X, pos.Y - _speed));
            }
            if (_keyState.IsKeyDown(Keys.Down))
            {
                SetTarget(new Vector2(pos.X, pos.Y + _speed));
            }
            if (_keyState.IsKeyDown(Keys.Right))
            {
                SetTarget(new Vector2(pos.X + _speed, pos.Y));
            }
            if (_keyState.IsKeyDown(Keys.Left))
            {
                SetTarget(new Vector2(pos.X - _speed, pos.Y));
            }
        }
    }
}
