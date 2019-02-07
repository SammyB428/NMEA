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

      public APB()
      {
         Mnemonic = "APB";
      }

      public override void Empty()
      {
         base.Empty();

         IsLoranBlinkOK = NMEA.Boolean.Unknown;
         IsLoranCCycleLockOK = NMEA.Boolean.Unknown;
         CrossTrackErrorMagnitude = 0.0D;
         DirectionToSteer = NMEA.LeftOrRight.Unknown;
         CrossTrackUnits = "";
         IsArrivalCircleEntered = NMEA.Boolean.Unknown;
         IsPerpendicular = NMEA.Boolean.Unknown;
         BearingOriginToDestination = 0.0D;
         BearingOriginToDestinationUnits = "";
         To = "";
         BearingPresentPositionToDestination = 0.0D;
         BearingPresentPositionToDestinationUnits = "";
         HeadingToSteer = 0.0D;
         HeadingToSteerUnits = "";
         FAAMode = NMEA.FAAModeIndicator.Unknown;

         Mnemonic = "APB";
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

         Boolean checksum_is_bad = sentence.IsChecksumBad();

         if (checksum_is_bad == Boolean.True)
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
