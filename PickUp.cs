using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public GameObject item; //the muffin
    public GameObject holding; //where the object goes
    public GameObject player; //player gameobject
    public GameObject trigger; //collider trigger

    public GameObject nest;// the actual nest
    public GameObject place;//where the muffin will be placed
    public static bool placed=false;

    public bool touchable = false;
    public Text pickup,drop,objective;
    private float nestDistance,foodDistance;

    // Start is called before the first frame update
    void Start()
    {

        pickup.text = "";
        drop.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        DistanceChecker();

        if (touchable && Input.GetKeyDown(KeyCode.E))
        {
            PickUpper();
        }

        if (nestDistance<4.4f && PlayerController.onHand && Input.GetKeyDown(KeyCode.E))
        {
            Dropper();
        }

        if (foodDistance < 2f)
        {

            pickup.text = "Press E to get Fish MacMuffin";
            touchable = true;
        }
        else if (foodDistance > 2f)
        {
            pickup.text = "";
            touchable = false;
        }

        if (PlayerController.onHand)
        {
            objective.text = "Return to your nest! Feed your babies!";
        }

        if (nestDistance < 4.4f && PlayerController.onHand)
        {

            drop.text = "Press E to feed the babies";
            
        }
        else if (nestDistance > 4.4f)
        {
            drop.text = "";
            
        }

    }


    void PickUpper()
    {
        trigger.GetComponent<Collider>().enabled = false;
        item.transform.parent = player.transform;
        item.transform.position = new Vector3(holding.transform.position.x, holding.transform.position.y, holding.transform.position.z);
        item.transform.localRotation = Quaternion.Euler(0f, 145f, 14.6f);
        PlayerController.onHand = true;
        pickup.gameObject.SetActive(false);
    }

    void Dropper()
    {
        item.transform.parent = null;
        item.transform.localPosition = new Vector3(place.transform.position.x, place.transform.position.y, place.transform.position.z);
        item.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        PlayerController.onHand = false;
        drop.gameObject.SetActive(false);
        placed = true;
        objective.text = "";
    }

    void DistanceChecker()
    {
        nestDistance = Vector3.Distance(player.transform.position, nest.transform.position);
        foodDistance = Vector3.Distance(player.transform.position, item.transform.position);

    }

    

    }
