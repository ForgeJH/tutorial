namespace NasBPDM
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnOut = new System.Windows.Forms.Button();
            this.txtTFSPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTFPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtDevPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBatPath = new System.Windows.Forms.Label();
            this.txtBatPath = new System.Windows.Forms.TextBox();
            this.labNXCOMPAT = new System.Windows.Forms.Label();
            this.labLARGEADDRESSAWARE = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(12, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "최신";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(93, 12);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(75, 23);
            this.btnIn.TabIndex = 1;
            this.btnIn.Text = "내부";
            this.btnIn.UseVisualStyleBackColor = true;
            // 
            // btnOut
            // 
            this.btnOut.Location = new System.Drawing.Point(174, 12);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(75, 23);
            this.btnOut.TabIndex = 2;
            this.btnOut.Text = "외부";
            this.btnOut.UseVisualStyleBackColor = true;
            // 
            // txtTFSPath
            // 
            this.txtTFSPath.Location = new System.Drawing.Point(68, 41);
            this.txtTFSPath.Name = "txtTFSPath";
            this.txtTFSPath.Size = new System.Drawing.Size(351, 21);
            this.txtTFSPath.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "TFS경로";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "TF경로";
            // 
            // txtTFPath
            // 
            this.txtTFPath.Location = new System.Drawing.Point(68, 70);
            this.txtTFPath.Multiline = true;
            this.txtTFPath.Name = "txtTFPath";
            this.txtTFPath.Size = new System.Drawing.Size(351, 42);
            this.txtTFPath.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "결과값";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(68, 224);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(351, 128);
            this.txtResult.TabIndex = 11;
            // 
            // txtDevPath
            // 
            this.txtDevPath.Location = new System.Drawing.Point(68, 118);
            this.txtDevPath.Multiline = true;
            this.txtDevPath.Name = "txtDevPath";
            this.txtDevPath.Size = new System.Drawing.Size(351, 46);
            this.txtDevPath.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "DEV경로";
            // 
            // lblBatPath
            // 
            this.lblBatPath.AutoSize = true;
            this.lblBatPath.Location = new System.Drawing.Point(10, 173);
            this.lblBatPath.Name = "lblBatPath";
            this.lblBatPath.Size = new System.Drawing.Size(53, 12);
            this.lblBatPath.TabIndex = 15;
            this.lblBatPath.Text = "DEV경로";
            // 
            // txtBatPath
            // 
            this.txtBatPath.Location = new System.Drawing.Point(68, 170);
            this.txtBatPath.Multiline = true;
            this.txtBatPath.Name = "txtBatPath";
            this.txtBatPath.Size = new System.Drawing.Size(351, 48);
            this.txtBatPath.TabIndex = 14;
            // 
            // labNXCOMPAT
            // 
            this.labNXCOMPAT.AutoSize = true;
            this.labNXCOMPAT.Location = new System.Drawing.Point(10, 382);
            this.labNXCOMPAT.Name = "labNXCOMPAT";
            this.labNXCOMPAT.Size = new System.Drawing.Size(75, 12);
            this.labNXCOMPAT.TabIndex = 16;
            this.labNXCOMPAT.Text = "NXCOMPAT";
            // 
            // labLARGEADDRESSAWARE
            // 
            this.labLARGEADDRESSAWARE.AutoSize = true;
            this.labLARGEADDRESSAWARE.Location = new System.Drawing.Point(10, 403);
            this.labLARGEADDRESSAWARE.Name = "labLARGEADDRESSAWARE";
            this.labLARGEADDRESSAWARE.Size = new System.Drawing.Size(143, 12);
            this.labLARGEADDRESSAWARE.TabIndex = 17;
            this.labLARGEADDRESSAWARE.Text = "LARGEADDRESSAWARE";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(431, 435);
            this.shapeContainer1.TabIndex = 18;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 14;
            this.lineShape1.X2 = 417;
            this.lineShape1.Y1 = 366;
            this.lineShape1.Y2 = 366;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(344, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(263, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 20;
            this.btnLoad.Text = "불러오기";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 435);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labLARGEADDRESSAWARE);
            this.Controls.Add(this.labNXCOMPAT);
            this.Controls.Add(this.lblBatPath);
            this.Controls.Add(this.txtBatPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDevPath);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTFPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTFSPath);
            this.Controls.Add(this.btnOut);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.shapeContainer1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximumSize = new System.Drawing.Size(447, 474);
            this.MinimumSize = new System.Drawing.Size(447, 474);
            this.Name = "Form1";
            this.Text = "NAS 배포 도움 V1.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnOut;
        private System.Windows.Forms.TextBox txtTFSPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTFPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtDevPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBatPath;
        private System.Windows.Forms.TextBox txtBatPath;
        private System.Windows.Forms.Label labNXCOMPAT;
        private System.Windows.Forms.Label labLARGEADDRESSAWARE;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
    }
}

