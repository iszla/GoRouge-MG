using System;
using LitJson;
using WebSocketSharp;

namespace GoRogueMG
{
    public class Sockets
    {
        static WebSocket sock;
        public JsonData message;
        public static string token;

        public Sockets() {
            sock = new WebSocket( "ws://localhost:3322/ws" );
            sock.OnMessage += (sender, e ) =>
                message = JsonMapper.ToObject( e.Data );

            sock.OnOpen += (sender, e ) =>
                SendMessage( "new_player Snook" );

            sock.Connect();
        }

        public static void SendMessage( string message ) {
            sock.Send( message );
        }

        public static void SendMovement( int x, int y ) {
            SendMessage( string.Format( "move {0} {1} {2}", x, y, token ) );
        }
    }
}

