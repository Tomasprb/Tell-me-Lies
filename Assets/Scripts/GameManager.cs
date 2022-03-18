using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public SceneChanger scIntance;

        private void Awake()
        {
            //Singleton pattern stuff, dont toucha my spagett
            if (instance != null)
            {
                Debug.LogWarning("FIGHT ME (ง'̀-'́)ง: Two copies of the Game Manager where detected. Stop messing around guys! ಥ_ಥ");
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            //Scene Changer primer
            scIntance = this.gameObject.GetComponent<SceneChanger>();
            if (scIntance == null)
            {
                scIntance = this.gameObject.AddComponent<SceneChanger>();
            }
        }

        void Update()
        {

        }

        #region Managment
        //Level Selector Managment
        private int _levelCounter = 0;
        [SerializeField]
        private int _levelOffset = 2;
        [SerializeField]
        private int _barLevel = 1;

        public void GoToNextLevel()
        {
            scIntance.ChangeScene(_levelOffset + _levelCounter);
        }

        public void GoToBar()
        {
            scIntance.ChangeScene(_barLevel);
        }

        public void LevelComplete()
        {
            _levelCounter++;
        }
        #endregion
    }
}
