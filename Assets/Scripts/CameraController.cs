using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    [SerializeField] private float distanceRay = 10f;
    [SerializeField] private float velocidad = 3.0f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        RaycastCannon();
    }

    private void LookAtPlayer()
    {
        Vector3 playerDirection = GetPlayerDirection();

        Quaternion newRotation = Quaternion.LookRotation(new Vector3(playerDirection.x, playerDirection.y, playerDirection.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, velocidad / 2 * Time.deltaTime);
    }

    private Vector3 GetPlayerDirection()
    {
        return player.transform.position - transform.position;
    }



    private void RaycastCannon()
    {
        
        RaycastHit hit;


        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        if (Physics.Raycast(startPoint, gameObject.transform.TransformDirection(Vector3.forward), out hit, distanceRay))
        {
            if (hit.transform.tag == "Player")
            {
                gameObject.GetComponentsInChildren<Light>()[0].color = Color.red;
                gameObject.GetComponentsInChildren<Light>()[0].spotAngle = 30;
                gameObject.GetComponentsInChildren<Light>()[0].intensity = 5;
            }
        }
        else
        {
            gameObject.GetComponentsInChildren<Light>()[0].color = Color.white;
            gameObject.GetComponentsInChildren<Light>()[0].spotAngle = 60;
            gameObject.GetComponentsInChildren<Light>()[0].intensity = 2;
        }


    }

    private void OnDrawGizmos()
    {

            Gizmos.color = Color.blue;
            Vector3 direction = gameObject.transform.TransformDirection(Vector3.forward) * distanceRay;
            Gizmos.DrawRay(gameObject.transform.position, direction);


    }

}
