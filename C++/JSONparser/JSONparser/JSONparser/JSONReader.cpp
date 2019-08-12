#include <iostream>
#include <fstream>
#include <string>
#include <stdlib.h>
#include "JSONReader.h"
using namespace std;
namespace json {
	//read
	int read(const string& filename, string* buffer) {
		ifstream file;
		file.open(filename);
		int index = 0;
		if (file.is_open())
		{
			while (!file.eof())
			{
				getline(file, buffer[index++]);
			}
			file.close();

		}
		return index;
	}
	//extract
	int extract(const string* buffer, int no_lines, int& line_number, Collection& collection) {
		//prints "name" value.
		bool inObject = false;
		int j = 0;
		unsigned int quote, quote2, quote3, quote4;
		string cpy, cpy2;
		collection.nvp = 0;
		while (no_lines > line_number) {
			quote = 0;
			quote2 = 0;
			quote3 = 0;
			quote4 = 0;
			quote = buffer[line_number].find_first_of("\"");
			if (quote != string::npos) {
				quote2 = buffer[line_number].find_first_of("\"", quote + 1);
				cpy = buffer[line_number].substr(quote, quote2 - quote + 1);
				quote3 = buffer[line_number].find_first_of("\"", quote2 + 1);
				if (quote3 == string::npos) { // We have a object.
					collection.objs[collection.objects].n = cpy;
					collection.objects += 1;
					inObject = true;
					line_number += 1;
					return 1;
				}
				else { // We have a namev.
					quote4 = buffer[line_number].find_first_of("\"", quote3 + 1);
					cpy2 = buffer[line_number].substr(quote3, quote4 - quote3 + 1);
					if (j == 0) {
						while (collection.objects > j) {
							if (collection.objs[j].n.length() > 0 && collection.objs[j].cnt == 0) {
								inObject = true;
								break;
							}
							j++;
						}
					}
					if (inObject == true) {
						collection.objs[j].namevalue[collection.objs[j].cnt].n = cpy;
						collection.objs[j].namevalue[collection.objs[j].cnt].v = cpy2;
						collection.objs[j].cnt += 1;
					}
					else {
						collection.namevalue[collection.nvp].n = cpy;
						collection.namevalue[collection.nvp].v = cpy2;
						collection.nvp += 1;
					}
				}
			}
			line_number += 1;
		}
		if (inObject == true) {
			inObject = false;
		}
		//cout << collection.CollectionName;
		//cout << buffer[line_number];

		return 0;

	}
	int print(const Collection& collection) {
		int i;
		int j;
		int count = 0;
		for (i = 0; i < collection.nvp; i++) {
			cout << "     " << collection.namevalue[i].n << "=>" << collection.namevalue[i].v << endl;
			count += 1;
		}
		for (i = 0; i < collection.objects; i++) {
			cout << collection.objs[i].n << endl;
			for (j = 0; j < collection.objs[i].cnt; j++) {
				cout << collection.objs[i].namevalue[j].n << "=>" << collection.objs[i].namevalue[j].v << endl;
				count += 1;
			}
		}
		return count;
	}
}

