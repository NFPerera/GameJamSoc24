using System;
using System.Collections;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Models
{
    public class BuildingModel : MonoBehaviour
    {
        [SerializeField] private Transform constructionPos;
        [SerializeField] private GameObject light;
        [SerializeField] private Transform body; 

        public bool IsAvailable { get; private set; }
        private void Awake()
        {
            IsAvailable = true;
            light.SetActive(false);
        }

        public void Construct()
        {
            IsAvailable = false;
            print("Building constructed");
            if(body != null){
            ShrinkBody(); // Call the shrink method when IsAvailable changes to false
            }
        }

        public Vector3 GetConstructionPos() => constructionPos.position;

        public void ToggleLight()
        {
            if (IsAvailable)
                light.SetActive(!light.activeSelf);
            else
                light.SetActive(false);
        }

        private void ShrinkBody()
        {
            StartCoroutine(ShrinkCoroutine());
        }

        private IEnumerator ShrinkCoroutine()
        {
            Vector3 originalScale = body.localScale;
            Vector3 targetScale = Vector3.zero;
            float duration = 0.5f; // Duration of the shrink animation
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                body.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            //body.localScale = targetScale;
            body.gameObject.SetActive(false);
            body.localScale = originalScale;
        }
    }
}