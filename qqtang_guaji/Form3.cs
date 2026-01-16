/*
 * 由SharpDevelop创建。
 * 用户： sansh
 * 日期: 2026/1/1
 * 时间: 22:01
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace qqtang_guaji
{
    /// <summary>
    /// Description of Form3.
    /// </summary>
    public partial class Form3 : Form
    {
        private CancellationToken _cts;
        public Form3()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            _cts = new CancellationToken();
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        void Button1Click(object sender, EventArgs e)
        {
            Scripts.SeasonCountry seasonCountry = new Scripts.SeasonCountry(_cts);

            seasonCountry.WaitTime = int.Parse(textBox2.Text) * 1000;

            // 设置日志动作
            seasonCountry.SetLogAction((message) =>
            {
                if (textBox1.InvokeRequired)
                {
                    textBox1.Invoke(new Action<string>((msg) =>
                    {
                        textBox1.AppendText(msg + Environment.NewLine);
                        textBox1.ScrollToCaret();
                    }), message);
                }
                else
                {
                    textBox1.AppendText(message + Environment.NewLine);
                    textBox1.ScrollToCaret();
                }
            });

            // 在新线程中运行
            System.Threading.Thread scriptThread = new System.Threading.Thread(() =>
            {
                seasonCountry.Run();
            });


            scriptThread.IsBackground = true;
            scriptThread.Start();

            seasonCountry.SetLogAction((message) =>
            {
                // 简单的检查 - 如果窗体已关闭，直接返回
                if (this.IsDisposed || textBox1.IsDisposed)
                    return;

                try
                {
                    if (textBox1.InvokeRequired)
                    {
                        textBox1.Invoke(new Action<string>((msg) =>
                        {
                            // 再次检查
                            if (!this.IsDisposed && !textBox1.IsDisposed)
                            {
                                textBox1.AppendText(msg + Environment.NewLine);
                            }
                        }), message);
                    }
                    else
                    {
                        if (!this.IsDisposed && !textBox1.IsDisposed)
                        {
                            textBox1.AppendText(message + Environment.NewLine);
                        }
                    }
                }
                catch (ObjectDisposedException)
                {
                    // 忽略已释放的控件异常
                }
            });
        }
    }
}
