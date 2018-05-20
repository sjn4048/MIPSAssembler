using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
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
        int currentLineNum = 0;
        
        //用于代码高亮
        private static readonly char[] delimitter = { ' ', '\t', ',', '\n', ';', '(', ')' };
        List<string> Highlight_blue = new List<string>()
        {
            "lb",
            "lbu",
            "lh",
            "lhu",
            "sw",
            "sb",
            "sh",
            "add",
            "addu",
            "sub",
            "subu",
            "slt",
            "sltu",
            "and",
            "or",
            "xor",
            "nor",
            "sll",
            "srl",
            "sra",
            "mult",
            "multu",
            "div",
            "divu",
            "addi",
            "addiu",
            "andi",
            "ori",
            "xori",
            "lui",
            "slti",
            "sltiu",
            "beq",
            "bne",
            "blez",
            "bgtz",
            "bltz",
            "bgez",
            "j",
            "jal",
            "jalr",
            "jr",
            "mfhi",
            "mflo",
            "mthi",
            "mtlo",
            "eret",
            "mfco",
            "mtco",
            "break",
            "syscall",
            "la",
            "li",
            "move"
        };
        List<string> Highlight_green = new List<string>(Register.reg_names);

        public Form1()
        {
            InitializeComponent();
            //linenum.Text = "1";
            outputPath = Environment.CurrentDirectory;
            editor.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            consoleTextbox.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void editor_TextChanged(object sender, EventArgs e)
        {
            saved = false;
            if (editor.Lines.Length != currentLineNum)
            {
                currentLineNum = editor.Lines.Length;
                panel1.Invalidate();
            }
            ChangeTitle();
        }

        private void toolStrip_Open_Click(object sender, EventArgs e) // 打开成功返回true
        {
            CheckSave();
            var ofDialog = new OpenFileDialog()
            {
                // InitialDirectory = "",
                Filter = "所有文件|*.*|coe文件|*.coe|bin文件|*.bin|汇编文件(*.a,*.asm)|*.a;*.asm"
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
                Filter = "所有文件|*.*|coe文件|*.coe|bin文件|*.bin|汇编文件(*.a,*.asm)|*.a;*.asm"
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
            if (saved == false && editor.Text != string.Empty && MessageBox.Show(text: "有未保存的内容，确认离开？", caption: "提示", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.YesNo) == DialogResult.No)
            {
                if (CurrentFileName == HAVENT_OPEN)
                    toolStrip_Saveelse.PerformClick();
                else
                    toolStrip_Save.PerformClick();
            }
        }

        private void CheckSave(FormClosingEventArgs e) // 可以正确退出返回true，需要中止操作返回false
        {
            if (saved == false && editor.Text != string.Empty && MessageBox.Show(text: "有未保存的内容，确认离开？", caption: "提示", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.YesNo) == DialogResult.No)
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

        private void CodeColoring() // 标注指定行
        {
            //var lines = editor.Text.Split('\n');
            //foreach (var line in lines)
            //{
            //    if (line.StartsWith("#") || line.StartsWith("/"))
            //        editor.Select(0, )

            //    if (Highlight_blue.IndexOf(word) > -1)
            //    {
            //        MySelect(editor, editor.SelectionStart, s, Color.CadetBlue, true);
            //    }
            //    if (Highlight_green.IndexOf(word) > -1)
            //    {
            //        MySelect(rich, rich.SelectionStart, s, Color.Green, true);
            //    }
            //}
        }

        public static void MySelect(System.Windows.Forms.RichTextBox tb, int i, string s, Color c, bool font)
        {
            tb.Select(i - s.Length, s.Length);
            tb.SelectionColor = c;
            //是否改变字体
            if (font)
                tb.SelectionFont = new Font("Consolas", 12, (FontStyle.Bold));
            //以下是把光标放到原来位置，并把光标后输入的文字重置
            tb.Select(i, 0);
            tb.SelectionFont = new Font("宋体", 12, (FontStyle.Regular));
            tb.SelectionColor = Color.Black;
        }

        private void toolStrip_binary_Click(object sender, EventArgs e)
        {
            var sfdialog = new SaveFileDialog()
            {
                Title = "保存.bin文件",
                Filter = "bin文件|*.bin|所有文件|*.*"
            };
            if (sfdialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var assembler = new MIPSAssembler();
                    var result = assembler.MipsToBin(editor.Text);
                    outputPath = Path.GetDirectoryName(sfdialog.FileName);
                    CurrentFileName = sfdialog.FileName;
                    toolStrip_new.PerformClick();
                    File.WriteAllText(CurrentFileName, result);
                }
                catch (Exception ex)
                {
                    consoleTextbox.Text += ex.Message + '\n';
                }
            }
            consoleTextbox.Text += "bin文件输出成功。按F8打开输出目录。\n";
        }

        private void toolStrip_Coe_Click(object sender, EventArgs e)
        {
            var sfdialog = new SaveFileDialog()
            {
                Title = "保存.coe文件",
                Filter = "coe文件|*.coe|所有文件|*.*"
            };
            if (sfdialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var assembler = new MIPSAssembler();
                    var result = assembler.MipsToCoe(editor.Text);
                    outputPath = Path.GetDirectoryName(sfdialog.FileName);
                    CurrentFileName = sfdialog.FileName;
                    File.WriteAllText(CurrentFileName, result);
                }
                catch (Exception ex)
                {
                    consoleTextbox.Text += ex.Message + '\n';
                }
            }
            consoleTextbox.Text += "coe文件输出成功。按F8打开输出目录。\n";
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

        private void toolStrip_new_Click(object sender, EventArgs e)
        {
            CheckSave();
            CurrentFileName = HAVENT_OPEN;
            saved = true;
            outputPath = "";
            editor.Clear();
            ChangeTitle();
        }

        private void toolStrip_Disassemble_Click(object sender, EventArgs e)
        {
            CheckSave();
            try
            {
                string result;
                if (CurrentFileName.ToLower().EndsWith(".coe"))
                    result = new MIPSDisassembler().CoeToMips(editor.Text);
                else if (CurrentFileName.ToLower().EndsWith(".bin"))
                    result = new MIPSDisassembler().BinToMips(editor.Text);
                else if (MessageBox.Show(text: @"请选择当前文件类型(coe请按""是""/bin请按""否"")", buttons: MessageBoxButtons.YesNo, caption: "请选择文件类型", icon: MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    result = new MIPSDisassembler().CoeToMips(editor.Text);
                }
                else
                {
                    result = new MIPSDisassembler().BinToMips(editor.Text);
                }
                toolStrip_new.PerformClick();
                editor.Text = result;
            }
            catch (Exception ex)
            {
                consoleTextbox.Text += ex.Message + '\n';
            }
            consoleTextbox.Text += "反汇编完成。\n";
        }

        private void consoleTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShowNumLines()
        {
            Point p = new Point(0, 0);
            int curIndex = this.editor.GetCharIndexFromPosition(p);
            int curLine = this.editor.GetLineFromCharIndex(curIndex);
            Point curPos = this.editor.GetPositionFromCharIndex(curIndex);
            p.Y += this.editor.Height;
            int curLastIndex = this.editor.GetCharIndexFromPosition(p) + 1;
            int curLastLine = this.editor.GetLineFromCharIndex(curLastIndex);
            Point curLastPos = this.editor.GetPositionFromCharIndex(curLastIndex);
            Graphics g = this.panel1.CreateGraphics();
            Font font = new Font(this.editor.Font, this.editor.Font.Style);
            SolidBrush brush = new SolidBrush(SystemColors.ControlDark);
            Rectangle rect = this.panel1.ClientRectangle;
            brush.Color = this.panel1.BackColor;
            g.FillRectangle(brush, 0, 0, this.panel1.ClientRectangle.Width, this.panel1.ClientRectangle.Height);
            brush.Color = SystemColors.ControlDark;
            //
            //绘制行号
            int lineSpace = 0;
            if (curLine != curLastLine)
            {
                lineSpace = (curLastPos.Y - curPos.Y) / (curLastLine - curLine);
            }
            else
            {
                lineSpace = Convert.ToInt32(this.editor.Font.Size);
            }
            int brushX = this.panel1.ClientRectangle.Width - Convert.ToInt32(font.Size * 3);
            int brushY = curLastPos.Y + Convert.ToInt32(font.Size * 0.03f);//惊人的算法啊！！
            for (int i = curLastLine; i >= curLine; i--)
            {
                g.DrawString((i + 1).ToString(), font, brush, brushX, brushY);
                brushY -= lineSpace;
            }
            g.Dispose();
            font.Dispose();
            brush.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ShowNumLines();
        }

        private void debugDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("实在抱歉，由于debug功能出现一定bug，为不影响使用，Release版暂时屏蔽了Debug接口。请等待下次更新。", "Please Wait..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static string GetLastWord(string str, int i)
        {
            string x = str;
            Regex reg = new Regex(@"\s+[a-z]+\s*", RegexOptions.RightToLeft);
            x = reg.Match(x).Value;

            Regex reg2 = new Regex(@"\s");
            x = reg2.Replace(x, "");
            return x;
        }

        private void editor_VScroll(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }
    }
}
