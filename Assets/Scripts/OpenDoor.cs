using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;
    public bool pressed;

    void Start()
    {
        pressed = false;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && pressed == false)
        {
            pressed = true;
            animator.SetTrigger("Open");
        }
    }
}
