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
   public class Parser
   {
      private System.Collections.Generic.Dictionary<string, Response> m_Responses;

      public Sentence Sentence;
      public string Parsed;

      public Parser()
      {
         Sentence = new Sentence();
         m_Responses = new System.Collections.Generic.Dictionary<string, Response>();
         m_Responses.Add("AAM", new AAM());
         m_Responses.Add("ALM", new ALM());
         m_Responses.Add("APB", new APB());
         m_Responses.Add("BEC", new BEC());
         m_Responses.Add("BOD", new BOD());
         m_Responses.Add("BWC", new BWC());
         m_Responses.Add("BWR", new BWR());
         m_Responses.Add("BWW", new BWW());
         m_Responses.Add("DBT", new DBT());
         m_Responses.Add("DCN", new DCN());
         m_Responses.Add("DPT", new DPT());
         m_Responses.Add("FSI", new FSI());
         m_Responses.Add("GDA", new GDA());
         m_Responses.Add("GDF", new GDF());
         m_Responses.Add("GDP", new GDP());
         m_Responses.Add("GGA", new GGA());
         m_Responses.Add("GLA", new GLA());
         m_Responses.Add("GLC", new GLC());
         m_Responses.Add("GLF", new GLF());
         m_Responses.Add("GLL", new GLL());
         m_Responses.Add("GLP", new GLP());
         m_Responses.Add("GOA", new GOA());
         m_Responses.Add("GOF", new GOF());
         m_Responses.Add("GOP", new GOP());
         m_Responses.Add("GSA", new GSA());
         m_Responses.Add("GSV", new GSV());
         m_Responses.Add("GTD", new GTD());
         m_Responses.Add("GXA", new GXA());
         m_Responses.Add("GXF", new GXF());
         m_Responses.Add("GXP", new GXP());
         m_Responses.Add("HCC", new HCC());
         m_Responses.Add("HDG", new HDG());
         m_Responses.Add("HDM", new HDM());
         m_Responses.Add("HDT", new HDT());
         m_Responses.Add("HSC", new HSC());
         m_Responses.Add("HVD", new HVD());
         m_Responses.Add("HVM", new HVM());
         m_Responses.Add("IMA", new IMA());
         m_Responses.Add("LCD", new LCD());
         m_Responses.Add("MHU", new MHU());
         m_Responses.Add("MTA", new MTA());
         m_Responses.Add("MTW", new MTW());
         m_Responses.Add("MWD", new MWD());
         m_Responses.Add("MWV", new MWV());
         m_Responses.Add("OLN", new OLN());
         m_Responses.Add("OSD", new OSD());
         m_Responses.Add("P", new P());
         m_Responses.Add("RMA", new RMA());
         m_Responses.Add("RMB", new RMB());
         m_Responses.Add("RMC", new RMC());
         m_Responses.Add("RMM", new RMM());
         m_Responses.Add("ROT", new ROT());
         m_Responses.Add("RPM", new RPM());
         m_Responses.Add("RSA", new RSA());
         m_Responses.Add("RSD", new RSD());
         m_Responses.Add("RTE", new RTE());
         m_Responses.Add("SFI", new SFI());
         m_Responses.Add("STN", new STN());
         m_Responses.Add("TEP", new TEP());
         m_Responses.Add("TRF", new TRF());
         m_Responses.Add("TTM", new TTM());
         m_Responses.Add("VBW", new VBW());
         m_Responses.Add("VDR", new VDR());
         m_Responses.Add("VHW", new VHW());
         m_Responses.Add("VLW", new VLW());
         m_Responses.Add("VPW", new VPW());
         m_Responses.Add("VTG", new VTG());
         m_Responses.Add("VWE", new VWE());
         m_Responses.Add("VWR", new VWR());
         m_Responses.Add("VWT", new VWT());
         m_Responses.Add("WCV", new WCV());
         m_Responses.Add("WDC", new WDC());
         m_Responses.Add("WDR", new WDR());
         m_Responses.Add("WNC", new WNC());
         m_Responses.Add("WPL", new WPL());
         m_Responses.Add("XDR", new XDR());
         m_Responses.Add("XTE", new XTE());
         m_Responses.Add("XTR", new XTR());
         m_Responses.Add("ZDA", new ZDA());
         m_Responses.Add("ZFI", new ZFI());
         m_Responses.Add("ZFO", new ZFO());
         m_Responses.Add("ZLZ", new ZLZ());
         m_Responses.Add("ZPI", new ZPI());
         m_Responses.Add("ZTA", new ZTA());
         m_Responses.Add("ZTE", new ZTE());
         m_Responses.Add("ZTG", new ZTG());
         m_Responses.Add("ZTI", new ZTI());
         m_Responses.Add("ZWP", new ZWP());
         m_Responses.Add("ZZU", new ZZU());
      }

      public static double KnotsToMilesPerHour(double knots)
      {
         return (knots * 1.15077945D);
      }

      public static double KnotsToKilometersPerHour(double knots)
      {
         return (knots * 1.852D);
      }

      public double Decimal(double degrees, EastOrWest e)
      {
         double return_value = 0.0D;

         return (return_value);
      }

      public void Empty()
      {
         Sentence.Empty();
      }

      public Response GetResponse(string mnemonic)
      {
         Response response;

         if (m_Responses.TryGetValue(mnemonic, out response) == false)
         {
            // We don't know how to parse this kind of sentence
            return (null);
         }

         return (response);
      }

      public bool Parse(string line)
      {
         Parsed = "";

         if (System.String.IsNullOrEmpty(line) == true)
         {
            return (false);
         }

         Sentence.Data = line;

         if (Sentence.IsGood() == false)
         {
            return (false);
         }

         string mnemonic = Sentence.Field(0);

         if (mnemonic.Length < 3)
         {
            return (false);
         }

         if (mnemonic[0] == 'P')
         {
            // Proprietary sentence
            mnemonic = "P";
         }
         else
         {
            if (mnemonic.Length < 4)
            {
               return (false);
            }

            mnemonic = mnemonic.Substring(2, 3);
         }

         Response response;

         if (m_Responses.TryGetValue(mnemonic, out response) == false)
         {
            // We don't know how to parse this kind of sentence
            return (false);
         }

         if (response.Parse(Sentence) == false)
         {
            return (false);
         }

         Parsed = mnemonic;

         return (true);
      }
   }
}
