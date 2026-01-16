using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace InputSimulator
{
    public class DirectInputSimulator
    {

        #region 导入Windows API
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        // 鼠标事件 - 物理鼠标操作
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        // =========== 窗口状态相关API ===========
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWnd);
        #endregion

        #region 常量
        // =========== 窗口消息鼠标操作常量 ===========
        private const uint WM_MOUSEMOVE = 0x0200;
        private const uint WM_LBUTTONDOWN = 0x0201;
        private const uint WM_LBUTTONUP = 0x0202;
        private const uint WM_LBUTTONDBLCLK = 0x0203;
        private const uint WM_RBUTTONDOWN = 0x0204;
        private const uint WM_RBUTTONUP = 0x0205;
        private const uint WM_MBUTTONDOWN = 0x0207;
        private const uint WM_MBUTTONUP = 0x0208;
        private const uint MK_LBUTTON = 0x0001;
        private const uint MK_RBUTTON = 0x0002;
        private const uint MK_MBUTTON = 0x0010;

        // 窗口显示命令常量
        private const int SW_HIDE = 0;              // 隐藏窗口
        private const int SW_SHOWNORMAL = 1;        // 正常显示窗口
        private const int SW_NORMAL = 1;            // 正常显示窗口
        private const int SW_SHOWMINIMIZED = 2;     // 最小化窗口
        private const int SW_SHOWMAXIMIZED = 3;     // 最大化窗口
        private const int SW_MAXIMIZE = 3;          // 最大化窗口
        private const int SW_SHOWNOACTIVATE = 4;    // 显示窗口但不激活
        private const int SW_SHOW = 5;              // 显示窗口
        private const int SW_MINIMIZE = 6;          // 最小化窗口
        private const int SW_SHOWMINNOACTIVE = 7;   // 最小化窗口但不激活
        private const int SW_SHOWNA = 8;            // 显示窗口但不激活
        private const int SW_RESTORE = 9;           // 恢复窗口
        private const int SW_SHOWDEFAULT = 10;      // 默认显示窗口
        private const int SW_FORCEMINIMIZE = 11;    // 强制最小化窗口
        #endregion

        // 结构体定义
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width => Right - Left;
            public int Height => Bottom - Top;
        }

        // 委托定义
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // 鼠标事件常量 - 物理鼠标
        private const uint MOUSEEVENTF_DOWN = 0x0002;
        private const uint MOUSEEVENTF_UP = 0x0004;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        // 窗口查找相关
        private const int MAX_WINDOW_TEXT_LENGTH = 256;

        // 单例实例
        private static DirectInputSimulator _instance;
        public static DirectInputSimulator Instance => _instance ?? (_instance = new DirectInputSimulator());

        // 当前目标窗口句柄
        private IntPtr _targetWindowHandle = IntPtr.Zero;

        // 目标窗口部分标题
        private string _targetWindowPartialTitle = "QQTPVE";

        // 错误处理委托
        public Action<string> ErrorHandler { get; set; }

        // 鼠标操作模式
        public enum MouseMode
        {
            Physical,    // 物理鼠标操作
            PostMessage, // 窗口消息（支持最小化）
            SendMessage  // 窗口消息（同步）
        }

        private MouseMode _mouseMode = MouseMode.PostMessage;

        // 默认构造函数
        private DirectInputSimulator()
        {
            // 默认错误处理：显示消息框
            ErrorHandler = message => MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // 重要构造函数
        private CancellationToken _token;
        public DirectInputSimulator(CancellationToken token)
        {
            // 默认错误处理：显示消息框
            this._token = token;
            ErrorHandler = message => MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // 设置鼠标操作模式
        public void SetMouseMode(MouseMode mode)
        {
            _mouseMode = mode;
        }

        // 获取当前鼠标模式
        public MouseMode GetMouseMode()
        {
            return _mouseMode;
        }

        // ==================== 窗口状态判断函数 ====================

        /// <summary>
        /// 判断窗口是否是最小化状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>true: 窗口是最小化状态; false: 窗口不是最小化状态</returns>
        public bool IsWindowMinimized(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
            {
                ShowError("窗口句柄无效");
                return false;
            }

            if (!IsWindow(hWnd))
            {
                ShowError("指定的窗口不存在");
                return false;
            }

            return IsIconic(hWnd);
        }

        /// <summary>
        /// 判断窗口是否是最大化状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>true: 窗口是最大化状态; false: 窗口不是最大化状态</returns>
        public bool IsWindowMaximized(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
            {
                ShowError("窗口句柄无效");
                return false;
            }

            if (!IsWindow(hWnd))
            {
                ShowError("指定的窗口不存在");
                return false;
            }

            return IsZoomed(hWnd);
        }

        /// <summary>
        /// 判断窗口是否可见
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>true: 窗口可见; false: 窗口不可见</returns>
        public bool IsWindowVisibleState(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
            {
                ShowError("窗口句柄无效");
                return false;
            }

            if (!IsWindow(hWnd))
            {
                ShowError("指定的窗口不存在");
                return false;
            }

            return IsWindowVisible(hWnd);
        }

        /// <summary>
        /// 获取窗口状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>字符串描述窗口状态：最小化/最大化/正常/隐藏/无效</returns>
        public string GetWindowState(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
            {
                return "窗口句柄无效";
            }

            if (!IsWindow(hWnd))
            {
                return "指定的窗口不存在";
            }

            if (!IsWindowVisible(hWnd))
            {
                return "隐藏";
            }

            if (IsIconic(hWnd))
            {
                return "最小化";
            }

            if (IsZoomed(hWnd))
            {
                return "最大化";
            }

            return "正常";
        }

        /// <summary>
        /// 将窗口最小化（如果窗口不是最小化状态）
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>true: 操作成功; false: 操作失败</returns>
        public bool MinimizeWindowIfNotMinimized(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
            {
                ShowError("窗口句柄无效");
                return false;
            }

            if (!IsWindow(hWnd))
            {
                ShowError("指定的窗口不存在");
                return false;
            }

            // 如果窗口已经最小化，直接返回成功
            if (IsIconic(hWnd))
            {
                Debug.WriteLine($"窗口已是最小化状态，无需操作");
                return true;
            }

            // 最小化窗口
            bool result = ShowWindow(hWnd, SW_MINIMIZE);

            if (result)
            {
                Debug.WriteLine($"窗口最小化成功");
                // 等待窗口最小化完成
                _token.WaitHandle.WaitOne(100);
            }
            else
            {
                ShowError($"窗口最小化失败");
            }

            return result;
        }

        /// <summary>
        /// 最小化当前目标窗口（如果窗口不是最小化状态）
        /// </summary>
        /// <returns>true: 操作成功; false: 操作失败</returns>
        public bool MinimizeTargetWindowIfNotMinimized()
        {
            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口");
                return false;
            }

            return MinimizeWindowIfNotMinimized(hWnd);
        }

        /// <summary>
        /// 恢复窗口（如果窗口是最小化状态）
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>true: 操作成功; false: 操作失败</returns>
        public bool RestoreWindowIfMinimized(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
            {
                ShowError("窗口句柄无效");
                return false;
            }

            if (!IsWindow(hWnd))
            {
                ShowError("指定的窗口不存在");
                return false;
            }

            // 如果窗口不是最小化状态，直接返回成功
            if (!IsIconic(hWnd))
            {
                Debug.WriteLine($"窗口不是最小化状态，无需恢复");
                return true;
            }

            // 恢复窗口
            bool result = ShowWindow(hWnd, SW_RESTORE);

            if (result)
            {
                Debug.WriteLine($"窗口恢复成功");
                // 等待窗口恢复完成
                _token.WaitHandle.WaitOne(100);
            }
            else
            {
                ShowError($"窗口恢复失败");
            }

            return result;
        }

        /// <summary>
        /// 显示或隐藏窗口
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="show">true: 显示窗口; false: 隐藏窗口</param>
        /// <returns>true: 操作成功; false: 操作失败</returns>
        public bool ShowWindowState(IntPtr hWnd, bool show)
        {
            if (hWnd == IntPtr.Zero)
            {
                ShowError("窗口句柄无效");
                return false;
            }

            if (!IsWindow(hWnd))
            {
                ShowError("指定的窗口不存在");
                return false;
            }

            int nCmdShow = show ? SW_SHOW : SW_HIDE;
            bool result = ShowWindow(hWnd, nCmdShow);

            if (result)
            {
                Debug.WriteLine($"窗口{(show ? "显示" : "隐藏")}成功");
                _token.WaitHandle.WaitOne(100);
            }
            else
            {
                ShowError($"窗口{(show ? "显示" : "隐藏")}失败");
            }

            return result;
        }

        // ==================== 辅助方法 ====================

        // 将客户端坐标转换为LParam
        private int MakeLParam(int x, int y)
        {
            return (y << 16) | (x & 0xFFFF);
        }

        // 安全的UInt到IntPtr转换
        private IntPtr UIntToIntPtr(uint value)
        {
            unchecked
            {
                return (IntPtr)(int)value;
            }
        }

        // 显示错误
        private void ShowError(string message)
        {
            ErrorHandler?.Invoke(message);
        }

        // ==================== 窗口查找方法 ====================

        // 设置目标窗口
        public void SetTargetWindow(string partialTitle)
        {
            _targetWindowPartialTitle = partialTitle;
            _targetWindowHandle = FindWindowByPartialTitle(partialTitle);

            if (_targetWindowHandle == IntPtr.Zero)
            {
                ShowError($"未找到包含 '{partialTitle}' 的窗口");
            }
            else
            {
                string windowTitle = GetWindowText(_targetWindowHandle);
                Debug.WriteLine($"找到目标窗口: {windowTitle}, 句柄: {_targetWindowHandle:X}");
            }
        }

        // 获取目标窗口句柄（如果未找到则重新查找）
        private IntPtr GetTargetWindow()
        {
            if (_targetWindowHandle == IntPtr.Zero && !string.IsNullOrEmpty(_targetWindowPartialTitle))
            {
                _targetWindowHandle = FindWindowByPartialTitle(_targetWindowPartialTitle);
            }
            return _targetWindowHandle;
        }

        // 查找包含部分标题的窗口
        public IntPtr FindWindowByPartialTitle(string partialTitle)
        {
            List<IntPtr> windowHandles = new List<IntPtr>();

            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    StringBuilder windowTitle = new StringBuilder(MAX_WINDOW_TEXT_LENGTH);
                    int titleLength = GetWindowText(hWnd, windowTitle, MAX_WINDOW_TEXT_LENGTH);

                    if (titleLength > 0)
                    {
                        string title = windowTitle.ToString();
                        if (title.IndexOf(partialTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            windowHandles.Add(hWnd);
                        }
                    }
                }
                return true;
            }, IntPtr.Zero);

            return windowHandles.Count > 0 ? windowHandles[0] : IntPtr.Zero;
        }

        // 获取窗口标题
        private string GetWindowText(IntPtr hWnd)
        {
            StringBuilder windowText = new StringBuilder(MAX_WINDOW_TEXT_LENGTH);
            GetWindowText(hWnd, windowText, MAX_WINDOW_TEXT_LENGTH);
            return windowText.ToString();
        }

        // 激活窗口
        public bool ActivateWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return false;
            return SetForegroundWindow(hWnd);
        }

        // 激活窗口（通过部分标题）
        public bool ActivateWindow(string partialTitle = null)
        {
            if (!string.IsNullOrEmpty(partialTitle))
            {
                SetTargetWindow(partialTitle);
            }

            IntPtr hWnd = GetTargetWindow();
            return ActivateWindow(hWnd);
        }

        // ==================== 鼠标操作方法（物理） ====================

        // 获取当前鼠标位置
        public void GetMousePosition(out int x, out int y)
        {
            POINT point;
            GetCursorPos(out point);
            x = point.X;
            y = point.Y;
        }

        // 移动鼠标到绝对位置（物理）
        public void MoveMouseTo(int x, int y)
        {
            SetCursorPos(x, y);
        }

        // 移动鼠标到窗口内位置（物理）
        public void MoveMouseToWindowPosition(int windowX, int windowY)
        {
            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口");
                return;
            }

            RECT windowRect;
            if (GetWindowRect(hWnd, out windowRect))
            {
                MoveMouseTo(windowRect.Left + windowX, windowRect.Top + windowY);
            }
        }

        // 移动鼠标到客户端区域位置（物理）
        public void MoveMouseToClientPosition(int clientX, int clientY)
        {
            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口");
                return;
            }

            POINT point = new POINT { X = clientX, Y = clientY };
            if (ClientToScreen(hWnd, ref point))
            {
                MoveMouseTo(point.X, point.Y);
            }
        }

        // 移动鼠标到客户端区域位置（通过部分标题，物理）
        public void MoveMouseToClientPositionByPartialTitle(string partialTitle, int clientX, int clientY)
        {
            SetTargetWindow(partialTitle);

            if (_mouseMode == MouseMode.Physical)
            {
                // 物理鼠标移动
                MoveMouseToClientPosition(clientX, clientY);
            }
            else
            {
                // 窗口消息移动（支持最小化）
                IntPtr hWnd = GetTargetWindow();
                if (hWnd == IntPtr.Zero)
                {
                    ShowError("未找到目标窗口");
                    return;
                }

                int lParam = MakeLParam(clientX, clientY);
                if (_mouseMode == MouseMode.PostMessage)
                {
                    PostMessage(hWnd, WM_MOUSEMOVE, IntPtr.Zero, (IntPtr)lParam);
                }
                else
                {
                    SendMessage(hWnd, WM_MOUSEMOVE, IntPtr.Zero, (IntPtr)lParam);
                }
            }
        }

        // 鼠标点击（物理）
        public void ClickMouse(int clickCount = 1, int delayBetweenClicks = 50)
        {
            if (_mouseMode == MouseMode.Physical)
            {
                // 物理鼠标点击
                for (int i = 0; i < clickCount; i++)
                {
                    mouse_event(MOUSEEVENTF_DOWN, 0, 0, 0, UIntPtr.Zero);
                    _token.WaitHandle.WaitOne(delayBetweenClicks);
                    mouse_event(MOUSEEVENTF_UP, 0, 0, 0, UIntPtr.Zero);
                    _token.WaitHandle.WaitOne(delayBetweenClicks);
                }
            }
            else
            {
                // 窗口消息点击（在当前位置）
                IntPtr hWnd = GetTargetWindow();
                if (hWnd == IntPtr.Zero)
                {
                    ShowError("未找到目标窗口");
                    return;
                }

                // 获取当前鼠标位置
                GetMousePosition(out int x, out int y);

                // 转换为客户端坐标（如果需要的话）
                // 这里假设x,y已经是客户端坐标或我们可以直接使用

                for (int i = 0; i < clickCount; i++)
                {
                    int lParam = MakeLParam(x, y);

                    if (_mouseMode == MouseMode.PostMessage)
                    {
                        PostMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, (IntPtr)lParam);
                        _token.WaitHandle.WaitOne(delayBetweenClicks);
                        PostMessage(hWnd, WM_LBUTTONUP, IntPtr.Zero, (IntPtr)lParam);
                    }
                    else
                    {
                        SendMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, (IntPtr)lParam);
                        _token.WaitHandle.WaitOne(delayBetweenClicks);
                        SendMessage(hWnd, WM_LBUTTONUP, IntPtr.Zero, (IntPtr)lParam);
                    }

                    _token.WaitHandle.WaitOne(delayBetweenClicks);
                }
            }
        }

        // ==================== 鼠标操作方法（窗口消息） ====================

        // 发送鼠标移动消息（支持最小化）
        public void SendMouseMove(int clientX, int clientY)
        {
            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口");
                return;
            }

            int lParam = MakeLParam(clientX, clientY);

            if (_mouseMode == MouseMode.PostMessage)
            {
                PostMessage(hWnd, WM_MOUSEMOVE, IntPtr.Zero, (IntPtr)lParam);
            }
            else
            {
                SendMessage(hWnd, WM_MOUSEMOVE, IntPtr.Zero, (IntPtr)lParam);
            }
        }

        // 发送鼠标左键点击消息（支持最小化）
        public void SendLeftClick(int clientX, int clientY, int delayBetweenEvents = 50)
        {
            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口");
                return;
            }

            int lParam = MakeLParam(clientX, clientY);

            // 发送鼠标移动消息
            SendMouseMove(clientX, clientY);
            _token.WaitHandle.WaitOne(10);

            // 发送鼠标左键按下消息
            if (_mouseMode == MouseMode.PostMessage)
            {
                PostMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, (IntPtr)lParam);
                _token.WaitHandle.WaitOne(delayBetweenEvents);
                PostMessage(hWnd, WM_LBUTTONUP, IntPtr.Zero, (IntPtr)lParam);
            }
            else
            {
                SendMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, (IntPtr)lParam);
                _token.WaitHandle.WaitOne(delayBetweenEvents);
                SendMessage(hWnd, WM_LBUTTONUP, IntPtr.Zero, (IntPtr)lParam);
            }
        }

        // 发送鼠标右键点击消息（支持最小化）
        public void SendRightClick(int clientX, int clientY, int delayBetweenEvents = 50)
        {
            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口");
                return;
            }

            int lParam = MakeLParam(clientX, clientY);

            // 发送鼠标移动消息
            SendMouseMove(clientX, clientY);
            _token.WaitHandle.WaitOne(10);

            // 发送鼠标右键按下和释放消息
            if (_mouseMode == MouseMode.PostMessage)
            {
                PostMessage(hWnd, WM_RBUTTONDOWN, (IntPtr)MK_RBUTTON, (IntPtr)lParam);
                _token.WaitHandle.WaitOne(delayBetweenEvents);
                PostMessage(hWnd, WM_RBUTTONUP, IntPtr.Zero, (IntPtr)lParam);
            }
            else
            {
                SendMessage(hWnd, WM_RBUTTONDOWN, (IntPtr)MK_RBUTTON, (IntPtr)lParam);
                _token.WaitHandle.WaitOne(delayBetweenEvents);
                SendMessage(hWnd, WM_RBUTTONUP, IntPtr.Zero, (IntPtr)lParam);
            }
        }

        // 发送鼠标双击消息（支持最小化）
        public void SendDoubleClick(int clientX, int clientY)
        {
            SendLeftClick(clientX, clientY, 30);
            _token.WaitHandle.WaitOne(10);
            SendLeftClick(clientX, clientY, 30);
        }

        // 发送鼠标中键点击消息（支持最小化）
        public void SendMiddleClick(int clientX, int clientY, int delayBetweenEvents = 50)
        {
            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口");
                return;
            }

            int lParam = MakeLParam(clientX, clientY);

            // 发送鼠标移动消息
            SendMouseMove(clientX, clientY);
            _token.WaitHandle.WaitOne(10);

            // 发送鼠标中键按下和释放消息
            if (_mouseMode == MouseMode.PostMessage)
            {
                PostMessage(hWnd, WM_MBUTTONDOWN, (IntPtr)MK_MBUTTON, (IntPtr)lParam);
                _token.WaitHandle.WaitOne(delayBetweenEvents);
                PostMessage(hWnd, WM_MBUTTONUP, IntPtr.Zero, (IntPtr)lParam);
            }
            else
            {
                SendMessage(hWnd, WM_MBUTTONDOWN, (IntPtr)MK_MBUTTON, (IntPtr)lParam);
                _token.WaitHandle.WaitOne(delayBetweenEvents);
                SendMessage(hWnd, WM_MBUTTONUP, IntPtr.Zero, (IntPtr)lParam);
            }
        }

        // 连续点击指定位置（支持最小化）
        public void ContinuousClick(int clientX, int clientY, int count = 10, int interval = 100)
        {
            for (int i = 0; i < count; i++)
            {
                SendLeftClick(clientX, clientY, 30);
                _token.WaitHandle.WaitOne(interval);
            }
        }

        // ==================== 键盘操作方法（向窗口发送消息） ====================

        // 构建lParam参数
        private uint BuildLParam(uint scanCode, bool isExtended, bool isKeyUp)
        {
            uint lParam = 0x0001u; // 重复次数为1

            // 扫描码（第16-23位）
            lParam |= (scanCode << 16);

            // 扩展键标志（第24位）
            if (isExtended)
                lParam |= (1u << 24);

            // 如果是释放键
            if (isKeyUp)
            {
                // 设置转换状态（第31位）和先前状态（第30位）
                lParam |= (1u << 31) | (1u << 30);
            }

            return lParam;
        }

        // 按下按键
        public void Press(string key)
        {
            if (!KeyConfig.KeyScanCodes.ContainsKey(key.ToLower()))
            {
                ShowError($"不支持的按键: {key}");
                return;
            }

            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口，无法发送按键");
                return;
            }

            SendKeyToWindow(hWnd, key);
            _token.WaitHandle.WaitOne(50);
        }

        // 向指定窗口发送按键
        private void SendKeyToWindow(IntPtr hWnd, string key)
        {
            key = key.ToLower();

            if (!KeyConfig.KeyVirtualCodes.TryGetValue(key, out ushort virtualKey))
            {
                ShowError($"未找到按键 '{key}' 的虚拟键码");
                return;
            }

            if (!KeyConfig.KeyScanCodes.TryGetValue(key, out ushort scanCode))
            {
                ShowError($"未找到按键 '{key}' 的扫描码");
                return;
            }

            bool isExtended = KeyConfig.ExtendedKeys.Contains(key);

            // 构建lParam参数
            uint lParamDown = BuildLParam(scanCode, isExtended, false);
            uint lParamUp = BuildLParam(scanCode, isExtended, true);

            // 使用安全的IntPtr转换方法
            IntPtr wParam = (IntPtr)virtualKey;
            IntPtr lParamDownPtr = UIntToIntPtr(lParamDown);
            IntPtr lParamUpPtr = UIntToIntPtr(lParamUp);

            // 发送按键按下消息
            SendMessage(hWnd, KeyConfig.WM_KEYDOWN, wParam, lParamDownPtr);
            _token.WaitHandle.WaitOne(20);

            // 发送按键释放消息
            SendMessage(hWnd, KeyConfig.WM_KEYUP, wParam, lParamUpPtr);
        }

        // 按住按键一段时间
        public void Hold(string key, int milliseconds)
        {
            if (!KeyConfig.KeyScanCodes.ContainsKey(key.ToLower()))
            {
                ShowError($"不支持的按键: {key}");
                return;
            }

            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口，无法发送按键");
                return;
            }

            key = key.ToLower();
            SendKeyDown(hWnd, key);
            _token.WaitHandle.WaitOne(milliseconds);
            SendKeyUp(hWnd, key);
            _token.ThrowIfCancellationRequested();
        }

        // 发送按键按下
        private void SendKeyDown(IntPtr hWnd, string key)
        {
            if (!KeyConfig.KeyVirtualCodes.TryGetValue(key, out ushort virtualKey) ||
                !KeyConfig.KeyScanCodes.TryGetValue(key, out ushort scanCode))
                return;

            bool isExtended = KeyConfig.ExtendedKeys.Contains(key);
            uint lParam = BuildLParam(scanCode, isExtended, false);

            IntPtr wParam = (IntPtr)virtualKey;
            IntPtr lParamPtr = UIntToIntPtr(lParam);

            SendMessage(hWnd, KeyConfig.WM_KEYDOWN, wParam, lParamPtr);
        }

        // 发送按键释放
        private void SendKeyUp(IntPtr hWnd, string key)
        {
            if (!KeyConfig.KeyVirtualCodes.TryGetValue(key, out ushort virtualKey) ||
                !KeyConfig.KeyScanCodes.TryGetValue(key, out ushort scanCode))
                return;

            bool isExtended = KeyConfig.ExtendedKeys.Contains(key);
            uint lParam = BuildLParam(scanCode, isExtended, true);

            IntPtr wParam = (IntPtr)virtualKey;
            IntPtr lParamPtr = UIntToIntPtr(lParam);

            SendMessage(hWnd, KeyConfig.WM_KEYUP, wParam, lParamPtr);
        }

        // 多次移动（连续按键）
        public void MoveMultiple(string direction, int count, int holdTimePerMove = 155)
        {
            for (int i = 0; i < count; i++)
            {
                Hold(direction, holdTimePerMove);
            }
        }

        // 使用PostMessage发送键盘消息（异步方式）
        public void PressPost(string key)
        {
            if (!KeyConfig.KeyScanCodes.ContainsKey(key.ToLower()))
            {
                ShowError($"不支持的按键: {key}");
                return;
            }

            IntPtr hWnd = GetTargetWindow();
            if (hWnd == IntPtr.Zero)
            {
                ShowError("未找到目标窗口，无法发送按键");
                return;
            }

            key = key.ToLower();

            if (!KeyConfig.KeyVirtualCodes.TryGetValue(key, out ushort virtualKey) ||
                !KeyConfig.KeyScanCodes.TryGetValue(key, out ushort scanCode))
                return;

            bool isExtended = KeyConfig.ExtendedKeys.Contains(key);

            // 构建lParam参数
            uint lParamDown = BuildLParam(scanCode, isExtended, false);
            uint lParamUp = BuildLParam(scanCode, isExtended, true);

            // 发送按键消息（异步）
            PostMessage(hWnd, KeyConfig.WM_KEYDOWN, (IntPtr)virtualKey, UIntToIntPtr(lParamDown));
            _token.WaitHandle.WaitOne(20);
            PostMessage(hWnd, KeyConfig.WM_KEYUP, (IntPtr)virtualKey, UIntToIntPtr(lParamUp));
        }

        // ==================== 辅助方法 ====================

        // 等待
        public void Wait(int milliseconds)
        {
            _token.WaitHandle.WaitOne(milliseconds);
            _token.ThrowIfCancellationRequested();
        }

        // 获取窗口句柄（通过部分标题）- 公开方法
        public IntPtr GetWindowHandleByPartialTitle(string partialTitle)
        {
            return FindWindowByPartialTitle(partialTitle);
        }

        // 获取当前活动窗口句柄
        public IntPtr GetActiveWindowHandle()
        {
            return GetForegroundWindow();
        }

        // ==================== 测试方法 ====================

        // 快速测试多种点击方法
        public void QuickTestMultipleMethods(IntPtr hWnd, int[] testPoints = null)
        {
            if (hWnd == IntPtr.Zero)
            {
                ShowError("无效的窗口句柄");
                return;
            }

            _targetWindowHandle = hWnd;

            Console.WriteLine("快速测试多种点击方法");

            // 如果没有提供测试点，使用默认的
            if (testPoints == null || testPoints.Length < 2)
            {
                testPoints = new int[] { 919, 497, 100, 100, 500, 100, 900, 100, 100, 300, 500, 300, 900, 300, 100, 500, 500, 500, 900, 500 };
            }

            int pointCount = testPoints.Length / 2;

            Console.WriteLine($"\n测试 {pointCount} 个点:");

            for (int i = 0; i < pointCount; i++)
            {
                int clientX = testPoints[i * 2];
                int clientY = testPoints[i * 2 + 1];
                Console.WriteLine($"  {i + 1}. ({clientX}, {clientY})");
            }

            Console.WriteLine("\n=== 测试PostMessage方法 ===");

            for (int i = 0; i < pointCount; i++)
            {
                int clientX = testPoints[i * 2];
                int clientY = testPoints[i * 2 + 1];
                Console.WriteLine($"\n点 {i + 1}: ({clientX}, {clientY})");

                // 发送点击事件
                SendLeftClick(clientX, clientY, 50);

                // 等待用户观察
                Console.Write("  观察游戏反应，按回车继续...");
                Console.ReadLine();
            }

            Console.WriteLine("\n所有测试完成!");
        }
    }
}
