using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Everest
{
    public class EverPlayerController : MonoBehaviour
    {
        [SerializeField]
        private Transform body;
        [SerializeField]
        private Transform pick;

        private Vector3 v3Pos;
        private float angle;
        private void FixedUpdate()
        {
            /** Get #Noped
            v3Pos = Input.mousePosition;
            //Ignore Z as is 2D
            v3Pos.z = 0;
            v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
            v3Pos = v3Pos - body.position;
            angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
            if (angle < 0.0f) angle += 360.0f;
            body.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            Vector3 vector = Camera.main.WorldToScreenPoint(orb.position);
            vector = Input.mousePosition - vector;
            float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

            pivot.position = orb.position;
            pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            
            **/

            //Clamp to body (body folows pickaxe)
            body.position = pick.position;
            //Rotate to mouse
            v3Pos = Input.mousePosition;
            v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
            v3Pos = v3Pos - pick.position;
            angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
            if (angle < 0.0f) angle += 360.0f;
            pick.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
