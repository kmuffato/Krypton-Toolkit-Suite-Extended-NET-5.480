﻿#region BSD License
/*
 * Use of this source code is governed by a BSD-style
 * license that can be found in the LICENSE.md file or at
 * https://github.com/Wagnerp/Krypton-Toolkit-Suite-Extended-NET-5.480/blob/master/LICENSE
 *
 */
#endregion

using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ExtendedControls.ExtendedToolkit.Wizard.Classes
{
    /// <summary>
    /// Summary description for WizardDesigner.
    /// </summary>
    public class WizardDesigner : ParentControlDesigner
    {
        #region old WndProc
        //		/// <summary>
        //		/// Overrides the handling of Mouse clicks to allow back-next to work in the designer
        //		/// </summary>
        //		/// <param name="msg"></param>
        //		protected override void WndProc(ref Message msg)
        //		{
        //			const int WM_LBUTTONDOWN = 0x0201;
        //			const int WM_LBUTTONDBLCLK = 0x0203;
        //			//When the user left clicks
        //			if (msg.Msg == WM_LBUTTONDOWN || msg.Msg == WM_LBUTTONDBLCLK)
        //			{
        //				// Get the control under the mouse
        //				ISelectionService ss = (ISelectionService)GetService(typeof(ISelectionService));
        //				
        //				if (ss.PrimarySelection is AC.ExtendedRenderer.Toolkit.Wizard.Wizard)
        //				{
        //					 AC.ExtendedRenderer.Toolkit.Wizard.Wizard wizard =  (AC.ExtendedRenderer.Toolkit.Wizard.Wizard) ss.PrimarySelection;
        //					// Extract the mouse position
        //					int xPos = (short)((uint)msg.LParam & 0x0000FFFF);
        //					int yPos = (short)(((uint)msg.LParam & 0xFFFF0000) >> 16);
        //
        //					// Pass on the mouse message
        //					wizard.ClickButtons(msg.HWnd, new Point(xPos, yPos));
        //					
        //					//Don't pass the Message on (i.e. Consume it)
        //					return;
        //				}
        //			}
        //
        //			base.WndProc(ref msg);
        //		}
        #endregion

        /// <summary>
        /// Prevents the grid from being drawn on the Wizard
        /// </summary>
        protected override bool DrawGrid
        {
            get
            {
                return base.DrawGrid && _allowGrid;
            }
        }
        private bool _allowGrid = true;

        //Doesn't seem to have any effect
        //		protected override bool EnableDragRect
        //		{
        //			get
        //			{
        //				return false; //base.EnableDragRect;
        //			}
        //		}


        /// <summary>
        /// Simple way to ensure <see cref="WizardPage"/>s only contained here
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public override bool CanParent(Control control)
        {
            if (control is WizardPage)
                return true;
            return false;
        }
        public override bool CanParent(ControlDesigner controlDesigner)
        {
            if (controlDesigner is WizardPageDesigner)
                return true;
            return false;
        }


        protected override bool GetHitTest(Point point)
        {
            Controls.KryptonWizard wiz = this.Control as Controls.KryptonWizard;

            if (wiz.btnNext.Enabled &&
                wiz.btnNext.ClientRectangle.Contains(wiz.btnNext.PointToClient(point)))
            {
                //Next can handle that
                return true;
            }
            if (wiz.btnBack.Enabled &&
                wiz.btnBack.ClientRectangle.Contains(wiz.btnBack.PointToClient(point)))
            {
                //Back can handle that
                return true;
            }
            //Nope not interested
            return false;
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                verbs.Add(new DesignerVerb("Add Page", new EventHandler(handleAddPage)));

                return verbs;
            }
        }

        private void handleAddPage(object sender, EventArgs e)
        {
            Controls.KryptonWizard wiz = this.Control as Controls.KryptonWizard;

            IDesignerHost h = (IDesignerHost)GetService(typeof(IDesignerHost));
            IComponentChangeService c = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            DesignerTransaction dt = h.CreateTransaction("Add Page");
            WizardPage page = (WizardPage)h.CreateComponent(typeof(WizardPage));
            c.OnComponentChanging(wiz, null);

            //Add a new page to the collection
            wiz.Pages.Add(page);
            wiz.Controls.Add(page);
            wiz.ActivatePage(page);

            c.OnComponentChanged(wiz, null, null, null);
            dt.Commit();
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            _allowGrid = false;
            base.OnPaintAdornments(pe);
            _allowGrid = true;

            //			if (base.DrawGrid)
            //			{
            //				Wizard wiz = this.Control as Wizard;
            //				Brush brush = new HatchBrush(HatchStyle.Percent10,SystemColors.ControlText, wiz.BackColor);
            //
            //				pe.Graphics.FillRectangle(brush,0,0,wiz.Width,8);
            //				pe.Graphics.FillRectangle(brush,0,0,8,wiz.pnlButtons.Top);
            //				pe.Graphics.FillRectangle(brush,0,wiz.pnlButtons.Top-8,wiz.Width,8);
            //				pe.Graphics.FillRectangle(brush,wiz.Width-8,0,8,wiz.pnlButtons.Top);
            //			}		
        }

    }
}