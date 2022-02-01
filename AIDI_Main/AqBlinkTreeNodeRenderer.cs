using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIDI_Main
{
    public class BlinkTreeNodeRenderer
    {
        private TreeView treeView;
        private System.Threading.Thread th;

        private int blinkRate;
        private bool isBlink = false;

        public BlinkTreeNodeRenderer(TreeView treeView)
        {
            Initialize(treeView, 1);
        }

        public BlinkTreeNodeRenderer(TreeView treeView, int blinkRate)
        {
            Initialize(treeView, blinkRate);
        }

        private void Initialize(TreeView treeView, int blinkRate)
        {
            if (blinkRate < 0 || blinkRate > 1000)
            {
                throw new ArgumentOutOfRangeException("blinkRate", "blinkRate应大于0且小于1000");
            }

            this.treeView = treeView;
            this.blinkRate = blinkRate;

            this.treeView.HideSelection = false;
            this.treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.treeView.DrawNode += new DrawTreeNodeEventHandler(treeView_DrawNode);
            this.treeView.Disposed += new EventHandler(treeView_Disposed);

            this.th = new System.Threading.Thread(new System.Threading.ThreadStart(Blink));
            this.th.IsBackground = true;
            this.th.Start();
        }

        // 一定的时间间隔更改isBlink的值，并使当前选择节点区域无效（不用对整个控件无效），发送绘制消息
        private void Blink()
        {
            int sleepTime = 1000 / blinkRate;
            while (true)
            {
                System.Threading.Thread.Sleep(sleepTime);
                isBlink = !isBlink;
                treeView.BeginInvoke(new MethodInvoker(delegate ()
                {
                    if (treeView.SelectedNode != null && treeView.Focused)
                    {
                        treeView.Invalidate(treeView.SelectedNode.Bounds);
                    }
                }));
            }
        }

        private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //设置为false由用户绘制而非操作系统绘制
            e.DrawDefault = false;
            if ((e.State & TreeNodeStates.Selected) != 0 || (e.State & TreeNodeStates.Focused) != 0)
            {
                //判断节点是否已选择或者已获得焦点，如果是true则绘制高亮显示、焦点框
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds);
                using (Pen focusedPen = new Pen(Color.Black))
                {
                    focusedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusedBounds = e.Bounds;
                    focusedBounds.Size = new Size(e.Bounds.Width - 1, e.Bounds.Height - 1);
                    e.Graphics.DrawRectangle(focusedPen, focusedBounds);
                }
                //节点根据isBlink状态绘制显示文本
                if (isBlink || !treeView.Focused)
                {
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.NodeFont, e.Bounds, Color.White);
                }
            }
            else
            {
                //绘制未被选择的其余节点
                TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.NodeFont, e.Bounds, e.Node.ForeColor);
            }
        }
        private void treeView_Disposed(object sender, EventArgs e)
        {
            th.Abort();
            th = null;
        }
    }


}
