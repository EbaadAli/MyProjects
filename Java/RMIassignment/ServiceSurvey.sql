create database ServiceSurvey;
use ServiceSurvey;
create table survey(
naics varchar(100),
summary varchar(200),
ProfitIn2008 decimal(6,2),
ProfitIn2009 decimal(6,2),
ProfitIn2010 decimal(6,2),
ProfitIn2011 decimal(6,2),
ProfitIn2012 decimal(6,2)
);

/*software publishers */

INSERT INTO survey values('Software publishers [51121]', 'Operating revenue (dollars x 1,000,000) (5)', 6226.50,5938.3,6250.40,6874.0,7549.0);

select * from survey;
INSERT INTO survey values('Software publishers [51121]', 'Operating expenses (dollars x 1,000,000) (6)', 5820.40,5398.5,5660.30,6417.80,6887.7);
INSERT INTO survey values('Software publishers [51121]', 'Salaries, wages and benefits (dollars x 1,000,000) (2)', 2832.60,2428.10,2723.6,2950.10,3192.10);
INSERT INTO survey values('Software publishers [51121]', 'Operating profit margin (percent) (3)', 6.5,9.1,9.4,6.6,8.8);


/* Data processing */
INSERT INTO survey values('Data processing, hosting, and related services [51821]  (7)', 'Operating revenue (dollars x 1,000,000) (5)', 2692.7,2775.9,3104.5,3544.6,3670.9);
INSERT INTO survey values('Data processing, hosting, and related services [51821]  (7)', 'Operating expenses (dollars x 1,000,000) (6)', 2391.7,2302,2791.7,2997.6,3142.9);
INSERT INTO survey values('Data processing, hosting, and related services [51821]  (7)', 'Salaries, wages and benefits (dollars x 1,000,000) (2)', 930.5,968.1,1167.7,1277.5,1343.6);
INSERT INTO survey values('Data processing, hosting, and related services [51821]  (7)', 'Operating profit margin (percent) (3)', 11.2,17.1,10.1,15.4,14.4);
select * from survey;
delete from survey where naics IS NULL;

/* computer systems design ... */

INSERT INTO survey values('Computer systems design and related services [54151]', 'Operating revenue (dollars x 1,000,000) (5)',30440.1,32436.3,32285.1,34524.9,36038.7);
INSERT INTO survey values('Computer systems design and related services [54151]', 'Operating expenses (dollars x 1,000,000) (6)',27893.6,28794.5,28467,30488.9,31541.5);
INSERT INTO survey values('Computer systems design and related services [54151]', 'Salaries, wages and benefits (dollars x 1,000,000) (2)',12692.2,12761.6,13010,14154.8,14709.9);
INSERT INTO survey values('Computer systems design and related services [54151]', 'Operating profit margin (percent) (3)',8.4,11.2,11.8,11.7,12.5);

alter table survey
ALTER COLUMN ProfitIn2008 decimal(10,2)
alter table survey
ALTER COLUMN ProfitIn2009 decimal(10,2)
alter table survey
ALTER COLUMN ProfitIn2010 decimal(10,2)
alter table survey
ALTER COLUMN ProfitIn2011 decimal(10,2)
alter table survey
ALTER COLUMN ProfitIn2012 decimal(10,2)


EXEC sp_RENAME 'survey.ProfitIn2008', 'year2008','COLUMN'
EXEC sp_RENAME 'survey.ProfitIn2009', 'year2009','COLUMN'
EXEC sp_RENAME 'survey.ProfitIn2010', 'year2010','COLUMN'
EXEC sp_RENAME 'survey.ProfitIn2011', 'year2011','COLUMN'
EXEC sp_RENAME 'survey.ProfitIn2012', 'year2012','COLUMN'

/*change first column last row value to 7548.9 */

SELECT * FROM survey