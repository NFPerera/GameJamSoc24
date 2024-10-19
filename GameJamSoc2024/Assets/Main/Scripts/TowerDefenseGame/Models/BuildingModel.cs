using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Models
{
    public class BuildingModel : MonoBehaviour
    {
        [SerializeField] private Transform constructionPos;


        public Vector3 GetConstructionPos() => constructionPos.position;
    }
}