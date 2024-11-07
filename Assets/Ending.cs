using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] public AudioClip yippeeSoundClip;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PlaySound(yippeeSoundClip);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
