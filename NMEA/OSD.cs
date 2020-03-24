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
   public class OSD : Response
   {
      public double HeadingDegreesTrue;
      public Boolean IsHeadingValid;
      public double VesselCourseDegreesTrue;
      public Reference VesselCourseReference;
      public double VesselSpeed;
      public Reference VesselSpeedReference;
      public double VesselSetDegreesTrue;
      public double VesselDriftSpeed;
      public string VesselDriftSpeedUnits;

      public OSD() : base("OSD")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         HeadingDegreesTrue = 0.0D;
         IsHeadingValid = Boolean.Unknown;
         VesselCourseDegreesTrue = 0.0D;
         VesselCourseReference = Reference.Unknown;
         VesselSpeed = 0.0D;
         VesselSpeedReference = Reference.Unknown;
         VesselSetDegreesTrue = 0.0D;
         VesselDriftSpeed = 0.0D;
         VesselDriftSpeedUnits = string.Empty;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** OSD - Own Ship Data
         **
         **        1   2 3   4 5   6 7   8   9 10
         **        |   | |   | |   | |   |   | |
         ** $--OSD,x.x,A,x.x,a,x.x,a,x.x,x.x,a*hh<CR><LF>
         **
         **  1) Heading, degrees true
         **  2) Status, A = Data Valid
         **  3) Vessel Course, degrees True
         **  4) Course Reference
         **  5) Vessel Speed
         **  6) Speed Reference
         **  7) Vessel Set, degrees True
         **  8) Vessel drift (speed)
         **  9) Speed Units
         ** 10) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         HeadingDegreesTrue = sentence.Double(1);
         IsHeadingValid = sentence.Boolean(2);
         VesselCourseDegreesTrue = sentence.Double(3);
         VesselCourseReference = sentence.Reference(4);
         VesselSpeed = sentence.Double(5);
         VesselSpeedReference = sentence.Reference(6);
         VesselSetDegreesTrue = sentence.Double(7);
         VesselDriftSpeed = sentence.Double(8);
         VesselDriftSpeedUnits = sentence.Field(9);

         return (true);
      }
   }
}
