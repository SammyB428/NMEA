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
   public class RMB : Response
   {
      public NMEA.Boolean IsDataValid;
      public double CrossTrackError;
      public NMEA.LeftOrRight DirectionToSteer;
      public string To;
      public string From;
      public NMEA.LatLong DestinationPosition;
      public double RangeToDestinationNauticalMiles;
      public double BearingToDestinationDegreesTrue;
      public double DestinationClosingVelocityKnots;
      public Boolean IsArrivalCircleEntered;
      public FAAModeIndicator Mode;

      public RMB()
      {
         DestinationPosition = new LatLong();
         Mnemonic = "RMB";
      }

      public override void Empty()
      {
         base.Empty();

         IsDataValid = Boolean.Unknown;
         CrossTrackError = 0.0D;
         DirectionToSteer = LeftOrRight.Unknown;
         To = "";
         From = "";
         DestinationPosition.Empty();
         RangeToDestinationNauticalMiles = 0.0D;
         BearingToDestinationDegreesTrue = 0.0D;
         DestinationClosingVelocityKnots = 0.0D;
         IsArrivalCircleEntered = Boolean.Unknown;
         Mode = NMEA.FAAModeIndicator.Unknown;

         Mnemonic = "RMB";
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

         Boolean checksum_is_bad = sentence.IsChecksumBad();

         if (checksum_is_bad == Boolean.True)
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
