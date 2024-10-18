using System.Collections.Generic;
using _Main.Scripts.Networking;

namespace _Main.Scripts.DevelopmentUtilities
{
    public class DictionarySerializer
    {
        public List<ulong> keys = new List<ulong>();
        public List<MasterManager.RoomData> values = new List<MasterManager.RoomData>();
    }
}