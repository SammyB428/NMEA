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
      public TargetStatus TargetStatus;
      public string ReferenceTarget;

      public TTM() : base("TTM")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         TargetNumber = 0;
         TargetDistance = 0.0;
         BearingFromOwnShip = 0.0;
         BearingUnits = string.Empty;
         TargetSpeed = 0.0;
         TargetCourse = 0.0;
         TargetCourseUnits = string.Empty;
         DistanceOfClosestPointOfApproach = 0.0;
         NumberOfMinutesToClosestPointOfApproach = 0.0;
         Increasing = string.Empty;
         TargetName = string.Empty;
         TargetStatus = TargetStatus.Unknown;
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
