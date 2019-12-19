using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStation : AncestorBehaviour
{
    public bool DBG = false;
    public float DBG_Speed = 0.05f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    [Header("=CAMERA=")]
    public Transform CameraRig;
    public Camera MainCamera;
    public OVRScreenFade ScreenFade;
    
    public Transform HandBoneParent;
    public Renderer DefaultHandRend;

    public Material[] HandMaterials = new Material[4];

    [Header("=ASTEROID=")]
    public GameObject AsteroidHand;
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public List<Bullet> bullets = new List<Bullet>();
    public AudioClip[] BulletSoundClips = new AudioClip[3];

    [Header("=RIGS=")]

    public Vector3 gameRig;
    public Transform videoRig;


    [Header("=UINX=")]
    public GameObject UINXControllerPrefab;
    public Transform UINXSlot;
    public Transform AsteroidUINXSlot;
    public UINXController UINXController;
    public bool UINXMode = false;
    bool firstTime = true;
    private void Awake()
    {
        
    }

    public void SwitchPositionToVideo()
    {
        if (firstTime)
        {
            gameRig = transform.position;
            firstTime = false;
        }
        transform.position = videoRig.position;
    }

    public void SwitchPositionToGame()
    {
        transform.position = gameRig;
    }

    public void SwitchAsteroidMode(bool state)
    {
        AsteroidHand.SetActive(state);
        if (state)
        {
            UINXController.transform.parent = AsteroidUINXSlot;
            UINXController.transform.localRotation = Quaternion.identity;
            UINXController.transform.localPosition = Vector3.zero;
        } else
        {
            UINXController.transform.parent = UINXSlot;
            UINXController.transform.localRotation = Quaternion.identity;
            UINXController.transform.localPosition = Vector3.zero;
        }
    }

    public void SetUINXMode(bool state)
    {
        if (UINXController == null)
        {
            UINXController = Instantiate(UINXControllerPrefab, UINXSlot).GetComponent<UINXController>();
            UINXController.transform.localPosition = Vector3.zero;
            UINXController.transform.localRotation = Quaternion.identity;
        }
        UINXMode = state;
        UINXController.raycastActivated = state;
        //Hands[1].uiMode = state;
        //Hands[1].SetGrabState(state ? EGrabType.UIMode : EGrabType.None);
        UINXController.gameObject.SetActive(state);
        HandVisible(state);
    }

    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        if (DBG)
        {
            if (Overmind.GetInstance.CurrentGame == EGames.Asteroid)
            {
                yaw += speedH * Input.GetAxis("Mouse X");
                pitch -= speedV * Input.GetAxis("Mouse Y");
                transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            } else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position += Vector3.left * DBG_Speed;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += Vector3.right * DBG_Speed;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += Vector3.up * DBG_Speed;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.position += Vector3.down * DBG_Speed;
                }
            }
        }
        if (Overmind.AsteroidGameManager.GameState == EAsteroidGameState.Loop || Overmind.AsteroidGameManager.GameState == EAsteroidGameState.Tutorial)
        {
            if (Input.GetKeyDown(KeyCode.B) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                Fire();
           }
        }
    }


    public void Fire()
    {
        bool found = false;
        for (int i = 0; i < bullets.Count; ++i)
        {
            if (bullets[i].isFree)
            {
                bullets[i].Spawn(FirePoint.position, FirePoint.rotation, Overmind.AsteroidGameManager.Bullet_Speed, Overmind.AsteroidGameManager.Bullet_LifeTime);
                found = true;
                break;
            }
        }
        if (!found)
        {
            var b = Instantiate(BulletPrefab, Overmind.AsteroidGameManager.Bullets_Root).GetComponent<Bullet>();
            b.Spawn(FirePoint.position, FirePoint.rotation, Overmind.AsteroidGameManager.Bullet_Speed, Overmind.AsteroidGameManager.Bullet_LifeTime, BulletSoundClips[Random.Range(0, BulletSoundClips.Length)]);
            bullets.Add(b);
        }
    }
    public void HandVisible(bool flag)
    {
        if (flag)
        {
            if (UINXController == null || UINXController.transform.parent == UINXSlot)
            {
                DefaultHandRend.enabled = true;
                AsteroidHand.SetActive(false);
            } else if (UINXController.transform.parent = AsteroidUINXSlot)
            {
                AsteroidHand.SetActive(true);
                DefaultHandRend.enabled = false;
            }
        } else {
            DefaultHandRend.enabled = false;
            AsteroidHand.SetActive(false);
        }
    }
    public void SetColors(EGames Game)
    {
        DefaultHandRend.material = HandMaterials[(int)Game];
        UINXController.SetColors(Game);
    }
}
