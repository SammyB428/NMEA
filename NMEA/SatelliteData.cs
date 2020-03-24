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
   public class SatelliteData
   {
      public int SatelliteNumber;
      public int ElevationDegrees;
      public int AzimuthDegreesTrue;
      public int SignalToNoiseRatio;

      public SatelliteData()
      {
      }

      public void Empty()
      {
         SatelliteNumber = 0;
         ElevationDegrees = 0;
         AzimuthDegreesTrue = 0;
         SignalToNoiseRatio = 0;
      }

      public void Parse(int first_field_number, Sentence sentence)
      {
         SatelliteNumber = sentence.Integer(first_field_number);
         ElevationDegrees = sentence.Integer(first_field_number + 1);
         AzimuthDegreesTrue = sentence.Integer(first_field_number + 2);

         string field_data = sentence.Field(first_field_number + 3);

         if (field_data.Length == 0)
         {
            SignalToNoiseRatio = (-1);
         }
         else
         {
            SignalToNoiseRatio = sentence.Integer(first_field_number + 3);
         }
      }
   }
}
