import os;

dbServerName = "";
dbUsername = "";
dbPassword = "";
dbName = "";

with open("EmptyWindowsService.exe.config.out", "wt") as fout:
    with open("EmptyWindowsService.exe.config", "rt") as fin:
        for line in fin:

            line = line.replace('\r\n', '\n');

            if ("$$MYSQL_ENDPOINT$$" in line)       : fout.write(line.replace("$$MYSQL_ENDPOINT$$", dbServerName));
            elif ("$$MYSQL_LOGIN$$" in line)        : fout.write(line.replace("$$MYSQL_LOGIN$$", dbUsername));
            elif ("$$MYSQL_PASSWORD$$" in line)     : fout.write(line.replace("$$MYSQL_PASSWORD$$", dbPassword));
            elif ("$$MYSQL_DATABASE$$" in line)      : fout.write(line.replace("$$MYSQL_DATABASE$$", dbName));
            else                                    : fout.write(line);

os.remove("EmptyWindowsService.exe.config");
os.rename("EmptyWindowsService.exe.config.out", "EmptyWindowsService.exe.config");
