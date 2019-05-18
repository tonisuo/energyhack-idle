using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
   public Transform runner;
   public Vector3 movementVector;

   void Awake(){
       runner = GetComponent<Transform>();
   }

    void Update(){
        Move(movementVector);
    }

    void Move(Vector3 movementVector){
        runner.position += movementVector * Time.deltaTime;
    }
}
