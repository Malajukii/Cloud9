using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cloud9
{
    // this will actually inherit some class that contains life and stuff,
    // but i just want to get a working copy of the game going
    public class Player : Entity
    {
        #region Properties
        const int playerMaxSpeed = 180;
        const int playerAcell = 4000;

        //this would be in the inventory normally
        AirShip airShip;

        // inventory.. stuff like that
        #endregion

        #region Initialization
        public Player()
            : base()
        {
            Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
            animations.Add("Idle", new Animation(0, 1, false, "Cloud 9/Plyr_big"));
            animations.Add("Run", new Animation(0.1f, 4, true, "Cloud 9/Player/Plyr_bigloop"));

            sprite = new Sprite(animations);
            sprite.PlayAnimation("Idle");

            airShip = new AirShip();
            airShip.Spawn();
            airShip.ChangeActivity();



            this.layer = 2;
            this.position = new Vector2(5000, 50 * Tile.Size - 300);
            this.velocity = new Vector2(0, 100);

            this.drawPriority = 0;
            this.collidesWithTiles = true;
            this.collidesWithOtherEntities = true;
            this.gravityEffect = 1000f;

            this.active = true;

            this.radius = 16;

            this.tileHeight = 2;
            this.tileWidth = 1;

            this.spriteEffects = SpriteEffects.None;
        }
        #endregion

        #region Methods
        public override void Update()
        {            
            HandleInput();
            base.Update();
        }

        private void HandleInput()
        {
            if (mount == null)
            {
                if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                {
                    sprite.PlayAnimation("Run");
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    if (velocity.X > -playerMaxSpeed)
                    {
                        velocity.X -= playerAcell * World.ElapsedSeconds;
                        if (velocity.X < -playerMaxSpeed)
                            velocity.X = -playerMaxSpeed;
                    }
                }
                else if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.D))
                {
                    sprite.PlayAnimation("Run");
                    spriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
                    if (velocity.X < playerMaxSpeed)
                    {
                        velocity.X += playerAcell * World.ElapsedSeconds;
                        if (velocity.X > playerMaxSpeed)
                            velocity.X = playerMaxSpeed;
                    }
                }
                else if (isOnGround)
                {
                    sprite.PlayAnimation("Idle");
                    velocity.X /= 1.1f;
                }

                //if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.W))
                //{
                //    sprite.PlayAnimation("Run");

                //    if (velocity.Y > -playerMaxSpeed)
                //    {
                //        velocity.Y -= playerAcell * World.ElapsedSeconds;
                //        if (velocity.Y < -playerMaxSpeed)
                //            velocity.Y = -playerMaxSpeed;
                //    }
                //}
                //else if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.S))
                //{
                //    sprite.PlayAnimation("Run");

                //    if (velocity.Y < playerMaxSpeed)
                //    {
                //        velocity.Y += playerAcell * World.ElapsedSeconds;
                //        if (velocity.Y > playerMaxSpeed)
                //            velocity.Y = playerMaxSpeed;
                //    }
                //}

                if (Input.Instance.KeyNewPressed(Microsoft.Xna.Framework.Input.Keys.Q))
                {
                    if (layer > 0)
                        ChangeLayers(layer - 1);
                }
                if (Input.Instance.KeyNewPressed(Microsoft.Xna.Framework.Input.Keys.E))
                {
                    if (layer < 5)
                        ChangeLayers(layer + 1);
                }


                if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.W) && isOnGround)
                {
                    velocity.Y -= 400;
                    isOnGround = false;
                }
                if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.W) && velocity.Y < 0)
                {
                    velocity.Y -= gravityEffect / 2 * World.ElapsedSeconds;
                }
            }

            if (Input.Instance.KeyNewPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                airShip.ChangeActivity();
                if (mount == null)
                {
                    mount = airShip;
                    airShip.Position = position;
                    mount.Ride(this);
                    sprite.PlayAnimation("Idle");
                }
                else
                {
                    mount.Dismount();
                    mount = null;
                }
            }

            if (Input.Instance.KeyNewPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                World.Instance.DrawTiles = !World.Instance.DrawTiles;
        }
        #endregion
    }
}
