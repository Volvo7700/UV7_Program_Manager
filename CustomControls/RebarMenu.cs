//using System;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Windows.Forms;

//public class RebarMenu : NativeWindow, IDisposable
//{
//    #region WinAPI & Strukturen

//    private const int WS_CHILD = 0x40000000;
//    private const int WS_VISIBLE = 0x10000000;

//    private const int RBS_VARHEIGHT = 0x0200;
//    private const int RBS_BANDBORDERS = 0x0400;
//    private const int RBS_AUTOSIZE = 0x2000;

//    private const int CCS_NORESIZE = 0x0004;
//    private const int CCS_NOPARENTALIGN = 0x0008;
//    private const int CCS_ADJUSTABLE = 0x0020;

//    private const int TBSTYLE_TOOLTIPS = 0x0100;
//    private const int TBSTYLE_DROPDOWN = 0x0008;

//    private const int RB_INSERTBAND = 0x0401;
//    private const int WM_COMMAND = 0x0111;
//    private const int WM_NOTIFY = 0x004E;
//    private const int WM_SYSKEYDOWN = 0x0104;
//    private const int WM_SYSCHAR = 0x0106;

//    private const int TBN_DROPDOWN = -521;

//    private const int TB_PRESSBUTTON = 0x0400 + 11;

//    [StructLayout(LayoutKind.Sequential)]
//    struct REBARBANDINFO
//    {
//        public int cbSize;
//        public int fMask;
//        public int fStyle;
//        public int clrFore;
//        public int clrBack;
//        public IntPtr hWndChild;
//        public int cxMinChild;
//        public int cyMinChild;
//        public int cx;
//        public IntPtr hbmBack;
//        public int wID;
//        public int cyChild;
//        public int cyMaxChild;
//        public int cyIntegral;
//        public int cxIdeal;
//        public IntPtr lParam;
//        public int cxHeader;
//    }

//    [StructLayout(LayoutKind.Sequential)]
//    struct TBBUTTON
//    {
//        public int iBitmap;
//        public int idCommand;
//        public byte fsState;
//        public byte fsStyle;
//        public byte bReserved0;
//        public byte bReserved1;
//        public IntPtr dwData;
//        public IntPtr iString;
//    }

//    [StructLayout(LayoutKind.Sequential)]
//    struct NMTOOLBAR
//    {
//        public NMHDR hdr;
//        public int iItem;
//        public TBBUTTON tbButton;
//        public int cchText;
//        public IntPtr pszText;
//        public RECT rcButton;
//        public int dwItemSpec;
//        public int uButtonState;
//        public int idCommand;
//        public IntPtr hwndCombo;
//    }

//    [StructLayout(LayoutKind.Sequential)]
//    struct NMHDR
//    {
//        public IntPtr hwndFrom;
//        public IntPtr idFrom;
//        public int code;
//    }

//    [StructLayout(LayoutKind.Sequential)]
//    struct RECT
//    {
//        public int left, top, right, bottom;
//    }

//    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
//    static extern IntPtr CreateWindowEx(
//        int dwExStyle,
//        string lpClassName,
//        string lpWindowName,
//        int dwStyle,
//        int x, int y,
//        int nWidth, int nHeight,
//        IntPtr hWndParent,
//        IntPtr hMenu,
//        IntPtr hInstance,
//        IntPtr lpParam);

//    [DllImport("user32.dll")]
//    static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

//    [DllImport("user32.dll")]
//    static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref REBARBANDINFO lParam);

//    [DllImport("user32.dll")]
//    static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TBBUTTON lParam);

//    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
//    static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

//    [DllImport("user32.dll")]
//    static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

//    [DllImport("user32.dll")]
//    static extern bool TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr prcRect);

//    [DllImport("user32.dll")]
//    static extern IntPtr CreatePopupMenu();

//    [DllImport("user32.dll")]
//    static extern bool DestroyMenu(IntPtr hMenu);

//    #endregion

//    #region Felder

//    private IntPtr _rebarHandle;
//    private IntPtr _toolbarHandle;
//    private readonly IntPtr _parentHandle;
//    private readonly IntPtr _instanceHandle;

