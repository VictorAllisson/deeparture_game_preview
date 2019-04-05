using UnityEngine;
using System.Collections;

public class Keyframe
{
    public Vector3 position;
    public Vector3 rotation;
    //public AnimatorClipInfo[] animShadow;

    //public Keyframe(Vector3 position, Vector3 rotation, AnimatorClipInfo[] animShadow)
    public Keyframe(Vector3 position, Vector3 rotation)
    {
        this.position = position;
        this.rotation = rotation;
        //this.animShadow = animShadow;
    }
}

public class TimeController : MonoBehaviour
{
    public GameObject player;
    public GameObject shadow;
    public Animator anima;

    public ArrayList keyframes;
    public bool isReversing = false;

    public int keyframe = 5;
    private int frameCounter = 0;
    private int reverseCounter = 0;

    private Vector3 currentPosition;
    private Vector3 previousPosition;
    private Vector3 currentRotation;
    private Vector3 previousRotation;

    private bool firstRun = true;

    void Start()
    {
        keyframes = new ArrayList();
        anima = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isReversing = true;
        }
        else
        {
            isReversing = false;
            firstRun = true;
        }
    }

    void FixedUpdate()
    {
        if (!isReversing)
        {
            if (frameCounter < keyframe)
            {
                frameCounter += 1;
            }
            else
            {
                frameCounter = 0;
                //keyframes.Add(new Keyframe(player.transform.position, player.transform.localEulerAngles, anima.GetCurrentAnimatorClipInfo(0)));
                keyframes.Add(new Keyframe(player.transform.position, player.transform.localEulerAngles));
            }
        }
        else
        {
            if (reverseCounter > 0)
            {
                reverseCounter -= 1;
            }
            else
            {
                reverseCounter = keyframe;
                RestorePositions();
            }

            if (firstRun)
            {
                firstRun = false;
                RestorePositions();
            }

            float interpolation = (float)reverseCounter / (float)keyframe;
            player.transform.position = Vector3.Lerp(previousPosition, currentPosition, interpolation);
            player.transform.localEulerAngles = Vector3.Lerp(previousRotation, currentRotation, interpolation);
            //shadow. = animShadow;
            //Preciso ajustar a sombra, https://docs.unity3d.com/ScriptReference/AnimatorOverrideController.html tem o que precisa pra ajustar
        }

        if (keyframes.Count > 10)
        {
            currentPosition = (keyframes[1] as Keyframe).position;
            previousPosition = (keyframes[0] as Keyframe).position;

            currentRotation = (keyframes[1] as Keyframe).rotation;
            previousRotation = (keyframes[0] as Keyframe).rotation;

            float interpolationShadow = (float)reverseCounter / (float)keyframe;
            shadow.transform.position = Vector3.Lerp(previousPosition, currentPosition, interpolationShadow);
            shadow.transform.localEulerAngles = Vector3.Lerp(previousRotation, currentRotation, interpolationShadow);
            


            keyframes.RemoveAt(0);
        }
    }

    void RestorePositions()
    {
        int lastIndex = keyframes.Count - 1;
        int secondToLastIndex = keyframes.Count - 2;

        if (secondToLastIndex >= 0)
        {
            currentPosition = (keyframes[lastIndex] as Keyframe).position;
            previousPosition = (keyframes[secondToLastIndex] as Keyframe).position;

            currentRotation = (keyframes[lastIndex] as Keyframe).rotation;
            previousRotation = (keyframes[secondToLastIndex] as Keyframe).rotation;

            keyframes.RemoveAt(lastIndex);
        }
    }
}