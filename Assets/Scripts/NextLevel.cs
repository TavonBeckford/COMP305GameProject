using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    public string gameobjecttag;
    public int levelnumber;

    private GameObject mob;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
{
    mob = GameObject.FindGameObjectWithTag(gameobjecttag);
    if (mob == null)
    {
            SceneManager.LoadSceneAsync(levelnumber);
    }

}
}