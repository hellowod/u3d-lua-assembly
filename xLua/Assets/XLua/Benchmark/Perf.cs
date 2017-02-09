using UnityEngine;
using XLua;

public class Perf : MonoBehaviour
{
    static LuaEnv lua = new LuaEnv();

    void Start()
    {
        Profiler.BeginSample("init");
        lua.DoString("require 'perf'");
        Profiler.EndSample();

        lua.Global.Get<LuaFunction>("main").Call();

        Application.logMessageReceived += this.log;
    }

	string logText = "";
	void log(string cond, string trace, LogType lt)
	{
		logText += cond;
		logText += "\n";
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 120, 50), "Test1"))
		{
            Profiler.BeginSample("test1");
            lua.Global.Get<LuaFunction>("test1").Call();
            Profiler.EndSample();
		}

		if (GUI.Button(new Rect(10, 100, 120, 50), "Test2"))
		{
            Profiler.BeginSample("test2");
            lua.Global.Get<LuaFunction>("test2").Call();
            Profiler.EndSample();
        }

		if (GUI.Button(new Rect(10, 200, 120, 50), "Test3"))
		{
            Profiler.BeginSample("test3");
            lua.Global.Get<LuaFunction>("test3").Call();
            Profiler.EndSample();
        }

		if (GUI.Button(new Rect(10, 300, 120, 50), "Test4"))
		{
            Profiler.BeginSample("test4");
            lua.Global.Get<LuaFunction>("test4").Call();
            Profiler.EndSample();
        }

		if (GUI.Button(new Rect(200, 10, 120, 50), "Test5"))
		{
            Profiler.BeginSample("test5");
            lua.Global.Get<LuaFunction>("test5").Call();
            Profiler.EndSample();
        }

        if (GUI.Button(new Rect(200, 100, 120, 50), "Test6 jit"))
        {
            Profiler.BeginSample("test6");
            lua.Global.Get<LuaFunction>("test6").Call();
            Profiler.EndSample();
        }

		if (GUI.Button(new Rect(200, 200, 120, 50), "Test6 non-jit"))
		{
			Profiler.BeginSample("test7");
            lua.Global.Get<LuaFunction>("test7").Call();
            Profiler.EndSample();
        }
        
		GUI.Label(new Rect(400, 200, 300, 150), logText);
	}
}
