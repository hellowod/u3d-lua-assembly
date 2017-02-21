using UnityEngine;
using System.Collections;
using SLua;

public class Perf : MonoBehaviour
{
	private LuaSvr lua;
    private string logText = "";

    void Start()
    {
        long startMem = System.GC.GetTotalMemory(true);

        float start = Time.realtimeSinceStartup;
        lua = new LuaSvr();
        lua.init(null, () => {
            Debug.Log("start cost: " + (Time.realtimeSinceStartup - start));

            var endMem = System.GC.GetTotalMemory(true);
            Debug.Log("startMem: " + startMem + ", endMem: " + endMem + ", " + "cost mem: " + (endMem - startMem));
            lua.start("perf");
        });

#if UNITY_5
        Application.logMessageReceived += this.log;
#else
		Application.RegisterLogCallback(this.log);
#endif
    }
    
    void log(string cond, string trace, LogType lt)
	{
		logText += cond;
		logText += "\n";
	}

    void OnGUI()
    {
        if (!lua.inited) {
            return;
        }

        if (GUI.Button(new Rect(10, 10, 120, 50), "Test1")) {
            logText = "";
            lua.luaState.getFunction("test1").call();
        }

        if (GUI.Button(new Rect(10, 100, 120, 50), "Test2")) {
            logText = "";
            lua.luaState.getFunction("test2").call();
        }

        if (GUI.Button(new Rect(10, 200, 120, 50), "Test3")) {
            logText = "";
            lua.luaState.getFunction("test3").call();
        }

        if (GUI.Button(new Rect(10, 300, 120, 50), "Test4")) {
            logText = "";
            lua.luaState.getFunction("test4").call();
        }

        if (GUI.Button(new Rect(200, 10, 120, 50), "Test5")) {
            logText = "";
            lua.luaState.getFunction("test5").call();
        }

        if (GUI.Button(new Rect(200, 100, 120, 50), "Test6 jit")) {
            logText = "";
            lua.luaState.getFunction("test6").call();
        }

        if (GUI.Button(new Rect(200, 200, 120, 50), "Test6 non-jit")) {
            logText = "";
            lua.luaState.getFunction("test7").call();
        }

        if (GUI.Button(new Rect(10, 400, 300, 50), "Click here for detail(in Chinese)")) {
            Application.OpenURL("http://www.sineysoft.com/post/164");
        }

        GUI.Label(new Rect(400, 200, 300, 50), logText);
    }
}
