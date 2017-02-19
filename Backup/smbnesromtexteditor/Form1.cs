//programmed by Shawn M. Crawford, 06/18/2010
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace smbnesromtexteditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string temphex, tempascii, filename, path;
        string rctext = "";
        string smbAsciiRet = "";

        string[] ms = new string[0x24];
        byte[] mb = new byte[0x24];
        int[] md = new int[0x24];
        string[] ms_final = new string[0x24];
        string[] msw = new string[0x24];
        string smbHexRet = "";
        int i = 0;
        string newgcString;
        int x = 0, y = 0, q = 0, offset = 0x0, offsetwr = 0x0;

        private void Form1_Load(object sender, EventArgs e)
        {
            disassembleToolStripMenuItem.Enabled = false;
            disButton.Enabled = false;
            updateButton.Enabled = false;
            updateToolStripMenuItem.Enabled = false;
            fnTextBox.Enabled = false;

            smb1TextBox.MaxLength = 0x5;
            smb2TextBox.MaxLength = 0x5;
            smb3TextBox.MaxLength = 0x4;
            smb4TextBox.MaxLength = 0x5;
            smb5TextBox.MaxLength = 0x5;
            smb6TextBox.MaxLength = 0x7;
            smb7TextBox.MaxLength = 0x5;
            smb8TextBox.MaxLength = 0x9;
            smb9TextBox.MaxLength = 0x15;
            smb10TextBox.MaxLength = 0x5;
            smb11TextBox.MaxLength = 0x10;
            smb12TextBox.MaxLength = 0x10;
            smb13TextBox.MaxLength = 0x16;
            smb14TextBox.MaxLength = 0xf;
            smb15TextBox.MaxLength = 0x12;
            smb16TextBox.MaxLength = 0x1a;
            smb17TextBox.MaxLength = 0xd;
            smb18TextBox.MaxLength = 0x11;
            smb19TextBox.MaxLength = 0x24;
            smb20TextBox.MaxLength = 0xd;
            smb21TextBox.MaxLength = 0xd;
            smb22TextBox.MaxLength = 0xd;
            smb23TextBox.MaxLength = 0x4;
        }

        private void fselButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open file...";
            ofd.Filter = "nes files (*.nes)|*.nes|All files (*.*)|*.*";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fnTextBox.Text = ofd.FileName;
                disassembleToolStripMenuItem.Enabled = true;
                disButton.Enabled = true;
                updateButton.Enabled = true;
                updateToolStripMenuItem.Enabled = true;

            }
            
            
            
        
        }


        private string decodeSMBText()
        {
            string smbAscii = "";
            //textFlag = 0;

            switch (temphex)
            {
                case "24":
                    smbAscii += " ";
                    break;
                case "28":
                    smbAscii += "-";
                    break;
                case "2B":
                    smbAscii += "!";
                    break;
                case "00":
                    smbAscii += "0";
                    break;
                case "01":
                    smbAscii += "1";
                    break;
                case "02":
                    smbAscii += "2";
                    break;
                case "03":
                    smbAscii += "3";
                    break;
                case "04":
                    smbAscii += "4";
                    break;
                case "05":
                    smbAscii += "5";
                    break;
                case "06":
                    smbAscii += "6";
                    break;
                case "07":
                    smbAscii += "7";
                    break;
                case "08":
                    smbAscii += "8";
                    break;
                case "09":
                    smbAscii += "9";
                    break;
                case "0A":
                    smbAscii += "A";
                    break;
                case "0B":
                    smbAscii += "B";
                    break;
                case "0C":
                    smbAscii += "C";
                    break;
                case "0D":
                    smbAscii += "D";
                    break;
                case "0E":
                    smbAscii += "E";
                    break;
                case "0F":
                    smbAscii += "F";
                    break;
                case "10":
                    smbAscii += "G";
                    break;
                case "11":
                    smbAscii += "H";
                    break;
                case "12":
                    smbAscii += "I";
                    break;
                case "13":
                    smbAscii += "J";
                    break;
                case "14":
                    smbAscii += "K";
                    break;
                case "15":
                    smbAscii += "L";
                    break;
                case "16":
                    smbAscii += "M";
                    break;
                case "17":
                    smbAscii += "N";
                    break;
                case "18":
                    smbAscii += "O";
                    break;
                case "19":
                    smbAscii += "P";
                    break;
                case "1A":
                    smbAscii += "Q";
                    break;
                case "1B":
                    smbAscii += "R";
                    break;
                case "1C":
                    smbAscii += "S";
                    break;
                case "1D":
                    smbAscii += "T";
                    break;
                case "1E":
                    smbAscii += "U";
                    break;
                case "1F":
                    smbAscii += "V";
                    break;
                case "20":
                    smbAscii += "W";
                    break;
                case "21":
                    smbAscii += "X";
                    break;
                case "22":
                    smbAscii += "Y";
                    break;
                case "23":
                    smbAscii += "Z";
                    break;
                default:
                    smbAscii += " ";
                    //textFlag = 1;
                    break;
            }

            return smbAscii;
        }

        private void disButton_Click(object sender, EventArgs e)
        {
            if (fnTextBox.Text != "")
            {
                path = fnTextBox.Text;
                filename = fnTextBox.Text.Substring(fnTextBox.Text.LastIndexOf('\\') + 1);
                //fnTextBox.Text = filename.ToString();

                loadfile();

            }
        }



        private void loadfile()
        {
            y = 0x5;
            offset = 0x765;
            smb1TextBox.Text = readText();

            y = 0x5;
            offset = 0x76D;
            smb2TextBox.Text = readText();

            y = 0x4;
            offset = 0x774;
            smb3TextBox.Text = readText();

            y = 0x5;
            offset = 0x796;
            smb4TextBox.Text = readText();

            y = 0x5;
            offset = 0x7AB;
            smb5TextBox.Text = readText();

            y = 0x7;
            offset = 0x7B3;
            smb6TextBox.Text = readText();

            y = 0x5;
            offset = 0x7BE;
            smb7TextBox.Text = readText();

            y = 0x9;
            offset = 0x7C6;
            smb8TextBox.Text = readText();

            y = 0x15;
            offset = 0x7D3;
            smb9TextBox.Text = readText();

            y = 0x5;
            offset = 0x7FD;
            smb10TextBox.Text = readText();

            y = 0x10;
            offset = 0xD67;
            smb11TextBox.Text = readText();

            y = 0x10;
            offset = 0xD7B;
            smb12TextBox.Text = readText();

            y = 0x16;
            offset = 0xD8F;
            smb13TextBox.Text = readText();

            y = 0xf;
            offset = 0xDA8;
            smb14TextBox.Text = readText();

            y = 0x12;
            offset = 0xDBB;
            smb15TextBox.Text = readText();

            y = 0x1a;
            offset = 0xDD2;
            smb16TextBox.Text = readText();

            y = 0xd;
            offset = 0xDF1;
            smb17TextBox.Text = readText();

            y = 0x11;
            offset = 0xE02;
            smb18TextBox.Text = readText();

            y = 0x24;
            offset = 0x6E27;
            smb19TextBox.Text = readText();

            y = 0xd;
            offset = 0x9FB6;
            smb20TextBox.Text = readText();

            y = 0xd;
            offset = 0x9FC6;
            smb21TextBox.Text = readText();

            y = 0xd;
            offset = 0x9FD6;
            smb22TextBox.Text = readText();

            y = 0x4;
            offset = 0x9FE6;
            smb23TextBox.Text = readText();

            //debug
            //System.IO.File.WriteAllText(@"C:\smbtextdump.txt", smbAsciiRet);
        }




        private string readText()
        {
            smbAsciiRet = "";

            FileStream fs1 = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fs1.Seek(offset, SeekOrigin.Begin);

            x = 0;

            while (x < y)
            {
                rctext = fs1.ReadByte().ToString("X");
                //if length is single digit add a 0 ( 1 now is 01)
                if (rctext.Length == 1)
                {
                    rctext = "0" + rctext;
                }
                temphex = rctext;
                decodeSMBText();
                smbAsciiRet += decodeSMBText();

                x++;
            }
            fs1.Close();
            return smbAsciiRet;
            
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            newgcString = smb1TextBox.Text;
            newgcString = newgcString.PadRight(0x5);
            offsetwr = 0x765;
            writeByte();
            
            newgcString = smb2TextBox.Text;
            newgcString = newgcString.PadRight(0x5);
            offsetwr = 0x76D;
            writeByte();

            newgcString = smb3TextBox.Text;
            newgcString = newgcString.PadRight(0x4);
            offsetwr = 0x774;
            writeByte();

            newgcString = smb4TextBox.Text;
            newgcString = newgcString.PadRight(0x5);
            offsetwr = 0x796;
            writeByte();

            newgcString = smb5TextBox.Text;
            newgcString = newgcString.PadRight(0x5);
            offsetwr = 0x7AB;
            writeByte();

            newgcString = smb6TextBox.Text;
            newgcString = newgcString.PadRight(0x7);
            offsetwr = 0x7B3;
            writeByte();

            newgcString = smb7TextBox.Text;
            newgcString = newgcString.PadRight(0x5);
            offsetwr = 0x7BE;
            writeByte();

            newgcString = smb8TextBox.Text;
            newgcString = newgcString.PadRight(0x9);
            offsetwr = 0x7C6;
            writeByte();

            newgcString = smb9TextBox.Text;
            newgcString = newgcString.PadRight(0x15);
            offsetwr = 0x7D3;
            writeByte();

            newgcString = smb10TextBox.Text;
            newgcString = newgcString.PadRight(0x5);
            offsetwr = 0x7FD;
            writeByte();

            newgcString = smb11TextBox.Text;
            newgcString = newgcString.PadRight(0x10);
            offsetwr = 0xD67;
            writeByte();

            newgcString = smb12TextBox.Text;
            newgcString = newgcString.PadRight(0x10);
            offsetwr = 0xD7B;
            writeByte();

            newgcString = smb13TextBox.Text;
            newgcString = newgcString.PadRight(0x16);
            offsetwr = 0xD8F;
            writeByte();

            newgcString = smb14TextBox.Text;
            newgcString = newgcString.PadRight(0xf);
            offsetwr = 0xDA8;
            writeByte();

            newgcString = smb15TextBox.Text;
            newgcString = newgcString.PadRight(0x12);
            offsetwr = 0xDBB;
            writeByte();

            newgcString = smb16TextBox.Text;
            newgcString = newgcString.PadRight(0x1a);
            offsetwr = 0xDD2;
            writeByte();

            newgcString = smb17TextBox.Text;
            newgcString = newgcString.PadRight(0xd);
            offsetwr = 0xDF1;
            writeByte();

            newgcString = smb18TextBox.Text;
            newgcString = newgcString.PadRight(0x11);
            offsetwr = 0xE02;
            writeByte();

            newgcString = smb19TextBox.Text;
            newgcString = newgcString.PadRight(0x24);
            offsetwr = 0x6E27;
            writeByte();

            newgcString = smb20TextBox.Text;
            newgcString = newgcString.PadRight(0xd);
            offsetwr = 0x9FB6;
            writeByte();

            newgcString = smb21TextBox.Text;
            newgcString = newgcString.PadRight(0xd);
            offsetwr = 0x9FC6;
            writeByte();

            newgcString = smb22TextBox.Text;
            newgcString = newgcString.PadRight(0xd);
            offsetwr = 0x9FD6;
            writeByte();

            newgcString = smb23TextBox.Text;
            newgcString = newgcString.PadRight(0x4);
            offsetwr = 0x9FE6;
            writeByte();

            //refresh
            loadfile();

            MessageBox.Show("Super Mario Bros. Game Text Updated!", "SMB NES ROM TEXT EDITOR", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private void writeByte()
        {
            FileStream fs2 = new FileStream(@path, FileMode.Open, FileAccess.Write);
            i = newgcString.Length;
            x = 0;
            while (x < i)
            {
                ms[x] = newgcString[x].ToString();
                tempascii = ms[x];
                encodeSMBText();
                smbHexRet += encodeSMBText();
                x++;
            }

            //setup array
            shrArray();

            q = 0;
            while (q < i)
            {
                md[q] = int.Parse(msw[q], System.Globalization.NumberStyles.HexNumber);
                ms_final[q] = md[q].ToString();
                mb[q] = byte.Parse(ms_final[q]);
                q++;
            }

            fs2.Seek(offsetwr, SeekOrigin.Begin);
            q = 0;
            while (q < i)
            {
                fs2.WriteByte(mb[q]);
                q++;
            }
            newgcString = "";
            smbHexRet = "";

            fs2.Close();
        }

        private void shrArray()
        {
            //4 is smallest, 0x24 is largest
            //send a value over as stringlength, denote it as i

            if (i > 0x2)
            {
                msw[0] = smbHexRet[0].ToString() + smbHexRet[1].ToString();
                msw[1] = smbHexRet[2].ToString() + smbHexRet[3].ToString();
                msw[2] = smbHexRet[4].ToString() + smbHexRet[5].ToString();
            }
            if (i > 0x3)
            {
                msw[3] = smbHexRet[6].ToString() + smbHexRet[7].ToString();
            }
            if (i > 0x4)
            {
                msw[4] = smbHexRet[8].ToString() + smbHexRet[9].ToString();
            }
            if (i > 0x5)
            {
                msw[5] = smbHexRet[10].ToString() + smbHexRet[11].ToString();
            }
            if (i > 0x6)
            {
                msw[6] = smbHexRet[12].ToString() + smbHexRet[13].ToString();
            }
            if (i > 0x7)
            {
                msw[7] = smbHexRet[14].ToString() + smbHexRet[15].ToString();
            }
            if (i > 0x8)
            {
                msw[8] = smbHexRet[16].ToString() + smbHexRet[17].ToString();
            }
            if (i > 0x9)
            {
                msw[9] = smbHexRet[18].ToString() + smbHexRet[19].ToString();
            }
            if (i > 0xa)
            {
                msw[10] = smbHexRet[20].ToString() + smbHexRet[21].ToString();
            }
            if (i > 0xb)
            {
                msw[11] = smbHexRet[22].ToString() + smbHexRet[23].ToString();
            }
            if (i > 0xc)
            {
                msw[12] = smbHexRet[24].ToString() + smbHexRet[25].ToString();
            }
            if (i > 0xd)
            {
                msw[13] = smbHexRet[26].ToString() + smbHexRet[27].ToString();
            }
            if (i > 0xe)
            {
                msw[14] = smbHexRet[28].ToString() + smbHexRet[29].ToString();
            }
            if (i > 0xf)
            {
                msw[15] = smbHexRet[30].ToString() + smbHexRet[31].ToString();
            }
            if (i > 0x10)
            {
                msw[16] = smbHexRet[32].ToString() + smbHexRet[33].ToString();
            }
            if (i > 0x11)
            {
                msw[17] = smbHexRet[34].ToString() + smbHexRet[35].ToString();
            }
            if (i > 0x12)
            {
                msw[18] = smbHexRet[36].ToString() + smbHexRet[37].ToString();
            }
            if (i > 0x13)
            {
                msw[19] = smbHexRet[38].ToString() + smbHexRet[39].ToString();
            }
            if (i > 0x14)
            {
                msw[20] = smbHexRet[40].ToString() + smbHexRet[41].ToString();
            }
            if (i > 0x15)
            {
                msw[21] = smbHexRet[42].ToString() + smbHexRet[43].ToString();
            }
            if (i > 0x16)
            {
                msw[22] = smbHexRet[44].ToString() + smbHexRet[45].ToString();
            }
            if (i > 0x17)
            {
                msw[23] = smbHexRet[46].ToString() + smbHexRet[47].ToString();
            }
            if (i > 0x18)
            {
                msw[24] = smbHexRet[48].ToString() + smbHexRet[49].ToString();
            }
            if (i > 0x19)
            {
                msw[25] = smbHexRet[50].ToString() + smbHexRet[51].ToString();
            }
            if (i > 0x1A)
            {
                msw[26] = smbHexRet[52].ToString() + smbHexRet[53].ToString();
            }
            if (i > 0x1B)
            {
                msw[27] = smbHexRet[54].ToString() + smbHexRet[55].ToString();
            }
            if (i > 0x1C)
            {
                msw[28] = smbHexRet[56].ToString() + smbHexRet[57].ToString();
            }
            if (i > 0x1D)
            {
                msw[29] = smbHexRet[58].ToString() + smbHexRet[59].ToString();
            }
            if (i > 0x1E)
            {
                msw[30] = smbHexRet[60].ToString() + smbHexRet[61].ToString();
            }
            if (i > 0x1F)
            {
                msw[31] = smbHexRet[62].ToString() + smbHexRet[63].ToString();
            }
            if (i > 0x20)
            {
                msw[32] = smbHexRet[64].ToString() + smbHexRet[65].ToString();
            }
            if (i > 0x21)
            {
                msw[33] = smbHexRet[66].ToString() + smbHexRet[67].ToString();
            }
            if (i > 0x22)
            {
                msw[34] = smbHexRet[68].ToString() + smbHexRet[69].ToString();
            }
            if (i > 0x23)
            {
                msw[35] = smbHexRet[70].ToString() + smbHexRet[71].ToString();
            }
            if (i > 0x24)
            {
                msw[36] = smbHexRet[72].ToString() + smbHexRet[73].ToString();
            }
        }

        private string encodeSMBText()
        {
            string smbHex = "";
            //textFlag = 0;

            switch (tempascii)
            {
                case " ":
                    smbHex += "24";
                    break;
                case "-":
                    smbHex += "28";
                    break;
                case "!":
                    smbHex += "2B";
                    break;
                case "0":
                    smbHex += "00";
                    break;
                case "1":
                    smbHex += "01";
                    break;
                case "2":
                    smbHex += "02";
                    break;
                case "3":
                    smbHex += "03";
                    break;
                case "4":
                    smbHex += "04";
                    break;
                case "5":
                    smbHex += "05";
                    break;
                case "6":
                    smbHex += "06";
                    break;
                case "7":
                    smbHex += "07";
                    break;
                case "8":
                    smbHex += "08";
                    break;
                case "9":
                    smbHex += "09";
                    break;
                case "A":
                    smbHex += "0A";
                    break;
                case "B":
                    smbHex += "0B";
                    break;
                case "C":
                    smbHex += "0C";
                    break;
                case "D":
                    smbHex += "0D";
                    break;
                case "E":
                    smbHex += "0E";
                    break;
                case "F":
                    smbHex += "0F";
                    break;
                case "G":
                    smbHex += "10";
                    break;
                case "H":
                    smbHex += "11";
                    break;
                case "I":
                    smbHex += "12";
                    break;
                case "J":
                    smbHex += "13";
                    break;
                case "K":
                    smbHex += "14";
                    break;
                case "L":
                    smbHex += "15";
                    break;
                case "M":
                    smbHex += "16";
                    break;
                case "N":
                    smbHex += "17";
                    break;
                case "O":
                    smbHex += "18";
                    break;
                case "P":
                    smbHex += "19";
                    break;
                case "Q":
                    smbHex += "1A";
                    break;
                case "R":
                    smbHex += "1B";
                    break;
                case "S":
                    smbHex += "1C";
                    break;
                case "T":
                    smbHex += "1D";
                    break;
                case "U":
                    smbHex += "1E";
                    break;
                case "V":
                    smbHex += "1F";
                    break;
                case "W":
                    smbHex += "20";
                    break;
                case "X":
                    smbHex += "21";
                    break;
                case "Y":
                    smbHex += "22";
                    break;
                case "Z":
                    smbHex += "23";
                    break;
                case "a":
                    smbHex += "0A";
                    break;
                case "b":
                    smbHex += "0B";
                    break;
                case "c":
                    smbHex += "0C";
                    break;
                case "d":
                    smbHex += "0D";
                    break;
                case "e":
                    smbHex += "0E";
                    break;
                case "f":
                    smbHex += "0F";
                    break;
                case "g":
                    smbHex += "10";
                    break;
                case "h":
                    smbHex += "11";
                    break;
                case "i":
                    smbHex += "12";
                    break;
                case "j":
                    smbHex += "13";
                    break;
                case "k":
                    smbHex += "14";
                    break;
                case "l":
                    smbHex += "15";
                    break;
                case "m":
                    smbHex += "16";
                    break;
                case "n":
                    smbHex += "17";
                    break;
                case "o":
                    smbHex += "18";
                    break;
                case "p":
                    smbHex += "19";
                    break;
                case "q":
                    smbHex += "1A";
                    break;
                case "r":
                    smbHex += "1B";
                    break;
                case "s":
                    smbHex += "1C";
                    break;
                case "t":
                    smbHex += "1D";
                    break;
                case "u":
                    smbHex += "1E";
                    break;
                case "v":
                    smbHex += "1F";
                    break;
                case "w":
                    smbHex += "20";
                    break;
                case "x":
                    smbHex += "21";
                    break;
                case "y":
                    smbHex += "22";
                    break;
                case "z":
                    smbHex += "23";
                    break;
                default:
                    smbHex += "24";
                    //textFlag = 1;
                    break;
            }

            return smbHex;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fselButton_Click(sender, e);
        }

        private void disassembleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            disButton_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();

            ab.ShowDialog();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateButton_Click(sender, e);
        }


    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
