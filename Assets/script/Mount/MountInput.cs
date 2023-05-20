using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountInput : MonoBehaviour
{
    [SerializeField] private playermovement playermovement;
    [SerializeField] private GameObject SpawnVFX;
    [SerializeField] private FixedButton Mountbtn;
    private MountProperties MountProperties;
    public GameObject MountItem;
    public GameObject Mount;
    public float Timer = 0f;
    public float MaxTime = 3f;
    public bool CanMount = true;
    public bool OnMount = false;

    private bool CanAddHeight = true;
    void Awake()
    {
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnMount();
        //  print(Mountbtn.Pressed);
        // LockPlayerHeight();
        ResetCanMount();

    }
    void LockPlayerHeight()
    {
        if (playermovement.IsMount)
        {
            Vector3 currentPosition = playermovement.transform.position;
            currentPosition.y = 30f;
            playermovement.transform.position = currentPosition;
        }
    }
    void SpawnMount()
    {
        if ((Input.GetKeyDown(KeyCode.M) || Mountbtn.Pressed) && CanMount)
        {

            CanMount = false;

            if (Mount == null)
            {
                // playermovement.transform.position += new Vector3(0, 2, 0);
                playermovement.IsMount = true;
                Mount = Instantiate(MountItem, transform.position, transform.rotation);
                MountProperties = Mount.GetComponent<MountProperties>();
                playermovement.MoveSpeed = MountProperties.MountSpeed;
                playermovement.MountAnim = Mount.GetComponent<AnimationController>();
                Instantiate(SpawnVFX, transform.position + Vector3.up, Quaternion.identity);
                Mount.transform.parent = transform;
            }
            else if (Mount != null)
            {
                CanMount = false;
                playermovement.MoveSpeed = playermovement.BaseSpeed;
                playermovement.IsMount = false;
                Destroy(Mount);
                Mount = null;

            }
        }
    }

    void ResetCanMount()
    {
        if (!CanMount)
        {
            Timer += Time.deltaTime;
            if (Timer >= MaxTime)
            {
                Timer = 0f;
                CanMount = true;
            }
        }
    }
}
