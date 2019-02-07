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

#region Using
using System;
using System.Text;
using NMEA;
using System.Windows.Media.Imaging;
using System.IO;
#endregion

namespace Test
{
   class Test
   {
      static void WriteIcon(System.IO.TextWriter kml, double heading)
      {
         kml.WriteLine("<IconStyle>" );
         kml.WriteLine(" <color>ff0000ff</color>" );
         kml.WriteLine(" <scale>0.7</scale>" );
         kml.Write(" <heading>");
         kml.Write(heading.ToString());
         kml.WriteLine("</heading>");
         kml.WriteLine(" <Icon><href>http://maps.google.com/mapfiles/kml/shapes/arrow.png</href></Icon>" );
         kml.WriteLine("</IconStyle>");
      }

      static string RotateOnePlace(string string_parameter)
      {
         if (string.IsNullOrEmpty(string_parameter) == true)
         {
            return ("");
         }

         if (string_parameter.Length < 2)
         {
            return (string_parameter);
         }

         return (string_parameter.Substring(1) + string_parameter.Substring(0, 1));
      }

      static string[] Rotate(string string_parameter)
      {
         int number_of_elements = 0;

         if ( string.IsNullOrEmpty( string_parameter ) == false )
         {
            number_of_elements = string_parameter.Length - 1;
         }

         if ( number_of_elements < 0 )
         {
            number_of_elements = 0;
         }

         if ( number_of_elements < 1 )
         {
            return( null );
         }

         string[] return_value = new string[number_of_elements];

         return_value[0] = RotateOnePlace(string_parameter);

         int return_value_index = 1;

         while (return_value_index < number_of_elements)
         {
            return_value[return_value_index] = RotateOnePlace(return_value[return_value_index - 1]);
            return_value_index++;
         }

         return (return_value);
      }

      static void Main(string[] args)
      {
         int index = 0;

         TimeSpan minimum_spacing = new TimeSpan(0, 0, 30); // 30 seconds

         while (index < args.Length)
         {
            using (System.IO.StreamReader reader = System.IO.File.OpenText(args[index]))
            {
               using (System.IO.TextWriter kml = new System.IO.StreamWriter(args[index] + ".kml"))
               {
                  kml.WriteLine( "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" );
                  kml.WriteLine( "<kml xmlns=\"http://earth.google.com/kml/2.2\">" );
                  kml.WriteLine( " <Document>" );
                  kml.WriteLine( "  <Style id=\"s2\">\r\n   <IconStyle>\r\n    <color>ff0000ff</color>\r\n    <scale>1.0</scale>\r\n    <Icon>\r\n     <href>root://icons/palette-4.png</href>\r\n     <x>64</x>\r\n     <y>128</y>\r\n     <w>32</w>\r\n     <h>32</h>\r\n    </Icon>\r\n   </IconStyle>\r\n  </Style>");
                  kml.WriteLine("  <Style id=\"s1\">\r\n   <IconStyle>\r\n    <color>ff0000ff</color>\r\n    <scale>1.0</scale>\r\n    <Icon>\r\n     <href>http://maps.google.com/mapfiles/kml/shapes/arrow.png</href>\r\n    </Icon>\r\n  <scale>0.7</scale> </IconStyle>\r\n  </Style>");
                  kml.WriteLine("  <Style id=\"wall\">");
                  kml.WriteLine("   <LineStyle><color>7f00ffff</color><width>4</width></LineStyle>");
                  kml.WriteLine("   <PolyStyle><color>7f00ff00</color></PolyStyle>");
                  kml.WriteLine("  </Style>");

                  StringBuilder wall = new StringBuilder(4096);

                  wall.Append("   <Placemark>\r\n");
                  wall.Append("    <styleUrl>#wall</styleUrl>\r\n");
                  wall.Append("    <LineString>\r\n");
                  wall.Append("     <extrude>1</extrude>\r\n");
                  wall.Append("     <tessellate>1</tessellate>\r\n");
                  wall.Append("     <altitudeMode>absolute</altitudeMode>\r\n");
                  wall.Append("     <coordinates>\r\n");

                  NMEA.Parser p = new NMEA.Parser();

                  RMC rmc = (RMC)p.GetResponse("RMC");
                  GGA gga = (GGA)p.GetResponse("GGA");
                  VTG vtg = (VTG)p.GetResponse("VTG");

                  TimeZoneInfo est_timezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

                  string input_string = null;

                  int year = 1980;
                  int month = 1;
                  int day = 6;

                  double last_speed = 0.0D;

                  DateTime output_time;
                  DateTime last_output_time = new DateTime(1980, 1, 6);
                  TimeSpan interval;

                  while ((input_string = reader.ReadLine()) != null)
                  {
                     if (p.Parse(input_string) == true)
                     {
                        if (p.Parsed == "GGA")
                        {
                           output_time = new DateTime(year, month, day, gga.UTCTime.Hour, gga.UTCTime.Minute, gga.UTCTime.Second, gga.UTCTime.Millisecond);

                           interval = output_time - last_output_time;

                           if (interval >= minimum_spacing)
                           {
                              last_output_time = output_time;
                              kml.Write("  <Placemark>\r\n   <description>");
                              int feet = (int) ( gga.AntennaAltitudeMeters * 3.2808399D );
                              int mph = (int)(last_speed * 1.15077945D);

                              // Add the day of the week to the time string, localze the time format.
                              DateTime est_time = TimeZoneInfo.ConvertTimeFromUtc(output_time, est_timezone);

                              kml.Write(est_time.ToString() + ", " + mph.ToString("N0") + "MPH, " + feet.ToString("N0") + " feet");
                              kml.WriteLine("</description>\r\n   <styleUrl>#s1</styleUrl>");

                              kml.WriteLine("   <LookAt>");
                              kml.Write( "    <latitude>");
                              kml.Write(gga.Position.Latitude.Decimal().ToString());
                              kml.Write("</latitude>\r\n    <longitude>");
                              kml.Write(gga.Position.Longitude.Decimal().ToString());
                              kml.WriteLine("</longitude>\r\n    <range>500</range>\r\n   </LookAt>");
                              kml.WriteLine("   <Point>\r\n    <extrude>1</extrude>\r\n    <altitudeMode>relativeToGround</altitudeMode>");
                              kml.Write("    <coordinates>");

                              string output = gga.Position.Longitude.Decimal().ToString();

                              output += "," + gga.Position.Latitude.Decimal().ToString();
                              output += "," + gga.AntennaAltitudeMeters.ToString();

                              kml.Write(output);
                              wall.Append(output);
                              wall.Append(" \r\n" );
                              kml.WriteLine("</coordinates>\r\n   </Point>");
                              kml.Write("   <TimeStamp><when>");
                              kml.Write(est_time.ToString("s"));
                              kml.WriteLine("</when></TimeStamp>");
                              kml.WriteLine("  </Placemark>");
                           }
                        }
                        else if (p.Parsed == "RMC")
                        {
                           year = rmc.UTCTime.Year;
                           month = rmc.UTCTime.Month;
                           day = rmc.UTCTime.Day;
                        }
                        else if (p.Parsed == "VTG")
                        {
                           last_speed = vtg.SpeedKnots;
                        }
                     }
                  }

                  kml.WriteLine(wall.ToString());
                  wall.Length = 0;
                  kml.WriteLine("   </coordinates>");
                  kml.WriteLine("  </LineString>");
                  kml.WriteLine(" </Placemark>");
                  kml.WriteLine( " </Document>\r\n</kml>" );
               }

               reader.Close();
            }

            index++;
         }
      }
   }
}
