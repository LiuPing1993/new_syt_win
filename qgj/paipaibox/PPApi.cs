using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace qgj
{
    public enum PPRet
    {
        PP_NO_ERROR = 0,   // No errors.
        PP_ERROR,
        PP_NOT_SUPPORTED,
        PP_API_UNINIT,
        PP_VERSION_ERROR
    };

    public enum PAIPAI_EVENT
    {
        BOX_CONNECTED = 1,
        BOX_DISCONNECTED,
        DECODE_SUCCESS,
        TOKEN_INVALID,
        EVENT_SIZE
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PAIPAI_SCENE
    {
        public string name;
        public int stateCount;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PAIPAI_SCENE_LIST
    {
        public int sceneCount;
        public IntPtr sceneArray;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PAIPAI_DECODE_RESULT
    {
        public string data;
        public int size;
    };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int pp_event_callback_t(PAIPAI_EVENT evt, IntPtr decode);

    class PPApi
    {
        const string dllPath = "\\paipai\\libapi.dll";

        [DllImport(dllPath, EntryPoint = "GetApiVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetApiVersion();

        [DllImport(dllPath, EntryPoint = "GetSceneList", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetSceneList();

        [DllImport(dllPath, EntryPoint = "GetCurrentScene", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetCurrentScene();

        [DllImport(dllPath, EntryPoint = "SelectScene", CallingConvention = CallingConvention.Cdecl)]
        public static extern PPRet SelectScene(IntPtr scene);

        [DllImport(dllPath, EntryPoint = "GetSceneState", CallingConvention = CallingConvention.Cdecl)]
        public static extern PPRet GetSceneState(ref int state);

        [DllImport(dllPath, EntryPoint = "SetSceneState", CallingConvention = CallingConvention.Cdecl)]
        public static extern PPRet SetSceneState(int state);

        [DllImport(dllPath, EntryPoint = "addEventCallback", CallingConvention = CallingConvention.Cdecl)]
        public static extern PPRet addEventCallback(pp_event_callback_t cl);

        [DllImport(dllPath, EntryPoint = "configSPToken", CallingConvention = CallingConvention.Cdecl)]
        public static extern PPRet configSPToken(StringBuilder token);

        [DllImport(dllPath, EntryPoint = "TerminalPaipai", CallingConvention = CallingConvention.Cdecl)]
        public static extern PPRet TerminalPaipai();

        [DllImport(dllPath, EntryPoint = "SetScanInterval", CallingConvention = CallingConvention.Cdecl)]
        public static extern PPRet SetScanInterval(int milliseconds);

        [DllImport(dllPath, EntryPoint = "InitialPaipai", CallingConvention = CallingConvention.Cdecl)]
        public static extern PPRet InitialPaipai(IntPtr hWnd, StringBuilder strPath, IntPtr iniParam);
    }
}