//    private Dictionary<int, MenuItemData> _menuItems = new Dictionary<int, MenuItemData>();
//    private int _nextCommandId = 1;

//    private bool _altPressed = false;

//    #endregion

//    public RebarMenu(IntPtr parentHandle)
//    {
//        _parentHandle = parentHandle;
//        _instanceHandle = Marshal.GetHINSTANCE(typeof(RebarMenu).Module);

//        CreateRebarAndToolbar();

//        this.AssignHandle(_rebarHandle);
//    }

//    private void CreateRebarAndToolbar()
//    {
//        _rebarHandle = CreateWindowEx(
//            0,
//            "ReBarWindow32",
//            null,
//            WS_CHILD | WS_VISIBLE | RBS_VARHEIGHT | RBS_BANDBORDERS | RBS_AUTOSIZE,
//            0, 0, 800, 30,
//            _parentHandle,
//            IntPtr.Zero,
//            _instanceHandle,
//            IntPtr.Zero);

//        SetWindowTheme(_rebarHandle, "Explorer", null);

//        _toolbarHandle = CreateWindowEx(
//            0,
//            "ToolbarWindow32",
//            null,
//            WS_CHILD | WS_VISIBLE | CCS_NOPARENTALIGN | CCS_NORESIZE | CCS_ADJUSTABLE | TBSTYLE_TOOLTIPS,
//            0, 0, 800, 30,
//            _rebarHandle,
//            IntPtr.Zero,
//            _instanceHandle,
//            IntPtr.Zero);

//        // Band hinzufügen
//        var rbBand = new REBARBANDINFO();
//        rbBand.cbSize = Marshal.SizeOf(rbBand);
//        rbBand.fMask = 0x0041; // RBBIM_STYLE | RBBIM_CHILD | RBBIM_CHILDSIZE | RBBIM_SIZE
//        rbBand.fStyle = 0;
//        rbBand.hWndChild = _toolbarHandle;
//        rbBand.cxMinChild = 100;
//        rbBand.cyMinChild = 30;
//        rbBand.cx = 800;

//        SendMessage(_rebarHandle, RB_INSERTBAND, IntPtr.Zero, ref rbBand);

//        // Toolbar initialisieren: Buttons hinzufügen erfolgt später
//    }

//    /// <summary>
//    /// Fügt ein Menüelement hinzu.
//    /// </summary>
//    /// <param name="text">Text mit &amp;-Mnemonic, z.B. "&Datei"</param>
//    /// <param name="hasDropDown">Ob Dropdownmenü (Pfeil) angezeigt wird</param>
//    public int AddMenuItem(string text, bool hasDropDown = false)
//    {
//        int cmdId = _nextCommandId++;
//        var btn = new TBBUTTON
//        {
//            iBitmap = 0,
//            idCommand = cmdId,
//            fsState = 0x04, // TBSTATE_ENABLED
//            fsStyle = 0x00 | (byte)(hasDropDown ? TBSTYLE_DROPDOWN : 0),
//            bReserved0 = 0,
//            bReserved1 = 0,
//            dwData = IntPtr.Zero,
//            iString = Marshal.StringToHGlobalAuto(text)
//        };

//        SendMessage(_toolbarHandle, 0x0414 /* TB_ADDBUTTONS */, (IntPtr)1, ref btn);

//        // Speichere Menüeintrag für ALT + Klick-Logik
//        var mi = new MenuItemData { CommandId = cmdId, Text = text, HasDropDown = hasDropDown };
//        _menuItems[cmdId] = mi;

//        return cmdId;
//    }

