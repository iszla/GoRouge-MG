using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GoRogueMG
{
    public enum TileType
    {
        DeepWater,
        ShallowWater,
        Beach,
        Grass
    }

    public class Tile
    {
        TileType type;
        Vector2 pos;

        public float X { get { return pos.X; } set { pos.X = value; } }

        public float Y { get { return pos.Y; } set { pos.Y = value; } }

        public Vector2 Position { get { return pos; } set { pos = value; } }

        public TileType Type { get { return type; } set { type = value; } }

        public Tile( TileType type ) {
            this.type = type;
            this.pos = Vector2.Zero;
        }

        public Tile( TileType type, Vector2 position ) {
            this.type = type;
            this.pos = position;
        }
    }
}

