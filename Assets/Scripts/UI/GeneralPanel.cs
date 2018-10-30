using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralPanel : MonoBehaviour {

    public Button OpenButton;
    public Button CloseButton;
    public GameObject PanelRoot;
    public GameObject CanvasRoot;
    public KeyCode KeyBoardShortCut;
    public System.Action<GeneralPanel> OnOpenCloseActionCallBack;
    public Text Title;
    public string TitleText;

    public enum PopUpState
    {
        Opened,
        Closed
    }
    private PopUpState _state;
    public PopUpState State
    {
        get
        {
            return _state;
        }
    }
    void Start()
    {
        OpenButton.onClick.AddListener(() => OnOpenClicked());
        CloseButton.onClick.AddListener(() => OnCloseClicked());
        _state = PopUpState.Closed;
        Title.text = TitleText;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyBoardShortCut))
        {
            if (_state == PopUpState.Closed)
                OnOpenClicked();
            else
                OnCloseClicked();
        }
    }
    // Update is called once per frame
    void OnOpenClicked()
    {

        OpenPanel();
    }
    void OnCloseClicked()
    {
        ClosePanel();
    }
    public void OpenPanel()
    {
        OpenButton.gameObject.SetActive(false);
        PanelRoot.SetActive(true);
        _state = PopUpState.Opened;
        OnOpenCloseActionCallBack?.Invoke(this);
    }
    public void ClosePanel()
    {
        OpenButton.gameObject.SetActive(true);
        PanelRoot.SetActive(false);
        _state = PopUpState.Closed;
        OnOpenCloseActionCallBack?.Invoke(this);
    }
    public virtual void AddNewGridItem(GridItem gItem, Item item)
    {

    }



}
