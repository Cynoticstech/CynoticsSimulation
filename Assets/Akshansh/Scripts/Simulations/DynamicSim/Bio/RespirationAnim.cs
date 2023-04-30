using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespirationAnim : MonoBehaviour
{
    public UnityEvent OnComplete;
    public float Temp = 5, itemNum = 10,minHeightClamp =4,maxHeightClamp = 10;
    [SerializeField] GameObject seed,grassHopper,water;
    public bool isAnimal,CanAnimate;

    [SerializeField] float animSpeed = 2f, maxHeight = 1f,heightMultiplier =1;
    [SerializeField] Vector3 minPos, maxPos;
    [SerializeField] List<GameObject> curtItems;

    float _Temp = 5, _itemNum = 10;
    bool isPlaying = false;
    private void Start()
    {
        AddItems();
    }
    public void AddItems()
    {
        if(curtItems.Count>0)
        {
            foreach(var v in curtItems)
            {
                Destroy(v);
            }
        }
        var _obj = !isAnimal? seed : grassHopper;
        for(int i =0;i<itemNum;i++)
        {
            var _pos = minPos;
            _pos.x = Random.Range(minPos.x, maxPos.x);
            _pos.y = Random.Range(minPos.y, maxPos.y);
            curtItems.Add(Instantiate(_obj,_pos,Quaternion.identity));
        }
    }
    private void Update()
    {
        if(isPlaying)
        {
            var _Temp = water.transform.localScale;
            _Temp.y += animSpeed * Time.deltaTime;
            water.transform.localScale = _Temp;
            if (_Temp.y>maxHeight)
            {
                isPlaying = false;
                OnComplete?.Invoke();
            }
        }
    }
    public void StartAnimation()
    {
        if (!CanAnimate)
            return;
        isPlaying = true;
        CanAnimate = false;
        _Temp = Temp;
        _itemNum = itemNum;
        maxHeight *= _Temp * _itemNum * Time.deltaTime*heightMultiplier;
        maxHeight = Mathf.Clamp(maxHeight, minHeightClamp, maxHeightClamp);
    }
    public void SetCategory(bool _animal)
    {
        isAnimal = _animal;
        AddItems();
    }
    public void SetTemp(float _val)
    {
        Temp = _val;
    }
    public void SetItem(float _val)
    {
        itemNum = _val;
    }
    private void OnDisable()
    {
        if (curtItems.Count > 0)
        {
            foreach (var v in curtItems)
            {
                Destroy(v);
            }
        }
    }
}
