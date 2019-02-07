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
   public class TTM : Response
   {
      public int TargetNumber;
      public double TargetDistance;
      public double BearingFromOwnShip;
      public string BearingUnits;
      public double TargetSpeed;
      public double TargetCourse;
      public string TargetCourseUnits;
      public double DistanceOfClosestPointOfApproach;
      public double NumberOfMinutesToClosestPointOfApproach;
      public string Increasing;
      public string TargetName;
      public NMEA.TargetStatus TargetStatus;
      public string ReferenceTarget;

      public TTM()
      {
         Mnemonic = "TTM";
      }

      public override void Empty()
      {
         base.Empty();

         TargetNumber = 0;
         TargetDistance = 0.0;
         BearingFromOwnShip = 0.0;
         BearingUnits = "";
         TargetSpeed = 0.0;
         TargetCourse = 0.0;
         TargetCourseUnits = "";
         DistanceOfClosestPointOfApproach = 0.0;
         NumberOfMinutesToClosestPointOfApproach = 0.0;
         Increasing = "";
         TargetName = "";
         TargetStatus = TargetStatus.Unknown;

         Mnemonic = "TTM";
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** TTM - Tracked Target Message
         **
         **                                         11     13
         **        1  2   3   4 5   6   7 8   9   10|    12| 14
         **        |  |   |   | |   |   | |   |   | |    | | |
         ** $--TTM,xx,x.x,x.x,a,x.x,x.x,a,x.x,x.x,a,c--c,a,a*hh<CR><LF>
         **
         **  1) Target Number
         **  2) Target Distance
         **  3) Bearing from own ship
         **  4) Bearing Units
         **  5) Target speed
         **  6) Target Course
         **  7) Course Units
         **  8) Distance of closest-point-of-approach
         **  9) Time until closest-point-of-approach "-" means increasing
         ** 10) "-" means increasing
         ** 11) Target name
         ** 12) Target Status
         ** 13) Reference Target
         ** 14) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         TargetNumber = sentence.Integer(1);
         TargetDistance = sentence.Double(2);
         BearingFromOwnShip = sentence.Double(3);
         BearingUnits = sentence.Field(4);
         TargetSpeed = sentence.Double(5);
         TargetCourse = sentence.Double(6);
         TargetCourseUnits = sentence.Field(7);
         DistanceOfClosestPointOfApproach = sentence.Double(8);
         NumberOfMinutesToClosestPointOfApproach = sentence.Double(9);
         Increasing = sentence.Field(10);
         TargetName = sentence.Field(11);

         string field_data = sentence.Field(12);

         if (field_data == "L")
         {
            TargetStatus = TargetStatus.Lost;
         }
         else if (field_data == "Q")
         {
            TargetStatus = TargetStatus.Query;
         }
         else if (field_data == "T")
         {
            TargetStatus = TargetStatus.Tracking;
         }
         else
         {
            TargetStatus = TargetStatus.Unknown;
         }

         ReferenceTarget = sentence.Field(13);

         return (true);
      }
   }
}
