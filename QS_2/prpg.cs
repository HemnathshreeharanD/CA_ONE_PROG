using System;
using System.Collections.Generic;

namespace FileExtensionInfoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dictionary Data Structure – extension → meaning
            Dictionary<string, string> fileInfo = new Dictionary<string, string>()
            {
                { ".mp4",  "MPEG-4 Video File" },
                { ".mov",  "Apple QuickTime Movie" },
                { ".avi",  "Audio Video Interleave File" },
                { ".mkv",  "Matroska Video File" },
                { ".webm", "WebM Video File" },
                { ".mp3",  "MPEG Layer-3 Audio" },
                { ".wav",  "Waveform Audio" },
                { ".flac", "Free Lossless Audio Codec" },
                { ".png",  "Portable Network Graphics Image" },
                { ".jpg",  "JPEG Image" },
                { ".jpeg", "JPEG Image" },
                { ".gif",  "Graphics Interchange Format" },
                { ".pdf",  "Portable Document Format" },
                { ".docx", "Microsoft Word Document" },
                { ".xlsx", "Microsoft Excel Spreadsheet" },
                { ".pptx", "Microsoft PowerPoint Presentation" },
                { ".zip",  "ZIP Archive" },
                { ".rar",  "WinRAR Compressed File" },
                { ".txt",  "Plain Text File" },
                { ".html", "HyperText Markup Language File" }
            };

            Console.WriteLine("=== File Extension Information System ===");

            while (true)
            {
                Console.Write("\nEnter a file extension (or type 'exit' to quit): ");
                string input = Console.ReadLine().Trim().ToLower();

                // Exit program
                if (input == "exit")
                {
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;
                }

                // Ensure extension starts with dot
                if (!input.StartsWith("."))
                {
                    input = "." + input;
                }

                // Check if known
                if (fileInfo.ContainsKey(input))
                {
                    Console.WriteLine($"Extension: {input}");
                    Console.WriteLine($"Description: {fileInfo[input]}");
                }
                else
                {
                    Console.WriteLine($"Unknown extension: {input}");
                    Console.WriteLine("Please try again with a valid file type.");
                }
            }
        }
    }
}
