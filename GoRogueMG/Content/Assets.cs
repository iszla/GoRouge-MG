using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Maps.Tiled;
using System.Collections.Generic;

namespace GoRogueMG
{
    public class Assets
    {
        //public static Texture2D example;
        public static SpriteFont font;
        public static TiledMap tileMap;

        // Tile textures
        public static Dictionary<TileType, Texture2D> tileTextures;
        public static Texture2D DeepWater;
        public static Texture2D ShallowWater;
        public static Texture2D Beach;
        public static Texture2D Grass;

        public static void LoadContent( ContentManager content, int level = 0 ) {
            tileTextures = new Dictionary<TileType, Texture2D>();

            switch ( level ) {
            case 0:
                font = content.Load<SpriteFont>( "Fonts/HackFont" );
                DeepWater = content.Load<Texture2D>( "Tiles/DeepWater" );
                ShallowWater = content.Load<Texture2D>( "Tiles/ShallowWater" );
                Beach = content.Load<Texture2D>( "Tiles/Beach" );
                Grass = content.Load<Texture2D>( "Tiles/Grass" );
                break;
            default:
                break;
            }

            tileTextures.Add( TileType.DeepWater, DeepWater );
            tileTextures.Add( TileType.ShallowWater, ShallowWater );
            tileTextures.Add( TileType.Beach, Beach );
            tileTextures.Add( TileType.Grass, Grass );
        }
    }
}

