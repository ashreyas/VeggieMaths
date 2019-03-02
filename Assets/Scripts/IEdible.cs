using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEdible
{
    string EdName { get; set; }
    float EdSize { get; set; }
    Sprite EdSprite { get; set; }
    AudioClip EdAudio { get; set; }
    GameObject EdObject { get; set; }
    void CreateEdObject(bool isDraggable);
    IEnumerator DisappearEdObject();
}
