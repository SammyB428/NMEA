#region License
/*
** Author: Samuel R. Blackburn
** Internet: wfc@pobox.com
**
** "You can get credit for something or get it done, but not both."
** Dr. Richard Garwin
**
** A modified BSD License follows. This license is based on the
** 3-clause license ("New BSD License" or "Modified BSD License") found at
** http://en.wikipedia.org/wiki/BSD_license
** The difference is the dropping of the "advertising" clause. You do not,
** I repeat, DO NOT have to include this copyright notice in binary distributions.
**
** Copyright (c) 2011, Samuel R. Blackburn
** All rights reserved.
**
** Redistribution and use in source and binary forms, with or without
** modification, are permitted provided that the following conditions are met:
**     * Redistributions of source code must retain the above copyright
**       notice, this list of conditions and the following disclaimer.
**     * Neither the name of the <organization> nor the
**       names of its contributors may be used to endorse or promote products
**       derived from this software without specific prior written permission.
**
** THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
** ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
** WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
** DISCLAIMED. IN NO EVENT SHALL SAMUEL R. BLACKBURN BE LIABLE FOR ANY
** DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
** (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
** LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
** ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
** (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
** SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**
*/
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