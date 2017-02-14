using SLua;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text logText;

    private LuaSvr lua;
	private int progress=0;

    void Start()
	{
#if UNITY_5
		Application.logMessageReceived += this.log;
#else
		Application.RegisterLogCallback(this.log);
#endif

		lua = new LuaSvr();
        lua.init(tick, complete, LuaSvrFlag.LSF_BASIC | LuaSvrFlag.LSF_EXTLIB);
	}

	void log(string cond, string trace, LogType lt)
	{
		logText.text += (cond + "\n");
	}

	void tick(int p)
	{
		progress = p;
	}

    void complete()
    {
        lua.start("main");
        object o = lua.luaState.getFunction("foo").call(1, 2, 3);
        object[] array = (object[])o;
        for (int n = 0; n < array.Length; n++) {
            Debug.Log(array[n]);
        }
        string s = (string)lua.luaState.getFunction("str").call(new object[0]);
        Debug.Log(s);
    }

	void OnGUI()
	{
        if (progress != 100) {
            GUI.Label(new Rect(0, 0, 100, 50), string.Format("Loading {0}%", progress));
        }
	}

}
