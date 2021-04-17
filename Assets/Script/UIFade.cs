using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public static UIFade instance;

    public Image fadeScreen;
    public bool loading;
    public float loadingTime = 4f;
    public void setLoading(bool loadBool)
    {
        loading = loadBool;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (loading && fadeScreen.color.a < 1)
        {
            fadeScreen.color = new Color(0, 0, 0, Mathf.MoveTowards(fadeScreen.color.a, 1f, loadingTime * Time.deltaTime));
        } else if (fadeScreen.color.a > 0)
        {
            fadeScreen.color = new Color(0, 0, 0, Mathf.MoveTowards(fadeScreen.color.a, 0f, loadingTime * Time.deltaTime));
        }
    }
}
