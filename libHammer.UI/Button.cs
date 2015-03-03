using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libHammer.UI
{
    public partial class Button: UserControl
    {
        #region Properties

        #endregion

        public Button()
        {
            this.DoubleBuffered = true;
            this.Text = "Untitled";
            
            InitializeComponent();
        }

        /// <summary>
        /// Render the button
        /// </summary>
        /// <param name="args"></param>
        protected override void OnPaint(PaintEventArgs args)
        {
            /* Calling the base OnPaint method. */
            base.OnPaint(args);

            Brush brush = new System.Drawing.SolidBrush(this.BackColor);
            args.Graphics.FillRectangle(brush, ClientRectangle);
            brush.Dispose();

            System.Drawing.SolidBrush drawBrush = new
                System.Drawing.SolidBrush(this.ForeColor);
            args.Graphics.DrawString(this.Text, this.Font, drawBrush, 0, 0);
            
        }
    }
}
