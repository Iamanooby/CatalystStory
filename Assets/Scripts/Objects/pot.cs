using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    private Animator anim;
    public BoolValue broken;

    // Start is called before the first frame update
    void Start()
    {
        if (broken.initialValue)
        {
            this.gameObject.SetActive(false);
            return;
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
        broken.initialValue = true;
    }
}
