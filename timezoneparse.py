import csv
import string
import re

def parseTimeOffset(input):
    if input == "UTC":
        outputString = "0"
    else:
        outputString = string.strip(input, "UTC")
        if outputString[0] == "+":
            outputString = outputString[1:]
        outputString = outputString.replace("?", "-")
        splitString = outputString.split(':')
        if splitString[0][0] != "-":
            splitString[0] = "+" + splitString[0]
        if splitString[0][1] == "0":
            splitString[0] = splitString[0].replace("0","")
        if splitString[0][0] == "+":
            splitString[0] = splitString[0][1:]
        outputString = splitString[0]
        print splitString
        if len(splitString) > 1:
            splitString[1] = str(float(splitString[1])/60)
            splitString[1] = splitString[1].replace("0", "")
            outputString += splitString[1]

        
    outputString += "f"
    return outputString

csvfile = open('timezones.csv', "rb")
outputfile = "output.txt"
file = open(outputfile, 'w')
reader = csv.reader(csvfile)

rownum = 0
for row in reader:
    outputString = "timezones[" + str(rownum) + '] = new TimeZoneItem(){Offset="' + parseTimeOffset(row[2])
    outputString += '", ShortName="' + row[0] + '", LongName="' + row[1] + '"};\n'
    rownum += 1
    file.write(outputString)

print rownum;
file.close()
    
