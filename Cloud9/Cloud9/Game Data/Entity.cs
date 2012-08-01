using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Cloud9
{
    public abstract class Entity : IGameData
    {
        #region Properties
        protected Vector2 position, velocity;     
        protected Sprite sprite;
        protected SpriteEffects spriteEffects;
        protected float drawPriority;
        protected int layer;
        protected bool active;
        protected float radius;        
        protected bool collidesWithOtherEntities, collidesWithTiles;
        protected float gravityEffect;
        
        
        //tilestuff
        protected bool isOnGround = false;
        protected int tileWidth, tileHeight;
        protected float groundLevel;

        protected SpriteBatch SpriteBatch
        {
            get { return World.Instance.SpriteBatch; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        #endregion

        #region Initialization
        public Entity()
        {
            // Im sure something will go here eventually
            
        }
        #endregion

        #region Methods

        public virtual void Update()
        {            
            
            velocity.Y += gravityEffect * World.ElapsedSeconds;
            sprite.Update();
            

            if (collidesWithOtherEntities)
            {
                Entity[] candidates = World.Instance.GetLayer(layer).GetEntities(active).ToArray(); // redundant, because if we are being updated we know we are active, but whatever
                foreach (Entity e in candidates)
                {
                    if (e == this || !e.collidesWithOtherEntities)
                        continue;
                    float distance = Vector2.Distance(e.position, position);
                    if (distance < radius + e.radius)
                        Collide(e);
                }
            }
            if (velocity.Length() == 0)
                return;

            if (collidesWithTiles && World.Instance.GetLayer(layer).HasTiles())
            {
                if (isOnGround)
                {
                    // ground mode
                    position.Y = groundLevel - radius;
                    velocity.Y = 0;

                    // check for walls
                    HorizontalRayChecks();

                    // check to see if he fals
                    CheckTouchingGround();
                }
                else
                {
                    HorizontalRayChecks();
                    CheckTouchingGround();
                }
            }

            position += velocity * World.ElapsedSeconds;

        }


        private void CheckTouchingGround()
        {
            if (velocity.Y < 0)
            {
                isOnGround = false;
                return;
            }
            Vector2 tile;
            //if (RayCastCheck(new Vector2(position.X - radius * 0.5f, position.Y + radius), new Vector2(position.X - radius, position.Y + 20), out tile))
            //{
            //    TouchGround((int)tile.Y);               
            //}
            if (RayCastCheck(new Vector2(position.X, position.Y + radius), new Vector2(position.X, position.Y + radius * 2), out tile))
            {
                TouchGround((int)tile.Y);
            }
            //else if (RayCastCheck(new Vector2(position.X + radius * 0.5f, position.Y + radius), new Vector2(position.X + radius, position.Y + 20), out tile))
            //{
            //    TouchGround((int)tile.Y);
            //}
            else
            {
                isOnGround = false;
                groundLevel = 0;
            }
        }
        private void TouchGround(int y)
        {   
            groundLevel = y * Tile.Size;
            isOnGround = true;  
        }
        private void HorizontalRayChecks()
        {
            Vector2 tile;
            if (RayCastCheck(new Vector2(position.X - tileWidth * Tile.Size / 2, position.Y), new Vector2(position.X + tileWidth * Tile.Size / 2, position.Y), out tile))
            {
                int x = (int)tile.X;
                int y = (int)tile.Y;
                Rectangle bounds = Tile.GetBounds(x, y);

                if (bounds.Center.X > position.X && velocity.X > 0)
                {
                    position.X = bounds.Left - tileWidth * Tile.Size / 2;
                    velocity.X = 0;
                }
                if (bounds.Center.X < position.X && velocity.X < 0)
                {
                    position.X = bounds.Right + tileWidth * Tile.Size / 2;
                    velocity.X = 0;
                }

                
            }
        }

        public bool RayCastCheck(Vector2 lineStart, Vector2 lineEnd, out Vector2 tile)
        {
            int left, right, top, bot;
            left = (int)Math.Min(lineStart.X, lineEnd.X);
            right = (int)Math.Max(lineStart.X, lineEnd.X);
            top = (int)Math.Min(lineStart.Y, lineEnd.Y);
            bot = (int)Math.Max(lineStart.Y, lineEnd.Y);

            left /= Tile.Size;
            right /= Tile.Size;
            top /= Tile.Size;
            bot /= Tile.Size;

            if (left < 0)
                left = 0;
            if (right >= World.Width)
                right = World.Width-1;
            if (top < 0)
                top = 0;
            if (bot >= World.Height)
                bot = World.Height-1;
            

            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bot; y++)
                {
                    if (!World.Instance.GetLayer(layer).GetTile(x, y).Collides)
                        continue;
                    
                    Rectangle rect = Tile.GetBounds(x, y);
                    if (rect.CollidesWithLine(lineStart, lineEnd))
                    {
                        tile = new Vector2(x, y);
                        return true;
                    }
                }
            }
            tile = Vector2.Zero;
            return false;
        }



       

        public virtual void Collide(Entity e)
        {
            
        }

        public virtual void Draw()
        {
            sprite.Draw(SpriteBatch, position - World.Instance.CameraPosition, 1, 0, spriteEffects, Color.White);
        }

        public void ChangeLayers(int target)
        {
            World.Instance.GetLayer(layer).GetEntities(active).Remove(this);
            layer = target;
            World.Instance.GetLayer(layer).GetEntities(active).Add(this);
        }

        public void ChangeActivity()
        {
            World.Instance.GetLayer(layer).GetEntities(active).Remove(this);
            active = !active;
            World.Instance.GetLayer(layer).GetEntities(active).Add(this);
        }
        
        public int CompareTo(object obj)
        {
            Entity e = obj as Entity;
            if (e != null)
                return drawPriority.CompareTo(e.drawPriority);
            return 0;
        }

        
        #endregion
       
    }
}
