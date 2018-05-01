using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndFadeOut : MonoBehaviour
{
    [Serializable]
    public class AffectedGameObject
    {
        public GameObject GameObject;

        private bool _startYPosSet = false;
        private float _startYPos;
        public float StartYPos {
            get {
                if (!_startYPosSet)
                {
                    _startYPosSet = true;
                    _startYPos = GameObject.transform.position.y;
                }
                return _startYPos;
            }
        }


        public TextMesh TextMesh;

        private bool _startAlphaSet = false;
        private float _startAlpha;
        public float StartAlpha {
            get {
                if (!_startAlphaSet)
                {
                    _startAlphaSet = true;
                    _startAlpha = TextMesh.color.a;
                }
                return _startAlpha;
            }
        }
    }

    public float MoveUpDistance;
    public float FadeOutTime;

    public List<AffectedGameObject> AffectedGameObjects = new List<AffectedGameObject>();

    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        float timeRatio = (Time.time - _startTime) / FadeOutTime;
        
        
        if (timeRatio >= 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            foreach (var afgo in AffectedGameObjects)
            {
                //Linear falldown

                //float currentY = afgo.StartYPos + (MoveUpDistance * timeRatio);
                //afgo.GameObject.transform.position = new Vector3(afgo.GameObject.transform.position.x, currentY, afgo.GameObject.transform.position.z);

                float currentAlpha = afgo.StartAlpha - (afgo.StartAlpha * timeRatio);
                afgo.TextMesh.color = new Color(afgo.TextMesh.color.r, afgo.TextMesh.color.g, afgo.TextMesh.color.b, currentAlpha);


                //Fast at beginning slower at the end

                Vector3 targetPos = new Vector3(afgo.GameObject.transform.position.x, afgo.StartYPos + MoveUpDistance, afgo.GameObject.transform.position.z);
                afgo.GameObject.transform.position = Vector3.Lerp(afgo.GameObject.transform.position, targetPos, Time.deltaTime * 2f);
                
                //Color targetColor = new Color(afgo.TextMesh.color.r, afgo.TextMesh.color.g, afgo.TextMesh.color.b, 0f);
                //afgo.TextMesh.color = Color.Lerp(afgo.TextMesh.color, targetColor, Time.deltaTime * 2f);

            }
        }
    }
}
