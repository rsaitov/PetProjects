import os
from os.path import expanduser
from datetime import datetime

userDirectory = expanduser("~")
downloadDirectory = f"{userDirectory}\Downloads"

now = datetime.now()
todayFolderName = now.strftime("%Y %m %d")
absoluteTodayFolderName = os.path.join(downloadDirectory, todayFolderName)

if (os.path.exists(absoluteTodayFolderName)):
    print(f"Path '{absoluteTodayFolderName}' already exists")
else:
    print("Path '{absoluteTodayFolderName}' not exists. Creating...")
    os.mkdir(absoluteTodayFolderName)

# TODO: get all folders from downloadDirectory and delete the empty ones