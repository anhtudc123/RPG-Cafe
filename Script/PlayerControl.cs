using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using static UnityEditor.PlayerSettings;

public class PlayerControl : MonoBehaviour
{
    public GameObject character;
    public Interact focus;
    Camera cam;
    public LayerMask movementMask;
    Animator anim;
    NavMeshAgent agent;
    public bool isSitting=false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray ray=cam.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                StandingUp();
                anim.SetBool("Walking", true);
                //Debug.Log("We hit" + hit.collider.name + " " + hit.point);
                agent.destination = hit.point;
                Interact interactable = hit.collider.GetComponent<Interact>();
                if (interactable != null)
                {
                   StandingUp();
                }
                if (interactable != null)
                {
                    anim.SetBool("Walking", true);
                    SetFocus(interactable);
                    agent.destination = hit.collider.gameObject.transform.position;
                }

            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                StandingUp();
                anim.SetBool("Walking", true);
                Debug.Log("We hit" + hit.collider.name + " " + hit.point);
                agent.destination = hit.point;
                RemoveFocus();
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (isSitting != true)
            {
                anim.SetBool("Walking", false);
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Interact interactable = hit.collider.GetComponent<Interact>();

                if (interactable != null)
                {
                    StandingUp();
                    anim.SetBool("Walking", true);
                    SetFocus(interactable);
                    agent.destination = hit.collider.gameObject.transform.position;
                }
            }
        }
    }
    public void StandingUp()
    {
        if (isSitting == true)
        {
            isSitting = false;
            anim.SetBool("Sitting", false);
            anim.SetTrigger("StandUp");
        }
    }
    public void SitDown()
    {
        anim.SetBool("Sitting",true);
        isSitting = true;
    }

    void SetFocus(Interact newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus.OnDefocused();
        focus = null;
    }
}
