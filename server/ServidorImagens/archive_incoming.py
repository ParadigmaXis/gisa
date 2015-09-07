import os
import re
import shutil
import sys, getopt
import datetime
from pwd import getpwnam

"""
script responsavel por agrupar ficheiros (segundo o padrao ddd.ddd.png ou dddd-dddd.jpg
dddd_ddd.tif e suas combinacoes) em pastas e move-las para um directorio de destino
"""

class HandleFilesAndFolders(object):
    
    source_folder = ""
    destination_folder = ""

    def __init__(self, source, destination):
        self.source_folder = source
        self.destination_folder = destination

    def get_list(self):
        return os.listdir(self.source_folder)

    def group_files(self, tree, currentFolder):
        self.print_log("Grouping files...")
        regCompile = re.compile("[0-9]+[-|_|.][0-9]+.\.*", re.IGNORECASE)

        for obj in tree:
            objAbsPath = os.path.join(currentFolder, obj)
            if os.path.isdir(objAbsPath):
                #print "dirName: " + obj
                objFolder = os.path.join(currentFolder, obj)
                lst = os.listdir(objFolder)
                if len(lst) > 0:
                    self.group_files(lst, objFolder)
            else:
                #print "fileName: " + obj
                if re.match(regCompile, obj):
                    self.group(obj, currentFolder)

    def group(self, fileObj, currentFolder):
        self.print_log("Current file (" + currentFolder +") : " + fileObj)
        procNr = fileObj.split('-')[0]
        #print "procNr: " + procNr
        filePath = os.path.join(currentFolder, fileObj)
        #print "filePath: " + filePath
        newdirPath = os.path.join(currentFolder, procNr)
        #print "newdirPath: " + newdirPath
        
        if not os.path.exists(newdirPath):
            os.makedirs(newdirPath)
            self.print_log("Folder created: " + newdirPath)
            
        #print "selDir: " + newdirPath
        #print "path: " + filePath
        shutil.move(filePath, newdirPath)

    def move_tree(self, tree, destination_folder):
        c = 0
        self.print_log("Files/directories moved: ")
        for obj in tree:
            shutil.move(os.path.join(self.source_folder, obj), destination_folder)
            self.print_log(obj)
            c += 1
        self.print_log("TOTAL: " + str(c))

    def delete_symlink(self, folder):
        if os.path.exists(folder):
            os.unlink(folder)

    def create_symlink(self, folder):
        if not os.path.exists(folder):
            os.symlink(self.source_folder, folder)
            self.change_owner_path(folder, False)

    def change_owner_path(self, path, topdown = True):
        if not topdown:
            os.chown(path, getpwnam('www-data').pw_uid, getpwnam('www-data').pw_uid)
        else:
            for root, dirs, files in os.walk(path):  
                for momo in dirs:  
                    os.chown(os.path.join(root, momo), getpwnam('www-data').pw_uid, getpwnam('www-data').pw_uid)
                for momo in files:
                    os.chown(os.path.join(root, momo), getpwnam('www-data').pw_uid, getpwnam('www-data').pw_uid)

    def print_log(self, msg):
        print msg

def main(argv):
    source = ""
    destination = ""
    help_str = "archive_images.py -s <source_directory> -d  <destination_directory>"

    try:
        opts, args = getopt.getopt(argv, "hs:d:", ["sdir=", "ddir="])
    except getopt.GetoptError:
        print help_str
        sys.exit(2)

    if len(opts) != 2:
        print "Numero de argumentos invalido: " + str(len(opts)) + " (esperados 2 argumentos)"
        print help_str
        sys.exit(2)

    for opt, arg in opts:
        if opt == "-h":
             print help_str
             sys.exit(0)
        elif opt in ("-s", "--sdir"):
             source = arg
        elif opt in ("-d", "--ddir"):
             destination = arg

    handler = HandleFilesAndFolders(source, destination)

    now = datetime.date.today()
    tomorrow = now + datetime.timedelta(days = 1)

    today_folder = str(now.month).rjust(2,"0") + str(now.day).rjust(2,"0")
    tomorrow_folder = str(tomorrow.month).rjust(2,"0") + str(tomorrow.day).rjust(2,"0")

    # garantir que a pasta referente ao ano esta criada
    year_folder_path = os.path.join(destination, str(now.year))
    newyear_folder_path = ""
    if not os.path.exists(year_folder_path):
        os.mkdir(year_folder_path)
        handler.change_owner_path(year_folder_path, False)
    if now.year != tomorrow.year:
        newyear_folder_path = os.path.join(destination, str(tomorrow.year))
        if not os.path.exists(newyear_folder_path):
            os.mkdir(newyear_folder_path)
            handler.change_owner_path(newyear_folder_path, False)

    today_folder_path = os.path.join(year_folder_path, today_folder)
    tomorrow_folder_path = os.path.join(newyear_folder_path if newyear_folder_path != "" else year_folder_path, tomorrow_folder)

    # apagar symlink entre a pasta do dia actual e a pasta incoming
    handler.delete_symlink(today_folder_path)
    
    lst = handler.get_list()

    if len(lst) > 0:
        if not os.path.exists(today_folder_path):   
            os.mkdir(today_folder_path)
            handler.change_owner_path(today_folder_path, False)
        handler.group_files(lst, source)
        handler.move_tree(handler.get_list(), today_folder_path) #pede-se uma nova lista pois a actual pode estar desactualizada pelo reagrupamento de imagens
        handler.change_owner_path(today_folder_path)
    else:
        handler.print_log("No files/directories moved!")
        #apagar directorio 
        if os.path.exists(today_folder_path) and len(os.listdir(today_folder_path)) == 0:
            os.remove(today_folder_path)

    #criar symlink
    handler.create_symlink(tomorrow_folder_path)

if __name__ == "__main__":
    main(sys.argv[1:])
