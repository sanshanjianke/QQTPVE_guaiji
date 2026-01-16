using Scripts;
using System;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace qqtang_guaji
{
    public partial class SubForm : Form
    {
        protected CancellationTokenSource _cts;
        
        public SubForm()
        {
            FormClosing += SubForm_FormClosing;
            _cts = new CancellationTokenSource();
        }

        protected virtual void SubForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();   // 通知脚本停止
            }
        }

    }
}
