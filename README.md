# EmptyWindowsService
Empty Windows Service integrated with MySQL DB - example to visit cities on the west coast for some Lemon Margaritas!

1. Clone this source to your local drive. You just need Visual Studio 2013 Express for the Windows Desktop
2. Update configServices.py to add your local database - hostname, database-name, login and password
3. Build this in your Visual Studio environment.
4. You can run this Windows Service as a native debugged application via a -debug switch in the command line (update App.config with your sensitive details from point 2 above, for this scenario)
5. This can be configured for start times, wait times between subsequent runs and extend this code as you want; for whatever tasks you need that can be thrown to a Windows Service.
6. To install as a Windows Service, run runFinal.cmd in the end in the bin/Debug folder after you compile the source, that updates the EmptyWindowsService.exe.config with the correct sensitive parameters from the python file; and then deletes the python script
7. installutil.exe will not only register this with your NT Service list; but will fire it up too
8. See all the cities on the west coast where you can enjoy a good lemon margarita! :)
