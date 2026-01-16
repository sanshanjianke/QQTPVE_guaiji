/*
 * 由SharpDevelop创建。
 * 用户： sansh
 * 日期: 2026/1/1
 * 时间: 21:53
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
	/// Description of Form1.
	/// </summary>
	public partial class Form1 : Form
	{
        private CancellationToken _cts;


        public Form1()
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
			Scripts.LittleFruitVillage xiaoguo_run = new Scripts.LittleFruitVillage(_cts);
    
			
			xiaoguo_run.WaitTime = int.Parse(textBox2.Text)*1000;
			
			
			
			// 设置日志动作
			xiaoguo_run.SetLogAction((message) => {
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
			System.Threading.Thread scriptThread = new System.Threading.Thread(() => {
				xiaoguo_run.Run();
			});
			
			
			
			scriptThread.IsBackground = true;
			scriptThread.Start();
			
			
			xiaoguo_run.SetLogAction((message) => {
				// 简单的检查 - 如果窗体已关闭，直接返回
				if (this.IsDisposed || textBox1.IsDisposed)
					return;
        
				try {
					if (textBox1.InvokeRequired) {
						textBox1.Invoke(new Action<string>((msg) => {
							// 再次检查
							if (!this.IsDisposed && !textBox1.IsDisposed) {
								textBox1.AppendText(msg + Environment.NewLine);
							}
						}), message);
					} else {
						if (!this.IsDisposed && !textBox1.IsDisposed) {
							textBox1.AppendText(message + Environment.NewLine);
						}
					}
				} catch (ObjectDisposedException) {
					// 忽略已释放的控件异常
				}
			});
		}
		void Button2Click(object sender, EventArgs e)
		{
			
		}
		void TextBox2TextChanged(object sender, EventArgs e)
		{
	
		}
	}
}
