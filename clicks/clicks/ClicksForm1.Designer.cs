using System;
// using System.Collections;
using System.Windows.Forms;
using libdraw;

namespace ClicksGame
{
    partial class ClicksForm1
    {
        private const int boardRows = 12;
        private const int boardCols = 18;
        private int skill;
        private bool gameStarted = false;
        // next variable is never used, it looks like bool variable for infinite mode is used directly from checkbox value
        // commented 03.08.2016
        // private bool infiMode = false;
        
        private GameDraw drw;
        private ClicksComp game;
        private static Version ver;
        private readonly string gameLibText = "Game drawing library \'libdraw.dll\'";
        private readonly string libLoadErrText= " could not be loaded";
        private readonly string libNotFoundErrText = " is not found";
        private readonly string libLoadErrCaption = "Library loading error";
        private string helpMsg = "[PH]help text here";
        private readonly string gameWonCaption = "Congratulations!";
        private readonly string gameOverCaption = "Game Over";
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpStart = new System.Windows.Forms.GroupBox();
            this.checkInfinite = new System.Windows.Forms.CheckBox();
            this.checkInfiniteEndless = new System.Windows.Forms.CheckBox();
            this.hardSkill = new System.Windows.Forms.RadioButton();
            this.normalSkill = new System.Windows.Forms.RadioButton();
            this.easySkill = new System.Windows.Forms.RadioButton();
            this.quitGame = new System.Windows.Forms.Button();
            this.startGame = new System.Windows.Forms.Button();
            this.grpGame = new System.Windows.Forms.GroupBox();
            this.boardBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.helpItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu1 = new System.Windows.Forms.ToolStripSeparator();
            this.newGameItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitProgramItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.btnUndoTurn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.skillPanel = new System.Windows.Forms.ToolStripStatusLabel();
            this.infoPanel = new System.Windows.Forms.ToolStripStatusLabel();
            this.modePanel = new System.Windows.Forms.ToolStripStatusLabel();
            this.kPanel = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpStart.SuspendLayout();
            this.grpGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.grpControl.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpStart
            // 
            this.grpStart.Controls.Add(this.checkInfinite);
            this.grpStart.Controls.Add(this.checkInfiniteEndless);
            this.grpStart.Controls.Add(this.hardSkill);
            this.grpStart.Controls.Add(this.normalSkill);
            this.grpStart.Controls.Add(this.easySkill);
            this.grpStart.Controls.Add(this.quitGame);
            this.grpStart.Controls.Add(this.startGame);
            this.grpStart.Location = new System.Drawing.Point(5, 0);
            this.grpStart.Name = "grpStart";
            this.grpStart.Size = new System.Drawing.Size(660, 455);
            this.grpStart.TabIndex = 0;
            this.grpStart.TabStop = false;
            // 
            // checkInfinite
            // 
            this.checkInfinite.AutoSize = true;
            this.checkInfinite.Location = new System.Drawing.Point(299, 172);
            this.checkInfinite.Name = "checkInfinite";
            this.checkInfinite.Size = new System.Drawing.Size(86, 17);
            this.checkInfinite.TabIndex = 6;
            this.checkInfinite.Text = "In&finite mode";
            this.checkInfinite.UseVisualStyleBackColor = true;
            this.checkInfinite.CheckedChanged += new System.EventHandler(this.checkInfinite_CheckedChanged);
            // 
            // checkInfiniteEndless
            // 
            this.checkInfiniteEndless.AutoSize = true;
            this.checkInfiniteEndless.Enabled = false;
            this.checkInfiniteEndless.Location = new System.Drawing.Point(318, 195);
            this.checkInfiniteEndless.Name = "checkInfiniteEndless";
            this.checkInfiniteEndless.Size = new System.Drawing.Size(116, 17);
            this.checkInfiniteEndless.TabIndex = 7;
            this.checkInfiniteEndless.Text = "&Pure endless mode";
            this.checkInfiniteEndless.UseVisualStyleBackColor = true;
            // 
            // hardSkill
            // 
            this.hardSkill.AutoSize = true;
            this.hardSkill.Location = new System.Drawing.Point(299, 149);
            this.hardSkill.Name = "hardSkill";
            this.hardSkill.Size = new System.Drawing.Size(48, 17);
            this.hardSkill.TabIndex = 5;
            this.hardSkill.Text = "&Hard";
            this.hardSkill.UseVisualStyleBackColor = true;
            // 
            // normalSkill
            // 
            this.normalSkill.AutoSize = true;
            this.normalSkill.Location = new System.Drawing.Point(299, 126);
            this.normalSkill.Name = "normalSkill";
            this.normalSkill.Size = new System.Drawing.Size(58, 17);
            this.normalSkill.TabIndex = 4;
            this.normalSkill.Text = "&Normal";
            this.normalSkill.UseVisualStyleBackColor = true;
            // 
            // easySkill
            // 
            this.easySkill.AutoSize = true;
            this.easySkill.Checked = true;
            this.easySkill.Location = new System.Drawing.Point(299, 103);
            this.easySkill.Name = "easySkill";
            this.easySkill.Size = new System.Drawing.Size(48, 17);
            this.easySkill.TabIndex = 3;
            this.easySkill.TabStop = true;
            this.easySkill.Text = "&Easy";
            this.easySkill.UseVisualStyleBackColor = true;
            // 
            // quitGame
            // 
            this.quitGame.Location = new System.Drawing.Point(283, 370);
            this.quitGame.Name = "quitGame";
            this.quitGame.Size = new System.Drawing.Size(94, 28);
            this.quitGame.TabIndex = 2;
            this.quitGame.Text = "E&xit";
            this.quitGame.UseVisualStyleBackColor = true;
            this.quitGame.Click += new System.EventHandler(this.quitGame_Click);
            // 
            // startGame
            // 
            this.startGame.Location = new System.Drawing.Point(252, 250);
            this.startGame.Name = "startGame";
            this.startGame.Size = new System.Drawing.Size(156, 56);
            this.startGame.TabIndex = 1;
            this.startGame.Text = "Start &Game";
            this.startGame.UseVisualStyleBackColor = true;
            this.startGame.Click += new System.EventHandler(this.startGame_Click);
            // 
            // grpGame
            // 
            this.grpGame.Controls.Add(this.boardBox);
            this.grpGame.Location = new System.Drawing.Point(5, 0);
            this.grpGame.Name = "grpGame";
            this.grpGame.Size = new System.Drawing.Size(660, 455);
            this.grpGame.TabIndex = 1;
            this.grpGame.TabStop = false;
            this.grpGame.Visible = false;
            // 
            // boardBox
            // 
            this.boardBox.BackgroundImage = global::ClicksGame.Properties.Resources.Image1;
            this.boardBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.boardBox.ContextMenuStrip = this.menuStrip1;
            this.boardBox.Location = new System.Drawing.Point(6, 14);
            this.boardBox.Name = "boardBox";
            this.boardBox.Size = new System.Drawing.Size(649, 433);
            this.boardBox.TabIndex = 0;
            this.boardBox.TabStop = false;
            this.boardBox.Paint += new System.Windows.Forms.PaintEventHandler(this.boardBox_Paint);
            this.boardBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.boardBox_MouseDown);
            this.boardBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.boardBox_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpItem,
            this.mnu1,
            this.newGameItem,
            this.undoItem,
            this.exitItem,
            this.mnu2,
            this.exitProgramItem});
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(216, 126);
            // 
            // helpItem
            // 
            this.helpItem.Name = "helpItem";
            this.helpItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpItem.Size = new System.Drawing.Size(215, 22);
            this.helpItem.Text = "Help";
            this.helpItem.Click += new System.EventHandler(this.helpItem_Click);
            // 
            // mnu1
            // 
            this.mnu1.Name = "mnu1";
            this.mnu1.Size = new System.Drawing.Size(212, 6);
            // 
            // newGameItem
            // 
            this.newGameItem.Name = "newGameItem";
            this.newGameItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.newGameItem.Size = new System.Drawing.Size(215, 22);
            this.newGameItem.Text = "New Game";
            this.newGameItem.Click += new System.EventHandler(this.newGameItem_Click);
            // 
            // undoItem
            // 
            this.undoItem.Name = "undoItem";
            this.undoItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoItem.Size = new System.Drawing.Size(215, 22);
            this.undoItem.Text = "Undo turn";
            this.undoItem.Click += new System.EventHandler(this.undoItem_Click);
            // 
            // exitItem
            // 
            this.exitItem.Name = "exitItem";
            this.exitItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitItem.Size = new System.Drawing.Size(215, 22);
            this.exitItem.Text = "Close current game";
            this.exitItem.Click += new System.EventHandler(this.exitItem_Click);
            // 
            // mnu2
            // 
            this.mnu2.Name = "mnu2";
            this.mnu2.Size = new System.Drawing.Size(212, 6);
            // 
            // exitProgramItem
            // 
            this.exitProgramItem.Name = "exitProgramItem";
            this.exitProgramItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitProgramItem.Size = new System.Drawing.Size(215, 22);
            this.exitProgramItem.Text = "Exit Program";
            this.exitProgramItem.Click += new System.EventHandler(this.exitProgramItem_Click);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.btnUndoTurn);
            this.grpControl.Location = new System.Drawing.Point(671, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(206, 455);
            this.grpControl.TabIndex = 2;
            this.grpControl.TabStop = false;
            // 
            // btnUndoTurn
            // 
            this.btnUndoTurn.Enabled = false;
            this.btnUndoTurn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUndoTurn.Location = new System.Drawing.Point(44, 50);
            this.btnUndoTurn.Name = "btnUndoTurn";
            this.btnUndoTurn.Size = new System.Drawing.Size(121, 44);
            this.btnUndoTurn.TabIndex = 0;
            this.btnUndoTurn.Text = "&Undo";
            this.btnUndoTurn.UseVisualStyleBackColor = true;
            this.btnUndoTurn.Visible = false;
            this.btnUndoTurn.Click += new System.EventHandler(this.btnUndoTurn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.skillPanel,
            this.infoPanel,
            this.modePanel,
            this.kPanel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(882, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // skillPanel
            // 
            this.skillPanel.AutoSize = false;
            this.skillPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.skillPanel.Name = "skillPanel";
            this.skillPanel.Size = new System.Drawing.Size(132, 17);
            // 
            // infoPanel
            // 
            this.infoPanel.AutoSize = false;
            this.infoPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(180, 17);
            // 
            // modePanel
            // 
            this.modePanel.AutoSize = false;
            this.modePanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.modePanel.Name = "modePanel";
            this.modePanel.Size = new System.Drawing.Size(153, 17);
            // 
            // kPanel
            // 
            this.kPanel.AutoSize = false;
            this.kPanel.Name = "kPanel";
            this.kPanel.Size = new System.Drawing.Size(402, 17);
            this.kPanel.Spring = true;
            // 
            // ClicksForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 480);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpStart);
            this.Controls.Add(this.grpGame);
            this.Controls.Add(this.grpControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ClicksForm1";
            this.Text = "ClicksForm1";
            this.grpStart.ResumeLayout(false);
            this.grpStart.PerformLayout();
            this.grpGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.grpControl.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStart;
        private System.Windows.Forms.GroupBox grpGame;
        private System.Windows.Forms.GroupBox grpControl;
        private PictureBox boardBox;
        private RadioButton hardSkill;
        private RadioButton normalSkill;
        private RadioButton easySkill;
        private Button quitGame;
        private Button startGame;
        private Button btnUndoTurn;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel skillPanel;
        private ToolStripStatusLabel infoPanel;
        private ToolStripStatusLabel kPanel;
        private ToolStripMenuItem helpItem;
        private ToolStripMenuItem newGameItem;
        private ToolStripMenuItem undoItem;
        private ToolStripMenuItem exitItem;
        private ToolStripMenuItem exitProgramItem;
        private ContextMenuStrip menuStrip1;
        private ToolStripSeparator mnu1;
        private ToolStripSeparator mnu2;
        private CheckBox checkInfinite;
        private CheckBox checkInfiniteEndless;
        private ToolStripStatusLabel modePanel;
    }
}

