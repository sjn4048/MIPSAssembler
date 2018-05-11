using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MIPSAssembler_Winform
{
    public partial class Form1 : Form
    {
        string CurrentFileName = HAVENT_OPEN;
        const string HAVENT_OPEN = "This_indicates_nothing_is_open";
        string outputPath;
        bool saved = true;

        public Form1()
        {
            InitializeComponent();
            linenum.Text = "1";
            outputPath = Environment.CurrentDirectory;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void editor_TextChanged(object sender, EventArgs e)
        {
            saved = false;
            if (editor.Lines.Length != linenum.Lines.Length)
            {
                var linenum_text = "1";
                for (int i = 2; i < editor.Lines.Length + 1; i++)
                {
                    linenum_text += $"\n{i}";
                }
                linenum.Text = linenum_text;
            }
            ChangeTitle();
            /// 以下为着色
            #region paint
            int currentLine; // TODO
            #endregion
        }

        private void toolStrip_Open_Click(object sender, EventArgs e) // 打开成功返回true
        {
            CheckSave();
            var ofDialog = new OpenFileDialog()
            {
                // InitialDirectory = "",
                Filter = "所有文件|*.*|coe文件|*.coe|bin文件|*.bin"
            };
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentFileName = ofDialog.FileName;
                editor.Text = File.ReadAllText(CurrentFileName);
                toolStrip_Save.Enabled = false;
                saved = true;
                ChangeTitle();
                //return true;
            }
            //return false;
        }

        private void toolStrip_Save_Click(object sender, EventArgs e) // 保存成功返回true
        {
            if (CurrentFileName != HAVENT_OPEN)
            {
                File.WriteAllText(CurrentFileName, editor.Text);
                toolStrip_Save.Enabled = false;
                saved = true;
                ChangeTitle();
                //return true;
            }
            else
            {
                toolStrip_Saveelse.PerformClick();
            }
            //return false;
        }

        private void toolStrip_Saveelse_Click(object sender, EventArgs e) // 成功返回true
        {
            var sfdialog = new SaveFileDialog()
            {
                Filter = "所有文件|*.*|coe文件|*.coe|bin文件|*.bin"
            };
            if (sfdialog.ShowDialog() == DialogResult.OK)
            {
                CurrentFileName = sfdialog.FileName;
                File.WriteAllText(CurrentFileName, editor.Text);
                saved = true;
                //return true;
            }
            //return false;
        }

        private void toolStrip_Add_Click(object sender, EventArgs e)
        {
            editor.Copy();
        }

        private void toolStrip_Paste_Click(object sender, EventArgs e)
        {
            editor.Paste();
        }

        private void toolStrip_Cut_Click(object sender, EventArgs e)
        {
            editor.Cut();
        }

        private void toolStrip_SelectAll_Click(object sender, EventArgs e)
        {
            editor.SelectAll();
        }

        private void toolStrip_Undo_Click(object sender, EventArgs e)
        {
            editor.Undo();
        }

        private void toolStrip_Redo_Click(object sender, EventArgs e)
        {
            editor.Redo();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CheckSave(e);
        }

        private void CheckSave() // 可以正确退出返回true，需要中止操作返回false
        {
            if (saved == false && MessageBox.Show(text: "有未保存的内容，确认离开？", caption: "提示", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.YesNo) == DialogResult.No)
            {
                if (CurrentFileName == HAVENT_OPEN)
                    toolStrip_Saveelse.PerformClick();
                else
                    toolStrip_Save.PerformClick();
            }
        }

        private void CheckSave(FormClosingEventArgs e) // 可以正确退出返回true，需要中止操作返回false
        {
            if (saved == false && MessageBox.Show(text: "有未保存的内容，确认离开？", caption: "提示", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                if (CurrentFileName == HAVENT_OPEN)
                    toolStrip_Saveelse.PerformClick();
                else
                    toolStrip_Save.PerformClick();
            }
        }


        private void toolStrip_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStrip_About_Click(object sender, EventArgs e)
        {
            new Form_About().ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ChangeTitle()
        {
            if (this.CurrentFileName != HAVENT_OPEN)
            {
                if (this.saved)
                    this.Text = this.Text = $"MyMIPS - {CurrentFileName}";
                else
                    this.Text = this.Text = $"MyMIPS - {CurrentFileName} *";
            }
            else
            {
                this.Text = this.Text = $"MyMIPS - 未保存文件";
            }
        }

        private void CodeColoring(int currentLine) // 标注指定行
        {
            string line = editor.Lines[currentLine];
            {
                // 着色注释
                int index = line.IndexOf('#');
                if (index != -1)
                    editor.Select();
            }
        }

        private void CodeColoring(int startLine, int endLine) // 注释指定范围行
        {
            for (int i = startLine; i < endLine; i++)
            {
                CodeColoring(i);
            }
        }

        private void toolStrip_binary_Click(object sender, EventArgs e)
        {
            var assembler = new MIPSAssembler();
            var result = assembler.MipsToBin(editor.Text);

            var sfdialog = new SaveFileDialog()
            {
                Title = "保存.bin文件",
                Filter = "bin文件|*.bin|所有文件|*.*"
            };
            if (sfdialog.ShowDialog() == DialogResult.OK)
            {
                outputPath = Path.GetDirectoryName(sfdialog.FileName);
                CurrentFileName = sfdialog.FileName;
                File.WriteAllText(CurrentFileName, result);
            }
        }

        private void toolStrip_Coe_Click(object sender, EventArgs e)
        {
            var assembler = new MIPSAssembler();
            var result = assembler.MipsToCoe(editor.Text);

            var sfdialog = new SaveFileDialog()
            {
                Title = "保存.coe文件",
                Filter = "coe文件|*.coe|所有文件|*.*"
            };
            if (sfdialog.ShowDialog() == DialogResult.OK)
            {
                outputPath = Path.GetDirectoryName(sfdialog.FileName);
                CurrentFileName = sfdialog.FileName;
                File.WriteAllText(CurrentFileName, result);
            }
        }

        private void toolStrip_OpenDir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(outputPath);
        }

        private void toolStrip_All_Click(object sender, EventArgs e)
        {
            toolStrip_binary.PerformClick();
            toolStrip_Coe.PerformClick();
        }
    }
}
