using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTexts : MonoBehaviour
{

    #region Singleton Pattern

    static FloatingTexts _instance;
    public static FloatingTexts Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<FloatingTexts>();
            return _instance;
        }
    }

    #endregion

    [Header("Details")]
    [SerializeField]
    Transform _parentObj;

    private const string FLOATING_TEXT_PREFAB_KEY = "FloatingText";
    private const float FLOATING_TEXT_GO_UP_VALUE = 150;

    [SerializeField]
    float _effectTime = 1;
    [SerializeField]
    float disappearTime = 0.5f;

    public enum Type
    {
        Info,
        Positive,
        Warning,
        Error
    }
    private void Awake()
    {
        _instance = this;
    }

    public void Show(string text, Type showType)
    {

     
        Text obj = ObjectPoolManager.Instance.GetObject<Text>(FLOATING_TEXT_PREFAB_KEY);
        obj.text = text;

        switch (showType)
        {
            case Type.Info:
                obj.color = Color.white;
                break;
            case Type.Warning:
                obj.color = Color.yellow;
                break;
            case Type.Error:
                obj.color = Color.magenta;
                break;
            case Type.Positive:
                obj.color = Color.green;
                break;


        }

        obj.transform.SetParent(_parentObj);

        obj.transform.localScale = Vector3.one;

        StartCoroutine(EffectCR(obj));
    }

  
    IEnumerator EffectCR(Text text_cmp)
    {

        float i = 0.0f;
        float rate = 1.0f / _effectTime;

        RectTransform rect = text_cmp.gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        Vector2 final_pos = new Vector2(0, FLOATING_TEXT_GO_UP_VALUE);
        Color firstColor = text_cmp.color;

        while (i <= 1)
        {
            i += Time.deltaTime * rate;
            rect.anchoredPosition = Vector2.Lerp(Vector2.zero, final_pos, i);
            yield return null;
        }
        i = 0.0f;
        rate = 1.0f / disappearTime;

        while (i <= 1)
        {
            i += Time.deltaTime * rate;
            rect.anchoredPosition = Vector2.Lerp(final_pos, final_pos + new Vector2(0, FLOATING_TEXT_GO_UP_VALUE/2), i);
            text_cmp.color = Color.Lerp(firstColor, Color.clear, i);
            yield return null;
        }

        ObjectPoolManager.Instance.RecycleObject(text_cmp.GetComponent<PoolableObjectInstance>());
    }
}