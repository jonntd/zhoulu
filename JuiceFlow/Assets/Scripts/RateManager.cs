using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateManager : MonoBehaviour
{

    public GameObject nerver;

    void OnEnable()
    {
        int result_rared=PlayerPrefs.GetInt("Rated_never");
        if (result_rared > 0)
        {
            if(nerver!=null)
                nerver.SetActive(false);
        }
    }

    void OnDisable()
    {

    }
}
