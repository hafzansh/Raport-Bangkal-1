using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Raport.Helper
{
    public class Constants
    {
        public static String[] header_title = {"Induk","Nama Siswa","Tempat Lahir","Tanggal Lahir","Jenis Kelamin","Agama","Alamat","Nama Ayah",
                                      "Nama Ibu","Pekerjaan Ayah","Pekerjaan Ibu","Pendidikan Sebelumnya","Kelurahan","Kecamatan"};
        public static float[] header_width = { 1, 3, 2, 2, 2, 2, 3, 3, 3, 2, 2, 2, 2, 2 };
        public static int rowC = 0;
        public static string[] header_list = {
            "agm3","agm4",
            "pkn3","pkn4",
            "bi4","bi4",
            "mtk3","mtk4",
            "ipa3","ipa4",
            "ips3","ips4",
            "sbdp3","sbdp4",
            "pjok3","pjok4",
            "bjr3","bjr4",
            "bing3","bing4",
            "bta3","bta4"

                                       };
        public static string dasis = "data_siswa";
        public static string dasis_title = "Data Siswa";
        public static string absen = "data_absen";
        public static string absen_title = "Absen";
        public static string sikap = "data_sikap";
        public static string sikap_title = "Sikap";
        public static string agm = "mp_agama";
        public static string agm_title = "Agama";
        public static string agm_title2 = "Agama2";
        public static string pkn = "mp_pkn";
        public static string pkn_title = "PKN";
        public static string pkn_title2 = "PKN2";
        public static string bi = "mp_bi";
        public static string bi_title = "Bahasa Indonesia";
        public static string bi_title2 = "Bahasa Indonesia2";
        public static string mtk = "mp_mtk";
        public static string mtk_title = "Matematika";
        public static string mtk_title2 = "Matematika2";
        public static string ipa = "mp_ipa";
        public static string ipa_title = "IPA";
        public static string ipa_title2 = "IPA2";
        public static string ips = "mp_ips";
        public static string ips_title = "IPS";
        public static string ips_title2 = "IPS2";
        public static string sbdp = "mp_sbdp";
        public static string sbdp_title = "SBdp";
        public static string sbdp_title2 = "SBdp2";
        public static string pjok = "mp_pjok";
        public static string pjok_title = "PJOK";
        public static string pjok_title2 = "PJOK2";
        public static string bjr = "mp_bjr";
        public static string bjr_title = "B. Banjar";
        public static string bjr_title2 = "B. Banjar2";
        public static string bing = "mp_bing";
        public static string bing_title = "B. Inggris";
        public static string bing_title2 = "B. Inggris2";
        public static string bta = "mp_bta";
        public static string bta_title = "BTA";
        public static string bta_title2 = "BTA2";
        public static string dasis_query = "CREATE TABLE data_siswa (induk INTEGER(8)  PRIMARY KEY,nama VARCHAR(30), " +
                        "tempat_lahir VARCHAR(15), tanggal_lahir VARCHAR(20), jenis_kelamin VARCHAR(9), agama VARCHAR(10)," +
                        "alamat TEXT, nama_ayah VARCHAR(30), nama_ibu VARCHAR(30), p_ayah VARCHAR(15), p_ibu VARCHAR(15)," +
                        "pendidikan_sebelumnya VARCHAR(10), kelurahan VARCHAR(15), kecamatan VARCHAR(15));";
        public static string absen_query = "CREATE TABLE data_absen (id INTEGER primary key autoincrement,induk INTEGER(8)  REFERENCES data_siswa(induk) ON DELETE NO ACTION " +
            "ON UPDATE NO ACTION, pramuka     VARCHAR(5),karate VARCHAR(5),pmr VARCHAR(5),tari VARCHAR(5),tinggi VARCHAR(6)," +
            "berat_badan VARCHAR(5),pendengaran VARCHAR(10),penglihatan VARCHAR(10),gigi VARCHAR(10),lainnya VARCHAR(10),s INTEGER(2)," +
            "i INTEGER(2),a INTEGER(2),FOREIGN KEY(induk) REFERENCES data_siswa(induk) ON DELETE CASCADE ON UPDATE CASCADE);";
        public static string sikap_query = "CREATE TABLE data_sikap (id INTEGER     PRIMARY KEY AUTOINCREMENT,induk INTEGER(8) REFERENCES data_siswa(induk) ON DELETE CASCADE ON UPDATE CASCADE," +
            "sikap1 INTEGER(1),sikap2 INTEGER(1),sikap3 INTEGER(1),sikap4 INTEGER(1));";
        public static string app_query = "CREATE TABLE app_settings (id INTEGER      PRIMARY KEY AUTOINCREMENT,wali_kelas VARCHAR(30)," +
            "nip_wali_kelas VARCHAR(16),kepala_sekolah VARCHAR(30),nip_kepala_sekolah VARCHAR(16),semester VARCHAR(10),tahun VARCHAR(10)," +
            "kelas VARCHAR(10),kd_agm3 INT(2),kd_agm4 INT(2),kd_pkn3 INT(2),kd_pkn4 INT(2),kd_bi3 INT(2),kd_bi4 INT(2),kd_mtk3 INT(2)," +
            "kd_mtk4 INT(2),kd_ipa3 INT(2),kd_ipa4 INT(2),kd_ips3 INT(2),kd_ips4 INT(2),kd_sbdp3 INT(2),kd_sbdp4 INT(2),kd_pjok3 INT(2)," +
            "kd_pjok4 INT(2),kd_bjr3 INT(2),kd_bjr4 INT(2),kd_bing3 INT(2),kd_bing4 INT(2),kd_bta3 INT(2),kd_bta4 INT(2)" +
            ",kkm_agm INT(2),kkm_pkn INT(2),kkm_bi INT(2),kkm_mtk INT(2),kkm_ipa INT(2),kkm_ips INT(2),kkm_sbdp INT(2),kkm_pjok INT(2),kkm_bjr INT(2)" +
            ",kkm_bing INT(2),kkm_bta INT(2));";
        public static string kd_query = "CREATE TABLE kompetensi_dasar (id INTEGER      PRIMARY KEY AUTOINCREMENT,kd VARCHAR(20),agm3 text default ' '," +
            "agm4  text default ' ',pkn3 text default ' ',pkn4  text default ' ',mtk3 text default ' ',mtk4  text default ' ',bi3 text default ' ',bi4   text default ' ',ips3 text default ' ',ips4  text default ' ',ipa3 text default ' ',ipa4  text default ' '," +
            "pjok3 text default ' ',pjok4 text default ' ',sbdp3 text default ' ',sbdp4 text default ' ',bjr3 text default ' ',bjr4  text default ' ',bing3 text default ' ',bing4 text default ' ',bta3 text default ' ',bta4  TEXT default ' ');";
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string folder = "Raport Bangkal 1/";
        public static string folderpath = Path.Combine(path,folder);
        public static string dbName;
        public static string dbVersion = "Version=3;";
        public static bool isLanding = true;
        public static bool isSaved = true;

        public static int current1;
        public static int current2;
        public static int current3;
        public static void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
