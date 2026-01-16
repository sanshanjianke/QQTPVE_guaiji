/*
 * 由SharpDevelop创建。
 * 用户： sansh
 * 日期: 2026/1/1
 * 时间: 21:58
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using Scripts;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace qqtang_guaji
{
	/// <summary>
	/// Description of Form2.
	/// </summary>
	public partial class Form2 : SubForm
    {
        protected SnailCatchingScript _script;
        public Form2()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
		
		
		void Button1Click(object sender, EventArgs e)
		{
            _script = new Scripts.SnailCatchingScript(_cts.Token);
            _script.MapCount = int.Parse(textBox2.Text);

            // 设置日志动作
            _script.SetLogAction((message) => {
				if (textBox1.InvokeRequired) {
					textBox1.Invoke(new Action<string>((msg) => {
						textBox1.AppendText(msg + Environment.NewLine);
						textBox1.ScrollToCaret();
					}), message);
				} else {
					textBox1.AppendText(message + Environment.NewLine);
					textBox1.ScrollToCaret();
				}
			});

            // 在新线程中运行
            _scriptThread = new Thread(() =>
            {
                try
                {
                    _script.Run();
                }
                catch (OperationCanceledException)
                {
                    // 正常取消，不算错误
                }
            });

            _scriptThread.IsBackground = true;
			_scriptThread.Start();			
		}

        void TextBox1TextChanged(object sender, EventArgs e)
		{
	
		}

		void Label1Click(object sender, EventArgs e)
		{
	
		}
		void Form2Load(object sender, EventArgs e)
		{
	
		}

    }
}
