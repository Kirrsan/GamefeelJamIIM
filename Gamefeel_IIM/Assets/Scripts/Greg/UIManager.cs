using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    
    public Image[] Cracks;
    private List<Image> CracksLeftToAdd = new List<Image>();

    
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Cracks.Length; i++)
        {
            CracksLeftToAdd.Add(Cracks[i]);
        }
    }
    
    
    public void AddCrack()
    {
        if (CracksLeftToAdd.Count <= 0) return;
        
        int crackToAdd = Random.Range(0, CracksLeftToAdd.Count);
        CracksLeftToAdd[crackToAdd].gameObject.SetActive(true);
        CracksLeftToAdd.RemoveAt(crackToAdd);
    }

    public void ResetCracks()
    {
        CracksLeftToAdd.Clear();
        for (int i = 0; i < Cracks.Length; i++)
        {
            Cracks[i].gameObject.SetActive(false);
            CracksLeftToAdd.Add(Cracks[i]);
        }
    }
}
