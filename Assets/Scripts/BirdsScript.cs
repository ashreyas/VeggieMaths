using System.Collections;
using UnityEngine;

public class BirdsScript : MonoBehaviour {
    Vector3 startPosi;
    float endPosi;
    System.Random rnd = new System.Random();
    // Use this for initialization
    void Start()
    {
        startPosi = gameObject.transform.localPosition;
        endPosi = -startPosi.x;
        StartCoroutine(FlyBird());
    }

    // Update is called once per frame
    void Update () {
	}

    public IEnumerator FlyBird()
    {
        yield return new WaitForSeconds(0.03f);
        if(gameObject.transform.localPosition.x <= endPosi)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + 5, gameObject.transform.localPosition.y);
        }
        else
        {
            gameObject.transform.localPosition = startPosi - new Vector3(rnd.Next(0, 5), 0f, 0f);
        }

        StartCoroutine(FlyBird());
    }
}
