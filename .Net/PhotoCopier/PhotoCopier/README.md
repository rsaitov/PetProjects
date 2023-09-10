# PhotoCopier

Allows move files to folders by predefined rules.

[C#] [.net 7.0] [console] [IO]

My devices use [Yandex.Disk](https://360.yandex.com/disk/) for automatic upload camera photo and video files. It stores all files to one folder. I like to structure my media-content and put them to folders chronologically.
This utility allows to move files to folders automatically, according to their dates and some internal rules. Some of them moved to config file __appsettings.json__, some are hardcoded in services.

I just need to execute utility on my upload folder for files. Some files will be moved to separate folders, which mean some event. I need only to append name of that event to folder name. Other files will be located to common file folder with division by months.

Now structuring my files takes only some minutes instead of hours.

Further enhancements:

- move all hardcoded settings to config file;
- get date/time from media-file metadata, not from file name. This will allow to use photo from any source with any file names;
- try to use AI photo analyzer for naming the event of the created folder.
