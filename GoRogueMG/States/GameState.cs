using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace GoRogueMG
{
    public class GameState : BaseState
    {
        Camera2D camera;
        private TileMap map;
        private Vector2 movement, camMin, camMax;

        public GameState( GameStateManager gsm, Camera2D camera )
            : base( gsm ) {
            this.camera = camera;

            map = LoadMap.Load( "http://localhost:3322/island/full" );
        }

        public override void Update( GameTime gameTime ) {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            movement = Vector2.Zero;

            // Camera bounds
            camMin = camera.ScreenToWorld( new Vector2( -64, -64 ) );
            camMax = camera.ScreenToWorld( new Vector2( 832, 512 ) );

            KeyboardState keyboardState = Keyboard.GetState();

            // camera
            if ( keyboardState.IsKeyDown( Keys.R ) )
                camera.ZoomIn( deltaSeconds );

            if ( keyboardState.IsKeyDown( Keys.F ) )
                camera.ZoomOut( deltaSeconds );

            if ( keyboardState.IsKeyDown( Keys.W ) )
                movement.Y -= 5;
            if ( keyboardState.IsKeyDown( Keys.S ) )
                movement.Y += 5;
            if ( keyboardState.IsKeyDown( Keys.A ) )
                movement.X -= 5;
            if ( keyboardState.IsKeyDown( Keys.D ) )
                movement.X += 5;

            camera.Position += movement;
        }

        public override void Draw( SpriteBatch sb, GameTime gameTime ) {
            foreach ( var tile in map.Tiles ) {
                if ( tile.X > camMin.X && tile.X < camMax.X && tile.Y > camMin.Y && tile.Y < camMax.Y )
                    sb.Draw( Assets.tileTextures[ tile.Type ], tile.Position, Color.White );
            }
        }
    }
}

