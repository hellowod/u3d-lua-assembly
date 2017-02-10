using UnityEngine;
using System.Collections;
using SLua;

public class Circle : MonoBehaviour
{
    private LuaSvr svr;
    private LuaTable self;
    private LuaFunction update;

    void Start()
    {
        svr = new LuaSvr();
        svr.init(null, () => {
            self = (LuaTable)svr.start("circle/circle");
            update = (LuaFunction)self["update"];
        });
    }

    void Update()
    {
        if (update != null) {
            update.call(self);
        }
    }
}
