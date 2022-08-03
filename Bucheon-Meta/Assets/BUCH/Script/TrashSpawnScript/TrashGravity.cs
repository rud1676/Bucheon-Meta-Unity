using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGravity : MonoBehaviour
{
    private Rigidbody r;
    [SerializeField] private ParticleSystem starEffect;
    private bool isTriggered;


    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "call_trash")
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnEnable()
    {
        if (gameObject.tag != "paper_trash")
        {
            r = GetComponent<Rigidbody>();
            isTriggered = false;
        }
        else
        {
            isTriggered = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered == false)
        {
            r.constraints = RigidbodyConstraints.FreezePosition;
            switch (gameObject.name)
            {
                case "trash_cup002_small(Clone)":
                case "trash_snack_002_small(Clone)":
                case "trash_food container_003_small(Clone)":
                case "trash_sofa_big(Clone)":
                case "trash_food container_001_small(Clone)":
                case "trash_table_big(Clone)":
                case "trash_washer_big(Clone)":
                case "trash_chair_big(Clone)":
                    transform.Rotate(new Vector3(-90, 0, 0));
                    break;
                case "trash_snack_001_small(Clone)":
                case "trash_glassbottle 1_small(Clone)":
                    transform.Rotate(new Vector3(90, 0, 0));
                    break;
                case "trash_cracked_glassbottle_002_small(Clone)":
                case "trash_cracked_glassbottle_001_small(Clone)":
                    transform.Rotate(new Vector3(0, 0, 90));
                    break;
            }
            if (gameObject.tag == "call_trash")
            {

            }
            isTriggered = true;
        }
    }
    public void PlayParticle()
    {
        starEffect.transform.parent = null;
        starEffect.transform.localScale = new Vector3(1, 1, 1);
        starEffect.Play();
        Destroy(starEffect.gameObject, starEffect.duration);
        Destroy(gameObject);
        BoardManager.Instance.showTrashPopUP(gameObject.tag);
    }
}
