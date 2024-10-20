using System;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts
{
    public class MasterManager : MonoBehaviour
    {
        private ChapterController chapterController;
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

        public void StartDialogue(string p_id)
        {
            chapterController.StartChapter(p_id);
        }


        public void SubscribeChapterController(ChapterController p_controller) => chapterController = p_controller;

    }
}
