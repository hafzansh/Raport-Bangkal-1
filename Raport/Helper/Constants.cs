using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Raport.Helper
{
    public class Constants
    {
        public static String[] header_title = {"Induk","Nama Siswa","Tempat Lahir","Tanggal Lahir","Jenis Kelamin","Agama","Alamat","Nama Ayah",
                                      "Nama Ibu","Pekerjaan Ayah","Pekerjaan Ibu","Pendidikan Sebelumnya","Kelurahan","Kecamatan"};
        public static float[] header_width = { 1, 3, 2, 2, 2, 2, 3, 3, 3, 2, 2, 2, 2, 2 };

        public static string dasis = "data_siswa";
        public static string dasis_title = "Data Siswa";

        public static string mtk = "mp_mtk";
        public static string mtk_title = "Matematika";

        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string folder = "Raport Bangkal 1/";
        public static string folderpath = Path.Combine(path,folder);
        public static string dbName = "pog.db";
        public static string dbVersion = "Version=3;";

        public static bool isSaved = true;

    }
}
