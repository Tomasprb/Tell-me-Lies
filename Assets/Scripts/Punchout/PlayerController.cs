using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Punchout{
    public class PlayerController : MonoBehaviour
    {
        /* ANIMATION STATES:
        * 0 = Idle
        * 1 = Punch
        * 2 = UpPunch
        * 3 = BigPunch
        * 4 = Dodge
        * 5 = Hit
        */
        [SerializeField]
        private EnemyController EnemyController;
        [SerializeField]
        private Image hpBar;
        [SerializeField]
        private GameObject explosion;
        public int PlayerState = 0;
        public int playerHP = 100;
        Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = this.gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(Input.GetKey(KeyCode.W) && PlayerState == 0){
                anim.SetInteger("PlayerState", 1);
                PlayerState = 1;
            }
            if(Input.GetKey(KeyCode.P) && PlayerState == 0){
                anim.SetInteger("PlayerState", 2);
                PlayerState = 2;
            }
            if(Input.GetKey(KeyCode.Space) && PlayerState == 0){
                anim.SetInteger("PlayerState", 3);
                PlayerState = 3;
            }
            if(Input.GetKey(KeyCode.S) && PlayerState == 0){
                anim.SetInteger("PlayerState", 4);
                PlayerState = 4;
            }
        }
        public void AlertObserver(string msg){
            if (msg.Equals("GoIdle"))
            {
                PlayerState = 0;
                anim.SetInteger("PlayerState", 0);
                // Do other things based on an attack ending.
            }
            if((msg.Equals("HitHook") || msg.Equals("HitStraight"))&& PlayerState!=4){
                UpdatePlayerHP(-34);
                anim.SetInteger("PlayerState", 5);
                PlayerState = 5;
            }
        }
        
        public void SendAlertObserverToEnemy(string msg){
            EnemyController.AlertObserverEnemy(msg);
        }

        public void UpdatePlayerHP(int hp){
            playerHP += hp;
            hpBar.fillAmount = (playerHP/100f);
            if(playerHP <= 0){
                Instantiate(explosion);
                this.gameObject.GetComponent<Renderer>().enabled = false;
                StartCoroutine(EndGame("lose"));
            }
        }

        public IEnumerator EndGame(string result){
            yield return new WaitForSeconds(1.5f);
            if(result == "lose"){
                // Lose, carry over variable to next scene
            } else{
                // Win, ""
            }
        }
    }

}

