using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{

    public AnimationCurve scaleDown;
    public float timeToScaleDown;
    private float timer;

    private Vector3 startScale;
    public Vector3 finalScale;
    // Start is called before the first frame update
    void Start()
    {
        startScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < timeToScaleDown)
        {
            timer += Time.deltaTime;
            float ratio = timer / timeToScaleDown;
            float curveRatio = scaleDown.Evaluate(ratio);
            gameObject.transform.localScale = Vector3.Lerp(startScale, finalScale, curveRatio);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
