using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ScriptForTesting : MonoBehaviour
{
    public Transform pivot; // the pivot point around which to rotate the door
    public float rotationAngle = 90f; // the angle to rotate the door
    public float rotationTime = 1f; // the time it takes to rotate the door

    private Quaternion baserotation;

    public GameObject MountItem;
    public GameObject Mount;
    public float Timer = 0f;
    public float MaxTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        baserotation = transform.rotation;

        // StartCoroutine(OpenDoor());
    }

    void Update()
    {
        Spawn();
    }
    // coroutine to rotate the door over time
    IEnumerator OpenDoor()
    {

        float t = 0f;
        while (t < rotationTime)
        {
            t += Time.deltaTime;
            transform.RotateAround(pivot.position, Vector3.up, rotationAngle * Time.deltaTime / rotationTime);
            yield return null;
        }
    }
    void Spawn()
    {
        //  Timer += Time.deltaTime;
        /*  if (Timer >= MaxTime)
          {
              Timer = 0f;*/
        if (Mount == null)
        {

            Mount = Instantiate(MountItem, transform.position, transform.rotation);
            Mount.transform.parent = transform;
        }

        // }
    }


    /*void Rotate()
    {
        // Quaternion currentRotation = transform.rotation;

        // transform.rotation = Quaternion.RotateTowards(currentRotation, playerTarget.rotation, 0.5f);
        // transform.rotation = Quaternion.Slerp(transform.rotation, playerTarget.rotation, 0.5f);

        Vector3 targetDirection = playerTarget.position - transform.position;
        // transform.rotation = Quaternion.LookRotation(playerTarget.position);

        // Vector3 eulerangle = Quaternion.EulerAngles(playerTarget.rotation);
        transform.rotation = Quaternion.Euler(playerTarget.position);
        //  transform.LookAt(playerTarget.position);
        //   transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }*/

    /*  void LookAtPlayer()
      {
          // Get the direction from the enemy to the player
          Vector3 direction = playerTarget.position - transform.position;

          // Calculate the rotation angle based on the player's rotation
          float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
          angle += playerTarget.rotation.y * Mathf.Rad2Deg + 180f;

          // Create a rotation that looks at the player with an offset angle
          Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);

          // Set the enemy's rotation to the target rotation
          transform.rotation = targetRotation;
      }*/

    /*   private void OnCollisionEnter(Collision target)
       {
           if (target.gameObject.CompareTag("Player"))
           {
               //  Vector3 direction = target.gameObject.GetComponent<playermovement>()._moveVector;
               print("player hit");
               rb.AddForce(Vector3.back * 100f, ForceMode.Impulse);
           }
       }*/
}
