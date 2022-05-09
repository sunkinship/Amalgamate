using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadAudioOffScreen : MonoBehaviour
{
    public GameObject audioCanvas;


    private void Start()
    {
        //audioCanvas.SetActive(true);
        //audioCanvas.SetActive(false);
        StartCoroutine(loadAudio());
    }

    public IEnumerator loadAudio()
    {
        audioCanvas.GetComponent<Canvas>().enabled = false;
        audioCanvas.transform.position = new Vector3(audioCanvas.transform.position.x, audioCanvas.transform.position.y + 100000, audioCanvas.transform.position.z);
        audioCanvas.SetActive(true);
        audioCanvas.transform.position = new Vector3(audioCanvas.transform.position.x, audioCanvas.transform.position.y - 100000, audioCanvas.transform.position.z);
        yield return new WaitForSeconds(.1f);
        audioCanvas.SetActive(false);
        audioCanvas.GetComponent<Canvas>().enabled = true;
        yield return null;
    }

}
