using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InputSimulator
{
    // 按键配置类
    public static class KeyConfig
    {
        // 扫描码映射
        public static Dictionary<string, ushort> KeyScanCodes = new Dictionary<string, ushort>
        {
            {"space", 0x39},
            {"a", 0x1E},
            {"b", 0x30},
            {"c", 0x2E},
            {"d", 0x20},
            {"e", 0x12},
            {"f", 0x21},
            {"g", 0x22},
            {"h", 0x23},
            {"i", 0x17},
            {"j", 0x24},
            {"k", 0x25},
            {"l", 0x26},
            {"m", 0x32},
            {"n", 0x31},
            {"o", 0x18},
            {"p", 0x19},
            {"q", 0x10},
            {"r", 0x13},
            {"s", 0x1F},
            {"t", 0x14},
            {"u", 0x16},
            {"v", 0x2F},
            {"w", 0x11},
            {"x", 0x2D},
            {"y", 0x15},
            {"z", 0x2C},
            {"0", 0x0B},
            {"1", 0x02},
            {"2", 0x03},
            {"3", 0x04},
            {"4", 0x05},
            {"5", 0x06},
            {"6", 0x07},
            {"7", 0x08},
            {"8", 0x09},
            {"9", 0x0A},
            {"enter", 0x1C},
            {"esc", 0x01},
            {"backspace", 0x0E},
            {"tab", 0x0F},
            {"ctrl", 0x1D},
            {"alt", 0x38},
            {"shift", 0x2A},
            {"windows", 0x5B},
            {"up", 0x48},
            {"down", 0x50},
            {"left", 0x4B},
            {"right", 0x4D},
            {"f1", 0x3B},
            {"f2", 0x3C},
            {"f3", 0x3D},
            {"f4", 0x3E},
            {"f5", 0x3F},
            {"f6", 0x40},
            {"f7", 0x41},
            {"f8", 0x42},
            {"f9", 0x43},
            {"f10", 0x44},
            {"f11", 0x57},
            {"f12", 0x58},
        };

        // 虚拟键码映射
        public static Dictionary<string, ushort> KeyVirtualCodes = new Dictionary<string, ushort>
        {
            {"space", 0x20},
            {"a", 0x41},
            {"b", 0x42},
            {"c", 0x43},
            {"d", 0x44},
            {"e", 0x45},
            {"f", 0x46},
            {"g", 0x47},
            {"h", 0x48},
            {"i", 0x49},
            {"j", 0x4A},
            {"k", 0x4B},
            {"l", 0x4C},
            {"m", 0x4D},
            {"n", 0x4E},
            {"o", 0x4F},
            {"p", 0x50},
            {"q", 0x51},
            {"r", 0x52},
            {"s", 0x53},
            {"t", 0x54},
            {"u", 0x55},
            {"v", 0x56},
            {"w", 0x57},
            {"x", 0x58},
            {"y", 0x59},
            {"z", 0x5A},
            {"0", 0x30},
            {"1", 0x31},
            {"2", 0x32},
            {"3", 0x33},
            {"4", 0x34},
            {"5", 0x35},
            {"6", 0x36},
            {"7", 0x37},
            {"8", 0x38},
            {"9", 0x39},
            {"enter", 0x0D},
            {"esc", 0x1B},
            {"backspace", 0x08},
            {"tab", 0x09},
            {"ctrl", 0x11},
            {"alt", 0x12},
            {"shift", 0x10},
            {"windows", 0x5B},
            {"up", 0x26},
            {"down", 0x28},
            {"left", 0x25},
            {"right", 0x27},
            {"f1", 0x70},
            {"f2", 0x71},
            {"f3", 0x72},
            {"f4", 0x73},
            {"f5", 0x74},
            {"f6", 0x75},
            {"f7", 0x76},
            {"f8", 0x77},
            {"f9", 0x78},
            {"f10", 0x79},
            {"f11", 0x7A},
            {"f12", 0x7B},
        };

        // 扩展键列表（需要特殊处理的键）
        public static HashSet<string> ExtendedKeys = new HashSet<string>
        {
            "up", "down", "left", "right", "enter", "shift", "ctrl", "alt"
        };

        // 窗口消息常量
        public const uint WM_KEYDOWN = 0x0100;
        public const uint WM_KEYUP = 0x0101;
        public const uint WM_CHAR = 0x0102;
    }
}
