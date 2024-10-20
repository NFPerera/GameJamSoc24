using System;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts
{
    public class MasterManager : MonoBehaviour
    {
        [SerializeField] private ChapterController chapterController;
        public static MasterManager Instance => m_instance;
        private static MasterManager m_instance;

        
        private void Awake()
        {
            if (m_instance != null && m_instance != this)
            {
                Destroy(this);
                return;
            }

            m_instance = this;
            
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                StartDialogue("CapturaDeStacy");
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                StartDialogue("InterrogacionDeStacy");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartDialogue("PaseoPorTerrenoCalcinado");
            }
        }

        public void StartDialogue(string p_id)
        {
            chapterController.StartChapter(p_id);
        }
            
    }
}
