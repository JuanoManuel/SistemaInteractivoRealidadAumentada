using UnityEngine;
using UnityEngine.UI;

public class PinManager : MonoBehaviour
{
    public Animator titleAnimator;
    public GameObject uIContent;
    public float timer;
    public bool start;

    private Camera arCamera;
    
    // Start is called before the first frame update
    void Awake()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (timer < 1f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                titleAnimator.SetBool("IsActive", true);
                start = false;
            }
        }
        

        uIContent.transform.LookAt(arCamera.transform);
    }
}
