using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
