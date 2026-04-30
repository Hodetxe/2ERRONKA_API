ALTER TABLE eskariak
    ADD COLUMN guztira_bruto DOUBLE NOT NULL DEFAULT 0 AFTER prezioa,
    ADD COLUMN deskontu_kopurua DOUBLE NOT NULL DEFAULT 0 AFTER guztira_bruto,
    ADD COLUMN deskontu_kodea VARCHAR(60) NULL AFTER deskontu_kopurua,
    ADD COLUMN deskontu_mota VARCHAR(30) NULL AFTER deskontu_kodea,
    ADD COLUMN deskontu_balioa DOUBLE NULL AFTER deskontu_mota;

UPDATE eskariak
SET guztira_bruto = prezioa,
    deskontu_kopurua = 0
WHERE guztira_bruto = 0;
