using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character;
    [SerializeField]
    private float playerSpeed = 5.0f;
    private float gravity = 9.81f;
    [SerializeField]
    private GameObject particleEffectPrefab;
    [SerializeField]
    private GameObject hitMarketParticlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //raycast from centre of main camera
       
        if (Input.GetMouseButton(0))
        {
            particleEffectPrefab.SetActive(true);

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit,Mathf.Infinity))
            {
                Debug.Log("Raycast got hit"+hit.transform.name);

                Instantiate(hitMarketParticlePrefab,hit.point,Quaternion.LookRotation(hit.normal));

            }
        }
        else
        {
            particleEffectPrefab.SetActive(false);
        }
    }
    private void Movement()
    {

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * playerSpeed;
        velocity.y -= gravity;
        velocity = transform.transform.TransformDirection(velocity);//it converts local space to world space direction


        character.Move(direction * Time.deltaTime * playerSpeed);

    }
}
