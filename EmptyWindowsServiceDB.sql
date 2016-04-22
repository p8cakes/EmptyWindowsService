-- ****************************** Module Header ******************************
-- Module Name:  Empty Windows Service DB for demonstration purposes
-- Project:      Empty Windows Service
--
-- Empty Windows Service SQL file to create the database, load tables for this demo application - please employ MySQL 5.5 or greater
--
-- Revisions:
--      1. Sundar Krishnamurthy         sundar@passion8cakes.com               4/22/2016       Initial file created.
-- ***************************************************************************/

-- Very, very, very bad things happen if you uncomment this line below. Do at your peril, you have been warned!
-- drop database if exists emptyWindowsServiceDB;

-- Create database emptyWindowsServiceDB, with utf8 and utf8_general_ci
create database if not exists emptyWindowsServiceDB character set utf8 collate utf8_general_ci;

-- Employ empty Windows Service DB;
use emptyWindowsServiceDB;

-- drop table if exists states;

-- T1. states table stores a few random states
create table if not exists states (
    stateId                                   int ( 10 ) unsigned not null auto_increment,
    name                                      varchar( 64 ) not null,
    abbreviation                              varchar( 2 ) not null,
    active                                    tinyint ( 1 ) unsigned not null default 0,
    created                                   datetime not null,
    lastUpdate                                datetime not null,
    key ( stateId ),
    unique index ix_name ( name )
) engine=innodb default character set=utf8;

-- drop table if exists cities;

-- T2. cities table stores a few random cities
create table if not exists cities (
    cityId                                    int ( 10 ) unsigned not null auto_increment,
    stateId                                   int ( 10 ) unsigned not null,
    name                                      varchar( 64 ) not null,
    active                                    tinyint ( 1 ) unsigned not null default 0,
    created                                   datetime not null,
    lastUpdate                                datetime not null,
    key ( cityId ),
    unique index ix_name ( name )
) engine=innodb default character set=utf8;

-- F1. Add constraint from activityLogs to testTakers for userId
drop procedure if exists addConstraint;

delimiter //

-- P1. Add the constraint if it does not exist
create procedure addConstraint ()
begin
    -- 2a. Add constraint for activityLogs to testTakers
    if not exists (select * from information_schema.TABLE_CONSTRAINTS where
                   CONSTRAINT_SCHEMA = DATABASE() and
                   CONSTRAINT_NAME   = 'fk_cities_states_stateId' and
                   CONSTRAINT_TYPE   = 'FOREIGN KEY') then

        alter table
            cities
        add constraint
            fk_cities_states_stateId
        foreign key (stateId)
        references states (stateId)
        on update cascade
        on delete cascade;
    end if;
end //

delimiter ;

-- Set the constraint
call addConstraint();

-- You don't need this procedure anymore
drop procedure addConstraint;

-- Drop procedure we are about to create, if it exists prior
drop procedure if exists addSomeStatesAndCities;

-- First user has to be injected manually
delimiter //

-- P2. Add the constraint if it does not exist
create procedure addSomeStatesAndCities ()
begin

    declare l_stateId                          int ( 10 ) unsigned;
    declare l_stateCount                       int ( 10 ) unsigned;

    select count(*) into l_stateCount from states;

    if l_stateCount = 0 then
        insert states (name, abbreviation, active, created, lastUpdate) values ('Washington', 'WA', 1, utc_timestamp(), utc_timestamp());
        insert states (name, abbreviation, active, created, lastUpdate) values ('California', 'CA', 1, utc_timestamp(), utc_timestamp());
        insert states (name, abbreviation, active, created, lastUpdate) values ('Oregon', 'OR', 1, utc_timestamp(), utc_timestamp());

        -- Insert some random cities
        select stateId into l_stateId from states where abbreviation='WA';

        insert cities (name, stateId, active, created, lastUpdate) values ('Bellevue', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Redmond', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Kirkland', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Bothell', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Seatac', l_stateId, 1, utc_timestamp(), utc_timestamp());

        -- Insert some random cities
        select stateId into l_stateId from states where abbreviation='OR';

        insert cities (name, stateId, active, created, lastUpdate) values ('Portland', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Seaside', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Astoria', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Beaverton', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Manzanita', l_stateId, 1, utc_timestamp(), utc_timestamp());

        -- Insert some random cities
        select stateId into l_stateId from states where abbreviation='CA';

        insert cities (name, stateId, active, created, lastUpdate) values ('Eureka', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Sacramento', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Yuba City', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Foster City ', l_stateId, 1, utc_timestamp(), utc_timestamp());
        insert cities (name, stateId, active, created, lastUpdate) values ('Carlsbad', l_stateId, 1, utc_timestamp(), utc_timestamp());

    end if;

end //

delimiter ;

-- Invoke SP to insert first cities and states
call addSomeStatesAndCities();

-- You don't need this anymore!
drop procedure addSomeStatesAndCities;

drop procedure if exists getCityCount;

delimiter //

-- P3. Get me a city and an abbreviated state!
create procedure getCityCount ( )
begin

    select
        count(*) as completeRecordCount
    from
        cities city
    inner join
        states state
    on
        city.stateId = state.stateId
    where
        state.active = 1
      and
        city.active = 1;

end //

delimiter ;

drop procedure if exists getCity;

delimiter //

-- P3. Get me a city and an abbreviated state!
create procedure getCity (
    in p_cityId                               varchar( 32 )
)
begin

    select
        city.name,
        state.abbreviation
    from
        cities city
    inner join
        states state
    on
        city.stateId = state.stateId
    where
        city.cityId = p_cityId
      and
        state.active = 1
      and
        city.active = 1;

end //

delimiter ;

drop procedure if exists walkThroughCitiesViaACursor;

delimiter //

-- P5. Walk through all the cities serially via a cursor
create procedure walkThroughCitiesViaACursor (
)
begin

    declare done int default false;

    declare l_city                                     varchar ( 64 ) default null;
    declare l_abbreviation                             varchar ( 2 ) default null;

    declare targetcursor cursor for
        select city.name, state.abbreviation from cities city inner join states state
        on city.stateId = state.stateId where city.active = 1 and state.active = 1
        order by city.cityId;

    declare continue handler for not found set done = true;

    open targetcursor;

    read_loop: loop

        fetch targetcursor into l_city, l_abbreviation;

        if done then
            leave read_loop;

        end if;

        select concat('I had some great lemon margaritas in ', l_city, ' ', l_abbreviation) as message;

        set done = false;

    end loop;

end //

delimiter ;
