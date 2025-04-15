using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keep : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);//在加载新场景时不销毁脚本挂载的对象
        //方式二 DontDestroyOnLoad(this.gameObject);
    }
}

