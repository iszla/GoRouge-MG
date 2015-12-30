using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoRogueMG
{
    public class GameStateManager
    {
        Stack<BaseState> states;

        public GameStateManager() {
            this.states = new Stack<BaseState>();
        }

        public void SetState( BaseState state ) {
            if ( this.states.Count < 0 )
                this.states.Clear();
            this.states.Push( state );
        }

        public void PushState( BaseState state ) {
            this.states.Push( state );
        }

        public void PopState( BaseState state ) {
            this.states.Pop();
        }

        public void Update( GameTime gameTime ) {
            this.states.Peek().Update( gameTime );
        }

        public void Draw( SpriteBatch sb, GameTime gameTime ) {
            this.states.Peek().Draw( sb, gameTime );
        }
    }
}

