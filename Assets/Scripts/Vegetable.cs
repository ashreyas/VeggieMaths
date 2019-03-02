using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Vegetable : MonoBehaviour, IEdible
{
    string _nameValue;
    float _EdSizeValue;
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
            return _EdSizeValue;
        }

        set
        {
            _EdSizeValue = value;
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
        _fruitObject.AddComponent<Vegetable>();
        _fruitObject.transform.localScale = new Vector3(1f, 1f, 1f);   //give it reasonable EdSize e.g. strawberry is small and watermelon is big, no sense in making them equally big
        _fruitObject.tag = EdName;
        _fruitObject.AddComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);   //make them detectable when they moved in particular zone.
        //_fruitObject.AddComponent<BoxCollider>().isTrigger = true;
        //_fruitObject.AddComponent<Rigidbody>().isKinematic = true;
        if (isDraggable) _fruitObject.AddComponent<Dragger>();  //make it draggable
    }

    public IEnumerator DisappearEdObject()
    {
        yield return new WaitForSeconds(0.05f);
        if (gameObject.GetComponent<RectTransform>().sizeDelta.x > 10)
            gameObject.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta - new Vector2(5f, 5f);
        else Destroy(gameObject);

        StartCoroutine(DisappearEdObject());
    }
}

public class Tomato : Vegetable
{
    public Tomato(bool isDraggable = false)
    {
        EdName = "Tomato";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/tomato");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Onion : Vegetable
{
    public Onion(bool isDraggable = false)
    {
        EdName = "Onion";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/onion");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Potato : Vegetable
{
    public Potato(bool isDraggable = false)
    {
        EdName = "Potato";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/potato");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Pumpkin : Vegetable
{
    public Pumpkin(bool isDraggable = false)
    {
        EdName = "Pumpkin";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/pumpkin");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Mushroom : Vegetable
{
    public Mushroom(bool isDraggable = false)
    {
        EdName = "Mushroom";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/mushroom");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Lettuce : Vegetable
{
    public Lettuce(bool isDraggable = false)
    {
        EdName = "Lettuce";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/lettuce");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Leek : Vegetable
{
    public Leek(bool isDraggable = false)
    {
        EdName = "Leek";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/leek");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Peas : Vegetable
{
    public Peas(bool isDraggable = false)
    {
        EdName = "Peas";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/green peas");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Eggplant : Vegetable
{
    public Eggplant(bool isDraggable = false)
    {
        EdName = "Eggplant";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/eggplant");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Cucumber : Vegetable
{
    public Cucumber(bool isDraggable = false)
    {
        EdName = "Cucumber";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/cucumber");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Corn : Vegetable
{
    public Corn(bool isDraggable = false)
    {
        EdName = "Corn";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/corn");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Cauliflower : Vegetable
{
    public Cauliflower(bool isDraggable = false)
    {
        EdName = "Cauliflower";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/cauliflower");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Carrot : Vegetable
{
    public Carrot(bool isDraggable = false)
    {
        EdName = "Carrot";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/carrot");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Capsicum : Vegetable
{
    public Capsicum(bool isDraggable = false)
    {
        EdName = "Capsicum";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/capsicum");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}
public class Broccoli : Vegetable
{
    public Broccoli(bool isDraggable = false)
    {
        EdName = "Broccoli";
        EdSize = 1.2f;
        EdSprite = Resources.Load<Sprite>("Sprites/Vegetables/broccoli");
        EdAudio = null;
        CreateEdObject(isDraggable);
    }
}