using UnityEngine;
using UnityEngine.UI;

public class PinManager : MonoBehaviour
{
    public Animator titleAnimator;
    public GameObject uIContent;
    public float timer, startTime = 1f;
    public bool start;
    public bool value;

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
            if (timer < startTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                titleAnimator.SetBool("IsActive", value);
                start = false;
                timer = 0f;
            }
        }
        

        uIContent.transform.LookAt(arCamera.transform);
    }
}
