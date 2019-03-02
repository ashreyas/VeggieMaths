using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour, IEdible
{
    string _nameValue;
    float _sizeValue;
    Sprite _sprite;
    AudioClip _audio;
    GameObject _fruitObject;

    public string EdName
    {
        get
        {
            return _nameValue;
        }

        set
        {
            _nameValue = value;
        }
    }
    public float EdSize
    {
        get
        {
            return _sizeValue;
        }

        set
        {
            _sizeValue = value;
        }
    }
    public Sprite EdSprite
    {
        get
        {
            return _sprite;
        }

        set
        {
            _sprite = value;
        }
    }
    public AudioClip EdAudio
    {
        get
        {
            return _audio;
        }

        set
        {
            _audio = value;
        }
    }

    public GameObject EdObject
    {
        get
        {
            return _fruitObject;
        }

        set
        {
            _fruitObject = value;
        }
    }

    public void CreateEdObject(bool isDraggable)
    {
        _fruitObject = new GameObject(EdName);  //create gameobject
        _fruitObject.AddComponent<Image>().sprite = EdSprite;  //add item image  
        _fruitObject.AddComponent<Fruit>();
        _fruitObject.transform.localScale = new Vector3(1f, 1f, 1f);   //give it reasonable size e.g. strawberry is small and watermelon is big, no sense in making them equally big
        _fruitObject.tag = EdName;
        _fruitObject.AddComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);   //make them detectable when they moved in particular zone.
        if (isDraggable) _fruitObject.AddComponent<Dragger>();  //make it draggable
    }

    public IEnumerator DisappearEdObject()
    {
        //to be coded.
        yield return new WaitForSeconds(0.05f);
        if (gameObject.GetComponent<RectTransform>().sizeDelta.x > 10)
            gameObject.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta - new Vector2(5f, 5f);
        else Destroy(gameObject);

        StartCoroutine(DisappearEdObject());
    }
}

public class Apple : Fruit
{
    public Apple(bool isDraggable = false)
    {
        EdName = "Apple";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Fruits/apple");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Banana : Fruit
{
    public Banana(bool isDraggable = false)
    {
        EdName = "Banana";
        EdSize = 1.7f;
        EdSprite = Resources.Load<Sprite>("Sprites/Fruits/banana");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Grape : Fruit
{
    public Grape(bool isDraggable = false)
    {
        EdName = "Grape";
        EdSize = 1.9f;
        EdSprite = Resources.Load<Sprite>("Sprites/Fruits/grapes");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Lemon : Fruit
{
    public Lemon(bool isDraggable = false)
    {
        EdName = "Lemon";
        EdSize = 0.9f;
        EdSprite = Resources.Load<Sprite>("Sprites/Fruits/lemon");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Mango : Fruit
{
    public Mango(bool isDraggable = false)
    {
        EdName = "Mango";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Fruits/mango");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Orange : Fruit
{
    public Orange(bool isDraggable = false)
    {
        EdName = "Orange";
        EdSize = 0.9f;
        EdSprite = Resources.Load<Sprite>("Sprites/Fruits/orange");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Pineapple : Fruit
{
    public Pineapple(bool isDraggable = false)
    {
        EdName = "Pineapple";
        EdSize = 2.7f;
        EdSprite = Resources.Load<Sprite>("Sprites/Fruits/pineapple");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Strawberry : Fruit
{
    public Strawberry(bool isDraggable = false)
    {
        EdName = "Strawberry";
        EdSize = 0.6f;
        EdSprite = Resources.Load<Sprite>("Sprites/Fruits/strawberry");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Watermelon : Fruit
{
    public Watermelon(bool isDraggable = false)
    {
        EdName = "Watermelon";
        EdSize = 2.7f;
        EdSprite = Resources.Load<Sprite>("Sprites/Fruits/watermelon");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}