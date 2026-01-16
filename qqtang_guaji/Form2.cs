/*
 * 由SharpDevelop创建。
 * 用户： sansh
 * 日期: 2026/1/1
 * 时间: 21:58
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
	/// Description of Form2.
	/// </summary>
	public partial class Form2 : Form
	{
		


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
			Scripts.SnailCatchingScript woniu_run = new Scripts.SnailCatchingScript();
    
			woniu_run.MapCount= int.Parse(textBox2.Text);
			
			// 设置日志动作
			woniu_run.SetLogAction((message) => {
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
				woniu_run.Run();
			});
			
			
			
			scriptThread.IsBackground = true;
			scriptThread.Start();
			woniu_run.SetLogAction((message) => {
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
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
	
		}
		void Form2FormClosed(object sender, FormClosedEventArgs e)
		{
	
		}
		void Form2FormClosing(object sender, FormClosingEventArgs e)
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
