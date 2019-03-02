using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

class Odd1OutScript : MonoBehaviour
{
    public static Odd1OutScript instance;

    System.Random rnd = new System.Random();
    EdibleCreator edibleCreator = new EdibleCreator();

    Sprite correctAnsSpr, incorrectAnsSpr;
    Transform[] progressholder;

    int tasksWon = 0;
    int progress = 1;
    List<IEdible> foodObjects = new List<IEdible>();
    Transform[] Foodspawner;
    string oddItem = "";
    void Start()
    {
        ShopManager sm = new ShopManager();
        sm.SetGifts();
        Foodspawner = GameObject.Find("Foodholder").transform.GetComponentsInChildren<Transform>();

        correctAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Confirm");
        incorrectAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Close");
        progressholder = GameObject.Find("Progressholder_Progresses").transform.GetComponentsInChildren<Transform>();

        instance = this;
        
        StartCoroutine(StartOdd1OutScene(0));
    }



    IEnumerator StartOdd1OutScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        foodObjects.Clear();
        GameObject[] previousObjects = GameObject.FindGameObjectsWithTag("Edible");
        List<GameObject> previousObjectsParent = new List<GameObject>();
        if (previousObjects.Length > 0)
            for (int i = 0; i < previousObjects.Length; i++)
            {
                previousObjectsParent.Add(previousObjects[i].transform.parent.gameObject);
            }

        for (int i = 0; i < previousObjectsParent.Count; i++)
        {
            Destroy(previousObjectsParent[i]);
        }

        //generating random item as exceptional item
        int t = rnd.Next(0, 2);
        IEdible oddEdible;
        
        if (t > 0)
        {
            oddEdible = edibleCreator.GetRandomFruit(true);
            oddEdible.EdObject.tag = "Fruits";
            oddItem = "Fruits";
        }
        else
        {
            oddEdible = edibleCreator.GetRandomVeggie(true);
            oddEdible.EdObject.tag = "Vegetables";
            oddItem = "Vegetables";
        }
        foodObjects.Add(oddEdible);


        // fill the random extra objects from the other food category
        int evenItemCount = 0;
        evenItemCount = Foodspawner.Length - 1 - foodObjects.Count;
        for (int i = 0; i < evenItemCount; i++)
        {
            IEdible evenEdible;
            if (oddItem == "Vegetables")
            {
                evenEdible = edibleCreator.GetRandomFruit(true);
                evenEdible.EdObject.tag = "Fruits";
            }
            else
            {
                evenEdible = edibleCreator.GetRandomVeggie(true);
                evenEdible.EdObject.tag = "Vegetables";
            }
            foodObjects.Add(evenEdible);
        }

        foodObjects = foodObjects.OrderBy(a => rnd.Next()).ToList();




        // Set edible positions
        for (int i = 0; i < foodObjects.Count; i++)
        {
            foodObjects[i].EdObject.GetComponent<RectTransform>().sizeDelta = new Vector2(145f, 145f);
            foodObjects[i].EdObject.transform.SetParent(GameObject.Find("Foodholder").transform, false);
            foodObjects[i].EdObject.transform.position = new Vector3(Foodspawner[i + 1].position.x, Foodspawner[i + 1].position.y, 1);

            GameObject fakeChild = new GameObject();
            fakeChild.tag = "Edible";
            fakeChild.transform.SetParent(foodObjects[i].EdObject.transform);
        }
    }
    public void DetectSelection(GameObject selected)
    {
        if (progress < 12)
        {
            if (selected.tag == oddItem)
            {
                //GameObject.Find("TrueAnswer").GetComponent<Text>().text = "Success!";
                SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.CorrectAns);
                progressholder[progress].GetComponent<Image>().color = Color.green;
                progressholder[progress + 1].GetComponent<Image>().sprite = correctAnsSpr;
                tasksWon++;
            }
            else
            {
                //GameObject.Find("TrueAnswer").GetComponent<Text>().text = "Fail!";
                SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.IncorrectAns);
                progressholder[progress].GetComponent<Image>().color = Color.red;
                progressholder[progress + 1].GetComponent<Image>().sprite = incorrectAnsSpr;
            }
            progressholder[progress + 1].GetComponent<Image>().color = Color.white;
            progress += 2;
            if (progress < 12)
            {
                StartCoroutine(StartOdd1OutScene(0.3f));
            }

            else
            {
                PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney", 0) + tasksWon);
                SceneController.instance.LoadSceneGameList();
            }
        }
    }
}