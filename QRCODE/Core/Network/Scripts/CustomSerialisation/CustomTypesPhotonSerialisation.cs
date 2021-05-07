#if PHOTON_UNITY_NETWORKING

using ExitGames.Client.Photon;
using UnityEngine;

namespace QRCode.Network
{
    public static class CustomTypesPhotonSerialisation 
    {
        //---<CORE>----------------------------------------------------------------------------------------------------<
        
        /// <summary>
        /// Add here all type to serialize for Photon.
        /// </summary>
        public static void Register()
        {
            PhotonPeer.RegisterType(typeof(Color), (byte) 'C',SerializeColor, DeserializeColor);
        }

        //---<COLOR SERIALIZATION & DESERIALIZATION>-------------------------------------------------------------------<
        public static readonly byte[] memColor = new byte[4 * 5];
        public static short SerializeColor(StreamBuffer outStreamBuffer, object color)
        {
            var c = (Color) color;
            lock (memColor)
            {
                byte[] bytes = memColor;
                int index = 0;
                Protocol.Serialize(c.r, bytes, ref index);
                Protocol.Serialize(c.g, bytes, ref index);
                Protocol.Serialize(c.b, bytes, ref index);
                Protocol.Serialize(c.a, bytes, ref index);
                outStreamBuffer.Write(bytes, 0, 4 * 5);
            }
            return 4 * 5; 
        }

        public static object DeserializeColor(StreamBuffer inStreamBuffer, short length)
        {
            var color = new Color();
            lock (memColor)
            {
                inStreamBuffer.Read(memColor, 0, 4 * 5);
                int index = 0;
                Protocol.Deserialize(out color.r, memColor, ref index);
                Protocol.Deserialize(out color.g, memColor, ref index);
                Protocol.Deserialize(out color.b, memColor, ref index);
                Protocol.Deserialize(out color.a, memColor, ref index);
            }

            return color;
        }
    }
}
#endif