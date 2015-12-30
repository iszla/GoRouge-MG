using LitJson;
using System.Net;
using System.IO;
using Microsoft.Xna.Framework;

namespace GoRogueMG
{
    public class LoadMap
    {
        public static TileMap Load( string mapUrl ) {
            string url = mapUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create( url );
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if ( response.StatusCode == HttpStatusCode.OK ) {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader( stream );

                string json = reader.ReadToEnd();
                JsonData mapJson = JsonMapper.ToObject( json );

                response.Close();
                stream.Close();

                Tile[,] tiles = new Tile[(int)mapJson[ "width" ], (int)mapJson[ "height" ]];
                int lx, ly, tileType;

                for ( int i = 0; i < mapJson[ "layers" ][ 0 ][ "data" ].Count; i++ ) {
                    lx = i % (int)mapJson[ "width" ];
                    ly = i / (int)mapJson[ "width" ];

                    tileType = (int)mapJson[ "layers" ][ 0 ][ "data" ][ i ];
                    tiles[ lx, ly ] = new Tile( (TileType)tileType, new Vector2( lx * 32, ly * 32 ) );
                }

                return new TileMap( (int)mapJson[ "width" ], (int)mapJson[ "height" ], tiles );
            }
            else {
                System.Console.WriteLine( "Unable to load map. Error code " + response.StatusCode );
                return null;
            }
        }
    }
}

