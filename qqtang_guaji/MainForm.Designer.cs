/*
 * 由SharpDevelop创建。
 * 用户： sansh
 * 日期: 2026/1/1
 * 时间: 21:44
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace qqtang_guaji
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 56);
            this.button1.TabIndex = 0;
            this.button1.Text = "抓蜗牛";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(31, 155);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 57);
            this.button2.TabIndex = 1;
            this.button2.Text = "小果村";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(31, 265);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 55);
            this.button3.TabIndex = 2;
            this.button3.Text = "四季之国";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(152, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 86);
            this.label2.TabIndex = 3;
            this.label2.Text = "捉蜗牛刷经验刷材料挂机：用于 捉蜗牛 地图的挂机脚本,注意关闭道具赛。地图作者名「残冰」。p.s. 4500以下血会暴毙，没法刷材料，只能刷经验。4500以上可以" +
    "刷材料。";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(152, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 104);
            this.label3.TabIndex = 4;
            this.label3.Text = "小果村刷材料：用于 小果村 地图的挂机脚本。地图作者「天璇」。注意关闭道具赛，注意输入法切换到英文模式。ps:需要6500血，因为要放9个泡。6500以下请刷抓蜗" +
    "牛。注意，有时候会因为电脑卡造成判定不准。";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(150, 265);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(270, 78);
            this.label4.TabIndex = 5;
            this.label4.Text = "四季之国更快刷材料：地图作者「」该模式需要将道具栏7放入鸭子，本模式需要使用道具鸭子和起爆器，请携带大量鸭子和起爆器入场，另本模式需要至少6000血，因为要放8个" +
    "泡。";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(31, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(344, 38);
            this.label5.TabIndex = 6;
            this.label5.Text = "外挂是QQ堂一大特色，不可不尝";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(29, 369);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(367, 112);
            this.label7.TabIndex = 8;
            this.label7.Text = "服务器资源有限，请晚上再使用挂机，早上8:00到凌晨0:00只能用30分钟。并选择人少的服挂机。";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 487);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "挂机脚本";
            this.ResumeLayout(false);

		}
	}
}
