using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    void Start()
    {
        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        bool left = anim.GetBool("isLeft");
        bool right = anim.GetBool("isRight");
        bool up = anim.GetBool("isUp");

        if ((left || right || up) && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!left && !right && !up)
        {
            audioSource.Stop();
        }
    }

    public void Animation(int index)
    {
        switch (index)
        {
            case 0:
                anim.SetBool("isLeft", true);
                break;
            case 1:
                anim.SetBool("isUp", true);
                break;
            case 2:
                anim.SetBool("isRight", true);
                break;
            case 3:
                anim.SetBool("isLeft", false);
                break;
            case 4:
                anim.SetBool("isUp", false);
                break;
            case 5:
                anim.SetBool("isRight", false);
                break;
        }
    }
}
