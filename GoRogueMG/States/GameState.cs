using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Collections.Generic;

namespace GoRogueMG
{
    public class GameState : BaseState
    {
        Camera2D camera;
        private TileMap map;
        private Vector2 movement;
        KeyboardState keyboardState, oldKeyboardState;
        bool moving;
        List<TileType> BlockingTiles;

        public GameState( GameStateManager gsm, Camera2D camera )
            : base( gsm ) {
            this.camera = camera;

            map = LoadMap.Load( "http://localhost:3322/island/full" );
            keyboardState = Keyboard.GetState();
            oldKeyboardState = keyboardState;
            moving = false;

            BlockingTiles = new List<TileType>();
            BlockingTiles.Add( TileType.DeepWater );
            BlockingTiles.Add( TileType.ShallowWater );
        }

        public override void Update( GameTime gameTime ) {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            keyboardState = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            movement = BaseBuilder.PlayerPosition;

            if ( mouse.LeftButton == ButtonState.Pressed )
                System.Console.WriteLine( string.Format( "{0},{1}", mouse.X, mouse.Y ) );

            // camera
            if ( keyboardState.IsKeyDown( Keys.R ) )
                camera.ZoomIn( deltaSeconds );

            if ( keyboardState.IsKeyDown( Keys.F ) )
                camera.ZoomOut( deltaSeconds );

            if ( keyboardState.IsKeyDown( Keys.W ) && oldKeyboardState.IsKeyUp( Keys.W ) ) {
                movement.Y -= 32;
                if ( CheckCollision( movement ) )
                    moving = true;
            }

            if ( keyboardState.IsKeyDown( Keys.S ) && oldKeyboardState.IsKeyUp( Keys.S ) ) {
                movement.Y += 32;
                if ( CheckCollision( movement ) )
                    moving = true;
            }

            if ( keyboardState.IsKeyDown( Keys.A ) && oldKeyboardState.IsKeyUp( Keys.A ) ) {
                movement.X -= 32;
                if ( CheckCollision( movement ) )
                    moving = true;
            }

            if ( keyboardState.IsKeyDown( Keys.D ) && oldKeyboardState.IsKeyUp( Keys.D ) ) {
                movement.X += 32;
                if ( CheckCollision( movement ) )
                    moving = true;
            }


            if ( moving ) {
                BaseBuilder.PlayerPosition = movement;
                camera.LookAt( BaseBuilder.PlayerPosition );

                Sockets.SendMovement( (int)BaseBuilder.PlayerPosition.X / 32, (int)BaseBuilder.PlayerPosition.Y / 32 );
                moving = false;
            }
            oldKeyboardState = keyboardState;
        }

        public override void Draw( SpriteBatch sb, GameTime gameTime ) {
            foreach ( var tile in map.Tiles ) {
                if ( tile.X > BaseBuilder.camMin.X && tile.X < BaseBuilder.camMax.X && tile.Y > BaseBuilder.camMin.Y && tile.Y < BaseBuilder.camMax.Y )
                    sb.Draw( Assets.tileTextures[ tile.Type ], tile.Position, Color.White );
            }

            sb.Draw( Assets.player, BaseBuilder.PlayerPosition, Color.White );
        }

        public bool CheckCollision( Vector2 newPosition ) {
            if ( BlockingTiles.Contains( map.Tiles[ (int)newPosition.X / 32, (int)newPosition.Y / 32 ].Type ) )
                return false;

            return true;
        }
    }
}

