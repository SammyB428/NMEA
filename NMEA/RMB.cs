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
   public class RMB : Response
   {
      public Boolean IsDataValid;
      public double CrossTrackError;
      public LeftOrRight DirectionToSteer;
      public string To;
      public string From;
      public LatLong DestinationPosition;
      public double RangeToDestinationNauticalMiles;
      public double BearingToDestinationDegreesTrue;
      public double DestinationClosingVelocityKnots;
      public Boolean IsArrivalCircleEntered;
      public FAAModeIndicator Mode;

      public RMB() : base("RMB")
      {
         DestinationPosition = new LatLong();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         IsDataValid = Boolean.Unknown;
         CrossTrackError = 0.0D;
         DirectionToSteer = LeftOrRight.Unknown;
         To = string.Empty;
         From = string.Empty;
         DestinationPosition.Empty();
         RangeToDestinationNauticalMiles = 0.0D;
         BearingToDestinationDegreesTrue = 0.0D;
         DestinationClosingVelocityKnots = 0.0D;
         IsArrivalCircleEntered = Boolean.Unknown;
         Mode = NMEA.FAAModeIndicator.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** RMB - Recommended Minimum Navigation Information
         **                                                             14
         **        1 2   3 4    5    6       7 8        9 10  11  12  13|
         **        | |   | |    |    |       | |        | |   |   |   | |
         ** $--RMB,A,x.x,a,c--c,c--c,llll.ll,a,yyyyy.yy,a,x.x,x.x,x.x,A*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Status, V = Navigation receiver warning
         **  2) Cross Track error - nautical miles
         **  3) Direction to Steer, Left or Right
         **  4) TO Waypoint ID
         **  5) FROM Waypoint ID
         **  6) Destination Waypoint Latitude
         **  7) N or S
         **  8) Destination Waypoint Longitude
         **  9) E or W
         ** 10) Range to destination in nautical miles
         ** 11) Bearing to destination in degrees True
         ** 12) Destination closing velocity in knots
         ** 13) Arrival Status, A = Arrival Circle Entered
         ** 14) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         IsDataValid = sentence.Boolean(1);
         CrossTrackError = sentence.Double(2);
         DirectionToSteer = sentence.LeftOrRight(3);
         From = sentence.Field(4);
         To = sentence.Field(5);
         DestinationPosition.Parse(6, 7, 8, 9, sentence);
         RangeToDestinationNauticalMiles = sentence.Double(10);
         BearingToDestinationDegreesTrue = sentence.Double(11);
         DestinationClosingVelocityKnots = sentence.Double(12);
         IsArrivalCircleEntered = sentence.Boolean(13);

         int checksum_field_number = sentence.ChecksumFieldNumber();

         if (checksum_field_number == 15)
         {
            Mode = sentence.FAAMode(14);
         }
         else
         {
            Mode = FAAModeIndicator.Unknown;
         }

         return (true);
      }
   }
}
