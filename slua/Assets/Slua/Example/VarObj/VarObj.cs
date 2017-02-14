using SLua;
using UnityEngine;

public class VarObj : MonoBehaviour
{
    private LuaSvr l;

    void Start()
    {
        l = new LuaSvr();
        l.init(null, () => {
            l.start("varobj");
        });
    }
}
