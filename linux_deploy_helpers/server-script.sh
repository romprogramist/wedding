#!/bin/bash

# rename all <your_project_name> entries and put this file on server
  
sudo service your_project_name stop
sudo unzip -o your_project_name.zip -d /var/www/your_project_name # your project folder on server
sudo rm your_project_name.zip
sudo service your_project_name start