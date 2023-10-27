using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pillulebleurouge : MonoBehaviour
{
    public void ClassicGame()
    {
        SceneManager.LoadScene("GroundScene");
    }
    public void NoGround()
    {
        SceneManager.LoadScene("NoGroundScene");
    }
}
