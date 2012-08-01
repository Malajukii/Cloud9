using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cloud9
{    
    public class Input : GameComponent
    {
        #region Singleton
        static Input instance;
        public static Input Instance
        {
            get
            {
                if (instance == null)
                    throw new NullReferenceException("Input has not yet been initialized");
                return instance;
            }
        }
        #endregion

        #region Initialization
        private Input(Game game)
            : base(game)
        {

        }

        public static void Initialize(Game game)
        {
            if (instance != null)
                throw new System.StackOverflowException("Input.Initialize can only be called once.");
            instance = new Input(game);
        }
        #endregion

        #region Properties
        KeyboardState cKstate, pKstate;
        MouseState cMstate, pMstate;
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            pKstate = cKstate;
            pMstate = cMstate;
            cKstate = Keyboard.GetState();
            cMstate = Mouse.GetState();

            base.Update(gameTime);
        }


        public bool KeyDown(Keys keys)
        {
            return cKstate.IsKeyDown(keys);
        }
        public bool KeyNewPressed(Keys keys)
        {
            return cKstate.IsKeyDown(keys) && pKstate.IsKeyUp(keys);
        }

        // will need more, but ill add it later
        #endregion
    }
}
