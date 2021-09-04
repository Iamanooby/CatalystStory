using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    // Script: Called when transitioning from one room (location) to another. 

    // cameraChangeMin (Vec2), cameraChangeMax (Vec2), indicates the amount of offset for the main camera max and min boundaries.
    public Vector2 cameraChangeMin;
    public Vector2 cameraChangeMax;

    // playerChange (Vec3), indicates the offset for the player character when going through transition
    public Vector3 playerChange;
    private CameraMovement cam;

    // needText (Bool) TRUE: show location name, FALSE: Dont show location name
    public bool needText;

    // placeName (str) location's name
    public string placeName;

    // For editing text GameObject
    public GameObject text;
    public Text placeText;

    // To edit text display length
    public float textDisplayDuration;

    // Animator for text fade
    public Animator transition;

    // To Change Audio
    public AudioClip newTrack;
    private AudioManager theAM;
    public bool needMusic;

    // Start is called before the first frame update
    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check that the 'Player' tag is the object that triggered
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPosition.x += cameraChangeMin.x;
            cam.minPosition.y += cameraChangeMin.y;
            cam.maxPosition.x += cameraChangeMax.x;
            cam.maxPosition.y += cameraChangeMax.y;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(placeNameCorout());
            }
            else
            {
                text.SetActive(false);
            }
            if (needMusic)
            {
                if (newTrack != null)
                {
                    theAM.ChangeBGM(newTrack);
                }
            }
        }
    }

    // Method to run in parallel with other processors with specified wait time
    private IEnumerator placeNameCorout()
    {
        text.SetActive(true);
        placeText.text = placeName;

        // Delay for x seconds
        yield return new WaitForSeconds(textDisplayDuration);
        transition.SetTrigger("Fade End");
        yield return new WaitForSeconds(1f);
        text.SetActive(false);
    }

}
