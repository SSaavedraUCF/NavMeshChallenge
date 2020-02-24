using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropScript : MonoBehaviour
{
    public GameObject item;
    public GameObject placeArea;
    public Text interact;
    public bool touching = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(PlayerController.onHand && touching && Input.GetKeyDown(KeyCode.E))
        {
            GetDown();
        } 
    }

     void OnCollisionEnter(Collision nest)
    {
        GameObject otherObj = nest.gameObject;
        Debug.Log("Collided with: " + otherObj);
        if (nest.gameObject.tag == "Player")
        {
            interact.text = "Press E to feed your young";
            touching = true;
        }

    }

    void OnCollisionExit(Collision nest)
    {
        if (nest.gameObject.tag == "Player")
        {
            interact.text = "";
            touching = false;
        }
    }


    void GetDown()
    {

        item.transform.parent = null;
        item.transform.position = new Vector3(placeArea.transform.position.x, placeArea.transform.position.y, placeArea.transform.position.z);
        item.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        PlayerController.onHand = true;
        interact.gameObject.SetActive(false);
        PlayerController.onHand = false;
    }
}
