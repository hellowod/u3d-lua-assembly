using UnityEngine;
using System.Collections;
using SLua;

public class FunctionTest : MonoBehaviour
{
    private LuaSvr lua;

    // Use this for initialization
    void Start()
    {
        lua = new LuaSvr();
        lua.init(null, () => {
            lua.start("utils/luafunction");
        });
    }

    void Update()
    {

    }
}
