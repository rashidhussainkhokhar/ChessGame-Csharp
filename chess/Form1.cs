using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace chess
{
    public partial class Form1 : Form
    {
        int whiteclickcount = 0;
        int blackclickcount = 0;
        PictureBox previouspb;
        Control[] ar;
        Control[] temppicbox;
        List<string> coordinates;
        List<string> pawnfirsttimemoved;
        int index = 0;
        int blackdieindex = 0;
        int whitedieindex = 0;
        bool movecheck = true;
        int blacklistboxcount = 0;
        int whitelistboxcount = 0;
        clock whitetime;
        clock blacktime;
        int blackpoints = 0;
        int blackscores = 0;
        int whitepoints = 0;
        int whitescores = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            Invalidate();
            ar = new PictureBox[1];
            temppicbox = new PictureBox[64];
            coordinates = new List<string>();
            pawnfirsttimemoved = new List<string>();
            listView1.Items.Clear();
            whitecomboBox1.SelectedIndex = 0;
            blackcomboBox2.SelectedIndex = 0;
            whitetime = new clock();
            blacktime = new clock();
            
            
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen p = new Pen(Color.SandyBrown, 8);
            Pen p2 = new Pen(Color.PeachPuff, 8);
            int p1x = panel1.Location.X;
            int p1y = panel1.Location.Y;
            int p2x = panel2.Location.X;
            int p2y = panel2.Location.Y;
            e.Graphics.DrawRectangle(p, p1x, p1y, panel1.Width, panel1.Height);
            e.Graphics.DrawRectangle(p2, p2x, p2y, panel2.Width, panel2.Height);
            marquee.Font = new Font(marquee.Font.FontFamily, 14, FontStyle.Bold);

        }
        private void aboutRkchessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abrk = new About();
            abrk.ShowDialog();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            marquee.Left += 5;
            if (marquee.Left >= this.Width)
            {
                marquee.Left = marquee.Width * -1;
            }

        }
        private void a1whiterook_MouseDown(object sender, MouseEventArgs e)
        {
            
                PictureBox currentpb = (PictureBox)sender;

                if (movecheck == true)
                {
                    
                    movewhitepieces(currentpb);
                    return;


                }
                if (movecheck == false)
                {

                    
                    moveblackpieces(currentpb);
                    return;

                }

                
                
        }
      public void movewhitepieces(PictureBox p)
      {
          whiteclickcount++;
          if (whiteclickcount == 1)
          {
              if (p.Tag.ToString() == "null")
              {
                  whiteclickcount = 0;
              }
              else if (p.Tag.ToString().StartsWith("black"))
              {
                  MessageBox.Show("It's White's turn ");
                  whiteclickcount = 0;
                  return;
              }
              else if (p.Tag.ToString() == "whitepawn")
              {
                  previouspb = p;
                  string name = p.Name.ToString();
                  char a = name[0];
                  char b = name[1];
                  int temnum = (int)char.GetNumericValue(b);
                  int diagonalchecknumleft = temnum;
                  int diagonalchecknumright = temnum;
                  char diagonalcharleft = a;
                  char diagonalcharright = a;
                  string nextcoordinate="";
                  if (pawnfirsttimemoved.Contains(p.Name.ToString()))
                  {
                      temnum++;
                      if (temnum <= 8 && temnum >= 1)
                      {
                          nextcoordinate = a.ToString() + temnum.ToString();
                          ar = this.Controls.Find(nextcoordinate, true);
                          string temptag2 = (string)ar[0].Tag;
                          if(temptag2.Contains("null"))
                          {
                          coordinates.Add(nextcoordinate);
                          }
                      }
                      diagonalcharright++;
                      diagonalchecknumright++;
                      if (diagonalchecknumright <= 8 && diagonalcharright <= 'h' && diagonalcharright >= 'a')
                      {
                          string coor = diagonalcharright.ToString()+diagonalchecknumright.ToString();
                          ar = this.Controls.Find(coor, true);
                          string temptag1 = (string)ar[0].Tag;
                          if (temptag1.StartsWith("black"))
                          {
                              coordinates.Add(coor);
                          }
                      }
                      diagonalcharleft--;
                      diagonalchecknumleft++;
                      if (diagonalchecknumleft <= 8 && diagonalcharleft <= 'h' && diagonalcharleft >= 'a')
                      {
                          string coor = diagonalcharleft.ToString() + diagonalchecknumleft.ToString();
                          ar = this.Controls.Find(coor, true);
                          string temptag1 = (string)ar[0].Tag;
                          if (temptag1.StartsWith("black"))
                          {
                              coordinates.Add(coor);
                          }
                          
                      }


                  }
                  else
                  {
                      for (int k = 0; k < 2; k++)
                      {
                          temnum++;
                          nextcoordinate = a.ToString() + temnum.ToString();
                          ar = this.Controls.Find(nextcoordinate, true);
                          string temptag2 = (string)ar[0].Tag;
                          if (temptag2.Contains("null"))
                          {
                              coordinates.Add(nextcoordinate);
                          }
                          else
                              return;

                      }
                      diagonalcharright++;
                      diagonalchecknumright++;
                      if (diagonalchecknumright <= 8 && diagonalcharright <= 'h' && diagonalcharright >= 'a')
                      {
                          string coor = diagonalcharright.ToString() + diagonalchecknumright.ToString();
                          ar = this.Controls.Find(coor, true);
                          string temptag1 = (string)ar[0].Tag;
                          if( temptag1.StartsWith("black"))
                          {
                              coordinates.Add(coor);
                          }
                          
                      }
                      diagonalcharleft--;
                      diagonalchecknumleft++;
                      if (diagonalchecknumleft <= 8 && diagonalcharleft <= 'h' && diagonalcharleft >= 'a')
                      {
                          string coor = diagonalcharleft.ToString() + diagonalchecknumleft.ToString();
                          ar = this.Controls.Find(coor, true);
                          string temptag1 = (string)ar[0].Tag;
                          if (temptag1.StartsWith("black"))
                          {
                              coordinates.Add(coor);
                          }
                         
                      }
                  }
                  
              }
              else if (p.Tag.ToString() == "whiteking")
              {

                  previouspb = p;
                  string name = p.Name.ToString();
                  char a = name[0];
                  char b = name[1];
                  int tempnum = (int)char.GetNumericValue(b);
                  int up = tempnum;
                  int down = tempnum;
                  char leftchar = a;
                  char rightchar = a;
                  char upperrightdiagonalchar = a;
                  int upperrightdigonalnum = tempnum;
                  char upperleftdiagonalchar = a;
                  int upperleftdigonalnum = tempnum;
                  char lowerrightdiagonalchar = a;
                  int lowerrightdigonalnum = tempnum;
                  char lowerleftdiagonalchar = a;
                  int lowerleftdigonalnum = tempnum;
                  //for up 
                  up++;
                  if (up <= 8)
                  {
                      string first = a.ToString() + up.ToString();
                      ar = this.Controls.Find(first, true);
                      string tag1=ar[0].Tag.ToString();
                      if (tag1.Contains("null") || tag1.Contains("black"))
                      {
                          coordinates.Add(first);
                      }
                  }

                  //for down
                  down--;
                  if (down >= 1)
                  {
                      string first = a.ToString() + down.ToString();
                      ar = this.Controls.Find(first, true);
                      string tag1 = ar[0].Tag.ToString();
                      if (tag1.Contains("null") || tag1.Contains("black"))
                      {
                          coordinates.Add(first);
                      }
                  }

                  //for right
                  rightchar++;
                  if (rightchar <= 'h')
                  {
                      string first = rightchar.ToString() + tempnum.ToString();
                      ar = this.Controls.Find(first, true);
                      string tag1 = ar[0].Tag.ToString();
                      if (tag1.Contains("null") || tag1.Contains("black"))
                      {
                          coordinates.Add(first);
                      }
                  }

                  //for left
                  leftchar--;
                  if (leftchar >='a')
                  {
                      string first = leftchar.ToString() + tempnum.ToString();
                      ar = this.Controls.Find(first, true);
                      string tag1 = ar[0].Tag.ToString();
                      if (tag1.Contains("null") || tag1.Contains("black"))
                      {
                          coordinates.Add(first);
                      }
                  }

                  //for upper right diagonal
                  upperrightdiagonalchar++;
                  upperrightdigonalnum++;
                  if (upperrightdigonalnum <= 8 && upperrightdiagonalchar <= 'h')
                  {
                      string first = upperrightdiagonalchar.ToString() + upperrightdigonalnum.ToString();
                      ar = this.Controls.Find(first, true);
                      string tag1 = ar[0].Tag.ToString();
                      if (tag1.Contains("null") || tag1.Contains("black"))
                      {
                          coordinates.Add(first);
                      }
                  }
                  //for upper left diagonal
                  upperleftdiagonalchar--;
                  upperleftdigonalnum++;
                  if (upperleftdigonalnum <=8 && upperleftdiagonalchar >= 'a')
                  {
                      string first = upperleftdiagonalchar.ToString() + upperleftdigonalnum.ToString();
                      ar = this.Controls.Find(first, true);
                      string tag1 = ar[0].Tag.ToString();
                      if (tag1.Contains("null") || tag1.Contains("black"))
                      {
                          coordinates.Add(first);
                      }
                  }

                  //for lower right diagonal
                  lowerrightdiagonalchar++;
                  lowerrightdigonalnum--;
                  if (lowerrightdigonalnum >= 1 && lowerrightdiagonalchar <= 'h')
                  {
                      string first = lowerrightdiagonalchar.ToString() +lowerrightdigonalnum.ToString();
                      ar = this.Controls.Find(first, true);
                      string tag1 = ar[0].Tag.ToString();
                      if (tag1.Contains("null") || tag1.Contains("black"))
                      {
                          coordinates.Add(first);
                      }
                  }

                  //for lower left diagonal

                  lowerleftdiagonalchar--;
                  lowerleftdigonalnum--;
                  if (lowerleftdigonalnum >= 1 && lowerleftdiagonalchar >= 'a')
                  {
                      string first = lowerleftdiagonalchar.ToString() + lowerleftdigonalnum.ToString();
                      ar = this.Controls.Find(first, true);
                      string tag1 = ar[0].Tag.ToString();
                      if (tag1.Contains("null") || tag1.Contains("black"))
                      {
                          coordinates.Add(first);
                      }
                  }
              }

              else if (p.Tag.ToString() == "whiterook")
              {
                  previouspb = p;
                  string name = p.Name.ToString();
                  char a = name[0];
                  char b = name[1];
                  int tempnum = (int)char.GetNumericValue(b);
                  int upnum = tempnum;
                  int downnum = tempnum;
                  char rightchar = a;
                  char leftchar = a;
                  string nextcoord = "";
                  
                  //for up

                  for (int j = tempnum; j <= 8; j++)
                  {
                      upnum++;
                      if (upnum <= 8)
                      {
                          nextcoord = a.ToString() + upnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

                  //for down

                  for (int k = 1; k <=tempnum; k++)
                  {
                      downnum--;
                      if (downnum >= 1)
                      {
                          nextcoord = a.ToString() + downnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  } 

                  //for right 

                  for (char k = a; k <= 'h'; k++)
                  {
                      rightchar++;
                      if (rightchar <= 'h')
                      {
                          nextcoord = rightchar.ToString() + tempnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

                  //for left 

                  for (char l = a; l >= 'a'; l--)
                  {
                      leftchar--;
                      if (leftchar >= 'a')
                      {
                          nextcoord = leftchar.ToString() + tempnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }
              }
              else if (p.Tag.ToString() == "whitebishop")
              {
                  previouspb = p;
                  string name = p.Name.ToString();
                  char a = name[0];
                  char b = name[1];
                  int tempnum = (int)char.GetNumericValue(b);
                  int upperrightnextnum = tempnum;
                  char upperrightnextchar = a;
                  int upperleftnextnum = tempnum;
                  char upperleftnextchar = a;
                  int lowerrightnextnum = tempnum;
                  char lowerrightnextchar = a;
                  int lowerleftnextnum = tempnum;
                  char lowerleftnextchar = a;
                  string nextcoord = "";

                  //for upper right diagonal

                  for (int loop = tempnum; loop <= 8; loop++)
                  {
                      upperrightnextnum++;
                      upperrightnextchar++;
                      if (upperrightnextnum <= 8 && upperrightnextchar<='h')
                      {
                          nextcoord = upperrightnextchar.ToString() + upperrightnextnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

                  //for upper left diagonal

                  for (char l = 'a'; l <= a; l++)
                  {
                      upperleftnextchar--;
                      upperleftnextnum++;
                      if (upperleftnextnum <= 8 && upperleftnextchar >= 'a')
                      {
                          nextcoord = upperleftnextchar.ToString() + upperleftnextnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }

                      }
                  }

                  //for lower left diagonal

                  for (char k = a; k >= 'a';k-- )
                  {
                      lowerleftnextchar--;
                      lowerleftnextnum--;
                      if (lowerleftnextnum >= 1 && lowerleftnextchar >= 'a')
                      {
                          nextcoord = lowerleftnextchar.ToString() + lowerleftnextnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

                  //for lower right diagonal

                  for (char pp = a; pp<='h'; pp++)
                  {
                      lowerrightnextchar++;
                      lowerrightnextnum--;
                      if (lowerrightnextnum >= 1 && lowerrightnextchar <= 'h')
                      {
                          nextcoord = lowerrightnextchar.ToString() + lowerrightnextnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }


              }

              else if (p.Tag.ToString() == "whiteknight")
              {
                  previouspb = p;
                  string name = p.Name.ToString();
                  char a = name[0];
                  char b = name[1];
                  int tempnum = (int)char.GetNumericValue(b);
                  int upnum = tempnum;
                  char upchar = a;
                  int downnum = tempnum;
                  char downchar = a;
                  int leftnum = tempnum;
                  char leftchar = a;
                  int rightnum = tempnum;
                  char rightchar = a;
                  string nextcoord = "";

                  //for up

                  int q = upnum;
                  int qq = 0;
                  int count1 = 0;
                  string tempcord1 = "";
                  for (int k = 0; k < 2; k++)
                  {
                      upnum++;
                      qq = q;
                      if (upnum <= 8 && (qq++) <= 8)
                      {
                          tempcord1 = upchar.ToString() + upnum.ToString();
                          count1++;

                      }
                  }
                  if (count1 == 2)
                  {
                      nextcoord = tempcord1;
                  }
                  if (nextcoord != "")
                  {
                      char x = nextcoord[0];
                      char y = nextcoord[1];
                      int numt = (int)char.GetNumericValue(y);
                      char upleftchar = x;
                      char uprightchar = x;
                      uprightchar++;
                      string tempcor = "";
                      if (uprightchar <= 'h')
                      {
                          tempcor = uprightchar.ToString() + numt.ToString();
                          ar = this.Controls.Find(tempcor, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null") || tag1.StartsWith("black"))
                          {
                              coordinates.Add(tempcor);
                          }
                      }
                      upleftchar--;
                      if (upleftchar >= 'a')
                      {
                          tempcor = upleftchar.ToString() + numt.ToString();
                          ar = this.Controls.Find(tempcor, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null") || tag1.StartsWith("black"))
                          {
                              coordinates.Add(tempcor);
                          }
                      }
                  }

                  //for down 

                  nextcoord = "";
                  int ch = downnum;
                  int chh = 0;
                  int count2 = 0;
                  string tempcord2 = "";
                  for (int k = 0; k < 2; k++)
                  {
                      downnum--;
                      chh = ch;
                      if (downnum >= 1 && (chh--) >= 1)
                      {
                          tempcord2 = a.ToString() + downnum.ToString();
                          count2++;

                      }
                  }
                  if (count2 == 2)
                  {
                      nextcoord = tempcord2;
                  }
                  if (nextcoord != "")
                  {
                      char xx = nextcoord[0];
                      char yy = nextcoord[1];
                      int numt1 = (int)char.GetNumericValue(yy);
                      char downleftchar = xx;
                      char downrightchar = xx;
                      downrightchar++;
                      string tempcor1 = "";
                      if (downrightchar <= 'h')
                      {
                          tempcor1 = downrightchar.ToString() + numt1.ToString();
                          ar = this.Controls.Find(tempcor1, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null") || tag1.StartsWith("black"))
                          {
                              coordinates.Add(tempcor1);
                          }
                      }
                      downleftchar--;
                      if (downleftchar >= 'a')
                      {
                          tempcor1 = downleftchar.ToString() + numt1.ToString();
                          ar = this.Controls.Find(tempcor1, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null") || tag1.StartsWith("black"))
                          {
                              coordinates.Add(tempcor1);
                          }
                      }
                  }

                  //for right 

                  nextcoord = "";
                  char che = rightchar;
                  char chhe;
                  int count = 0;
                  string tempcord = "";
                  for (int k = 0; k < 2; k++)
                  {
                      rightchar++;
                      chhe = che;
                      if (rightchar <= 'h' && (chhe++) <= 'h')
                      {
                          tempcord = rightchar.ToString() + tempnum.ToString();
                          count++;

                      }
                  }
                  if (count == 2)
                  {
                      nextcoord = tempcord;
                  }
                  if (nextcoord != "")
                  {
                      char xx = nextcoord[0];
                      char yy = nextcoord[1];
                      int numt1 = (int)char.GetNumericValue(yy);
                      int uprightnum = numt1;
                      int downrightnum = numt1;
                      uprightnum++;
                      string tempcor1 = "";
                      if (uprightnum <= 8)
                      {
                          tempcor1 = xx.ToString() + uprightnum.ToString();
                          ar = this.Controls.Find(tempcor1, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null") || tag1.StartsWith("black"))
                          {
                              coordinates.Add(tempcor1);
                          }
                      }
                      downrightnum--;
                      if (downrightnum >= 1)
                      {
                          tempcor1 = xx.ToString() + downrightnum.ToString();
                          ar = this.Controls.Find(tempcor1, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null") || tag1.StartsWith("black"))
                          {
                              coordinates.Add(tempcor1);
                          }
                      }
                  }

                  //for left

                  nextcoord = "";
                  char chee = leftchar;
                  char chhee;
                  int count3 = 0;
                  string tempcord3 = "";
                  for (int k = 0; k < 2; k++)
                  {
                      leftchar--;
                      chhee = chee;
                      if (leftchar >= 'a' && (chhee--) >= 'a')
                      {
                          tempcord3 = leftchar.ToString() + tempnum.ToString();
                          count3++;

                      }
                  }
                  if (count3 == 2)
                  {
                      nextcoord = tempcord3;
                  }
                  if (nextcoord != "")
                  {
                      char xx = nextcoord[0];
                      char yy = nextcoord[1];
                      int numt1 = (int)char.GetNumericValue(yy);
                      int upleftnum = numt1;
                      int downleftnum = numt1;
                      upleftnum++;
                      string tempcor1 = "";
                      if (upleftnum <= 8)
                      {
                          tempcor1 = xx.ToString() + upleftnum.ToString();
                          ar = this.Controls.Find(tempcor1, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null") || tag1.StartsWith("black"))
                          {
                              coordinates.Add(tempcor1);
                          }
                      }
                      downleftnum--;
                      if (downleftnum >= 1)
                      {
                          tempcor1 = xx.ToString() + downleftnum.ToString();
                          ar = this.Controls.Find(tempcor1, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null") || tag1.StartsWith("black"))
                          {
                              coordinates.Add(tempcor1);
                          }
                      }
                  }
              }
              else if (p.Tag.ToString() == "whitequeen")
              {
                  previouspb = p;
                  string name = p.Name.ToString();
                  char a = name[0];
                  char b = name[1];
                  int tempnum = (int)char.GetNumericValue(b);
                  int upnum = tempnum;
                  int downnum = tempnum;
                  char rightchar = a;
                  char leftchar = a;
                  string nextcoord = "";
                  
                  //for up

                  for (int j = tempnum; j <= 8; j++)
                  {
                      upnum++;
                      if (upnum <= 8)
                      {
                          nextcoord = a.ToString() + upnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

                  //for down

                  for (int k = 1; k <=tempnum; k++)
                  {
                      downnum--;
                      if (downnum >= 1)
                      {
                          nextcoord = a.ToString() + downnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  } 

                  //for right 

                  for (char k = a; k <= 'h'; k++)
                  {
                      rightchar++;
                      if (rightchar <= 'h')
                      {
                          nextcoord = rightchar.ToString() + tempnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

                  //for left 

                  for (char l = a; l >= 'a'; l--)
                  {
                      leftchar--;
                      if (leftchar >= 'a')
                      {
                          nextcoord = leftchar.ToString() + tempnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

                 
                  int upperrightnextnum = tempnum;
                  char upperrightnextchar = a;
                  int upperleftnextnum = tempnum;
                  char upperleftnextchar = a;
                  int lowerrightnextnum = tempnum;
                  char lowerrightnextchar = a;
                  int lowerleftnextnum = tempnum;
                  char lowerleftnextchar = a;

                  //for upper right diagonal

                  for (int loop = tempnum; loop <= 8; loop++)
                  {
                      upperrightnextnum++;
                      upperrightnextchar++;
                      if (upperrightnextnum <= 8 && upperrightnextchar <= 'h')
                      {
                          nextcoord = upperrightnextchar.ToString() + upperrightnextnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

                  //for upper left diagonal

                  for (char l = 'a'; l <= a; l++)
                  {
                      upperleftnextchar--;
                      upperleftnextnum++;
                      if (upperleftnextnum <= 8 && upperleftnextchar >= 'a')
                      {
                          nextcoord = upperleftnextchar.ToString() + upperleftnextnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }

                      }
                  }

                  //for lower left diagonal

                  for (char k = a; k >= 'a'; k--)
                  {
                      lowerleftnextchar--;
                      lowerleftnextnum--;
                      if (lowerleftnextnum >= 1 && lowerleftnextchar >= 'a')
                      {
                          nextcoord = lowerleftnextchar.ToString() + lowerleftnextnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

                  //for lower right diagonal

                  for (char pp = a; pp <= 'h'; pp++)
                  {
                      lowerrightnextchar++;
                      lowerrightnextnum--;
                      if (lowerrightnextnum >= 1 && lowerrightnextchar <= 'h')
                      {
                          nextcoord = lowerrightnextchar.ToString() + lowerrightnextnum.ToString();
                          ar = this.Controls.Find(nextcoord, true);
                          string tag1 = ar[0].Tag.ToString();
                          if (tag1.Contains("null"))
                          {
                              coordinates.Add(nextcoord);
                          }
                          if (tag1.StartsWith("white"))
                          {
                              break;
                          }
                          if (tag1.StartsWith("black"))
                          {
                              coordinates.Add(nextcoord);
                              break;
                          }
                      }
                  }

              }



          }
          if(whiteclickcount==2)
          {
              if (coordinates.IndexOf(p.Name.ToString()) != -1)
              {
                  string tagtemp = p.Tag.ToString();
                  if (tagtemp.StartsWith("black") &&  tagtemp.EndsWith("king"))
                  {
                      whiteclickcount = 0;
                      return;
                  }
                  else if (tagtemp.StartsWith("black"))
                  {
                      if (previouspb.Tag.ToString().EndsWith("pawn"))
                      {
                          whitepoints = whitepoints + 1;
                          whitescores = whitepoints * 20;
                      }
                      else if (previouspb.Tag.ToString().EndsWith("knight"))
                      {
                          whitepoints = whitepoints + 3;
                          whitescores = whitepoints * 20;

                      }
                      else if (previouspb.Tag.ToString().EndsWith("bishop"))
                      {
                          whitepoints = whitepoints + 3;
                          whitescores = whitepoints * 20;

                      }
                      else if (previouspb.Tag.ToString().EndsWith("rook"))
                      {
                          whitepoints = whitepoints + 5;
                          whitescores = whitepoints * 20;

                      }
                      else if (previouspb.Tag.ToString().EndsWith("queen"))
                      {
                          whitepoints = whitepoints + 9;
                          whitescores = whitepoints * 20;

                      }
                      else if (previouspb.Tag.ToString().EndsWith("king"))
                      {
                          whitepoints = whitepoints + 90;
                          whitescores = whitepoints * 20;

                      }
                      positionwhitelabel27.Text = p.Name.ToString().ToUpper();
                      pointswhitelabel26.Text = whitepoints.ToString();
                      scorewhitelabel25.Text = whitescores.ToString();
                      blackdieindex++;
                      string diepb = "blackdiepb" + blackdieindex;
                      ar = this.Controls.Find(diepb, true);
                      ar[0].BackgroundImage = p.Image;
                      ar[0].BackgroundImageLayout = ImageLayout.Center;
                      p.Tag = previouspb.Tag;
                      previouspb.Tag = "null";
                      p.Image = previouspb.Image;
                      previouspb.Image = null;
                      string checkpawn = "";
                      checkpawn = p.Tag.ToString();
                      if (checkpawn.Substring(5, 4) == "pawn")
                      {
                          pawnfirsttimemoved.Add(p.Name.ToString());
                      }
                      movecheck = false;
                      playerstatuslabel.Text = "Opening game - Black to play";
                      whitelistboxcount++;
                      string[] row = { whitelistboxcount.ToString(), whitetime.Hour+" : "+whitetime.Min+" : "+whitetime.Sec, p.Tag.ToString() + " form " + previouspb.Name.ToString() + " to " + p.Name.ToString()+" killed : "+tagtemp };
                      ListViewItem li = new ListViewItem(row);
                      listView1.Items.Add(li);
                  }
                  else
                  {
                      if (previouspb.Tag.ToString().EndsWith("pawn"))
                      {
                          whitepoints = whitepoints + 1;
                          whitescores = whitepoints * 10;
                      }
                      else if (previouspb.Tag.ToString().EndsWith("knight"))
                      {
                          whitepoints = whitepoints + 3;
                          whitescores = whitepoints * 10;

                      }
                      else if (previouspb.Tag.ToString().EndsWith("bishop"))
                      {
                          whitepoints = whitepoints + 3;
                          whitescores = whitepoints * 10;

                      }
                      else if (previouspb.Tag.ToString().EndsWith("rook"))
                      {
                          whitepoints = whitepoints + 5;
                          whitescores = whitepoints * 10;

                      }
                      else if (previouspb.Tag.ToString().EndsWith("queen"))
                      {
                          whitepoints = whitepoints + 9;
                          whitescores = whitepoints * 10;

                      }
                      else if (previouspb.Tag.ToString().EndsWith("king"))
                      {
                          whitepoints = whitepoints + 90;
                          whitescores = whitepoints * 10;

                      }
                      pointswhitelabel26.Text = whitepoints.ToString();
                      scorewhitelabel25.Text = whitescores.ToString();
                      positionwhitelabel27.Text = p.Name.ToString().ToUpper();
                      p.Image = previouspb.Image;
                      previouspb.Image = null;
                      string temptag = previouspb.Tag.ToString();
                      previouspb.Tag = p.Tag;
                      p.Tag = temptag;
                      string checkpawn = "";
                      checkpawn = p.Tag.ToString();
                      if (checkpawn.Substring(5, 4) == "pawn")
                      {
                          pawnfirsttimemoved.Add(p.Name.ToString());
                      }
                      movecheck = false;
                      playerstatuslabel.Text = "Opening game - Black to play";
                      whitelistboxcount++;
                      string[] row = { whitelistboxcount.ToString(), whitetime.Hour + " : " + whitetime.Min + " : " + whitetime.Sec, p.Tag.ToString() + " form " + previouspb.Name.ToString() + " to " + p.Name.ToString() };
                      ListViewItem li = new ListViewItem(row);
                      listView1.Items.Add(li);
                  }
              }
              else
              {
                  
                  MessageBox.Show("Invalid move :  Try again ");
              }
              whiteclickcount = 0;
              coordinates.Clear();
          }
      }
      public void moveblackpieces(PictureBox p)
      {
           blackclickcount++;
           if (blackclickcount == 1)
           {
               if (p.Tag.ToString() == "null")
               {
                   blackclickcount = 0;
               }
               else if (p.Tag.ToString().StartsWith("white"))
               {
                   MessageBox.Show("It's Black turn ");
                   blackclickcount = 0;
                   return;
               }
               else if (p.Tag.ToString() == "blackpawn")
               {
                   previouspb = p;
                   string name = p.Name.ToString();
                   char a = name[0];
                   char b = name[1];
                   int temnum = (int)char.GetNumericValue(b);
                   int diagonalchecknumleft = temnum;
                   int diagonalchecknumright = temnum;
                   char diagonalcharleft = a;
                   char diagonalcharright = a;
                   string nextcoordinate = "";
                   if (pawnfirsttimemoved.Contains(p.Name.ToString()))
                   {
                       temnum--;
                       if (temnum <= 8 && temnum >= 1)
                       {
                           nextcoordinate = a.ToString() + temnum.ToString();
                           ar = this.Controls.Find(nextcoordinate, true);
                           string temptag2 = (string)ar[0].Tag;
                           if (temptag2.Contains("null"))
                           {
                               coordinates.Add(nextcoordinate);
                           }
                       }
                       diagonalcharright++;
                       diagonalchecknumright--;
                       if (diagonalchecknumright <= 8 && diagonalchecknumright >= 1 && diagonalcharright <= 'h' && diagonalcharright >= 'a')
                       {
                           string coor = diagonalcharright.ToString() + diagonalchecknumright.ToString();
                           ar = this.Controls.Find(coor, true);
                           string temptag1 = (string)ar[0].Tag;
                           if (temptag1.StartsWith("white"))
                           {
                               coordinates.Add(coor);
                           }
                       }
                       diagonalcharleft--;
                       diagonalchecknumleft--;
                       if (diagonalchecknumleft <= 8 && diagonalchecknumleft >= 1 && diagonalcharleft <= 'h' && diagonalcharleft >= 'a')
                       {
                           string coor = diagonalcharleft.ToString() + diagonalchecknumleft.ToString();
                           ar = this.Controls.Find(coor, true);
                           string temptag1 = (string)ar[0].Tag;
                           if (temptag1.StartsWith("white"))
                           {
                               coordinates.Add(coor);
                           }

                       }


                   }
                   else
                   {
                       for (int k = 0; k < 2; k++)
                       {
                           temnum--;
                           nextcoordinate = a.ToString() + temnum.ToString();
                           ar = this.Controls.Find(nextcoordinate, true);
                           string temptag2 = (string)ar[0].Tag;
                           if (temptag2.Contains("null"))
                           {
                               coordinates.Add(nextcoordinate);
                           }
                           else
                               return;

                       }
                       diagonalcharright++;
                       diagonalchecknumright--;
                       if (diagonalchecknumright <= 8 && diagonalchecknumright >= 1 && diagonalcharright <= 'h' && diagonalcharright >= 'a')
                       {
                           string coor = diagonalcharright.ToString() + diagonalchecknumright.ToString();
                           ar = this.Controls.Find(coor, true);
                           string temptag1 = (string)ar[0].Tag;
                           if (temptag1.StartsWith("white"))
                           {
                               coordinates.Add(coor);
                           }

                       }
                       diagonalcharleft--;
                       diagonalchecknumleft--;
                       if (diagonalchecknumleft <= 8 && diagonalchecknumleft >= 1 && diagonalcharleft <= 'h' && diagonalcharleft >= 'a')
                       {
                           string coor = diagonalcharleft.ToString() + diagonalchecknumleft.ToString();
                           ar = this.Controls.Find(coor, true);
                           string temptag1 = (string)ar[0].Tag;
                           if (temptag1.StartsWith("white"))
                           {
                               coordinates.Add(coor);
                           }

                       }
                   }

               }

               //for black rook

               else if (p.Tag.ToString() == "blackrook")
               {
                   previouspb = p;
                   string name = p.Name.ToString();
                   char a = name[0];
                   char b = name[1];
                   int tempnum = (int)char.GetNumericValue(b);
                   int upnum = tempnum;
                   int downnum = tempnum;
                   char rightchar = a;
                   char leftchar = a;
                   string nextcoord = "";

                   //for up

                   for (int j = tempnum; j <= 8; j++)
                   {
                       upnum++;
                       if (upnum <= 8)
                       {
                           nextcoord = a.ToString() + upnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for down

                   for (int k = 1; k <= tempnum; k++)
                   {
                       downnum--;
                       if (downnum >=1)
                       {
                           nextcoord = a.ToString() + downnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for right 

                   for (char k = a; k <= 'h'; k++)
                   {
                       rightchar++;
                       if (rightchar <= 'h')
                       {
                           nextcoord = rightchar.ToString() + tempnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for left 

                   for (char l = a; l >= 'a'; l--)
                   {
                       leftchar--;
                       if (leftchar >= 'a')
                       {
                           nextcoord = leftchar.ToString() + tempnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }
               }

               // for black king

               else if (p.Tag.ToString() == "blackking")
               {

                   previouspb = p;
                   string name = p.Name.ToString();
                   char a = name[0];
                   char b = name[1];
                   int tempnum = (int)char.GetNumericValue(b);
                   int up = tempnum;
                   int down = tempnum;
                   char leftchar = a;
                   char rightchar = a;
                   char upperrightdiagonalchar = a;
                   int upperrightdigonalnum = tempnum;
                   char upperleftdiagonalchar = a;
                   int upperleftdigonalnum = tempnum;
                   char lowerrightdiagonalchar = a;
                   int lowerrightdigonalnum = tempnum;
                   char lowerleftdiagonalchar = a;
                   int lowerleftdigonalnum = tempnum;
                   //for up 
                   up++;
                   if (up <= 8)
                   {
                       string first = a.ToString() + up.ToString();
                       ar = this.Controls.Find(first, true);
                       string tag1 = ar[0].Tag.ToString();
                       if (tag1.Contains("null") || tag1.Contains("white"))
                       {
                           coordinates.Add(first);
                       }
                   }

                   //for down
                   down--;
                   if (down >= 1)
                   {
                       string first = a.ToString() + down.ToString();
                       ar = this.Controls.Find(first, true);
                       string tag1 = ar[0].Tag.ToString();
                       if (tag1.Contains("null") || tag1.Contains("white"))
                       {
                           coordinates.Add(first);
                       }
                   }

                   //for right
                   rightchar++;
                   if (rightchar <= 'h')
                   {
                       string first = rightchar.ToString() + tempnum.ToString();
                       ar = this.Controls.Find(first, true);
                       string tag1 = ar[0].Tag.ToString();
                       if (tag1.Contains("null") || tag1.Contains("white"))
                       {
                           coordinates.Add(first);
                       }
                   }

                   //for left
                   leftchar--;
                   if (leftchar >= 'a')
                   {
                       string first = leftchar.ToString() + tempnum.ToString();
                       ar = this.Controls.Find(first, true);
                       string tag1 = ar[0].Tag.ToString();
                       if (tag1.Contains("null") || tag1.Contains("white"))
                       {
                           coordinates.Add(first);
                       }
                   }

                   //for upper right diagonal
                   upperrightdiagonalchar++;
                   upperrightdigonalnum++;
                   if (upperrightdigonalnum <= 8 && upperrightdiagonalchar <= 'h')
                   {
                       string first = upperrightdiagonalchar.ToString() + upperrightdigonalnum.ToString();
                       ar = this.Controls.Find(first, true);
                       string tag1 = ar[0].Tag.ToString();
                       if (tag1.Contains("null") || tag1.Contains("white"))
                       {
                           coordinates.Add(first);
                       }
                   }
                   //for upper left diagonal
                   upperleftdiagonalchar--;
                   upperleftdigonalnum++;
                   if (upperleftdigonalnum <= 8 && upperleftdiagonalchar >= 'a')
                   {
                       string first = upperleftdiagonalchar.ToString() + upperleftdigonalnum.ToString();
                       ar = this.Controls.Find(first, true);
                       string tag1 = ar[0].Tag.ToString();
                       if (tag1.Contains("null") || tag1.Contains("white"))
                       {
                           coordinates.Add(first);
                       }
                   }

                   //for lower right diagonal
                   lowerrightdiagonalchar++;
                   lowerrightdigonalnum--;
                   if (lowerrightdigonalnum >= 1 && lowerrightdiagonalchar <= 'h')
                   {
                       string first = lowerrightdiagonalchar.ToString() + lowerrightdigonalnum.ToString();
                       ar = this.Controls.Find(first, true);
                       string tag1 = ar[0].Tag.ToString();
                       if (tag1.Contains("null") || tag1.Contains("white"))
                       {
                           coordinates.Add(first);
                       }
                   }

                   //for lower left diagonal

                   lowerleftdiagonalchar--;
                   lowerleftdigonalnum--;
                   if (lowerleftdigonalnum >= 1 && lowerleftdiagonalchar >= 'a')
                   {
                       string first = lowerleftdiagonalchar.ToString() + lowerleftdigonalnum.ToString();
                       ar = this.Controls.Find(first, true);
                       string tag1 = ar[0].Tag.ToString();
                       if (tag1.Contains("null") || tag1.Contains("white"))
                       {
                           coordinates.Add(first);
                       }
                   }
               }

               //for black bishop

               else if (p.Tag.ToString() == "blackbishop")
               {
                   previouspb = p;
                   string name = p.Name.ToString();
                   char a = name[0];
                   char b = name[1];
                   int tempnum = (int)char.GetNumericValue(b);
                   int upperrightnextnum = tempnum;
                   char upperrightnextchar = a;
                   int upperleftnextnum = tempnum;
                   char upperleftnextchar = a;
                   int lowerrightnextnum = tempnum;
                   char lowerrightnextchar = a;
                   int lowerleftnextnum = tempnum;
                   char lowerleftnextchar = a;
                   string nextcoord = "";

                   //for upper right diagonal

                   for (int loop = tempnum; loop <= 8; loop++)
                   {
                       upperrightnextnum++;
                       upperrightnextchar++;
                       if (upperrightnextnum <= 8 && upperrightnextchar <= 'h')
                       {
                           nextcoord = upperrightnextchar.ToString() + upperrightnextnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for upper left diagonal

                   for (char l = 'a'; l <= a; l++)
                   {
                       upperleftnextchar--;
                       upperleftnextnum++;
                       if (upperleftnextnum <= 8 && upperleftnextchar >= 'a')
                       {
                           nextcoord = upperleftnextchar.ToString() + upperleftnextnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }

                       }
                   }

                   //for lower left diagonal

                   for (char k = a; k >= 'a'; k--)
                   {
                       lowerleftnextchar--;
                       lowerleftnextnum--;
                       if (lowerleftnextnum >= 1 && lowerleftnextchar >= 'a')
                       {
                           nextcoord = lowerleftnextchar.ToString() + lowerleftnextnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for lower right diagonal

                   for (char pp = a; pp <= 'h'; pp++)
                   {
                       lowerrightnextchar++;
                       lowerrightnextnum--;
                       if (lowerrightnextnum >= 1 && lowerrightnextchar <= 'h')
                       {
                           nextcoord = lowerrightnextchar.ToString() + lowerrightnextnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }


               }

               //for black knight

               else if (p.Tag.ToString() == "blackknight")
               {
                   previouspb = p;
                   string name = p.Name.ToString();
                   char a = name[0];
                   char b = name[1];
                   int tempnum = (int)char.GetNumericValue(b);
                   int upnum = tempnum;
                   char upchar = a;
                   int downnum = tempnum;
                   char downchar = a;
                   int leftnum = tempnum;
                   char leftchar = a;
                   int rightnum = tempnum;
                   char rightchar = a;
                   string nextcoord = "";

                   //for up

                   int q = upnum;
                   int qq = 0;
                   int count1 = 0;
                   string tempcord1 = "";
                   for (int k = 0; k < 2; k++)
                   {
                       upnum++;
                       qq = q;
                       if (upnum <= 8 && (qq++) <= 8)
                       {
                           tempcord1 = upchar.ToString() + upnum.ToString();
                           count1++;

                       }
                   }
                   if (count1 == 2)
                   {
                       nextcoord = tempcord1;
                   }
                   if (nextcoord != "")
                   {
                       char x = nextcoord[0];
                       char y = nextcoord[1];
                       int numt = (int)char.GetNumericValue(y);
                       char upleftchar = x;
                       char uprightchar = x;
                       uprightchar++;
                       string tempcor = "";
                       if (uprightchar <= 'h')
                       {
                           tempcor = uprightchar.ToString() + numt.ToString();
                           ar = this.Controls.Find(tempcor, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null") || tag1.StartsWith("white"))
                           {
                               coordinates.Add(tempcor);
                           }
                       }
                       upleftchar--;
                       if (upleftchar >= 'a')
                       {
                           tempcor = upleftchar.ToString() + numt.ToString();
                           ar = this.Controls.Find(tempcor, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null") || tag1.StartsWith("white"))
                           {
                               coordinates.Add(tempcor);
                           }
                       }
                   }

                   //for down 

                   nextcoord = "";
                   int ch = downnum;
                   int chh = 0;
                   int count2 = 0;
                   string tempcord2 = "";
                   for (int k = 0; k < 2; k++)
                   {
                       downnum--;
                       chh = ch;
                       if (downnum >= 1 && (chh--) >= 1)
                       {
                           tempcord2 = a.ToString() + downnum.ToString();
                           count2++;

                       }
                   }
                   if (count2 == 2)
                   {
                       nextcoord = tempcord2;
                   }
                   if (nextcoord != "")
                   {
                       char xx = nextcoord[0];
                       char yy = nextcoord[1];
                       int numt1 = (int)char.GetNumericValue(yy);
                       char downleftchar = xx;
                       char downrightchar = xx;
                       downrightchar++;
                       string tempcor1 = "";
                       if (downrightchar <= 'h')
                       {
                           tempcor1 = downrightchar.ToString() + numt1.ToString();
                           ar = this.Controls.Find(tempcor1, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null") || tag1.StartsWith("white"))
                           {
                               coordinates.Add(tempcor1);
                           }
                       }
                       downleftchar--;
                       if (downleftchar >= 'a')
                       {
                           tempcor1 = downleftchar.ToString() + numt1.ToString();
                           ar = this.Controls.Find(tempcor1, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null") || tag1.StartsWith("white"))
                           {
                               coordinates.Add(tempcor1);
                           }
                       }
                   }

                   //for right 

                   nextcoord = "";
                   char che = rightchar;
                   char chhe;
                   int count = 0;
                   string tempcord = "";
                   for (int k = 0; k < 2; k++)
                   {
                       rightchar++;
                       chhe = che;
                       if (rightchar <= 'h' && (chhe++) <= 'h')
                       {
                           tempcord = rightchar.ToString() + tempnum.ToString();
                           count++;

                       }
                   }
                   if (count == 2)
                   {
                       nextcoord = tempcord;
                   }
                   if (nextcoord != "")
                   {
                       char xx = nextcoord[0];
                       char yy = nextcoord[1];
                       int numt1 = (int)char.GetNumericValue(yy);
                       int uprightnum = numt1;
                       int downrightnum = numt1;
                       uprightnum++;
                       string tempcor1 = "";
                       if (uprightnum <= 8)
                       {
                           tempcor1 = xx.ToString() + uprightnum.ToString();
                           ar = this.Controls.Find(tempcor1, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null") || tag1.StartsWith("white"))
                           {
                               coordinates.Add(tempcor1);
                           }
                       }
                       downrightnum--;
                       if (downrightnum >= 1)
                       {
                           tempcor1 = xx.ToString() + downrightnum.ToString();
                           ar = this.Controls.Find(tempcor1, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null") || tag1.StartsWith("white"))
                           {
                               coordinates.Add(tempcor1);
                           }
                       }
                   }

                   //for left

                   nextcoord = "";
                   char chee = leftchar;
                   char chhee;
                   int count3 = 0;
                   string tempcord3 = "";
                   for (int k = 0; k < 2; k++)
                   {
                       leftchar--;
                       chhee = chee;
                       if (leftchar >= 'a' && (chhee--) >= 'a')
                       {
                           tempcord3 = leftchar.ToString() + tempnum.ToString();
                           count3++;

                       }
                   }
                   if (count3 == 2)
                   {
                       nextcoord = tempcord3;
                   }
                   if (nextcoord != "")
                   {
                       char xx = nextcoord[0];
                       char yy = nextcoord[1];
                       int numt1 = (int)char.GetNumericValue(yy);
                       int upleftnum = numt1;
                       int downleftnum = numt1;
                       upleftnum++;
                       string tempcor1 = "";
                       if (upleftnum <= 8)
                       {
                           tempcor1 = xx.ToString() + upleftnum.ToString();
                           ar = this.Controls.Find(tempcor1, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null") || tag1.StartsWith("white"))
                           {
                               coordinates.Add(tempcor1);
                           }
                       }
                       downleftnum--;
                       if (downleftnum >= 1)
                       {
                           tempcor1 = xx.ToString() + downleftnum.ToString();
                           ar = this.Controls.Find(tempcor1, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null") || tag1.StartsWith("white"))
                           {
                               coordinates.Add(tempcor1);
                           }
                       }
                   }
               }

               //for black que

               else if (p.Tag.ToString() == "blackqueen")
               {
                   previouspb = p;
                   string name = p.Name.ToString();
                   char a = name[0];
                   char b = name[1];
                   int tempnum = (int)char.GetNumericValue(b);
                   int upnum = tempnum;
                   int downnum = tempnum;
                   char rightchar = a;
                   char leftchar = a;
                   string nextcoord = "";

                   //for up

                   for (int j = tempnum; j <= 8; j++)
                   {
                       upnum++;
                       if (upnum <= 8)
                       {
                           nextcoord = a.ToString() + upnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for down

                   for (int k = 1; k <= tempnum; k++)
                   {
                       downnum--;
                       if (downnum >= 1)
                       {
                           nextcoord = a.ToString() + downnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for right 

                   for (char k = a; k <= 'h'; k++)
                   {
                       rightchar++;
                       if (rightchar <= 'h')
                       {
                           nextcoord = rightchar.ToString() + tempnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for left 

                   for (char l = a; l >= 'a'; l--)
                   {
                       leftchar--;
                       if (leftchar >= 'a')
                       {
                           nextcoord = leftchar.ToString() + tempnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }


                   int upperrightnextnum = tempnum;
                   char upperrightnextchar = a;
                   int upperleftnextnum = tempnum;
                   char upperleftnextchar = a;
                   int lowerrightnextnum = tempnum;
                   char lowerrightnextchar = a;
                   int lowerleftnextnum = tempnum;
                   char lowerleftnextchar = a;

                   //for upper right diagonal

                   for (int loop = tempnum; loop <= 8; loop++)
                   {
                       upperrightnextnum++;
                       upperrightnextchar++;
                       if (upperrightnextnum <= 8 && upperrightnextchar <= 'h')
                       {
                           nextcoord = upperrightnextchar.ToString() + upperrightnextnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for upper left diagonal

                   for (char l = 'a'; l <= a; l++)
                   {
                       upperleftnextchar--;
                       upperleftnextnum++;
                       if (upperleftnextnum <= 8 && upperleftnextchar >= 'a')
                       {
                           nextcoord = upperleftnextchar.ToString() + upperleftnextnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }

                       }
                   }

                   //for lower left diagonal

                   for (char k = a; k >= 'a'; k--)
                   {
                       lowerleftnextchar--;
                       lowerleftnextnum--;
                       if (lowerleftnextnum >= 1 && lowerleftnextchar >= 'a')
                       {
                           nextcoord = lowerleftnextchar.ToString() + lowerleftnextnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

                   //for lower right diagonal

                   for (char pp = a; pp <= 'h'; pp++)
                   {
                       lowerrightnextchar++;
                       lowerrightnextnum--;
                       if (lowerrightnextnum >= 1 && lowerrightnextchar <= 'h')
                       {
                           nextcoord = lowerrightnextchar.ToString() + lowerrightnextnum.ToString();
                           ar = this.Controls.Find(nextcoord, true);
                           string tag1 = ar[0].Tag.ToString();
                           if (tag1.Contains("null"))
                           {
                               coordinates.Add(nextcoord);
                           }
                           if (tag1.StartsWith("black"))
                           {
                               break;
                           }
                           if (tag1.StartsWith("white"))
                           {
                               coordinates.Add(nextcoord);
                               break;
                           }
                       }
                   }

               }

           }
           if (blackclickcount == 2)
           {
               if (coordinates.IndexOf(p.Name.ToString()) != -1)
               {
                   string tagtemp = p.Tag.ToString();
                   if (tagtemp.StartsWith("white") && tagtemp.EndsWith("king"))
                   {
                       blackclickcount = 0;
                       return;
                   }
                   if (tagtemp.StartsWith("white"))
                   {
                       if (previouspb.Tag.ToString().EndsWith("pawn"))
                       {
                           blackpoints = blackpoints + 1;
                           blackscores = blackpoints * 20;
                       }
                       else if (previouspb.Tag.ToString().EndsWith("knight"))
                       {
                           blackpoints = blackpoints + 3;
                           blackscores = blackpoints * 20;

                       }
                       else if (previouspb.Tag.ToString().EndsWith("bishop"))
                       {
                           blackpoints = blackpoints + 3;
                           blackscores = blackpoints * 20;

                       }
                       else if (previouspb.Tag.ToString().EndsWith("rook"))
                       {
                           blackpoints = blackpoints + 5;
                           blackscores = blackpoints * 20;

                       }
                       else if (previouspb.Tag.ToString().EndsWith("queen"))
                       {
                           blackpoints = blackpoints + 9;
                           blackscores = blackpoints * 20;

                       }
                       else if (previouspb.Tag.ToString().EndsWith("king"))
                       {
                           blackpoints = blackpoints + 90;
                           blackscores = blackpoints * 20;

                       }
                       pointsblacklabel31.Text = blackpoints.ToString();
                       scoreblacklabel.Text = blackscores.ToString();
                       whitedieindex++;
                       positionblacklabel30.Text = p.Name.ToString().ToUpper();
                       string diepb = "whitediepb" + whitedieindex;
                       ar = this.Controls.Find(diepb, true);
                       ar[0].BackgroundImage = p.Image;
                       ar[0].BackgroundImageLayout = ImageLayout.Center;
                       p.Tag = previouspb.Tag;
                       previouspb.Tag = "null";
                       p.Image = previouspb.Image;
                       previouspb.Image = null;
                       string checkpawn = "";
                       checkpawn = p.Tag.ToString();
                       if (checkpawn.Substring(5, 4) == "pawn")
                       {
                           pawnfirsttimemoved.Add(p.Name.ToString());
                       }
                       movecheck = true;
                       playerstatuslabel.Text = "Opening game - White to play";
                       blacklistboxcount++;
                       string[] row = { blacklistboxcount.ToString(), blacktime.Hour+" : "+blacktime.Min+" : "+blacktime.Sec, p.Tag.ToString() + " form " + previouspb.Name.ToString() + " to " + p.Name.ToString()+" killed "+tagtemp };
                       ListViewItem li = new ListViewItem(row);
                       listView1.Items.Add(li);

                   }
                   else
                   {
                       if (previouspb.Tag.ToString().EndsWith("pawn"))
                       {
                           blackpoints = blackpoints + 1;
                           blackscores = blackpoints * 10;
                       }
                       else if (previouspb.Tag.ToString().EndsWith("knight"))
                       {
                           blackpoints = blackpoints + 3;
                           blackscores = blackpoints * 10;

                       }
                       else if (previouspb.Tag.ToString().EndsWith("bishop"))
                       {
                           blackpoints = blackpoints + 3;
                           blackscores = blackpoints * 10;

                       }
                       else if (previouspb.Tag.ToString().EndsWith("rook"))
                       {
                           blackpoints = blackpoints + 5;
                           blackscores = blackpoints * 10;

                       }
                       else if (previouspb.Tag.ToString().EndsWith("queen"))
                       {
                           blackpoints = blackpoints + 9;
                           blackscores = blackpoints * 10;

                       }
                       else if (previouspb.Tag.ToString().EndsWith("king"))
                       {
                           blackpoints = blackpoints + 90;
                           blackscores = blackpoints * 10;

                       }
                       pointsblacklabel31.Text = blackpoints.ToString();
                       scoreblacklabel.Text = blackscores.ToString();
                       positionblacklabel30.Text = p.Name.ToString().ToUpper();
                       p.Image = previouspb.Image;
                       previouspb.Image = null;
                       string temptag = previouspb.Tag.ToString();
                       previouspb.Tag = p.Tag;
                       p.Tag = temptag;
                       string checkpawn = "";
                       checkpawn = p.Tag.ToString();
                       if (checkpawn.Substring(5, 4) == "pawn")
                       {
                           pawnfirsttimemoved.Add(p.Name.ToString());
                       }
                       movecheck = true;
                       playerstatuslabel.Text = "Opening game - White to play";
                       blacklistboxcount++;
                       string[] row = { blacklistboxcount.ToString(), blacktime.Hour+" : "+blacktime.Min+" : "+blacktime.Sec, p.Tag.ToString() + " from " + previouspb.Name.ToString() + " to " + p.Name.ToString() };
                       ListViewItem li = new ListViewItem(row);
                       listView1.Items.Add(li);
                   }
               }
               else
               {

                   MessageBox.Show("Invalid move :  Try again ");
               }
               blackclickcount = 0;
               coordinates.Clear();
           }
      }

      private void whitetimer_Tick(object sender, EventArgs e)
      {
          if (movecheck == true)
          {
              whitetimer.Start();
              blacktimer.Stop();

          }
          if (movecheck == false)
          {
              whitetimer.Stop();
              blacktimer.Start();
          }
          whitetime.Sec++;
          clockwhitelabel24.Text = whitetime.Hour + " : " + whitetime.Min + " : " + whitetime.Sec;
          if (whitetime.Sec >= 59)
          {
              whitetime.Min++;
              whitetime.Sec = 0;
              clockwhitelabel24.Text = whitetime.Hour + " : " + whitetime.Min + " : " + whitetime.Sec;
          }
          if (whitetime.Min >= 59)
          {
              whitetime.Min = 0;
              whitetime.Hour++;
              clockwhitelabel24.Text = whitetime.Hour + " : " + whitetime.Min + " : " + whitetime.Sec;
          }
          
          

      }

      private void backtimer_Tick(object sender, EventArgs e)
      {
          if (movecheck == true)
          {
              whitetimer.Start();
              blacktimer.Stop();

          }
          if (movecheck == false)
          {
              whitetimer.Stop();
              blacktimer.Start();
          }
          blacktime.Sec++;   
          clockblacklabel33.Text = blacktime.Hour + " : " + blacktime.Min + " : " + blacktime.Sec;
          if (blacktime.Sec >= 59)
          {
              blacktime.Sec = 0;
              blacktime.Min++;
              clockblacklabel33.Text = blacktime.Hour + " : " + blacktime.Min + " : " + blacktime.Sec;

          }
          if (blacktime.Min >= 59)
          {
              whitetime.Min = 0;
              blacktime.Hour++;
              clockblacklabel33.Text = blacktime.Hour + " : " + blacktime.Min + " : " + blacktime.Sec;
          }

      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
          this.Close();
      }

    }
}
