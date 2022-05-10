using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnE : MonoBehaviour
{
    public Animator dotfuighnb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            StartCoroutine(remove());
            
        }
    }

    public IEnumerator remove()
    {
        dotfuighnb.SetBool("disappear", true);
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }

}
