using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BulletBehaviour : MonoBehaviour
    {
        public float BulletSpeed=20f;
        public int damage = 40;
        public Rigidbody2D rb;
        // Start is called before the first frame update
        void Start()
        {
            rb.velocity=transform.right*BulletSpeed;
        }

        private void OnTriggerEnter2D(Collider2D HitInfo) 
        {
            EnemyScript enemy=HitInfo.GetComponent<EnemyScript>();
            if(enemy!=null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);   
        }
        private void FixedUpdate() 
        {
            
           if(!GetComponent<Renderer>().isVisible)
           {
               Destroy(gameObject);
           }
                    
        
        }
    }
