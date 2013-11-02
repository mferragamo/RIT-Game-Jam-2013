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
     * @description - This is the base class for all moving characters. All moving objects
     * should extend from this.
     *****************************************************************************************/
    class Character
    {
        // Storage
        private string      _name;
        private Texture2D   _asset;
        private string      _assetName;

        // Positioning
        private Vector2     _pos;
        private Vector2     _velocity;
        private Vector2     _acceleration;
        private Vector2     _target;
        private double      _maxSpeed;
        private double      _maxForce;
        
        // Drawing
        private Vector2     _origin;
        private Vector2     _scale;

        /*********************************************************************
         * Constructor
         *********************************************************************/
        public Character(string pName, string pAsset, Vector2 pPos)
        {
            _name = pName;
            _assetName = pAsset;
            _asset = ConstantsApp.IMAGES[_name];

            _pos = pPos;
            _target = pPos;
            _velocity = new Vector2(0, 0);
            _acceleration = new Vector2(0, 0);
            _maxSpeed = 5;
            _maxForce = 0.1;

            _origin = new Vector2((float)0.5, (float)0.5);
            _scale = new Vector2(1, 1);

            Console.WriteLine(pName + " created at " + pPos.X + ", " + pPos.Y);
        }

        /*********************************************************************
         * Properties
         *********************************************************************/
        public Vector2 pos {
            get { return _pos; }
            set { _pos = value; }
        }

        public Vector2 velocity {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector2 acceleration {
            get { return _acceleration; }
            set { _acceleration = value; }
        }

        /*********************************************************************
         * Methods
         *********************************************************************/
        /*********************************************************************
         * @description - Calls the character to move to a certain point
         * @param - (pEndPos) The desired final position for the character
         *********************************************************************/
        public void SetTarget(Vector2 pEndPos)
        {
            _target = pEndPos;
        }

        /*********************************************************************
         * @description - Updates Character
         * @param - (dt) Provides a snapshot of timing values
         *********************************************************************/
        public void Update(GameTime dt)
        {
            Seek(_target);
            // Add acceleration to velocity, limit to maxspeed
            velocity = Vector2.Add(velocity, acceleration);
            //velocity = Vector2.Normalize(velocity);
            //velocity = Vector2.Multiply(velocity, (float)_maxSpeed);

            // Update position with velocity
            pos = Vector2.Add(pos, velocity);

            // Reset acceleration
            acceleration = Vector2.Multiply(acceleration, 0);
        }

        /*********************************************************************
         * @description - Adds a force to acceleration
         * @param - (pForce) The amount of force to be applied
         *********************************************************************/
        private void ApplyForce(Vector2 pForce)
        {
            acceleration = Vector2.Add(acceleration, pForce);
        }

        /*********************************************************************
         * @description - Seeks a target point
         * @param - (pTarget) Point to seek
         *********************************************************************/
        private void Seek(Vector2 pTarget)
        {
            Vector2 tDesired = Vector2.Subtract(pTarget, pos);
            //tDesired = Vector2.Normalize(tDesired);
            //tDesired = Vector2.Multiply(tDesired, (float)_maxSpeed);

            Vector2 tSteer = Vector2.Subtract(tDesired, velocity);
            //tSteer = Vector2.Normalize(tSteer);
            //tSteer = Vector2.Multiply(tSteer, (float)_maxForce);

            ApplyForce(tSteer);
        }

        /*********************************************************************
         * @description - Draws the character on screen
         * @param - (tSB) SpriteBatch from main game class
         *********************************************************************/
        public void DrawCharacter(SpriteBatch tSB)
        {
            tSB.Draw(_asset, _pos, null, Color.White, 0, _origin, _scale, SpriteEffects.None, 1);
        }
    }
}
