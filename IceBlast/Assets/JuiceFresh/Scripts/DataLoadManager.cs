using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoadManager : MonoBehaviour
{

    void Awake()
    {
        DataManager.instance.game_load_m_level_map();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
