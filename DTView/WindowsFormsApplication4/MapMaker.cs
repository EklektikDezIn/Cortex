using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextGen
{
    public partial class MapMaker : Form
    {
        public MapMaker()
        {
            InitializeComponent();
            UpdateTools();
        }
        private void UpdateTools()
        {//UPDATE VALUES TO CURRENT SETTINGS
            int size = Main.CalcSiz();
            //Update Trackbar Maximums
            trackBar1.Maximum = size * 3 / 4;
            trackBar2.Maximum = size * 3 / 4;
            trackBar3.Maximum = size * 3 / 4;
            trackBar4.Maximum = size * 3 / 4;
            trackBar8.Maximum = size;
            trackBar9.Maximum = size;
            trackBar5.Value = Main.RLevel;
            label5.Text = Main.RLevel.ToString();

            //Update Trackbar Values
            if (Main.corners[0] > trackBar1.Maximum) { trackBar1.Value = trackBar1.Maximum; Main.corners[0] = trackBar1.Maximum; } else { trackBar1.Value = Main.corners[0]; }
            if (Main.corners[1] > trackBar2.Maximum) { trackBar2.Value = trackBar2.Maximum; Main.corners[1] = trackBar2.Maximum; } else { trackBar2.Value = Main.corners[1]; }
            if (Main.corners[2] > trackBar3.Maximum) { trackBar3.Value = trackBar3.Maximum; Main.corners[2] = trackBar3.Maximum; } else { trackBar3.Value = Main.corners[2]; }
            if (Main.corners[3] > trackBar4.Maximum) { trackBar4.Value = trackBar4.Maximum; Main.corners[3] = trackBar4.Maximum; } else { trackBar4.Value = Main.corners[3]; }
            trackBar6.Value = (int)Main.TreeProb;
            trackBar7.Value = (int)Main.BushProb;
            trackBar8.Value = (int)Main.WaterHeight;
            trackBar9.Value = (int)Main.LavaHeight;
            trackBar10.Value = (int)Main.CloudProb;
            trackBar11.Value = (int)Main.CaveProb;
            trackBar12.Value = (int)Main.roughness;
            trackBar13.Value = (int)Main.BeachWidth;

            //Updtate Lable Text
            label1.Text = Main.corners[0].ToString();
            label2.Text = Main.corners[1].ToString();
            label3.Text = Main.corners[2].ToString();
            label4.Text = Main.corners[3].ToString();

            label6.Text = Main.TreeProb.ToString();
            label7.Text = Main.BushProb.ToString();

            label8.Text = Main.WaterHeight.ToString();
            label14.Text = Main.LavaHeight.ToString();


            label16.Text = Main.CloudProb.ToString();
            label17.Text = Main.CaveProb.ToString();

            label22.Text = Main.BeachWidth.ToString();
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {//UPDATE ROOM LEVEL
            Main.RLevel = trackBar5.Value;
            UpdateTools();
        }

        

        private void trackBar1_Scroll(object sender, EventArgs e)
        {//UPDATE CORNER 1
            Main.corners[0] = trackBar1.Value;
            label1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {//UPDATE CORNER 2
            Main.corners[1] = trackBar2.Value;
            label2.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {//UPDATE CORNER 3
            Main.corners[2] = trackBar3.Value;
            label3.Text = trackBar3.Value.ToString();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {//UPDATE CORNER 4
            Main.corners[3] = trackBar4.Value;
            label4.Text = trackBar4.Value.ToString();
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {//UPDTATE TREE PROBABILITY
            Main.TreeProb = trackBar6.Value;
            label6.Text = trackBar6.Value.ToString();
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {//UPDATE BUSH PROBABILITY
            Main.BushProb = trackBar7.Value;
            label7.Text = trackBar7.Value.ToString();
        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {//UPDATE WATER HEIGHT
            Main.WaterHeight = trackBar8.Value;
            label8.Text = trackBar8.Value.ToString();
        }

        private void trackBar9_Scroll(object sender, EventArgs e)
        {//UPDATE LAVA HEIGHT
            Main.LavaHeight = trackBar9.Value;
            label14.Text = trackBar9.Value.ToString();
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {//UPDATE CLOUD PROBABILITY
            Main.CloudProb = trackBar10.Value;
            label16.Text = trackBar10.Value.ToString();
        }

        private void trackBar11_Scroll(object sender, EventArgs e)
        {//UPDATE CAVE PROBABILITY
            Main.CaveProb = trackBar11.Value;
            label17.Text = trackBar11.Value.ToString();
        }
        private void trackBar12_Scroll(object sender, EventArgs e)
        {//UPDATE TERRAIN ROUGHNESS
            Main.roughness = trackBar12.Value;
            //label17.Text = trackBar11.Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {//GENERATE MAP
            Main.createRoom();
            Hide();
            Main.MapOpen = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {//EXIT MAP MAKER
            Hide();
            Main.MapOpen = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {//OPEN LOAD DIALOG
            openFileDialog1.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {//OPEN SAVE DIALOG
            saveFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {//LOAD FILE
            Main.path[2] = openFileDialog1.FileName;
            Main.Map = Main.convertToRoom(File.Read(Main.path[2])).toDisplay();
            Hide();
            Main.MapOpen = false;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {//SAVE MAP
            Main.path[1] = saveFileDialog1.FileName;
            File.Clear(Main.path[1]);
            List<string> ents = new List<string>();
            ents.Add(Main.FEmpty.getRoomSize().ToString() + "\r\n");
            foreach (Entity ent in Main.Map)
            {
               ents.Add(ent + "\r\n");
            }
            File.Write(Main.path[1], ents);
            Hide();
            Main.MapOpen = false;
        }

        private void MapMaker_FormClosed(object sender, FormClosedEventArgs e)
        {//RESTORES OPEN ABILITY
            Main.MapOpen = false;
        }

        private void trackBar13_Scroll(object sender, EventArgs e)
        {//UPDATE BEACH WIDTH
            Main.BeachWidth = trackBar13.Value;
            label22.Text = trackBar13.Value.ToString();
        }

        

        

    }
}
