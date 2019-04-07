using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;   // For BitmMapData type
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using ShadowEngine;

namespace Cortex
{
    public partial class Form1 : Form
    {

        bool mouselock;
        bool zo = true;
        int scene = 0;
        char[] block = { '#', '#' };
        OpenTK.Vector2 lastMousePos = new OpenTK.Vector2();
        OpenTK.Vector2 delta = new OpenTK.Vector2(0, 0);

        public Form1()
        {
            InitializeComponent();
        }


        private void popCB()
        {
            string name = "";
            int i = 0;

            foreach (Archive data in Main.Archives)
            {
                name = data.Name;
                comboBox1.Items.Add(new Item(name, i));
                comboBox2.Items.Add(new Item(name, i));
                i++;
            }
        }
        // Content item for the combo box
        private class Item
        {
            public string Name;
            public int Value;
            public Item(string name, int value)
            {
                Name = name;
                Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }

        int MRefs = -1;
        public void MRef(int opt)
        {
            if (opt == 0)
            {
                MRefs = tabControl1.SelectedIndex;
                tabControl1.SelectedIndex = 0;
            }
            else if (opt == 1)
            {
                tabControl1.SelectedIndex = MRefs;
            }
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            //Ensures that the OPENGL form is in place before assigning any functions
            Draw.loaded = true;
            Draw.Load();
            popCB();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            Draw.UpdateImage(Width, Height); //Show First Frame
            timer1.Start(); // Begin Timer
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            //Update Image
            Draw.Render();
            glControl1.SwapBuffers();
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            //Updates the image according to to updated window parameters
            Draw.UpdateImage(Width, Height);
        }
        public void startTimer()
        {
            timer1.Start();
        }

        public void stopTimer()
        {
            timer1.Stop();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Code for timer iteration
            Draw.Render();
            glControl1.SwapBuffers();
            Rotate(); //Spin image according to mouse
            Main.ii++; //increase the itteration count
            label9.Text = "Current Frame: " + Main.ii.ToString();
            if (Main.Flash == 1)
            {
                if (Main.ii % 10 > 5)
                {
                    Main.Map[0].data.setTint(2);
                }
                else
                {
                    Main.Map[0].data.setTint(5);
                }
            }
            else if (Main.Flash == 2)
            {
                if (Main.ii % 10 > 5)
                {
                    Main.Map[0].data.setTint(1);
                    Main.Map[1].data.setTint(2);
                }
                else
                {
                    Main.Map[0].data.setTint(4);
                    Main.Map[1].data.setTint(5);
                }
            }
        }

        public override void ResetCursor()
        {
            //Adjust image and move mouse to the center
            OpenTK.Input.Mouse.SetPosition(glControl1.Right / 2, glControl1.Bottom / 2);
            lastMousePos = new OpenTK.Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
        }

        public void setBlock()
        {//UPDATES THE DISPLAYED BLOCK COUNT
            label10.Text = "Block Count: " + Main.Map.Count.ToString();
            Refresh();
        }
        public void setName()
        {//UPDATES THE DISPLAYED NAME OF THE MAP
            label46.Text = Main.FEmpty.Name;
        }
        private void Form1_Enter(object sender, EventArgs e)
        {
            //Reset the mouse position when the form is selected
            ResetCursor();
        }

        private void glControl1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {//Keyboard control

            base.OnKeyPress(e);

            if (e.KeyChar == 27)
            {
                //Escape
                this.Close();

            }

            switch (e.KeyChar.ToString().ToLower()[0])
            {
                case 'w'://Forward
                    Main.cam.Move(0f, 0.1f, 0f);
                    Draw.UpdateImage(Width, Height);
                    PosData();
                    break;
                case 'a'://Left
                    Main.cam.Move(-0.1f, 0f, 0f);
                    Draw.UpdateImage(Width, Height);
                    PosData();
                    break;
                case 's'://Backward
                    Main.cam.Move(0f, -0.1f, 0f);
                    Draw.UpdateImage(Width, Height);
                    PosData();
                    break;
                case 'd'://Right
                    Main.cam.Move(0.1f, 0f, 0f);
                    Draw.UpdateImage(Width, Height);
                    PosData();
                    break;
                case 'e'://Up
                    Main.cam.Move(0f, 0f, 0.1f);
                    Draw.UpdateImage(Width, Height);
                    PosData();
                    break;
                case 'q'://Down
                    Main.cam.Move(0f, 0f, -0.1f);
                    Draw.UpdateImage(Width, Height);
                    PosData();
                    break;
                case 'u'://Zoom In
                    if (zo)
                    {
                        Main.cam.Zoom -= 40;
                        Main.cam.Speed /= 3;
                        Main.cam.Sensitivity /= 3;
                        zo = false;
                    }
                    Draw.UpdateImage(Width, Height);
                    break;
                case 'i'://Zoom Out
                    if (!zo)
                    {
                        Main.cam.Zoom += 40;
                        Main.cam.Speed *= 3;
                        Main.cam.Sensitivity *= 3;
                        zo = true;
                    }
                    Draw.UpdateImage(Width, Height);
                    break;

                case 'j'://Previous Iteration
                    LastIt();
                    break;
                case 'k'://Next Iteration
                    NextIt();
                    break;
                case 'm'://Mini Map
                    if (Main.MiniMap)
                    {
                        Main.MiniMap = false;
                        Main.Map = Main.FEmpty.hollow().toDisplay();
                    }
                    else
                    {
                        Main.MiniMap = true;
                    }
                    break;
            }
        }

