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
	public partial class Form1 : SubForm
    {


        public Form1()
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

            Scripts.LittleFruitVillage script = new Scripts.LittleFruitVillage(_cts.Token);
            script.WaitTime = (int)(Convert.ToDouble(textBox2.Text) * 1000);

            // 设置日志动作
            script.SetLogAction((message) => {
                if (textBox1.InvokeRequired)
                {
                    textBox1.Invoke(new Action<string>((msg) => {
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
            Thread scriptThread = new Thread(() =>
            {
                try
                {
                    script.Run();
                }
                catch (OperationCanceledException)
                {
                    // 正常取消，不算错误
                }
            });



            scriptThread.IsBackground = true;
			scriptThread.Start();

		}
		void Button2Click(object sender, EventArgs e)
		{
			
		}
		void TextBox2TextChanged(object sender, EventArgs e)
		{
	
		}
	}
}
