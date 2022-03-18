using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

namespace Game.Punchout{
    public class EnemyController : MonoBehaviour
    {        
        /* ANIMATION STATES:
        * 1 = PunchStraight
        * 2 = PunchHook
        * 3 = Die
        * 4 = HitHigh
        * 5 = HitLow
        * 6 = BlockHigh 
        * 7 = BlockLow
        */
        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private Image hpBar;
        public int EnemyState = 0;
        public int EnemyHP = 100;
        public bool isStunned = false;
        [SerializeField]
        bool waiting = false;
        
        int stunnedHits = 0;
        Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = this.gameObject.GetComponent<Animator>();
                float waitTime = UnityEngine.Random.Range(2f, 5f);
                waiting = true;
                StartCoroutine(WaitSecondsAndPunch(waitTime));
        }

        // Update is called once per frame
        void FixedUpdate()
        {
        }

        private IEnumerator WaitSecondsAndPunch(float secs){
            yield return new WaitForSeconds(secs);
            if(EnemyState==0 && !isStunned){
                int punchType = UnityEngine.Random.Range(0,100);
                if(punchType <= 50){
                    anim.SetInteger("EnemyState", 1);
                    EnemyState = 1;
                waiting = false;
                } else {
                    anim.SetInteger("EnemyState", 2);
                    EnemyState = 2;
                waiting = false;
                }
            }
        }

        public void SendAlertObserverToPlayer(string msg){
            playerController.AlertObserver(msg);
        }

        public void AlertObserverEnemy(string msg){
            if(EnemyState!=3){
                if(isStunned && msg.Contains("Hit")){
                    EnemyState=0;
                    anim.SetInteger("EnemyState", 0);
                }
                if(msg.Equals("HitLow") && EnemyState == 1){
                    Debug.Log("StartStun");
                    StopCoroutine("WaitSecondsAndPunch");
                    waiting = false;
                    Debug.Log("HitSuccess");
                    EnemyState=5;
                    anim.SetInteger("EnemyState", 5);
                    anim.SetBool("IsStunned", true);
                    isStunned = true;
                    UpdateEnemyHP(-10);
                }
                if(msg.Equals("HitLow") && EnemyState == 0 && !isStunned){
                    anim.SetInteger("EnemyState", 7);
                    playerController.UpdatePlayerHP(-5);
                }
                if((msg.Equals("HitHigh") || msg.Equals("HitBig")) && EnemyState == 0 && !isStunned){
                    anim.SetInteger("EnemyState", 6);
                    playerController.UpdatePlayerHP(-5);
                }
                if((msg.Equals("HitLow") || msg.Equals("HitHigh")) && isStunned && EnemyState == 0 && stunnedHits <= 4){
                    Debug.Log("HitStun");
                    if(msg.Equals("HitLow")){
                        anim.SetInteger("EnemyState", 5);
                        EnemyState = 5;
                        stunnedHits++;
                        UpdateEnemyHP(-2);
                    } else
                    if(msg.Equals("HitHigh")){
                        anim.SetInteger("EnemyState", 4);
                        EnemyState = 4;
                        stunnedHits++;
                        UpdateEnemyHP(-2);
                    }
                }
                if(((msg.Equals("HitLow") || msg.Equals("HitHigh")) && isStunned && stunnedHits == 5) || msg.Equals("HitBig") && isStunned){
                    Debug.Log("EndStun");
                    stunnedHits = 0;
                    EnemyState = 4;
                    isStunned = false;
                    anim.SetBool("IsStunned", false);
                    anim.SetInteger("EnemyState", 4);
                    UpdateEnemyHP(-7);
                }
            
                if (msg.Equals("GoIdle"))
                {
                    StopCoroutine("WaitSecondsAndPunch");
                    if(!isStunned){
                        float waitTime = UnityEngine.Random.Range(3f, 7f);
                        waiting = true;
                        StartCoroutine(WaitSecondsAndPunch(waitTime));
                    }
                    EnemyState = 0;
                    anim.SetInteger("EnemyState", 0);
                    UpdateEnemyHP(0);
                    // Do other things based on an attack ending.
                }
            }
        }
        
        public void UpdateEnemyHP(int hp){
            EnemyHP += hp;
            hpBar.fillAmount = EnemyHP/100f;
            if(EnemyHP<=0){
                EnemyState=3;
                StopCoroutine("WaitSecondsAndPunch");
                waiting=false;
                anim.SetInteger("EnemyState", 3);
                playerController.EndGame("win");
            }
        }
    }
}