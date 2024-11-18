/****************************************************
    文件：WindowRoot
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-14 9:39:42
	功能：窗体基类服务
*****************************************************/
using CommonNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class WindowRoot : MonoBehaviour
{
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;
    protected NetSvc netSvc;
    protected TimerSvc timerSvc;
    protected AssetLoaderSvc loaderSvc;
    protected bool IsSetGameObject = false;

    /// <summary>
    /// 重新加载
    /// </summary>
    protected virtual void InitWnd()
    {
        resSvc = ResSvc.instance;
        audioSvc = AudioSvc.instance;
        netSvc = NetSvc.instance;
        timerSvc = TimerSvc.Instance;
        loaderSvc= AssetLoaderSvc.instance;
        if (!IsSetGameObject)
        {
            SetGameObject();
            IsSetGameObject = true;
        }
    }
    /// <summary>
    /// 清空窗体
    /// </summary>
    protected virtual void ClearWnd()
    {
        resSvc = null;
        audioSvc = null;
        netSvc = null;
        timerSvc = null;
    }
    /// <summary>
    /// 设置窗体状态,默认true
    /// </summary>
    /// <param name="isActive"></param>
    public void SetWndState(bool isActive = true)
    {

        if (gameObject.activeSelf != isActive)
        {
            SetActive(gameObject, isActive);
        }
        if (isActive)
        {
            InitWnd();
        }
        else
        {
            ClearWnd();
        }
    }
    /// <summary>
    /// 获取窗体状态
    /// </summary>
    public bool GetWndState()
    {
        return gameObject.activeSelf;
    }
    #region Tool Functions
    /// <summary>
    /// 设置物体显示
    /// </summary>
    /// <param name="go"></param>
    /// <param name="isActive"></param>
    protected void SetActive(GameObject go, bool isActive = true)
    {
        go.SetActive(isActive);
    }
    protected void SetActive(Transform transform, bool state = true)
    {
        transform.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rectTransform, bool state = true)
    {
        rectTransform.gameObject.SetActive(state);
    }
    protected void SetActive(Image img, bool state = true)
    {
        img.gameObject.SetActive(state);
    }
    protected void SetActive(Text txt, bool state = true)
    {
        txt.gameObject.SetActive(state);
    }
    /// <summary>
    /// 设置text的重载 
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="context"></param>
    protected void SetText(Text txt, string context = "")
    {
        txt.text = context;
    }
    protected void SetText(Transform transform, int num = 0)
    {
        SetText(transform.GetComponent<Text>(), num);
    }
    protected void SetText(Transform transform, string context = "")
    {
        SetText(transform.GetComponent<Text>(), context);
    }
    protected void SetText(GameObject gameObject, string context = "")
    {
        SetText(gameObject.transform.GetComponent<Text>(), context);
    }
    protected void SetText(Text txt, int num = 0)
    {
        SetText(txt, num.ToString());
    }
    protected void SetText(Text txt, float num = 0)
    {
        SetText(txt, num.ToString());
    }
    protected Transform SetTranFind(string path)
    {
        return transform.Find(path);
    }
    /// <summary>
    /// 在父物体下通过路径找到GameObject
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    protected Transform SetTranFind(GameObject gameObject, string path)
    {
        return gameObject.transform.Find(path);
    }
    protected GameObject GetGameObject(string path)
    {
        return SetTranFind(path).gameObject;
    }
    protected GameObject GetGameObject(GameObject gameObject, string path)
    {
        return SetTranFind(gameObject, path).gameObject;
    }
    protected Animation GetAnimation(string path)
    {
        return SetTranFind(path).GetComponent<Animation>();
    }
    protected Button GetButton(string path)
    {
        return SetTranFind(path).GetComponent<Button>();
    }
    protected Button GetButton(GameObject gameObject)
    {
        return gameObject.GetComponent<Button>();
    }
    protected Toggle GetToggle(GameObject gameObject)
    {
        return gameObject.GetComponent<Toggle>();
    }
    protected Button GetButton(GameObject gameObject, string path)
    {
        return SetTranFind(gameObject, path).GetComponent<Button>();
    }
    protected Text GetText(string path)
    {
        return SetTranFind(path).GetComponent<Text>();
    }
    protected Text GetText(Transform transform)
    {
        return transform.GetComponent<Text>();
    }
    protected Text GetText(GameObject gameObject, string path)
    {
        return SetTranFind(gameObject, path).GetComponent<Text>();
    }
    protected Image GetImg(string path)
    {
        return SetTranFind(path).GetComponent<Image>();
    }
    protected Image GetImg(GameObject obj)
    {
        return obj.GetComponent<Image>();
    }
    protected RawImage GetRawImg(string path)
    {
        return SetTranFind(path).GetComponent<RawImage>();
    }
    protected Image GetImg(Transform transform)
    {
        return transform.GetComponent<Image>();
    }
    protected Image GetImg(GameObject gameObject, string path)
    {
        return SetTranFind(gameObject, path).GetComponent<Image>();
    }
    //设置监听
    protected void AddListener(Button button, UnityAction call)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(call);
    }
    //设置监听
    protected void AddListener(Toggle toggle, UnityAction<bool> call)
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(call);
    }
    /// <summary>
    /// 设置对象物体 只设置一次
    /// </summary>
    protected virtual void SetGameObject()
    {

    }
    protected string FormatString(string data)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < data.Length; i += 4)
        {
            int end = Math.Min(i + 4, data.Length);
            string chunk = data.Substring(i, end - i);
            sb.Append(chunk);
            sb.Append(Environment.NewLine);
        }
        return sb.ToString();
    }
    #endregion
    #region Click Evts
    protected void OnClick(GameObject game, Action<object> evt, object args)
    {
        Listener listener = game.GetOrAddComponent<Listener>();
        listener.onClick = evt;
        listener.args = args;
    }
    protected void OnClickDown(GameObject game, Action<PointerEventData> evt)
    {
        Listener listener = game.GetOrAddComponent<Listener>();
        listener.onClickDown = evt;
    }
    protected void OnClickUp(GameObject game, Action<PointerEventData> evt)
    {
        Listener listener = game.GetOrAddComponent<Listener>();
        listener.onClickUp = evt;
    }
    protected void OnDrag(GameObject game, Action<PointerEventData> evt)
    {
        Listener listener = game.GetOrAddComponent<Listener>();
        listener.onDrag = evt;
    }
    #endregion


}
