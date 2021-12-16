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

        public static string agm = "mp_agama";
        public static string agm_title = "Agama";
        public static string pkn = "mp_agama";
        public static string pkn_title = "Agama";
        public static string bi = "mp_agama";
        public static string bi_title = "Agama";
        public static string mtk = "mp_mtk";
        public static string mtk_title = "Matematika";
        public static string ipa = "mp_ipa";
        public static string ipa_title = "IPA";
        public static string ips = "mp_ips";
        public static string ips_title = "IPS";
        public static string sbdp = "mp_sbdp";
        public static string sbdp_title = "SBdp";
        public static string pjok = "mp_pjok";
        public static string pjok_title = "PJOK";
        public static string bjr = "mp_bjr";
        public static string bjr_title = "B. Banjar";
        public static string bing = "mp_bing";
        public static string bing_title = "B. Inggris";
        public static string bta = "mp_bta";
        public static string bta_title = "BTA";
        public static string dasis_query = "CREATE TABLE data_siswa (induk INTEGER(8)  PRIMARY KEY,nama VARCHAR(30), " +
                        "tempat_lahir VARCHAR(15), tanggal_lahir VARCHAR(20), jenis_kelamin VARCHAR(9), agama VARCHAR(10)," +
                        "alamat TEXT, nama_ayah VARCHAR(30), nama_ibu VARCHAR(30), p_ayah VARCHAR(15), p_ibu VARCHAR(15)," +
                        "pendidikan_sebelumnya VARCHAR(10), kelurahan VARCHAR(15), kecamatan VARCHAR(15));";
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string folder = "Raport Bangkal 1/";
        public static string folderpath = Path.Combine(path,folder);
        public static string dbName = "pog";
        public static string dbVersion = "Version=3;";

        public static bool isSaved = true;

    }
}
