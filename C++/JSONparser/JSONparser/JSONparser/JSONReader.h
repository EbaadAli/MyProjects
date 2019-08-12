#pragma once
#include <iostream>
#define SIZE 50
#ifndef _JSONReader_h_
#define _JSONReader_h_
using namespace std;

namespace json {

	struct nameV {
		string n;
		string v;
	};
	struct obj {
		string n;
		int cnt;
		nameV namevalue[SIZE];
	};
	struct Collection {
		string name;
		string value;
		int nvp;
		int objects;
		obj objs[SIZE];
		nameV namevalue[SIZE];
	};


	int read(const string&, string*);
	int extract(const string*, int, int&, Collection&);
	int print(const Collection&);
}
#endif
