create database sdServices;
use sdServices;
create table surv(
Summary varchar(200),
year2008 int,
year2009 int,
year2010 int,
year2011 int,
year2012 int
);

/*software publishers */

INSERT INTO surv values('Number of taxfilers(2)', 24986960,25244320,25484240,25869670,26155710);
INSERT INTO surv values('Average age of taxfilers(years)(2,12)', 47,47,47,47,48);
INSERT INTO surv values('Number of Persons', 3212420,32425050,32700430,33087940,33399630);
INSERT INTO surv values('Average age of persons (years) (12)', 39,39,39,39,39);
INSERT INTO surv values ('Number of persons with total income (5)', 24731470,24964290,25227050,25599300,25797510);
INSERT INTO surv values('Median employment income, both sexes (dollars) (6,8)', 29390,29020,29730,30640,31660);

SELECT * FROM surv