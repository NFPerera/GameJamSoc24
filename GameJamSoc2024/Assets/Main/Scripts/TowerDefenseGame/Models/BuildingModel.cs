using System;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Models
{
    public class BuildingModel : MonoBehaviour
    {
        [SerializeField] private Transform constructionPos;
        [SerializeField] private GameObject light;

        public bool IsAvailable { get; private set; }
        private void Awake()
        {
            IsAvailable = true;
            light.SetActive(false);
        }

        public void Construct() => IsAvailable = false;

        public Vector3 GetConstructionPos() => constructionPos.position;

        public void ToggleLight()
        {
            if(IsAvailable)
                light.SetActive(!light.activeSelf);
            else
                light.SetActive(false);
        }
    }
}