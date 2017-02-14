using SLua;
using UnityEngine;

public class ValueType : MonoBehaviour
{
    private LuaSvr l;

    void Start()
    {
        l = new LuaSvr();
        l.init(null, () => {
            l.start("valuetype");
        });
    }
}
