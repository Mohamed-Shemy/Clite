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

namespace Clite
{
    public partial class Editor : Form
    {
        bool move = false;
        int movx, movy, ErrorCount = 0;
        string FilePath = "";
        string Code = "";

        public Editor()
        {
            InitializeComponent();
        }
        private void Editor_Load(object sender, EventArgs e)
        {
            ParentContainer.Panel2Collapsed = true;
        }
        protected override void WndProc(ref Message m)
        {
            const UInt32 WM_NCHITTEST = 0x0084;
            const UInt32 WM_MOUSEMOVE = 0x0200;

            const UInt32 HTLEFT = 10;
            const UInt32 HTRIGHT = 11;
            const UInt32 HTBOTTOMRIGHT = 17;
            const UInt32 HTBOTTOM = 15;
            const UInt32 HTBOTTOMLEFT = 16;
            const UInt32 HTTOP = 12;
            const UInt32 HTTOPLEFT = 13;
            const UInt32 HTTOPRIGHT = 14;

            const int RESIZE_HANDLE_SIZE = 10;
            bool handled = false;
            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                Size formSize = this.Size;
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);

                Dictionary<UInt32, Rectangle> boxes = new Dictionary<UInt32, Rectangle>() {
            {HTBOTTOMLEFT, new Rectangle(0, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
            {HTBOTTOM, new Rectangle(RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, formSize.Width - 2*RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
            {HTBOTTOMRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
            {HTRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2*RESIZE_HANDLE_SIZE)},
            {HTTOPRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, 0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE) },
            {HTTOP, new Rectangle(RESIZE_HANDLE_SIZE, 0, formSize.Width - 2*RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE) },
            {HTTOPLEFT, new Rectangle(0, 0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE) },
            {HTLEFT, new Rectangle(0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2*RESIZE_HANDLE_SIZE) }
        };

                foreach (KeyValuePair<UInt32, Rectangle> hitBox in boxes)
                {
                    if (hitBox.Value.Contains(clientPoint))
                    {
                        m.Result = (IntPtr)hitBox.Key;
                        handled = true;
                        break;
                    }
                }
            }

            if (!handled)
                base.WndProc(ref m);
        }
        private void MoveMouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            movx = e.X;
            movy = e.Y;
        }
        private void MoveMouse(object sender, MouseEventArgs e)
        {
            if (move)
                this.SetDesktopLocation(MousePosition.X - movx, MousePosition.Y - movy);
        }
        private void MoveMouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }
        private void MaxWindowClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }
        private void MinWindowClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void CollapsErrorListClick(object sender, EventArgs e)
        {
            ParentContainer.Panel2Collapsed = !ParentContainer.Panel2Collapsed;
        }
        private void LoadCodeClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FilePath = ofd.FileName;
                StreamReader sr = new StreamReader(FilePath);
                edit.Text = sr.ReadToEnd();
                sr.Close();
            }
        }
        private void CompileClick(object sender, EventArgs e)
        {
            ErrorCount = 0;
            Parser parser;
            error_list.Items.Clear();
            lbl_stats.Text = "";
            txt_out.Text = "";
            if (ParentContainer.Panel2Collapsed)
            {
                ParentContainer.Panel2Collapsed = false;
                Tabs.SelectedIndex = 1;
                txt_out.Text = "Compiling .....\n";
            }
            if (edit.Text.Length < 1)
            {
                MessageBox.Show("No Code To Compile It!!", "No Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                parser = new Parser(new Scanner(String2Stream(edit.Text)));
                try
                {
                    IProgram prog = parser.Program();
                    StaticTypeCheck.Validate(prog);
                    IProgram transformed = TypeTransformer.Transform(prog);
                    txt_out.Text += transformed.Display();
                    txt_out.Text += "\n-------------------------------\n";
                    txt_out.Text += "\nResults:\n";
                    State state = Interpreter.Interpret(transformed);
                    Code = state.Display();
                    txt_out.Text += Code;
                    ParentContainer.Panel2Collapsed = true;
                    lbl_stats.Text = "Successful Compiling";
                }
                catch (IException ex)
                {
                    txt_out.Text = ex.Type + " Error :  " + ex.Message;
                    ListViewItem lvi = new ListViewItem((++ErrorCount).ToString());
                    lvi.SubItems.Add(ex.Type);
                    lvi.SubItems.Add(ex.File);
                    lvi.SubItems.Add(ex.LineNumber.ToString());
                    lvi.SubItems.Add(ex.ColmunNumber.ToString());
                    lvi.SubItems.Add(ex.Message);
                    error_list.Items.Add(lvi);
                    lbl_stats.Text = "Failed Compiling";
                    Tabs.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    txt_out.Text = " Error :  " + ex.Message;
                    ListViewItem lvi = new ListViewItem((++ErrorCount).ToString());
                    lvi.SubItems.Add("Unkown");
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add(ex.Message);
                    error_list.Items.Add(lvi);
                    lbl_stats.Text = "Failed Compiling";
                }
            }
        }
        private void SaveCodeClick(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if(sfd.ShowDialog() == DialogResult.OK)
                using (StreamWriter file = new StreamWriter(sfd.FileName))
                    file.WriteLine(edit.Text);
        }

        private void GoToErrorLineDClick(object sender, EventArgs e)
        {
            if (error_list.Items.Count < 1) return;
            int nl = int.Parse(error_list.SelectedItems[0].SubItems[3].Text);
            if (nl == 0) return;
            edit.SelectionStart = nl;
            edit.SelectionLength = edit.GetLineLength(nl);
        }

        private void BuildClick(object sender, EventArgs e)
        {
            try
            {
                CompileClick(null, null);
                Build b = new Build(Code, "test.exe");
            }
            catch (Exception ex)
            {
                txt_out.Text = " Error :  " + ex.Message;
                ListViewItem lvi = new ListViewItem((++ErrorCount).ToString());
                lvi.SubItems.Add("Build");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add(ex.Message);
                error_list.Items.Add(lvi);
                lbl_stats.Text = "Failed Building";
            }
        }

        public Stream String2Stream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        private void ExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
