/****************************************************
    文件：SpriteAnimation.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/8/7 20:7:35
	功能：Nothing
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
    public string SpriteName;
    public string Path;
    public int Speed;
    public int PlayStartIndex;
    public int PlayEndIndex;
    public bool Loop;
    private Dictionary<string, Sprite> m_Sprites=new Dictionary<string, Sprite>();
    private int m_PlayCurrIndex;
    private float timer = 0f; // 计时器
    private Image SpriteImage;
    public void Start()
    {
        m_PlayCurrIndex = PlayStartIndex;
        SpriteImage = GetComponent<Image>();
    }
    public void Update()
    {
        if (Loop)
        {
            timer += Time.deltaTime;
            // 如果计时器超过了每次执行的间隔时间
            if (timer >= 1f / Speed)
            {
                m_PlayCurrIndex++;
                //重置当前索引
                if (m_PlayCurrIndex > PlayEndIndex) { m_PlayCurrIndex = PlayStartIndex; }
                string CurrSpriteName = SpriteName + m_PlayCurrIndex.ToString();
                if (m_Sprites.ContainsKey(CurrSpriteName))
                {
                    SpriteImage.sprite = m_Sprites[CurrSpriteName];
                }
                else
                {
                    Sprite sprite = Resources.Load<Sprite>(Path + CurrSpriteName);
                    SpriteImage.sprite = sprite;
                    m_Sprites.Add(CurrSpriteName, sprite);

                }
                timer -= 1f / Speed;
            }

        }
    }
}