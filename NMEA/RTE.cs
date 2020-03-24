#region License
/*
Author: Samuel R. Blackburn
Internet: wfc@pobox.com

"You can get credit for something or get it done, but not both."
Dr. Richard Garwin

The MIT License (MIT)

Copyright (c) 1996-2019 Sam Blackburn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

/* SPDX-License-Identifier: MIT */

#endregion

namespace NMEA
{
   public class RTE : Response
   {
      public RouteType Type;
      public string RouteName;
      public System.Collections.ArrayList Waypoints;

      public RTE() : base("RTE")
      {
         Waypoints = new System.Collections.ArrayList();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         Type = RouteType.Unknown;
         RouteName = string.Empty;
         Waypoints.Clear();
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** RTE - Routes
         **
         **        1   2   3 4	 5		       x    n
         **        |   |   | |    |           |    |
         ** $--RTE,x.x,x.x,a,c--c,c--c, ..... c--c*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Total number of messages being transmitted
         **  2) Message Number
         **  3) Message mode
         **     c = complete route, all waypoints
         **     w = working route, the waypoint you just left, the waypoint you're heading to then all the rest
         **  4) Waypoint ID
         **  x) More Waypoints
         **  n) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         int message_number = sentence.Integer(2);

         if (message_number == 1)
         {
            Empty();
         }

         string temp_string = sentence.Field(3);

         if (temp_string == "c")
         {
            Type = RouteType.Complete;
         }
         else if (temp_string == "w")
         {
            Type = RouteType.Working;
         }
         else
         {
            Type = RouteType.Unknown;
         }

         RouteName = sentence.Field(4);

         int number_of_data_fields = sentence.GetNumberOfDataFields();

         int field_number = 5;

         while (field_number < number_of_data_fields)
         {
            Waypoints.Add(sentence.Field(field_number));
            field_number++;
         }

         return (true);
      }
   }
}