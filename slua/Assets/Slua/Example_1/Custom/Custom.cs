using SLua;
using System;
using UnityEngine;

/***
 * Custom.cs
 * 
 * 自定义导出类基本实现方式
 */ 
[CustomLuaClassAttribute]
public class Custom : MonoBehaviour
{
	private int v = 520;
	private static string vs = "xiaoming & hanmeimei";
	private LuaSvr lua;

	private static Custom ins;

    void Start()
    {
        ins = this;

        lua = new LuaSvr();
        lua.init(null, () => {
            lua.start("custom");
        });
    }

	// this exported function don't generate stub code if it had MonoPInvokeCallbackAttribute attribute, only register it
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    public static int instanceCustom(IntPtr l)
	{
		Custom self = (Custom)LuaObject.checkSelf(l);
		LuaObject.pushValue(l, true);
		LuaDLL.lua_pushstring(l, "xiaoming");
		LuaDLL.lua_pushstring(l, "hanmeimei");
		LuaDLL.lua_pushinteger(l, self.v);
		return 4;
	}

	// this exported function don't generate stub code, only register it
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[StaticExport]
    public static int staticCustom(IntPtr l)
	{
		LuaObject.pushValue(l, true);
		LuaDLL.lua_pushstring(l, vs);
		LuaObject.pushObject(l, ins);
		return 3;
	}

	public int this[string key] {
        get {
            if (key == "test")
                return v;
            return 0;
        }
        set {
            if (key == "test") {
                v = value;
            }
        }
    }

	public string getTypeName(Type t)
	{
		return t.Name;
	}
}


/***
 * 重载默认库函数方式（基于C#的属性标签方式，相比tolua方便很多）
 * 
 */ 
namespace SLua
{
    [OverloadLuaClass(typeof(GameObject))]
	public class MyGameObject : LuaObject {
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
		public static int Find_s(IntPtr l) {
			UnityEngine.Debug.Log ("GameObject.Find overloaded my MyGameObject.Find");
			try {
				System.String a1;
				checkType(l,1,out a1);
				var ret=UnityEngine.GameObject.Find(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			catch(Exception e) {
				return error(l,e);
			}
		}
	}

}