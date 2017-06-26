Parse the file names of all mp3 files in a folder (and subfolders).

If the file name does not begin with the two-digit track number, prepend the two-digit track number
and a space to the file name and rename the file.

This is good for mp3 players that do not use the metadata to determine track order, only the file name.  Like the stupid one in my car.

Usage:  AddMp3TrackNumber folder_name [rename]
Add [rename] parameter to make changes, otherwise will just print the names to change.
