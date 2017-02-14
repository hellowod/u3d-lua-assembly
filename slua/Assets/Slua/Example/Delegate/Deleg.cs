using SLua;
using System;
using System.Collections.Generic;
using UnityEngine;

/***
 * Delegate 回调和实现
 */ 
[CustomLuaClassAttribute]
public class Deleg : MonoBehaviour
{
	public delegate bool GetBundleInfoDelegate(string path, out string url, out int ver);
	public delegate void SimpleDelegate(string path, GameObject g);


    public static GetBundleInfoDelegate d;
    public static SimpleDelegate s;

    private LuaSvr lua;

    public static GetBundleInfoDelegate dx {
        get {
            return d;
        }
        set {
            d = value;
        }
    }

	void Start()
	{
		lua = new LuaSvr();
		lua.init(null,()=>{
			lua.start("delegate");
		});
	}

    public static void callD()
    {
        string url;
        int ver;
        if (d != null) {
            bool ret = d("/path", out url, out ver);
            Debug.Log(string.Format("{0},{1},{2}", ret, url, ver));
        }
        if (s != null) {
            s("GameObject", new GameObject("SimpleDelegate"));
        }
    }

    public static void setcallback2(Action<int> a, Action<string> b)
    {
        if (a != null) a(1);
        if (b != null) b("hello");
    }

	public static void testFunc(Func<int> f)
	{
		Debug.Log(string.Format("Func return {0}", f()));
	}

	public static void testAction(Action<int, string> f)
	{
		f(1024, "caoliu");
	}

	public static void testDAction(Action<int, Dictionary<int, object>> f)
	{
		f(1024, new Dictionary<int, object>());

	}

	public static void callDAction()
	{
		if (daction != null) {
            daction(2048, new Dictionary<int, object>());
        }
	}

	public static Action<int, Dictionary<int, object>> daction;

	public static Func<int, string, bool> getFunc(Func<int, string, bool> f)
	{
		return f;
	}
}
