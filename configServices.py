import os

# Add your DB connection details here

dbServerName = ""
dbUsername = ""
dbPassword = ""
dbName = ""

with open("EmptyWindowsService.exe.config.out", "wt") as fout:
    with open("EmptyWindowsService.exe.config", "rt") as fin:
        for line in fin:

            newLine = line.replace('\r\n', '\n')

            if ("$$MYSQL_ENDPOINT$$" in newLine)       : newLine = newLine.replace("$$MYSQL_ENDPOINT$$", dbServerName)
            if ("$$MYSQL_LOGIN$$" in newLine)          : newLine = newLine.replace("$$MYSQL_LOGIN$$", dbUsername)
            if ("$$MYSQL_PASSWORD$$" in newLine)       : newLine = newLine.replace("$$MYSQL_PASSWORD$$", dbPassword)
            if ("$$MYSQL_DATABASE$$" in newLine)       : newLine = newLine.replace("$$MYSQL_DATABASE$$", dbName)

            fout.write(newLine)

os.remove("EmptyWindowsService.exe.config")
os.rename("EmptyWindowsService.exe.config.out", "EmptyWindowsService.exe.config")
