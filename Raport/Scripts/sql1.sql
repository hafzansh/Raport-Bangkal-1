SELECT distinct ds.induk, ds.nama,
    agm.nilai1 agm_3 ,agm.nilai2 agm_4,
    pkn.nilai1 pkn_3 ,pkn.nilai2 pkn_4,
    bi.nilai1 bi_3 ,bi.nilai2 bi_4,
    mtk.nilai1 mtk_3 ,mtk.nilai2 mtk_4,
    ipa.nilai1 ipa_3 ,ipa.nilai2 ipa_4,
    ips.nilai1 ips_3 ,ips.nilai2 ips_4,
    sbdp.nilai1 sbdp_3 ,sbdp.nilai2 sbdp_4,
    pjok.nilai1 pjok_3 ,pjok.nilai2 pjok_4,
    bjr.nilai1 bjr_3 ,bjr.nilai2 bjr_4,
    bing.nilai1 bing_3 ,bing.nilai2 bing_4
    from data_siswa ds 
    inner join view_mp_agama agm on ds.induk=agm.induk
    inner join view_mp_pkn pkn on ds.induk=pkn.induk
    inner join view_mp_bi bi on ds.induk=bi.induk
    inner join view_mp_mtk mtk on ds.induk=mtk.induk
    inner join view_mp_ipa ipa on ds.induk=ipa.induk
    inner join view_mp_ips ips on ds.induk=ips.induk
    inner join view_mp_sbdp sbdp on ds.induk=sbdp.induk
    inner join view_mp_pjok pjok on ds.induk=pjok.induk    
    inner join view_mp_bta bta on ds.induk=bta.induk
    inner join view_mp_bjr bjr on ds.induk=bjr.induk
    inner join view_mp_bing bing on ds.induk=bing.induk