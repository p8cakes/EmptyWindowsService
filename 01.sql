--   13.    P8. Update phdUsers table with this data
create procedure updatePhDUser (
    in p_userId                               int ( 10 ) unsigned,
    in p_firstName                            varchar( 100 ),
    in p_lastName                             varchar( 100 ),
    in p_salt                                 varchar( 32 ),
    in p_email                                varchar( 128 ),
    in p_active                               tinyint ( 1 ) unsigned,
    in p_status                               int ( 10 ) unsigned,
    in p_accessKey                            varchar( 32 ),
    in p_createdBy                            int ( 10 ) unsigned,
    in p_mailSent                             tinyint ( 1 ) unsigned,
    in p_suppressFlag                         tinyint ( 1 ) unsigned
)
begin

    declare l_query                           varchar( 1024 );
    declare l_message                         varchar( 4096 );
    declare l_username                        varchar( 32 );
    declare l_userId                          int ( 10 ) unsigned;
    declare l_element                         varchar( 1024 );
    declare l_existingUserId                  int ( 10 ) unsigned;
    declare l_errorFlag                       tinyint ( 1 ) unsigned;
    declare l_errorMessage                    varchar( 96 );
    declare l_salt                            varchar( 32 );
    declare l_accessKey                       varchar( 32 );

    set l_salt = "NULL";
    set l_accessKey = "NULL";

    if p_salt is not null then
        set l_salt = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
    end if;

    if p_accessKey is not null then
        set l_accessKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
    end if;

    set l_message = '';
    set l_errorFlag = 0;
    set l_userId = p_userId;
    set l_existingUserId = null;

    if p_email is not null and p_email != '' then
        select userId into l_existingUserId from phdUsers where email = p_email;

        if l_existingUserId is null then
            set l_errorFlag = 0;
        elseif l_existingUserId != p_userId then
            set l_errorFlag = 1;
            set l_errorMessage = concat('Found another user with userId: ', l_existingUserId, ' that has the same email address');
        end if;
    end if;

    if l_errorFlag = 0 then
        select trim(concat(firstName, ' ', ifnull(lastName, ''))) into l_username from phdUsers where userId = p_createdBy;

        set l_username = trim(replace(l_username, '\'', '\'\''));

        if p_userId = 0 then

            insert phdUsers (
                firstName,
                lastName,
                email,
                salt,
                password,
                accessKey,
                active,
                status,
                created,
                lastUpdate,
				mailSent
            ) values (
                p_firstName,
                p_lastName,
                p_email,
                p_salt,
                '',
                p_accessKey,
                p_active,
                p_status,
                utc_timestamp(),
                utc_timestamp(),
				p_mailSent
            );

            select last_insert_id() into l_userId;

            set l_message=concat('{"NewUser":{\n',
                                    '"firstName":"', replace(ifnull(p_firstName, 'NULL'), '"', '\\"'), '",\n',
                                    '"lastName":"', replace(ifnull(p_lastName, 'NULL'), '"', '\\"'), '",\n',
                                    '"email":"', ifnull(p_email, 'NULL'), '",\n',
                                    '"salt":"', l_salt, '",\n',
                                    '"accessKey":"', l_accessKey, '",\n',
                                    '"active":', ifnull(p_active, 0), ',\n',
                                    '"status":', ifnull(p_status, 0), ',\n',
                                    '"mailSent":', ifnull(p_mailSent, 0), ',\n',
                                    '"changedBy":"', replace(l_userName, '"', '\\"'), '"}}');
								
        else

            set l_query = 'update phdUsers set ';
            set l_message = '{"OldUser":{\n';

            if p_firstName is not null then

                set l_query = concat(l_query, 'firstName=');

                if p_firstName = '' then
                    set l_query = concat(l_query, 'null');
                    set l_message = concat(l_message,'"firstName":"NULL"');
                else
                    set l_element = replace(p_firstName, '\'', '\'\'');

                    set l_query = concat(l_query, '\'', l_element, '\'');
                    set l_message = concat(l_message,'"firstName":"', replace(l_element, '"', '\\"'), '"');

                end if;
            end if;

            if p_lastName is not null then

                if l_message != '{"OldUser":{\n' then
                    set l_query = concat(l_query, ',');
                    set l_message = concat(l_message, ',\n');
                end if;

                set l_query = concat(l_query, 'lastName=');

                if p_lastName = '' then
                    set l_query = concat(l_query, 'null');
                    set l_message = concat(l_message,'"lastName":"NULL"');
                else
                    set l_element = replace(p_lastName, '\'', '\'\'');

                    set l_query = concat(l_query, '\'', l_element, '\'');
                    set l_message = concat(l_message,'"lastName":"', replace(l_element, '"', '\\"'), '"');

                end if;
            end if;

            if p_email is not null then

                if l_message != '{"OldUser":{\n' then
                    set l_query = concat(l_query, ',');
                    set l_message = concat(l_message, ',\n');
                end if;

                set l_query = concat(l_query, 'email=');

                if p_email = '' then
                    set l_query = concat(l_query, 'null');
                    set l_message = concat(l_message,'"email":"NULL"');
                else
                    set l_element = replace(p_email, '\'', '\'\'');

                    set l_query = concat(l_query, '\'', l_element, '\'');
                    set l_message = concat(l_message,'"email":"', replace(l_element, '"', '\\"'), '"');

                end if;
            end if;

            if p_active is not null then
                if l_message != '{"OldUser":{\n' then
                    set l_query = concat(l_query, ',');
                    set l_message = concat(l_message, ',\n');
                end if;

                set l_query = concat(l_query, 'active=', p_active);
                set l_message = concat(l_message, '"active":', p_active);
            end if;

            if p_status is not null then
                if l_message != '{"OldUser":{\n' then
                    set l_query = concat(l_query, ',');
                    set l_message = concat(l_message, ',\n');
                end if;

                set l_query = concat(l_query, 'status=', p_status);
                set l_message = concat(l_message, '"status":', p_status);
            end if;

            if p_salt is not null then
                if l_message != '{"OldUser":{\n' then
                    set l_query = concat(l_query, ',');
                    set l_message = concat(l_message, ',\n');
                end if;

                set l_query = concat(l_query, 'salt=\'', p_salt, '\'');
                set l_message = concat(l_message, '"salt":"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"');

            end if;

            if p_accessKey is not null then
                if l_message != '{"OldUser":{\n' then
                    set l_query = concat(l_query, ',');
                    set l_message = concat(l_message, ',\n');
                end if;

                set l_query = concat(l_query, 'accessKey=\'', p_accessKey, '\'');
                set l_message = concat(l_message, '"accessKey":"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"');

            end if;

            if p_mailSent = 1 then
                if l_message != '{"OldUser":{\n' then
                    set l_query = concat(l_query, ',');
                    set l_message = concat(l_message, ',\n');
                end if;

                set l_query = concat(l_query, 'mailSent=1');
                set l_message = concat(l_message, '"mailSent":', p_mailSent);
            end if;

            if l_query = 'update phdUsers set ' and l_message = '{"OldUser":{\n' then
                set l_message = '';
            elseif l_query != 'update phdUsers set ' then
                set l_query = concat(l_query, ', lastUpdate=utc_timestamp() where userId=', l_userId, ';');

                set l_message = concat(l_message, ',\n"changedBy":"', replace(l_username, '"', '\\"'), '" } }');

                set @statement = l_query;
                prepare stmt from @statement;
                execute stmt;
                deallocate prepare stmt;

            end if;
        end if;
    end if;

    if l_message != '' then
        insert activityLogs (
            userId,
            message,
            created
        ) values (
            l_userId,
            l_message,
            utc_timestamp()
        );

        if p_userId = 0 then
            set l_message = '{"Update":{"Message":"Created new user with userId:';
        else
            set l_message = '{"Update":{"Message":"Updated user with userId:';
        end if;

        insert activityLogs (
            userId,
            message,
            created
        ) values (
            p_createdBy,
            concat(l_message, l_userId, '"}}'),
            utc_timestamp()
        );

    end if;

    if p_suppressFlag = 0 then
        select
            l_userId as userId,
            l_errorFlag as errorFlag,
            l_errorMessage as errorMessage;
    end if;
	
end //

delimiter ;
