using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager instance = null;
    public static PlayerManager Instance
    {
        get
        {
            return instance;

        }
    }
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject.GetComponent<PlayerManager>());
            return;
        }
        instance = this;
    }
    [SerializeField] private PlayerInteraction playerInteraction;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Interaction()
    {
        playerInteraction.Interaction();
    }

}