        private void LastIt()
        {
            //Moves scene to previous iteration
            scene--;
            updateScene();
        }

        private void NextIt()
        {
            //Moves scene to next iteration
            //Main.Map = Main.FEmpty.Senses(Main.FlashVec[1]);
            Main.Time(1);
            scene++;
            //updateScene();
        }

        public void updateScene()
        {
            switch (scene)
            {
                case 0:
                    Main.Map = Main.FEmpty.hollow().toDisplay();
                    break;
                //case 1:
                //    Main.Map = Main.FEmpty.Senses(Main.FlashVec[1]);
                //    break;
                //case 2:
                //    Main.Map = Main.IA[0].Inpt;
                //    break;
                case 1:

                    //foreach (OpenTK.Vector3 point in Main.IA[0].location())
                    //{
                    //    Console.Out.WriteLine(point.ToStringS());
                    //}
                    //Console.Out.WriteLine("****");
                    break;
            }

        }
        public void PosData()
        {//UPDATES THE DISPLAYED COORDINATE DATA
            label3.Text = /*"x = "*/ "X: " + Math.Round(Main.cam.GetXPos() - 1, 2).ToString();
            label4.Text = /*"y = "*/ "Y: " + Math.Round(Main.cam.GetYPos() - 1, 2).ToString();
            label5.Text = /*"z = "*/ "Z: " + Math.Round((Main.cam.GetZPos() * -1) - 1, 2).ToString();

            label6.Text = /*"x = "*/ "X: " + Math.Round(Main.cam.GetXAng(), 2).ToString();
            label7.Text = /*"y = "*/ "Y: " + Math.Round(Main.cam.GetYAng(), 2).ToString();
            label8.Text = /*"z = "*/ "Z: " + Math.Round(Main.cam.GetZAng(), 2).ToString();
            if (Main.MiniMap)
            {
                Room mMap = Main.FEmpty.getObjectsAt(Main.cam.Position.offSet(-Main.Range, -Main.Range, -Main.Range).Round(), Main.cam.Position.offSet(Main.Range, Main.Range, Main.Range).Round());
                mMap.offset = Main.cam.Position.offSet(-Main.Range, -Main.Range, -Main.Range).Round();
                Main.Map = mMap.toDisplay();
                if (Main.Flash > 0)
                {
                    Main.addFlash();
                }
            }

        }
        public void Rotate()
        {//ROTATES THE CAMERA
            //Adjusts image based on mouse positioning
            if (mouselock)
            {
                delta = lastMousePos - new OpenTK.Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);

                ResetCursor();
                Draw.UpdateImage(Width, Height);

            }
            Main.cam.AddRotation(delta.X, delta.Y);
            PosData();
        }


