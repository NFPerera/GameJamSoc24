using UnityEngine;

namespace _Main.Scripts.Networking
{
    public class SpawnableObject : MonoBehaviour
    {
        [SerializeField] private int spawnObjectId;

        public int SpawnObjectId => spawnObjectId;
        public ulong MyOwnerId { get; private set; }
        
        public void SetOwnerIdClientRpc(ulong ownerId)
        {
            MyOwnerId = ownerId;
        }
    }
}