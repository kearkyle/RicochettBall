//****************************************************************************************************************************
//Program name: "Ricochet". This is a simple ball ricochetting on walls.                                                           *
//Copyright (C) 2020  Vong Chen                                                                                        *
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License  *
//version 3 as published by the Free Software Foundation.                                                                    *
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied         *
//warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.     *
//A copy of the GNU General Public License v3 is available here:  <https://www.gnu.org/licenses/>.                           *
//****************************************************************************************************************************

//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**

//Author: Vong Chen
//Mail: vchen7@csu.fullerton.edu

//Program name:  Skateboard Animation
//Programming language: C Sharp
//Date development of program began: 2020-November-06
//Date of last update: 2020-November-07
//Course ID: CPSC 223N-01
//Assignment number: 04
//Date assignment is due: 2020-November-07

//Purpose:  This program will show a ball bouncing around in a user panel

//Files in project:  Ricomain.cs, Ricoui.cs

//This file's name: Ricoui.cs
//This file purpose: This file contains the structures of the user interface window
//Date last modified: 2020-November-07
//To compile Ricoui.cs:
//              mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:Ricoui.dll Ricoui.cs


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class Ricochetinterface : Form
{

    private Label author = new Label();
    private Label speed_label = new Label();
    private Label direction_label = new Label();
    private Label coord_label = new Label();
    private Label x_label = new Label();
    private Label y_label = new Label();
    private TextBox speed_text = new TextBox();
    private TextBox direction_text = new TextBox();
    private TextBox x_text = new TextBox();
    private TextBox y_text = new TextBox();
    private Button startbutton = new Button();
    private Button newbutton = new Button();
    private Button exitbutton = new Button();
    private Panel headerpanel = new Panel();
    private Rectangle display = new Rectangle(0,100,1024,500);
    private Panel controlpanel = new Panel();
    //Ball coords + time
    private const double ball_center_initial_coord_x = 512;
    private const double ball_center_initial_coord_y = 325;
    private double ball_center_current_coord_x;
    private double ball_center_current_coord_y;
    private double ball_upper_left_current_coord_x;
    private double ball_upper_left_current_coord_y;
    private double ball_delta_x;
    private double ball_delta_y;
    private double ball_direction;
    private double radians;
    //Time
    private double ball_linear_speed_pix_per_sec;
    private double ball_linear_speed_pix_per_tic;
    //Declare data about the motion clock:
    private static System.Timers.Timer ball_motion_control_clock = new System.Timers.Timer();
    private const double ball_motion_control_clock_rate = 43.5;  //Units are Hz
    //Declare data about the refresh clock;
    private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
    private const double graphic_refresh_rate = 23.3;  //Units are Hz = #refreshes per second

    //ui size
    private Size maximuminterfacesize = new Size(1024, 800);
    private Size minimuminterfacesize = new Size(1024, 800);

    private const double ball_radius = 10;    //Radius measured in pixels

    public Ricochetinterface()  //Constructor
    {//Set the size of the user interface box.
       System.Console.WriteLine("formwidth = 1024. formheight = 800.");
       Size = new Size(400,240); 
       //Set the limits regarding how much the user may re-size the window.
       MaximumSize = maximuminterfacesize;
       MinimumSize = minimuminterfacesize;
        //Initialize text strings
        Text = "Assignment 4";
        author.Text = "Ricochet Ball by Vong Chen";
        speed_label.Text = "Enter Speed (pixel/second)";
        direction_label.Text = "Enter direction (degrees)";
        coord_label.Text = "Coordinates of center of ball";
        x_label.Text = "X =";
        y_label.Text = "Y =";
        startbutton.Text = "Start";
        newbutton.Text = "New";
        exitbutton.Text = "Quit";

        //Set sizes
        author.Size = new Size(800, 44);
        speed_label.Size = new Size(150, 20);
        direction_label.Size = new Size(150, 20);
        coord_label.Size = new Size(150, 20);
        x_label.Size = new Size(25, 20);
        y_label.Size = new Size(25, 20);
        startbutton.Size = new Size(60, 30);
        newbutton.Size = new Size(60, 30);
        exitbutton.Size = new Size(60, 30);
        headerpanel.Size = new Size(1024, 100);
        controlpanel.Size = new Size(1024, 200);
        speed_text.Size = new Size(75,30);
        direction_text.Size = new Size(75,30);
        x_text.Size = new Size(45,40);
        y_text.Size = new Size(45,40);

        //Set colors
        headerpanel.BackColor = Color.Blue;
        controlpanel.BackColor = Color.LightBlue;
        startbutton.BackColor = Color.Gray;
        newbutton.BackColor = Color.Gray;
        exitbutton.BackColor = Color.Gray;
        speed_label.BackColor = Color.LightGreen;
        direction_label.BackColor = Color.LightGreen;
        x_label.BackColor = Color.LightGreen;
        coord_label.BackColor = Color.LightGreen;
        y_label.BackColor = Color.LightGreen;

        //Set fonts
        author.Font = new Font("Times New Roman", 26, FontStyle.Regular);
        speed_label.Font = new Font("Arial", 8, FontStyle.Regular);
        direction_label.Font = new Font("Arial", 8, FontStyle.Regular);
        coord_label.Font = new Font("Arial", 8, FontStyle.Regular);
        x_label.Font = new Font("Arial", 8, FontStyle.Regular);
        y_label.Font = new Font("Arial", 8, FontStyle.Regular);
        speed_text.Font = new Font("Arial", 10, FontStyle.Regular);
        direction_text.Font = new Font("Arial", 10, FontStyle.Regular);
        x_text.Font = new Font("Arial", 10, FontStyle.Regular);
        y_text.Font = new Font("Arial", 10, FontStyle.Regular);
        startbutton.Font = new Font("Liberation Serif", 15, FontStyle.Regular);
        newbutton.Font = new Font("Liberation Serif", 15, FontStyle.Regular);
        exitbutton.Font = new Font("Liberation Serif", 15, FontStyle.Regular);
        speed_label.TextAlign = ContentAlignment.MiddleCenter;
        direction_label.TextAlign = ContentAlignment.MiddleCenter;
        x_label.TextAlign = ContentAlignment.MiddleCenter;
        y_label.TextAlign = ContentAlignment.MiddleCenter;
        coord_label.TextAlign = ContentAlignment.MiddleCenter;


        //Set locations
        headerpanel.Location = new Point(0, 0);
        author.Location = new Point(300, 40);
        speed_label.Location = new Point(200, 20);
        speed_text.Location = new Point(360,20);
        direction_label.Location = new Point(620, 20);
        direction_text.Location = new Point(780,20);
        coord_label.Location = new Point(530, 75);
        x_label.Location = new Point(500, 100);
        x_text.Location = new Point(540,100);
        y_label.Location = new Point(600, 100);
        y_text.Location = new Point(640,100);
        startbutton.Location = new Point(50, 80);
        newbutton.Location = new Point(50, 30);
        exitbutton.Location = new Point(920, 90);
        headerpanel.Location = new Point(0, 0);
        controlpanel.Location = new Point(0, 570);

        //Associate the Compute button with the Enter key of the keyboard
        AcceptButton = newbutton;

        
        //Setting up the clocks
        //Set up the motion clock.  This clock controls the rate of update of the coordinates of the ball.
        ball_motion_control_clock.Enabled = false;
        ball_motion_control_clock.Elapsed += new ElapsedEventHandler(Update_ball_position);

        //Set up the refresh clock.
        graphic_area_refresh_clock.Enabled = false;  //Initially the clock controlling the rate of updating the display is stopped.
        graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_display);  

        //Add controls to  the form
        Controls.Add(headerpanel);
            headerpanel.Controls.Add(author);
        Controls.Add(controlpanel);
            controlpanel.Controls.Add(speed_label);
            controlpanel.Controls.Add(direction_label);
            controlpanel.Controls.Add(coord_label);
            controlpanel.Controls.Add(x_label);
            controlpanel.Controls.Add(y_label);
            controlpanel.Controls.Add(startbutton);
            controlpanel.Controls.Add(newbutton);
            controlpanel.Controls.Add(exitbutton);
            controlpanel.Controls.Add(speed_text);
            controlpanel.Controls.Add(direction_text);
            controlpanel.Controls.Add(x_text);
            controlpanel.Controls.Add(y_text);
        //Ball initial coords
        ball_center_current_coord_x = ball_center_initial_coord_x;
        ball_center_current_coord_y = ball_center_initial_coord_y;
        System.Console.WriteLine("Initial coordinates: ball_center_current_coord_x = {0}. ball_center_current_coord_y = {1}.",
                               ball_center_current_coord_x,ball_center_current_coord_y);
        //Register the event handler.  In this case each button has an event handler, but no other 
        //controls have event handlers.
        startbutton.Click += new EventHandler(All_systems_go);
        newbutton.Click += new EventHandler(newvalue);
        exitbutton.Click += new EventHandler(stoprun);  //The '+' is required.

        //Open this user interface window in the center of the display.
        CenterToScreen();

    }//End of constructor

    
    protected override void OnPaint(PaintEventArgs ee) {
        Graphics graph = ee.Graphics;

        graph.FillRectangle(Brushes.Cornsilk,display); // displays background
        ball_upper_left_current_coord_x = ball_center_current_coord_x - ball_radius;
        ball_upper_left_current_coord_y = ball_center_current_coord_y - ball_radius;
        graph.FillEllipse(Brushes.Red,(int)ball_upper_left_current_coord_x,
                                      (int)ball_upper_left_current_coord_y,
                                      (float)(2.0*ball_radius),
                                      (float)(2.0*ball_radius));

    base.OnPaint(ee);
    } // End of Onpain

    protected void Update_ball_position(System.Object sender, ElapsedEventArgs evt)
   {  ball_center_current_coord_x += ball_delta_x;
      ball_center_current_coord_y -= ball_delta_y;  
      //Determine if the ball has made a collision with the right wall.
      if((int)System.Math.Round(ball_center_current_coord_x + ball_radius) >= 1014)
             ball_delta_x = -ball_delta_x;
      //Determine if the ball has made a collision with the lower wall
      if((int)System.Math.Round(ball_center_current_coord_y + ball_radius) >= 569.5) 
            ball_delta_y = -ball_delta_y;
      //Determine if the ball has made a collision with the left wall
      if((int)System.Math.Round(ball_center_current_coord_y - ball_radius) <= 100)  
            ball_delta_y = -ball_delta_y;
      //Determine if the ball has made a collision with the upper wall
      if((int)System.Math.Round(ball_center_current_coord_x - ball_radius) <= 0)  
            ball_delta_x = -ball_delta_x;

   }//End of method Update_ball_position

    protected void All_systems_go(Object sender,EventArgs events)
   {//The refreshclock is started.
        Start_graphic_clock(graphic_refresh_rate);
    //The motion clock is started.
        Start_ball_clock(ball_motion_control_clock_rate);
        ball_linear_speed_pix_per_sec = Double.Parse(speed_text.Text);
        ball_direction = Double.Parse(direction_text.Text);
        radians = (ball_direction * (Math.PI)) / 180;
        ball_linear_speed_pix_per_tic = ball_linear_speed_pix_per_sec/ball_motion_control_clock_rate;
        ball_delta_x = ball_linear_speed_pix_per_tic * Math.Cos(radians);
        ball_delta_y = ball_linear_speed_pix_per_tic * Math.Sin(radians);
        startbutton.Enabled = false;
        newbutton.Enabled = true;
    }

   protected void Start_graphic_clock(double refresh_rate)
   {   double actual_refresh_rate = 1.0;  //Minimum refresh rate is 1 Hz to avoid a potential division by a number close to zero
       double elapsed_time_between_tics;
       if(refresh_rate > actual_refresh_rate) 
           actual_refresh_rate = refresh_rate;
       elapsed_time_between_tics = 1000.0/actual_refresh_rate;  //elapsedtimebetweentics has units milliseconds.
       graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_tics);
       graphic_area_refresh_clock.Enabled = true;  //Start clock ticking.
   }

   protected void Start_ball_clock(double update_rate)
   {   double elapsed_time_between_ball_moves;
       if(update_rate < 1.0) update_rate = 1.0;  //This program does not allow updates slower than 1 Hz.
       elapsed_time_between_ball_moves = 1000.0/update_rate;  //1000.0ms = 1second.  
       //The variable elapsed_time_between_ball_moves has units "milliseconds".
       ball_motion_control_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball_moves);
       ball_motion_control_clock.Enabled = true;   //Start clock ticking.
   }
   protected void Update_display(System.Object sender, ElapsedEventArgs evt)
   {  Invalidate();  //This creates an artificial event so that the graphic area will repaint itself.
      x_text.Text = String.Format("{000}",ball_center_current_coord_x);
      y_text.Text = String.Format("{000}",ball_center_current_coord_y);
      if(!ball_motion_control_clock.Enabled)
          {graphic_area_refresh_clock.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }
    
    protected void newvalue(Object sender, EventArgs events) {
        System.Console.WriteLine("ALl values have been resetted.");
        ball_motion_control_clock.Enabled = false;
        graphic_area_refresh_clock.Enabled = false;
        speed_text.Text = "";
        direction_text.Text = "";
        x_text.Text = "";
        y_text.Text = "";
        ball_center_current_coord_x = ball_center_initial_coord_x - ball_radius;
        ball_center_current_coord_y = ball_center_initial_coord_y - ball_radius;
        startbutton.Enabled = true;
        newbutton.Enabled = false;
        Invalidate();
    }
    
    //Method to execute when the exit button receives an event, namely: receives a mouse click
    protected void stoprun(Object sender, EventArgs events)
    {
        Close();
    }//End of stoprun

}//End of class