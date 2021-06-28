using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string m_Path = Application.dataPath;

        string NutPath = m_Path + "/Assets/nut.jpg";

        Debug.Log(NutPath);
    }

    
}
