using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Collections;
using System.Linq;

class RecipesSceneScript : MonoBehaviour
{
    public static RecipesSceneScript instance;
    System.Random rnd = new System.Random();
    public List<string> answers = new List<string>();
    EdibleCreator edibleCreator = new EdibleCreator();

    Sprite correctAnsSpr, incorrectAnsSpr;
    Transform[] progressholder;

    int tasksWon = 0;
    int answersCount, progress = 1, answeredCount = 0;
    ImportRecipe loadedData;
    List<IEdible> foodObjects = new List<IEdible>();
    Transform[] Foodspawner;

    void Start()
    {
        ShopManager sm = new ShopManager();
        sm.SetGifts();
        Foodspawner = GameObject.Find("Foodholder").transform.GetComponentsInChildren<Transform>();

        correctAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Confirm");
        incorrectAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Close");
        progressholder = GameObject.Find("Progressholder_Progresses").transform.GetComponentsInChildren<Transform>();

        instance = this;
        StartCoroutine(ImportDataFrom("Recipes.json"));
    }

    IEnumerator ImportDataFrom(string fileName)
    {
        string filePath;
        filePath = Path.Combine(Application.streamingAssetsPath + "/", fileName);
        string dataAsJson = "";
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
                Debug.LogError(System.Environment.NewLine + www.error);
            else
                dataAsJson = www.downloadHandler.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(filePath);
        }
        loadedData = JsonUtility.FromJson<ImportRecipe>(dataAsJson);
        StartCoroutine(StartRecipeScene(loadedData, 0));
    }


    IEnumerator StartRecipeScene(ImportRecipe loadedData, float delay)
    {
        yield return new WaitForSeconds(delay);
        answers.Clear();
        foodObjects.Clear();
        answeredCount = 0;
        answersCount = 0;
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

        int Edible = 1;
        if (PlayerPrefs.GetInt("EdFruit") == 1) Edible = 2;


        // selecting a random recipe
        int selectedRecipe = rnd.Next(0, loadedData.recipes.Length);
        GameObject.Find("Instructions").GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("Recipe-instructions") + " " + loadedData.recipes[selectedRecipe].name + ", " + LocalizationManager.instance.GetLocalizedValue("select");

        // fill the recipe objects in list
        for (int i = 0; i < loadedData.recipes[selectedRecipe].recipeData.Length; i++)
        {
            for (int j = 0; j < loadedData.recipes[selectedRecipe].recipeData[i].count; j++)
            {
                IEdible recipeEdible = edibleCreator.GetEdible(loadedData.recipes[selectedRecipe].recipeData[i].edible, true);
                answers.Add(loadedData.recipes[selectedRecipe].recipeData[i].edible);
                foodObjects.Add(recipeEdible);
            }
            GameObject.Find("Instructions").GetComponent<Text>().text += " " + loadedData.recipes[selectedRecipe].recipeData[i].count + " "
            + LocalizationManager.instance.GetLocalizedValue(loadedData.recipes[selectedRecipe].recipeData[i].edible);
            GameObject.Find("Instructions").GetComponent<Text>().text += loadedData.recipes[selectedRecipe].recipeData[i].count > 1 ? "(s)" : "";
            GameObject.Find("Instructions").GetComponent<Text>().text += i == loadedData.recipes[selectedRecipe].recipeData.Length - 1 ? "." : ", ";
        }
        answersCount = answers.Count;
        // fill the random extra objects in list
        int dummyItemCount = 0;
        dummyItemCount = Foodspawner.Length - 1 - foodObjects.Count;
        for (int i = 0; i < dummyItemCount; i++)
        {
            IEdible dummyEdible;
            if (Edible > 1)
            {
                dummyEdible = rnd.Next(0, 2) > 0 ? edibleCreator.GetRandomFruit(true) : edibleCreator.GetRandomVeggie(true);
            }
            else dummyEdible = edibleCreator.GetRandomVeggie(true);
            foodObjects.Add(dummyEdible);
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
        answeredCount++;
        if (answers.Contains(selected.name))
        {
            answers.RemoveAt(answers.IndexOf(selected.name));
        }

        if (answeredCount == answersCount && progress < 12)
        {
            if (answers.Count < 1)
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
                StartCoroutine(StartRecipeScene(loadedData, 0.3f));
            }
            else
            {
                PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney", 0) + tasksWon);
                SceneController.instance.LoadSceneGameList();
            }
        }
    }
}