using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cloud9
{

    public class AirShip : Mount
    {
        #region Properties
        int maxSpeedX = 400;
        int accelX = 800;
        int maxSpeedY = 400;
        int accelY = 1000;
        #endregion

        #region Initialization
        public AirShip()
            : base()
        {
            Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
            animations.Add("Idle", new Animation(0, 1, false, "Cloud 9/blocks/Ships/full"));

            sprite = new Sprite(animations);
            sprite.PlayAnimation("Idle");





            this.layer = 2;
            this.position = new Vector2(5000, 50 * Tile.Size - 300);
            this.velocity = new Vector2(0, 100);

            this.drawPriority = 0;
            this.collidesWithTiles = false;
            this.collidesWithOtherEntities = false;
            this.gravityEffect = 10f;

            this.active = true;

            this.radius = 42;

            this.tileHeight = 2;
            this.tileWidth = 1;

            this.spriteEffects = SpriteEffects.None;
        }
        #endregion

        #region Methods
        public override Vector2 GetPlayerOffset()
        {
            return new Vector2((spriteEffects == SpriteEffects.None ? 1 : -1) * 20, 0);
        }

        protected override void UpdateInput()
        {
            if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
                if (velocity.X > -maxSpeedX)
                {
                    velocity.X -= accelX * World.ElapsedSeconds;
                    if (velocity.X < -maxSpeedX)
                        velocity.X = -maxSpeedX;
                }
            }
            else if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {

                spriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
                if (velocity.X < maxSpeedX)
                {
                    velocity.X += accelX * World.ElapsedSeconds;
                    if (velocity.X > maxSpeedX)
                        velocity.X = maxSpeedX;
                }
            }


            if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {


                if (velocity.Y > -maxSpeedY)
                {
                    velocity.Y -= accelY * World.ElapsedSeconds;
                    if (velocity.Y < -maxSpeedY)
                        velocity.Y = -maxSpeedY;
                }
            }
            else if (Input.Instance.KeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {


                if (velocity.Y < maxSpeedY)
                {
                    velocity.Y += accelY * World.ElapsedSeconds;
                    if (velocity.Y > maxSpeedY)
                        velocity.Y = maxSpeedY;
                }
            }
        }



        #endregion

    }
}
