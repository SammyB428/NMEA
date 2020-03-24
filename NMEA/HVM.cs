﻿#region License
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
   public class HVM : Response
   {
      public double MagneticVariationDegrees;
      public NMEA.EastOrWest MagneticVariationDirection;

      public HVM() : base("HVM")
      {
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         MagneticVariationDegrees = 0.0D;
         MagneticVariationDirection = EastOrWest.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** HVM - Magnetic Variation, Manually set
         **
         **        1   2 3
         **        |   | |
         ** $--HVM,x.x,a*hh<CR><LF>
         **
         ** Field Number: 
         **  1) Magnetic Variation degrees
         **  2) Magnetic Variation direction, E = Easterly, W = Westerly
         **  3) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         MagneticVariationDegrees = sentence.Double(1);
         MagneticVariationDirection = sentence.EastOrWest(2);

         return (true);
      }
   }
}
