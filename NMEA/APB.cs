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
   /// <summary>
   /// The APB sentence is the Autopilot "B" sentence.
   /// </summary>
   public class APB : Response
   {
      public NMEA.Boolean IsLoranBlinkOK;
      public NMEA.Boolean IsLoranCCycleLockOK;
      public double CrossTrackErrorMagnitude;
      public NMEA.LeftOrRight DirectionToSteer;
      public string CrossTrackUnits;
      public NMEA.Boolean IsArrivalCircleEntered;
      public NMEA.Boolean IsPerpendicular;
      public double BearingOriginToDestination;
      public string BearingOriginToDestinationUnits;
      public string To;
      public double BearingPresentPositionToDestination;
      public string BearingPresentPositionToDestinationUnits;
      public double HeadingToSteer;
      public string HeadingToSteerUnits;
      public NMEA.FAAModeIndicator FAAMode;

      public APB() : base("APB")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         IsLoranBlinkOK = NMEA.Boolean.Unknown;
         IsLoranCCycleLockOK = NMEA.Boolean.Unknown;
         CrossTrackErrorMagnitude = 0.0D;
         DirectionToSteer = NMEA.LeftOrRight.Unknown;
         CrossTrackUnits = string.Empty;
         IsArrivalCircleEntered = NMEA.Boolean.Unknown;
         IsPerpendicular = NMEA.Boolean.Unknown;
         BearingOriginToDestination = 0.0D;
         BearingOriginToDestinationUnits = string.Empty;
         To = string.Empty;
         BearingPresentPositionToDestination = 0.0D;
         BearingPresentPositionToDestinationUnits = string.Empty;
         HeadingToSteer = 0.0D;
         HeadingToSteerUnits = string.Empty;
         FAAMode = NMEA.FAAModeIndicator.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** APB - Autopilot Sentence "B"
         **                                         13    15
         **        1 2 3   4 5 6 7 8   9 10   11  12|   14|
         **        | | |   | | | | |   | |    |   | |   | |
         ** $--APB,A,A,x.x,a,N,A,A,x.x,a,c--c,x.x,a,x.x,a*hh<CR><LF>
         **
         **  1) Status
         **     V = LORAN-C Blink or SNR warning
         **     V = general warning flag or other navigation systems when a reliable
         **         fix is not available
         **  2) Status
         **     V = Loran-C Cycle Lock warning flag
         **     A = OK or not used
         **  3) Cross Track Error Magnitude
         **  4) Direction to steer, L or R
         **  5) Cross Track Units, N = Nautical Miles
         **  6) Status
         **     A = Arrival Circle Entered
         **  7) Status
         **     A = Perpendicular passed at waypoint
         **  8) Bearing origin to destination
         **  9) M = Magnetic, T = True
         ** 10) Destination Waypoint ID
         ** 11) Bearing, present position to Destination
         ** 12) M = Magnetic, T = True
         ** 13) Heading to steer to destination waypoint
         ** 14) M = Magnetic, T = True
         ** 15) Checksum or FAA Mode Indicator
         ** 16) Checksum if FAA Mode Indicator is present
         */

         // 2005-02-24, according to http://gpsd.berlios.de/NMEA.txt, NMEA added another field to the sentence

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         IsLoranBlinkOK = sentence.Boolean(1);
         IsLoranCCycleLockOK = sentence.Boolean(2);
         CrossTrackErrorMagnitude = sentence.Double(3);
         DirectionToSteer = sentence.LeftOrRight(4);
         CrossTrackUnits = sentence.Field(5);
         IsArrivalCircleEntered = sentence.Boolean(6);
         IsPerpendicular = sentence.Boolean(7);
         BearingOriginToDestination = sentence.Double(8);
         BearingOriginToDestinationUnits = sentence.Field(9);
         To = sentence.Field(10);
         BearingPresentPositionToDestination = sentence.Double(11);
         BearingPresentPositionToDestinationUnits = sentence.Field(12);
         HeadingToSteer = sentence.Double(13);
         HeadingToSteerUnits = sentence.Field(14);

         if (sentence.GetNumberOfDataFields() == 16)
         {
            // Parse here...
            FAAMode = sentence.FAAMode(15);
         }
         else
         {
            FAAMode = FAAModeIndicator.Unknown;
         }

         return (true);
      }
   }
}
