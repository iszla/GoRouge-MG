using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.Collisions;

namespace GoRogueMG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BaseBuilder : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameStateManager GSM;
        Sockets socket;

        public static Vector2 PlayerPosition;

        private FramesPerSecondCounter _frameCounter;
        private Camera2D camera;
        private ViewportAdapter viewport;

        public BaseBuilder() {
            graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";                
            graphics.IsFullScreen = false;
            IsMouseVisible = true;
            Window.Title = "GoRouge";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            _frameCounter = new FramesPerSecondCounter();

            GSM = new GameStateManager();
            socket = new Sockets();

            viewport = new BoxingViewportAdapter( graphics.GraphicsDevice, 800, 480 );
            camera = new Camera2D( viewport ) {
                MinimumZoom = 0.1f,
                MaximumZoom = 2.0f,
                Zoom = 1.0f,
                Origin = new Vector2( 400, 240 ),
                Position = new Vector2( 408, 270 )
            };

            Window.AllowUserResizing = true;

            GSM.PushState( new GameState( GSM, camera ) );

            while ( true ) {
                if ( socket.message == null ) {
                    continue;
                }
                else {
                    PlayerPosition = new Vector2( (int)socket.message[ "X" ] * 32, (int)socket.message[ "Y" ] * 32 );
                    camera.LookAt( PlayerPosition );

                    Sockets.token = (string)socket.message[ "Token" ];
                    break;
                }
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch( GraphicsDevice );

            Assets.LoadContent( this.Content, 0 ); 
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime ) {
            
            if ( Keyboard.GetState().IsKeyDown( Keys.Escape ) ) {
                Exit();
            }

            GSM.Update( gameTime );

            // TODO: Add your update logic here            
            base.Update( gameTime );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime ) {
            _frameCounter.Update( gameTime );

            graphics.GraphicsDevice.Clear( Color.Black );
            spriteBatch.Begin( transformMatrix: camera.GetViewMatrix() );

            GSM.Draw( spriteBatch, gameTime );

            spriteBatch.DrawString( Assets.font, string.Format( "FPS: {0} X: {1} Y: {2}", _frameCounter.CurrentFramesPerSecond, camera.Position.X / 32, camera.Position.Y / 32 ),
                                    new Vector2( camera.Position.X, camera.Position.Y ), Color.White );
            spriteBatch.End();
            base.Draw( gameTime );
        }
    }
}