//    /// <summary>
//    /// Ruft auf, wenn die Toolbar-Nachrichten abgefangen werden sollen (WM_COMMAND, WM_NOTIFY)
//    /// </summary>
//    public void ProcessWindowMessage(ref Message m)
//    {
//        if (m.Msg == WM_COMMAND)
//        {
//            int cmdId = m.WParam.ToInt32() & 0xFFFF;
//            if (_menuItems.TryGetValue(cmdId, out var item))
//            {
//                MenuItemClicked?.Invoke(this, new MenuClickEventArgs(item));
//                m.Result = IntPtr.Zero;
//            }
//        }
//        else if (m.Msg == WM_NOTIFY)
//        {
//            var nmhdr = Marshal.PtrToStructure<NMHDR>(m.LParam);
//            if (nmhdr.code == TBN_DROPDOWN)
//            {
//                // Dropdown öffnen
//                ShowDropDownMenu(nmhdr.hwndFrom, m.LParam);
//                m.Result = IntPtr.Zero;
//            }
//        }
//        else if (m.Msg == WM_SYSKEYDOWN)
//        {
//            if ((int)m.WParam == (int)Keys.Menu) // ALT Taste gedrückt
//                _altPressed = true;
//        }
//        else if (m.Msg == WM_SYSCHAR)
//        {
//            if (_altPressed)
//            {
//                char ch = (char)m.WParam;
//                HandleAltMnemonic(Char.ToUpper(ch));
//                m.Result = IntPtr.Zero;
//            }
//        }
//    }

//    private void HandleAltMnemonic(char ch)
//    {
//        foreach (var kv in _menuItems)
//        {
//            if (GetMnemonic(kv.Value.Text) == ch)
//            {
//                ActivateMenuButton(kv.Value.CommandId);
//                break;
//            }
//        }
//        _altPressed = false;
//    }

//    private char GetMnemonic(string text)
//    {
//        int i = text.IndexOf('&');
//        if (i >= 0 && i < text.Length - 1)
//            return Char.ToUpper(text[i + 1]);
//        return '\0';
//    }

//    private void ActivateMenuButton(int cmdId)
//    {
//        SendMessage(_toolbarHandle, TB_PRESSBUTTON, (IntPtr)cmdId, (IntPtr)1);
//        // Optional: Dropdown aufklappen bei Dropdown-Buttons
//        if (_menuItems[cmdId].HasDropDown)
//        {
//            // Menü anzeigen (Platzhalter - Du musst TrackPopupMenu hier selbst implementieren)
//        }
//    }

//    private void ShowDropDownMenu(IntPtr hwndFrom, IntPtr lParam)
//    {
//        // Hier TrackPopupMenu aufrufen mit eigenem Kontextmenü
//        // Für Demo einfach ein statisches Beispiel
//        IntPtr hMenu = CreatePopupMenu();
//        AppendMenu(hMenu, MF_STRING, 101, "Unterpunkt 1");
//        AppendMenu(hMenu, MF_STRING, 102, "Unterpunkt 2");

//        GetCursorPos(out POINT pt);
//        TrackPopupMenu(hMenu, TPM_LEFTALIGN | TPM_TOPALIGN, pt.X, pt.Y, 0, _parentHandle, IntPtr.Zero);
//        DestroyMenu(hMenu);
//    }

//    #region WinAPI Zusatz

//    [DllImport("user32.dll")]
//    static extern bool GetCursorPos(out POINT lpPoint);

//    [DllImport("user32.dll", CharSet = CharSet.Auto)]
//    static extern bool AppendMenu(IntPtr hMenu, uint uFlags, uint uIDNewItem, string lpNewItem);

//    private const uint MF_STRING = 0x0000;
//    private const uint TPM_LEFTALIGN = 0x0000;
//    private const uint TPM_TOPALIGN = 0x0010;

//    [StructLayout(LayoutKind.Sequential)]
//    struct POINT
//    {
//        public int X, Y;
//    }

//    #endregion

//    #region IDisposable

//    public void Dispose()
//    {
//        foreach (var mi in _menuItems.Values)
//        {
//            if (mi.Text != null)
//                Marshal.FreeHGlobal(mi.Text);
//        }
//        _menuItems.Clear();
//        this.ReleaseHandle();
//    }

//    #endregion

//    public event EventHandler<MenuClickEventArgs> MenuItemClicked;

//    class MenuItemData
//    {
//        public int CommandId;
//        public string Text;
//        public bool HasDropDown;
//    }
//}

//public class MenuClickEventArgs : EventArgs
//{
//    public string MenuText { get; }
//    public int CommandId { get; }

//    internal MenuClickEventArgs(RebarMenu.MenuItemData mi)
//    {
//        MenuText = mi.Text;
//        CommandId = mi.CommandId;
//    }
//}
