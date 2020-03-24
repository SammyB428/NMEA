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
   public class VBW : Response
   {
      public double LongitudinalWaterSpeed;
      public double TransverseWaterSpeed;
      public Boolean IsWaterSpeedValid;
      public double LongitudinalGroundSpeed;
      public double TransverseGroundSpeed;
      public Boolean IsGroundSpeedValid;

      public VBW() : base("VBW")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         LongitudinalWaterSpeed = 0.0D;
         TransverseWaterSpeed = 0.0D;
         IsWaterSpeedValid = Boolean.Unknown;
         LongitudinalGroundSpeed = 0.0D;
         TransverseGroundSpeed = 0.0D;
         IsGroundSpeedValid = Boolean.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** VBW - Dual Ground/Water Speed
         **
         **        1   2   3 4   5   6 7
         **        |   |   | |   |   | |
         ** $--VBW,x.x,x.x,A,x.x,x.x,A*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Longitudinal water speed, "-" means astern
         **  2) Transverse water speed, "-" means port
         **  3) Status, A = Data Valid
         **  4) Longitudinal ground speed, "-" means astern
         **  5) Transverse ground speed, "-" means port
         **  6) Status, A = Data Valid
         **  7) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         LongitudinalWaterSpeed = sentence.Double(1);
         TransverseWaterSpeed = sentence.Double(2);
         IsWaterSpeedValid = sentence.Boolean(3);
         LongitudinalGroundSpeed = sentence.Double(4);
         TransverseGroundSpeed = sentence.Double(5);
         IsGroundSpeedValid = sentence.Boolean(6);

         return (true);
      }
   }
}
