using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface interactable
{
    /// <summary>
    /// Set and write NPC dialogue and portaits based on quest state
    /// </summary>
    /// <param name="portrait"></param>
    void Interact(Sprite portrait);
}
