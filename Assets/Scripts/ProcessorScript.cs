using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class ProcessorScript : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {
            Destroy(col.GetComponent<Dragger>());
            if (col.GetComponent<Vegetable>() != null)
                StartCoroutine(col.GetComponent<Vegetable>().DisappearEdObject());
            else StartCoroutine(col.GetComponent<Fruit>().DisappearEdObject());



            if (SceneManager.GetActiveScene().name == "GameRecipeScene")
            {
                RecipesSceneScript.instance.DetectSelection(col.gameObject);
            }
            else if (SceneManager.GetActiveScene().name == "GameOdd1OutScene")
            {
                Odd1OutScript.instance.DetectSelection(col.gameObject);
            }
        }
    }
}
