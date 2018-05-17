using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterManager : MonoBehaviour
{
    //Zero sized vector not allowed in shader
    private List<Vector4> _bloodSplatterOrigins = new List<Vector4>();
    private List<Vector4> _bloodSplatterPatters = new List<Vector4>();
    private int _bloodSplatterCount = 0;

    private void Start()
    {
        EventSystem.Instance.AddBloodSplatterEvent += AddBloodSplatter;
        EventSystem.Instance.GetBloodSplattersEvent += GetBloodSplatters;
        EventSystem.Instance.GetBloodSplatterPatternsEvent += GetBloodSplatterPatterns;
        EventSystem.Instance.GetBloodSplatterCountEvent += GetBloodSplatterCount;

        //Array has to be initialized fully to work with the shader
        for (int i = 0; i < 1023; i++)
        {
            _bloodSplatterOrigins.Add(new Vector4(0, 0, -100, -100));
            _bloodSplatterPatters.Add(new Vector4(0, 0, 0, 0));
        }
    }

    private void OnDestroy()
    {
        EventSystem.Instance.AddBloodSplatterEvent -= AddBloodSplatter;
        EventSystem.Instance.GetBloodSplattersEvent -= GetBloodSplatters;
        EventSystem.Instance.GetBloodSplatterPatternsEvent -= GetBloodSplatterPatterns;
        EventSystem.Instance.GetBloodSplatterCountEvent -= GetBloodSplatterCount;
    }

    public void AddBloodSplatter(Vector3 worldCoords)
    {
        if(_bloodSplatterCount < _bloodSplatterOrigins.Count)
        {
            _bloodSplatterOrigins[_bloodSplatterCount] = new Vector4(worldCoords.x, worldCoords.y, worldCoords.z, 1.0f);
            _bloodSplatterPatters[_bloodSplatterCount] = new Vector4(Random.Range(0, 1000), Random.Range(0, 1000), Random.Range(0, 1000), Random.Range(0, 1000));
            _bloodSplatterCount++;
        }
    }

    public List<Vector4> GetBloodSplatters()
    {
        return _bloodSplatterOrigins;
    }
    public List<Vector4> GetBloodSplatterPatterns()
    {
        return _bloodSplatterPatters;
    }

    public int GetBloodSplatterCount()
    {
        return _bloodSplatterCount;
    }
}
