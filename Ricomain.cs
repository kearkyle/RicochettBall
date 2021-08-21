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

//To compile Ricomain.cs
//                              mcs -r:System -r:System.Windows.Forms -r:Ricoui.dll -out:Rico.exe Ricomain.cs


using System;

using System.Windows.Forms;

public class Ricomain
{
    static void Main(string[] args)
    {System.Console.WriteLine("Welcome to the main method of animated skateboard program.");
     Ricochetinterface ricochetapp = new Ricochetinterface();
     Application.Run(ricochetapp);
     System.Console.WriteLine("Main method will now shutdown");
    }
}
