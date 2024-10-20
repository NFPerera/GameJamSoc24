using System;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Models
{
    public class BuildingModel : MonoBehaviour
    {
        [SerializeField] private Transform constructionPos;
        [SerializeField] private GameObject light;

        private void Awake()
        {
            light.SetActive(false);
        }

        public Vector3 GetConstructionPos() => constructionPos.position;

        public void ToggleLight() => light.SetActive(!light.activeSelf);
    }
}