SELECT distinct ds.induk,
    bta.nilai1 bta_3 ,bta.nilai2 bta_4
    from data_siswa ds 
    inner join view_mp_bta bta on ds.induk=bta.induk