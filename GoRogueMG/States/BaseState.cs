using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoRogueMG
{
    public abstract class BaseState
    {
        private GameStateManager gsm;

        public BaseState( GameStateManager gsm ) {
            this.gsm = gsm;
        }

        public abstract void Update( GameTime gameTime );

        public abstract void Draw( SpriteBatch sb, GameTime gameTime );
    }
}

