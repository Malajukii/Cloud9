using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Cloud9
{
    public abstract class Mount : Entity
    {
        #region Properties
        protected Entity rider;

        
        #endregion

        #region Initialization
        public Mount()
            : base()
        {
        }
        #endregion

        #region Methods
        public abstract Vector2 GetPlayerOffset();

        public void Ride(Entity e)
        {
            rider = e;
        }
        public void Dismount()
        {
            rider = null;
        }

        public override void Update()
        {
            if (rider is Player)
                UpdateInput();
           
            base.Update();
            if (rider != null)
            {
                rider.Position = position + GetPlayerOffset();
            }
        }

        protected virtual void UpdateInput()
        {
            
        }
        #endregion

    }
}
