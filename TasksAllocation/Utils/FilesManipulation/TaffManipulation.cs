using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;
using System.IO;

namespace TasksAllocation.Utils.FilesManipulation
{
    class TaffManipulation
    {
        public static string ExtractCff(string taffFilename, ref ErrorManager errorManager)
        {
            string cffFilename = null;
            string line = "";
            StreamReader streamReader = new StreamReader(taffFilename);
            bool[] validOpenClosingSection = { false, false };

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                line = line.Trim();

                if (line.StartsWith(TaffKeywords.OPENING_CONFIG_DATA))
                {
                    validOpenClosingSection[0] = true;
                }

                if (line.StartsWith(TaffKeywords.CLOSING_CONFIG_DATA))
                {
                    validOpenClosingSection[1] = true;
                    break;
                }

                if (validOpenClosingSection[0] && line.StartsWith(TaffKeywords.CONFIG_FILENAME))
                {
                    string[] lineData = line.Split(Symbols.EQUALITY);
                    const int N_LINE_DATA = 2;

                    if (lineData.Length == N_LINE_DATA)
                    {
                        cffFilename = lineData[1].Trim(Symbols.DOUBLE_QUOTE);

                        if (cffFilename.Length > 0)
                        {
                            string[] filenameData = cffFilename.Split(Symbols.DOT);
                            const int N_FILENAME_DATA = 2;

                            if (filenameData.Length == N_FILENAME_DATA)
                            {
                                if (filenameData[1] == CffKeywords.FILE_EXTENSION)
                                {
                                    if (cffFilename.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
                                    {
                                        string filePath = Path.GetDirectoryName(taffFilename);
                                        cffFilename = $"{filePath}{Path.DirectorySeparatorChar}{cffFilename}";

                                        if (!File.Exists(cffFilename))
                                        {
                                            string[] error = new string[3];
                                            error[0] = "CFF file does not exist";
                                            error[1] = cffFilename;
                                            error[2] = "Please provide valid file path";
                                            errorManager.Errors.Add(error);
                                        }
                                    }
                                    else
                                    {
                                        string[] error = new string[3];
                                        error[0] = "CFF file's path contains invalid characters";
                                        error[1] = cffFilename;
                                        error[2] = "Path must not contain <, >, :, \", /, \\, |, ?, *";
                                        errorManager.Errors.Add(error);
                                    }
                                }
                                else
                                {
                                    string[] error = new string[3];
                                    error[0] = "Filename has invalid file extension";
                                    error[1] = filenameData[1];
                                    error[2] = CffKeywords.FILE_EXTENSION;
                                    errorManager.Errors.Add(error);
                                }
                            }
                            else
                            {
                                string[] error = new string[3];
                                error[0] = "File extension cannot be found";
                                error[1] = filenameData.Length.ToString();
                                error[2] = N_FILENAME_DATA.ToString();
                                errorManager.Errors.Add(error);
                            }
                        }
                        else
                        {
                            string[] error = new string[3];
                            error[0] = "No valid file can be found";
                            error[1] = cffFilename;
                            error[2] = "*.cff (pattern)";
                            errorManager.Errors.Add(error);
                        }
                    }
                    else
                    {
                        string[] error = new string[3];
                        error[0] = $"No value for {TaffKeywords.CONFIG_FILENAME} can be found";
                        error[1] = "0";
                        error[2] = $"{TaffKeywords.CONFIG_FILENAME}=\"*.cff\"";
                        errorManager.Errors.Add(error);
                    }
                }
            }

            streamReader.Close();

            if (!validOpenClosingSection[0] || !validOpenClosingSection[1])
            {
                string[] error = new string[3];
                error[0] = $"There is no configration data section";
                error[1] = "null";
                error[2] = $"The section starts with {TaffKeywords.OPENING_CONFIG_DATA} and end with {TaffKeywords.CLOSING_CONFIG_DATA}";
                errorManager.Errors.Add(error);

                return null;
            }

            if (cffFilename == null)
            {
                string[] error = new string[3];
                error[0] = $"There is no cff file";
                error[1] = "null";
                error[2] = "*.cff (pattern)";
                errorManager.Errors.Add(error);
            }
            return cffFilename;
        }
    }
}
