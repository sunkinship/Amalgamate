using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnE : MonoBehaviour
{
    public Animator dotfuighnb;
    private static bool alreadyDone;

    // Start is called before the first frame update
    void Start()
    {
        //alreadyDone = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(alreadyDone == true)
        {
            Destroy(this.gameObject);
        }

        if(Input.GetKey(KeyCode.E))
        {
            
            StartCoroutine(remove());
            
        }
    }

    public IEnumerator remove()
    {
        dotfuighnb.SetBool("disappear", true);
        yield return new WaitForSeconds(1.2f);
        alreadyDone = true;
        Destroy(this.gameObject);
         
    }

}
