using UnityEngine;

namespace _Main.Scripts.DevelopmentUtilities
{
    public class Finder : MonoBehaviour
    {
        
            
        public static GameObject FindChildGameObjectByName(GameObject p_topParentGameObject, string p_gameObjectName)
        {
            for (int i = 0; i < p_topParentGameObject.transform.childCount; i++)
            {
                if (p_topParentGameObject.transform.GetChild(i).name.ToLower() == p_gameObjectName.ToLower())
                {
                    return p_topParentGameObject.transform.GetChild(i).gameObject;
                }

                GameObject tmp = FindChildGameObjectByName(p_topParentGameObject.transform.GetChild(i).gameObject,
                    p_gameObjectName);

                if (tmp != null)
                {
                    return tmp;
                }

            }
            
            return null;
        }
    }
}