        private void Form1_DoubleClick(object sender, EventArgs e)
        {//LOCKS MOUSE POSITION FOR CAMERA CONTROL
            if (mouselock)
            {
                mouselock = false;
                tabControl1.Show();

            }
            else
            {
                mouselock = true;
                tabControl1.Hide();
            }
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {//RESTORES CURSOR CONTROL AT FORM LOSE FOCUS
            mouselock = false;
            tabControl1.Show();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {//ADJUSTS CAMERA DIRECTIONAL MOVEMENT SPEED
            Main.cam.Speed = trackBar1.Value * .1f;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {//ADJUST CAMERA ANGLE MOVEMENT SPEED
            Main.cam.Sensitivity = trackBar2.Value * .001f;
        }


        private void button1_Click(object sender, EventArgs e)
        {//MOVES TO PREVIOUS ITERATION
            LastIt();
        }

        private void button2_Click(object sender, EventArgs e)
        {//MOVES TO NEXT ITERATION
            NextIt();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Main.Range = trackBar3.Value;
        }

        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {//ENSURES THAT ONLY ONE RADIO BUTTON IS SELECTED
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {//ENSURES THAT ONLY ONE RADIO BUTTON IS SELECTED
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {//ENSURES THAT ONLY ONE RADIO BUTTON IS SELECTED
            if (radioButton3.Checked)
            {
                radioButton4.Checked = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {//ENSURES THAT ONLY ONE RADIO BUTTON IS SELECTED
            if (radioButton4.Checked)
            {
                radioButton3.Checked = false;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {//ENSURES THAT ONLY ONE RADIO BUTTON IS SELECTED
            if (radioButton5.Checked)
            {
                radioButton6.Checked = false;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {//ENSURES THAT ONLY ONE RADIO BUTTON IS SELECTED
            if (radioButton6.Checked)
            {
                radioButton5.Checked = false;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {//ENSURES THAT ONLY ONE RADIO BUTTON IS SELECTED
            if (radioButton7.Checked)
            {
                radioButton8.Checked = false;
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {//ENSURES THAT ONLY ONE RADIO BUTTON IS SELECTED
            if (radioButton8.Checked)
            {
                radioButton7.Checked = false;
            }
        }
                

        private void Render_Click(object sender, EventArgs e)
        {//GENERATE MAP
            Main.createRoom();
        }

        private void Load_Click(object sender, EventArgs e)
        {//OPEN LOAD DIALOG
            Main.Stored = Main.FEmpty.Clone();
            openFileDialog1.ShowDialog();
        }

        private void Save_Click(object sender, EventArgs e)
        {//OPEN SAVE DIALOG
            saveFileDialog1.FileName = Main.FEmpty.Name;
            saveFileDialog1.ShowDialog();

        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {//LOAD FILE
            string path = openFileDialog1.FileName;
            Main.path[2] = path;
            Main.FEmpty = Main.convertToRoom(File.Read(Main.path[2]));
            Main.FEmpty.Name = path.Substring(path.LastIndexOf('\\') + 1, path.LastIndexOf('.') - path.LastIndexOf('\\') - 1);
            Main.Refresh();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {//SAVE MAP
            Main.path[1] = saveFileDialog1.FileName;
            File.Clear(Main.path[1]);
            File.Write(Main.path[1], Main.FEmpty.getRoomSize() + "\r\n" + Main.FEmpty.ToString());
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {//SHOWS THE OBJECT SELECTORS WHEN TAB 3 IS ACTIVE

            Main.Flash = 0;
            for (int i = 0; i <= 2; i++)
            {
                if (Main.Map.Count > 0)
                {
                    if (Main.Map[0].data.Symbol.Equals('_'))
                    {
                        Main.Map.RemoveAt(0);
                    }
                }
            }

            if (tabControl1.SelectedIndex == 2)
            {
                Main.Flash = 2;
                Main.addFlash();

                BlueX.Value = (int)Main.FlashVec[0].X;
                BlueY.Value = (int)Main.FlashVec[0].Y;
                BlueZ.Value = (int)Main.FlashVec[0].Z;

                RedX.Value = (int)Main.FlashVec[1].X;
                RedY.Value = (int)Main.FlashVec[1].Y;
                RedZ.Value = (int)Main.FlashVec[1].Z;

                RoomX.Value = Main.FEmpty.getXDim();
                RoomY.Value = Main.FEmpty.getYDim();
                RoomZ.Value = Main.FEmpty.getZDim();

            }
            if (tabControl1.SelectedIndex == 3)
            {
                Main.Flash = 1;
                Main.addFlash();

                BlueX2.Value = (int)Main.FlashVec[1].X;
                BlueY2.Value = (int)Main.FlashVec[1].Y;
                BlueZ2.Value = (int)Main.FlashVec[1].Z;


            }
        }


        private void BlueX_ValueChanged(object sender, EventArgs e)
        {//UPDATES THE BLUE X VALUE
            int val = (int)BlueX.Value;
            int comp = Main.FEmpty.getXDim() - 1;
            if (val <= comp)
            {
                Main.Map[0].Pos.X = val;
                Main.FlashVec[0].X = val;
            }
            else
            {
                Main.Map[0].Pos.X = comp;
                Main.FlashVec[0].X = comp;
                BlueX.Value = comp;
            }
        }

        private void BlueY_ValueChanged(object sender, EventArgs e)
        {//UPDATES THE BLUE Y VALUE
            int val = (int)BlueY.Value;
            int comp = Main.FEmpty.getYDim() - 1;
            if (val <= comp)
            {
                Main.Map[0].Pos.Y = val;
                Main.FlashVec[0].Y = val;
            }
            else
            {
                Main.Map[0].Pos.Y = comp;
                Main.FlashVec[0].Y = comp;
                BlueY.Value = comp;
            }
        }

        private void BlueZ_ValueChanged(object sender, EventArgs e)
        {//UPDATES TEHE BLUE Z VALUE
            int val = (int)BlueZ.Value;
            int comp = Main.FEmpty.getZDim() - 1;
            if (val <= comp)
            {
                Main.Map[0].Pos.Z = val;
                Main.FlashVec[0].Z = val;
            }
            else
            {
                Main.Map[0].Pos.Z = comp;
                Main.FlashVec[0].Z = comp;
                BlueZ.Value = comp;
            }
        }

        private void BlueX2_ValueChanged(object sender, EventArgs e)
        {//UPDATES THE BLUE X VALUE
            int val = (int)BlueX2.Value;
            int comp = Main.FEmpty.getXDim() - 1;
            if (val <= comp)
            {
                Main.Map[0].Pos.X = val;
                Main.FlashVec[1].X = val;
            }
            else
            {
                Main.Map[0].Pos.X = comp;
                Main.FlashVec[1].X = comp;
                BlueX2.Value = comp;
            }
            Blue2_ValueChanged();
        }

        private void BlueY2_ValueChanged(object sender, EventArgs e)
        {//UPDATES THE BLUE Y VALUE
            int val = (int)BlueY2.Value;
            int comp = Main.FEmpty.getYDim() - 1;
            if (val <= comp)
            {
                Main.Map[0].Pos.Y = val;
                Main.FlashVec[1].Y = val;
            }
            else
            {
                Main.Map[0].Pos.Y = comp;
                Main.FlashVec[1].Y = comp;
                BlueY2.Value = comp;
            }
            Blue2_ValueChanged();
        }

        private void BlueZ2_ValueChanged(object sender, EventArgs e)
        {//UPDATES TEHE BLUE Z VALUE
            int val = (int)BlueZ2.Value;
            int comp = Main.FEmpty.getZDim() - 1;
            if (val <= comp)
            {
                Main.Map[0].Pos.Z = val;
                Main.FlashVec[1].Z = val;
            }
            else
            {
                Main.Map[0].Pos.Z = comp;
                Main.FlashVec[1].Z = comp;
                BlueZ2.Value = comp;
            }
            Blue2_ValueChanged();
        }

        private void Blue2_ValueChanged()
        {
            label49.Text = Main.FEmpty.symToText(Main.FlashVec[1]).ToString();
            if (label49.Text.Equals("Character"))
            {
                UpdateI();
            }
            else
            {
                UpdateI();
                Lobotomy.Enabled = false;
            }
        }

        private void RedX_ValueChanged(object sender, EventArgs e)
        {//UPDATES THE RED X VALUE
            int val = (int)RedX.Value;
            int comp = Main.FEmpty.getXDim() - 1;
            if (val <= comp)
            {
                Main.Map[1].Pos.X = val;
                Main.FlashVec[1].X = val;
            }
            else
            {
                Main.Map[1].Pos.X = comp;
                Main.FlashVec[1].X = comp;
                RedX.Value = comp;
            }
        }

        private void RedY_ValueChanged(object sender, EventArgs e)
        {//UPDATES THE RED Y VALUE
            int val = (int)RedY.Value;
            int comp = Main.FEmpty.getYDim() - 1;
            if (val <= comp)
            {
                Main.Map[1].Pos.Y = val;
                Main.FlashVec[1].Y = val;
            }
            else
            {
                Main.Map[1].Pos.Y = comp;
                Main.FlashVec[1].Y = comp;
                RedY.Value = comp;
            }
        }

        private void RedZ_ValueChanged(object sender, EventArgs e)
        {//UPDATES THE RED Z VALUE
            int val = (int)RedZ.Value;
            int comp = Main.FEmpty.getZDim() - 1;
            if (val <= comp)
            {
                Main.Map[1].Pos.Z = val;
                Main.FlashVec[1].Z = val;
            }
            else
            {
                Main.Map[1].Pos.Z = comp;
                Main.FlashVec[1].Z = comp;
                RedZ.Value = comp;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {//SETS THE PRIMARY BLOCK TYPE FOR THE MAP EDITOR
            block[0] = Main.Archives[comboBox1.SelectedIndex].Symbol;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {//SETS THE SECONDARY BLOCK TYPE FOR THE MAP EDITOR

            block[1] = Main.Archives[comboBox2.SelectedIndex].Symbol;
        }

        private void button3_Click(object sender, EventArgs e)
        {//ADDS A BLOCK
            Main.Stored = Main.FEmpty.Clone();
            if (radioButton1.Checked)
            {
                Main.FEmpty.setObject(Main.FlashVec[0], block[0]);
                Main.Refresh();
            }
            else if (radioButton2.Checked)
            {
                Main.FEmpty.setObject(Main.FlashVec[1], block[0]);
                Main.Refresh();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {//REMOVES A BLOCK
            Main.Stored = Main.FEmpty.Clone();
            if (radioButton1.Checked)
            {
                Main.FEmpty.removeObject(Main.FlashVec[0]);
                Main.Refresh();
            }
            else if (radioButton2.Checked)
            {
                Main.FEmpty.removeObject(Main.FlashVec[1]);
                Main.Refresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {//ADDS BLOCKS FROM RED TO BLUE
            Main.Stored = Main.FEmpty.Clone();
            Main.FEmpty.addToArea(Main.FlashVec[0], Main.FlashVec[1], block[0]);
            Main.Refresh();
        }


        private void button5_Click(object sender, EventArgs e)
        {//REPLACES BLOCKS FROM RED TO BLUE
            Main.Stored = Main.FEmpty.Clone();
            Main.FEmpty.addArea(Main.FlashVec[0], Main.FlashVec[1], block[0]);
            Main.Refresh();
        }
        private void button7_Click(object sender, EventArgs e)
        {//EXCHANGES BLOCK2 FOR BLOCK 1 FROM RED TO BLUE
            Main.Stored = Main.FEmpty.Clone();
            Main.FEmpty.swapArea(Main.FlashVec[0], Main.FlashVec[1], block[1], block[0]);
            Main.Refresh();
        }
        private void button11_Click(object sender, EventArgs e)
        {//SETS ALL NON-EMPTY BLOCKS TO BLOCK 1 FROM RED TO BLUE
            Main.Stored = Main.FEmpty.Clone();
            Main.FEmpty.replaceArea(Main.FlashVec[0], Main.FlashVec[1], block[0]);
            Main.Refresh();
        }

        private void button10_Click(object sender, EventArgs e)
        {//ADDS BLOCKS IN RADIUS
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton3.Checked)
                {
                    Main.FEmpty.addToRadiusI(Main.FlashVec[0], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
                else if (radioButton4.Checked)
                {
                    Main.FEmpty.addToRadiusI(Main.FlashVec[1], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton3.Checked)
                {
                    Main.FEmpty.addToRadius(Main.FlashVec[0], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
                else if (radioButton4.Checked)
                {
                    Main.FEmpty.addToRadius(Main.FlashVec[1], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {//REPLACES BLOCKS IN RADIUS
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton3.Checked)
                {
                    Main.FEmpty.addRadiusI(Main.FlashVec[0], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
                else if (radioButton4.Checked)
                {
                    Main.FEmpty.addRadiusI(Main.FlashVec[1], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton3.Checked)
                {
                    Main.FEmpty.addRadius(Main.FlashVec[0], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
                else if (radioButton4.Checked)
                {
                    Main.FEmpty.addRadius(Main.FlashVec[1], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
            }
        }



        private void button12_Click(object sender, EventArgs e)
        {//REPLACES BLOCKS AROUND RADIUS
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton3.Checked)
                {
                    Main.FEmpty.replaceRadiusI(Main.FlashVec[0], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
                else if (radioButton4.Checked)
                {
                    Main.FEmpty.replaceRadiusI(Main.FlashVec[1], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton3.Checked)
                {
                    Main.FEmpty.replaceRadius(Main.FlashVec[0], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
                else if (radioButton4.Checked)
                {
                    Main.FEmpty.replaceRadius(Main.FlashVec[1], (int)numericUpDown1.Value, block[0]);
                    Main.Refresh();
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {//SETS DATA FOR SPHERE RADIUS
            int comp = Main.FEmpty.getXDim() - 1;
            if (!(numericUpDown1.Value <= comp))
            {
                numericUpDown1.Value = comp;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            Main.path[3] = saveFileDialog2.FileName;
            File.Clear(Main.path[3]);
            File.Write(Main.path[3], Main.FlashVec[0].distanceTo(Main.FlashVec[1]).offSetS(1, 1, 1).ToPositive() + "\r\n" + Main.FEmpty.getObjectsAt(Main.FlashVec[0], Main.FlashVec[1]).ToString());
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            Main.path[4] = openFileDialog2.FileName;
            label43.Text = openFileDialog2.FileName;
            OpenTK.Vector3 temp = Main.FlashVec[0];
            OpenTK.Vector3 temp2 = Main.FlashVec[1];
            Main.Scene = Main.convertToRoom(File.Read(Main.path[4]));
            Main.siz = Main.FEmpty.getRoomSize();
            Main.FlashVec[0] = temp;
            Main.FlashVec[1] = temp2;
            Main.Refresh();
        }

        private void button13_Click(object sender, EventArgs e)
        {//ADDS A LOADED SCENE OBJECT TO THE MAP
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton5.Checked)
                {
                    Main.FEmpty.addToSceneI(Main.Scene, Main.FlashVec[0], block[0]);
                    Main.Refresh();
                }
                else if (radioButton6.Checked)
                {
                    Main.FEmpty.addToSceneI(Main.Scene, Main.FlashVec[1], block[0]);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton5.Checked)
                {
                    Main.FEmpty.addToScene(Main.Scene, Main.FlashVec[0]);
                    Main.Refresh();
                }
                else if (radioButton6.Checked)
                {
                    Main.FEmpty.addToScene(Main.Scene, Main.FlashVec[1]);
                    Main.Refresh();
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {//REPLACES BLOCKS ACCORDING TO SCENE FILE TYPE
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton5.Checked)
                {
                    Main.FEmpty.addSceneI(Main.Scene, Main.FlashVec[0], block[0]);
                    Main.Refresh();
                }
                else if (radioButton6.Checked)
                {
                    Main.FEmpty.addSceneI(Main.Scene, Main.FlashVec[1], block[0]);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton5.Checked)
                {
                    Main.FEmpty.addScene(Main.Scene, Main.FlashVec[0]);
                    Main.Refresh();
                }
                else if (radioButton6.Checked)
                {
                    Main.FEmpty.addScene(Main.Scene, Main.FlashVec[1]);
                    Main.Refresh();
                }
            }
        }



        private void button23_Click(object sender, EventArgs e)
        {//ADDS THE SHAPE OF A LOADED SCENE OBJECT TO THE MAP USING BLOCK 1
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton5.Checked)
                {
                    Main.FEmpty.addToSceneI(Main.Scene, Main.FlashVec[0], block[0]);
                    Main.Refresh();
                }
                else if (radioButton6.Checked)
                {
                    Main.FEmpty.addToSceneI(Main.Scene, Main.FlashVec[1], block[0]);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton5.Checked)
                {
                    Main.FEmpty.addToSceneB(Main.Scene, Main.FlashVec[0], block[0]);
                    Main.Refresh();
                }
                else if (radioButton6.Checked)
                {
                    Main.FEmpty.addToSceneB(Main.Scene, Main.FlashVec[1], block[0]);
                    Main.Refresh();
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {//REPLACES THE SHAPE OF A LOADED SCENE OBJECT TO THE MAP USING BLOCK 1
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton5.Checked)
                {
                    Main.FEmpty.addSceneI(Main.Scene, Main.FlashVec[0], block[0]);
                    Main.Refresh();
                }
                else if (radioButton6.Checked)
                {
                    Main.FEmpty.addSceneI(Main.Scene, Main.FlashVec[1], block[0]);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton5.Checked)
                {
                    Main.FEmpty.addSceneB(Main.Scene, Main.FlashVec[0], block[0]);
                    Main.Refresh();
                }
                else if (radioButton6.Checked)
                {
                    Main.FEmpty.addSceneB(Main.Scene, Main.FlashVec[1], block[0]);
                    Main.Refresh();
                }
            }
        }
        public bool Check2()
        {
            return checkBox2.Checked;
        }
        public bool Check3()
        {
            return checkBox3.Checked;
        }
        public bool Check4()
        {
            return checkBox4.Checked;
        }
        public bool Check5()
        {
            return checkBox5.Checked;
        }
        public int getRad()
        {
            return (int)numericUpDown1.Value;
        }
        public int getS()
        {
            return (int)numericUpDown2.Value - 1;
        }
        public string RB3()
        {
            if (radioButton3.Checked)
            {
                return "Red";
            }
            else
            {
                return "Blue";
            }
        }
        public string RB4()
        {
            if (radioButton5.Checked)
            {
                return "Red";
            }
            else
            {
                return "Blue";
            }
        }
        public string RB5()
        {
            if (radioButton7.Checked)
            {
                return "Red";
            }
            else
            {
                return "Blue";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {//CHANGES THE SIZE OF THE ROOM
            Main.Stored = Main.FEmpty.Clone();
            Main.FEmpty = Main.FEmpty.changeSize((int)RoomX.Value, (int)RoomY.Value, (int)RoomZ.Value);
            Main.siz = Main.FEmpty.getRoomSize();
            Main.Refresh();
        }

        private void button14_Click(object sender, EventArgs e)
        {//ROTATES THE SELECTED AREA
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton7.Checked)
                {
                    Room rot = Main.FEmpty.RotD(Main.FlashVec[0], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
                else if (radioButton8.Checked)
                {
                    Room rot = Main.FEmpty.RotD(Main.FlashVec[1], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton7.Checked)
                {
                    Room rot = Main.FEmpty.RotU(Main.FlashVec[0], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
                else if (radioButton8.Checked)
                {
                    Room rot = Main.FEmpty.RotU(Main.FlashVec[1], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {//ROTATES THE SELECTED AREA
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton7.Checked)
                {
                    Room rot = Main.FEmpty.RotL(Main.FlashVec[0], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
                else if (radioButton8.Checked)
                {
                    Room rot = Main.FEmpty.RotL(Main.FlashVec[1], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton7.Checked)
                {
                    Room rot = Main.FEmpty.RotR(Main.FlashVec[0], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
                else if (radioButton8.Checked)
                {
                    Room rot = Main.FEmpty.RotR(Main.FlashVec[1], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
            }


        }

        private void button16_Click(object sender, EventArgs e)
        {//ROTATES THE SELECTED AREA
            Main.Stored = Main.FEmpty.Clone();
            if (checkBox1.Checked)
            {
                if (radioButton7.Checked)
                {
                    Room rot = Main.FEmpty.RotSI(Main.FlashVec[0], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
                else if (radioButton8.Checked)
                {
                    Room rot = Main.FEmpty.RotSI(Main.FlashVec[1], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
            }
            else
            {
                if (radioButton7.Checked)
                {
                    Room rot = Main.FEmpty.RotS(Main.FlashVec[0], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
                else if (radioButton8.Checked)
                {
                    Room rot = Main.FEmpty.RotS(Main.FlashVec[1], (int)numericUpDown2.Value);
                    Main.FEmpty.addScene(rot, rot.offset);
                    Main.Refresh();
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {//INVERTS THE SELECTED AREA
            Main.Stored = Main.FEmpty.Clone();

            if (radioButton7.Checked)
            {
                Room rot = Main.FEmpty.InvertUD(Main.FlashVec[0], (int)numericUpDown2.Value);
                Main.FEmpty.addScene(rot, rot.offset);
                Main.Refresh();
            }
            else if (radioButton8.Checked)
            {
                Room rot = Main.FEmpty.InvertUD(Main.FlashVec[1], (int)numericUpDown2.Value);
                Main.FEmpty.addScene(rot, rot.offset);
                Main.Refresh();
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {//INVERTS THE SELECTED AREA
            Main.Stored = Main.FEmpty.Clone();

            if (radioButton7.Checked)
            {
                Room rot = Main.FEmpty.InvertLR(Main.FlashVec[0], (int)numericUpDown2.Value);
                Main.FEmpty.addScene(rot, rot.offset);
                Main.Refresh();
            }
            else if (radioButton8.Checked)
            {
                Room rot = Main.FEmpty.InvertLR(Main.FlashVec[1], (int)numericUpDown2.Value);
                Main.FEmpty.addScene(rot, rot.offset);
                Main.Refresh();
            }


        }

        private void button26_Click(object sender, EventArgs e)
        {//INVERTS THE SELECTED AREA
            Main.Stored = Main.FEmpty.Clone();
            if (radioButton7.Checked)
            {
                Room rot = Main.FEmpty.InvertFB(Main.FlashVec[0], (int)numericUpDown2.Value);
                Main.FEmpty.addScene(rot, rot.offset);
                Main.Refresh();
            }
            else if (radioButton8.Checked)
            {
                Room rot = Main.FEmpty.InvertFB(Main.FlashVec[1], (int)numericUpDown2.Value);
                Main.FEmpty.addScene(rot, rot.offset);
                Main.Refresh();

            }
        }

        private void button27_Click(object sender, EventArgs e)
        {//UNDOES THE LAST COMMAND
            Room temp = Main.FEmpty;
            Main.FEmpty = Main.Stored;
            Main.Stored = temp;
            Main.Refresh();
        }

        private void Lobotomy_Click(object sender, EventArgs e)
        {
            if (Intelligence.findIntel(Main.FlashVec[1]).Equals(Main.potato))
            {
                Main.IA.Add(new Intelligence(Main.FlashVec[1]));
                UpdateI();
            }
        }
        private void UpdateI()
        {
            Intelligence Curr = Intelligence.findIntel(Main.FlashVec[1]);
            if (!Curr.Equals(Main.potato))
            {
                Lobotomy.Enabled = false;

                Smell.Enabled = true;
                Sight.Enabled = true;
                Hearing.Enabled = true;
                Speed.Enabled = true;
                Energy.Enabled = true;
                Health.Enabled = true;
                Hunger.Enabled = true;
                Thirst.Enabled = true;

                label50.Enabled = true;
                label51.Enabled = true;
                label52.Enabled = true;
                label53.Enabled = true;
                label55.Enabled = true;
                label56.Enabled = true;
                label57.Enabled = true;
                label58.Enabled = true;

                Smell.Value = Curr.stat[0];
                Sight.Value = Curr.stat[1];
                Hearing.Value = Curr.stat[2];
                Speed.Value = Curr.stat[3];
                Energy.Value = Curr.stat[4];
                Health.Value = Curr.stat[5];
                Hunger.Value = Curr.stat[6];
                Thirst.Value = Curr.stat[7];

            }
            else
            {
                Lobotomy.Enabled = true;

                Smell.Enabled = false;
                Sight.Enabled = false;
                Hearing.Enabled = false;
                Speed.Enabled = false;
                Energy.Enabled = false;
                Health.Enabled = false;
                Hunger.Enabled = false;
                Thirst.Enabled = false;

                label50.Enabled = false;
                label51.Enabled = false;
                label52.Enabled = false;
                label53.Enabled = false;
                label55.Enabled = false;
                label56.Enabled = false;
                label57.Enabled = false;
                label58.Enabled = false;

                Smell.Value = 0;
                Sight.Value = 0;
                Hearing.Value = 0;
                Speed.Value = 0;
                Energy.Value = 0;
                Health.Value = 0;
                Hunger.Value = 0;
                Thirst.Value = 0;
            }
        }

        private void Smell_ValueChanged(object sender, EventArgs e)
        {
            Intelligence.findIntel(Main.FlashVec[1]).stat[0] = (int)Smell.Value;
        }
        private void Sight_ValueChanged(object sender, EventArgs e)
        {
            Intelligence.findIntel(Main.FlashVec[1]).stat[1] = (int)Sight.Value;
        }
        private void Hearing_ValueChanged(object sender, EventArgs e)
        {
            Intelligence.findIntel(Main.FlashVec[1]).stat[2] = (int)Hearing.Value;
        }
        private void Speed_ValueChanged(object sender, EventArgs e)
        {
            Intelligence.findIntel(Main.FlashVec[1]).stat[3] = (int)Speed.Value;
        }
        private void Health_ValueChanged(object sender, EventArgs e)
        {
            Intelligence.findIntel(Main.FlashVec[1]).stat[4] = (int)Health.Value;
        }
        private void Energy_ValueChanged(object sender, EventArgs e)
        {
            Intelligence.findIntel(Main.FlashVec[1]).stat[5] = (int)Energy.Value;
        }
        private void Hunger_ValueChanged(object sender, EventArgs e)
        {
            Intelligence.findIntel(Main.FlashVec[1]).stat[6] = (int)Hunger.Value;
        }
        private void Thirst_ValueChanged(object sender, EventArgs e)
        {
            Intelligence.findIntel(Main.FlashVec[1]).stat[7] = (int)Thirst.Value;
        }

       

        private void reply(String otpt)
        {
            textBox1.Text = textBox1.Text + "\n" + otpt;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            reply("Enter")
        }


    }
}







