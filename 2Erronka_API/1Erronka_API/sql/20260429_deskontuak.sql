CREATE TABLE IF NOT EXISTS deskontuak (
    id INT NOT NULL AUTO_INCREMENT,
    kodea VARCHAR(60) NOT NULL,
    mota VARCHAR(30) NOT NULL,
    balioa DOUBLE NOT NULL,
    aktibo TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (id),
    UNIQUE KEY deskontuak_kodea_unique (kodea)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

ALTER TABLE eskariak
    ADD COLUMN deskontuak_id INT NULL AFTER deskontu_kopurua,
    ADD CONSTRAINT FK_ESKARIAK_DESKONTUAK FOREIGN KEY (deskontuak_id) REFERENCES deskontuak(id);
