using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PICOPY
{
    public class CopyLogic
    {
        private static string filePath; // variable to hold path of file
        private static string m_CurrentFile;
        protected static List<string> files; // list that will hold file names to copy


        public CopyLogic(string desPath)
        {
            filePath = desPath;

        }


        public static string p_filepath
        {

            get { return filePath; }
        }

        public static string mp_CurrentFile
        {
            get
            {
                return m_CurrentFile;
            }
            set
            {
                m_CurrentFile = value;
            }
        }

        //Copy the file to destination folder using finalpath where file need to be copied
        public static void Copy()
        {


            string _finalPath;
            _finalPath = filePath;

            var filename = m_CurrentFile.Substring(m_CurrentFile.LastIndexOf("\\") + 1); //extracting filename from the full path of source

            if (System.IO.Directory.Exists(_finalPath))
            {
                _finalPath = System.IO.Path.Combine(_finalPath, filename); // combining source folder path with file name to be copied

                System.IO.File.Copy(m_CurrentFile, _finalPath, true);
            }


        }


        //parameters as filename will be passed
        public static void Current(string file)
        {
            //hold the current displayed file
            m_CurrentFile = file;

        }

    }
}
