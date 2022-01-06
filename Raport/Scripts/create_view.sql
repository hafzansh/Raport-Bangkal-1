CREATE VIEW view_mp_mtk3 AS
    SELECT id,
           mp_mtk.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_mtk
           INNER JOIN
           data_siswa ON mp_mtk.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_mtk4 AS
    SELECT id,
           mp_mtk.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_mtk
           INNER JOIN
           data_siswa ON mp_mtk.induk = data_siswa.induk;

CREATE VIEW view_mp_mtk AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_mtk3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_mtk4 b ON c.induk = b.induk;
           
CREATE VIEW view_mp_ipa3 AS
    SELECT id,
           mp_ipa.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_ipa
           INNER JOIN
           data_siswa ON mp_ipa.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_ipa4 AS
    SELECT id,
           mp_ipa.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_ipa
           INNER JOIN
           data_siswa ON mp_ipa.induk = data_siswa.induk;

CREATE VIEW view_mp_ipa AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_ipa3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_ipa4 b ON c.induk = b.induk;
		   
CREATE VIEW view_mp_ips3 AS
    SELECT id,
           mp_ips.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_ips
           INNER JOIN
           data_siswa ON mp_ips.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_ips4 AS
    SELECT id,
           mp_ips.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_ips
           INNER JOIN
           data_siswa ON mp_ips.induk = data_siswa.induk;

CREATE VIEW view_mp_ips AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_ips3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_ips4 b ON c.induk = b.induk;
		   
CREATE VIEW view_mp_sbdp3 AS
    SELECT id,
           mp_sbdp.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_sbdp
           INNER JOIN
           data_siswa ON mp_sbdp.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_sbdp4 AS
    SELECT id,
           mp_sbdp.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_sbdp
           INNER JOIN
           data_siswa ON mp_sbdp.induk = data_siswa.induk;

CREATE VIEW view_mp_sbdp AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_sbdp3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_sbdp4 b ON c.induk = b.induk;
CREATE VIEW view_mp_pjok3 AS
    SELECT id,
           mp_pjok.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_pjok
           INNER JOIN
           data_siswa ON mp_pjok.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_pjok4 AS
    SELECT id,
           mp_pjok.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_pjok
           INNER JOIN
           data_siswa ON mp_pjok.induk = data_siswa.induk;

CREATE VIEW view_mp_pjok AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_pjok3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_pjok4 b ON c.induk = b.induk;

CREATE VIEW view_mp_bjr3 AS
    SELECT id,
           mp_bjr.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_bjr
           INNER JOIN
           data_siswa ON mp_bjr.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_bjr4 AS
    SELECT id,
           mp_bjr.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_bjr
           INNER JOIN
           data_siswa ON mp_bjr.induk = data_siswa.induk;

CREATE VIEW view_mp_bjr AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_bjr3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_bjr4 b ON c.induk = b.induk;		   
CREATE VIEW view_mp_bing3 AS
    SELECT id,
           mp_bing.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_bing
           INNER JOIN
           data_siswa ON mp_bing.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_bing4 AS
    SELECT id,
           mp_bing.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_bing
           INNER JOIN
           data_siswa ON mp_bing.induk = data_siswa.induk;

CREATE VIEW view_mp_bing AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_bing3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_bing4 b ON c.induk = b.induk;		   

CREATE VIEW view_mp_bta3 AS
    SELECT id,
           mp_bta.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_bta
           INNER JOIN
           data_siswa ON mp_bta.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_bta4 AS
    SELECT id,
           mp_bta.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_bta
           INNER JOIN
           data_siswa ON mp_bta.induk = data_siswa.induk;

CREATE VIEW view_mp_bta AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_bta3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_bta4 b ON c.induk = b.induk;		   
		   
CREATE VIEW view_mp_agama3 AS
    SELECT id,
           mp_agama.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_agama
           INNER JOIN
           data_siswa ON mp_agama.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_agama4 AS
    SELECT id,
           mp_agama.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_agama
           INNER JOIN
           data_siswa ON mp_agama.induk = data_siswa.induk;

CREATE VIEW view_mp_agama AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_agama3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_agama4 b ON c.induk = b.induk;		   
		   
CREATE VIEW view_mp_pkn3 AS
    SELECT id,
           mp_pkn.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_pkn
           INNER JOIN
           data_siswa ON mp_pkn.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_pkn4 AS
    SELECT id,
           mp_pkn.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_pkn
           INNER JOIN
           data_siswa ON mp_pkn.induk = data_siswa.induk;

CREATE VIEW view_mp_pkn AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_pkn3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_pkn4 b ON c.induk = b.induk;

CREATE VIEW view_mp_bi3 AS
    SELECT id,
           mp_bi.induk,
           data_siswa.nama,
           kdp1,
           kdp2,
           kdp3,
           kdp4,
           kdp5,
           tugas1,
           tugas2,
           tugas3,
           tugas4,
           tugas5,
           CASE WHEN (kdp1 = 0 AND 
                      kdp2 = 0 AND 
                      kdp3 = 0 AND 
                      kdp4 = 0 AND 
                      kdp5 = 0 AND 
                      tugas1 = 0 AND 
                      tugas2 = 0 AND 
                      tugas3 = 0 AND 
                      tugas4 = 0 AND 
                      tugas5 = 0) THEN 0 ELSE (kdp1 + kdp2 + kdp3 + kdp4 + kdp5 + tugas1 + tugas2 + tugas3 + tugas4 + tugas5) / (CASE WHEN KDP1 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP2 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP3 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP4 = 0 THEN 0 ELSE 1 END + CASE WHEN KDP5 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS1 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS2 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS3 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS4 = 0 THEN 0 ELSE 1 END + CASE WHEN TUGAS5 = 0 THEN 0 ELSE 1 END) END AS Rerata,
           uts,
           uas
      FROM mp_bi
           INNER JOIN
           data_siswa ON mp_bi.induk = data_siswa.induk;
		   
CREATE VIEW view_mp_bi4 AS
    SELECT id,
           mp_bi.induk,
           data_siswa.nama,
           kdk1,
           kdk2,
           kdk3,
           kdk4,
           kdk5,
           CASE WHEN (kdk1 = 0 AND 
                      kdk2 = 0 AND 
                      kdk3 = 0 AND 
                      kdk4 = 0 AND 
                      kdk5 = 0) THEN 0 ELSE (kdk1 + kdk2 + kdk3 + kdk4 + kdk5) / (CASE WHEN kdk1 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk2 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk3 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk4 = 0 THEN 0 ELSE 1 END + CASE WHEN kdk5 = 0 THEN 0 ELSE 1 END) END AS Rerata
      FROM mp_bi
           INNER JOIN
           data_siswa ON mp_bi.induk = data_siswa.induk;

CREATE VIEW view_mp_bi AS
    SELECT c.induk,
           ( (a.rerata * 0.6) + ( (a.uts + a.uas) * 0.4) / 2) AS nilai1,
           b.rerata AS nilai2
      FROM data_siswa c
           INNER JOIN
           view_mp_bi3 a ON c.induk = a.induk
           INNER JOIN
           view_mp_bi4 b ON c.induk = b.induk;		   