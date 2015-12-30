using System;

namespace GoRogueMG
{
    public class TileMap
    {
        int width;
        int height;
        Tile[,] tiles;

        public Tile[,] Tiles { get { return tiles; } }

        public TileMap( int width, int height, Tile[,] tiles ) {
            this.width = width;
            this.height = height;
            this.tiles = (Tile[,])tiles.Clone();
        }
    }
}

